using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using PKSimpleDataGenerator.Entities;

namespace PKSimpleDataGenerator.Mappers
{
    public class DatabaseMapper
    {
        public string Error { get; private set; }

        public DatabaseEntity GetListFromFile(string fileName)
        {
            var fileContent = GetFileContent(fileName);

            if (string.IsNullOrEmpty(fileContent)) return null;

            try
            {
                return JsonConvert.DeserializeObject<DatabaseEntity>(fileContent);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }


        private string GetFileContent(string fileName)
        {
            var toReturn = string.Empty;
            try
            {
                toReturn = File.ReadAllText(fileName, Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return toReturn;
        }
    }
}