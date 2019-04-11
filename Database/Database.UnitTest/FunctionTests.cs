using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Database.UnitTest
{
    public class FunctionTests
    {
        private DbContextOptions<BarOMeterContext> options;

        [SetUp]
        public void setup()
        {
            options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "TestOfFunctions")
                    .Options;
        }

        [Test]
        public void Add_Entity_To_Database()
        {
            using (var uow = new UnitOfWork(options))
            {
                uow.BarRepository.Add(new Bar()
                {
                    Address = "Address",
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
                Assert.AreEqual("Testbar", uow.BarRepository.Get("Testbar").BarName);
                Assert.AreEqual(18,uow.BarRepository.Get("Testbar").AgeLimit);
                Assert.AreEqual(0,uow.BarRepository.Get("Testbar").AvgRating);
                Assert.AreEqual("Address",uow.BarRepository.Get("Testbar").Address);
                Assert.AreEqual(12345678,uow.BarRepository.Get("Testbar").CVR);

            }
        }



    }
}