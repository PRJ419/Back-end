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
    class BarEventRepositoryTest
    {
        private BarEventRepository _uut;
        private DbContextOptions<BarOMeterContext> _options;
        private BarOMeterContext _context;
        private SqliteConnection _connection;
        private BarEvent _barEvent;
        private BarEvent _barEvent2;


        /// <summary>
        /// This setup is run before every test, hence creating the database before every test.
        /// TearDown makes sure to delete the database between each test, to make sure we have a clean
        /// database.
        /// Due to DataSeeding there will always be two bars in the repository, which is why
        /// the tests sometimes assert on these.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _connection = new SqliteConnection("Datasource=:memory:");
            _connection.Open();
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseSqlite(_connection).Options;
            _context = new BarOMeterContext(_options);
            _uut = new BarEventRepository(_context);
            _context.Database.EnsureCreated();

            _barEvent = new BarEvent()
            {
                BarName = "Katrines Kælder",
                Date = new DateTime(1997, 01, 01),
                EventName = "TestEvent",
                Image = "www.testbillede.dk",
            };
            _barEvent2 = new BarEvent()
            {
                BarName = "Katrines Kælder",
                Date = new DateTime(2019, 01, 01),
                EventName = "TestEvent2",
                Image = "www.testbillede.dk",
            };
        }
        [Test]
        public void BarEventRepository_EditExistingBarEvent_BarEventEdited()
        {
            _uut.Add(_barEvent);
            _context.SaveChanges();

            var editedBarEvent = new BarEvent()
            {
                BarName = "Katrines Kælder",
                Date = new DateTime(2019, 12, 12),
                EventName = "TestEvent",
                Image = "www.testbillede.dk",
            };
            _uut.Edit(editedBarEvent);
            _context.SaveChanges();

            Assert.AreEqual(new DateTime(2019,12,12),
                _uut.Get("Katrines Kælder", "TestEvent").Date );
            
        }

        [Test]
        public void BarEventRepository_EditExistingBarEventWithoutChanging_BarEventEdited()
        {
            _uut.Add(_barEvent);
            _context.SaveChanges();

            var editedBarEvent = new BarEvent()
            {
                BarName = "Katrines Kælder",
                Date = new DateTime(1997, 01, 01),
                EventName = "TestEvent",
                Image = "www.testbillede.dk",
            };
            _uut.Edit(editedBarEvent);
            _context.SaveChanges();

            Assert.AreEqual(new DateTime(1997, 01, 01),
                _uut.Get("Katrines Kælder", "TestEvent").Date);

        }

        [Test]
        public void BarEventRepository_AddSeveralEventsToSameBar_EventsAdded()
        {
            _uut.Add(_barEvent);
            _uut.Add(_barEvent2);

            Assert.AreEqual(2, _context.SaveChanges());
        }



        [Test]
        public void BarEventRepository_ConstraintOnEventName_ThrowsException()
        {
            var barEvent = new BarEvent()
            {
                BarName = "Katrines Kælder",
                Date = new DateTime(1997, 01, 01),
                EventName = "Hej",
                Image = "www.testbillede.dk",
            };
            var barEvent2 = new BarEvent()
            {
                BarName = "Katrines Kælder",
                Date = new DateTime(1997, 01, 01),
                EventName = "Hej",
                Image = "www.testbillede.dk",
            };
            _uut.Add(barEvent);
            Assert.That(() => _uut.Add(barEvent2), Throws.Exception);
        }

        /// <summary>
        /// Should fail, but apparently doesn't due to some weirdness with EFCore's Add() function
        /// </summary>
        [Test]
        public void BarEventRepository_AddWithoutBarName_ThrowsException()
        {
            var eventToAdd = new BarEvent()
            {
                //BarName = "Katrines Kælder",
                Date = new DateTime(2000,01,01),
                EventName = "EventName",
                Image = "FakeImg"
            };

            Assert.That(() => _uut.Add(eventToAdd), Throws.Exception);
        }

        [Test]
        public void BarEventRepository_AddWithoutEventName_ThrowsException()
        {
            var eventToAdd = new BarEvent()
            {
                BarName = "Katrines Kælder",
                Date = new DateTime(2000, 01, 01),
                //EventName = "EventName",
                Image = "FakeImg"
            };

            Assert.That(() => _uut.Add(eventToAdd), Throws.Exception);
        }

        [Test]
        public void BarEventRepository_AddWithoutKeys_ThrowsException()
        {
            var eventToAdd = new BarEvent()
            {
                //BarName = "Katrines Kælder",
                Date = new DateTime(2000, 01, 01),
                //EventName = "EventName",
                Image = "FakeImg"
            };

            Assert.That(() => _uut.Add(eventToAdd), Throws.Exception);
        }

        [TearDown]
        public void TearDown()
        {
            _connection.Close();
        }
    }
}
