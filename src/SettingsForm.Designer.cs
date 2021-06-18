
namespace CloudflareTunnelRunner
{
    partial class SettingsForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.settingsGroup = new System.Windows.Forms.GroupBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.endpointTextBox = new System.Windows.Forms.TextBox();
            this.endpointLabel = new System.Windows.Forms.Label();
            this.domainTextBox = new System.Windows.Forms.TextBox();
            this.domainLabel = new System.Windows.Forms.Label();
            this.settingsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsGroup
            // 
            this.settingsGroup.Controls.Add(this.saveButton);
            this.settingsGroup.Controls.Add(this.portTextBox);
            this.settingsGroup.Controls.Add(this.portLabel);
            this.settingsGroup.Controls.Add(this.endpointTextBox);
            this.settingsGroup.Controls.Add(this.endpointLabel);
            this.settingsGroup.Controls.Add(this.domainTextBox);
            this.settingsGroup.Controls.Add(this.domainLabel);
            this.settingsGroup.Location = new System.Drawing.Point(12, 12);
            this.settingsGroup.Name = "settingsGroup";
            this.settingsGroup.Size = new System.Drawing.Size(478, 289);
            this.settingsGroup.TabIndex = 8;
            this.settingsGroup.TabStop = false;
            this.settingsGroup.Text = "Settings";
            this.settingsGroup.Enter += new System.EventHandler(this.settingsGroupBox_Enter);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(293, 176);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 14;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(182, 147);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(186, 23);
            this.portTextBox.TabIndex = 13;
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(111, 150);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(29, 15);
            this.portLabel.TabIndex = 12;
            this.portLabel.Text = "Port";
            // 
            // endpointTextBox
            // 
            this.endpointTextBox.Location = new System.Drawing.Point(182, 118);
            this.endpointTextBox.Name = "endpointTextBox";
            this.endpointTextBox.Size = new System.Drawing.Size(186, 23);
            this.endpointTextBox.TabIndex = 11;
            // 
            // endpointLabel
            // 
            this.endpointLabel.AutoSize = true;
            this.endpointLabel.Location = new System.Drawing.Point(111, 121);
            this.endpointLabel.Name = "endpointLabel";
            this.endpointLabel.Size = new System.Drawing.Size(55, 15);
            this.endpointLabel.TabIndex = 10;
            this.endpointLabel.Text = "Endpoint";
            // 
            // domainTextBox
            // 
            this.domainTextBox.Location = new System.Drawing.Point(182, 89);
            this.domainTextBox.Name = "domainTextBox";
            this.domainTextBox.Size = new System.Drawing.Size(186, 23);
            this.domainTextBox.TabIndex = 9;
            // 
            // domainLabel
            // 
            this.domainLabel.AutoSize = true;
            this.domainLabel.Location = new System.Drawing.Point(111, 92);
            this.domainLabel.Name = "domainLabel";
            this.domainLabel.Size = new System.Drawing.Size(49, 15);
            this.domainLabel.TabIndex = 8;
            this.domainLabel.Text = "Domain";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 313);
            this.Controls.Add(this.settingsGroup);
            this.Name = "SettingsForm";
            this.Text = "Cloudflare Tunnel Runner";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.settingsGroup.ResumeLayout(false);
            this.settingsGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox settingsGroup;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox endpointTextBox;
        private System.Windows.Forms.Label endpointLabel;
        private System.Windows.Forms.TextBox domainTextBox;
        private System.Windows.Forms.Label domainLabel;
        private System.Windows.Forms.Button saveButton;
    }
}

