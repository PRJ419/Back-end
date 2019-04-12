using System.Linq;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
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
        public void BarRepository_GetBestBars_GetsListOfBars()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "GetBest")
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
                uow.BarRepository.Add(bar);
                uow.BarRepository.Add(bar2);
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                var bars = uow.BarRepository.GetBestBars().ToList();
                
                Assert.AreEqual("New bar", bars[0].BarName);
                Assert.AreSame("Bar", bars[1].BarName);
            }

        }

        [Test]
        public void BarRepository_GetWorstBars_GetsListOfBars()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "GetWorst")
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
                uow.BarRepository.Add(bar);
                uow.BarRepository.Add(bar2);
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                var bars = uow.BarRepository.GetWorstBars().ToList();

                Assert.AreSame("Bar", bars[0].BarName);
                Assert.AreSame("New bar", bars[1].BarName);
            }

        }

        [Test]
        public void BarRepository_AddThreeBarsRequestTwo_GetListOfTwo()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "GetTwo")
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
                CVR = 88888858,
                PhoneNumber = 12345679,
                Educations = "ST",
                Email = "NewFake@email",
                ShortDescription = "New short description",
                LongDescription = "New long description"
            };
            var bar3 = new Bar()
            {
                BarName = "New new bar",
                Address = "New new fakeAddress",
                AgeLimit = 23,
                AvgRating = 4,
                CVR = 88888884,
                PhoneNumber = 12345679,
                Educations = "ST",
                Email = "NewNewFake@email",
                ShortDescription = "New new short description",
                LongDescription = "New new long description"
            };

            using (var uow = new UnitOfWork(options))
            {
                uow.BarRepository.Add(bar);
                uow.BarRepository.Add(bar2);
                uow.BarRepository.Add(bar3);
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                var bars = uow.BarRepository.GetXBars(1, 2).ToList();
                Assert.AreEqual(2, bars.Count);
                Assert.AreEqual("New bar", bars[0].BarName);
                Assert.AreEqual("New new bar", bars[1].BarName);
            }
        }

        [Test]
        public void BarRepository_AddThreeBarsRequestZero_GetEmptyList()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "GetEmpty")
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
                CVR = 88888858,
                PhoneNumber = 12345679,
                Educations = "ST",
                Email = "NewFake@email",
                ShortDescription = "New short description",
                LongDescription = "New long description"
            };
            var bar3 = new Bar()
            {
                BarName = "New new bar",
                Address = "New new fakeAddress",
                AgeLimit = 23,
                AvgRating = 4,
                CVR = 88888884,
                PhoneNumber = 12345679,
                Educations = "ST",
                Email = "NewNewFake@email",
                ShortDescription = "New new short description",
                LongDescription = "New new long description"
            };

            using (var uow = new UnitOfWork(options))
            {
                uow.BarRepository.Add(bar);
                uow.BarRepository.Add(bar2);
                uow.BarRepository.Add(bar3);
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                var bars = uow.BarRepository.GetXBars(0, 0);
                Assert.AreEqual(0, bars.Count());
            }
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
                Assert.That(barFound.All(x=>x.PhoneNumber == 12345678));
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
                uow.BarRepository.Add(bar);
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                uow.BarRepository.Edit(EditedBar);
                uow.Complete();
                Assert.AreEqual("New bar", uow.BarRepository.Get("New bar").BarName);
                Assert.AreEqual("New fakeAddress", uow.BarRepository.Get("New bar").Address);
                Assert.AreEqual(21, uow.BarRepository.Get("New bar").AgeLimit);
                Assert.AreEqual(3, uow.BarRepository.Get("New bar").AvgRating);
                Assert.AreEqual("ST", uow.BarRepository.Get("New bar").Educations);
                Assert.AreEqual(12345679, uow.BarRepository.Get("New bar").PhoneNumber);
                Assert.AreEqual(88888888, uow.BarRepository.Get("New bar").CVR);
                Assert.AreEqual("NewFake@email", uow.BarRepository.Get("New bar").Email);
                Assert.AreEqual("New short description", uow.BarRepository.Get("New bar").ShortDescription);
                Assert.AreEqual("New long description", uow.BarRepository.Get("New bar").LongDescription);

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

                Assert.That(() => uow.BarRepository.Add(bar2), Throws.Exception);
            }
        }
    }
}
