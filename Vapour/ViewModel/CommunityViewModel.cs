using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using Vapour.Command;
using Vapour.Model;
using Vapour.Model.Dto;
using Vapour.State;
using Vapour.ViewModel.Base;

namespace Vapour.ViewModel
{
    public class CommunityViewModel : BaseViewModel
    {
        private readonly VapourDatabaseEntities _dataContext;
        private readonly IAuthenticator _authenticator;

        public CommunityViewModel(VapourDatabaseEntities dataContext, IAuthenticator authenticator)
        {
            _dataContext = dataContext;
            _authenticator = authenticator;
            Wallet = _authenticator.CurrentUser.WalletBalance.ToString();
            Name = _authenticator.CurrentUser.Name;
            GetAllUsers();
            Getfollowers();
            Getfollowing();
            SelectedUser = Users[0];
        }

        private List<UserDto> _users = new List<UserDto>();
        public List<UserDto> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _nick;
        public string Nick
        {
            get => _nick;
            set
            {
                _nick = value;
                OnPropertyChanged(nameof(Nick));
            }
        }

        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private bool _iffollow;
        public bool IfFollow
        {
            get => _iffollow;
            set
            {
                _iffollow = value;
                OnPropertyChanged(nameof(IfFollow));
            }
        }

        private string _isfollow;
        public string IsFollow
        {
            get => _isfollow;
            set
            {
                _isfollow = value;
                OnPropertyChanged(nameof(IsFollow));
            }
        }

        private string _wallet;
        public string Wallet
        {
            get => _wallet;
            set
            {
                _wallet = value;
                OnPropertyChanged(nameof(Wallet));
            }
        }


        private string _funds;
        public string Funds
        {
            get { return _funds; }
            set
            {
                _funds = value;
                OnPropertyChanged(nameof(Funds));
            }
        }

        private int _following;
        public int Following
        {
            get => _following;
            set
            {
                _following = value;
                OnPropertyChanged(nameof(Following));
            }
        }

        private int _followers;
        public int Followers
        {
            get => _followers;
            set
            {
                _followers = value;
                OnPropertyChanged(nameof(Followers));
            }
        }


        private string _looking;
        
        public string Looking
        {
            get => _looking;
            set
            {
                _looking = value;
                OnPropertyChanged(nameof(Looking));
            }
        }

        private string _bText;
        public string BText
        {
            get => _bText;
            set
            {
                _bText = value;
                OnPropertyChanged(nameof(BText));
            }
        }

        private void Getfollowers()
        {
            var users = _dataContext.Users.ToList();
            _followers = 0;
            foreach (var user in users)
            {
                if (_dataContext.Follows
                        .Where(x => x.FollowerId == _authenticator.CurrentUser.Id)
                        .Where(y => y.UserId == user.Id)
                        .Count(z => z.FollowerId == _authenticator.CurrentUser.Id) != 0)
                {
                    _followers++;
                }


            }
        }

        private void Getfollowing()
        {
            var users = _dataContext.Users.ToList();
            _following = 0;
            foreach (var user in users)
            {
                if (_dataContext.Follows
                        .Where(x => x.FollowerId == user.Id)
                        .Where(y => y.UserId == _authenticator.CurrentUser.Id)
                        .Count(z => z.FollowerId != _authenticator.CurrentUser.Id) != 0)
                {
                    _following++;
                }


            }
            Following = _following;
        }




        private UserDto _selectedUser;
        public UserDto SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                if (value != null)
                {
                    Id = value.Id;
                    Nick = value.Name;
                    IsFollow = value.IsFollow;
                    IfFollow = value.IfFollow;
                    CButton();
                }

                OnPropertyChanged(nameof(SelectedUser));
            }
        }






        private void GetAllUsers()
        {
            var users = _dataContext.Users.ToList();
            var Alluser = new List<UserDto>();
            foreach (var user in users)
            {
                bool ifFollow = false;
                var isFollow = "";
                if (_dataContext.Follows
                        .Where(x => x.FollowerId == user.Id)
                        .Where(y => y.UserId == _authenticator.CurrentUser.Id)
                        .Count(z => z.FollowerId != _authenticator.CurrentUser.Id) != 0)
                {
                    ifFollow = true;
                    isFollow =  "(obserwowany)";
                }
                if (user.Id != _authenticator.CurrentUser.Id)
                {

                    Alluser.Add(new UserDto()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        IsFollow = isFollow,
                        IfFollow = ifFollow,
                    }); 
                }
            }
            Users = Alluser;
            SelectedUser = Users[0];
        }

        private List<UserDto> GetUsers(string searching)
        {
            var users = _dataContext.Users.ToList();
            var user = new List<UserDto>();

            if (searching == "" || searching == null)
            { 
                GetAllUsers();
                return Users;
            }

            foreach (var u in users)
            {
                if (u.Name.ToLower().Contains(searching.ToLower()))
                {
                    if (u.Id != _authenticator.CurrentUser.Id)
                    {

                        user.Add(new UserDto()
                        {
                            Id = u.Id,
                            Name = u.Name,
                            IsFollow = "",
                            IfFollow = false,
                        });
                    }
                }
            }
            foreach (var u in user)
            {
                if (_dataContext.Follows
                        .Where(x => x.FollowerId == u.Id)
                        .Where(y => y.UserId == _authenticator.CurrentUser.Id)
                        .Count(z => z.FollowerId != _authenticator.CurrentUser.Id) != 0)
                {
                    u.IfFollow = true;
                    u.IsFollow = "(Obserwujesz)";
                }
            }
            if (user == null)
                return null;
            else
                return user;
        }



        private ICommand _addFunds;
        public ICommand AddFunds
        {
            get
            {
                return _addFunds ?? (_addFunds = new RelayCommand(o =>
                {
                    if (IfCorrectAmount())
                    {
                        MessageBox.Show(
                            "Podano złą ilość pieniędzy. \nPamiętaj możesz wpłacić do 1000 zł" +
                            "\nza jednym doładowaniem ","", MessageBoxButton.OK);
                        return;
                    }

                    MessageBox.Show(
                        "Twoje konto zostało doładowane"
                        + "\nŚrodki na koncie: "
                        + (_authenticator.CurrentUser.WalletBalance + decimal.Parse(Funds))+"zł",
                        "Dziękujemy za skorzystanie z naszego sklepu"
                        , MessageBoxButton.OK);

                    var walletBalanceAfterAddingFunds = _authenticator.CurrentUser.WalletBalance + decimal.Parse(Funds);

                    _dataContext.Users
                        .First(u => u.Id == _authenticator.CurrentUser.Id)
                        .WalletBalance = walletBalanceAfterAddingFunds;
                    _dataContext.SaveChanges();

                    Wallet = _authenticator.CurrentUser.WalletBalance.ToString();
                },
                o => true));
            }
        }

        private ICommand _search;
        public ICommand Search
        {
            get
            {
                return _search ?? (_search = new RelayCommand(
                    o =>
                    {
                        Users=GetUsers(_looking);

                    }, 
                    o => true));
            }
        }



        private ICommand _changefollow;
        public ICommand ChangeFollow
        {
            get
            {
                return _changefollow ?? (_changefollow = new RelayCommand(
                    o =>
                    {
                        if (SelectedUser != null)
                        {
                            if (SelectedUser.IfFollow == true) {

                                var Del = _dataContext.Follows.Where(g => g.FollowerId == _selectedUser.Id).First(f => f.UserId == _authenticator.CurrentUser.Id);
                                _dataContext.Follows.Remove(Del);

                                _dataContext.SaveChanges();
                            }
                        else
                        {
                            _dataContext.Follows.Add(new Follow()
                            {
                                UserId = _authenticator.CurrentUser.Id,
                                FollowerId = SelectedUser.Id
                            });

                            _dataContext.SaveChanges();
                        }
                        CButton();
                        Users = GetUsers(Looking);
                        Getfollowing();
                        }
                    }, o => true));
            }
        }



        private bool IfCorrectAmount()
        {
            try
            {
                decimal conv = decimal.Parse(Funds);
                if (conv > 0 && conv <= 1000) return false;
                else return true;
            }
            catch (FormatException ex)
            {
                return true;
            }
        }
        private void CButton()
        {
            if (_dataContext.Follows
                   .Where(g => g.FollowerId == _selectedUser.Id)
                   .Count(u => u.UserId == _authenticator.CurrentUser.Id) != 0)
            {
                BText = "Przestań obserwować";
            }
            else
            {
                BText = "Obserwuj";
            }
        }
    }
}
