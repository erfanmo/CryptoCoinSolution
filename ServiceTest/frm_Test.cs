using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using CommonObjects;
using FrameworkBaseService;
using Utilities;
using System.Security.Cryptography;
using System.IO;
using CryptoCoinPropertyLib;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;

namespace ServiceTest
{
    public partial class frm_Test : Form
    {

        ServiceOperation serviceOperation = null;

        public frm_Test()
        {
            InitializeComponent();
            btn_Initialize_Click(this, null);
        }

        private void btn_Initialize_Click(object sender, EventArgs e)
        {
            serviceOperation = new ServiceOperation();
            for (int i = 0; i < 10; i++)
            {
                int nextId = serviceOperation.GetNextId();
                List<string> parameters = new List<string>();
                parameters.Add("Sample" + i.ToString());
                CryptoCoinService service = new CryptoCoinService("CryptoCoinService" + nextId.ToString(), @"C:\Tests\CryptoCoinFramework\Config\CryptoCoinService", nextId, parameters);
                serviceOperation.AddService(service);
            }
            CryptoCoinService CryptoCoinService = (CryptoCoinService)serviceOperation.GetBaseService();
            CryptoCoinService.InitializeService();

            //-----------------------------

        }

        private void btn_Test_Click(object sender, EventArgs e)
        {
            //-----------------------------------------
            string refCode = Utility.GetUniqueId();
            UserCredential uCredential = new UserCredential();
            uCredential.UserName = Utility.GetParameter("UserName");
            uCredential.Password = Utility.GetParameter("Password1");
            uCredential.ExtendedPassword = Utility.GetParameter("Password2");
            Information.SecureCredential(uCredential, refCode);
            Request request = new Request();
            request.BinaryData = null;
            request.Command = "CryptoCoinServiceTest";
            request.CommandType = "Normal";
            request.Credential = uCredential;
            request.Parameters = "";
            request.RefrenceNo = refCode;
            //-----------------------------------------
            CryptoCoinService ctrService = (CryptoCoinService)serviceOperation.GetBaseService();
            Response response = ctrService.DoJob(request);
            new frm_ShowResult(response.Command + "\r\n" +
                response.MessageCode + "\r\n" +
                response.Message).ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<MonitoringItem> mList = serviceOperation.MonitorObject();
            MessageBox.Show("Test");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string result = serviceOperation.Ping("");
            MessageBox.Show(result);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> paramList = new List<string>();
            //***************************************** Simple Candel Analyser
            ////-- Mandatory
            //paramList.Add("Name=" + "MySampleStrategy");
            //paramList.Add("StrategyName=" + "SimpleCandelAnalyser");
            //paramList.Add("JustLog=" + "True");
            ////-- Params
            //paramList.Add("PairCoins=" + "BTCUSDT");
            //paramList.Add("Amount=" + "100");
            //paramList.Add("BuyGapAmount=" + "20");
            //paramList.Add("SellGapAmount=" + "20");
            //paramList.Add("FeeRate=" + "0.001");
            //paramList.Add("ScheduleTime=" + "5");
            //paramList.Add("TimeWindowOneMin=" + "2");
            //paramList.Add("TimeWindowQuarter=" + "1");
            //paramList.Add("LossLimit=" + "0.6");
            //paramList.Add("WaitAfterLoss=" + "60");
            //paramList.Add("ProfitLimit=" + "0.4");
            //paramList.Add("WaitAfterProfit=" + "30");
            //paramList.Add("PositiveNegetiveRatioQuarter=" + "0.8");
            //paramList.Add("NegetivePositiveRatioQuarter=" + "0.4");
            //paramList.Add("PositiveNegetiveRatioOneMin=" + "0.4");
            //paramList.Add("AccumulationIgnore=" + "80");
            //paramList.Add("Key=" + "");
            //paramList.Add("Secret=" + "");
            ////***************************************** Three Step Watch Order
            ////-- Mandatory
            //paramList.Add("Name=" + "ThreeStepWatchOrderSample");
            //paramList.Add("StrategyName=" + "ThreeStepWatchOrder");
            //paramList.Add("JustLog=" + "True");
            ////-- Params
            //paramList.Add("PairCoins=BTCUSDT");
            //paramList.Add("Amount=100");
            //paramList.Add("FeeRate=0.001");
            //paramList.Add("BuyGapAmount=10");
            //paramList.Add("SellGapAmount=10");
            //paramList.Add("ScheduleTime=2");
            //paramList.Add("BuyPrice=41500");
            //paramList.Add("HighSellWatchPeriod=50");
            //paramList.Add("LowBuyWatchPeriod=70");
            //paramList.Add("TolerableLossLimit=5");
            //paramList.Add("TolerableProfitLimit=1000");
            //paramList.Add("AcceptableProfitLimit=0.04");
            //paramList.Add("AccumulationIgnore=" + "100");
            //paramList.Add("TimeWindowQuarter=3");
            //paramList.Add("PositiveNegetiveRatio=0.7");
            //paramList.Add("NegetivePositiveRatio=0.2");
            //paramList.Add("LossPositiveNegetiveRatio=0.6");
            //paramList.Add("MaxLossCount=2");
            //paramList.Add("StartLossCount=0");
            //paramList.Add("AutoReload=True");
            //paramList.Add("ReloadGap=300");
            //paramList.Add("Key=");
            //paramList.Add("Secret=");
            //***************************************** Watch Order
            //-- Mandatory
            paramList.Add("Name=" + "WatchOrderSample");
            paramList.Add("StrategyName=" + "WatchOrder");
            paramList.Add("JustLog=" + "True");
            //-- Params
            paramList.Add("PairCoins=BTCUSDT");
            paramList.Add("FeeRate=0.001");
            paramList.Add("BuyGapAmount=10");
            paramList.Add("SellGapAmount=10");
            paramList.Add("ScheduleTime=4");
            paramList.Add("HighSellWatchPeriod=100");
            paramList.Add("LowBuyWatchPeriod=100");
            paramList.Add("AcceptableProfitLimit=0.20");
            paramList.Add("AcceptableLossLimit=1.5");
            paramList.Add("Orders=100_30000;100_29000");
            paramList.Add("Wait=2");
            paramList.Add("MaxActive=2");
            paramList.Add("Key=");
            paramList.Add("Secret=");
            //-- Auto ReOrder
            paramList.Add("AutoReload=True");
            paramList.Add("AutoReloadCount=100");
            paramList.Add("AutoOrderCount=4");
            paramList.Add("AutoOrderAmount=50");
            paramList.Add("AutoOrderWatchPeriod=300");
            paramList.Add("AutoOrderLockPeriod=400");
            paramList.Add("AutoOrderCoefficient=100");
            //-- OrderInstedLoss
            paramList.Add("OrderInstedLoss=True");
            paramList.Add("HigherPriceAmount=300");
            //-- Control Loss Count
            paramList.Add("ControlLossCount=True");
            paramList.Add("LossCount=1");
            //-- Notification
            paramList.Add("ProfitLossNotification=True");
            paramList.Add("PeriodicalNotification=True");
            paramList.Add("PeriodicalNotificationSchedule=3");
            paramList.Add("EmailList=mor.hosseini@gmail.com;m-hosseinia@agri-bank.com");
            paramList.Add("MobileDelivery=");
            paramList.Add("SendEmail=True");
            paramList.Add("SendSMS=False");
            paramList.Add("SendNotification=False");
            //-----------------------------------------
            string refCode = Utility.GetUniqueId();
            UserCredential uCredential = new UserCredential();
            uCredential.UserName = "";
            uCredential.Password = "";
            uCredential.ExtendedPassword = "";
            Request request = new Request();
            request.BinaryData = null;
            request.Command = "CryptoCoinServiceRegisterStrategy";
            request.CommandType = "Normal";
            request.Credential = uCredential;
            request.Parameters = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), paramList));
            request.RefrenceNo = refCode;
            //-----------------------------------------
            CryptoCoinService service = (CryptoCoinService)serviceOperation.GetBaseService();
            Response response = service.DoJob(request);
            if (response.MessageCode == "000000")
            {
                List<string> respList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(response.Result));
            }
            new frm_ShowResult(response.Command + "\r\n" +
                response.MessageCode + "\r\n" +
                response.Message).ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<string> paramList = new List<string>();
            //-- Mandatory
            //paramList.Add("Name=" + "MySampleStrategy");
            //paramList.Add("Name=" + "ThreeStepWatchOrderSample");
            paramList.Add("Name=" + "WatchOrderSample");
            
            //-----------------------------------------
            string refCode = Utility.GetUniqueId();
            UserCredential uCredential = new UserCredential();
            uCredential.UserName = "";
            uCredential.Password = "";
            uCredential.ExtendedPassword = "";
            Request request = new Request();
            request.BinaryData = null;
            request.Command = "CryptoCoinServiceStartStrategy";
            request.CommandType = "Normal";
            request.Credential = uCredential;
            request.Parameters = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), paramList));
            request.RefrenceNo = refCode;
            //-----------------------------------------
            CryptoCoinService service = (CryptoCoinService)serviceOperation.GetBaseService();
            Response response = service.DoJob(request);
            if (response.MessageCode == "000000")
            {
                List<string> respList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(response.Result));
            }
            new frm_ShowResult(response.Command + "\r\n" +
                response.MessageCode + "\r\n" +
                response.Message).ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<string> paramList = new List<string>();
            //-- Mandatory
            paramList.Add("Name=" + "MySampleStrategy");
            //-----------------------------------------
            string refCode = Utility.GetUniqueId();
            UserCredential uCredential = new UserCredential();
            uCredential.UserName = "";
            uCredential.Password = "";
            uCredential.ExtendedPassword = "";
            Request request = new Request();
            request.BinaryData = null;
            request.Command = "CryptoCoinServiceGetMonitoring";
            request.CommandType = "Normal";
            request.Credential = uCredential;
            request.Parameters = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), paramList));
            request.RefrenceNo = refCode;
            //-----------------------------------------
            CryptoCoinService service = (CryptoCoinService)serviceOperation.GetBaseService();
            Response response = service.DoJob(request);
            if (response.MessageCode == "000000")
            {
                List<string> respList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(response.Result));
                MonitorObj Obj = JsonConvert.DeserializeObject<MonitorObj>(respList[0]);
                MessageBox.Show("Loss = " + Obj.totalLoss + "\r\n" +
                    "Profit = " + Obj.totalProfit + "\r\n" +
                    "Count = " + Obj.count);
            }
            new frm_ShowResult(response.Command + "\r\n" +
                response.MessageCode + "\r\n" +
                response.Message).ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            List<string> paramList = new List<string>();
            //-- Mandatory
            paramList.Add("Name=" + "MySampleStrategy");
            //-----------------------------------------
            string refCode = Utility.GetUniqueId();
            UserCredential uCredential = new UserCredential();
            uCredential.UserName = "";
            uCredential.Password = "";
            uCredential.ExtendedPassword = "";
            Request request = new Request();
            request.BinaryData = null;
            request.Command = "CryptoCoinServiceGetStrategyParams";
            request.CommandType = "Normal";
            request.Credential = uCredential;
            request.Parameters = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), paramList));
            request.RefrenceNo = refCode;
            //-----------------------------------------
            CryptoCoinService service = (CryptoCoinService)serviceOperation.GetBaseService();
            Response response = service.DoJob(request);
            if (response.MessageCode == "000000")
            {
                List<string> respList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(response.Result));
                List<string> Obj = JsonConvert.DeserializeObject<List<string>>(respList[0]);
            }
            new frm_ShowResult(response.Command + "\r\n" +
                response.MessageCode + "\r\n" +
                response.Message).ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            List<string> paramList = new List<string>();
            //-- Mandatory
            //paramList.Add("Name=" + "MySampleStrategy");
            //paramList.Add("Name=" + "ThreeStepWatchOrderSample");
            paramList.Add("Name=" + "WatchOrderSample");

            //-----------------------------------------
            string refCode = Utility.GetUniqueId();
            UserCredential uCredential = new UserCredential();
            uCredential.UserName = "";
            uCredential.Password = "";
            uCredential.ExtendedPassword = "";
            Request request = new Request();
            request.BinaryData = null;
            request.Command = "CryptoCoinServiceTerminateStrategy";
            request.CommandType = "Normal";
            request.Credential = uCredential;
            request.Parameters = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), paramList));
            request.RefrenceNo = refCode;
            //-----------------------------------------
            CryptoCoinService service = (CryptoCoinService)serviceOperation.GetBaseService();
            Response response = service.DoJob(request);
            if (response.MessageCode == "000000")
            {
                List<string> respList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(response.Result));
            }
            new frm_ShowResult(response.Command + "\r\n" +
                response.MessageCode + "\r\n" +
                response.Message).ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            List<string> paramList = new List<string>();
            //-- Mandatory

            //-----------------------------------------
            string refCode = Utility.GetUniqueId();
            UserCredential uCredential = new UserCredential();
            uCredential.UserName = "";
            uCredential.Password = "";
            uCredential.ExtendedPassword = "";
            Request request = new Request();
            request.BinaryData = null;
            request.Command = "CryptoCoinServiceGetStrategyList";
            request.CommandType = "Normal";
            request.Credential = uCredential;
            request.Parameters = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), paramList));
            request.RefrenceNo = refCode;
            //-----------------------------------------
            CryptoCoinService service = (CryptoCoinService)serviceOperation.GetBaseService();
            Response response = service.DoJob(request);
            if (response.MessageCode == "000000")
            {
                List<string> respList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(response.Result));
            }
            new frm_ShowResult(response.Command + "\r\n" +
                response.MessageCode + "\r\n" +
                response.Message).ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            List<string> paramList = new List<string>();
            //-- Mandatory 

            //-----------------------------------------
            string refCode = Utility.GetUniqueId();
            UserCredential uCredential = new UserCredential();
            uCredential.UserName = "";
            uCredential.Password = "";
            uCredential.ExtendedPassword = "";
            Request request = new Request();
            request.BinaryData = null;
            request.Command = "CryptoCoinServiceGetActiveStrategies";
            request.CommandType = "Normal";
            request.Credential = uCredential;
            request.Parameters = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), paramList));
            request.RefrenceNo = refCode;
            //-----------------------------------------
            CryptoCoinService service = (CryptoCoinService)serviceOperation.GetBaseService();
            Response response = service.DoJob(request);
            if (response.MessageCode == "000000")
            {
                List<string> respList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(response.Result));
            }
            new frm_ShowResult(response.Command + "\r\n" +
                response.MessageCode + "\r\n" +
                response.Message).ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            frm_EmptyStrategy form = new frm_EmptyStrategy(serviceOperation);
            form.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            frm_NorouziStrategy form = new frm_NorouziStrategy(serviceOperation);
            form.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            frm_MohebiStrategy form = new frm_MohebiStrategy(serviceOperation);
            form.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            frm_SafiStrategy form = new frm_SafiStrategy(serviceOperation);
            form.ShowDialog();
        }

    }
}
