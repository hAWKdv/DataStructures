namespace Graphs.Exceptions.Common
{
    using System;

    public class GraphException : Exception
    {
        public GraphException()
            : base()
        {
        }

        public GraphException(string message)
            : base(message)
        {
        }

        public GraphException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public GraphException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public GraphException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }
    }
}
