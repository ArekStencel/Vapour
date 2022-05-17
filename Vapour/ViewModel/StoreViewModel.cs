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
        private readonly VapourDatabaseEntities _dataContext;

        private List<string> _games = new List<string>();
        public List<string> Games
        {
            get => _games;
            set
            {
                _games = value;
                OnPropertyChanged(nameof(Games));
            }
        }

        private void GetAllGames()
        {
            var games = _dataContext.Games.ToList();
            foreach (var game in games.Where(user => _games != null))
            {
                _games.Add(game.ToString());
            }
        }

        public StoreViewModel(VapourDatabaseEntities dataContext)
        {
            _dataContext = dataContext;
            GetAllGames();
        }
    }
}
