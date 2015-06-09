using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Controls;
using MetroFramework.Forms;

namespace Harmony
{
    public class AuthorisationCodeDialogView : MetroForm, IAuthorisationCodeDialogView
    {
        private MetroLabel _accessTokenLabel;
        private MetroTextBox _accessTokenText;
        private MetroButton _confirmButton;

        public AuthorisationCodeDialogView()
        {
            InitializeComponent();
        }

        public string AccessToken
        {
            get { return _accessTokenText.Text; }
        }

        public string PromptForAccessToken()
        {
            DialogResult dialogResult = ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                return AccessToken;
            }

            return null;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthorisationCodeDialogView));
            this._accessTokenLabel = new MetroFramework.Controls.MetroLabel();
            this._accessTokenText = new MetroFramework.Controls.MetroTextBox();
            this._confirmButton = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // _accessTokenLabel
            // 
            this._accessTokenLabel.AutoSize = true;
            this._accessTokenLabel.Location = new System.Drawing.Point(12, 36);
            this._accessTokenLabel.Name = "_accessTokenLabel";
            this._accessTokenLabel.Size = new System.Drawing.Size(170, 19);
            this._accessTokenLabel.TabIndex = 0;
            this._accessTokenLabel.Text = "Enter Google Access Token:";
            // 
            // _accessTokenText
            // 
            this._accessTokenText.Lines = new string[0];
            this._accessTokenText.Location = new System.Drawing.Point(188, 37);
            this._accessTokenText.MaxLength = 32767;
            this._accessTokenText.Name = "_accessTokenText";
            this._accessTokenText.PasswordChar = '\0';
            this._accessTokenText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this._accessTokenText.SelectedText = "";
            this._accessTokenText.Size = new System.Drawing.Size(332, 20);
            this._accessTokenText.TabIndex = 1;
            this._accessTokenText.UseSelectable = true;
            // 
            // _confirmButton
            // 
            this._confirmButton.Location = new System.Drawing.Point(533, 35);
            this._confirmButton.Name = "_confirmButton";
            this._confirmButton.Size = new System.Drawing.Size(58, 23);
            this._confirmButton.TabIndex = 2;
            this._confirmButton.Text = "&OK";
            this._confirmButton.UseSelectable = true;
            // 
            // AuthorisationCodeDialogView
            // 
            this.ClientSize = new System.Drawing.Size(607, 81);
            this.Controls.Add(this._confirmButton);
            this.Controls.Add(this._accessTokenText);
            this.Controls.Add(this._accessTokenLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AuthorisationCodeDialogView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Resizable = false;
            
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = string.Empty;

            _confirmButton.DialogResult = DialogResult.OK;
        }
    }
}
