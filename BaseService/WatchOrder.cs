using BinanceAPICall;
using CryptoCoinPropertyLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkBaseService
{
    public class WatchOrder : BaseStrategy
    {
        List<WatchOrderObj> lstOrders = null;
        int LossCount = 0;

        public WatchOrder(Dictionary<string, string> parameters, string name, string basePath) : base(parameters, name, basePath)
        {
            
        }

        public override void Init()
        {
            CreateOrders();
            if (bool.Parse(strategyInfo.paramDic["AutoReload"]))
                Reload();
            if (bool.Parse(strategyInfo.paramDic["ProfitLossNotification"]) ||
                bool.Parse(strategyInfo.paramDic["PeriodicalNotification"]))
            {
                strategyInfo.notification = new Notification();
                strategyInfo.notification.thread = new System.Threading.Thread(strategyInfo.notification.DoJob);
                strategyInfo.notification.thread.IsBackground = true;
                strategyInfo.notification.thread.Start();
                string subject = "TraidingNotification-Start-" + strategyInfo.name;
                string body = "Strategy '" + strategyInfo.name + "' Started At: " + DateTime.Now.ToString() + "\r\n\r\n" +
                    "Initial Load = " + strategyInfo.paramDic["Orders"];
                List<string> parameters = new List<string>();
                parameters.Add("EMAIL");
                parameters.Add(strategyInfo.paramDic["EmailList"]);
                parameters.Add(subject);
                parameters.Add(body);
                strategyInfo.notification.QueueAction(parameters, "ENQUEUE");
            }
            ServiceInfo.LogStrategyAction(strategyInfo, "Initialize", strategyInfo.name + ", Initialized");
        }

        private OrderStatus BuyUntilSuccess(WatchOrderObj order)
        {
            OrderStatus statusObj = null;
            Order buyObj = null;
            string result = "";
            //-- Just Log Action
            if (bool.Parse(strategyInfo.paramDic["JustLog"]))
            {
                String orderId = "1000000";
                decimal buyPrice = decimal.Parse(order.Price) +
                    decimal.Parse(strategyInfo.paramDic["BuyGapAmount"]);
                decimal amount = decimal.Parse(order.Amount);
                decimal quantity = Math.Round(amount / buyPrice, 6);
                statusObj = new OrderStatus();
                statusObj.price = buyPrice.ToString();
                statusObj.origQty = quantity.ToString();
                statusObj.orderId = orderId;
                ServiceInfo.LogStrategyAction(strategyInfo, "BUY", strategyInfo.name + ", Reach Buy condition, Price=" + buyPrice +
                            ", Quantity=" + quantity + ", OrderId=" + orderId);
                return (statusObj);
            }
            //-- Real Action
            while (true)
            {
                //-- Get Current Price
                result = BinanceAPI.GetJson("v3/ticker/price", "symbol=" + strategyInfo.paramDic["PairCoins"]);
                Price priceObj = JsonConvert.DeserializeObject<Price>(result);
                ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ",Ready to Buy Current Price for " +
                    strategyInfo.paramDic["PairCoins"] + "=" + priceObj.price);
                //-- Create Order
                decimal buyPrice = decimal.Parse(priceObj.price) +
                    decimal.Parse(strategyInfo.paramDic["BuyGapAmount"]);
                decimal amount = decimal.Parse(order.Amount);
                decimal quantity = Math.Round(amount / buyPrice, 6);
                result = BinanceAPI.PostSignedJson("v3/order", "symbol=" + strategyInfo.paramDic["PairCoins"] + "&" +
                    "side=BUY" + "&" + "type=LIMIT" + "&" +
                    "quantity=" + quantity + "&" +
                    "price=" + buyPrice + "&" + "timeInForce=GTC" + "&" + "recvWindow=6000");
                ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ",Buy Transaction Source for " +
                    strategyInfo.paramDic["PairCoins"] + "=" + result);
                buyObj = JsonConvert.DeserializeObject<Order>(result);
                ServiceInfo.LogStrategyAction(strategyInfo, "BUY", strategyInfo.name + ", Reach Buy condition, Price=" + buyObj.price +
                            ", Quantity=" + buyObj.origQty + ", OrderId=" + buyObj.orderId);
                //-- Check Order State (n Times)
                int counter = 0;
                while (counter <= 5)
                {
                    try
                    {
                        //-- Wait for (Wait*Schedule) second
                        ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ", Counter=" + counter.ToString() +
                            ", Wait for Buy Result");
                        strategyInfo.mre.WaitOne(int.Parse(strategyInfo.paramDic["Wait"]) *
                            int.Parse(strategyInfo.paramDic["ScheduleTime"]) * 1000);
                        strategyInfo.mre.Reset();
                        result = BinanceAPI.GetSignedJson("v3/order", "symbol=" + strategyInfo.paramDic["PairCoins"] +
                            "&" + "orderId=" + buyObj.orderId);
                        statusObj = JsonConvert.DeserializeObject<OrderStatus>(result);
                        break;
                    }
                    catch (Exception ex)
                    {
                        counter++;
                        ServiceInfo.LogStrategyAction(strategyInfo, "Error", strategyInfo.name + ", Counter=" + counter.ToString() + 
                            ", " + ex.Message);
                    }
                }
                if (statusObj.status == "FILLED")
                {
                    ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ", Order Filled, Price=" + statusObj.price +
                            ", Quantity=" + statusObj.origQty + ", OrderId=" + statusObj.orderId);
                    break;
                }
                else
                {
                    try
                    {
                        ServiceInfo.LogStrategyAction(strategyInfo, "CANCEL_BUY", strategyInfo.name + ", Buy Order Not Filled, Price=" + statusObj.price +
                                ", Quantity=" + statusObj.origQty + ", OrderId=" + statusObj.orderId);
                        result = BinanceAPI.DeleteSignedJson("v3/order", "symbol=" + strategyInfo.paramDic["PairCoins"] + "&" +
                            "orderId=" + buyObj.orderId);
                    }
                    catch (Exception ex)
                    {
                        ServiceInfo.LogStrategyAction(strategyInfo, "Error", strategyInfo.name + ", " + ex.Message);
                        strategyInfo.mre.WaitOne(int.Parse(strategyInfo.paramDic["ScheduleTime"]) * 1000);
                        strategyInfo.mre.Reset();
                    }
                }
            }
            return (statusObj);
        }

        private OrderStatus SellUntilSuccess(OrderStatus originalTran)
        {
            OrderStatus statusObj = null;
            Order sellObj = null;
            string result = "";
            //-- Just Log Action
            if (bool.Parse(strategyInfo.paramDic["JustLog"]))
            {
                //-- Get Current Price
                result = BinanceAPI.GetJson("v3/ticker/price", "symbol=" + strategyInfo.paramDic["PairCoins"]);
                Price priceObj = JsonConvert.DeserializeObject<Price>(result);
                //-- Create Result
                String orderId = "2000000";
                decimal sellPrice = decimal.Parse(priceObj.price) -
                    decimal.Parse(strategyInfo.paramDic["SellGapAmount"]);
                decimal amount = sellPrice * decimal.Parse(originalTran.origQty);
                decimal quantity = decimal.Parse(originalTran.origQty);
                statusObj = new OrderStatus();
                statusObj.price = sellPrice.ToString();
                statusObj.origQty = quantity.ToString();
                statusObj.orderId = orderId;
                ServiceInfo.LogStrategyAction(strategyInfo, "SELL", strategyInfo.name + ", Reach Sell condition, Price=" + sellPrice +
                            ", Quantity=" + quantity + ", OrderId=" + orderId + ", Remained Amount=" + amount);
                return (statusObj);
            }
            //-- Real Action
            while (true)
            {
                //-- Get Current Price
                result = BinanceAPI.GetJson("v3/ticker/price", "symbol=" + strategyInfo.paramDic["PairCoins"]);
                Price priceObj = JsonConvert.DeserializeObject<Price>(result);
                ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ",Ready to Buy Current Price for " +
                    strategyInfo.paramDic["PairCoins"] + "=" + priceObj.price);
                //-- Create Order
                decimal sellPrice = decimal.Parse(priceObj.price) -
                    decimal.Parse(strategyInfo.paramDic["SellGapAmount"]);
                result = BinanceAPI.PostSignedJson("v3/order", "symbol=" + strategyInfo.paramDic["PairCoins"] + "&" +
                    "side=SELL" + "&" + "type=LIMIT" + "&" +
                    "quantity=" + Math.Round(decimal.Parse(originalTran.origQty), 6).ToString() + "&" +
                    "price=" + sellPrice + "&" + "timeInForce=GTC" + "&" + "recvWindow=6000");
                ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ",Sell Transaction Source for " +
                    strategyInfo.paramDic["PairCoins"] + "=" + result);
                sellObj = JsonConvert.DeserializeObject<Order>(result);
                ServiceInfo.LogStrategyAction(strategyInfo, "SELL", strategyInfo.name + ", Reach Sell condition, Price=" + sellObj.price +
                            ", Quantity=" + sellObj.origQty + ", OrderId=" + sellObj.orderId);
                //-- Check Order State
                int counter = 0;
                while (counter <= 5)
                {
                    try
                    {
                        //-- Wait for (Wait*Schedule) second
                        ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ", Counter=" + counter.ToString() +
                            ", Wait for Sell Result");
                        strategyInfo.mre.WaitOne(int.Parse(strategyInfo.paramDic["Wait"]) *
                            int.Parse(strategyInfo.paramDic["ScheduleTime"]) * 1000);
                        strategyInfo.mre.Reset();
                        result = BinanceAPI.GetSignedJson("v3/order", "symbol=" + strategyInfo.paramDic["PairCoins"] +
                            "&" + "orderId=" + sellObj.orderId);
                        statusObj = JsonConvert.DeserializeObject<OrderStatus>(result);
                        break;
                    }
                    catch (Exception ex)
                    {
                        counter++;
                        ServiceInfo.LogStrategyAction(strategyInfo, "Error", strategyInfo.name + ", Counter=" + counter.ToString() +
                            ", " + ex.Message);
                    }
                }
                if (statusObj.status == "FILLED")
                {
                    ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ", Order Filled, Price=" + statusObj.price +
                            ", Quantity=" + statusObj.origQty + ", OrderId=" + statusObj.orderId);
                    break;
                }
                else
                {
                    try
                    {
                        ServiceInfo.LogStrategyAction(strategyInfo, "CANCEL_SELL", strategyInfo.name + ", Sell Order Not Filled, Price=" + statusObj.price +
                                ", Quantity=" + statusObj.origQty + ", OrderId=" + statusObj.orderId);
                        result = BinanceAPI.DeleteSignedJson("v3/order", "symbol=" + strategyInfo.paramDic["PairCoins"] + "&" +
                            "orderId=" + sellObj.orderId);
                    }
                    catch (Exception ex)
                    {
                        ServiceInfo.LogStrategyAction(strategyInfo, "Error", strategyInfo.name + ", " + ex.Message);
                        strategyInfo.mre.WaitOne(int.Parse(strategyInfo.paramDic["ScheduleTime"]) * 1000);
                        strategyInfo.mre.Reset();
                    }
                }
            }
            return (statusObj);
        }

        private OrderStatus SellOrder(OrderStatus originalTran)
        {
            OrderStatus statusObj = null;
            Order sellObj = null;
            decimal sellPrice = 0;
            string result = "";
            //-- Just Log Action
            if (bool.Parse(strategyInfo.paramDic["JustLog"]))
            {
                //-- Create Result
                String orderId = "2000000";
                sellPrice = decimal.Parse(originalTran.price) +
                    decimal.Parse(strategyInfo.paramDic["HigherPriceAmount"]);
                decimal amount = sellPrice * decimal.Parse(originalTran.origQty);
                decimal quantity = decimal.Parse(originalTran.origQty);
                statusObj = new OrderStatus();
                statusObj.price = sellPrice.ToString();
                statusObj.origQty = quantity.ToString();
                statusObj.orderId = orderId;
                ServiceInfo.LogStrategyAction(strategyInfo, "SELL", strategyInfo.name + ", Reach Sell condition - Set Order with Higer Price insted Of Loss, Price=" + sellPrice +
                            ", Quantity=" + quantity + ", OrderId=" + orderId + ", Remained Amount=" + amount);
                return (statusObj);
            }
            //-- Create Order
            sellPrice = decimal.Parse(originalTran.price) +
                decimal.Parse(strategyInfo.paramDic["HigherPriceAmount"]);
            result = BinanceAPI.PostSignedJson("v3/order", "symbol=" + strategyInfo.paramDic["PairCoins"] + "&" +
                "side=SELL" + "&" + "type=LIMIT" + "&" +
                "quantity=" + Math.Round(decimal.Parse(originalTran.origQty), 6).ToString() + "&" +
                "price=" + sellPrice + "&" + "timeInForce=GTC" + "&" + "recvWindow=6000");
            sellObj = JsonConvert.DeserializeObject<Order>(result);
            ServiceInfo.LogStrategyAction(strategyInfo, "SELL", strategyInfo.name + ", Reach Sell condition - Set Order with Higer Price insted Of Loss, Price=" + sellObj.price +
                        ", Quantity=" + sellObj.origQty + ", OrderId=" + sellObj.orderId);
            //-- Wait for (Wait*Schedule) second
            ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ", Wait for Sell Result");
            strategyInfo.mre.WaitOne(int.Parse(strategyInfo.paramDic["Wait"]) *
                int.Parse(strategyInfo.paramDic["ScheduleTime"]) * 1000);
            strategyInfo.mre.Reset();
            return (statusObj);
        }

        private void CreateOrders()
        {
            BinanceAPI.key = strategyInfo.paramDic["Key"];
            BinanceAPI.secret = strategyInfo.paramDic["Secret"];
            lstOrders = new List<WatchOrderObj>();
            string[] splited = strategyInfo.paramDic["Orders"].Split(';');
            int id = 1;
            foreach (string str in splited)
            {
                string[] item = str.Split('_');
                WatchOrderObj obj = new WatchOrderObj();
                obj.OrderNo = id.ToString();
                obj.Amount = item[0];
                obj.Price = item[1];
                obj.Status = "Created";
                obj.LossProfit = null;
                lstOrders.Add(obj);
                id++;
            }
        }

        private bool BuyFeasibility()
        {
            bool result = true;
            int runnigCount = 0;
            foreach (WatchOrderObj obj in lstOrders)
            {
                if (obj.Status == "Created" || obj.Status == "Sold")
                    continue;
                runnigCount++;
            }
            if (runnigCount >= int.Parse(strategyInfo.paramDic["MaxActive"]))
            {
                result = false;
                ServiceInfo.LogStrategyAction(strategyInfo, "FEASIBILITY", strategyInfo.name + ",Buy Feasibility Result for " +
                            strategyInfo.paramDic["PairCoins"] + 
                            ", is Fulse, Max Active Order can be at most " +
                            strategyInfo.paramDic["MaxActive"]);
            }
            if (bool.Parse(strategyInfo.paramDic["ControlLossCount"]) && 
                LossCount >= int.Parse(strategyInfo.paramDic["LossCount"]))
            {
                result = false;
                ServiceInfo.LogStrategyAction(strategyInfo, "FEASIBILITY", strategyInfo.name + ",Buy Feasibility Result for " +
                            strategyInfo.paramDic["PairCoins"] +
                            ", is Fulse, Max Loss Count can be at most " +
                            strategyInfo.paramDic["LossCount"]);
            }
            return (result);
        }

        private void Reload()
        {
            //-- Get Current Price
            string result = BinanceAPI.GetJson("v3/ticker/price", "symbol=" + strategyInfo.paramDic["PairCoins"]);
            Price priceObj = JsonConvert.DeserializeObject<Price>(result);
            //-- Check Need for Reorder
            decimal priceDif = 0;
            foreach (WatchOrderObj obj in lstOrders)
            {
                if (obj.Status == "Created")
                {
                    priceDif = decimal.Parse(priceObj.price) -
                        decimal.Parse(obj.Price);
                    break;
                }
            }
            if (lstOrders.Count != 0 &&
                priceDif < decimal.Parse(strategyInfo.paramDic["AutoOrderLockPeriod"]))
            {
                ServiceInfo.LogStrategyAction(strategyInfo, "ReOrderNotMatch", strategyInfo.name + ", REORDER Not Match, Difference Value=" +
                priceDif);
                return;
            }
            //-- Get 24h-5m Candles
            result = BinanceAPI.GetJson("v3/klines", "symbol=" + strategyInfo.paramDic["PairCoins"] + "&" +
                                    "interval=5m" + "&" + "limit=288");
            string[][] objQuarter = JsonConvert.DeserializeObject<string[][]>(result);
            List<decimal> lstPrice = new List<decimal>();
            foreach (string[] item in objQuarter)
            {
                lstPrice.Add(Math.Round(decimal.Parse(item[1]) / decimal.Parse(strategyInfo.paramDic["AutoOrderCoefficient"])) * decimal.Parse(strategyInfo.paramDic["AutoOrderCoefficient"]));
                lstPrice.Add(Math.Round(decimal.Parse(item[4]) / decimal.Parse(strategyInfo.paramDic["AutoOrderCoefficient"])) * decimal.Parse(strategyInfo.paramDic["AutoOrderCoefficient"]));
            }
            lstPrice.Sort();
            //-- Create Sorted Dictionary
            Dictionary<decimal, int> dicPrice = new Dictionary<decimal, int>();
            for (int i = lstPrice.Count - 1; i >= 0; i--)
            {
                try
                {
                    dicPrice.Add(lstPrice[i], 1);
                }
                catch
                {
                    dicPrice[lstPrice[i]]++;
                }
            }
            //-- Current Price Position
            List<decimal> lstSearch = dicPrice.Keys.ToList();
            int position = lstSearch.Count;
            for (int i = 0; i < lstSearch.Count; i++)
            {
                if (decimal.Parse(priceObj.price) > lstSearch[i])
                {
                    position = i;
                    break;
                }
            }
            //-- Clear and ReCreate Orders
            string orderStr = "";
            int OrderCount = 0;
            decimal lastPrice = decimal.Parse(priceObj.price);
            for (int i = position; i < lstSearch.Count; i++)
            {
                if (OrderCount >= int.Parse(strategyInfo.paramDic["AutoOrderCount"]))
                    break;
                if ((lstSearch[position] - lstSearch[i]) > 
                    decimal.Parse(strategyInfo.paramDic["AutoOrderWatchPeriod"]))
                {
                    if (orderStr != "")
                        orderStr += ";";
                    orderStr += strategyInfo.paramDic["AutoOrderAmount"] + "_" + 
                        lstSearch[i];
                    lastPrice = lstSearch[i];
                    position = i;
                    OrderCount++;
                }
            }
            while (OrderCount < int.Parse(strategyInfo.paramDic["AutoOrderCount"]))
            {
                if (orderStr != "")
                    orderStr += ";";
                orderStr += strategyInfo.paramDic["AutoOrderAmount"] + "_" +
                    (Math.Round(lastPrice / decimal.Parse(strategyInfo.paramDic["AutoOrderCoefficient"])) * decimal.Parse(strategyInfo.paramDic["AutoOrderCoefficient"]) - 
                    decimal.Parse(strategyInfo.paramDic["AutoOrderWatchPeriod"]));
                lastPrice = lastPrice - (decimal.Parse(strategyInfo.paramDic["AutoOrderWatchPeriod"]));
                OrderCount++;
            }
            strategyInfo.paramDic["Orders"] = orderStr;
            lstOrders.Clear();
            CreateOrders();
            ServiceInfo.LogStrategyAction(strategyInfo, "ReOrder", strategyInfo.name + ", REORDER Successfully, Value=" +
                orderStr);
        }

        public override void DoJob()
        {
            string result = "";
            int counter = 0;
            bool canReload = true;
            decimal totalProfit = 0;
            decimal totalLoss = 0;
            int registredOrder = 0;
            while (true)
            {
                try
                {
                    canReload = true;
                    //-- Get Current Price
                    result = BinanceAPI.GetJson("v3/ticker/price", "symbol=" + strategyInfo.paramDic["PairCoins"]);
                    Price priceObj = JsonConvert.DeserializeObject<Price>(result);
                    ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ", Buy Check Current Price for " +
                        strategyInfo.paramDic["PairCoins"] + "=" + priceObj.price);
                    //-- Check and do action  for each order in order list
                    foreach (WatchOrderObj order in lstOrders)
                    {
                        decimal difference = decimal.Parse(priceObj.price) - decimal.Parse(order.Price);
                        switch (order.Status)
                        {
                            case "Created":
                                //-- Ready to Buy
                                if (difference < 0 &&
                                    BuyFeasibility() &&
                                    Math.Abs(difference) > (30 * decimal.Parse(strategyInfo.paramDic["LowBuyWatchPeriod"]) / 100))
                                {
                                    order.Status = "ReadyToBuy";
                                    ServiceInfo.LogStrategyAction(strategyInfo, "READY", strategyInfo.name + ", Ready to Buy for" +
                                        strategyInfo.paramDic["PairCoins"] + ", Price=" + priceObj.price +
                                        ", Difference to Target=" + difference +
                                        ", Order No=" + order.OrderNo);
                                    canReload = false;
                                }
                                break;
                            case "ReadyToBuy":
                                canReload = false;
                                //-- Change Buy Price Point
                                if (difference < 0 &&
                                    Math.Abs(difference) > (decimal.Parse("1.2") * decimal.Parse(strategyInfo.paramDic["LowBuyWatchPeriod"])))
                                {
                                    order.Price = (decimal.Parse(order.Price) -
                                        decimal.Parse(strategyInfo.paramDic["LowBuyWatchPeriod"])).ToString();
                                    ServiceInfo.LogStrategyAction(strategyInfo, "CHANGE", strategyInfo.name + ", Change Buy Price Point for " +
                                        strategyInfo.paramDic["PairCoins"] + ", Price=" + priceObj.price +
                                        ", Target Changed to=" + order.Price +
                                        ", Order No=" + order.OrderNo);
                                }
                                //-- Buy Order Reached go to Sell
                                else if (difference > 0 &&
                                    difference > (20 * decimal.Parse(strategyInfo.paramDic["HighSellWatchPeriod"]) / 100))
                                {
                                    order.BuyStatusObj = BuyUntilSuccess(order);
                                    order.Status = "ReadyToSell";
                                    ServiceInfo.LogStrategyAction(strategyInfo, "READY", strategyInfo.name + ", Ready to Sell for " +
                                        strategyInfo.paramDic["PairCoins"] + ", Price=" + priceObj.price +
                                        ", Difference to Target=" + difference +
                                        ", Order No=" + order.OrderNo);
                                }
                                break;
                            case "ReadyToSell":
                                canReload = false;
                                decimal currentLossProfit = (decimal.Parse(priceObj.price) * decimal.Parse(order.BuyStatusObj.origQty)) -
                                        (decimal.Parse(order.BuyStatusObj.price) * decimal.Parse(order.BuyStatusObj.origQty)) -
                                        (decimal.Parse("1.3") * decimal.Parse(order.Amount) * decimal.Parse(strategyInfo.paramDic["FeeRate"]));
                                //-- Prevent Loss
                                if (difference < 0 &&
                                    Math.Abs(currentLossProfit) > decimal.Parse(strategyInfo.paramDic["AcceptableLossLimit"]))
                                {
                                    if (bool.Parse(strategyInfo.paramDic["OrderInstedLoss"]) == false)
                                    {
                                        order.SellStatusObj = SellUntilSuccess(order.BuyStatusObj);
                                        order.LossProfit = currentLossProfit.ToString();
                                        totalLoss += Math.Abs(currentLossProfit);
                                        //-----------------------Notification
                                        string subject = "TraidingNotification-Loss-" + strategyInfo.name;
                                        string body = "Strategy '" + strategyInfo.name + "' Loss At: " + DateTime.Now.ToString() + "\r\n\r\n" +
                                            "Loss Limit Reached for " + strategyInfo.paramDic["PairCoins"] + "\r\n" +
                                            "Loss Amount = " + order.LossProfit + "\r\n" +
                                            "Total Loss = " + totalLoss + "\r\n" +
                                            "Order No = " + order.OrderNo + "\r\n" +
                                            "Order Quantity = " + order.BuyStatusObj.origQty;
                                        List<string> parameters = new List<string>();
                                        parameters.Add("EMAIL");
                                        parameters.Add(strategyInfo.paramDic["EmailList"]);
                                        parameters.Add(subject);
                                        parameters.Add(body);
                                        strategyInfo.notification.QueueAction(parameters, "ENQUEUE");
                                        //-----------------------------------
                                    }
                                    else
                                    {
                                        order.SellStatusObj = SellOrder(order.BuyStatusObj);
                                        order.LossProfit = "0";
                                        registredOrder++;
                                        //-----------------------Notification
                                        string subject = "TraidingNotification-Order Insted Loss-" + strategyInfo.name;
                                        string body = "Strategy '" + strategyInfo.name + "' Order Insted Loss At: " + DateTime.Now.ToString() + "\r\n\r\n" +
                                            "Loss Limit Reached for " + strategyInfo.paramDic["PairCoins"] + "\r\n" +
                                            "Loss Amount = " + order.LossProfit + "\r\n" +
                                            "Order No = " + order.OrderNo + "\r\n" +
                                            "Order Quantity = " + order.BuyStatusObj.origQty + "\r\n" +
                                            "Reorder Price = " + (decimal.Parse(order.BuyStatusObj.price) +
                                                decimal.Parse(strategyInfo.paramDic["HigherPriceAmount"])).ToString();
                                        List < string > parameters = new List<string>();
                                        parameters.Add("EMAIL");
                                        parameters.Add(strategyInfo.paramDic["EmailList"]);
                                        parameters.Add(subject);
                                        parameters.Add(body);
                                        strategyInfo.notification.QueueAction(parameters, "ENQUEUE");
                                        //-----------------------------------
                                    }
                                    order.Status = "Sold";
                                    LossCount++;
                                    ServiceInfo.LogStrategyAction(strategyInfo, "LOSS", strategyInfo.name + ",Loss Limit Reached for " +
                                        strategyInfo.paramDic["PairCoins"] + " ,Price=" + order.SellStatusObj.price +
                                        ", Loss Amount=" + order.LossProfit +
                                        ", Order No=" + order.OrderNo);
                                }
                                //-- Change Sell Profit Limit and Ready to Sell
                                else if (order.LossProfit == null && 
                                    difference > 0 && 
                                    currentLossProfit > (decimal.Parse("1.2") * decimal.Parse(strategyInfo.paramDic["AcceptableProfitLimit"])))
                                {
                                    order.LossProfit = strategyInfo.paramDic["AcceptableProfitLimit"];
                                    ServiceInfo.LogStrategyAction(strategyInfo, "CHANGE", strategyInfo.name + ",Change Sell Profit Limit for " +
                                        strategyInfo.paramDic["PairCoins"] + " ,Price=" + priceObj.price +
                                        ", Profit Changed to=" + order.LossProfit +
                                        ", Order No=" + order.OrderNo);
                                }
                                //-- Sell and Save Profit
                                else if (order.LossProfit != null &&
                                    currentLossProfit < decimal.Parse(order.LossProfit))
                                {
                                    order.SellStatusObj = SellUntilSuccess(order.BuyStatusObj);
                                    order.LossProfit = currentLossProfit.ToString();
                                    order.Status = "Sold";
                                    totalProfit += currentLossProfit;
                                    //-----------------------Notification
                                    string subject = "TraidingNotification-Profit-" + strategyInfo.name;
                                    string body = "Strategy '" + strategyInfo.name + "' Profit At: " + DateTime.Now.ToString() + "\r\n\r\n" +
                                        "Profit Limit Reached for " + strategyInfo.paramDic["PairCoins"] + "\r\n" +
                                        "Profit Amount = " + order.LossProfit + "\r\n" +
                                        "Total Profit = " + totalProfit + "\r\n" +
                                        "Order No = " + order.OrderNo + "\r\n" +
                                        "Order Quantity = " + order.BuyStatusObj.origQty;
                                    List<string> parameters = new List<string>();
                                    parameters.Add("EMAIL");
                                    parameters.Add(strategyInfo.paramDic["EmailList"]);
                                    parameters.Add(subject);
                                    parameters.Add(body);
                                    strategyInfo.notification.QueueAction(parameters, "ENQUEUE");
                                    //-----------------------------------
                                    ServiceInfo.LogStrategyAction(strategyInfo, "PROFIT", strategyInfo.name + ",Profit Limit Reached for " +
                                        strategyInfo.paramDic["PairCoins"] + " ,Price=" + order.SellStatusObj.price +
                                        ", Profit Amount=" + currentLossProfit +
                                        ", Order No=" + order.OrderNo);
                                }
                                //-- Change again Sell Profit Limit
                                else if (order.LossProfit != null &&
                                    currentLossProfit > (decimal.Parse(order.LossProfit) + (decimal.Parse("0.7") * decimal.Parse(strategyInfo.paramDic["AcceptableProfitLimit"]))))
                                {
                                    order.LossProfit = (decimal.Parse(order.LossProfit) + (decimal.Parse("0.5") * decimal.Parse(strategyInfo.paramDic["AcceptableProfitLimit"]))).ToString();
                                    ServiceInfo.LogStrategyAction(strategyInfo, "CHANGE", strategyInfo.name + ",Change Sell Profit Limit for " +
                                        strategyInfo.paramDic["PairCoins"] + " ,Price=" + priceObj.price +
                                        ", Profit Changed to=" + order.LossProfit +
                                        ", Order No=" + order.OrderNo);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    //-- Monitoring Orders
                    foreach (WatchOrderObj order in lstOrders)
                    {
                        string buyPrice = "";
                        string sellPrice = "";
                        try
                        {
                            buyPrice = order.BuyStatusObj.price;
                        }
                        catch
                        {}
                        try
                        {
                            sellPrice = order.SellStatusObj.price;
                        }
                        catch
                        { }
                        ServiceInfo.LogStrategyAction(strategyInfo, "Info", " OrderNo=" + order.OrderNo +
                            ", Amount=" + order.Amount +
                            ", Inital Price=" + order.Price +
                            ", Status=" + order.Status +
                            ", Buy Price=" + buyPrice +
                            ", Sell Price=" + sellPrice + 
                            ", Loss Profit=" + order.LossProfit);
                    }
                    //-- Wait for next loop
                    ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ", Next Cycle Loop"
                        + ", Total Profit=" + totalProfit
                        + ", Total Loss=" + totalLoss
                        + ", Order Count=" + registredOrder);
                    strategyInfo.mre.WaitOne(int.Parse(strategyInfo.paramDic["ScheduleTime"]) * 1000);
                    counter++;
                    if (bool.Parse(strategyInfo.paramDic["AutoReload"]) && canReload && 
                        counter > int.Parse(strategyInfo.paramDic["AutoReloadCount"]))
                    {
                        Reload();
                        counter = 0;
                    }
                    strategyInfo.mre.Reset();
                }
                catch (Exception ex)
                {
                    ServiceInfo.LogStrategyAction(strategyInfo, "Error", strategyInfo.name + ", " + ex.Message);
                    strategyInfo.mre.WaitOne(int.Parse(strategyInfo.paramDic["ScheduleTime"]) * 1000);
                    strategyInfo.mre.Reset();
                }
            }
        }

        public override List<string> GetParams()
        {
            List<string> parameters = new List<string>();
            parameters.Add("PairCoins");
            parameters.Add("FeeRate");
            parameters.Add("BuyGapAmount");
            parameters.Add("SellGapAmount");
            parameters.Add("ScheduleTime");
            parameters.Add("HighSellWatchPeriod");
            parameters.Add("LowBuyWatchPeriod");
            parameters.Add("AcceptableProfitLimit");
            parameters.Add("AcceptableLossLimit");
            parameters.Add("Orders");
            parameters.Add("Wait");
            parameters.Add("MaxActive");
            parameters.Add("AutoReload");
            parameters.Add("AutoReloadCount");
            parameters.Add("AutoOrderCount");
            parameters.Add("AutoOrderAmount");
            parameters.Add("AutoOrderWatchPeriod");
            parameters.Add("AutoOrderLockPeriod");
            parameters.Add("AutoOrderCoefficient");
            parameters.Add("OrderInstedLoss");
            parameters.Add("HigherPriceAmount");
            parameters.Add("ControlLossCount");
            parameters.Add("LossCount");
            parameters.Add("Key");
            parameters.Add("Secret");
            parameters.Add("ProfitLossNotification");
            parameters.Add("PeriodicalNotification");
            parameters.Add("PeriodicalNotificationSchedule");
            parameters.Add("EmailList");
            parameters.Add("MobileDelivery");
            parameters.Add("SendEmail");
            parameters.Add("SendSMS");
            parameters.Add("SendNotification");
            return (parameters);
        }

    }
}
