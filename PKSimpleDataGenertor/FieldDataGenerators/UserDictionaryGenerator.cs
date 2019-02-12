using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using PKSimpleDataGenerator.Entities;

namespace PKSimpleDataGenerator.FieldDataGenerators
{
    public class UserDictionaryGenerator : IFieldDataGenerator
    {
        private readonly Dictionary<string, UserDataDictionary> _userDictionaries;
        private readonly List<string> _userDictionaryNames;

        private bool _isRegex;


        public UserDictionaryGenerator(Dictionary<string, UserDataDictionary> userDictionaries,
            FieldEntity field)
        {
            _userDictionaries = userDictionaries ?? throw new ArgumentNullException(nameof(userDictionaries));

            if (userDictionaries.Count == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(userDictionaries));

            Field = field ?? throw new ArgumentNullException(nameof(field));

            if (string.IsNullOrEmpty(field.Data))
                throw new Exception("Field has no user dictionary set, or expression in 'Data' is empty");

            CheckIfRegex();

            _userDictionaryNames = GetOrderedUserDictionaryNames();

            CheckIfUserDictionaryExists();
        }

        public FieldEntity Field { get; set; }


        public string GenerateData()
        {
            var toReturn = Field.Data;

            foreach (var dictionaryName in _userDictionaryNames)
            {
                var replaceName = _isRegex ? $"{{{dictionaryName}}}" : Field.Data;

                IRandomString userDictionary = GetUserDictionaryByName(dictionaryName);
                var replaceValue = userDictionary.GetRandomString();

                toReturn = toReturn.Replace(replaceName, replaceValue);
            }

            return toReturn;
        }


        private UserDataDictionary GetUserDictionaryByName(string name)
        {
            var toSearach = name.ToLowerInvariant();

            _userDictionaries.TryGetValue(toSearach, out var toRetun);

            return toRetun;
        }

        private List<string> GetOrderedUserDictionaryNames()
        {
            var toReturn = new List<string>();

            if (_isRegex)
            {
                var regex = new Regex(@"\{(.+?)\}");
                var matches = regex.Matches(Field.Data);

                if (matches.Count > 0)
                    foreach (Match match in matches)
                        toReturn.Add(match.Value.Substring(1, match.Length - 2));
                else
                    throw new Exception($"Field {Field.Name}: can't parse Field.Data: {Field.Data}");
            }
            else
            {
                toReturn.Add(Field.Data);
            }

            return toReturn;
        }

        private void CheckIfUserDictionaryExists()
        {
            foreach (var dictionaryName in _userDictionaryNames)
            {
                var toCheck = dictionaryName.ToLowerInvariant();
                if (!_userDictionaries.ContainsKey(toCheck))
                    throw new Exception($"Cant find user dictionary named {dictionaryName} field {Field.Name}");
            }
        }

        private void CheckIfRegex()
        {
            _isRegex = Field.Data.IndexOf("{", StringComparison.Ordinal) > -1;
        }
    }
}