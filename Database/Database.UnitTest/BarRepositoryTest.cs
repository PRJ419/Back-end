using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using NUnit.Framework;

namespace Database.UnitTest
{
    public class BarRepositoryTest
    {
        [SetUp]
        public void setup()
        {
            // HERE'S THE SETUP
        }

        [Test]
        public void BarRepository_Add2Bars1Duplicate_ThrowsException()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "Testfunction")
                    .Options;

            using (var uow = new UnitOfWork(options))
            {
                var bar = new Bar()
                {
                    Address = "Address",
                    AgeLimit = 18,
                    AvgRating = 0,
                    BarName = "Testbar",
                    CVR = 12345678,
                    Educations = "IKT",
                    Email = "FakeMail",
                    ShortDescription = "ShortDesc",
                    LongDescription = "LongDesc",
                };
                uow.BarRepository.Add(bar);
                
                uow.Complete();

                var bar2 = new Bar()
                {
                    Address = "Address2",
                    AgeLimit = 21,
                    AvgRating = 0,
                    BarName = "Testbar",
                    CVR = 87654321,
                    Educations = "IKT",
                    Email = "FakeMail2",
                    ShortDescription = "ShortDesc",
                    LongDescription = "LongDesc"
                };

                Assert.That(() => uow.BarRepository.Add(bar2), Throws.Exception);
            }
        }
    }
}
