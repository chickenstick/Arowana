#region - Using Statements -

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace PasswordHolder
{
    public static class LogUtility
    {

        #region - Constants -

        private const string EVENT_LOG_SOURCE = "PasswordHolder";

        #endregion

        #region - Public Methods -

        public static bool LogToFile(string fileName, string message)
        {
            try
            {
                LogToFileInternal(fileName, message);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool LogToFile(string fileName, Exception ex)
        {
            try
            {
                LogToFileInternal(fileName, ex.ToString());
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool LogToFile(Exception ex)
        {
            try
            {
                string fileName = string.Format("{0}_{1:yyyyMMddHHmmssfff}.txt", ex.GetType().Name, DateTime.Now);
                LogToFileInternal(fileName, ex.ToString());
            }
            catch
            {
                return false;
            }
            return true;
        }

        #endregion

        #region - Private Methods -

        private static void LogToFileInternal(string fileName, string message)
        {
            using (StreamWriter sWriter = new StreamWriter(fileName, true))
            {
                sWriter.Write(message);
            }
        }

        #endregion

    }
}
