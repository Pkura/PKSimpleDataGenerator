using System.Diagnostics;
using PKSimpleDataGenerator.Utlis;
using Xunit;

namespace PKSimpleDataGeneratorTests
{
    public class UtlisTest
    {
        [Fact]
        public void GenerateDomainTest()
        {
            for (var i = 0; i < 10000; i++)
            {
                var toTest = DomainGenerator.GenerateDomain("@");
                Assert.Contains("@", toTest);
            }
        }

        [Fact]
        public void GeneratePageAdressTest()
        {
            for (var i = 0; i < 10000; i++)
            {
                var toTest = DomainGenerator.GeneratePageAdress();
                Assert.Contains("www.ra", toTest);
            }
        }

        [Fact]
        public void EmailAddressGeneratorTest()
        {
            for (var i = 0; i < 10000; i++)
            {
                var toTest = EmailAddressGenerator.GenerateEmail();
                Debug.WriteLine(toTest);
                Assert.Contains("@", toTest);
            }
        }
    }
}