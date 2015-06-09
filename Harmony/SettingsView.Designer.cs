using MetroFramework.Controls;

namespace Harmony
{
    partial class SettingsView
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
            this.labelCalendarId = new MetroFramework.Controls.MetroLabel();
            this.textCalendarId = new MetroFramework.Controls.MetroTextBox();
            this.buttonOK = new MetroFramework.Controls.MetroButton();
            this.buttonCancel = new MetroFramework.Controls.MetroButton();
            this.trackbarSyncInterval = new MetroFramework.Controls.MetroTrackBar();
            this.labelSyncInterval = new MetroFramework.Controls.MetroLabel();
            this.labelIntervalValue = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // labelCalendarId
            // 
            this.labelCalendarId.AutoSize = true;
            this.labelCalendarId.Location = new System.Drawing.Point(33, 83);
            this.labelCalendarId.Name = "labelCalendarId";
            this.labelCalendarId.Size = new System.Drawing.Size(109, 19);
            this.labelCalendarId.TabIndex = 0;
            this.labelCalendarId.Text = "Google Calendar";
            // 
            // textCalendarId
            // 
            this.textCalendarId.Lines = new string[0];
            this.textCalendarId.Location = new System.Drawing.Point(148, 83);
            this.textCalendarId.MaxLength = 32767;
            this.textCalendarId.Name = "textCalendarId";
            this.textCalendarId.PasswordChar = '\0';
            this.textCalendarId.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textCalendarId.SelectedText = "";
            this.textCalendarId.Size = new System.Drawing.Size(199, 20);
            this.textCalendarId.TabIndex = 1;
            this.textCalendarId.UseSelectable = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(191, 198);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseSelectable = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(272, 198);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseSelectable = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // trackbarSyncInterval
            // 
            this.trackbarSyncInterval.BackColor = System.Drawing.Color.Transparent;
            this.trackbarSyncInterval.Location = new System.Drawing.Point(148, 117);
            this.trackbarSyncInterval.Maximum = 60;
            this.trackbarSyncInterval.Minimum = 1;
            this.trackbarSyncInterval.Name = "trackbarSyncInterval";
            this.trackbarSyncInterval.Size = new System.Drawing.Size(143, 45);
            this.trackbarSyncInterval.TabIndex = 4;
            this.trackbarSyncInterval.ValueChanged += new System.EventHandler(this.trackbarSyncInterval_ValueChanged);
            // 
            // labelSyncInterval
            // 
            this.labelSyncInterval.AutoSize = true;
            this.labelSyncInterval.Location = new System.Drawing.Point(33, 128);
            this.labelSyncInterval.Name = "labelSyncInterval";
            this.labelSyncInterval.Size = new System.Drawing.Size(81, 19);
            this.labelSyncInterval.TabIndex = 7;
            this.labelSyncInterval.Text = "Sync Interval";
            // 
            // labelIntervalValue
            // 
            this.labelIntervalValue.AutoSize = true;
            this.labelIntervalValue.Location = new System.Drawing.Point(297, 128);
            this.labelIntervalValue.Name = "labelIntervalValue";
            this.labelIntervalValue.Size = new System.Drawing.Size(50, 19);
            this.labelIntervalValue.TabIndex = 8;
            this.labelIntervalValue.Text = "0:00:00";
            // 
            // SettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 249);
            this.Controls.Add(this.labelIntervalValue);
            this.Controls.Add(this.labelSyncInterval);
            this.Controls.Add(this.trackbarSyncInterval);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textCalendarId);
            this.Controls.Add(this.labelCalendarId);
            this.Name = "SettingsView";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroLabel labelCalendarId;
        private MetroTextBox textCalendarId;
        private MetroButton buttonOK;
        private MetroButton buttonCancel;
        private MetroTrackBar trackbarSyncInterval;
        private MetroLabel labelSyncInterval;
        private MetroLabel labelIntervalValue;
    }
}
