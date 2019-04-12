using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Database.UnitTest
{
    class ReviewRepositoryTest
    {
        [Test]
        public void ReviewRepository_EditExistingReview_RatingChanged()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "EditBarEvent")
                    .Options;

            using (var uow = new UnitOfWork(options))
            {
                var review = new Review()
                {
                    BarName = "Testbar",
                    Username = "TestUser",
                    BarPressure = 3,

                };
                uow.ReviewRepository.Add(review);
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                var editedReview = new Review()
                {
                    BarName = "Testbar",
                    Username = "TestUser",
                    BarPressure = 5,
                };
                uow.ReviewRepository.Edit(editedReview);
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                Assert.AreEqual(5, uow.ReviewRepository.Get("Testbar", "TestUser").BarPressure);
            }
        }
    }
}
