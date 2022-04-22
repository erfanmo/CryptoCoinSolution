using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utilities;
using CommonObjects;
using FrameworkConnectionInterface;
using System.IO;
using Newtonsoft.Json;
using CryptoCoinPropertyLib;
using System.IO;

namespace ServiceDllTest
{
    public partial class frm_Test : Form
    {
        public frm_Test()
        {
            InitializeComponent();
            string path = AppDomain.CurrentDomain.BaseDirectory + "Keys.txt";
            StreamReader reader = new StreamReader(path);
            txt_Key.Text = reader.ReadLine();
            txt_Secret.Text = reader.ReadLine();
            reader.Close();
        }

        private void btn_Test_Click(object sender, EventArgs e)
        {
            string refNum = Utility.GetUniqueId();
            ChavoshConnection connection = new ChavoshConnection(50,
                "127.0.0.1",
                12000,
                30000);
            Request request = new Request();
            request.BinaryData = null;
            request.Command = "CryptoCoinServiceTest";
            request.CommandType = "Normal";
            request.Parameters = "Ref Num is:" + refNum;
            request.RefrenceNo = refNum;
            request.Credential = new UserCredential();
            request.Credential.UserName = "";
            request.Credential.Password = "";
            request.Credential.ExtendedPassword = "";
            Response response = connection.SendCommand(request);


            MessageBox.Show(response.MessageCode + ": " + response.Message + "\r\n" +
                 response.Result + "\r\n");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            List<string> paramList = new List<string>();
            //-- Mandatory

            //-----------------------------------------
            string refNum = Utility.GetUniqueId();
            ChavoshConnection connection = new ChavoshConnection(50,
                "127.0.0.1",
                12000,
                30000);
            Request request = new Request();
            request.BinaryData = null;
            request.Command = "CryptoCoinServiceGetStrategyList";
            request.CommandType = "Normal";
            request.Parameters = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), paramList));
            request.RefrenceNo = refNum;
            request.Credential = new UserCredential();
            request.Credential.UserName = "";
            request.Credential.Password = "";
            request.Credential.ExtendedPassword = "";
            Response response = connection.SendCommand(request);
            if (response.MessageCode == "000000")
            {
                List<string> respList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(response.Result));
            }
            MessageBox.Show(response.MessageCode + ": " + response.Message + "\r\n" +
                 response.Result + "\r\n");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            List<string> paramList = new List<string>();
            //-- Mandatory

            //-----------------------------------------
            string refNum = Utility.GetUniqueId();
            ChavoshConnection connection = new ChavoshConnection(50,
                "127.0.0.1",
                12000,
                30000);
            Request request = new Request();
            request.BinaryData = null;
            request.Command = "CryptoCoinServiceGetActiveStrategies";
            request.CommandType = "Normal";
            request.Parameters = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), paramList));
            request.RefrenceNo = refNum;
            request.Credential = new UserCredential();
            request.Credential.UserName = "";
            request.Credential.Password = "";
            request.Credential.ExtendedPassword = "";
            Response response = connection.SendCommand(request);
            if (response.MessageCode == "000000")
            {
                List<string> respList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(response.Result));
            }
            MessageBox.Show(response.MessageCode + ": " + response.Message + "\r\n" +
                 response.Result + "\r\n");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            List<string> paramList = new List<string>();
            //-- Mandatory
            paramList.Add("Name=" + txt_Name.Text);
            //-----------------------------------------
            string refNum = Utility.GetUniqueId();
            ChavoshConnection connection = new ChavoshConnection(50,
                "127.0.0.1",
                12000,
                30000);
            Request request = new Request();
            request.BinaryData = null;
            request.Command = "CryptoCoinServiceGetActiveStrategies";
            request.CommandType = "Normal";
            request.Parameters = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), paramList));
            request.RefrenceNo = refNum;
            request.Credential = new UserCredential();
            request.Credential.UserName = "";
            request.Credential.Password = "";
            request.Credential.ExtendedPassword = "";
            Response response = connection.SendCommand(request);
            if (response.MessageCode == "000000")
            {
                List<string> respList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(response.Result));
                if (respList.Count > 0)
                {
                    List<string> Obj = JsonConvert.DeserializeObject<List<string>>(respList[0]);
                }
            }
            MessageBox.Show(response.MessageCode + ": " + response.Message + "\r\n" +
                 response.Result + "\r\n");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<string> paramList = new List<string>();
            //-- Mandatory
            paramList.Add("Name=" + txt_Name.Text);
            //-----------------------------------------
            string refNum = Utility.GetUniqueId();
            ChavoshConnection connection = new ChavoshConnection(50,
                "127.0.0.1",
                12000,
                30000);
            Request request = new Request();
            request.BinaryData = null;
            request.Command = "CryptoCoinServiceGetMonitoring";
            request.CommandType = "Normal";
            request.Parameters = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), paramList));
            request.RefrenceNo = refNum;
            request.Credential = new UserCredential();
            request.Credential.UserName = "";
            request.Credential.Password = "";
            request.Credential.ExtendedPassword = "";
            Response response = connection.SendCommand(request);
            if (response.MessageCode == "000000")
            {
                List<string> respList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(response.Result));
                MonitorObj Obj = JsonConvert.DeserializeObject<MonitorObj>(respList[0]);
                MessageBox.Show("Loss = " + Obj.totalLoss + "\r\n" +
                    "Profit = " + Obj.totalProfit + "\r\n" +
                    "Count = " + Obj.count);
            }
            MessageBox.Show(response.MessageCode + ": " + response.Message + "\r\n" +
                 response.Result + "\r\n");
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
            //***************************************** Three Step Watch Order
            //-- Mandatory
            paramList.Add("Name=" + txt_Name.Text);
            paramList.Add("StrategyName=" + "WatchOrder");
            paramList.Add("JustLog=" + chk_JustLog.Checked.ToString());
            //-- Params
            paramList.Add("PairCoins=" + txt_PairCoins.Text);
            paramList.Add("FeeRate=" + txt_FeeRate.Text);
            paramList.Add("BuyGapAmount=10");
            paramList.Add("SellGapAmount=10");
            paramList.Add("ScheduleTime=" + txt_Schedule.Text);
            paramList.Add("HighSellWatchPeriod=" + txt_HighSellWatchPeriod.Text);
            paramList.Add("LowBuyWatchPeriod=" + txt_LowBuyWatchPeriod.Text);
            paramList.Add("AcceptableProfitLimit=" + txt_AcceptableProfitLimit.Text);
            paramList.Add("AcceptableLossLimit=" + txt_AcceptableLossLimit.Text);
            paramList.Add("Orders=" + txt_Params.Text);
            paramList.Add("Wait=" + txt_Wait.Text);
            paramList.Add("MaxActive=" + txt_MaxActiveOrder.Text);
            paramList.Add("Key=" + txt_Key.Text);
            paramList.Add("Secret=" + txt_Secret.Text);
            //-- Auto ReOrder
            paramList.Add("AutoReload=" + chk_AutoReload.Checked.ToString());
            paramList.Add("AutoReloadCount=" + txt_AutoReloadCount.Text.ToString());
            paramList.Add("AutoOrderCount=" + txt_AutoOrderCount.Text.ToString());
            paramList.Add("AutoOrderAmount=" + txt_AutoOrderAmount.Text.ToString());
            paramList.Add("AutoOrderWatchPeriod=" + txt_AutoOrderWatchPeriod.Text.ToString());
            paramList.Add("AutoOrderLockPeriod=" + txt_AutoOrderLockPeriod.Text.ToString());
            paramList.Add("AutoOrderCoefficient=" + txt_AutoOrderCoefficient.Text.ToString());
            //-- OrderInstedLoss
            paramList.Add("OrderInstedLoss=" + chk_OrderInstedLoss.Checked.ToString());
            paramList.Add("HigherPriceAmount=" + txt_HigherPriceAmount.Text.ToString());
            //-- Control Loss Count
            paramList.Add("ControlLossCount=" + chk_LossCount.Checked.ToString());
            paramList.Add("LossCount=" + txt_LossCount.Text.ToString());
            //-- Notification
            paramList.Add("ProfitLossNotification=" + chk_ProfitLossNotification.Checked.ToString());
            paramList.Add("PeriodicalNotification=" + chk_PeriodicalNotification.Checked.ToString());
            paramList.Add("PeriodicalNotificationSchedule=" + txt_PeriodicalNotify.Text);
            paramList.Add("EmailList=" + txt_EmailDelivery.Text);
            paramList.Add("MobileDelivery=" + txt_MobileDelivery.Text);
            paramList.Add("SendEmail=" + chk_Email.Checked.ToString());
            paramList.Add("SendSMS=" + chk_SMS.Checked.ToString());
            paramList.Add("SendNotification=" + chk_Notification.Checked.ToString());
            //-----------------------------------------
            string refNum = Utility.GetUniqueId();
            ChavoshConnection connection = new ChavoshConnection(50,
                "127.0.0.1",
                12000,
                30000);
            Request request = new Request();
            request.BinaryData = null;
            request.Command = "CryptoCoinServiceRegisterStrategy";
            request.CommandType = "Normal";
            request.Parameters = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), paramList));
            request.RefrenceNo = refNum;
            request.Credential = new UserCredential();
            request.Credential.UserName = "";
            request.Credential.Password = "";
            request.Credential.ExtendedPassword = "";
            Response response = connection.SendCommand(request);
            if (response.MessageCode == "000000")
            {
                List<string> respList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(response.Result));
            }
            MessageBox.Show(response.MessageCode + ": " + response.Message + "\r\n" +
                 response.Result + "\r\n");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<string> paramList = new List<string>();
            //-- Mandatory
            //paramList.Add("Name=" + "MySampleStrategy");
            //paramList.Add("Name=" + "ThreeStepWatchOrderSample");
            paramList.Add("Name=" + txt_Name.Text);

            //-----------------------------------------
            string refNum = Utility.GetUniqueId();
            ChavoshConnection connection = new ChavoshConnection(50,
                "127.0.0.1",
                12000,
                30000);
            Request request = new Request();
            request.BinaryData = null;
            request.Command = "CryptoCoinServiceStartStrategy";
            request.CommandType = "Normal";
            request.Parameters = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), paramList));
            request.RefrenceNo = refNum;
            request.Credential = new UserCredential();
            request.Credential.UserName = "";
            request.Credential.Password = "";
            request.Credential.ExtendedPassword = "";
            Response response = connection.SendCommand(request);
            if (response.MessageCode == "000000")
            {
                List<string> respList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(response.Result));
            }
            MessageBox.Show(response.MessageCode + ": " + response.Message + "\r\n" +
                 response.Result + "\r\n");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            List<string> paramList = new List<string>();
            //-- Mandatory
            //paramList.Add("Name=" + "MySampleStrategy");
            //paramList.Add("Name=" + "ThreeStepWatchOrderSample");
            paramList.Add("Name=" + txt_Name.Text);

            //-----------------------------------------
            string refNum = Utility.GetUniqueId();
            ChavoshConnection connection = new ChavoshConnection(50,
                "127.0.0.1",
                12000,
                30000);
            Request request = new Request();
            request.BinaryData = null;
            request.Command = "CryptoCoinServiceTerminateStrategy";
            request.CommandType = "Normal";
            request.Parameters = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), paramList));
            request.RefrenceNo = refNum;
            request.Credential = new UserCredential();
            request.Credential.UserName = "";
            request.Credential.Password = "";
            request.Credential.ExtendedPassword = "";
            Response response = connection.SendCommand(request);
            if (response.MessageCode == "000000")
            {
                List<string> respList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(response.Result));
            }
            MessageBox.Show(response.MessageCode + ": " + response.Message + "\r\n" +
                 response.Result + "\r\n");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            frm_EmptyStrategy form = new frm_EmptyStrategy();
            form.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            frm_NorouziStrategy form = new frm_NorouziStrategy();
            form.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            frm_MohebiStrategy form = new frm_MohebiStrategy();
            form.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            frm_SafiStrategy form = new frm_SafiStrategy();
            form.ShowDialog();
        }

    }
}
