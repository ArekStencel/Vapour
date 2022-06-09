using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using Vapour.Model;
using Vapour.Model.Dto;
using Vapour.State;
using Vapour.ViewModel.Base;

namespace Vapour.ViewModel
{
    public class LibraryViewModel : BaseViewModel
    {

       private readonly VapourDatabaseEntities _dataContext;
        private readonly IAuthenticator _authenticator;

        private List<GameDto> _gamesCollection = new List<GameDto>();
        public List<GameDto> GamesCollection
        {
            get => _gamesCollection;
            set
            {
                _gamesCollection = value;
                OnPropertyChanged(nameof(GamesCollection));
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
                AverageRate = GetAverageRate(value.Id);
                Comments = GetGameComments(value.Id);
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

        private void GetUserGames()
        {
            var userGames = from gs in _dataContext.GamesCollections
                join g in _dataContext.Games on gs.GameId equals g.Id
                where gs.UserId == _authenticator.CurrentUser.Id
                select new
                {
                    Title = g.Title,
                    Id = g.Id,
                };


            foreach (var game in userGames)
            {
                _gamesCollection.Add(new GameDto()
                    {
                        Id = game.Id,
                        Title = game.Title,
                    });
            }
        }

        public LibraryViewModel(VapourDatabaseEntities dataContext, IAuthenticator authenticator)
        {
            _dataContext = dataContext;
            _authenticator = authenticator;
            GetUserGames();
            SelectedGame = GamesCollection[0];
        }

        private ICommand _playGame;
        public ICommand PlayGame
        {
            get
            {
                return _playGame ?? (_playGame = new RelayCommand(
                    (object o) =>
                    {
                        MessageBox.Show("Gra jest właśnie uruchamiana");
                    },
                    (object o) =>
                    {
                        return true;
                    }));
            }
        }


        private ICommand _addComment;
        public ICommand AddComment
        {
            get
            {
                return _addComment ?? (_addComment = new RelayCommand(
                    (object o) =>
                    {
                        MessageBox.Show("Dodałeś recenzję!");
                    },
                    (object o) =>
                    {
                        return true;
                    }));
            }
        }
    }
}
