using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.RepositoryInterfaces;

namespace DAL
{
    public class CoffeeRepository : ICoffeeRepository
    {
        private readonly CoffeeShopDbContext dbContext;

        public CoffeeRepository(CoffeeShopDbContext db)
        {
            dbContext = db;
        }


        public IQueryable<CoffeeModel> Get()
        {
            return dbContext.Coffees;
        }
    }
}
