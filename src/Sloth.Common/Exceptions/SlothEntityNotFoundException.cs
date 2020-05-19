using System;

namespace Sloth.Common.Exceptions
{
    public class SlothEntityNotFoundException : SlothException
    {
        public SlothEntityNotFoundException(string message) : base(message) { }
        public SlothEntityNotFoundException(string message, Exception exception) : base(message, exception) { }
    }
}
