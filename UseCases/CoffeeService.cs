using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.RepositoryInterfaces;

namespace UseCases
{
    public class CoffeeService : ICoffeeService
    {
        private readonly ICoffeeRepository coffeeRepository;

        public CoffeeService(ICoffeeRepository coffeeRepository)
        {
            this.coffeeRepository = coffeeRepository;
        }

        public IQueryable<CoffeeModel> Get()
        {
            return coffeeRepository.Get();
        }
    }
}
