using System;
using System.Linq;
using PKSimpleDataGenerator;

namespace PKSimpleDataGenertor
{
    class Program
    {
        static void Main(string[] args)
        {

            var userData = new UserDataFromFiles(@"d:\temp\").GetUserDataDictionaries();

            Console.WriteLine(userData["aaabbb"].HasData);
            for(int i=40;i<50;i++) Console.WriteLine(userData["aaabbb"].GetCustomString(i));

            Console.WriteLine(userData["aaabbb"].GetCustomString(8));
            Console.ReadKey();
        }
    }
}
