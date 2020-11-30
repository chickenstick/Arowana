#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Arowana.Exceptions
{
    public class DeserializationException : Exception
    {

        #region - Constructors -

        public DeserializationException()
            : base()
        {

        }

        public DeserializationException(string message)
            : base(message)
        {

        }

        public DeserializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public DeserializationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        #endregion

    }
}
