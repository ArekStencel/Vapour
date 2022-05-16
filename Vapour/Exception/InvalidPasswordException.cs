using System;

namespace Vapour.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public InvalidPasswordException(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public InvalidPasswordException(string message, string email, string password) : base(message)
        {
            Email = email;
            Password = password;
        }

        public InvalidPasswordException(string message, Exception innerException, string email, string password) : base(message, innerException)
        {
            Email = email;
            Password = password;
        }
    }
}