using System;
using System.Collections.Generic;
using System.Text;

namespace BDLibrary
{
    class Error : Exception
    {
        public Error() : base() { }
        public Error(string message) : base(message) { }
    }
}
