using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using PKSimpleDataGenerator.Entities;

namespace PKSimpleDataGenerator
{
    public class FieldDataGeneratorUserDictionary : IFieldDataGenerator
    {
        private Dictionary<string, UserDataDictionary> _userDictionaries;

        private bool _isRegex;
        private List<string> _userDictionaryNames;



        public FieldDataGeneratorUserDictionary(Dictionary<string, UserDataDictionary> userDictionaries,
            FieldEntity field)
        {
            _userDictionaries = userDictionaries ?? throw new ArgumentNullException(nameof(userDictionaries));

            if (userDictionaries.Count == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(userDictionaries));

            Field = field ?? throw new ArgumentNullException(nameof(field));

            if (string.IsNullOrEmpty(field.Data))
                throw new Exception("Field has no user dictionary set, or expression 'Data' is empty");

            CheckIfRegex();
            
            _userDictionaryNames = GetOrderedUserDictionaryNames();

            CheckIfUserDictionaryExists();
        }

        private  List<string> GetOrderedUserDictionaryNames()
        {
            var toReturn = new List<string>();

            if (_isRegex)
            {
                Regex regex = new Regex(@"\{(.+?)\}");
                var matches = regex.Matches(Field.Data);
                
                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                    {
                        toReturn.Add(match.Value.Substring(1,match.Length-2));
                    }
                }
                
            } else {
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
                {
                    throw new Exception($"Cant find user dictionary named {dictionaryName} for field {Field.Name}");
                }
            }
        }

        public FieldEntity Field { get; set; }


        public string GenerateData()
        {
            string gggg;
            return "asdasd";
        }

        private void CheckIfRegex()
        {
            _isRegex = Field.Data.IndexOf("{", StringComparison.Ordinal) > -1;
        }
    }
}