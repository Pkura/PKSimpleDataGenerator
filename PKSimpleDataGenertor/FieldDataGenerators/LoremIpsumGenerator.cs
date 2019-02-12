using System;
using System.Linq;
using PKSimpleDataGenerator.Entities;

namespace PKSimpleDataGenerator.FieldDataGenerators
{
    public class LoremIpsumGenerator : IFieldDataGenerator
    {
        private const string LoremIpsum =
                @"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            ;

        public LoremIpsumGenerator(FieldEntity field)
        {
            Field = field ?? throw new ArgumentNullException(nameof(field));
        }

        public FieldEntity Field { get; set; }

        public string GenerateData()
        {
            var count = 1;

            if (Field.Number > 0) count = Field.Number;

            return string.Join(Environment.NewLine, Enumerable.Repeat(LoremIpsum, count));
        }
    }
}