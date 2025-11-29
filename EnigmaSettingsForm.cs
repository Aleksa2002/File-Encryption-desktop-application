using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ZastitaInformacija.Core.Algorithms.Crypto;

namespace ZastitaInformacija
{
    public partial class EnigmaSettingsForm : Form
    {
        private readonly List<char> _addedLetters = [];
        public EnigmaSettingsForm()
        {
            InitializeComponent();
        }
        // ...

        private void EnigmaSettingsForm_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 3; i++)
            {
                var rotorComboBox = Controls.Find($"rotor{i}ComboBox", true).FirstOrDefault();
                var positionComboBox = Controls.Find($"position{i}ComboBox", true).FirstOrDefault();
                var ringSettingComboBox = Controls.Find($"ringSettings{i}ComboBox", true).FirstOrDefault();

                if (rotorComboBox is ComboBox rcb &&
                    (positionComboBox is ComboBox pcb) &&
                    (ringSettingComboBox is ComboBox rscb))
                {
                    rcb.DataSource = Enigma.AvailableRotors.Select(r => r.Name).ToArray();               

                    rcb.SelectedIndex = i -1;

                    pcb.DataSource = Enumerable.Range(0, 26).Select(n => (char)('A' + n)).ToArray();
                    rscb.DataSource = Enumerable.Range(0, 26).Select(n => (char)('A' + n)).ToArray();

                }
            }

            plug1ComboBox.Items.AddRange([.. Enumerable.Range(0, 26).Select(n => ((char)('A' + n)).ToString())]);
            plug2ComboBox.Items.AddRange([.. Enumerable.Range(1, 26).Select(n => ((char)('A' + n)).ToString())]);

            plug1ComboBox.SelectedIndexChanged += plugComboBox_SelectedIndexChanged;
            plug2ComboBox.SelectedIndexChanged += plugComboBox_SelectedIndexChanged;

            plug1ComboBox.SelectedIndex = 0;
            plug2ComboBox.SelectedIndex = 0;
        }

        private void plugComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (sender is ComboBox plugComboBox)
            {
                var otherPlugComboBox = plugComboBox == plug1ComboBox ? plug2ComboBox : plug1ComboBox;

                otherPlugComboBox.SelectedIndexChanged -= plugComboBox_SelectedIndexChanged;

                var selectedItem = otherPlugComboBox.SelectedItem;

                otherPlugComboBox.Items.Clear();
                otherPlugComboBox.Items.AddRange([.. Enumerable.Range(0, 26).Select(n => ((char)('A' + n)).ToString())]);
                otherPlugComboBox.Items.Remove(plugComboBox.SelectedItem);
                foreach (var c in _addedLetters)
                    otherPlugComboBox.Items.Remove(c.ToString());

                otherPlugComboBox.SelectedItem = selectedItem;

                otherPlugComboBox.SelectedIndexChanged += plugComboBox_SelectedIndexChanged;
            }
        }

        private void addPairButton_Click(object sender, EventArgs e)
        {
            if (plug1ComboBox.SelectedItem == null || plug2ComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select both plugs.", "Information", 0 ,MessageBoxIcon.Information);
                return;
            }

            var selectedItem1 = plug1ComboBox.SelectedItem;
            var selectedItem2 = plug2ComboBox.SelectedItem;

            _addedLetters.AddRange(
                selectedItem1.ToString()![0],
                selectedItem2.ToString()![0]);

            plugboardListView.Items.Add($"{selectedItem1} - {selectedItem2}");

            plug1ComboBox.SelectedIndexChanged -= plugComboBox_SelectedIndexChanged;
            plug2ComboBox.SelectedIndexChanged -= plugComboBox_SelectedIndexChanged;

            plug1ComboBox.Items.Remove(selectedItem1);
            plug2ComboBox.Items.Remove(selectedItem2);
            plug1ComboBox.Items.Remove(selectedItem2);
            plug2ComboBox.Items.Remove(selectedItem1);

            plug1ComboBox.SelectedItem = null;
            plug2ComboBox.SelectedItem = null;


            plug1ComboBox.SelectedIndexChanged += plugComboBox_SelectedIndexChanged;
            plug2ComboBox.SelectedIndexChanged += plugComboBox_SelectedIndexChanged;

        }
        private void clearPairsButton_Click(object sender, EventArgs e)
        {
            _addedLetters.Clear();
            plugboardListView.Clear();

            plug1ComboBox.SelectedIndex = 0;
            plug2ComboBox.SelectedIndex = 0;

        }

        private void createKeyButton_Click(object sender, EventArgs e)
        {
            var newKey = "";
            var reflectorName = "";

            for (int i = 1; i <= 3; i++)
            {
                var rotorComboBox = Controls.Find($"rotor{i}ComboBox", true).FirstOrDefault();
                var positionComboBox = Controls.Find($"position{i}ComboBox", true).FirstOrDefault();
                var ringSettingComboBox = Controls.Find($"ringSettings{i}ComboBox", true).FirstOrDefault();
                var radioButton = Controls.Find($"radioButton{i}", true).FirstOrDefault();

                if (rotorComboBox is ComboBox rcb &&
                    (positionComboBox is ComboBox pcb) &&
                    (ringSettingComboBox is ComboBox rscb) &&
                    (radioButton is RadioButton rb))
                {
                    newKey += (rcb.SelectedIndex + 1).ToString();
                    newKey += pcb.SelectedItem!.ToString()![0];
                    newKey += rscb.SelectedItem!.ToString()![0];
                    reflectorName = rb.Checked ? rb.Text : reflectorName;
                }
            }
            newKey += reflectorName;
            _addedLetters.ForEach( c => newKey += c);

            Debug.WriteLine(newKey);
        }
    }
}
