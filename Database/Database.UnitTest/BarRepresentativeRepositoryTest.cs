using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entities;
using Database.Repository_Implementations;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Database.UnitTest
{
    [TestFixture]
    class BarRepresentativeRepositoryTest
    {

        private BarRepresentativeRepository _uut;
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
            _uut = new BarRepresentativeRepository(_context);
            _context.Database.EnsureCreated();
        }

        [Test]
        public void Edit_Entity()
        {
            _uut.Add(new BarRepresentative()
            {
                Username = "TestUsername",
                Name = "TestName",
                BarName = "Katrines Kælder"
            });
            _context.SaveChanges();

            BarRepresentative newRep = new BarRepresentative()
            {
                Username = "TestUsername",
                Name = "NewName",
            };

            _uut.Edit(newRep);
            _context.SaveChanges();
        

            Assert.AreEqual("NewName", _uut.Get("TestUsername").Name);
        }

        [Test]
        public void Edit_Entity_WithoutChangingAnyInformation()
        {
            _uut.Add(new BarRepresentative()
            {
                Username = "TestUsername",
                Name = "TestName",
                BarName = "Katrines Kælder"
            });
            _context.SaveChanges();

            BarRepresentative newRep = new BarRepresentative()
            {
                Username = "TestUsername",
                Name = "TestName",
            };

            _uut.Edit(newRep);
            _context.SaveChanges();

            Assert.AreEqual("TestName", _uut.Get("TestUsername").Name);
        }


        [Test]
        public void Add_Several_Representatives()
        {

            var _barRep = new BarRepresentative()
            {
                Username = "TestUsername",
                Name = "TestName",
                BarName = "Katrines Kælder"
            };
            _uut.Add(_barRep);
            

            var _barRep2 = new BarRepresentative()
            {
                Username = "TestUsername2",
                Name = "TestName2",
                BarName = "Katrines Kælder"
            };
            _uut.Add(_barRep2);
            

            Assert.AreEqual(2,_context.SaveChanges());
        }

        [Test]
        public void Constraint_OnUsername_ThrowsAnException()
        {
            var _barRep = new BarRepresentative()
            {
                Username = "TestUsername",
                Name = "TestName",
                BarName = "Katrines Kælder"
            };
            _uut.Add(_barRep);
            _context.SaveChanges();

            var _barRep2 = new BarRepresentative()
            {
                Username = "TestUsername",
                Name = "TestName2",
                BarName = "Katrines Kælder",
            };

            Assert.That(()=>_uut.Add(_barRep2),Throws.Exception);
        }

        [Test]
        public void BarRepRepo_WithoutUserName_ThrowsAnException()
        {
            var barRep = new BarRepresentative()
            {
                //Username = "TestUsername",
                Name = "TestName",
                BarName = "Katrines Kælder"
            };
            
            Assert.That(() => _uut.Add(barRep), Throws.Exception);
        }


        /// <summary>
        /// Fails because primary keys can be null in SQLite
        /// </summary>
        [Test]
        public void BarRepRepo_WithoutBarName_ThrowsAnException()
        {
            var barRep = new BarRepresentative()
            {
                Username = "TestUsername",
                Name = "TestName",
                //BarName = "Katrines Kælder"
            };

            Assert.That(() => _uut.Add(barRep), Throws.Exception);
        }

        [TearDown]
        public void TearDown()
        {
            _connection.Close();
        }
    }
}
