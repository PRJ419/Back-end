using AutoMapper;
using Database;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WebApi.DTOs.AutoMapping;
using WebApi.DTOs.BarEvent;
using WebApi.DTOs.BarRepresentative;
using WebApi.DTOs.Bars;
using WebApi.DTOs.Coupon;
using WebApi.DTOs.Customers;
using WebApi.DTOs.Drinks;
using WebApi.DTOs.ReviewDto;

namespace WebApi.Test.UnitTest.DtoConverterTests
{
    [TestFixture]
    public class AutoMapperTests
    {
        private IMapper uut;

        [SetUp]
        public void Setup()
        {
            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            uut = new Mapper(configuration);
        }

        [Test]
        public void Map_MapFromReviewToReviewDto_CorrectTypeReturned()
        {
            // Arrange 
            var review = new Review();

            // Act
            var dtoResult = uut.Map<ReviewDto>(review);

            // Assert
            Assert.That(dtoResult, Is.TypeOf<ReviewDto>());
        }

        [Test]
        public void Map_MapFromReviewToReviewDto_CorrectProperties()
        {
            // Arrange 
            var review = new Review()
            {
                Bar = null,
                BarName = "TestBar",
                BarPressure = 5,
                Customer = null,
                Username = "username",
            };

            // Act
            var dtoResult = uut.Map<ReviewDto>(review);

            // Assert
            Assert.That(dtoResult.BarName,      Is.EqualTo(review.BarName));
            Assert.That(dtoResult.BarPressure,  Is.EqualTo(review.BarPressure));
            Assert.That(dtoResult.Username,     Is.EqualTo(review.Username));

        }

        [Test]
        public void Map_MapFromReviewDtoToReview_CorrectType()
        {
            var reviewDto = new ReviewDto();
            var review = uut.Map<Review>(reviewDto);
            Assert.That(review, Is.TypeOf<Review>());
        }

        [Test]
        public void Map_MapFromDrinkToDrinkDto_CorrectType()
        {
            var drink = new Drink();
            var drinkDto = uut.Map<DrinkDto>(drink);
            Assert.That(drinkDto, Is.TypeOf<DrinkDto>());
        }

        [Test]
        public void Map_MapFromDrinkDtoToDrink_CorrectType()
        {
            var drinkDto = new DrinkDto();
            var drink = uut.Map<Drink>(drinkDto);
            Assert.That(drink, Is.TypeOf<Drink>());
        }

        [Test]
        public void Map_MapFromCustomerToCustomerDto_CorrectType()
        {
            var custDto = new CustomerDto();
            var cust = uut.Map<Customer>(custDto);
            Assert.That(cust, Is.TypeOf<Customer>());
        }

        [Test]
        public void Map_MapFromCustomerDtoToCustomer_CorrectType()
        {
            var cust = new Customer();
            var custDto = uut.Map<CustomerDto>(cust);
            Assert.That(custDto, Is.TypeOf<CustomerDto>());
        }

        [Test]
        public void Map_MapFromCouponDtoToCoupon_CorrectType()
        {
            var couponDto = new CouponDto();
            var coupon = uut.Map<Coupon>(couponDto);
            Assert.That(coupon, Is.TypeOf<Coupon>());
        }

        [Test]
        public void Map_MapFromCouponToCouponDto_CorrectType()
        {
            var coupon = new Coupon();
            var couponDto = uut.Map<CouponDto>(coupon);
            Assert.That(couponDto, Is.TypeOf<CouponDto>());
        }

        [Test]
        public void Map_MapFromBarToBarDto_CorrectType()
        {
            var bar = new Bar();
            var barDto = uut.Map<BarDto>(bar);
            Assert.That(barDto, Is.TypeOf<BarDto>());
        }

        [Test]
        public void Map_MapFromBarDtoToBar_CorrectType()
        {
            var barDto = new BarDto();
            var bar = uut.Map<Bar>(barDto);
            Assert.That(bar, Is.TypeOf<Bar>());
        }

        [Test]
        public void Map_MapFromBarDtoToBarSimpleDto_CorrectType()
        {
            var bar = new Bar();
            var barSimpleDto = uut.Map<BarSimpleDto>(bar);
            Assert.That(barSimpleDto, Is.TypeOf<BarSimpleDto>());
        }

        [Test]
        public void Map_MapFromBarRepToBarRepDto_CorrectType()
        {
            var barRep = new BarRepresentative();
            var barRepDto = uut.Map<BarRepresentativeDto>(barRep);
            Assert.That(barRepDto, Is.TypeOf<BarRepresentativeDto>());
        }

        [Test]
        public void Map_MapFromBarRepDtoToBarRep_CorrectType()
        {
            var barRepDto = new BarRepresentativeDto();
            var barRep = uut.Map<BarRepresentative>(barRepDto);
            Assert.That(barRep, Is.TypeOf<BarRepresentative>());
        }

        [Test]
        public void Map_MapFromBarEventToBarEventDto_CorrectType()
        {
            var barEvent = new BarEvent();
            var barEventDto = uut.Map<BarEventDto>(barEvent);
            Assert.That(barEventDto, Is.TypeOf<BarEventDto>());
        }

        [Test]
        public void Map_MapFromBarEventDtoToBarEvent_CorrectType()
        {
            var barEventDto = new BarEventDto();
            var barEvent = uut.Map<BarEvent>(barEventDto);
            Assert.That(barEvent, Is.TypeOf<BarEvent>());
        }



    }
}