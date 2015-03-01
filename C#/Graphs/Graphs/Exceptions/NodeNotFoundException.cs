namespace Graphs.Exceptions.Common
{
    using System;
    using Common;

    public class NodeNotFoundException : GraphException
    {
        public NodeNotFoundException()
            : base()
        {
        }

        public NodeNotFoundException(string message)
            : base(message)
        {
        }

        public NodeNotFoundException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public NodeNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NodeNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }
    }
}
