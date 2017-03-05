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
using Arowana.Actions;
using Arowana.Exceptions;
using Arowana.Factories;
using Arowana.Storage;

#endregion

namespace PasswordHolder
{
    public partial class MainForm : Form
    {

        #region - Constants -

        private const string FILE_FILTER = "Password File (*.pwd)|*.pwd|All Files (*.*)|*.*";

        #endregion

        #region - Fields -
        
        private AccountCollection _accountCollection;
        private string _password;
        private bool _overrideFormClosing;
        private bool _isNew;
        private bool _isDirty;
        private string _fileName;
        private string _initialFileDialogDirectory;

        #endregion

        #region - Constructor -

        public MainForm()
        {
            InitializeComponent();
            _overrideFormClosing = false;
        }

        #endregion

        #region - Form Events -

        private void Form1_Load(object sender, EventArgs e)
        {
            _accountCollection = null;
            _password = null;
            _isNew = false;
            _isDirty = false;
            _fileName = null;
            _initialFileDialogDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            saveToolStripMenuItem.Enabled = false;
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            lstAccounts.Items.Clear();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _isNew = true;
            _isDirty = false;
            _accountCollection = new AccountCollection();
            SetMainFormForNew();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _accountCollection = OpenAccountCollection();
            if (_accountCollection != null)
            {
                _isNew = false;
                _isDirty = false;
                SetMainFormForOpen();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_isNew)
            {
                if (!string.IsNullOrWhiteSpace(_fileName) && !string.IsNullOrWhiteSpace(_password))
                {
                    SaveAccountCollection();
                    _isDirty = false;
                    return;
                }

                _fileName = null;
                _password = null;
                FileDialogResult<string> fileResult = DialogUtility.ShowSaveFileDialog(this, FILE_FILTER, _initialFileDialogDirectory);
                if (fileResult.Result == DialogResult.OK && fileResult.ResultObject != null)
                {
                    _fileName = fileResult.ResultObject;
                    FormDialogResult<string> passwordResult = DialogUtility.ShowPasswordDialog(this);
                    if (passwordResult.Result == DialogResult.OK && passwordResult.ResultObject != null)
                    {
                        _password = passwordResult.ResultObject;
                        SaveAccountCollection();
                        _isDirty = false;
                    }
                }
            }
            else
            {
                SaveAccountCollection();
                _isDirty = false;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _overrideFormClosing = false;
            bool result = CanExit();
            if (result)
            {
                _overrideFormClosing = true;
                Application.Exit();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormDialogResult<Account> result = DialogUtility.ShowAccountDialog(this, _accountCollection, "Add Account", "Add", true);
            if (result.Result == DialogResult.OK && result.ResultObject != null)
            {
                _accountCollection.Add(result.ResultObject);
                UpdateFileChangedStatus(true);
                SetListDataSource();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstAccounts.SelectedIndex;
            if (selectedIndex > -1)
            {
                Account selectedAccount = GetAccountFromIndex(selectedIndex);
                FormDialogResult<Account> result = DialogUtility.ShowAccountDialog(this, _accountCollection, "Edit Account", "Edit", false, selectedAccount);
                if (result.Result == DialogResult.OK && result.ResultObject != null)
                {
                    UpdateExistingAccount(result.ResultObject);
                    UpdateFileChangedStatus(true);
                    SetListDataSource();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DialogUtility.ShowConfirmDialog(this, "Delete?", "Are you sure you want to delete this account?"))
            {
                DeleteAccount();
            }
        }

        private void lstAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFormFromSelection();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_overrideFormClosing && e.CloseReason != CloseReason.ApplicationExitCall)
            {
                bool result = CanExit();
                e.Cancel = !result;
            }
        }

        #endregion

        #region - Private Methods -

        private void SetMainFormForNew()
        {
            lstAccounts.Enabled = true;
            lstAccounts.Items.Clear();
            lstAccounts.SelectedIndex = -1;
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        private AccountCollection OpenAccountCollection()
        {
            AccountCollection collection = null;

            FileDialogResult<string> fileResult = DialogUtility.ShowOpenFileDialog(this, FILE_FILTER, _initialFileDialogDirectory);
            if (fileResult.Result != DialogResult.OK && fileResult.ResultObject == null)
            {
                return null;
            }
            _fileName = fileResult.ResultObject;

            FormDialogResult<string> passwordResult = DialogUtility.ShowPasswordDialog(this);
            if (passwordResult.Result != DialogResult.OK && passwordResult.ResultObject == null)
            {
                return null;
            }
            _password = passwordResult.ResultObject;

            IFactory factory = FormFactory.GetFactory(_password);
            IStorage storage = FormFactory.GetStorage();

            string serialized = storage.RetrieveData(_fileName);
            ActionList actionList = factory.GetActionList();

            try
            {
                collection = actionList.ReverseActions<AccountCollection>(serialized);
            }
            catch (DeserializationException)
            {
                DialogUtility.ShowErrorMessageBox(this, "Error", "Incorrect password.");
            }

            return collection;
        }

        private void SetMainFormForOpen()
        {
            lstAccounts.Enabled = true;
            lstAccounts.SelectedIndex = -1;
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            SetListDataSource();
        }

        private void SetListDataSource()
        {
            if (_accountCollection != null)
            {
                Account selectedAccount = lstAccounts.SelectedItem as Account;
                List<Account> accountList = _accountCollection.ToAccountList().ToList();

                lstAccounts.BeginUpdate();
                int selectedIndex = -1;
                lstAccounts.Items.Clear();
                for (int i = 0; i < accountList.Count(); i++)
                {
                    Account act = accountList[i];
                    if (act.Equals(selectedAccount))
                    {
                        selectedIndex = i;
                    }
                    lstAccounts.Items.Add(act);
                }
                lstAccounts.SelectedIndex = selectedIndex;
                lstAccounts.EndUpdate();

                SetFormFromSelection();
            }
        }

        private void SetFormFromSelection()
        {
            int selectedIndex = lstAccounts.SelectedIndex;
            if (selectedIndex > -1)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                Account account = GetAccountFromIndex(selectedIndex);
                SetDetailDisplay(account);
            }
            else
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                SetDetailDisplay(null);
            }
        }

        private Account GetAccountFromIndex(int index)
        {
            if (_accountCollection != null)
            {
                return _accountCollection.ToAccountList().ElementAt(index);
            }
            return null;
        }

        private void SetDetailDisplay(Account account)
        {
            if (account == null)
            {
                txtAccountName.Text = string.Empty;
                txtUsername.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtNotes.Text = string.Empty;
            }
            else
            {
                txtAccountName.Text = account.Name;
                txtUsername.Text = account.ActiveCredential.UserName;
                txtPassword.Text = account.ActiveCredential.Password;
                txtNotes.Text = account.Notes;
            }
        }

        private void SaveAccountCollection()
        {
            IFactory factory = FormFactory.GetFactory(_password);
            IStorage storage = FormFactory.GetStorage();

            ActionList actionList = factory.GetActionList();
            string serialized = actionList.DoActions(_accountCollection);
            storage.StoreData(_fileName, serialized);
        }

        private bool CanExit()
        {
            if (_accountCollection != null && _isDirty)
            {
                bool result = DialogUtility.ShowConfirmDialog(this, "Exit?", "Changes have not been saved.  Are you sure you want to exit?");
                return result;
            }
            else
            {
                return true;
            }
        }

        private void UpdateFileChangedStatus(bool fileChanged)
        {
            _isDirty = fileChanged;
            saveToolStripMenuItem.Enabled = fileChanged;
        }

        private void UpdateExistingAccount(Account account)
        {
            Account existingAccount = _accountCollection[account.Name];
            existingAccount.CredentialHistory.Add(account.ActiveCredential);
            existingAccount.ActiveCredential = new Credential();
            existingAccount.ActiveCredential.UserName = account.ActiveCredential.UserName;
            existingAccount.ActiveCredential.Password = account.ActiveCredential.Password;
            existingAccount.ActiveCredential.CreatedDate = DateTime.Now;
            existingAccount.Notes = account.Notes;
        }

        private void DeleteAccount()
        {
            Account selectedAccount = lstAccounts.SelectedItem as Account;
            _accountCollection.Remove(selectedAccount.Name);
            SetListDataSource();
            UpdateFileChangedStatus(true);
        }

        #endregion

    }
}
