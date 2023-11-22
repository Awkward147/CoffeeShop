using Core;
using System.Linq;

namespace UseCases
{
    public interface ICoffeeService
    {
        IQueryable<CoffeeModel> Get();
    }
}