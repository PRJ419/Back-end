using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using Database;
using Database.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WebApi.Controllers;
using WebApi.DTOs.AutoMapping;
using WebApi.DTOs.Coupon;
using WebApi.DTOs.Customers;

namespace WebApi.Test.UnitTest.ControllerTests
{
    [TestFixture]
    public class CustomerControllerTests
    {

        private CustomerController uut;
        private IUnitOfWork mockUnitOfWork;
        private IMapper mapper;
        private List<Customer> defaultList;
        private Customer defaultCustomer;
        private CustomerDto defaultCustomerDto;
        private List<CustomerDto> correctResultList;


        [SetUp]
        public void Setup()
        {
            mockUnitOfWork = Substitute.For<IUnitOfWork>();

            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            mapper = new Mapper(configuration);

            uut = new CustomerController(mockUnitOfWork, mapper);

            defaultList = new List<Customer>()
            {
                new Customer()
                {
                    Name = "TestBruger",
                    Reviews = null,
                    Email = "test@gmail.com",
                    Username = "TheLegend27",
                    DateOfBirth = DateTime.Now,
                    FavoriteBar = "Katrines Kælder",
                    FavoriteDrink = "Isvand"
                },
                new Customer()
                {
                    Name = "TestBruger2",
                    Reviews = null,
                    Email = "test2@gmail.com",
                    Username = "xXNoobXx",
                    DateOfBirth = DateTime.Now,
                    FavoriteBar = "Katrines Kælder",
                    FavoriteDrink = "Isvand med øl"
                }
            };
            defaultCustomer = defaultList[0];

            // Direct conversion without navigational property
            correctResultList = new List<CustomerDto>()
            {
                new CustomerDto()
                {
                    Name = "TestBruger",
                    Email = "test@gmail.com",
                    Username = "TheLegend27",
                    DateOfBirth = DateTime.Now,
                    FavoriteBar = "Katrines Kælder",
                    FavoriteDrink = "Isvand"
                },
                new CustomerDto()
                {
                    Name = "TestBruger2",
                    Email = "test2@gmail.com",
                    Username = "xXNoobXx",
                    DateOfBirth = DateTime.Now,
                    FavoriteBar = "Katrines Kælder",
                    FavoriteDrink = "Isvand med øl"
                }
            };
            defaultCustomerDto = correctResultList[0];
        }


        [Test]
        public void GetCustomers_UnitOfWorkReturnsList_UutReturnsCorrectType()
        {
            mockUnitOfWork.CustomerRepository
                .GetAll()
                .Returns(defaultList);

            var result = uut.GetCustomers();
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetCustomers_UnitOfWorkReturnsEmptyList_UutReturnsCorrectType()
        {
            mockUnitOfWork.CustomerRepository
                .Find(Arg.Any<Expression<Func<Customer, bool>>>())
                .Returns(new List<Customer>());

            var result = uut.GetCustomers();
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void GetCustomers_UnitOfWorkReturnsList_UutReturnsCorrectDtoList()
        {

            mockUnitOfWork.CustomerRepository.GetAll()
                .Returns(defaultList);

            var objectResult = uut.GetCustomers();
            var result = (objectResult as OkObjectResult).Value as List<CustomerDto>;

            Assert.That(result[0].Username, Is.EqualTo(defaultList[0].Username));
            Assert.That(result[0].Name, Is.EqualTo(defaultList[0].Name));
            Assert.That(result[0].Email, Is.EqualTo(defaultList[0].Email));
            Assert.That(result[0].DateOfBirth, Is.EqualTo(defaultList[0].DateOfBirth));
            Assert.That(result[0].FavoriteBar, Is.EqualTo(defaultList[0].FavoriteBar));
            Assert.That(result[0].FavoriteDrink, Is.EqualTo(defaultList[0].FavoriteDrink));

            Assert.That(result[1].Username, Is.EqualTo(defaultList[1].Username));
            Assert.That(result[1].Name, Is.EqualTo(defaultList[1].Name));
            Assert.That(result[1].Email, Is.EqualTo(defaultList[1].Email));
            Assert.That(result[1].DateOfBirth, Is.EqualTo(defaultList[1].DateOfBirth));
            Assert.That(result[1].FavoriteBar, Is.EqualTo(defaultList[1].FavoriteBar));
            Assert.That(result[1].FavoriteDrink, Is.EqualTo(defaultList[1].FavoriteDrink));
        }

        [Test]
        public void GetCustomer_UnitOfWorkReturnsCustomer_UutReturnsCorrectType()
        {
            var key = defaultCustomerDto.Username;
            mockUnitOfWork.CustomerRepository.Get(key)
                .Returns(defaultCustomer);

            var result = uut.GetCustomer(key);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetCustomer_UnitOfWorkReturnsCustomer_UutReturnsCorrectCustomerDto()
        {
            var key = defaultCustomerDto.Username;
            mockUnitOfWork.CustomerRepository.Get(key)
                .Returns(defaultCustomer);

            var result = uut.GetCustomer(key);
            var resultObj = (result as OkObjectResult).Value as CustomerDto;

            Assert.That(resultObj.Username, Is.EqualTo(defaultList[0].Username));
            Assert.That(resultObj.Name, Is.EqualTo(defaultList[0].Name));
            Assert.That(resultObj.Email, Is.EqualTo(defaultList[0].Email));
            Assert.That(resultObj.DateOfBirth, Is.EqualTo(defaultList[0].DateOfBirth));
            Assert.That(resultObj.FavoriteBar, Is.EqualTo(defaultList[0].FavoriteBar));
            Assert.That(resultObj.FavoriteDrink, Is.EqualTo(defaultList[0].FavoriteDrink));
        }

        [Test]
        public void GetCustomer_UnitOfWorkReturnsNull_UutReturnsBadRequest()
        {
            var key = defaultCustomerDto.Username;
            mockUnitOfWork.CustomerRepository.Get(key)
                .ReturnsNull();

            var result = uut.GetCustomer(key);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void AddCustomer_UnitOfWorkAcceptsModel_UutReturnsCreatedWithRouteAndObject()
        {
            var result = uut.AddCustomer(defaultCustomerDto);
            var resultObj = result as CreatedResult;

            Assert.That(result, Is.TypeOf<CreatedResult>());
            Assert.That(resultObj.Location, Is.EqualTo($"api/Customer/{defaultCustomerDto.Username}"));
            Assert.That(resultObj.Value, Is.TypeOf<CustomerDto>());
        }

        [Test]
        public void AddCustomer_UnitOfWorkThrowsException_ReturnsBadRequest()
        {
            mockUnitOfWork.CustomerRepository
                .When(repo => repo.Add(Arg.Any<Customer>()))
                .Do(x => throw new Exception());

            var result = uut.AddCustomer(defaultCustomerDto);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void DeleteCustomer_UnitOfWorkDoesntThrow_Success()
        {
            var result = uut.DeleteCustomer(defaultCustomerDto.Username);
            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public void DeleteCustomer_UnitOfWorkThrowsException_NotDeleted()
        {
            string username = defaultCustomerDto.Username;
            mockUnitOfWork.CustomerRepository
                .When(repo =>
                {
                    repo.Delete(username);
                })
                .Do(x => throw new Exception());

            var result = uut.DeleteCustomer(username);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void EditCustomer_CorrectInput_ReturnsCreated()
        {
            var result = uut.EditCustomer(defaultCustomerDto);
            Assert.That(result, Is.TypeOf<CreatedResult>());
        }

        [Test]
        public void EditCustomer_CorrectInput_ReturnsRouteAndObject()
        {
            var result = uut.EditCustomer(defaultCustomerDto);
            var resultObj = (result as CreatedResult);
            Assert.That(resultObj.Location,
                Is.EqualTo($"api/customers/{defaultCustomerDto.Username}"));
            Assert.That(resultObj.Value, Is.TypeOf<CustomerDto>());
        }

        [Test]
        public void EditCustomer_CorrectInput_ReturnsEquivalentObject()
        {
            var result = uut.EditCustomer(defaultCustomerDto);
            var resultObj = (result as CreatedResult).Value as CustomerDto;

            Assert.That(resultObj.Username, Is.EqualTo(defaultCustomerDto.Username));
            Assert.That(resultObj.Name, Is.EqualTo(defaultCustomerDto.Name));
            Assert.That(resultObj.Email, Is.EqualTo(defaultCustomerDto.Email));
            Assert.That(resultObj.DateOfBirth, Is.EqualTo(defaultCustomerDto.DateOfBirth));
            Assert.That(resultObj.FavoriteBar, Is.EqualTo(defaultCustomerDto.FavoriteBar));
            Assert.That(resultObj.FavoriteDrink, Is.EqualTo(defaultCustomerDto.FavoriteDrink));
        }

        [Test]
        public void EditCoupon_BadInput_ReturnsBadRequest()
        {
            mockUnitOfWork.CustomerRepository
                .When(repo => repo.Edit(Arg.Any<Customer>()))
                .Do(x => throw new Exception());
            var result = uut.EditCustomer(defaultCustomerDto);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

    }
}