using System;
using System.Collections.Generic;
using System.Text;

namespace ZW.Common
{
    public class ZWException : Exception
    {
        public ZWException(string message) : base(message) { _ErrorCode = -1; }

        public ZWException(int errorCode,string message) : base(message) { _ErrorCode = errorCode; }

        public ZWException(string message, Exception innerException) : base(message, innerException) { }

        private int _ErrorCode;
        public int ErrorCode
        {
            get { return ErrorCode; }
            set { ErrorCode = value; }
        }
    }

}
