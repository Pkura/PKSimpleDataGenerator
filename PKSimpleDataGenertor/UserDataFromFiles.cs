using System.Collections.Generic;
using System.IO;

namespace PKSimpleDataGenerator
{
    public class UserDataFromFiles
    {
        private const string DictionaryExtension = "pksdg";
        private readonly string _path;

        public UserDataFromFiles(string path)
        {
            _path = path;
        }

        public Dictionary<string, UserDataDictionary> GetUserDataDictionaries()
        {
            var toReturn = new Dictionary<string, UserDataDictionary>();

            if (!Directory.Exists(_path)) return toReturn;

            var files = Directory.GetFiles(_path, $"*.{DictionaryExtension}");

            foreach (var filePath in files)
            {
                var dicName = Path.GetFileNameWithoutExtension(filePath).ToLowerInvariant();
                toReturn.Add(dicName, new UserDataDictionary(filePath));
            }

            return toReturn;
        }
    }
}