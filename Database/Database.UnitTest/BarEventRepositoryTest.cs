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
    class BarEventRepositoryTest
    {
        private BarEventRepository _repository;
        private DbContextOptions<BarOMeterContext> _options;
        private BarOMeterContext _context;
        [Test]
        public void BarEventRepository_EditExistingBarEvent_BarEventEdited()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "EditBarEvent")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new BarEventRepository(_context);

            var barevent = new BarEvent()
            {
                BarName = "Testbar",
                Date = new DateTime(1997,01,01),
                EventName = "TestEvent",
            };
            _repository.Add(barevent);
            _context.SaveChanges();

            var editedBarEvent = new BarEvent()
            {
                BarName = "Testbar",
                Date = new DateTime(2019, 12, 12),
                EventName = "TestEvent",
            };
            _repository.Edit(editedBarEvent);
            _context.SaveChanges();

            Assert.AreEqual(new DateTime(2019,12,12),
                _repository.Get("Testbar", "TestEvent").Date );
            
        }
    }
}
