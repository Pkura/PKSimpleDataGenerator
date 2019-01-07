using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using PKSimpleDataGenerator.Entities;

namespace PKSimpleDataGenerator
{
    public class DatabaseMapper
    {
        public List<DatabaseEntity> GetListFromFile(string fileName)
        {
            var fileContent = GetFileContent(fileName);
            
            if (string.IsNullOrEmpty(fileContent)) return new List<DatabaseEntity>();

            return JsonConvert.DeserializeObject<List<DatabaseEntity>>(fileContent);
        }


        private string GetFileContent(string fileName)
        {
            var toReturn = string.Empty;
            try
            {
                toReturn = File.ReadAllText(fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return toReturn;
        }
    }
}