#region - Using Statements -

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace PasswordHolder
{
    public partial class PasswordForm : Form
    {

        #region - Fields -

        private bool _okButtonClicked;

        #endregion

        #region - Constructor -

        public PasswordForm()
        {
            InitializeComponent();
            txtPassword.Text = null;
            _okButtonClicked = false;
        }

        #endregion

        #region - Properties -

        public string PasswordText
        {
            get
            {
                return txtPassword.Text;
            }
        }

        #endregion

        #region - Event Handlers -

        private void PasswordForm_Load(object sender, EventArgs e)
        {
            btnOk.Enabled = (txtPassword.Text.Length > 0);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _okButtonClicked = false;
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                DialogUtility.ShowErrorMessageBox(this, "Password Required", "A password is required.");
                return;
            }

            txtPassword.Text = txtPassword.Text;
            this.DialogResult = DialogResult.OK;
            _okButtonClicked = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = (txtPassword.Text.Length > 0);
        }

        private void PasswordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_okButtonClicked && e.CloseReason != CloseReason.ApplicationExitCall)
            {
                txtPassword.Text = null;
                this.DialogResult = DialogResult.Cancel;
            }
        }

        #endregion

    }
}
