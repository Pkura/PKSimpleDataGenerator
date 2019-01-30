using System;
using System.Collections.Generic;
using System.IO;
using Args;
using Newtonsoft.Json;
using PKSimpleDataGenerator.Entities;
using PKSimpleDataGenerator.Mappers;

namespace PKSimpleDataGenerator
{
    internal class Program
    {
        private static CommandParams _commandParams = new CommandParams();
        private static DatabaseEntity _databaseEntity;
        private static Dictionary<string, UserDataDictionary> _userDataDictionaries;

        private static void Main(string[] args)
        {
            ReadCommandParams(args);

            if (!CheckCommandParams())
            {
                ErrorExit(null);
                return;
            }

            if (!ReadDatabaseConfig())
            {
                ErrorExit(null);
                return;
            }
            ConsoleDatabaseInfo();

            if (!_databaseEntity.CheckConnection())
            {
                ErrorExit(_databaseEntity.Errors);
                return;
            }

            var userDataMapper = new UserDataFromFiles(_commandParams.DicFolder);
            _userDataDictionaries = userDataMapper.GetUserDataDictionaries();
            ConsoleUserDictionaryInfo();
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

        private static void ConsoleUserDictionaryInfo()
        {
            Console.WriteLine($"User dictionaries:{_userDataDictionaries.Count}");
            Console.WriteLine("====================");
            var count = 1;
            foreach (var userDataDictionary in _userDataDictionaries)
            {
                Console.WriteLine($"{count}:{userDataDictionary.Key} : {userDataDictionary.Value.DataCount}" );
                count++;
            }
            Console.WriteLine();
        }

        private static void ConsoleDatabaseInfo()
        {
            if (_databaseEntity == null) return;

            Console.WriteLine("Database Info:");
            Console.WriteLine("==============");

            Console.WriteLine($"Database: {_databaseEntity.InitialCatalog}");
            Console.WriteLine($"Host: {_databaseEntity.DataSource}");

            Console.WriteLine($"Tables:{_databaseEntity.Tables.Count}");
            Console.WriteLine("=========");
            Console.WriteLine($"{JsonConvert.SerializeObject(_databaseEntity.Tables, Formatting.Indented)}");
            Console.WriteLine();
        }

        private static void ErrorExit(string msg)
        {

            if (!string.IsNullOrEmpty(msg)) Console.WriteLine(msg);
            Console.WriteLine("----- any key to die -----");
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