using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PKSimpleDataGenerator
{
    public class UserDataDictionary : ICustomString
    {
        private readonly List<string> _dataList = new List<string>();
        private readonly Random _random = new Random();

        public UserDataDictionary(string fileName)
        {
            if (!File.Exists(fileName)) return;
            _dataList = File.ReadAllLines(fileName,Encoding.Default).ToList().Distinct().ToList();
        }

        public bool HasData => _dataList.Count > 0;

        private int DataCount => _dataList.Count;

        public string GetCustomString()
        {
            return HasData ? GetCustomString(_random.Next(0, DataCount)) : string.Empty;
        }

        public string GetCustomString(int index)
        {

            return _dataList[index];
        }
    }
}