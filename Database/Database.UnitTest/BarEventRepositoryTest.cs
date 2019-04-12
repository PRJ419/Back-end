using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Database.UnitTest
{
    class BarEventRepositoryTest
    {
        [Test]
        public void BarEventRepository_EditExistingBarEvent_BarEventEdited()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "EditBarEvent")
                    .Options;

            using (var uow = new UnitOfWork(options))
            {
                var barevent = new BarEvent()
                {
                    BarName = "Testbar",
                    Date = new DateTime(1997,01,01),
                    EventName = "TestEvent",
                };
                uow.BarEventRepository.Add(barevent);
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                var editedBarEvent = new BarEvent()
                {
                    BarName = "Testbar",
                    Date = new DateTime(2019, 12, 12),
                    EventName = "TestEvent",
                };
                uow.BarEventRepository.Edit(editedBarEvent);
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                Assert.AreEqual(new DateTime(2019,12,12), 
                    uow.BarEventRepository.Get("Testbar", "TestEvent").Date );
            }
        }
    }
}
