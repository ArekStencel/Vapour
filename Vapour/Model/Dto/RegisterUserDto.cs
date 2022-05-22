using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vapour.Model.Dto
{
    public class RegisterUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public decimal WalletBalance { get; set; } = 0;
        public int RoleId { get; set; } = 2;
        public string Description { get; set; }
    }
}
