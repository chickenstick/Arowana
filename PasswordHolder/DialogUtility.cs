#region - Using Statements -

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Arowana;

#endregion

namespace PasswordHolder
{
    public static class DialogUtility
    {

        #region - Public Methods -

        public static void ShowErrorMessageBox(IWin32Window owner, string caption, string text)
        {
            MessageBox.Show(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowErrorMessageBox(IWin32Window owner, string caption, IEnumerable<string> errorMessages)
        {
            string text = string.Join(Environment.NewLine, errorMessages);
            ShowErrorMessageBox(owner, caption, text);
        }

        public static bool ShowConfirmDialog(IWin32Window owner, string caption, string text)
        {
            DialogResult result = MessageBox.Show(owner, text, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            return result == DialogResult.OK;
        }

        public static FileDialogResult<string> ShowOpenFileDialog(IWin32Window owner, string fileFilter, string initialDirectory)
        {
            string fileName = null;
            string directory = null;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = fileFilter;
            dialog.Multiselect = false;
            dialog.InitialDirectory = initialDirectory;
            DialogResult result = dialog.ShowDialog(owner);
            if (result == DialogResult.OK)
            {
                string fName = dialog.FileName;
                if (File.Exists(fName))
                {
                    FileInfo fInfo = new FileInfo(fName);
                    directory = fInfo.Directory.FullName;

                    fileName = fName;
                }
                else
                {
                    ShowErrorMessageBox(owner, "File Not Found", "Could not find selected file.");
                }
            }

            return new FileDialogResult<string>(result, fileName, directory);
        }

        public static FileDialogResult<string> ShowSaveFileDialog(IWin32Window owner, string fileFilter, string initialDirectory)
        {
            string fileName = null;
            string directory = null;

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = fileFilter;
            dialog.InitialDirectory = initialDirectory;
            DialogResult result = dialog.ShowDialog(owner);
            if (result == DialogResult.OK)
            {
                fileName = dialog.FileName;

                FileInfo fInfo = new FileInfo(fileName);
                directory = fInfo.Directory.FullName;
            }

            return new FileDialogResult<string>(result, fileName, directory);
        }

        public static FormDialogResult<string> ShowPasswordDialog(IWin32Window owner)
        {
            PasswordForm passwordForm = FormFactory.GetPasswordForm();
            DialogResult result = passwordForm.ShowDialog(owner);
            if (result == DialogResult.OK)
            {
                return new FormDialogResult<string>(result, passwordForm.PasswordText);
            }

            return new FormDialogResult<string>(result, null);
        }

        public static FormDialogResult<Account> ShowAccountDialog(IWin32Window owner, AccountCollection accountCollection, 
            string windowTitle, string okButtonText, bool isAdd, Account existingAccount = null)
        {
            AccountForm accountForm = FormFactory.GetAccountForm();
            accountForm.AccountCollectionObject = accountCollection;
            accountForm.WindowTitle = windowTitle;
            accountForm.OkButtonText = okButtonText;
            accountForm.IsAdd = isAdd;
            accountForm.SetFormFromAccount(existingAccount);

            DialogResult result = accountForm.ShowDialog(owner);
            if (result == DialogResult.OK)
            {
                return new FormDialogResult<Account>(result, accountForm.AccountObject);
            }

            return new FormDialogResult<Account>(result, null);
        }

        #endregion

    }
}
