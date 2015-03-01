namespace Graphs.Exceptions
{
    using System;
    using Common;

    public class NullAlgorithmStrategyException : GraphException
    {
        public NullAlgorithmStrategyException()
            : base()
        {
        }

        public NullAlgorithmStrategyException(string message)
            : base(message)
        {
        }

        public NullAlgorithmStrategyException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public NullAlgorithmStrategyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NullAlgorithmStrategyException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }
    }
}
