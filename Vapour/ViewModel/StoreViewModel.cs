using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Vapour.Model;
using Vapour.Model.Dto;
using Vapour.State;
using Vapour.ViewModel.Base;

namespace Vapour.ViewModel
{
    public class StoreViewModel : BaseViewModel
    {
        private readonly VapourDatabaseEntities _dataContext;
        private readonly IAuthenticator _authenticator;

        private List<GameDto> _games = new List<GameDto>();
        public List<GameDto> Games
        {
            get => _games;
            set
            {
                _games = value;
                OnPropertyChanged(nameof(Games));
            }
        }

        private List<GameCommentDto> _comments = new List<GameCommentDto>();
        public List<GameCommentDto> Comments
        {
            get => _comments;
            set
            {
                _comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }

        private GameDto _selectedGame;
        public GameDto SelectedGame
        {
            get => _selectedGame;
            set
            {
                _selectedGame = value;
                Title = value.Title;
                Price = value.Price.ToString();
                Genre = value.Genre;
                Currency = value.Currency;
                Description = value.Description;
                ReleaseDate = value.ReleaseDate;
                AverageRate = GetAverageRate(value.Id);
                Comments = GetGameComments(value.Id);
                SetButtonText();

                OnPropertyChanged(nameof(SelectedGame));
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _price;
        public string Price
        {
            get => _price;
            set
            {
                _price = value;
                if(_price == "0,0")
                {
                    _price = "Bezpłatne";
                }
                OnPropertyChanged(nameof(Price));
            }
        }

        private string _currency;
        public string Currency
        {
            get => _currency;
            set
            {
                _currency = value;
                OnPropertyChanged(nameof(Currency));
            }
        }

        private string _genre;
        public string Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                OnPropertyChanged(nameof(Genre));
            }
        }

        private string _releaseDate;
        public string ReleaseDate
        {
            get => _releaseDate;
            set
            {
                _releaseDate = value;
                OnPropertyChanged(nameof(ReleaseDate));
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string _averageRate;
        public string AverageRate
        {
            get => _averageRate;
            set
            {
                _averageRate = value;
                OnPropertyChanged(nameof(AverageRate));
            }
        }

        private string _buttonText;
        public string ButtonText
        {
            get => _buttonText;
            set
            {
                _buttonText = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }

        private string GetAverageRate(int id)
        {
            if (_dataContext.Rates.Any(r => r.GameId == id) == false)
            {
                return "NA";
            }

            double SumRate = 0;
            int howMany = 0;
            foreach (var rate in _dataContext.Rates)
            {
                if (rate.GameId == id)
                {
                    SumRate += rate.Rate1;
                    howMany++;
                }
            }

            return Math.Round((SumRate / howMany),2).ToString();
        }

        private List<GameCommentDto> GetGameComments(int id)
        {
            var comments = _dataContext.Comments.ToList();
            var gameComments = new List<GameCommentDto>();

            foreach (var comment in comments)
            {
                if (comment.GameId == id)
                {
                    var user = _dataContext.Users.Where(u => u.Id == comment.UserId).FirstOrDefault();

                    var isFollowing = "";

                    if (_dataContext.Follows
                        .Where(x => x.FollowerId == user.Id)
                        .Where(y => y.UserId == _authenticator.CurrentUser.Id)
                        .Where(z => z.FollowerId != _authenticator.CurrentUser.Id)
                        .Count() != 0)
                    {
                        isFollowing = "(Obserwujesz)";
                    }

                    gameComments.Add(new GameCommentDto() {
                        User = user.Name,
                        IsFollowing = isFollowing,
                        Text = comment.Text,
                        Date = comment.CreatedAt.ToString()
                    }) ;
                }
            }

            return gameComments;
        }

        private void GetAllGames()
        {
            var games = _dataContext.Games.ToList();

            foreach (var game in games)
            {
                var price = game.Price.ToString();
                if (price == "0,0")
                {
                    price = "Bezpłatne";
                }
                var currency = "";
                if (price!= "Bezpłatne")
                {
                    currency = " zł";
                }
                _games.Add(new GameDto()
                {
                    Id = game.Id,
                    Title = game.Title,
                    Price = price,
                    Currency = currency,
                    Genre = game.Genre.Name,
                    Description = game.Description,
                    ReleaseDate = game.ReleaseDate.ToString("dd.MM.yyyy")
                }) ;
            }
        }

        private void SetButtonText()
        {
            if(_dataContext.GamesCollections
            .Where(g => g.GameId == _selectedGame.Id)
            .Where(u => u.UserId == _authenticator.CurrentUser.Id)
            .Count() != 0)
            {
                ButtonText = "Kupiony";
            }
            else
            {
                ButtonText = "Kup";
            }
        }

        public StoreViewModel(VapourDatabaseEntities dataContext, IAuthenticator authenticator)
        {
            _dataContext = dataContext;
            _authenticator = authenticator; 
            GetAllGames();
            SelectedGame = Games[0];
        }

        private ICommand _buyGame;
        public ICommand BuyGame
        {
            get
            {
                return _buyGame ?? (_buyGame = new RelayCommand(
                    (object o) =>
                    {
                        if (IsWalletBallanceGreaterThanGamePrice())
                        {
                            MessageBoxResult declain = MessageBox.Show(
                                "Brak środków na koncie. \nCena gry: " 
                                + _selectedGame.Price 
                                + "\nŚrodki na koncie: " 
                                + _authenticator.CurrentUser.WalletBalance, "Brak środków na koncie", MessageBoxButton.OK);
                            return;
                        }

                        _dataContext.GamesCollections.Add(new GamesCollection()
                        {
                            UserId = _authenticator.CurrentUser.Id,
                            GameId = _selectedGame.Id,
                        }) ;

                        MessageBoxResult accept = MessageBox.Show(
                                "Gratulujemy zakupu. \nCena gry: "
                                + _selectedGame.Price
                                + "\nŚrodki na koncie: "
                                + _authenticator.CurrentUser.WalletBalance
                                + "\nSaldo po zakupie: "
                                + (_authenticator.CurrentUser.WalletBalance-decimal.Parse(_selectedGame.Price)), 
                                "Gratulujemy zakupu", MessageBoxButton.OK);

                        _dataContext.Users.Where(u => u.Id == _authenticator.CurrentUser.Id)
                        .First().WalletBalance -= decimal.Parse(_selectedGame.Price);

                        _dataContext.SaveChanges();
                        SetButtonText();

                    },
                    (object o) =>
                    {
                        if (_dataContext.GamesCollections
                        .Where(g => g.GameId == _selectedGame.Id)
                        .Where(u => u.UserId == _authenticator.CurrentUser.Id)
                        .Count() != 0)
                        {
                            return false;
                        }
                        return true;
                    }));
            }
        }

        private bool IsWalletBallanceGreaterThanGamePrice()
        {
            if (_selectedGame.Price == "Bezpłatne")
            {
                _selectedGame.Price = "0,0";
            }
            return _authenticator.CurrentUser.WalletBalance < decimal.Parse(_selectedGame.Price);
        }
    }
}
