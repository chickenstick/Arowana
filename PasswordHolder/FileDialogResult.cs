#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace PasswordHolder
{
    public class FileDialogResult<T> : FormDialogResult<T>
    {

        #region - Constructors -

        public FileDialogResult()
            : base()
        {
        }

        public FileDialogResult(DialogResult result, T resultObject)
            : base(result, resultObject)
        {
        }

        public FileDialogResult(DialogResult result, T resultObject, string selectedFileDirectory)
            : base(result, resultObject)
        {
            SelectedFileDirectory = selectedFileDirectory;
        }

        #endregion

        #region - Properties -

        public string SelectedFileDirectory { get; set; }

        #endregion

    }
}
