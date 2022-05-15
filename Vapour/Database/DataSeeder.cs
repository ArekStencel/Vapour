using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vapour.Model;

namespace Vapour.Database
{
    internal class DataSeeder
    {
        private readonly VapourDatabaseEntities _dataContext;
        
        public DataSeeder(VapourDatabaseEntities dataContext)
        {
            _dataContext = dataContext;
        }
        
        public void Seed()
        {
            if (_dataContext.Database.Exists())
            {
                if (!_dataContext.Roles.Any())
                {
                    var roles = AddRoles();
                    _dataContext.Roles.AddRange(roles);
                    _dataContext.SaveChanges();
                }

                if (!_dataContext.Users.Any())
                {
                    var users = AddUsers();
                    _dataContext.Users.AddRange(users);
                    _dataContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> AddRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "Admin"
                },
                new Role()
                {
                    Name = "User"
                }
            };

            return roles;
        }

        private IEnumerable<User> AddUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Name = "Patryk",
                    Email = "patryk@test.pl",
                    Password = "haslo",
                    WalletBalance = 2115,
                    RoleId = 1,
                    Description = "opis"
                },
                new User()
                {
                    Name = "Mariusz",
                    Email = "mariusz@test.pl",
                    Password = "haslo",
                    WalletBalance = 420,
                    RoleId = 1,
                    Description = "mariano italiano"
                },
                new User()
                {
                    Name = "Arek",
                    Email = "arek@test.pl",
                    Password = "haslo",
                    WalletBalance = 2137,
                    RoleId = 2,
                    Description = "arek"
                },
                new User()
                {
                    Name = "Dawid",
                    Email = "dawid@test.pl",
                    Password = "haslo",
                    WalletBalance = 69,
                    RoleId = 2,
                    Description = "opis"
                }
            };

            return users;
        }
    }
}
