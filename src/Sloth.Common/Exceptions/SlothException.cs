using System;

namespace Sloth.Common.Exceptions
{
    public class SlothException : Exception
    {
        public SlothException(string message) : base(message) { }
        public SlothException(string message, Exception exception) : base(message, exception) { }
    }
}
