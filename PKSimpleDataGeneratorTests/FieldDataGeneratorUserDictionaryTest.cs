using System.Collections.Generic;
using System.IO;
using System.Text;
using PKSimpleDataGenerator;
using PKSimpleDataGenerator.Entities;
using Xunit;

namespace PKSimpleDataGeneratorTests
{
    public class FieldDataGeneratorUserDictionaryTest
    {
        private string CreateTempTxtDict()
        {
            var fileName = Path.GetTempFileName();
            var sb = new StringBuilder();

            for (var i = 1; i <= 1000; i++)
                sb.AppendLine($"Line:{i}");

            File.AppendAllText(fileName, sb.ToString());
            return fileName;

        }

        [Fact]
        public void Test1()
        {
            var userDictionaries = new Dictionary<string, UserDataDictionary>
            {
                { "alpaka", new UserDataDictionary(CreateTempTxtDict()) },
                { "zygfryt", new UserDataDictionary(CreateTempTxtDict()) }
            };

            var field = new FieldEntity()
            {
                Data = "{Alpaka}...{Zygfryt}"
            };
            var fieldGenerator = new FieldDataGeneratorUserDictionary(userDictionaries, field);
            
            Assert.NotEmpty(fieldGenerator.GenerateData());
        }
    }
}