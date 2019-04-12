using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Repository_Implementations;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Database.UnitTest
{
    class DrinksRepositoryTest
    {
        [Test]
        public void Edit_Entity()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>()
                    .UseInMemoryDatabase(databaseName: "TestOfEditDrinksRepo")
                    .Options;

            using (var uow = new UnitOfWork(options))
            {
                uow.DrinkRepository.Add(new Drink()
                {
                    BarName = "TestBar",
                    DrinksName = "TestDrink",
                    Price = 100
                });
                uow.Complete();

                Drink newDrink = new Drink()
                {
                    BarName = "TestBar",
                    DrinksName = "TestDrink",
                    Price = 600
                };

                uow.DrinkRepository.Edit(newDrink);
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                Assert.AreEqual(600, uow.DrinkRepository.Get("TestBar","TestDrink").Price);
            }
        }
    }
}
