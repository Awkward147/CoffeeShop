using Core;
using Refit;

namespace CoffeeFront.RefitInterface
{
    public interface ApiInterface
    {
        [Get("/odata/Coffees/get")]
        Task<ApiResponse<List<CoffeeModel>>> GetCoffees();
    }
}
