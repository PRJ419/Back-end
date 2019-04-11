using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Database.UnitTest
{
    public class BarRepositoryTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void BarRepository_AddTwoFindSpecific_FindsSpecific()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "FindSpecific")
                    .Options;

            var bar = new Bar()
            {
                BarName = "Bar",
                Address = "FakeAddress",
                AgeLimit = 18,
                AvgRating = 0,
                CVR = 88888888,
                PhoneNumber = 12345678,
                Educations = "IKT",
                Email = "Fake@email",
                ShortDescription = "Short description",
                LongDescription = "Long description"
            };
            var bar2 = new Bar()
            {
                BarName = "New bar",
                Address = "New fakeAddress",
                AgeLimit = 21,
                AvgRating = 3,
                CVR = 88888888,
                PhoneNumber = 12345679,
                Educations = "ST",
                Email = "NewFake@email",
                ShortDescription = "New short description",
                LongDescription = "New long description"
            };

            using (var uow = new UnitOfWork(options))
            {
                var barFound = uow.BarRepository.Find(x => x.PhoneNumber == 12345678);

                foreach (var bars in barFound)
                {
                    Assert.AreEqual("New bar", bars.BarName);
                    Assert.AreEqual("New fakeAddress", bars.Address);
                    Assert.AreEqual(21, bars.AgeLimit);
                    Assert.AreEqual(3, bars.AvgRating);
                    Assert.AreEqual(88888888, bars.CVR);
                    Assert.AreEqual(12345679, bars.PhoneNumber);
                    Assert.AreEqual("ST", bars.Educations);
                    Assert.AreEqual("NewFake@email", bars.Email);
                    Assert.AreEqual("New short description", bars.ShortDescription);
                    Assert.AreEqual("New long description", bars.LongDescription);
                }
            }

        }

        [Test]
        public void BarRepository_AddTwoBarsGetAll_GetAListOfTwo()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "GetAllList")
                    .Options;

            var bar = new Bar()
            {
                BarName = "Bar",
                Address = "FakeAddress",
                AgeLimit = 18,
                AvgRating = 0,
                CVR = 88888888,
                PhoneNumber = 12345678,
                Educations = "IKT",
                Email = "Fake@email",
                ShortDescription = "Short description",
                LongDescription = "Long description"
            };
            var Bar2 = new Bar()
            {
                BarName = "New bar",
                Address = "New fakeAddress",
                AgeLimit = 21,
                AvgRating = 3,
                CVR = 88888888,
                PhoneNumber = 12345679,
                Educations = "ST",
                Email = "NewFake@email",
                ShortDescription = "New short description",
                LongDescription = "New long description"
            };

            using (var uow = new UnitOfWork(options))
            {
                uow.BarRepository.Add(bar);
                uow.BarRepository.Add(Bar2);
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                Assert.AreEqual(2, uow.BarRepository.GetAll().Count());
            }
        }

        [Test]
        public void BarRepository_EditBar_BarIsEdited()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "EditBar")
                    .Options;

            var bar = new Bar()
            {
                BarName = "New bar",
                Address = "FakeAddress",
                AgeLimit = 18,
                AvgRating = 0,
                CVR = 88888888,
                PhoneNumber = 12345678,
                Educations = "IKT",
                Email = "Fake@email",
                ShortDescription = "Short description",
                LongDescription = "Long description"
            };
            var EditedBar = new Bar()
            {
                BarName = "New bar",
                Address = "New fakeAddress",
                AgeLimit = 21,
                AvgRating = 3,
                CVR = 88888888,
                PhoneNumber = 12345679,
                Educations = "ST",
                Email = "NewFake@email",
                ShortDescription = "New short description",
                LongDescription = "New long description"
            };

            using (var uow = new UnitOfWork(options))
            {

            }
        }

        [Test]
        public void BarRepository_GetBar_GetBarWithKey()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "GetBarWithKey")
                    .Options;

            var bar = new Bar()
            {
                BarName = "New bar",
                Address = "FakeAddress",
                AgeLimit = 18,
                AvgRating = 0,
                CVR = 88888888,
                PhoneNumber = 12345678,
                Educations = "IKT",
                Email = "Fake@email",
                ShortDescription = "Short description",
                LongDescription = "Long description"
            };
            var bar2 = new Bar()
            {
                BarName = "Testbar",
                Address = "FakeAddress2",
                AgeLimit = 18,
                AvgRating = 0,
                CVR = 88888889,
                PhoneNumber = 12345679,
                Educations = "IKT",
                Email = "Fake@email2",
                ShortDescription = "Short description",
                LongDescription = "Long description"
            };
            using (var uow = new UnitOfWork(options))
            {
                uow.BarRepository.Add(bar);
                uow.BarRepository.Add(bar2);
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                Assert.AreEqual("Testbar", uow.BarRepository.Get("Testbar").BarName);
                Assert.AreEqual("FakeAddress2", uow.BarRepository.Get("Testbar").Address);
                Assert.AreEqual(18, uow.BarRepository.Get("Testbar").AgeLimit);
                Assert.AreEqual(0, uow.BarRepository.Get("Testbar").AvgRating);
                Assert.AreEqual(88888889, uow.BarRepository.Get("Testbar").CVR);
                Assert.AreEqual(12345679, uow.BarRepository.Get("Testbar").PhoneNumber);
                Assert.AreEqual("IKT", uow.BarRepository.Get("Testbar").Educations);
                Assert.AreEqual("Fake@email2", uow.BarRepository.Get("Testbar").Email);
                Assert.AreEqual("Short description", uow.BarRepository.Get("Testbar").ShortDescription);
                Assert.AreEqual("Long description", uow.BarRepository.Get("Testbar").LongDescription);
            }
        }

        [Test]
        public void BarRepository_AddBars_NoException()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "AddBars")
                    .Options;
            var bar = new Bar()
            {
                BarName = "New bar",
                Address = "FakeAddress",
                AgeLimit = 18,
                AvgRating = 0,
                CVR = 88888888,
                PhoneNumber = 12345678,
                Educations = "IKT",
                Email = "Fake@email",
                ShortDescription = "Short description",
                LongDescription = "Long description"
            };
            using (var uow = new UnitOfWork(options))
            {
                uow.BarRepository.Add(bar);
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                Assert.AreEqual("New bar", uow.BarRepository.Get("New bar").BarName);
                Assert.AreEqual("FakeAddress", uow.BarRepository.Get("New bar").Address);
                Assert.AreEqual(18, uow.BarRepository.Get("New bar").AgeLimit);
                Assert.AreEqual(0, uow.BarRepository.Get("New bar").AvgRating);
                Assert.AreEqual("IKT", uow.BarRepository.Get("New bar").Educations);
                Assert.AreEqual("Fake@email", uow.BarRepository.Get("New bar").Email);
                Assert.AreEqual("Short description", uow.BarRepository.Get("New bar").ShortDescription);
                Assert.AreEqual("Long description", uow.BarRepository.Get("New bar").LongDescription);
                Assert.AreEqual(12345678, uow.BarRepository.Get("New bar").PhoneNumber);
            }
        }

        [Test]
        public void BarRepository_DeleteExistingBar_BarNoLongerInDB()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "DeleteExistingBar")
                    .Options;
            var bar = new Bar()
            {
                BarName = "New bar",
                Address = "FakeAddress",
                AgeLimit = 18,
                AvgRating = 0,
                CVR = 88888888,
                PhoneNumber = 12345678,
                Educations = "IKT",
                Email = "Fake@email",
                ShortDescription = "Short description",
                LongDescription = "Long description"
            };
            using (var uow = new UnitOfWork(options))
            {
                uow.BarRepository.Add(bar);
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                uow.BarRepository.Delete("New bar");
                uow.Complete();

                Assert.AreEqual(null, uow.BarRepository.Get("New bar"));
            }
        }

        [Test]
        public void BarRepository_Add2Bars1DuplicateName_ThrowsException()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "Add2Bars1Duplicate")
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
                    PhoneNumber = 12345678,
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
                    PhoneNumber = 12345679,
                    Educations = "IKT",
                    Email = "FakeMail2",
                    ShortDescription = "ShortDesc",
                    LongDescription = "LongDesc"
                };

                Assert.That(() =>
                {
                    if (uow != null) uow.BarRepository.Add(bar2);
                }, Throws.Exception);
            }
        }
    }
}
