using System;
using System.IO;
using Args;
using PKSimpleDataGenerator.Entities;
using PKSimpleDataGenerator.Mappers;

namespace PKSimpleDataGenerator
{
    internal class Program
    {
        private static CommandParams _commandParams = new CommandParams();
        private static DatabaseEntity _databaseEntity;

        private static void Main(string[] args)
        {
            ReadCommandParams(args);

            if (!CheckCommandParams())
            {
                ErrorExit();
                return;
            }

            if (!ReadDatabaseConfig())
            {
                ErrorExit();
                return;
            }
            if (!_databaseEntity.CheckConnection())
            {
                ErrorExit();
                return;
            }

            //var workingDirectory = AppContext.BaseDirectory;


            //var databaseMapper = new DatabaseMapper();
            //var l = databaseMapper.GetListFromFile(workingDirectory);

            //Console.WriteLine(workingDirectory);
            //var maper = new DatabaseMapper();
            //var dd = maper.GetListFromFile(@"d:\temp\db.test.txt");

            //Console.WriteLine(maper.Error);


            //Console.WriteLine(JsonConvert.SerializeObject(dd,Formatting.Indented));

            //var userData = new UserDataFromFiles(@"d:\temp\").GetUserDataDictionaries();

            //Console.WriteLine(userData["aaabbb"].HasData);
            //for(int i=1;i<50;i++) Console.WriteLine(userData["aaabbb"].GetCustomString());

            //Console.WriteLine(userData["aaabbb"].GetCustomString(8));
            Console.ReadKey();
        }

        private static void ErrorExit()
        {
            Console.ReadKey();
        }

        private static bool ReadDatabaseConfig()
        {
            var dbMapper = new DatabaseMapper();
            _databaseEntity = dbMapper.GetListFromFile(_commandParams.DbConfig);

            if (_databaseEntity == null)
            {
                Console.WriteLine($"Cant read db config from file {_commandParams.DbConfig}. {dbMapper.Error}");
                return false;
            }

            return true;
        }


        private static void ReadCommandParams(string[] args)
        {
            _commandParams = Configuration.Configure<CommandParams>().CreateAndBind(args);
        }

        private static bool CheckCommandParams()
        {
            if (!Directory.Exists(_commandParams.DicFolder))
            {
                Console.WriteLine("Dictionary folder not set or incorect! Set to app dir.");
                _commandParams.DicFolder = AppContext.BaseDirectory;
            }
            if (string.IsNullOrEmpty(_commandParams.DbConfig))
            {
                Console.WriteLine("Missing parameter /dbconfig");
                return false;
            }

            if (!File.Exists(_commandParams.DbConfig))
            {
                Console.WriteLine($"Cant access file set in /dbconfig {_commandParams.DbConfig}");
                return false;
            }

            return true;
        }

        private class CommandParams
        {
            public string DicFolder { get; set; }
            public string DbConfig { get; set; }
        }
    }
}