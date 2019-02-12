using System;
using PKSimpleDataGenerator.Enums;
using PKSimpleDataGenerator.Utils;

namespace PKSimpleDataGenerator.Utlis
{
    public static class EmailAddressGenerator
    {
        private static readonly Random _random = new Random();
        private static readonly char[] _randomSpecial = @"!#_-".ToCharArray();

        public static string GenerateEmail()
        {
            var gender = (GenderEnum) (_random.Next(1000) % 2 == 0 ? 0 : 1);
            var name = NameGenerator.GenerateFirstName(gender);
            var surname = NameGenerator.GenerateLastName();
            var special1 = _randomSpecial[_random.Next(_randomSpecial.Length)].ToString();
            var special2 = _randomSpecial[_random.Next(_randomSpecial.Length)].ToString();
            var domain = DomainGenerator.GenerateDomain("@mail.");
            var dateYear = _random.Next(1950, 2018).ToString();
            var randPath = _random.Next(4);

            switch (randPath)
            {
                case 0:
                    return $"{name}{special1}{surname}{domain}".ToLower();
                case 1:
                    return $"{surname}{special1}{name}{special2}{dateYear}{domain}".ToLower();
                case 2:
                    return $"{name}{special1}{surname}{dateYear}{domain}".ToLower();
                case 3:
                    return $"{surname}{special2}{special1}{dateYear}{domain}".ToLower();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}