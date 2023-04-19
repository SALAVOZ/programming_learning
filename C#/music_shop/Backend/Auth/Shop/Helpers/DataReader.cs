using System.Collections.Generic;
using System.IO;

namespace Shop.Helpers
{
    public static class DataReader
    {
        public static List<string> ReadData(string path)
        {
            List<string> dataList = new List<string>();
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    dataList.Add(line);
                }
            }
            return dataList;
        }
    }
}
