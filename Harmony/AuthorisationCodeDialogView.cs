using System;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof (AuthorisationCodeDialogView));
            _accessTokenLabel = new MetroLabel();
            _accessTokenText = new MetroTextBox();
            _confirmButton = new MetroButton();
            SuspendLayout();
            //
            // _accessTokenLabel
            //
            _accessTokenLabel.AutoSize = true;
            _accessTokenLabel.Location = new System.Drawing.Point(12, 36);
            _accessTokenLabel.Name = "_accessTokenLabel";
            _accessTokenLabel.Size = new System.Drawing.Size(170, 19);
            _accessTokenLabel.TabIndex = 0;
            _accessTokenLabel.Text = @"Enter Google Access Token:";
            //
            // _accessTokenText
            //
            _accessTokenText.Lines = new string[0];
            _accessTokenText.Location = new System.Drawing.Point(188, 37);
            _accessTokenText.MaxLength = 32767;
            _accessTokenText.Name = "_accessTokenText";
            _accessTokenText.PasswordChar = '\0';
            _accessTokenText.ScrollBars = ScrollBars.None;
            _accessTokenText.SelectedText = "";
            _accessTokenText.Size = new System.Drawing.Size(332, 20);
            _accessTokenText.TabIndex = 1;
            _accessTokenText.UseSelectable = true;
            //
            // _confirmButton
            //
            _confirmButton.Location = new System.Drawing.Point(533, 35);
            _confirmButton.Name = "_confirmButton";
            _confirmButton.Size = new System.Drawing.Size(58, 23);
            _confirmButton.TabIndex = 2;
            _confirmButton.Text = @"&OK";
            _confirmButton.UseSelectable = true;
            //
            // AuthorisationCodeDialogView
            //
            ClientSize = new System.Drawing.Size(607, 81);
            Controls.Add(_confirmButton);
            Controls.Add(_accessTokenText);
            Controls.Add(_accessTokenLabel);
            Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            Name = "AuthorisationCodeDialogView";
            ResumeLayout(false);
            PerformLayout();
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