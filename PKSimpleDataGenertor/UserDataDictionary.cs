using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PKSimpleDataGenerator
{
    public class UserDataDictionary : IRandomString
    {
        private readonly List<string> _dataList = new List<string>();
        private readonly Random _random = new Random();

        public UserDataDictionary(string fileName)
        {
            if (!File.Exists(fileName)) return;
            _dataList = File.ReadAllLines(fileName,Encoding.Default).ToList().Distinct().ToList();
        }

        public bool HasData => _dataList.Count > 0;

        public int DataCount => _dataList.Count;

        public string GetRandomString()
        {
            return HasData ? GetRandomString(_random.Next(0, DataCount)) : string.Empty;
        }

        public string GetRandomString(int index)
        {
            if (index > DataCount - 1)
            {
                index = index % (DataCount - 1);
            }

            return _dataList[index];
        }
    }
}