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
    class ReviewRepositoryTest
    {
        private ReviewRepository _repository;
        private DbContextOptions<BarOMeterContext> _options;
        private BarOMeterContext _context;

        [Test]
        public void ReviewRepository_EditExistingReview_RatingChanged()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "EditReview")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new ReviewRepository(_context);

            var review = new Review()
            {
                BarName = "Testbar",
                Username = "TestUser",
                BarPressure = 3,
            };
            _repository.Add(review);
            _context.SaveChanges();

            var editedReview = new Review()
            {
                BarName = "Testbar",
                Username = "TestUser",
                BarPressure = 5,
            };
            _repository.Edit(editedReview);
            _context.SaveChanges();

            Assert.AreEqual(5, _repository.Get("Testbar", "TestUser").BarPressure);
            
        }
    }
}
