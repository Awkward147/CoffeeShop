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
            for (int i = 1; i <= 11; i++)
            {
                modelBuilder.Entity<CoffeeModel>().HasData(
                    new CoffeeModel
                    {
                        CoffeeId = i,
                        Name = $"Coffee {i}",
                        Price = 4.99m + i, 
                        ImageUrl = "https://i.ibb.co/T45P0Kh/360-F-116619399-YA611b-KNOW35ff-K0-Oiyua-Ocj-Ag-Xg-KBui.jpg"
                    }
                );
            }
        }
    }
}
