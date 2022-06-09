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
                CheckForRateEdit();
                CommentText = "";
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

            return Math.Round((SumRate / howMany), 2).ToString();
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

                    gameComments.Add(new GameCommentDto()
                    {
                        User = user.Name,
                        IsFollowing = isFollowing,
                        Id = comment.Id,
                        Text = comment.Text,
                        Date = comment.CreatedAt.ToString()
                    });
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
            CommentButtonContent = "Dodaj nowy komentarz";
            RateButtonContent = "Dodaj ocene";
            CheckForRateEdit();
        }

        private ICommand _playGame;

        public ICommand PlayGame
        {
            get
            {
                return _playGame ?? (_playGame = new RelayCommand(
                    (object o) => { MessageBox.Show("Gra jest właśnie uruchamiana"); },
                    (object o) => { return true; }));
            }
        }



        private ICommand _addComment;

        public ICommand AddComment
        {
            get
            {
                return _addComment ?? (_addComment = new RelayCommand(
                    (object o) => { MessageBox.Show("Komentarz został dodany"); },
                    (object o) => { return true; }));
            }
        }


        private ICommand _addRate;

        public ICommand AddRate
        {
            get
            {
                return _addRate ?? (_addRate = new RelayCommand(
                    (object o) => { MessageBox.Show("Ocena została dodana"); },
                    (object o) => { return true; }));
            }
        }


        private string _commentButtonContent;

        public string CommentButtonContent
        {
            get { return _commentButtonContent; }
            set
            {
                _commentButtonContent = value;
                OnPropertyChanged(nameof(CommentButtonContent));
            }
        }

        private string _rateButtonContent;

        public string RateButtonContent
        {
            get { return _rateButtonContent; }
            set
            {
                _rateButtonContent = value;
                OnPropertyChanged(nameof(RateButtonContent));
            }
        }



        private int _sliderValue;

        public int SliderValue
        {
            get { return _sliderValue; }
            set
            {
                _sliderValue = value;
                SliderText = value.ToString();
                OnPropertyChanged(nameof(SliderValue));
            }
        }


        private string _sliderText;

        public string SliderText
        {
            get { return _sliderText; }
            set
            {
                _sliderText = value;
                OnPropertyChanged(nameof(SliderText));
            }
        }




        private string _commentText;

        public string CommentText
        {
            get { return _commentText; }
            set
            {
                _commentText = value;
                OnPropertyChanged(nameof(CommentText));
            }
        }


        private int _commentId;
        public int CommentId
        {
            get => _commentId;
            set
            {
                _commentId = value;
                OnPropertyChanged(nameof(CommentId));
            }
        }





        private GameCommentDto _selectedComment;

        public GameCommentDto SelectedComment
        {
            get => _selectedComment;
            set
            {
                _selectedComment = value;
                if (value != null)
                {
                    CommentId = value.Id;
                }
                OnPropertyChanged(nameof(SelectedComment));
                CheckForCommentEdit();
            }
        }


        public bool CheckForCommentEdit()
        {
            var query = from c in _dataContext.Comments
                where c.Id == CommentId
                select new
                {
                    userId = c.UserId
                };


                if (query.First().userId == _authenticator.CurrentUser.Id)
                {
                    CommentButtonContent = "Edytuj";
                    CommentText = SelectedComment.Text;
                    return true;
                }
                
            CommentButtonContent = "Dodaj nowy komentarz";
            return false;
        }

        public bool CheckForRateEdit()
        {
            var query = from c in _dataContext.Rates
                where c.GameId == SelectedGame.Id 
                && c.UserId == _authenticator.CurrentUser.Id
                select new
                {
                    userId = c.UserId,
                    rate = c.Rate1
                };

            try
            {
                if (query.First().userId == _authenticator.CurrentUser.Id)
                {
                    RateButtonContent = "Edytuj";
                    SliderValue = query.First().rate;
                    return true;
                }
            }
            catch (Exception e)
            {
                RateButtonContent = "Dodaj ocene";
                SliderValue = 0;
                return false;
            }

            RateButtonContent = "Dodaj ocene";
            SliderValue = 0;
            return false;
        }

    }
}
