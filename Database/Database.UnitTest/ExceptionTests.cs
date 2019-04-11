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
        private DbContextOptions<BarOMeterContext> options;

        [SetUp]
        public void setup()
        {
            options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "Testfunction")
                    .Options;
        }

        [Test]
        public void TestFunction()
        {
      

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
                //Assert.AreEqual(uow.BarRepository.Get().BarName, "Testbar");
                Assert.AreEqual("Testbar",uow.BarRepository.Get("Testbar").BarName);
            }

        }
    }
}
