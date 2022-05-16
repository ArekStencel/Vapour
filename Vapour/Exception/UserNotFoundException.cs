using System;

namespace Vapour.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public string UserEmail { get; set; }

        public UserNotFoundException(string userEmail)
        {
            UserEmail = userEmail;
        }

        public UserNotFoundException(string message, string userEmail) : base(message)
        {
            UserEmail = userEmail;
        }

        public UserNotFoundException(string message, Exception innerException, string userEmail) : base(message, innerException)
        {
            UserEmail = userEmail;
        }
    }
}