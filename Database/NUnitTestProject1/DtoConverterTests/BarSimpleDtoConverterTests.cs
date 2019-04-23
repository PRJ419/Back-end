using System.Collections.Generic;
using Database;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WebApi.DTOs.Bars;

namespace WebApi.Test.UnitTest.DtoConverterTests
{
    [TestFixture]
    public class BarSimpleDtoConverterTests
    {
        private List<Bar> defaultList;

        [SetUp]
        public void Setup()
        {
            defaultList = new List<Bar>()
                {new Bar()
                {
                    BarName = "TestBar",
                    AvgRating = 4.0,
                },
                    new Bar()
                    {
                        BarName = "TestBar2",
                    }
                };
        }
        [Test]
        public void ToDtoList_Input2Bars_OutputDtoListEquivalent()
        {
            var resultList = BarSimpleDtoConverter.ToDtoList(defaultList);
            Assert.That(resultList.Count, Is.EqualTo(2));
            Assert.That(resultList[0].BarName, Is.EqualTo(defaultList[0].BarName));
            Assert.That(resultList[1].BarName, Is.EqualTo(defaultList[1].BarName));
        }

        [Test]
        public void ToDtoList_Input0Bars_OutputEmptyList()
        {
            var resultList = BarSimpleDtoConverter.ToDtoList(new List<Bar>());
            Assert.That(resultList.Count, Is.EqualTo(0));
        }

    }
}