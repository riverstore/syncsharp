using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncSharp.Business
{
    class CustomException : Exception
    {
         public CustomException(): base()
        {
        }

        public CustomException(string error, Exception e) : base(error, e)
        {
        }

        public CustomException(string error) : base(error)
        {
        }
    }
}
