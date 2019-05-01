using System;
using System.Linq;
using Database.Repository_Implementations;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Database.UnitTest
{
    [TestFixture]
    public class CustomerRepositoryTest
    {
        private CustomerRepository _uut;
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
            _uut = new CustomerRepository(_context);
            _context.Database.EnsureCreated();
        }

        [Test]
        public void Add_Entity_To_Database()
        {
            _uut.Add(new Customer()
            {
                Username = "TestUsername",
                Name = "TestName",
                DateOfBirth = DateTime.MaxValue,
                Email = "Test@Dab.com",
                FavoriteBar = "TestBar",
                FavoriteDrink = "TestDrink"
            });
            _context.SaveChanges();
            

            Assert.AreEqual("TestUsername",_uut.Get("TestUsername").Username);
            Assert.AreEqual("TestName",_uut.Get("TestUsername").Name);
            Assert.AreEqual(DateTime.MaxValue,_uut.Get("TestUsername").DateOfBirth);
            Assert.AreEqual("Test@Dab.com",_uut.Get("TestUsername").Email);
            Assert.AreEqual("TestBar",_uut.Get("TestUsername").FavoriteBar);
            Assert.AreEqual("TestDrink",_uut.Get("TestUsername").FavoriteDrink);

        }

        [Test]
        public void Edit_Entity()
        {
            _uut.Add(new Customer()
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

            _uut.Edit(newCustomer);
            _context.SaveChanges();
        
            Assert.AreEqual("TestUsername", _uut.Get("TestUsername").Username);
            Assert.AreEqual("TestName", _uut.Get("TestUsername").Name);
            Assert.AreEqual(DateTime.MaxValue, _uut.Get("TestUsername").DateOfBirth);
            Assert.AreEqual("NewMail@Dab.com", _uut.Get("TestUsername").Email);
            Assert.AreEqual("NewBar", _uut.Get("TestUsername").FavoriteBar);
            Assert.AreEqual("NewDrink", _uut.Get("TestUsername").FavoriteDrink);
            
        }

        [Test]
        public void Remove_Entity_From_Database()
        {
            _uut.Add(new Customer()
            {
                Username = "TestUsername",
                Name = "TestName",
                DateOfBirth = DateTime.MaxValue,
                Email = "Test@Dab.com",
                FavoriteBar = "TestBar",
                FavoriteDrink = "TestDrink"
            });

            _context.SaveChanges();
            _uut.Delete("TestUsername");
            _context.SaveChanges();
            
            Assert.AreEqual(null, _uut.Get("TestUsername"));
            
        }

        [Test]
        public void Get_All_Customers()
        {
            _uut.Add(new Customer()
            {
                Username = "TestUsername1",
                Name = "TestName1",
                DateOfBirth = DateTime.MaxValue,
                Email = "Test@Dab.com1",
                FavoriteBar = "TestBar1",
                FavoriteDrink = "TestDrink1"
            });
            _context.SaveChanges();

            _uut.Add(new Customer()
            {
                Username = "TestUsername2",
                Name = "TestName2",
                DateOfBirth = DateTime.MaxValue,
                Email = "Test@Dab.com2",
                FavoriteBar = "TestBar2",
                FavoriteDrink = "TestDrink2"
            });
            _context.SaveChanges();
            
            Assert.AreEqual(4, _uut.GetAll().Count());
            
        }

        [Test]
        public void CustomerRepository_EmailUnique_ThrowsException()
        {
            _uut.Add(new Customer()
            {
                Username = "TestUsername2",
                Name = "TestName2",
                DateOfBirth = DateTime.MaxValue,
                Email = "Test@Dab.com2",
                FavoriteBar = "TestBar2",
                FavoriteDrink = "TestDrink2"
            });
            _context.SaveChanges();

            Assert.That(()=> _uut.Add(new Customer()
            {
                Username = "TestUsername2",
                Name = "TestName2",
                DateOfBirth = DateTime.MaxValue,
                Email = "Test@Dab.com2",
                FavoriteBar = "TestBar2",
                FavoriteDrink = "TestDrink2"
            }), Throws.Exception);
        }


        [Test]
        public void Find_Customer()
        {
            _uut.Add(new Customer()
            {
                Username = "TestUsername",
                Name = "TestName",
                DateOfBirth = DateTime.MaxValue,
                Email = "Test@Dab.com",
                FavoriteBar = "TestBar",
                FavoriteDrink = "TestDrink"
            });
            _context.SaveChanges();

            _uut.Add(new Customer()
            {
                Username = "TestUsername2",
                Name = "TestName2",
                DateOfBirth = DateTime.MaxValue,
                Email = "Test@Dab.com2",
                FavoriteBar = "TestBar2",
                FavoriteDrink = "TestDrink2"
            });
            _context.SaveChanges();
            
            var foundCustomer = _uut.Find(c => c.Username == "TestUsername2").ToList();
            Assert.Contains(_uut.Get("TestUsername2"),foundCustomer);
        }

        [TearDown]
        public void TearDown()
        {
            _connection.Close();
        }
    }
}