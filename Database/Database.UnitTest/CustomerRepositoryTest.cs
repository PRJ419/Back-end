using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Database.UnitTest
{
    public class CustomerRepositoryTest
    {

        [Test]
        public void Add_Entity_To_Database()
        {
           var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "TestOfAddCustomerRepo")
                    .Options;

            using (var uow = new UnitOfWork(options))
            {
                uow.CustomerRepository.Add(new Customer()
                {
                    Username = "TestUsername",
                    Name = "TestName",
                    DateOfBirth = DateTime.MaxValue,
                    Email = "Test@Dab.com",
                    FavoriteBar = "TestBar",
                    FavoriteDrink = "TestDrink"
                });
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                Assert.AreEqual("TestUsername",uow.CustomerRepository.Get("TestUsername").Username);
                Assert.AreEqual("TestName",uow.CustomerRepository.Get("TestUsername").Name);
                Assert.AreEqual(DateTime.MaxValue,uow.CustomerRepository.Get("TestUsername").DateOfBirth);
                Assert.AreEqual("Test@Dab.com",uow.CustomerRepository.Get("TestUsername").Email);
                Assert.AreEqual("TestBar",uow.CustomerRepository.Get("TestUsername").FavoriteBar);
                Assert.AreEqual("TestDrink",uow.CustomerRepository.Get("TestUsername").FavoriteDrink);
            }
        }

        [Test]
        public void Edit_Entity()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "TestOfEditCustomerRepo")
                    .Options;

            using (var uow = new UnitOfWork(options))
            {
                uow.CustomerRepository.Add(new Customer()
                {
                    Username = "TestUsername",
                    Name = "TestName",
                    DateOfBirth = DateTime.MaxValue,
                    Email = "Test@Dab.com",
                    FavoriteBar = "TestBar",
                    FavoriteDrink = "TestDrink"
                });
                uow.Complete();

                Customer newCustomer = new Customer()
                {
                    Username = "TestUsername",
                    Name = "TestName",
                    DateOfBirth = DateTime.MaxValue,
                    Email = "NewMail@Dab.com",
                    FavoriteBar = "NewBar",
                    FavoriteDrink = "NewDrink"
                };

                uow.CustomerRepository.Edit(newCustomer);
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                Assert.AreEqual("TestUsername", uow.CustomerRepository.Get("TestUsername").Username);
                Assert.AreEqual("TestName", uow.CustomerRepository.Get("TestUsername").Name);
                Assert.AreEqual(DateTime.MaxValue, uow.CustomerRepository.Get("TestUsername").DateOfBirth);
                Assert.AreEqual("NewMail@Dab.com", uow.CustomerRepository.Get("TestUsername").Email);
                Assert.AreEqual("NewBar", uow.CustomerRepository.Get("TestUsername").FavoriteBar);
                Assert.AreEqual("NewDrink", uow.CustomerRepository.Get("TestUsername").FavoriteDrink);
            }
        }

        [Test]
        public void Remove_Entity_From_Database()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "TestOfDeleteCustomerRepo")
                    .Options;

            using (var uow = new UnitOfWork(options))
            {
                uow.CustomerRepository.Add(new Customer()
                {
                    Username = "TestUsername",
                    Name = "TestName",
                    DateOfBirth = DateTime.MaxValue,
                    Email = "Test@Dab.com",
                    FavoriteBar = "TestBar",
                    FavoriteDrink = "TestDrink"
                });

                uow.Complete();
                uow.CustomerRepository.Delete("TestUsername");
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                Assert.AreEqual(null, uow.CustomerRepository.Get("TestUsername"));
            }
        }

        [Test]
        public void Get_All_Customers()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "TestOfGetAllCustomerRepo")
                    .Options;

            using (var uow = new UnitOfWork(options))
            {
                uow.CustomerRepository.Add(new Customer()
                {
                    Username = "TestUsername1",
                    Name = "TestName1",
                    DateOfBirth = DateTime.MaxValue,
                    Email = "Test@Dab.com1",
                    FavoriteBar = "TestBar1",
                    FavoriteDrink = "TestDrink1"
                });
                uow.Complete();

                uow.CustomerRepository.Add(new Customer()
                {
                    Username = "TestUsername2",
                    Name = "TestName2",
                    DateOfBirth = DateTime.MaxValue,
                    Email = "Test@Dab.com2",
                    FavoriteBar = "TestBar2",
                    FavoriteDrink = "TestDrink2"
                });
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                Assert.AreEqual(2, uow.CustomerRepository.GetAll().Count());
            }
        }
    }

}