using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vapour.Model;
using Vapour.State;

namespace Vapour.ViewModel
{
    public class StoreViewModel : BaseViewModel
    {
        private readonly VapourDatabaseEntities _dataContext;
        private readonly IAuthenticator _authenticator;

        private List<Game> _games = new List<Game>();
        public List<Game> Games
        {
            get => _games;
            set
            {
                _games = value;
                OnPropertyChanged(nameof(Games));
            }
        }

        private Game _selectedGame;
        public Game SelectedGame
        {
            get => _selectedGame;
            set
            {
                _selectedGame = value;
                Title = value.Title;
                Price = value.Price.ToString();
                Genre = value.Genre.Name;
                Description = value.Description;
                ReleaseDate = value.ReleaseDate.ToString();
                AverageRate = GetAverageRate(value.Id);

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
                OnPropertyChanged(nameof(Price));
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

        private string GetAverageRate(int id)
        {
            if (_dataContext.Rates.Any(r => r.GameId == id) == false)
            {
                return "NA";
            }

            //return _dataContext.Rates
            //    .Where(r => r.GameId == id)
            //    .GroupBy(r => r.GameId)
            //    .Select(r => r.Key)
            //    .Average()
            //    .ToString();

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

            return (SumRate / howMany).ToString();
        }

        private void GetAllGames()
        {
            var games = _dataContext.Games.ToList();

            foreach (var game in games)
            {
               _games.Add(game);
            }
        }

        public StoreViewModel(VapourDatabaseEntities dataContext, IAuthenticator authenticator)
        {
            _dataContext = dataContext;
            _authenticator = authenticator; 
            GetAllGames();
        }
    }
}
