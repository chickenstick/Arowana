#region - Using Statements -

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

using Arowana.Exceptions;
using Arowana.Serialization;

#endregion

namespace Arowana.Actions
{
    public sealed class ActionList
    {

        #region - Fields -

        private ISerializer _serializer;
        private ActionBase[] _actionList;

        #endregion

        #region - Constructor -

        public ActionList(ISerializer serializer, params ActionBase[] actions)
        {
            _serializer = serializer;
            _actionList = actions ?? new ActionBase[0];
            SetActions();
        }

        #endregion

        #region - Public Methods -

        public string DoActions<T>(T input)
        {
            using (MemoryStream inputStream = new MemoryStream())
            {
                byte[] bytes = ToByteArray(input);
                if(_actionList.Length > 0)
                {
                    bytes = _actionList[0].DoAction(bytes);
                }
                return Convert.ToBase64String(bytes);
            }
        }

        public T ReverseActions<T>(string serialized)
        {
            byte[] bytes = Convert.FromBase64String(serialized);
            if (_actionList.Length > 0)
            {
                int startIndex = _actionList.Length - 1;
                bytes = _actionList[startIndex].ReverseAction(bytes);
            }
            return FromByteArray<T>(bytes);
        }

        #endregion

        #region - Private Methods -

        private void SetActions()
        {
            for (int i = 0; i < _actionList.Length; i++)
            {
                ActionBase current = _actionList[i];

                if (i > 0)
                {
                    current.PreviousAction = _actionList[i - 1];
                }
                if (i < _actionList.Length - 1)
                {
                    current.NextAction = _actionList[i + 1];
                }
            }
        }

        private byte[] ToByteArray<T>(T obj)
        {
            return _serializer.Serialize(obj);
        }

        private T FromByteArray<T>(byte[] array)
        {
            try
            {
                return _serializer.Deserialize<T>(array);
            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                throw new DeserializationException("Unable to deserialize string.", ex);
            }
        }

        #endregion

    }
}
