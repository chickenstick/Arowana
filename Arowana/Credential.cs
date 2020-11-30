#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

#endregion

namespace Arowana
{
    [Serializable]
    public class Credential : ISerializable, IEquatable<Credential>
    {

        #region - Constructors -

        public Credential()
        {
            UserName = string.Empty;
            Password = string.Empty;
            CreatedDate = DateTime.MinValue;
        }

        protected Credential(SerializationInfo info, StreamingContext context)
        {
            this.UserName = info.GetString(Serialization.SerializationConstants.Credential.USER_NAME);
            this.Password = info.GetString(Serialization.SerializationConstants.Credential.PASSWORD);
            this.CreatedDate = info.GetDateTime(Serialization.SerializationConstants.Credential.CREATE_DATE);
        }

        #endregion

        #region - Properties -

            
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }

        #endregion

        #region - Public Methods -

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override bool Equals(object obj)
        {
            Credential credential = obj as Credential;
            if (credential == null)
            {
                return false;
            }
            return Equals(credential);
        }

        public override int GetHashCode()
        {
            const int NUM1 = 9513;
            const int NUM2 = 709;
            int hash = NUM1;
            if (UserName != null)
            {
                hash = (NUM2 * hash) + UserName.GetHashCode();
            }
            if (Password != null)
            {
                hash = (NUM2 * hash) + Password.GetHashCode();
            }
            hash = (NUM2 * hash) + CreatedDate.GetHashCode();
            return hash;
        }

        #endregion

        #region - ISerializable Members -

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Serialization.SerializationConstants.Credential.USER_NAME, this.UserName);
            info.AddValue(Serialization.SerializationConstants.Credential.PASSWORD, this.Password);
            info.AddValue(Serialization.SerializationConstants.Credential.CREATE_DATE, this.CreatedDate);
        }

        #endregion

        #region - IEquatable<Credential> Members -

        public bool Equals(Credential other)
        {
            if (other == null)
            {
                return false;
            }
            return (this.UserName == other.UserName) &&
                (this.Password == other.Password) &&
                (this.CreatedDate == other.CreatedDate);
        }

        #endregion

    }
}
