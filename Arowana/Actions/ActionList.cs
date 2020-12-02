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
        private IStringSerializer _stringSerializer;
        private IAction[] _actionList;

        #endregion

        #region - Constructor -

        public ActionList(ISerializer serializer, IStringSerializer stringSerializer, params IAction[] actions)
        {
            _serializer = serializer;
            _stringSerializer = stringSerializer;
            _actionList = actions ?? new IAction[0];
        }

        #endregion

        #region - Public Methods -

        public string DoActions<T>(T input)
        {
            byte[] bytes = ToByteArray(input);
            foreach (IAction action in _actionList)
            {
                bytes = action.DoAction(bytes);
            }
            return _stringSerializer.Stringify(bytes);
        }

        public T ReverseActions<T>(string serialized)
        {
            byte[] bytes = _stringSerializer.Destringify(serialized);
            for (int i = _actionList.Length - 1; i >= 0; i--)
            {
                bytes = _actionList[i].ReverseAction(bytes);
            }
            return FromByteArray<T>(bytes);
        }

        #endregion

        #region - Private Methods -

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
