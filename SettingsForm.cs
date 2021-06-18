using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudflareTunnelRunner
{
    public partial class SettingsForm : Form
    {
        private UserSettings userSettings;

        public SettingsForm()
        {
            InitializeComponent();
            userSettings = new UserSettings();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            domainTextBox.Text = userSettings.Domain;
            endpointTextBox.Text = userSettings.Endpoint;
            portTextBox.Text = userSettings.Port;
        }

        private void settingsPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void settingsGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            userSettings.Domain = domainTextBox.Text;
            userSettings.Endpoint = endpointTextBox.Text;
            userSettings.Port = portTextBox.Text;
            userSettings.Save();
            this.Close();
        }
    }
}
