using Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CoffeeShopDbContext : IdentityDbContext
    {
        public CoffeeShopDbContext(DbContextOptions<CoffeeShopDbContext> options) : base (options) { }
        
        public DbSet<CoffeeModel> Coffees { get;set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            DummyData data = new DummyData();

            data.SeedData(builder);

            base.OnModelCreating(builder);
        }
    }
}
