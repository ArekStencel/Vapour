using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vapour.Model;

namespace Vapour.Database
{
    internal class DataSeeder
    {
        private readonly VapourDatabaseEntities _dataContext;
        
        public DataSeeder(VapourDatabaseEntities dataContext)
        {
            _dataContext = dataContext;
        }
        
        public void Seed()
        {
            if (_dataContext.Database.Exists())
            {
                Console.WriteLine("siema");
            }
        }
    }
}
