using System.Collections.Generic;
using System.IO;
using System.Text;
using PKSimpleDataGenerator;
using PKSimpleDataGenerator.Entities;
using PKSimpleDataGenerator.FieldDataGenerators;
using Xunit;

namespace PKSimpleDataGeneratorTests
{
    public class FieldDataGeneratorUserDictionaryTest
    {
        private string CreateTempTxtDict(bool incrementValues = true)
        {
            var fileName = Path.GetTempFileName();
            var sb = new StringBuilder();

            for (var i = 1; i <= 1000; i++)
                if (incrementValues)
                {
                    sb.AppendLine($"Line:{i}");
                }
                else
                {
                    sb.AppendLine($"Line:0");
                }


            File.AppendAllText(fileName, sb.ToString());
            return fileName;
        }

        [Fact]
        public void TestWithTwoValues()
        {
            var userDictionaries = new Dictionary<string, UserDataDictionary>
            {
                {"alpaka", new UserDataDictionary(CreateTempTxtDict(false))},
                {"zygfryt", new UserDataDictionary(CreateTempTxtDict(false))}
            };

            var field = new FieldEntity()
            {
                Data = "{Alpaka}...{Zygfryt}"
            };
            var fieldGenerator = new UserDictionaryGenerator(userDictionaries, field);
            var value = fieldGenerator.GenerateData();
            Assert.NotEmpty(value);
            Assert.Equal("Line:0...Line:0", value);
        }

        [Fact]
        public void TestOneValue()
        {
            var userDictionaries = new Dictionary<string, UserDataDictionary>
            {
                {"alpaka", new UserDataDictionary(CreateTempTxtDict())},
                {"zygfryt", new UserDataDictionary(CreateTempTxtDict())}
            };

            var field = new FieldEntity()
            {
                Data = "Alpaka"
            };
            var fieldGenerator = new UserDictionaryGenerator(userDictionaries, field);
            var value = fieldGenerator.GenerateData();
            Assert.NotEmpty(value);
            Assert.Contains("Line:", value);
        }
    }
}