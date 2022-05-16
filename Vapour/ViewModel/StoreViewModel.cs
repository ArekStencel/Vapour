using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vapour.Model;

namespace Vapour.ViewModel
{
    public class StoreViewModel : BaseViewModel
    {
        private readonly VapourDatabaseEntities _data = new VapourDatabaseEntities();

        private string _user;
        public string User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private List<string> _users = new List<string>();

        public List<string> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public StoreViewModel()
        {
            var users = _data.Users.ToList();
            foreach (var user in users.Where(user => _users != null))
            {
                _users.Add(user.ToString());
            }
        }
    }
}
