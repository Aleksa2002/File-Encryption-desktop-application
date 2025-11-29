using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZastitaInformacija.Core.Interfaces;
using ZastitaInformacija.Core.Settings;

namespace ZastitaInformacija.Core.Services
{
    internal class SettingsManagerService : ISettingsManagerService
    {
        private static readonly string SettingsFilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

        public void Save(AppSettings settings)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(SettingsFilePath, json);
        }

        public AppSettings Load()
        {
            if (!File.Exists(SettingsFilePath))
                return new AppSettings(); // default

            string json = File.ReadAllText(SettingsFilePath);
            return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
        }
    }
}
