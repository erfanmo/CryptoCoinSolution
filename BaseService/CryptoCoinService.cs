using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonObjects;
using Utilities;
using Logging;
using System.IO;
using System.Data;
using CryptoCoinDataAccess;
using CryptoCoinPropertyLib;
using Newtonsoft.Json;

namespace FrameworkBaseService
{
    public class CryptoCoinService : BaseService, BaseServiceInterface
    {

        public CryptoCoinService(string serviceName, string basePath, int id, List<string> parameters)
            : base(serviceName, basePath, id, parameters)
        {

        }

        #region BaseServiceInterface Members

        public delegate Response CommandHandler(Request request);

        public Response DoJob(Request request)
        {
            Response response = null;
            CommandHandler handler = null;

            DateTime startAction = DateTime.Now;
            try
            {                
                try
                {
                    handler = ServiceInfo.ProcDic[request.Command];
                }
                catch
                {
                    handler = null;
                }
                if (handler != null)
                {
                    response = handler(request);
                }
                else
                {
                    response = CreateResponse(request, "CryptoCoin300001");
                }
                if (response.MessageCode != "000000")
                {
                    throw new Exception(response.MessageCode + ": " + response.Message);
                }
                else
                {
                    TotalSuccessfull++;
                    //****************** Do Log Action
                    LogItem item = new LogItem();
                    item.Action = "DoJob";
                    item.Date = Utility.PersianDate();
                    item.Description = request.Command + ": Do Job Successfully";
                    item.Duration = "";
                    item.LogLevel = LogInfo.LogLevel.LOW_DETAIL;
                    item.ModuleName = "CryptoCoinService";
                    item.OutputMessageType = LogInfo.OutputMessageType.NOTIFICATION;
                    item.OutputType = LogInfo.OutputType.FILE;
                    item.SubModule = "CryptoCoinService";
                    item.Time = Utility.GetTime();
                    item.UniqueId = request.RefrenceNo;
                    LogWorker worker = ServiceInfo.logger.GetLogWorker(ServiceInfo.logInfo);
                    worker.WriteLog(item, ServiceInfo.logInfo);
                    //********************************
                }
            }
            catch (Exception ex)
            {
                TotalError++;
                LastErrorMsg = ex.Message;
                //****************** Do Log Action
                LogItem item = new LogItem();
                item.Action = "DoJob";
                item.Date = Utility.PersianDate();
                item.Description = "Exception: " + request.Command + ", " + ex.Message;
                item.Duration = "";
                item.LogLevel = LogInfo.LogLevel.LOW_DETAIL;
                item.ModuleName = "CryptoCoinService";
                item.OutputMessageType = LogInfo.OutputMessageType.ERROR;
                item.OutputType = LogInfo.OutputType.FILE;
                item.SubModule = "CryptoCoinService";
                item.Time = Utility.GetTime();
                item.UniqueId = request.RefrenceNo;
                LogWorker worker = ServiceInfo.logger.GetLogWorker(ServiceInfo.logInfo);
                worker.WriteLog(item, ServiceInfo.logInfo);
                //********************************
            }
            DateTime endAction = DateTime.Now;
            try
            {
                if (bool.Parse(ServiceInfo.CryptoCoinParams["EnableStatistics"].Value))
                    ServiceInfo.ManageStatistics("SET", endAction - startAction, response);
            }
            catch
            {}
            return (response);
        }

        public bool InitializeService()
        {
            bool result = true;
            try
            {
                //-------------------- Load Parameters
                List<MessageObj> listParams = (List<MessageObj>)Utility.DeserializeXML_File(typeof(List<MessageObj>),
                    BasePath + "\\CryptoCoinParams.xml");
                ServiceInfo.CryptoCoinParams = new Dictionary<string, MessageObj>();
                foreach (MessageObj p in listParams)
                {
                    ServiceInfo.CryptoCoinParams.Add(p.Key, p);
                }
                //-------------------- Ramona Logging Settings
                ServiceInfo.logger = new Logger();
                ServiceInfo.logInfo = new LogInfo();
                if (ServiceInfo.CryptoCoinParams["LogType"].Value == "DAILY_YEAR_MONTH")
                    ServiceInfo.logInfo.loggingType = LogInfo.LogType.DAILY_YEAR_MONTH;
                else if (ServiceInfo.CryptoCoinParams["LogType"].Value == "MONTHLY_SIMPLE")
                    ServiceInfo.logInfo.loggingType = LogInfo.LogType.MONTHLY_SIMPLE;
                else if (ServiceInfo.CryptoCoinParams["LogType"].Value == "MONTHLY_YEAR")
                    ServiceInfo.logInfo.loggingType = LogInfo.LogType.MONTHLY_YEAR;
                if (ServiceInfo.CryptoCoinParams["LogLevel"].Value == "DETAIL")
                    ServiceInfo.logInfo.loggingLevel = LogInfo.LogLevel.DETAIL;
                else if (ServiceInfo.CryptoCoinParams["LogLevel"].Value == "HIGH_DETAIL")
                    ServiceInfo.logInfo.loggingLevel = LogInfo.LogLevel.HIGH_DETAIL;
                else if (ServiceInfo.CryptoCoinParams["LogLevel"].Value == "LOW_DETAIL")
                    ServiceInfo.logInfo.loggingLevel = LogInfo.LogLevel.LOW_DETAIL;
                ServiceInfo.logger.AddLogWorker(BasePath + "\\" + "Log", ServiceInfo.logInfo);
                //-------------------- Make Delegate Dictionary
                ServiceInfo.ProcDic = new Dictionary<string, CommandHandler>();
                CommandHandler handler = new CommandHandler(CryptoCoinServiceTest);
                ServiceInfo.ProcDic.Add("CryptoCoinServiceTest", handler);
                handler = new CommandHandler(CryptoCoinServiceRegisterStrategy);
                ServiceInfo.ProcDic.Add("CryptoCoinServiceRegisterStrategy", handler);
                handler = new CommandHandler(CryptoCoinServiceGetStrategyParams);
                ServiceInfo.ProcDic.Add("CryptoCoinServiceGetStrategyParams", handler);
                handler = new CommandHandler(CryptoCoinServiceStartStrategy);
                ServiceInfo.ProcDic.Add("CryptoCoinServiceStartStrategy", handler);
                handler = new CommandHandler(CryptoCoinServiceGetMonitoring);
                ServiceInfo.ProcDic.Add("CryptoCoinServiceGetMonitoring", handler);
                handler = new CommandHandler(CryptoCoinServiceGetStrategyList);
                ServiceInfo.ProcDic.Add("CryptoCoinServiceGetStrategyList", handler);
                handler = new CommandHandler(CryptoCoinServiceTerminateStrategy);
                ServiceInfo.ProcDic.Add("CryptoCoinServiceTerminateStrategy", handler);
                handler = new CommandHandler(CryptoCoinServiceGetActiveStrategies);
                ServiceInfo.ProcDic.Add("CryptoCoinServiceGetActiveStrategies", handler);

                //-------------------- Other Initialization
                List<MessageObj> listMsg = (List<MessageObj>)Utility.DeserializeXML_File(typeof(List<MessageObj>),
                    BasePath + "\\CryptoCoinMessages.xml");
                ServiceInfo.CryptoCoinMessages = new Dictionary<string, MessageObj>();
                foreach (MessageObj msg in listMsg)
                {
                    ServiceInfo.CryptoCoinMessages.Add(msg.Key, msg);
                }
                //****************** Do Log Action
                LogItem item = new LogItem();
                item.Action = "InitializeService";
                item.Date = Utility.PersianDate();
                item.Description = "Initialize Service Successfully";
                item.Duration = "";
                item.LogLevel = LogInfo.LogLevel.LOW_DETAIL;
                item.ModuleName = "CryptoCoinService";
                item.OutputMessageType = LogInfo.OutputMessageType.NOTIFICATION;
                item.OutputType = LogInfo.OutputType.FILE;
                item.SubModule = "CryptoCoinService";
                item.Time = Utility.GetTime();
                item.UniqueId = "CryptoCoinService";
                LogWorker worker = ServiceInfo.logger.GetLogWorker(ServiceInfo.logInfo);
                worker.WriteLog(item, ServiceInfo.logInfo);
                //********************************
            }
            catch (Exception ex)
            {
                result = false;
                //****************** Do Log Action
                LogItem item = new LogItem();
                item.Action = "InitializeService";
                item.Date = Utility.PersianDate();
                item.Description = "Exception: " + ex.Message;
                item.Duration = "";
                item.LogLevel = LogInfo.LogLevel.LOW_DETAIL;
                item.ModuleName = "CryptoCoinService";
                item.OutputMessageType = LogInfo.OutputMessageType.ERROR;
                item.OutputType = LogInfo.OutputType.FILE;
                item.SubModule = "CryptoCoinService";
                item.Time = Utility.GetTime();
                item.UniqueId = "CryptoCoinService";
                LogWorker worker = ServiceInfo.logger.GetLogWorker(ServiceInfo.logInfo);
                worker.WriteLog(item, ServiceInfo.logInfo);
                //********************************
            }
            return (result);
        }

        #endregion

        #region General

        private Response CryptoCoinServiceTest(Request request)
        {
            //System.Threading.Thread.Sleep(int.Parse(request.Parameters));
            Response response = new Response();
            response.Message = "Ok";
            response.MessageCode = "000000";
            response.Result = "CryptoCoinService - Test: " + request.Parameters;
            response.BinaryData = null;
            response.Command = "Complete";
            response.CommandType = request.CommandType;
            response.Schema = "";
            response.RefrenceNo = request.RefrenceNo;
            return (response);
        }

        private Dictionary<string, string> GetParamDic(List<string> arrayParams)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (string par in arrayParams)
            {
                if (par.Trim() == "")
                    continue;
                char[] chList = new char[1];
                chList[0] = '=';
                string[] splited = par.Split(chList, 2);
                result.Add(splited[0].Trim(), splited[1]);
            }
            return (result);
        }

        private Dictionary<string, string> GetParamDic(string[] arrayParams)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (string item in arrayParams)
            {
                if (item.Trim() == "")
                    continue;
                char[] chList = new char[1];
                chList[0] = '=';
                string[] splited = item.Split(chList, 2);
                result.Add(splited[0].Trim(), splited[1]);
            }
            return (result);
        }

        #endregion

        #region CryptoCoin Service

        private Response CryptoCoinServiceGetStrategyParams(Request request)
        {
            Response response = CreateResponse(request, "000000");
            MessageObj msg = null;
            try
            {
                //-- Get Parameters
                List<string> paramList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(request.Parameters));
                Dictionary<string, string> paramDic = GetParamDic(paramList);
                //-- Do Action
                List<string> resultList = new List<string>();
                List<string> result = ServiceInfo.StrategyDic[paramDic["Name"]].strategy.GetParams();
                resultList.Add(JsonConvert.SerializeObject(result));
                //-- Serialize and return data list
                response.Result = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), resultList));
            }
            catch (Exception ex)
            {
                if (msg == null)
                    msg = ServiceInfo.CryptoCoinMessages["CryptoCoin300003"];
                response.MessageCode = msg.Key;
                response.Message = msg.Value;
                //****************** Do Log Action
                LogItem item = new LogItem();
                item.Action = "CryptoCoinServiceGetStrategyParams";
                item.Date = Utility.PersianDate();
                item.Description = "User=" + request.Credential.UserName + " ,Exception: " + ex.Message;
                item.Duration = "";
                item.LogLevel = LogInfo.LogLevel.LOW_DETAIL;
                item.ModuleName = "CryptoCoinService";
                item.OutputMessageType = LogInfo.OutputMessageType.ERROR;
                item.OutputType = LogInfo.OutputType.FILE;
                item.SubModule = "CryptoCoinService";
                item.Time = Utility.GetTime();
                item.UniqueId = request.RefrenceNo;
                LogWorker worker = ServiceInfo.logger.GetLogWorker(ServiceInfo.logInfo);
                worker.WriteLog(item, ServiceInfo.logInfo);
                //********************************
            }
            return (response);
        }

        private Response CryptoCoinServiceRegisterStrategy(Request request)
        {
            Response response = CreateResponse(request, "000000");
            MessageObj msg = null;
            try
            {
                //-- Get Parameters
                List<string> paramList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(request.Parameters));
                Dictionary<string, string> paramDic = GetParamDic(paramList);
                //-- Do Action
                switch (paramDic["StrategyName"])
                {
                    case "SimpleCandelAnalyser":
                        SimpleCandelAnalyser simpleCandelAnalyser = new SimpleCandelAnalyser(paramDic,
                            paramDic["Name"],
                            BasePath);
                        break;
                    case "ThreeStepWatchOrder":
                        ThreeStepWatchOrder threeStepWatchOrder = new ThreeStepWatchOrder(paramDic,
                            paramDic["Name"],
                            BasePath);
                        break;
                    case "WatchOrder":
                        WatchOrder watchOrder = new WatchOrder(paramDic,
                            paramDic["Name"],
                            BasePath);
                        break;
                    case "EmptyStrategy":
                        EmptyStrategy emptyStrategy = new EmptyStrategy(paramDic,
                            paramDic["Name"],
                            BasePath);
                        break;
                    case "NorouziStrategy":
                        NorouziStrategy norouziStrayegy = new NorouziStrategy(paramDic,
                            paramDic["Name"],
                            BasePath);
                        break;
                    case "MohebiStrategy":
                        MohebiStrategy mohebiStrayegy = new MohebiStrategy(paramDic,
                            paramDic["Name"],
                            BasePath);
                        break;
                    case "SafiStrategy":
                        SafiStrategy safiStrayegy = new SafiStrategy(paramDic,
                            paramDic["Name"],
                            BasePath);
                        break;
                    default:
                        msg = ServiceInfo.CryptoCoinMessages["CryptoCoin300006"];
                        throw new Exception(msg.Key + ":" + msg.Value);
                }
                //-- Serialize and return data list
                List<string> resultList = new List<string>();
                response.Result = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), resultList));
            }
            catch (Exception ex)
            {
                if (msg == null)
                    msg = ServiceInfo.CryptoCoinMessages["CryptoCoin300002"];
                response.MessageCode = msg.Key;
                response.Message = msg.Value + "\r\n" + ex.Message;
                //****************** Do Log Action
                LogItem item = new LogItem();
                item.Action = "CryptoCoinServiceRegisterStrategy";
                item.Date = Utility.PersianDate();
                item.Description = "User=" + request.Credential.UserName + " ,Exception: " + ex.Message;
                item.Duration = "";
                item.LogLevel = LogInfo.LogLevel.LOW_DETAIL;
                item.ModuleName = "CryptoCoinService";
                item.OutputMessageType = LogInfo.OutputMessageType.ERROR;
                item.OutputType = LogInfo.OutputType.FILE;
                item.SubModule = "CryptoCoinService";
                item.Time = Utility.GetTime();
                item.UniqueId = request.RefrenceNo;
                LogWorker worker = ServiceInfo.logger.GetLogWorker(ServiceInfo.logInfo);
                worker.WriteLog(item, ServiceInfo.logInfo);
                //********************************
            }
            return (response);
        }

        private Response CryptoCoinServiceGetStrategyList(Request request)
        {
            Response response = CreateResponse(request, "000000");
            MessageObj msg = null;
            try
            {
                //-- Get Parameters
                List<string> paramList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(request.Parameters));
                Dictionary<string, string> paramDic = GetParamDic(paramList);
                //-- Do Action
                List<string> resultList = new List<string>();
                resultList.Add("SimpleCandelAnalyser");
                resultList.Add("ThreeStepWatchOrder");
                resultList.Add("WatchOrder");
                //-- Serialize and return data list
                response.Result = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), resultList));
            }
            catch (Exception ex)
            {
                if (msg == null)
                    msg = ServiceInfo.CryptoCoinMessages["CryptoCoin300003"];
                response.MessageCode = msg.Key;
                response.Message = msg.Value;
                //****************** Do Log Action
                LogItem item = new LogItem();
                item.Action = "CryptoCoinServiceGetStrategyList";
                item.Date = Utility.PersianDate();
                item.Description = "User=" + request.Credential.UserName + " ,Exception: " + ex.Message;
                item.Duration = "";
                item.LogLevel = LogInfo.LogLevel.LOW_DETAIL;
                item.ModuleName = "CryptoCoinService";
                item.OutputMessageType = LogInfo.OutputMessageType.ERROR;
                item.OutputType = LogInfo.OutputType.FILE;
                item.SubModule = "CryptoCoinService";
                item.Time = Utility.GetTime();
                item.UniqueId = request.RefrenceNo;
                LogWorker worker = ServiceInfo.logger.GetLogWorker(ServiceInfo.logInfo);
                worker.WriteLog(item, ServiceInfo.logInfo);
                //********************************
            }
            return (response);
        }

        private Response CryptoCoinServiceGetActiveStrategies(Request request)
        {
            Response response = CreateResponse(request, "000000");
            MessageObj msg = null;
            try
            {
                //-- Get Parameters
                List<string> paramList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(request.Parameters));
                Dictionary<string, string> paramDic = GetParamDic(paramList);
                //-- Do Action
                List<string> resultList = new List<string>();
                foreach (string item in ServiceInfo.StrategyDic.Keys)
                {
                    resultList.Add(item);
                }
                //-- Serialize and return data list
                response.Result = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), resultList));
            }
            catch (Exception ex)
            {
                if (msg == null)
                    msg = ServiceInfo.CryptoCoinMessages["CryptoCoin300003"];
                response.MessageCode = msg.Key;
                response.Message = msg.Value;
                //****************** Do Log Action
                LogItem item = new LogItem();
                item.Action = "CryptoCoinServiceGetActiveStrategies";
                item.Date = Utility.PersianDate();
                item.Description = "User=" + request.Credential.UserName + " ,Exception: " + ex.Message;
                item.Duration = "";
                item.LogLevel = LogInfo.LogLevel.LOW_DETAIL;
                item.ModuleName = "CryptoCoinService";
                item.OutputMessageType = LogInfo.OutputMessageType.ERROR;
                item.OutputType = LogInfo.OutputType.FILE;
                item.SubModule = "CryptoCoinService";
                item.Time = Utility.GetTime();
                item.UniqueId = request.RefrenceNo;
                LogWorker worker = ServiceInfo.logger.GetLogWorker(ServiceInfo.logInfo);
                worker.WriteLog(item, ServiceInfo.logInfo);
                //********************************
            }
            return (response);
        }

        private Response CryptoCoinServiceStartStrategy(Request request)
        {
            Response response = CreateResponse(request, "000000");
            MessageObj msg = null;
            try
            {
                //-- Get Parameters
                List<string> paramList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(request.Parameters));
                Dictionary<string, string> paramDic = GetParamDic(paramList);
                //-- Do Action
                List<string> resultList = new List<string>();
                ServiceInfo.StrategyDic[paramDic["Name"]].strategy.Start();
                //-- Serialize and return data list
                response.Result = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), resultList));
            }
            catch (Exception ex)
            {
                if (msg == null)
                    msg = ServiceInfo.CryptoCoinMessages["CryptoCoin300002"];
                response.MessageCode = msg.Key;
                response.Message = msg.Value;
                //****************** Do Log Action
                LogItem item = new LogItem();
                item.Action = "CryptoCoinServiceStartStrategy";
                item.Date = Utility.PersianDate();
                item.Description = "User=" + request.Credential.UserName + " ,Exception: " + ex.Message;
                item.Duration = "";
                item.LogLevel = LogInfo.LogLevel.LOW_DETAIL;
                item.ModuleName = "CryptoCoinService";
                item.OutputMessageType = LogInfo.OutputMessageType.ERROR;
                item.OutputType = LogInfo.OutputType.FILE;
                item.SubModule = "CryptoCoinService";
                item.Time = Utility.GetTime();
                item.UniqueId = request.RefrenceNo;
                LogWorker worker = ServiceInfo.logger.GetLogWorker(ServiceInfo.logInfo);
                worker.WriteLog(item, ServiceInfo.logInfo);
                //********************************
            }
            return (response);
        }

        private Response CryptoCoinServiceTerminateStrategy(Request request)
        {
            Response response = CreateResponse(request, "000000");
            MessageObj msg = null;
            try
            {
                //-- Get Parameters
                List<string> paramList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(request.Parameters));
                Dictionary<string, string> paramDic = GetParamDic(paramList);
                //-- Do Action
                List<string> resultList = new List<string>();
                ServiceInfo.StrategyDic[paramDic["Name"]].strategy.Terminate();
                //-- Serialize and return data list
                response.Result = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), resultList));
            }
            catch (Exception ex)
            {
                if (msg == null)
                    msg = ServiceInfo.CryptoCoinMessages["CryptoCoin300005"];
                response.MessageCode = msg.Key;
                response.Message = msg.Value;
                //****************** Do Log Action
                LogItem item = new LogItem();
                item.Action = "CryptoCoinServiceTerminateStrategy";
                item.Date = Utility.PersianDate();
                item.Description = "User=" + request.Credential.UserName + " ,Exception: " + ex.Message;
                item.Duration = "";
                item.LogLevel = LogInfo.LogLevel.LOW_DETAIL;
                item.ModuleName = "CryptoCoinService";
                item.OutputMessageType = LogInfo.OutputMessageType.ERROR;
                item.OutputType = LogInfo.OutputType.FILE;
                item.SubModule = "CryptoCoinService";
                item.Time = Utility.GetTime();
                item.UniqueId = request.RefrenceNo;
                LogWorker worker = ServiceInfo.logger.GetLogWorker(ServiceInfo.logInfo);
                worker.WriteLog(item, ServiceInfo.logInfo);
                //********************************
            }
            return (response);
        }

        private Response CryptoCoinServiceGetMonitoring(Request request)
        {
            Response response = CreateResponse(request, "000000");
            MessageObj msg = null;
            try
            {
                //-- Get Parameters
                List<string> paramList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(request.Parameters));
                Dictionary<string, string> paramDic = GetParamDic(paramList);
                //-- Do Action
                List<string> resultList = new List<string>();
                string result = ServiceInfo.StrategyDic[paramDic["Name"]].strategy.AddMonitoringInfo("GET", null);
                resultList.Add(result);
                //-- Serialize and return data list
                response.Result = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), resultList));
            }
            catch (Exception ex)
            {
                if (msg == null)
                    msg = ServiceInfo.CryptoCoinMessages["CryptoCoin300003"];
                response.MessageCode = msg.Key;
                response.Message = msg.Value;
                //****************** Do Log Action
                LogItem item = new LogItem();
                item.Action = "CryptoCoinServiceGetMonitoring";
                item.Date = Utility.PersianDate();
                item.Description = "User=" + request.Credential.UserName + " ,Exception: " + ex.Message;
                item.Duration = "";
                item.LogLevel = LogInfo.LogLevel.LOW_DETAIL;
                item.ModuleName = "CryptoCoinService";
                item.OutputMessageType = LogInfo.OutputMessageType.ERROR;
                item.OutputType = LogInfo.OutputType.FILE;
                item.SubModule = "CryptoCoinService";
                item.Time = Utility.GetTime();
                item.UniqueId = request.RefrenceNo;
                LogWorker worker = ServiceInfo.logger.GetLogWorker(ServiceInfo.logInfo);
                worker.WriteLog(item, ServiceInfo.logInfo);
                //********************************
            }
            return (response);
        }

        #endregion

        private Response CreateResponse(Request request, string code)
        {
            MessageObj msg = ServiceInfo.CryptoCoinMessages[code];
            Response response = new Response();
            response.Message = msg.Value;
            response.MessageCode = msg.Key;
            response.Result = "";
            response.BinaryData = null;
            response.Command = "Complete";
            response.CommandType = request.CommandType;
            response.Schema = "";
            response.RefrenceNo = request.RefrenceNo;
            string test = this.ServiceName;
            return (response);
        }

    }
}
