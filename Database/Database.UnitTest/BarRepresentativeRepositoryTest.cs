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
    [TestFixture]
    class BarRepresentativeRepositoryTest
    {

        private BarRepresentativeRepository _repository;
        private DbContextOptions<BarOMeterContext> _options;
        private BarOMeterContext _context;

        [Test]
        public void Edit_Entity()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>()
                    .UseInMemoryDatabase(databaseName: "TestOfBarRepRepo")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new BarRepresentativeRepository(_context);

            _repository.Add(new BarRepresentative()
            {
                Username = "TestUsername",
                Name = "TestName",
                BarName = "TestBar"
            });
            _context.SaveChanges();

            BarRepresentative newRep = new BarRepresentative()
            {
                Username = "TestUsername",
                Name = "NewName",
            };

            _repository.Edit(newRep);
            _context.SaveChanges();
        

            Assert.AreEqual("NewName", _repository.Get("TestUsername").Name);
        }
    }
}
