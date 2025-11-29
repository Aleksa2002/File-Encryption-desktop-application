using System;
using System.Security.Cryptography;
using ZastitaInformacija.Core.Interfaces;


namespace ZastitaInformacija.Core.Algorithms.Crypto
{
    internal class XXTEACFB : ICryptoAlgorithm
    {
        private const int BlockSize = 16; // 128-bit CFB

        public byte[] GenerateRandomKey(byte[]? seed = null)
        {
            var result = new byte[BlockSize * 2]; // 16 bytes IV + 16 bytes key
            if (seed is null)
            {
                RandomNumberGenerator.Fill(result);
            }
            else
            {
                // Deterministic: SHA256(seed) gives 32 bytes exactly
                var hash = SHA256.HashData(seed);
                Buffer.BlockCopy(hash, 0, result, 0, 32);
            }
            return result;
        }

        public byte[] Encrypt(byte[] data, byte[] key)
        {
            byte[] iv = new byte[BlockSize];
            Buffer.BlockCopy(key, 0, iv, 0, BlockSize);
            var realKey = new byte[BlockSize];
            Buffer.BlockCopy(key, BlockSize, realKey, 0, BlockSize);
            return EncryptWithIv(data, realKey, iv);
        }

        public byte[] Decrypt(byte[] data, byte[] key)
        {
            if (data.Length < 0)
                throw new ArgumentException("Ciphertext too short — missing data.", nameof(data));

            byte[] iv = new byte[BlockSize];
            Buffer.BlockCopy(key, 0, iv, 0, BlockSize);
            var realKey = new byte[BlockSize];
            Buffer.BlockCopy(key, BlockSize, realKey, 0, BlockSize);
            return DecryptWithIv(data, realKey, iv);
        }

        private static byte[] EncryptWithIv(byte[] plaintext, byte[] key, byte[] iv)
        {
            if (iv is null || iv.Length != BlockSize)
                throw new ArgumentException($"IV must be {BlockSize} bytes.", nameof(iv));

            byte[] outBuf = new byte[plaintext.Length];
            byte[] prev = (byte[])iv.Clone();
            int offset = 0;
            while (offset < plaintext.Length)
            {
                byte[] keystream = XXTEA.EncryptBlock(prev, key, BlockSize);
                int chunk = Math.Min(BlockSize, plaintext.Length - offset);
                for (int i = 0; i < chunk; i++)
                {
                    byte ct = (byte)(plaintext[offset + i] ^ keystream[i]);
                    outBuf[offset + i] = ct;
                    prev[i] = ct;
                }
                if (chunk < BlockSize) break;
                offset += chunk;
            }
            return outBuf;
        }

        private static byte[] DecryptWithIv(byte[] ciphertext, byte[] key, byte[] iv)
        {
            if (iv is null || iv.Length != BlockSize)
                throw new ArgumentException($"IV must be {BlockSize} bytes.", nameof(iv));

            byte[] outBuf = new byte[ciphertext.Length];
            byte[] prev = (byte[])iv.Clone();
            int offset = 0;
            while (offset < ciphertext.Length)
            {
                byte[] keystream = XXTEA.EncryptBlock(prev, key, BlockSize);
                int chunk = Math.Min(BlockSize, ciphertext.Length - offset);
                for (int i = 0; i < chunk; i++)
                {
                    byte ct = ciphertext[offset + i];
                    byte pt = (byte)(ct ^ keystream[i]);
                    outBuf[offset + i] = pt;
                    prev[i] = ct;
                }
                if (chunk < BlockSize) break;
                offset += chunk;
            }
            return outBuf;
        }
    }
}