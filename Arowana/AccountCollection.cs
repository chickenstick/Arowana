#region - Using Statements -

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

#endregion

namespace Arowana
{
    [Serializable]
    public class AccountCollection : ISerializable, IDictionary<string, Account>, IEquatable<AccountCollection>
    {

        #region - Fields -

        private Dictionary<string, Account> _dictionary;

        #endregion

        #region - Constructors -

        public AccountCollection()
        {
            _dictionary = new Dictionary<string, Account>();
        }

        [Obsolete]
        public AccountCollection(SerializationInfo info, StreamingContext context)
        {
            _dictionary = info.GetValue(Serialization.SerializationConstants.AccountCollection.DICTIONARY, typeof(Dictionary<string, Account>)) as Dictionary<string, Account>;
        }

        #endregion

        #region - Public Methods -

        public void Add(Account account)
        {
            _dictionary.Add(account.Name, account);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override bool Equals(object obj)
        {
            AccountCollection other = obj as AccountCollection;
            if (other == null)
            {
                return false;
            }
            return Equals(other);
        }

        public override int GetHashCode()
        {
            const int NUM1 = 8352;
            const int NUM2 = 1481;
            int hash = NUM1;
            foreach (KeyValuePair<string, Account> item in _dictionary)
            {
                hash = (NUM2 * hash) + item.Value.GetHashCode();
            }
            return hash;
        }

        public List<Account> ToAccountList()
        {
            return _dictionary.ToList().OrderBy(x => x.Key).Select(x => x.Value).ToList();
        }

        #endregion

        #region - ISerializable Members -

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Serialization.SerializationConstants.AccountCollection.DICTIONARY, _dictionary);
        }

        #endregion

        #region - IDictionary<string, Account> Members -

        void IDictionary<string, Account>.Add(string key, Account value)
        {
            _dictionary.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return _dictionary.Keys; }
        }

        public bool Remove(string key)
        {
            return _dictionary.Remove(key);
        }

        public bool TryGetValue(string key, out Account value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        public ICollection<Account> Values
        {
            get { return _dictionary.Values; }
        }

        public Account this[string key]
        {
            get
            {
                return _dictionary[key];
            }
            set
            {
                _dictionary[key] = value;
            }
        }

        void ICollection<KeyValuePair<string, Account>>.Add(KeyValuePair<string, Account> item)
        {
            _dictionary.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, Account> item)
        {
            return _dictionary.Contains(item);
        }

        void ICollection<KeyValuePair<string, Account>>.CopyTo(KeyValuePair<string, Account>[] array, int arrayIndex)
        {
            if (arrayIndex == 0)
            {
                array = _dictionary.ToArray();
            }
            else if (arrayIndex >= _dictionary.Count)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                array = _dictionary.Skip(arrayIndex).ToArray(); ;
            }
        }

        public int Count
        {
            get { return _dictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, Account> item)
        {
            return _dictionary.Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<string, Account>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region - IEquatable<AccountCollection> Members -

        public bool Equals(AccountCollection other)
        {
            if (other == null)
            {
                return false;
            }

            if (this._dictionary == null && other._dictionary == null)
            {
                return true;
            }

            if ((this._dictionary == null && other._dictionary != null) || (this._dictionary != null && other._dictionary == null))
            {
                return false;
            }

            if (this._dictionary.Count != other._dictionary.Count)
            {
                return false;
            }

            List<string> allKeys = this._dictionary.Keys.Select(x => x).ToList();
            foreach (string key in allKeys)
            {
                Account thisValue = this._dictionary[key];
                if (!other._dictionary.ContainsKey(key))
                {
                    return false;
                }
                Account otherValue = other._dictionary[key];
                if (!thisValue.Equals(otherValue))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

    }
}
