using System;
using System.Linq;
using Database.Repository_Implementations;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Database.UnitTest
{
    [TestFixture]
    public class CustomerRepositoryTest
    {
        private CustomerRepository _repository;
        private DbContextOptions<BarOMeterContext> _options;
        private BarOMeterContext _context;

        [Test]
        public void Add_Entity_To_Database()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "AddCustomer")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new CustomerRepository(_context);


            _repository.Add(new Customer()
            {
                Username = "TestUsername",
                Name = "TestName",
                DateOfBirth = DateTime.MaxValue,
                Email = "Test@Dab.com",
                FavoriteBar = "TestBar",
                FavoriteDrink = "TestDrink"
            });
            _context.SaveChanges();
            

            Assert.AreEqual("TestUsername",_repository.Get("TestUsername").Username);
            Assert.AreEqual("TestName",_repository.Get("TestUsername").Name);
            Assert.AreEqual(DateTime.MaxValue,_repository.Get("TestUsername").DateOfBirth);
            Assert.AreEqual("Test@Dab.com",_repository.Get("TestUsername").Email);
            Assert.AreEqual("TestBar",_repository.Get("TestUsername").FavoriteBar);
            Assert.AreEqual("TestDrink",_repository.Get("TestUsername").FavoriteDrink);

        }

        [Test]
        public void Edit_Entity()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "EditCustomer")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new CustomerRepository(_context);

            _repository.Add(new Customer()
            {
                Username = "TestUsername",
                Name = "TestName",
                DateOfBirth = DateTime.MaxValue,
                Email = "Test@Dab.com",
                FavoriteBar = "TestBar",
                FavoriteDrink = "TestDrink"
            });
            _context.SaveChanges();

            Customer newCustomer = new Customer()
            {
                Username = "TestUsername",
                Name = "TestName",
                DateOfBirth = DateTime.MaxValue,
                Email = "NewMail@Dab.com",
                FavoriteBar = "NewBar",
                FavoriteDrink = "NewDrink"
            };

            _repository.Edit(newCustomer);
            _context.SaveChanges();
        
            Assert.AreEqual("TestUsername", _repository.Get("TestUsername").Username);
            Assert.AreEqual("TestName", _repository.Get("TestUsername").Name);
            Assert.AreEqual(DateTime.MaxValue, _repository.Get("TestUsername").DateOfBirth);
            Assert.AreEqual("NewMail@Dab.com", _repository.Get("TestUsername").Email);
            Assert.AreEqual("NewBar", _repository.Get("TestUsername").FavoriteBar);
            Assert.AreEqual("NewDrink", _repository.Get("TestUsername").FavoriteDrink);
            
        }

        [Test]
        public void Remove_Entity_From_Database()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "RemoveCustomer")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new CustomerRepository(_context);

            _repository.Add(new Customer()
            {
                Username = "TestUsername",
                Name = "TestName",
                DateOfBirth = DateTime.MaxValue,
                Email = "Test@Dab.com",
                FavoriteBar = "TestBar",
                FavoriteDrink = "TestDrink"
            });

            _context.SaveChanges();
            _repository.Delete("TestUsername");
            _context.SaveChanges();
            
            Assert.AreEqual(null, _repository.Get("TestUsername"));
            
        }

        [Test]
        public void Get_All_Customers()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "GetAllCustomers")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new CustomerRepository(_context);

            _repository.Add(new Customer()
            {
                Username = "TestUsername1",
                Name = "TestName1",
                DateOfBirth = DateTime.MaxValue,
                Email = "Test@Dab.com1",
                FavoriteBar = "TestBar1",
                FavoriteDrink = "TestDrink1"
            });
            _context.SaveChanges();

            _repository.Add(new Customer()
            {
                Username = "TestUsername2",
                Name = "TestName2",
                DateOfBirth = DateTime.MaxValue,
                Email = "Test@Dab.com2",
                FavoriteBar = "TestBar2",
                FavoriteDrink = "TestDrink2"
            });
            _context.SaveChanges();
            
            Assert.AreEqual(2, _repository.GetAll().Count());
            
        }


        [Test]
        public void Find_Customer()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "FindCustomer")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new CustomerRepository(_context);

            _repository.Add(new Customer()
            {
                Username = "TestUsername",
                Name = "TestName",
                DateOfBirth = DateTime.MaxValue,
                Email = "Test@Dab.com",
                FavoriteBar = "TestBar",
                FavoriteDrink = "TestDrink"
            });
            _context.SaveChanges();

            _repository.Add(new Customer()
            {
                Username = "TestUsername2",
                Name = "TestName2",
                DateOfBirth = DateTime.MaxValue,
                Email = "Test@Dab.com2",
                FavoriteBar = "TestBar2",
                FavoriteDrink = "TestDrink2"
            });
            _context.SaveChanges();
            
            var foundCustomer = _repository.Find(c => c.Username == "TestUsername2").ToList();
            Assert.Contains(_repository.Get("TestUsername2"),foundCustomer);
        }
    }
}