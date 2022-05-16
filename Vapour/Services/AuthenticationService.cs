﻿using System.CodeDom;
using System.Linq;
using System.Threading.Tasks;
using Vapour.Exceptions;
using Vapour.Model;
using Vapour.State;

namespace Vapour.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly VapourDatabaseEntities _data = new VapourDatabaseEntities();

        // public AuthenticationService(VapourDatabaseEntities data)
        // {
            // _data = data;
        // }

        public Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword, int roleId = 2)
        {
            var result = RegistrationResult.Success;

            if (password != confirmPassword)
            {
                result = RegistrationResult.PasswordsDoNotMatch;
            }

            // Todo change if statement
            // var emailAccount = _data.Users.All(u => u.Email.Equals(email)); // await _accountService.GetByEmail(email);
            // if (emailAccount)
            // {
            //     result = RegistrationResult.EmailAlreadyExists;
            // }

            if (result == RegistrationResult.Success)
            {
                // string hashedPassword = _passwordHasher.HashPassword(password);

                var newUser = new User()
                {
                    Email = email,
                    Name = username,
                    Password = password, // hashedPassword,
                    RoleId = roleId,
                };

                _data.Users.Add(newUser);
                _data.SaveChangesAsync();
            }

            return Task.FromResult(result);
        }

        public async Task<User> Login(string email, string password)
        {
            var storedAccount = _data.Users.First(u => u.Email.Equals(email)); // await _accountService.GetByUsername(username);

            if (storedAccount == null)
            {
                throw new UserNotFoundException(email);
            }

            var passwordResult = storedAccount.Password.Equals(password); // _passwordHasher.VerifyHashedPassword(storedAccount.AccountHolder.PasswordHash, password);

            // if (passwordResult != PasswordVerificationResult.Success)
            // {
            //     throw new InvalidPasswordException(username, password);
            // }

            if (passwordResult == false)
            {
                throw new InvalidPasswordException(email, password);
            }

            return storedAccount;
        }
    }
}