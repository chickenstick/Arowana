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
    public class FormDialogResult<T>
    {

        #region - Constructor -

        public FormDialogResult()
        {
            Result = DialogResult.None;
        }

        public FormDialogResult(DialogResult result, T resultObject)
        {
            Result = result;
            ResultObject = resultObject;
        }

        #endregion

        #region - Properties -

        public DialogResult Result { get; set; }
        public T ResultObject { get; set; }

        #endregion

    }
}
