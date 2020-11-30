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
    public class Account : ISerializable, IEquatable<Account>
    {

        #region - Constructors -

        public Account()
        {
            this.Name = string.Empty;
            this.CreateDate = DateTime.MinValue;
            this.ActiveCredential = null;
            this.CredentialHistory = new List<Credential>();
            this.Notes = string.Empty;
        }

        protected Account(SerializationInfo info, StreamingContext context)
        {
            this.Name = info.GetString(Serialization.SerializationConstants.Account.NAME);
            this.CreateDate = info.GetDateTime(Serialization.SerializationConstants.Account.CREATE_DATE);
            this.ActiveCredential = info.GetValue(Serialization.SerializationConstants.Account.ACTIVE_CREDENTIAL, typeof(Credential)) as Credential;
            this.CredentialHistory = info.GetValue(Serialization.SerializationConstants.Account.CREDENTIAL_HISTORY, typeof(List<Credential>)) as List<Credential>;
            this.Notes = info.GetString(Serialization.SerializationConstants.Account.NOTES);
        }

        #endregion

        #region - Properties -

        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public Credential ActiveCredential { get; set; }
        public List<Credential> CredentialHistory { get; set; }
        public string Notes { get; set; }

        #endregion

        #region - Public Methods -

        public Credential AddNewCredential(string userName, string password, DateTime createDate)
        {
            Credential credential = new Credential()
            {
                UserName = userName,
                Password = password,
                CreatedDate = createDate
            };

            CredentialHistory.Add(credential);

            return credential;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            Account account = obj as Account;
            if (account == null)
            {
                return false;
            }
            return Equals(account);
        }

        public override int GetHashCode()
        {
            const int NUM1 = 5404;
            const int NUM2 = 823;
            int hash = NUM1;
            if (Name != null)
            {
                hash = (NUM2 * hash) + Name.GetHashCode();
            }
            hash = (NUM2 * hash) + CreateDate.GetHashCode();
            if (ActiveCredential != null)
            {
                hash = (NUM2 * hash) + ActiveCredential.GetHashCode();
            }
            if (CredentialHistory != null)
            {
                foreach (Credential cred in CredentialHistory)
                {
                    hash = (NUM2 * hash) + cred.GetHashCode();
                }
            }
            if (Notes != null)
            {
                hash = (NUM2 * hash) + Notes.GetHashCode();
            }
            return hash;
        }

        #endregion

        #region - ISerializable Members -

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Serialization.SerializationConstants.Account.NAME, this.Name);
            info.AddValue(Serialization.SerializationConstants.Account.CREATE_DATE, this.CreateDate);
            info.AddValue(Serialization.SerializationConstants.Account.ACTIVE_CREDENTIAL, this.ActiveCredential);
            info.AddValue(Serialization.SerializationConstants.Account.CREDENTIAL_HISTORY, this.CredentialHistory);
            info.AddValue(Serialization.SerializationConstants.Account.NOTES, this.Notes);
        }

        #endregion

        #region - IEquatable<Account> Members -

        public bool Equals(Account other)
        {
            if (other == null)
            {
                return false;
            }
            bool isValid = (this.Name == other.Name) &&
                (this.CreateDate == other.CreateDate) &&
                (this.Notes == other.Notes);
            if (!isValid)
            {
                return false;
            }

            if ((this.ActiveCredential == null && other.ActiveCredential != null) || (this.ActiveCredential != null && other.ActiveCredential == null))
            {
                return false;
            }
            if (this.ActiveCredential != null && other.ActiveCredential != null)
            {
                if (!this.ActiveCredential.Equals(other.ActiveCredential))
                {
                    return false;
                }
            }

            if (this.CredentialHistory == null && other.CredentialHistory == null)
            {
                return true;
            }

            if ((this.CredentialHistory == null && other.CredentialHistory != null) || (this.CredentialHistory != null && other.CredentialHistory == null))
            {
                return false;
            }

            if (this.CredentialHistory.Count != other.CredentialHistory.Count)
            {
                return false;
            }

            for (int i = 0; i < this.CredentialHistory.Count; i++)
            {
                Credential cred1 = this.CredentialHistory[i];
                Credential cred2 = other.CredentialHistory[i];
                if (!cred1.Equals(cred2))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

    }
}
