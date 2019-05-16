using System;

namespace uStora.Common.Exceptions
{
    public class WorkSheetNotFoundException : Exception
    {
        public WorkSheetNotFoundException()
        {
        }

        public WorkSheetNotFoundException(string message)
        : base(message)
        {
        }

        public WorkSheetNotFoundException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}