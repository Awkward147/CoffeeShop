using Core;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Linq;
using UseCases;

namespace CoffeeShop.Controllers
{
    // [ODataRouteComponent("Coffees")]
    
    [Route("odata/Coffees")]
    [ApiController]
    public class CoffeesController : ControllerBase
    {
        private readonly CoffeeShopDbContext coffeeShopDbContext;
        private readonly ICoffeeService coffeeService;


        public CoffeesController(CoffeeShopDbContext coffeeShopDb , ICoffeeService service)
        {
            coffeeService = service;
            coffeeShopDbContext = coffeeShopDb;
        }

        [HttpGet("get")]
        [EnableQuery]
        public IQueryable<CoffeeModel> Get()
        {
            return coffeeService.Get();
            //return coffeeShopDbContext.Coffees;
        }


    }
}
