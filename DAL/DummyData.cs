using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DummyData
    {
        public void SeedData(ModelBuilder modelBuilder)
        {
            for (int i = 1; i <= 5; i++)
            {
                modelBuilder.Entity<CoffeeModel>().HasData(
                    new CoffeeModel
                    {
                        CoffeeId = i,
                        Name = $"Coffee {i}",
                        Price = 4.99m + i, // Adjust the price as needed
                        ImageUrl = $"https://example.com/image{i}.jpg"
                    }
                );
            }
        }
    }
}
