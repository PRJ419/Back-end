using System.Linq;
using Database.Repository_Implementations;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Database.UnitTest
{
    [TestFixture]
    public class BarRepositoryTest
    {
        private BarRepository _repository;
        private DbContextOptions<BarOMeterContext> _options;
        private BarOMeterContext _context;

        [Test]
        public void BarRepository_GetBestBars_GetsListOfBars()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "GetBest")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new BarRepository(_context);

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
                LongDescription = "Long description",
                Image = "FakeImg"
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
                LongDescription = "New long description",
                Image = "FakeImg"
            };

            _repository.Add(bar);
            _repository.Add(bar2);
            _context.SaveChanges();

            var bars = _repository.GetBestBars().ToList();
            Assert.AreEqual("New bar", bars[0].BarName);
            Assert.AreSame("Bar", bars[1].BarName);
        }

        [Test]
        public void BarRepository_GetWorstBars_GetsListOfBars()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "GetWorst")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new BarRepository(_context);

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
                LongDescription = "Long description",
                Image = "FakeImg"
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
                LongDescription = "New long description",
                Image = "FakeImg"
            };

            _repository.Add(bar);
            _repository.Add(bar2);
            _context.SaveChanges();

            var bars = _repository.GetWorstBars().ToList();

            Assert.AreSame("Bar", bars[0].BarName);
            Assert.AreSame("New bar", bars[1].BarName);
        }

        [Test]
        public void BarRepository_AddThreeBarsRequestTwo_GetListOfTwo()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "AddThreeGetTwo")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new BarRepository(_context);

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
                LongDescription = "Long description",
                Image = "FakeImg"
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
                LongDescription = "New long description",
                Image = "FakeImg"
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
                LongDescription = "New new long description",
                Image = "FakeImg"
            };

            _repository.Add(bar);
            _repository.Add(bar2);
            _repository.Add(bar3);
            _context.SaveChanges();
            

            var bars = _repository.GetXBars(1, 2).ToList();
            Assert.AreEqual(2, bars.Count);
            Assert.AreEqual("New bar", bars[0].BarName);
            Assert.AreEqual("New new bar", bars[1].BarName);
            
        }

        [Test]
        public void BarRepository_AddThreeBarsRequestZero_GetEmptyList()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "GetEmpty")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new BarRepository(_context);

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
                LongDescription = "Long description",
                Image = "FakeImg"
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
                LongDescription = "New long description",
                Image = "FakeImg"
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
                LongDescription = "New new long description",
                Image = "FakeImg"
            };

            _repository.Add(bar);
            _repository.Add(bar2);
            _repository.Add(bar3);
            _context.SaveChanges();

            var bars = _repository.GetXBars(0, 0);
            Assert.AreEqual(0, bars.Count());
        }


        [Test]
        public void BarRepository_AddTwoFindSpecific_FindsSpecific()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "FindSpecific")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new BarRepository(_context);

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
                LongDescription = "Long description",
                Image = "FakeImg"
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
                LongDescription = "New long description",
                Image = "FakeImg"
            };
            _repository.Add(bar);
            _repository.Add(bar2);
            _context.SaveChanges();

            var barFound = _repository.Find(x => x.PhoneNumber == 12345678);
            Assert.That(barFound.All(x=>x.PhoneNumber == 12345678));
            
        }

        [Test]
        public void BarRepository_AddTwoBarsGetAll_GetAListOfTwo()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "GetListOfAll")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new BarRepository(_context);

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
                LongDescription = "Long description",
                Image = "FakeImg"
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
                LongDescription = "New long description",
                Image = "FakeImg"
            };

            _repository.Add(bar);
            _repository.Add(bar2);
            _context.SaveChanges();

            Assert.AreEqual(2, _repository.GetAll().Count());
            
        }

        [Test]
        public void BarRepository_EditBar_BarIsEdited()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "EditBar")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new BarRepository(_context);

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
                LongDescription = "Long description",
                Image = "FakeImg"
            };
            var editedBar = new Bar()
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
                LongDescription = "New long description",
                Image = "New FakeImg"
            };

            _repository.Add(bar);
            _context.SaveChanges();

            _repository.Edit(editedBar);
            _context.SaveChanges();
            Assert.AreEqual("New bar", _repository.Get("New bar").BarName);
            Assert.AreEqual("New fakeAddress", _repository.Get("New bar").Address);
            Assert.AreEqual(21, _repository.Get("New bar").AgeLimit);
            Assert.AreEqual(3, _repository.Get("New bar").AvgRating);
            Assert.AreEqual("ST", _repository.Get("New bar").Educations);
            Assert.AreEqual(12345679, _repository.Get("New bar").PhoneNumber);
            Assert.AreEqual(88888888, _repository.Get("New bar").CVR);
            Assert.AreEqual("NewFake@email", _repository.Get("New bar").Email);
            Assert.AreEqual("New short description", _repository.Get("New bar").ShortDescription);
            Assert.AreEqual("New long description", _repository.Get("New bar").LongDescription);
            Assert.AreEqual("New FakeImg", _repository.Get("New bar").Image);

        }

        [Test]
        public void BarRepository_GetBar_GetBarWithKey()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "GetBarWithKey")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new BarRepository(_context);

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
                LongDescription = "Long description",
                Image = "FakeImg"
            };
            var bar2 = new Bar()
            {
                BarName = "Testbar",
                Address = "FakeAddress2",
                AgeLimit = 18,
                AvgRating = 0,
                CVR = 88888889,
                PhoneNumber = 12345679,
                Educations = "IKT2",
                Email = "Fake@email2",
                ShortDescription = "Short description2",
                LongDescription = "Long description2",
                Image = "FakeImg2"
            };
            
            _repository.Add(bar);
            _repository.Add(bar2);
            _context.SaveChanges();
            

            Assert.AreEqual("Testbar", _repository.Get("Testbar").BarName);
            Assert.AreEqual("FakeAddress2", _repository.Get("Testbar").Address);
            Assert.AreEqual(18, _repository.Get("Testbar").AgeLimit);
            Assert.AreEqual(0, _repository.Get("Testbar").AvgRating);
            Assert.AreEqual(88888889, _repository.Get("Testbar").CVR);
            Assert.AreEqual(12345679, _repository.Get("Testbar").PhoneNumber);
            Assert.AreEqual("IKT2", _repository.Get("Testbar").Educations);
            Assert.AreEqual("Fake@email2", _repository.Get("Testbar").Email);
            Assert.AreEqual("Short description2", _repository.Get("Testbar").ShortDescription);
            Assert.AreEqual("Long description2", _repository.Get("Testbar").LongDescription);
            Assert.AreEqual("FakeImg2", _repository.Get("Testbar").Image);
            
        }

        [Test]
        public void BarRepository_AddBars_NoException()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "AddBars")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new BarRepository(_context);

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
                LongDescription = "Long description",
                Image = "FakeImg"
            };
            _repository.Add(bar);
            _context.SaveChanges();
            
            Assert.AreEqual("New bar", _repository.Get("New bar").BarName);
            Assert.AreEqual("FakeAddress", _repository.Get("New bar").Address);
            Assert.AreEqual(18, _repository.Get("New bar").AgeLimit);
            Assert.AreEqual(0, _repository.Get("New bar").AvgRating);
            Assert.AreEqual("IKT", _repository.Get("New bar").Educations);
            Assert.AreEqual("Fake@email", _repository.Get("New bar").Email);
            Assert.AreEqual("Short description", _repository.Get("New bar").ShortDescription);
            Assert.AreEqual("Long description", _repository.Get("New bar").LongDescription);
            Assert.AreEqual(12345678, _repository.Get("New bar").PhoneNumber);
            Assert.AreEqual("FakeImg", _repository.Get("New bar").Image);
            
        }

        [Test]
        public void BarRepository_DeleteExistingBar_BarNoLongerInDB()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "DeleteExistingBar")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new BarRepository(_context);

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
                LongDescription = "Long description",
                Image = "FakeImg"
            };

            _repository.Add(bar);
            _context.SaveChanges();
            

            _repository.Delete("New bar");
            _context.SaveChanges();
            Assert.AreEqual(null, _repository.Get("New bar"));
            
        }

        [Test]
        public void BarRepository_Add2Bars1DuplicateName_ThrowsException()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "Add2Bars1Duplicate")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new BarRepository(_context);


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
                Image = "FakeImg"
            };
            _repository.Add(bar);
            _context.SaveChanges();

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
                LongDescription = "LongDesc",
                Image = "FakeImg"
            };

                Assert.That(() => _repository.Add(bar2), Throws.Exception);
            
        }
    }
}
