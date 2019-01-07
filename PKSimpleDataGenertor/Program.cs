using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace PKSimpleDataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new SqlConnectionStringBuilder();
            c.inte

            var userData = new UserDataFromFiles(@"d:\temp\").GetUserDataDictionaries();

            Console.WriteLine(userData["aaabbb"].HasData);
            for(int i=1;i<50;i++) Console.WriteLine(userData["aaabbb"].GetCustomString());

            Console.WriteLine(userData["aaabbb"].GetCustomString(8));
            Console.ReadKey();
        }
    }
}
