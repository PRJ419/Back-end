using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Repository_Implementations;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Database.UnitTest
{
    [TestFixture]
    class DrinksRepositoryTest
    {
        private DrinkRepository _uut;
        private DbContextOptions<BarOMeterContext> _options;
        private BarOMeterContext _context;
        private SqliteConnection _connection;


        [SetUp]
        public void Setup()
        {
            _connection = new SqliteConnection("Datasource=:memory:");
            _connection.Open();
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseSqlite(_connection).Options;
            _context = new BarOMeterContext(_options);
            _uut = new DrinkRepository(_context);
            _context.Database.EnsureCreated();
        }

        [Test]
        public void Edit_Entity()
        {
            _uut.Add(new Drink()
            {
                BarName = "Katrines Kælder",
                DrinksName = "TestDrink",
                Price = 100
            });
            _context.SaveChanges();

            Drink newDrink = new Drink()
            {
                BarName = "Katrines Kælder",
                DrinksName = "TestDrink",
                Price = 600
            };

            _uut.Edit(newDrink);
            _context.SaveChanges();
            
            Assert.AreEqual(600, _uut.Get("Katrines Kælder","TestDrink").Price);
        }

        [Test]
        public void Edit_EntityWithoutChanges()
        {
            _uut.Add(new Drink()
            {
                BarName = "Katrines Kælder",
                DrinksName = "TestDrink",
                Price = 100
            });
            _context.SaveChanges();

            Drink newDrink = new Drink()
            {
                BarName = "Katrines Kælder",
                DrinksName = "TestDrink",
                Price = 100
            };

            _uut.Edit(newDrink);
            _context.SaveChanges();

            Assert.AreEqual(100, _uut.Get("Katrines Kælder", "TestDrink").Price);
        }

        [Test]
        public void DrinkRepository_AddTwoEntitiesWithSameKeys_ExceptionThrown()
        {
            _uut.Add(new Drink()
            {
                BarName = "Katrines Kælder",
                DrinksName = "TestDrink",
                Price = 100
            });
            _context.SaveChanges();

            Assert.That(()=> _uut.Add(new Drink()
            {
                BarName = "Katrines Kælder",
                DrinksName = "TestDrink",
                Price = 100
            }), Throws.Exception);
            
        }

        [TearDown]
        public void TearDown()
        {
            _connection.Close();
        }
    }
}
