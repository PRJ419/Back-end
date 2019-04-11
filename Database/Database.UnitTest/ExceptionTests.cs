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
    public class ExceptionTests
    {
        [SetUp]
        public void setup()
        {
            // HERE'S THE SETUP
        }

        [Test]
        public void TestFunction()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "Testfunction")
                    .Options;

            using (var uow = new UnitOfWork(options))
            {
                uow.BarRepository.Add(new Bar()
                    { Address = "Address",
                      AgeLimit = 18,
                      AvgRating = 0,
                      BarName = "Testbar",
                      CVR = 12345678,
                      Educations = "IKT",
                      ShortDescription = "ShortDesc",
                      LongDescription = "LongDesc",
                });
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                Assert.AreEqual(uow.BarRepository.Get().BarName, "Testbar");
            }

        }
    }
}
