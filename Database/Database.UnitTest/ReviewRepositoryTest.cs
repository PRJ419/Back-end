using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Repository_Implementations;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace Database.UnitTest
{
    [TestFixture]
    class ReviewRepositoryTest
    {
        private ReviewRepository _uut;
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
            _uut = new ReviewRepository(_context);
            _context.Database.EnsureCreated();
        }

        [Test]
        public void ReviewRepository_EditExistingReview_RatingChanged()
        {

            var editedReview = new Review()
            {
                BarName = "Katrines Kælder",
                Username = "Bodega Bent",
                BarPressure = 0,
            };
            _uut.Edit(editedReview);
            _context.SaveChanges();

            Assert.AreEqual(0, _uut.Get("Katrines Kælder", "Bodega Bent").BarPressure);
        }

        [Test]
        public void ReviewRepository_EditExistingReviewWithoutChanging_RatingNotChanged()
        {

            var editedReview = new Review()
            {
                BarName = "Katrines Kælder",
                Username = "Bodega Bent",
                BarPressure = 3,
            };
            _uut.Edit(editedReview);
            _context.SaveChanges();

            Assert.AreEqual(3, _uut.Get("Katrines Kælder", "Bodega Bent").BarPressure);
        }

        [Test]
        public void ReviewRepository_AddTwoWithSameKeys_ThrowsException()
        {
            var review = new Review()
            {
                BarName = "Katrines Kælder",
                Username = "Bodega Bent",
                BarPressure = 3,
            };
            _uut.Add(review);

            Assert.That(()=>_context.SaveChanges(), Throws.Exception);
        }

        [Test]
        public void ReviewRepo_WithoutUsername_ThrowsAnException()
        {
            var reviewToAdd = new Review()
            {
                BarName = "Katrines Kælder",
                //Username = "Bodega Bent",
                BarPressure = 3,
            };
            Assert.That(() => _uut.Add(reviewToAdd), Throws.Exception);
        }

        [Test]
        public void ReviewRepo_WithoutBarName_ThrowsAnException()
        {
            var reviewToAdd = new Review()
            {
                //BarName = "Katrines Kælder",
                Username = "Bodega Bent",
                BarPressure = 3,
            };
            Assert.That(() => _uut.Add(reviewToAdd), Throws.Exception);
        }

        [Test]
        public void ReviewRepo_WithoutKeys_ThrowsAnException()
        {
            var reviewToAdd = new Review()
            {
                //BarName = "Katrines Kælder",
                //Username = "Bodega Bent",
                BarPressure = 3,
            };
            Assert.That(() => _uut.Add(reviewToAdd), Throws.Exception);
        }


        [TearDown]
        public void TearDown()
        {
            _connection.Close();
        }
    }
}
