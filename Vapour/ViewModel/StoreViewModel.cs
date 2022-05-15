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
                OnPropertyChanged(User);
            }
        }

        public StoreViewModel()
        {
            Console.WriteLine("store");
            Console.WriteLine(_data.Users.First().ToString());
            Console.WriteLine(_data.Users.First().Email);
            User = _data.Users.First().Name;
        }
    }
}
