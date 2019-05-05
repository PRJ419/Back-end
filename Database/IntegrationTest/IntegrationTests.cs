using NUnit.Framework;

namespace IntegrationTest
{
    public class IntegrationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        // As it was not possible to throw SQL exceptions in the Unit Test of WebApi, 
        // we will test that the WebApi reacts correctly to these. 

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}