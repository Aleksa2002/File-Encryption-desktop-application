namespace ZastitaInformacija
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            AlgorithmsGroupBox = new GroupBox();
            FilesGroupBox = new GroupBox();
            ChooseFileLabel = new Label();
            SelectFileButton1 = new Button();
            SelectFileTextBox1 = new TextBox();
            EncryptButton = new Button();
            DecryptButton = new Button();
            XXTEACFBRadioButton = new RadioButton();
            XXTEARadioButton = new RadioButton();
            EnigmaRadioButton = new RadioButton();
            groupBox2 = new GroupBox();
            PortListeningCheckBox = new CheckBox();
            CooseFolderLabel = new Label();
            XFolderTextBox = new TextBox();
            TargerFolderTextBox = new TextBox();
            FileWatcherCheckBox = new CheckBox();
            XFolderButton = new Button();
            TargetFolderButton = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            openFileDialog1 = new OpenFileDialog();
            groupBox4 = new GroupBox();
            SelectFileButton2 = new Button();
            SelectFileTextBox2 = new TextBox();
            SendFileButton = new Button();
            PortTextBox = new TextBox();
            IpAdressTextBox = new TextBox();
            IpAddressLabel = new Label();
            PortLabel = new Label();
            groupBox5 = new GroupBox();
            NotificationsTextBox = new RichTextBox();
            AlgorithmsGroupBox.SuspendLayout();
            FilesGroupBox.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // AlgorithmsGroupBox
            // 
            AlgorithmsGroupBox.Controls.Add(FilesGroupBox);
            AlgorithmsGroupBox.Controls.Add(XXTEACFBRadioButton);
            AlgorithmsGroupBox.Controls.Add(XXTEARadioButton);
            AlgorithmsGroupBox.Controls.Add(EnigmaRadioButton);
            AlgorithmsGroupBox.Font = new Font("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AlgorithmsGroupBox.Location = new Point(12, 12);
            AlgorithmsGroupBox.Name = "AlgorithmsGroupBox";
            AlgorithmsGroupBox.Size = new Size(584, 205);
            AlgorithmsGroupBox.TabIndex = 0;
            AlgorithmsGroupBox.TabStop = false;
            AlgorithmsGroupBox.Text = "Algorithms";
            // 
            // FilesGroupBox
            // 
            FilesGroupBox.Controls.Add(ChooseFileLabel);
            FilesGroupBox.Controls.Add(SelectFileButton1);
            FilesGroupBox.Controls.Add(SelectFileTextBox1);
            FilesGroupBox.Controls.Add(EncryptButton);
            FilesGroupBox.Controls.Add(DecryptButton);
            FilesGroupBox.Enabled = false;
            FilesGroupBox.Location = new Point(152, 25);
            FilesGroupBox.Name = "FilesGroupBox";
            FilesGroupBox.Size = new Size(410, 162);
            FilesGroupBox.TabIndex = 6;
            FilesGroupBox.TabStop = false;
            FilesGroupBox.Text = "Files";
            // 
            // ChooseFileLabel
            // 
            ChooseFileLabel.AutoSize = true;
            ChooseFileLabel.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ChooseFileLabel.Location = new Point(143, 39);
            ChooseFileLabel.Name = "ChooseFileLabel";
            ChooseFileLabel.Size = new Size(91, 14);
            ChooseFileLabel.TabIndex = 10;
            ChooseFileLabel.Text = "Choose file:";
            // 
            // SelectFileButton1
            // 
            SelectFileButton1.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SelectFileButton1.Location = new Point(19, 78);
            SelectFileButton1.Name = "SelectFileButton1";
            SelectFileButton1.Size = new Size(119, 22);
            SelectFileButton1.TabIndex = 9;
            SelectFileButton1.Text = "Select file";
            SelectFileButton1.UseVisualStyleBackColor = true;
            SelectFileButton1.Click += SelectFileButton1_Click;
            // 
            // SelectFileTextBox1
            // 
            SelectFileTextBox1.BackColor = SystemColors.Control;
            SelectFileTextBox1.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SelectFileTextBox1.Location = new Point(166, 78);
            SelectFileTextBox1.Name = "SelectFileTextBox1";
            SelectFileTextBox1.PlaceholderText = " No file selected";
            SelectFileTextBox1.ReadOnly = true;
            SelectFileTextBox1.Size = new Size(221, 22);
            SelectFileTextBox1.TabIndex = 8;
            // 
            // EncryptButton
            // 
            EncryptButton.Enabled = false;
            EncryptButton.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            EncryptButton.Location = new Point(166, 119);
            EncryptButton.Name = "EncryptButton";
            EncryptButton.Size = new Size(98, 22);
            EncryptButton.TabIndex = 5;
            EncryptButton.Text = "Encrypt";
            EncryptButton.UseVisualStyleBackColor = true;
            EncryptButton.Click += EncryptButton_Click;
            // 
            // DecryptButton
            // 
            DecryptButton.Enabled = false;
            DecryptButton.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DecryptButton.Location = new Point(289, 119);
            DecryptButton.Name = "DecryptButton";
            DecryptButton.Size = new Size(98, 22);
            DecryptButton.TabIndex = 4;
            DecryptButton.Text = "Decrypt";
            DecryptButton.UseVisualStyleBackColor = true;
            DecryptButton.Click += DecryptButton_Click;
            // 
            // XXTEACFBRadioButton
            // 
            XXTEACFBRadioButton.AutoSize = true;
            XXTEACFBRadioButton.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            XXTEACFBRadioButton.Location = new Point(28, 148);
            XXTEACFBRadioButton.Name = "XXTEACFBRadioButton";
            XXTEACFBRadioButton.Size = new Size(102, 18);
            XXTEACFBRadioButton.TabIndex = 2;
            XXTEACFBRadioButton.Text = "XXTea (CFB)";
            XXTEACFBRadioButton.UseVisualStyleBackColor = true;
            XXTEACFBRadioButton.CheckedChanged += XXTEACFBRadioButton_CheckedChanged;
            // 
            // XXTEARadioButton
            // 
            XXTEARadioButton.AutoSize = true;
            XXTEARadioButton.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            XXTEARadioButton.Location = new Point(28, 102);
            XXTEARadioButton.Name = "XXTEARadioButton";
            XXTEARadioButton.Size = new Size(60, 18);
            XXTEARadioButton.TabIndex = 1;
            XXTEARadioButton.Text = "XXTea";
            XXTEARadioButton.UseVisualStyleBackColor = true;
            XXTEARadioButton.CheckedChanged += XXTEARadioButton_CheckedChanged;
            // 
            // EnigmaRadioButton
            // 
            EnigmaRadioButton.AutoSize = true;
            EnigmaRadioButton.Checked = true;
            EnigmaRadioButton.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            EnigmaRadioButton.Location = new Point(28, 60);
            EnigmaRadioButton.Name = "EnigmaRadioButton";
            EnigmaRadioButton.Size = new Size(67, 18);
            EnigmaRadioButton.TabIndex = 0;
            EnigmaRadioButton.TabStop = true;
            EnigmaRadioButton.Text = "Enigma";
            EnigmaRadioButton.UseVisualStyleBackColor = true;
            EnigmaRadioButton.CheckedChanged += EnigmaRadioButton_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(PortListeningCheckBox);
            groupBox2.Controls.Add(CooseFolderLabel);
            groupBox2.Controls.Add(XFolderTextBox);
            groupBox2.Controls.Add(TargerFolderTextBox);
            groupBox2.Controls.Add(FileWatcherCheckBox);
            groupBox2.Controls.Add(XFolderButton);
            groupBox2.Controls.Add(TargetFolderButton);
            groupBox2.Font = new Font("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(12, 223);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(402, 221);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Settings";
            // 
            // PortListeningCheckBox
            // 
            PortListeningCheckBox.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PortListeningCheckBox.Location = new Point(28, 185);
            PortListeningCheckBox.Name = "PortListeningCheckBox";
            PortListeningCheckBox.Size = new Size(124, 18);
            PortListeningCheckBox.TabIndex = 14;
            PortListeningCheckBox.Text = "Port listening";
            PortListeningCheckBox.UseVisualStyleBackColor = true;
            PortListeningCheckBox.CheckedChanged += PortListeningCheckBox_CheckedChangedAsync;
            // 
            // CooseFolderLabel
            // 
            CooseFolderLabel.AutoSize = true;
            CooseFolderLabel.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CooseFolderLabel.Location = new Point(140, 55);
            CooseFolderLabel.Name = "CooseFolderLabel";
            CooseFolderLabel.Size = new Size(112, 14);
            CooseFolderLabel.TabIndex = 9;
            CooseFolderLabel.Text = "Choose folders:";
            // 
            // XFolderTextBox
            // 
            XFolderTextBox.BackColor = SystemColors.Control;
            XFolderTextBox.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            XFolderTextBox.Location = new Point(192, 140);
            XFolderTextBox.Name = "XFolderTextBox";
            XFolderTextBox.PlaceholderText = " No folder selected";
            XFolderTextBox.ReadOnly = true;
            XFolderTextBox.Size = new Size(185, 22);
            XFolderTextBox.TabIndex = 8;
            // 
            // TargerFolderTextBox
            // 
            TargerFolderTextBox.BackColor = SystemColors.Control;
            TargerFolderTextBox.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TargerFolderTextBox.Location = new Point(192, 95);
            TargerFolderTextBox.Name = "TargerFolderTextBox";
            TargerFolderTextBox.PlaceholderText = " No foleder selected";
            TargerFolderTextBox.ReadOnly = true;
            TargerFolderTextBox.Size = new Size(185, 22);
            TargerFolderTextBox.TabIndex = 7;
            // 
            // FileWatcherCheckBox
            // 
            FileWatcherCheckBox.Enabled = false;
            FileWatcherCheckBox.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FileWatcherCheckBox.Location = new Point(267, 186);
            FileWatcherCheckBox.Name = "FileWatcherCheckBox";
            FileWatcherCheckBox.Size = new Size(110, 18);
            FileWatcherCheckBox.TabIndex = 6;
            FileWatcherCheckBox.Text = "File watcher";
            FileWatcherCheckBox.UseVisualStyleBackColor = true;
            FileWatcherCheckBox.CheckedChanged += FileWatcherCheckBox_CheckedChanged;
            // 
            // XFolderButton
            // 
            XFolderButton.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            XFolderButton.Location = new Point(28, 140);
            XFolderButton.Name = "XFolderButton";
            XFolderButton.Size = new Size(131, 22);
            XFolderButton.TabIndex = 3;
            XFolderButton.Text = "X Folder";
            XFolderButton.UseVisualStyleBackColor = true;
            XFolderButton.Click += XFolderButton_Click;
            // 
            // TargetFolderButton
            // 
            TargetFolderButton.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TargetFolderButton.Location = new Point(28, 95);
            TargetFolderButton.Name = "TargetFolderButton";
            TargetFolderButton.Size = new Size(131, 22);
            TargetFolderButton.TabIndex = 2;
            TargetFolderButton.Text = "Target Folder";
            TargetFolderButton.UseVisualStyleBackColor = true;
            TargetFolderButton.Click += TargetFolderButton_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(SelectFileButton2);
            groupBox4.Controls.Add(SelectFileTextBox2);
            groupBox4.Controls.Add(SendFileButton);
            groupBox4.Controls.Add(PortTextBox);
            groupBox4.Controls.Add(IpAdressTextBox);
            groupBox4.Controls.Add(IpAddressLabel);
            groupBox4.Controls.Add(PortLabel);
            groupBox4.Font = new Font("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox4.Location = new Point(435, 223);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(394, 221);
            groupBox4.TabIndex = 2;
            groupBox4.TabStop = false;
            groupBox4.Text = "File Transfer";
            // 
            // SelectFileButton2
            // 
            SelectFileButton2.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SelectFileButton2.Location = new Point(26, 141);
            SelectFileButton2.Name = "SelectFileButton2";
            SelectFileButton2.Size = new Size(119, 22);
            SelectFileButton2.TabIndex = 13;
            SelectFileButton2.Text = "Select file";
            SelectFileButton2.UseVisualStyleBackColor = true;
            SelectFileButton2.Click += SelectFileButton2_Click;
            // 
            // SelectFileTextBox2
            // 
            SelectFileTextBox2.BackColor = SystemColors.Control;
            SelectFileTextBox2.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SelectFileTextBox2.Location = new Point(183, 141);
            SelectFileTextBox2.Name = "SelectFileTextBox2";
            SelectFileTextBox2.PlaceholderText = " No file selected";
            SelectFileTextBox2.ReadOnly = true;
            SelectFileTextBox2.Size = new Size(185, 22);
            SelectFileTextBox2.TabIndex = 12;
            // 
            // SendFileButton
            // 
            SendFileButton.Enabled = false;
            SendFileButton.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SendFileButton.Location = new Point(259, 182);
            SendFileButton.Name = "SendFileButton";
            SendFileButton.Size = new Size(109, 22);
            SendFileButton.TabIndex = 11;
            SendFileButton.Text = "Send";
            SendFileButton.UseVisualStyleBackColor = true;
            SendFileButton.Click += SendFileButton_ClickAsync;
            // 
            // PortTextBox
            // 
            PortTextBox.BackColor = SystemColors.Control;
            PortTextBox.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PortTextBox.Location = new Point(183, 96);
            PortTextBox.Name = "PortTextBox";
            PortTextBox.PlaceholderText = " 5000";
            PortTextBox.Size = new Size(185, 22);
            PortTextBox.TabIndex = 9;
            PortTextBox.TextChanged += PortTextBox_TextChanged;
            // 
            // IpAdressTextBox
            // 
            IpAdressTextBox.BackColor = SystemColors.Control;
            IpAdressTextBox.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            IpAdressTextBox.Location = new Point(183, 52);
            IpAdressTextBox.Name = "IpAdressTextBox";
            IpAdressTextBox.PlaceholderText = " 127.0.0.1";
            IpAdressTextBox.Size = new Size(185, 22);
            IpAdressTextBox.TabIndex = 8;
            IpAdressTextBox.TextChanged += IpAdressTextBox_TextChanged;
            // 
            // IpAddressLabel
            // 
            IpAddressLabel.AutoSize = true;
            IpAddressLabel.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            IpAddressLabel.Location = new Point(26, 55);
            IpAddressLabel.Name = "IpAddressLabel";
            IpAddressLabel.Size = new Size(84, 14);
            IpAddressLabel.TabIndex = 1;
            IpAddressLabel.Text = "Ip address:";
            // 
            // PortLabel
            // 
            PortLabel.AutoSize = true;
            PortLabel.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PortLabel.Location = new Point(26, 99);
            PortLabel.Name = "PortLabel";
            PortLabel.Size = new Size(42, 14);
            PortLabel.TabIndex = 0;
            PortLabel.Text = "Port:";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(NotificationsTextBox);
            groupBox5.Font = new Font("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox5.Location = new Point(618, 12);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(211, 205);
            groupBox5.TabIndex = 3;
            groupBox5.TabStop = false;
            groupBox5.Text = "Notifications";
            // 
            // NotificationsTextBox
            // 
            NotificationsTextBox.BackColor = SystemColors.Control;
            NotificationsTextBox.Font = new Font("Consolas", 9F);
            NotificationsTextBox.Location = new Point(18, 35);
            NotificationsTextBox.Name = "NotificationsTextBox";
            NotificationsTextBox.ReadOnly = true;
            NotificationsTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            NotificationsTextBox.Size = new Size(174, 152);
            NotificationsTextBox.TabIndex = 17;
            NotificationsTextBox.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(842, 456);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox2);
            Controls.Add(AlgorithmsGroupBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "File Encryption App";
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            AlgorithmsGroupBox.ResumeLayout(false);
            AlgorithmsGroupBox.PerformLayout();
            FilesGroupBox.ResumeLayout(false);
            FilesGroupBox.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox AlgorithmsGroupBox;
        private RadioButton EnigmaRadioButton;
        private RadioButton XXTEACFBRadioButton;
        private RadioButton XXTEARadioButton;
        private GroupBox groupBox2;
        private Button XFolderButton;
        private Button TargetFolderButton;
        private FolderBrowserDialog folderBrowserDialog1;
        private CheckBox FileWatcherCheckBox;
        private Button DecryptButton;
        private OpenFileDialog openFileDialog1;
        private TextBox TargerFolderTextBox;
        private Button EncryptButton;
        private TextBox XFolderTextBox;
        private GroupBox FilesGroupBox;
        private Button SelectFileButton1;
        private TextBox SelectFileTextBox1;
        private GroupBox groupBox4;
        private Label IpAddressLabel;
        private Label PortLabel;
        private TextBox PortTextBox;
        private TextBox IpAdressTextBox;
        private Button SendFileButton;
        private Button SelectFileButton2;
        private TextBox SelectFileTextBox2;
        private Label CooseFolderLabel;
        private CheckBox PortListeningCheckBox;
        private Label statusLabel;
        private Label label4;
        private GroupBox groupBox5;
        private Label ChooseFileLabel;
        private RichTextBox NotificationsTextBox;
    }
}