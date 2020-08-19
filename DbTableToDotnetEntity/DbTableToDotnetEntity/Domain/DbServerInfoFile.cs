using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using DbTableToDotnetEntity.Models;

namespace DbTableToDotnetEntity.Domain
{
    public class DbServerInfoFile
    {
        public static List<string> ReadHistoryData()
        {
            var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var historyConnStrList = new List<string>();
            if (!File.Exists(path + "ServerCache.txt")) return historyConnStrList;
            using var reader = new StreamReader(path + "ServerCache.txt");
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                historyConnStrList.Add(line);
            }
            return historyConnStrList;
        }

        public static void WriteHistoryData(string connStr)
        {
            var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            if (!File.Exists(path + "ServerCache.txt"))
            {
                using var fs = new FileStream(path + "ServerCache.txt", FileMode.Create, FileAccess.Write);
                using var sw = new StreamWriter(fs);
                sw.WriteLine(connStr);
            }
            else
            {
                var data = new List<string>();
                using (var reader = new StreamReader(path + "ServerCache.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(connStr))
                        {
                            return;
                        }
                        data.Add(line);
                    }
                    data.Add(connStr);
                }
                using var writer = new StreamWriter(path + "ServerCache.txt");
                foreach (var item in data)
                {
                    writer.WriteLine(item);
                }
            }
        }

        public static void WriteHistoryDataDirect(string connStr)
        {
            var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            if (!File.Exists(path + "ServerCache.txt"))
            {
                using var fs = new FileStream(path + "ServerCache.txt", FileMode.Create, FileAccess.Write);
                using var sw = new StreamWriter(fs);
                sw.WriteLine(connStr);
            }
            else
            {
                var data = new List<string>();
                using (var reader = new StreamReader(path + "ServerCache.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        data.Add(line);
                    }
                    data.Add(connStr);
                }
                using var writer = new StreamWriter(path + "ServerCache.txt");
                foreach (var item in data)
                {
                    writer.WriteLine(item);
                }
            }
        }

        public static List<DbServerInfo> ConvertHistoryToModel()
        {
            var historyConnStrList = ReadHistoryData();
            var connStrModels = new List<DbServerInfo>();
            foreach (var item in historyConnStrList)
            {
                var arr = item.Split(';');
                var model = new DbServerInfo();
                for (var i = 0; i < arr.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            {
                                model.Database = arr[i].Split('=')[1];
                                break;
                            }
                        case 1:
                            {
                                model.Ip = arr[i].Split('=')[1];
                                break;
                            }
                        case 2:
                            {
                                model.UserId = arr[i].Split('=')[1];
                                break;
                            }
                        case 3:
                            {
                                model.Password = arr[i].Split('=')[1];
                                break;
                            }
                        case 5:
                            {
                                model.Encoding = arr[i].Split('=')[1];
                                break;
                            }
                        case 6:
                            {
                                model.TcpIpPort = arr[i].Split('=')[1];
                                break;
                            }
                        case 7:
                            {
                                model.DatabaseType = arr[i].Split('=')[1];
                                break;
                            }
                    }
                }
                connStrModels.Add(model);
            }
            return connStrModels;
        }

        public static ObservableCollection<DbServerHistoryItem> GetDbServerHistoryItems()
        {
            var dbServerInfos = ConvertHistoryToModel();
            var items = new ObservableCollection<DbServerHistoryItem>();
            foreach (var item in dbServerInfos)
            {
                items.Add(new DbServerHistoryItem(item.Ip, item.DatabaseType));
            }
            return items;
        }

        public static ObservableCollection<DbServerInfo> GetDbServerInfoHistoryItems()
        {
            var dbServerInfos = ConvertHistoryToModel();
            return new ObservableCollection<DbServerInfo>(dbServerInfos);
        }
    }
}
