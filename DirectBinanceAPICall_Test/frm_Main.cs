using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using BinanceAPICall;
using CryptoCoinPropertyLib;
using Newtonsoft.Json;
using System.Net;

namespace DirectBinanceAPICall
{
    public partial class frm_Main : Form
    {
        public frm_Main()
        {
            InitializeComponent();
            BinanceAPI.key = txt_Key.Text;
            BinanceAPI.secret = txt_Secret.Text;
            txt_DT.Text = DateTime.Now.ToString();
            txt_UnixDT.Text = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            txt_StartTime.Text = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
        }

        //--Get depth of a symbol
        private void btn_Depth_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.GetJson("v1/depth", "symbol=BTCUSDT");
                SymbolDepth obj = JsonConvert.DeserializeObject<SymbolDepth>(result);
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
            }
            Utility.LogToFile(result, "GetSymbolDepth");
            txt_Result.Text = result;
        }

        //--Get latest price of all symbols
        private void btn_AllPrices_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo= Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;
            //e.Cancel = (result == DialogResult.No);

            

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.GetJson("v1/ticker/allPrices");
                List<Price> obj = JsonConvert.DeserializeObject<List<Price>>(result);
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
            }
            Utility.LogToFile(result, "GetAllPrices");
            txt_Result.Text = result;

            INotification _mail = new MailService("samen2any@gmail.com");
            _mail.Notify("TestSubject", "TestMessage");
        }
        //--Get Account Info
        private void btn_AccountInfo_Click(object sender, EventArgs e)
        {

            BinanceAPI.key = txt_Key.Text;
            BinanceAPI.secret = txt_Secret.Text;

            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.GetSignedJson("v3/account");
                AccountInfo obj = JsonConvert.DeserializeObject<AccountInfo>(result);
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
            }
            Utility.LogToFile(result, "GetAccountInfo");
            txt_Result.Text = result;
        }
        //--Get Orders
        private void btn_Orders_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.GetSignedJson("v3/allOrders", "symbol=" + txt_PairCoins.Text +
                    "&" + "startTime=" + txt_StartTime.Text +
                    "&" + "limit=100");
                List<OrderHistory> obj = JsonConvert.DeserializeObject<List<OrderHistory>>(result);
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
                result = ex.Message;
            }
            Utility.LogToFile(result, "GetAllOrders");
            txt_Result.Text = result;
        }
        //--Test Order (BUY or SELL)
        private void btn_TestLimit_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.PostSignedJson("v3/order/test", "symbol=BTCUSDT" + "&" + "side=BUY" + "&" + "type=LIMIT" + "&" + "quantity=1.00" + "&" + "price=20000" + "&" + "timeInForce=GTC" + "&" + "recvWindow=6000");
                Order obj = JsonConvert.DeserializeObject<Order>(result);
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
                result = ex.Message;
            }
            Utility.LogToFile(result, "TestLimitOrder");
            txt_Result.Text = result;
        }
        //--Place a order (BUY or SELL)
        private void btn_LimitOrder_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.PostSignedJson("v3/order", "symbol=BTCUSDT" + "&" +
                "side=" + txt_Side.Text + "&" +
                "type=LIMIT" + "&" +
                "quantity=" + txt_Quantity.Text + "&" +
                "price=" + txt_Price.Text + "&" +
                "timeInForce=GTC" + "&" +
                "recvWindow=6000");
                Order obj = JsonConvert.DeserializeObject<Order>(result);
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
                result = ex.Message;
            }
            Utility.LogToFile(result, "LimitOrder");
            txt_Result.Text = result;
        }
        //--Cancel an order
        private void btn_CancelOrder_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.DeleteSignedJson("v3/order", "symbol=" + txt_PairCoins.Text + "&" + 
                    "orderId=" + txt_OrderId.Text);
                CancelOrder obj = JsonConvert.DeserializeObject<CancelOrder>(result);
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
                result = ex.Message;
            }
            Utility.LogToFile(result, "CancelOrder");
            txt_Result.Text = result;
        }
        //--Check an order's status
        private void btn_GetOrderStatus_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.GetSignedJson("v3/order", "symbol=" + txt_PairCoins.Text + "&" +
                "orderId=" + txt_OrderId.Text);
                OrderStatus obj = JsonConvert.DeserializeObject<OrderStatus>(result);
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
                result = ex.Message;
            }
            Utility.LogToFile(result, "GetOrderStatus");
            txt_Result.Text = result;
        }

        private void btn_Connectivity_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.GetJson("v3/ping");
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
            }
            Utility.LogToFile(result, "TestConnectivity");
            txt_Result.Text = result;
        }

        private void btn_ServerTime_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.GetJson("v3/time");
                try
                {
                    ServerTime obj = JsonConvert.DeserializeObject<ServerTime>(result);
                }
                catch (Exception ex)
                {
                    Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                    error.desc = ex.Message;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            Utility.LogToFile(result, "ServerTime");
            txt_Result.Text = result;
        }

        private void btn_ExchangeInfo_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.GetJson("v3/exchangeInfo");
                ExchangeInfo obj = JsonConvert.DeserializeObject<ExchangeInfo>(result);
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
            }
            Utility.LogToFile(result, "ExchangeInfo");
            txt_Result.Text = result;
        }

        private void btn_RecentTrades_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.GetJson("v3/trades", "symbol=BTCUSDT" + "&" + "limit=500");
                List<Trade> obj = JsonConvert.DeserializeObject<List<Trade>>(result);
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
            }
            Utility.LogToFile(result, "RecentTrades");
            txt_Result.Text = result;
        }

        private void btn_OldTradeLookup_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.GetJson("v3/historicalTrades", "symbol=BTCUSDT" + "&" + "limit=500" + "&" + "fromId=504328455");
                List<Trade> obj = JsonConvert.DeserializeObject<List<Trade>>(result);
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
            }
            Utility.LogToFile(result, "OldTradesLookUp");
            txt_Result.Text = result;
        }

        private void btn_AggregateTrades_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.GetJson("v3/aggTrades", "symbol=BTCUSDT" + "&" + "limit=500");
                List<AggregateTrade> obj = JsonConvert.DeserializeObject<List<AggregateTrade>>(result);
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
            }
            Utility.LogToFile(result, "AggregateTrades");
            txt_Result.Text = result;
        }

        private void btn_Candlestick_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.GetJson("v3/klines", "symbol=BTCUSDT" + "&" + "interval=1h");
                string[][] obj = JsonConvert.DeserializeObject<string[][]>(result);
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
            }
            Utility.LogToFile(result, "CandlestickData");
            txt_Result.Text = result;
        }

        private void btn_CurrentAveragePrice_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.GetJson("v3/avgPrice", "symbol=ETHUSDT");
                AveragePrice obj = JsonConvert.DeserializeObject<AveragePrice>(result);
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
            }
            Utility.LogToFile(result, "CurrentAveragePrice");
            txt_Result.Text = result;
        }

        private void btn_24hrTickerPriceChange_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.GetJson("v3/ticker/24hr", "symbol=BTCUSDT");
                Price24 obj = JsonConvert.DeserializeObject<Price24>(result);
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
            }
            Utility.LogToFile(result, "24hrTickerPrice");
            txt_Result.Text = result;
        }

        private void btn_SymbolPrice_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.GetJson("v3/ticker/price", "symbol=ETHUSDT");
                Price obj = JsonConvert.DeserializeObject<Price>(result);
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
            }
            Utility.LogToFile(result, "SymbolPrice");
            txt_Result.Text = result;
        }

        private void btn_SymbolBookTicker_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.GetJson("v3/ticker/bookTicker", "symbol=BTCUSDT");
                SymbolBookTicker obj = JsonConvert.DeserializeObject<SymbolBookTicker>(result);
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
            }
            Utility.LogToFile(result, "SymbolBookTicker");
            txt_Result.Text = result;
        }

        private void btn_CancelAllOrders_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.DeleteSignedJson("v3/openOrders", "symbol=" + txt_PairCoins.Text);
                List<CancelOrder> obj = JsonConvert.DeserializeObject<List<CancelOrder>>(result);
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
                result = ex.Message;
            }
            Utility.LogToFile(result, "CancelAllOrders");
            txt_Result.Text = result;
        }

        private void btn_GetOpenOrders_Click(object sender, EventArgs e)
        {
            IpInfo _ipInfo = Utility.GetIPInfo();
            string _confirmation = string.Format("Country:{2}\r\nIP:{0}\r\nLocalIP:{1}\r\nRegion:{3}\r\nCity:{4}\r\nPostal:{5}", _ipInfo.Ip, _ipInfo.LocalIp, _ipInfo.Country, _ipInfo.Region, _ipInfo.City, _ipInfo.Postal);
            var _authorization = MessageBox.Show(_confirmation, "caption",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (_authorization == DialogResult.No)
                return;

            txt_Result.Clear();
            string result = "";
            try
            {
                result = BinanceAPI.GetSignedJson("v3/openOrders", "symbol=" + txt_PairCoins.Text);
                List<OrderStatus> obj = JsonConvert.DeserializeObject<List<OrderStatus>>(result);
            }
            catch (Exception ex)
            {
                Error error = JsonConvert.DeserializeObject<Error>(ex.Message);
                error.desc = ex.Message;
                result = ex.Message;
            }
            Utility.LogToFile(result, "GetOpenOrders");
            txt_Result.Text = result;
        }

        private void btn_FromUnix_Click(object sender, EventArgs e)
        {
            DateTimeOffset offsetDate;
            DateTime regularDate;
            offsetDate = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(txt_UnixDT.Text));
            regularDate = offsetDate.DateTime;
            txt_DT.Text = regularDate.ToString();
        }

    }
}
