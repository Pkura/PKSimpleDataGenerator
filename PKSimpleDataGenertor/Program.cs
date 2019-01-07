using System;
using System.Data.Common;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace PKSimpleDataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
          
            var maper = new DatabaseMapper();
            var dd = maper.GetListFromFile(@"d:\temp\db.test.txt");

            Console.WriteLine(maper.Error);


            Console.WriteLine(JsonConvert.SerializeObject(dd,Formatting.Indented));

            //var userData = new UserDataFromFiles(@"d:\temp\").GetUserDataDictionaries();

            //Console.WriteLine(userData["aaabbb"].HasData);
            //for(int i=1;i<50;i++) Console.WriteLine(userData["aaabbb"].GetCustomString());

            //Console.WriteLine(userData["aaabbb"].GetCustomString(8));
            Console.ReadKey();
        }
    }
}
