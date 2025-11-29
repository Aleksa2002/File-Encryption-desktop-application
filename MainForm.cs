using System.Diagnostics;
using System.Text;
using ZastitaInformacija.Core.Algorithms.Crypto;
using ZastitaInformacija.Core.Algorithms.Hash;
using ZastitaInformacija.Core.Interfaces;
using ZastitaInformacija.Core.Services;
using ZastitaInformacija.Core.Settings;
using ZastitaInformacija.Core.Utilities;

namespace ZastitaInformacija
{
    public partial class MainForm : Form
    {
        private readonly IFileWatcherService _watcherService;
        private readonly ISettingsManagerService _settingsManagerService;
        private readonly IFileTransferService _fileTransferService;
        private readonly IFileEncryptionService _fileEncryptionService;
        private readonly AppSettings _appSettings;
        private ICryptoAlgorithm _cryptoAlgorithm = new Enigma(); // Default algorithm
        private CancellationTokenSource? _listenerCts;

        private const int DEFUALT_PORT = 5000;
        public MainForm()
        {
            InitializeComponent();
            _watcherService = new FileWatcherService(OnFileCreated);
            _fileTransferService = new FileTransferService();
            _settingsManagerService = new SettingsManagerService();
            _fileEncryptionService = new FileEncryptionService();
            _appSettings = _settingsManagerService.Load();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            AddNewNotification("Welcome to the File Encryption App! 🎉 ✨");
            AddNewNotification($"Logged in at 📶: {DateTime.Now:yyyy-MM-dd HH:mm:ss} 📆");
            TargerFolderTextBox.Text = _appSettings.TargetFolder ?? "No folder selected";
            XFolderTextBox.Text = _appSettings.XFolder ?? "No folder selected";

            if (!string.IsNullOrEmpty(_appSettings.TargetFolder))
            {
                _watcherService.SetWatcherPath(_appSettings.TargetFolder);

                if (!string.IsNullOrEmpty(_appSettings.XFolder))
                    FileWatcherCheckBox.Enabled = true;
            }
            FileWatcherCheckBox.Checked = _appSettings.IsWatcherEnabled;
            FilesGroupBox.Enabled = !FileWatcherCheckBox.Checked; // Enable the file selection group box based on checkbox state
        }
        private void OnFileCreated(string filePath)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnFileCreated(filePath)));
                return;
            }
            _fileEncryptionService.EncryptFile(
                filePath: filePath,
                outputFolderPath: _appSettings.XFolder,
                algorithm: _cryptoAlgorithm,
                logMessage: msg => BeginInvoke(() => AddNewNotification(msg)));
        }
        private void TargetFolderButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if (!string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
            {
                _watcherService.SetWatcherPath(folderBrowserDialog1.SelectedPath); // Start a new watcher with the selected path
                _watcherService.SetIsWatcherEnabled(_appSettings.IsWatcherEnabled); // Reattach the event handler

                if (!string.IsNullOrEmpty(_appSettings.XFolder))
                    FileWatcherCheckBox.Enabled = true; // Enable the radio button if X folder is selected

                _appSettings.TargetFolder = folderBrowserDialog1.SelectedPath; // Save the selected path to settings
                TargerFolderTextBox.Text = _appSettings.TargetFolder;
            }
        }
        private void XFolderButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if (!string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
            {
                if (!string.IsNullOrEmpty(_appSettings.TargetFolder))
                    FileWatcherCheckBox.Enabled = true; // Enable the radio button if X folder is selected

                if (!string.IsNullOrEmpty(SelectFileTextBox1.Text))
                    EncryptButton.Enabled = true; // Enable the encrypt button if x folder is selected

                _appSettings.XFolder = folderBrowserDialog1.SelectedPath; // Save the selected path to settings
                XFolderTextBox.Text = _appSettings.XFolder;
            }
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _settingsManagerService.Save(_appSettings); // Save settings on form close
        }
        private void FileWatcherCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _watcherService.SetIsWatcherEnabled(FileWatcherCheckBox.Checked); // Enable or disable the watcher based on checkbox state
            if (FileWatcherCheckBox.Checked)
            {
                AddNewNotification("File watcher started 🚀 📈.");
                AddNewNotification($"Watching folder 👁 📂: {_appSettings.TargetFolder}.");
                AddNewNotification($"Encrypted files will be saved to folder 📂: {_appSettings.XFolder}.");
            }
            else
            {
                AddNewNotification("File watcher stopped ⏸ 📉.");
            }
            _appSettings.IsWatcherEnabled = FileWatcherCheckBox.Checked; // Update settings
            FilesGroupBox.Enabled = !FileWatcherCheckBox.Checked; // Enable the file selection group box based on checkbox state
        }
        private void SelectFileButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (File.Exists(openFileDialog1.FileName))
            {
                DecryptButton.Enabled = true;
                SelectFileTextBox1.Text = openFileDialog1.FileName;
                if (!string.IsNullOrEmpty(_appSettings.XFolder))
                    EncryptButton.Enabled = true;
            }
        }
        private void DecryptButton_Click(object sender, EventArgs e)
        {
            _fileEncryptionService.DecryptFile(
                filePath: SelectFileTextBox1.Text,
                selectOutputFolder: () => 
                {
                    var asyncResult = BeginInvoke(() =>
                    {
                        folderBrowserDialog1.ShowDialog();
                        return folderBrowserDialog1.SelectedPath;
                    });
                    return (string?)EndInvoke(asyncResult);
                },
                logMessage: msg => BeginInvoke(() => AddNewNotification(msg)));
        }
        private void EncryptButton_Click(object sender, EventArgs e)
        {
            _fileEncryptionService.EncryptFile(
                filePath: SelectFileTextBox1.Text,
                outputFolderPath: _appSettings.XFolder,
                algorithm: _cryptoAlgorithm,
                logMessage: msg => BeginInvoke(() => AddNewNotification(msg)));
        }
        private void EnigmaRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (EnigmaRadioButton.Checked)
                _cryptoAlgorithm = new Enigma();
        }
        private void XXTEARadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (XXTEARadioButton.Checked)
                _cryptoAlgorithm = new XXTEA();
        }
        private void XXTEACFBRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (XXTEACFBRadioButton.Checked)
                _cryptoAlgorithm = new XXTEACFB();
        }
        private void SelectFileButton2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (File.Exists(openFileDialog1.FileName))
            {
                SelectFileTextBox2.Text = openFileDialog1.FileName;
                if (!string.IsNullOrEmpty(PortTextBox.Text)
                    && !string.IsNullOrEmpty(IpAdressTextBox.Text))
                    SendFileButton.Enabled = true;
            }
        }
        private void IpAdressTextBox_TextChanged(object sender, EventArgs e)
        {
            if (IpAdressTextBox.Text.Length > 0
                && !string.IsNullOrEmpty(PortTextBox.Text)
                && !string.IsNullOrEmpty(SelectFileTextBox2.Text))
            {
                SendFileButton.Enabled = true;
            }
            else
            {
                SendFileButton.Enabled = false;
            }
        }
        private void PortTextBox_TextChanged(object sender, EventArgs e)
        {
            if (PortTextBox.Text.Length > 0
                && !string.IsNullOrEmpty(IpAdressTextBox.Text)
                && !string.IsNullOrEmpty(SelectFileTextBox2.Text))
            {
                SendFileButton.Enabled = true;
            }
            else
            {
                SendFileButton.Enabled = false;
            }
        }
        private async void PortListeningCheckBox_CheckedChangedAsync(object sender, EventArgs e)
        {
            if (PortListeningCheckBox.Checked)
            {
                _listenerCts = new CancellationTokenSource();
                await _fileTransferService.StartListenerAsync(
                    port: DEFUALT_PORT,
                    selectOutputFolder: () =>
                    {
                        var asyncResult = BeginInvoke(() =>
                        {
                            MessageBox.Show(
                                $"You just received a new file 🎁 🎉." + Environment.NewLine + Environment.NewLine + "Please select folder for received file 📂.",
                                "New File Received",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            folderBrowserDialog1.ShowDialog();
                            return folderBrowserDialog1.SelectedPath;
                        });
                        return (string?)EndInvoke(asyncResult);
                    },
                    algorithmFactory: algId => Mappings.MapAlgorithm(algId),
                    logMessage: msg => BeginInvoke(() => AddNewNotification(msg)),
                    ct: _listenerCts.Token);
            }
            else
            {
                _listenerCts?.Cancel();
                _listenerCts = null;
            }
        }
        private async void SendFileButton_ClickAsync(object sender, EventArgs e)
        {
            var filePath = SelectFileTextBox2.Text;
            var ipAddress = IpAdressTextBox.Text;
            var isParsed = int.TryParse(PortTextBox.Text, out int port);
            if (File.Exists(filePath) && isParsed)
            {
                await _fileTransferService.SendFileAsync(
                    host: ipAddress,
                    port: port,
                    filePath: filePath,
                    algorithm: _cryptoAlgorithm,
                    tiger: new TigerHash(),
                    logMessage: msg => BeginInvoke(() => AddNewNotification(msg)));

            }
        }
        private void AddNewNotification(string msg)
        {
            NotificationsTextBox.AppendText(msg + Environment.NewLine + Environment.NewLine);
        }
    }
}
