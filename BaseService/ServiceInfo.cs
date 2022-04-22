using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logging;
using CommonObjects;
using System.IO;
using Utilities;

namespace FrameworkBaseService
{
    public class ServiceInfo
    {

        public List<BaseService> ServiceList = new List<BaseService>();
        public static Dictionary<string, CryptoCoinService.CommandHandler> ProcDic = null;

        public static Dictionary<string, MessageObj> CryptoCoinMessages = null;
        public static Dictionary<string, MessageObj> CryptoCoinParams = null;

        public static LogInfo logInfo = null;
        public static Logger logger = null;

        public static Dictionary<string, long> StatisticsDic = new Dictionary<string, long>();
        public static long TotalCount = 0;
        public static TimeSpan AverageTime = new TimeSpan(0, 0, 0, 0, 0);
        public static List<string> ManageStatistics(string action, TimeSpan duration, Response response)
        {
            List<string> result = null;
            lock (typeof(ServiceInfo))
            {
                try
                {
                    if (action == "SET")
                    {
                        try
                        {
                            StatisticsDic[response.MessageCode]++;
                        }
                        catch
                        {
                            StatisticsDic.Add(response.MessageCode, 1);
                        }
                        AverageTime = TimeSpan.FromTicks((TimeSpan.FromTicks(AverageTime.Ticks * TotalCount) + duration).Ticks / (TotalCount + 1));
                        TotalCount++;
                    }
                    else if (action == "GET")
                    {
                        result = new List<string>();
                        result.Add("TotalCount=" + TotalCount.ToString());
                        TotalCount = 0;
                        result.Add("AverageTime=" + AverageTime.ToString());
                        AverageTime = TimeSpan.FromTicks(0);
                        foreach (string key in StatisticsDic.Keys)
                        {
                            result.Add(key + "=" + StatisticsDic[key].ToString());
                        }
                        StatisticsDic.Clear();
                    }
                }
                catch
                { }
            }
            return (result);
        }

        public static Dictionary<string, StrategyInfo> StrategyDic = new Dictionary<string, StrategyInfo>();

        public static void LogStrategyAction(StrategyInfo strategy, string action, string data)
        {
            try
            {
                if (strategy.currentLogDate != Utility.PersianDateNoSlash())
                {
                    try
                    {
                        strategy.writer.Close();
                    }
                    catch 
                    {}
                    strategy.currentLogDate = Utility.PersianDateNoSlash();
                    if (!Directory.Exists(strategy.basePath + "\\StrategyLog\\" + strategy.name))
                    {
                        Directory.CreateDirectory(strategy.basePath + "\\StrategyLog\\" +
                                        strategy.name);
                    }
                    strategy.writer = new StreamWriter(strategy.basePath + "\\StrategyLog\\" +
                                        strategy.name + "\\" + strategy.currentLogDate + ".log", true);
                }
                strategy.writer.WriteLine(Utility.PersianDate() + "|" +
                    Utility.GetTime() + "|" +
                    action + "|" +
                    data);
                strategy.writer.Flush();
            }
            catch (Exception ex)
            {}
        }

    }
}
