using System;

namespace Clockwork
{
    /// <summary>
    /// Represents errors returned by the Clockwork API
    /// </summary>
    /// <remarks>
    /// An additional ErrorCode parameter is added to the Exception object so you can lookup 
    /// the error in the Clockwork documentation
    /// </remarks>
    [Serializable]
    public class APIException : Exception
    {
        public int ErrorCode { get; private set; }

        public APIException()
            : base()
        {

        }
        public APIException(string message)
            : base(message)
        {

        }
        public APIException(string message, Exception inner)
            : base(message, inner)
        {
            
        }

        public APIException(string message, int errorcode)
            : base(message)
        {
            ErrorCode = errorcode;
        }
    }
}
