using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vapour.Model;
using Vapour.State;

namespace Vapour.ViewModel
{
    public class LibraryViewModel : BaseViewModel
    {
        private readonly VapourDatabaseEntities _dataContext;
        private readonly IAuthenticator _authenticator;

        private List<string> _gamesCollection = new List<string>();
        public List<string> GamesCollection
        {
            get => _gamesCollection;
            set
            {
                _gamesCollection = value;
                OnPropertyChanged(nameof(GamesCollection));
            }
        }

        private void GetUserGamesCollection()
        {
            var games = _dataContext.GamesCollections.Where(u => u.UserId == _authenticator.CurrentUser.Id).ToList();
            foreach (var game in games)
            {
                _gamesCollection.Add(game.Game.Title);
            }
        }

        public LibraryViewModel(IAuthenticator authenticator, VapourDatabaseEntities dataContext)
        {
            _authenticator = authenticator;
            _dataContext = dataContext;
            GetUserGamesCollection();
        }
    }
}
