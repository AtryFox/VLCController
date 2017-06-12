using System;

namespace DerAtrox.VLCController.Exceptions
{
    public class ApiRespondException : Exception
    {
        public ApiRespondException()
        {
        }

        public ApiRespondException(string message) : base(message)
        {
        }
    }
}
