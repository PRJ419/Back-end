using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Database.UnitTest
{
    class BarRepresentativeRepositoryTest
    {
        [Test]
        public void Edit_Entity()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>()
                    .UseInMemoryDatabase(databaseName: "TestOfEditBarRepRepo")
                    .Options;

            using (var uow = new UnitOfWork(options))
            {
                uow.BarRepRepository.Add(new BarRepresentative()
                {
                    Username = "TestUsername",
                    Name = "TestName",
                    BarName = "TestBar"
                });
                uow.Complete();

                BarRepresentative newRep = new BarRepresentative()
                {
                    Username = "TestUsername",
                    Name = "NewName",
                };

                uow.BarRepRepository.Edit(newRep);
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                Assert.AreEqual("NewName", uow.BarRepRepository.Get("TestUsername").Name);
            }

        }
    }
}
