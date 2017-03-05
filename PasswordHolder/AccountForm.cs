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

using Arowana;

#endregion

namespace PasswordHolder
{
    public partial class AccountForm : Form
    {

        #region - Fields -

        private bool _okButtonClicked;

        #endregion

        #region - Constructor -

        public AccountForm()
        {
            InitializeComponent();
            _okButtonClicked = false;
            AccountObject = null;
        }

        #endregion

        #region - Properties -

        public bool IsAdd { get; set; }

        public Account AccountObject { get; set; }

        public AccountCollection AccountCollectionObject { get; set; }

        public string WindowTitle
        {
            get { return Text; }
            set { Text = value; }
        }

        public string OkButtonText
        {
            get { return btnOk.Text; }
            set { btnOk.Text = value; }
        }

        #endregion

        #region - Event Handlers -

        private void btnOk_Click(object sender, EventArgs e)
        {
            _okButtonClicked = false;
            List<string> errorMessages = new List<string>();
            bool isValid = ValidateAccountForm(out errorMessages);
            if (!isValid)
            {
                DialogUtility.ShowErrorMessageBox(this, "Validation Error", errorMessages);
                return;
            }

            this.AccountObject = GetAccountFromForm();
            this.DialogResult = DialogResult.OK;
            _okButtonClicked = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _okButtonClicked = false;
            this.Close();
        }

        private void AccountForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_okButtonClicked && e.CloseReason != CloseReason.ApplicationExitCall)
            {
                this.AccountObject = null;
                this.DialogResult = DialogResult.Cancel;
            }
        }

        #endregion

        #region - Public Methods -

        public void SetFormFromAccount(Account account)
        {
            if (account != null)
            {
                txtAccount.Text = account.Name;
                txtAccount.Enabled = false;
                txtUsername.Text = account.ActiveCredential.UserName;
                txtPassword.Text = account.ActiveCredential.Password;
                txtNotes.Text = account.Notes;
            }
        }

        #endregion

        #region - Private Methods -

        private bool ValidateAccountForm(out List<string> errorMessages)
        {
            errorMessages = new List<string>();

            if (string.IsNullOrWhiteSpace(txtAccount.Text))
            {
                errorMessages.Add("Account is a required field.");
            }
            else if (this.IsAdd)
            {
                if (AccountCollectionObject.ContainsKey(txtAccount.Text))
                {
                    errorMessages.Add("Account already exists.");
                }
            }
            else
            {
                if (!AccountCollectionObject.ContainsKey(txtAccount.Text))
                {
                    errorMessages.Add("Account does not exist.");
                }
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                errorMessages.Add("Username is a required field.");
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorMessages.Add("Password is a required field.");
            }

            return errorMessages.Count == 0;
        }

        private Account GetAccountFromForm()
        {
            Account account = new Account();
            account.Name = txtAccount.Text;
            account.CreateDate = DateTime.Now;
            account.ActiveCredential = new Credential();
            account.ActiveCredential.UserName = txtUsername.Text;
            account.ActiveCredential.Password = txtPassword.Text;
            account.ActiveCredential.CreatedDate = DateTime.Now;
            account.Notes = txtNotes.Text;
            return account;
        }

        #endregion

    }
}
