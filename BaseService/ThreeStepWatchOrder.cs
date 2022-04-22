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
    public class ThreeStepWatchOrder : BaseStrategy
    {

        private int lossCounter = 0;

        public ThreeStepWatchOrder(Dictionary<string, string> parameters, string name, string basePath) : base(parameters, name, basePath)
        {

        }

        public override void Init()
        {
            BinanceAPI.key = strategyInfo.paramDic["Key"];
            BinanceAPI.secret = strategyInfo.paramDic["Secret"];
            lossCounter = int.Parse(strategyInfo.paramDic["StartLossCount"]);
            //-- Set Buy Price
            if (bool.Parse(strategyInfo.paramDic["AutoReload"]) == true)
            {
                string result = BinanceAPI.GetJson("v3/ticker/price", "symbol=" + strategyInfo.paramDic["PairCoins"]);
                Price priceObj = JsonConvert.DeserializeObject<Price>(result);
                strategyInfo.paramDic["BuyPrice"] = (decimal.Parse(priceObj.price) +
                                    decimal.Parse(strategyInfo.paramDic["ReloadGap"])).ToString();
            }
            ServiceInfo.LogStrategyAction(strategyInfo, "Initialize", strategyInfo.name + ", Initialized");
        }

        private OrderStatus BuyUntilSuccess()
        {
            OrderStatus statusObj = null;
            Order buyObj = null;
            string result = "";
            //-- Just Log Action
            if (bool.Parse(strategyInfo.paramDic["JustLog"]))
            {
                String orderId = "1000000";
                decimal buyPrice = decimal.Parse(strategyInfo.paramDic["BuyPrice"]) +
                    decimal.Parse(strategyInfo.paramDic["BuyGapAmount"]);
                decimal amount = decimal.Parse(strategyInfo.paramDic["Amount"]);
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
                decimal amount = decimal.Parse(strategyInfo.paramDic["Amount"]);
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
                //-- Wait for 2 second
                ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ", Wait for Buy Result");
                strategyInfo.mre.WaitOne(int.Parse(strategyInfo.paramDic["ScheduleTime"]) * 1000);
                strategyInfo.mre.Reset();
                //-- Check Order State
                result = BinanceAPI.GetSignedJson("v3/order", "symbol=" + strategyInfo.paramDic["PairCoins"] + 
                    "&" + "orderId=" + buyObj.orderId);
                statusObj = JsonConvert.DeserializeObject<OrderStatus>(result);
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
                    catch(Exception ex)
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
                //-- Wait for 2 second
                ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ", Wait for Sell Result");
                strategyInfo.mre.WaitOne(int.Parse(strategyInfo.paramDic["ScheduleTime"]) * 1000);
                strategyInfo.mre.Reset();
                //-- Check Order State
                result = BinanceAPI.GetSignedJson("v3/order", "symbol=" + strategyInfo.paramDic["PairCoins"] +
                    "&" + "orderId=" + sellObj.orderId);
                statusObj = JsonConvert.DeserializeObject<OrderStatus>(result);
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

        private bool MarketProcessFeasibility(decimal currentPrice)
        {
            bool returnVal = false;
            string result = BinanceAPI.GetJson("v3/klines", "symbol=" + strategyInfo.paramDic["PairCoins"] + "&" +
                                "interval=1m" + "&" + "limit=" + int.Parse(strategyInfo.paramDic["TimeWindowQuarter"]));
            string[][] objQuarter = JsonConvert.DeserializeObject<string[][]>(result);
            decimal quarterNeg = 0;
            decimal quarterPos = 0;
            //-- calculate ratio for quarter candels
            decimal dif = 0;
            foreach (string[] item in objQuarter)
            {
                dif = decimal.Parse(item[4]) - decimal.Parse(item[1]);
                if (dif > 0)
                    quarterPos += dif;
                if (dif < 0)
                    quarterNeg += Math.Abs(dif);
            }
            decimal positiveNegetiveRatio = quarterPos / (quarterNeg + quarterPos);
            if (positiveNegetiveRatio >= decimal.Parse(strategyInfo.paramDic["PositiveNegetiveRatio"]))
            {
                returnVal = true;
            }
            if (positiveNegetiveRatio <= decimal.Parse(strategyInfo.paramDic["NegetivePositiveRatio"]))
            {
                returnVal = true;
            }
            if (quarterPos + quarterNeg < decimal.Parse(strategyInfo.paramDic["AccumulationIgnore"]))
            {
                returnVal = false;
                ServiceInfo.LogStrategyAction(strategyInfo, "FEASIBILITY", strategyInfo.name + ",Accumulation Ignore " +
                            strategyInfo.paramDic["PairCoins"] + "=" + returnVal.ToString() +
                            ", Value=" + quarterPos + quarterNeg + " < Limit=" + strategyInfo.paramDic["AccumulationIgnore"]);
            }
            if (dif < 0)
            {
                returnVal = false;
                ServiceInfo.LogStrategyAction(strategyInfo, "FEASIBILITY", strategyInfo.name + ",Last Candle is not positive " +
                            strategyInfo.paramDic["PairCoins"] + "=" + returnVal.ToString() +
                            ", Candle Value=" + dif);
            }
            ServiceInfo.LogStrategyAction(strategyInfo, "FEASIBILITY", strategyInfo.name + ",Feasibility Result for " +
                            strategyInfo.paramDic["PairCoins"] + "=" + returnVal.ToString() +
                            ", positive Negetive Ratio=" + positiveNegetiveRatio);
            //-- Check price position in current 3h market
            //if (returnVal)
            //{
            //    result = BinanceAPI.GetJson("v3/klines", "symbol=" + strategyInfo.paramDic["PairCoins"] + "&" +
            //                        "interval=15m" + "&" + "limit=12");
            //    objQuarter = JsonConvert.DeserializeObject<string[][]>(result);
            //    List<decimal> lstPrice = new List<decimal>();
            //    foreach (string[] item in objQuarter)
            //    {
            //        lstPrice.Add(decimal.Parse(item[1]));
            //        lstPrice.Add(decimal.Parse(item[4]));
            //    }
            //    lstPrice.Sort();
            //    int position = lstPrice.Count;
            //    for (int i = 0; i < lstPrice.Count; i++)
            //    {
            //        if (currentPrice < lstPrice[i])
            //        {
            //            position = i + 1;
            //            break;
            //        }
            //    }
            //    if (position > 12)
            //    {
            //        returnVal = false;
            //        ServiceInfo.LogStrategyAction(strategyInfo, "FEASIBILITY", strategyInfo.name + ",Current Price for " +
            //                strategyInfo.paramDic["PairCoins"] + "=" + returnVal.ToString() +
            //                ", is above half in current 3 hour, price=" + currentPrice);
            //    }
            //}
            //-- Check market for recent Loss count (3*Quarter)
            if (lossCounter >= int.Parse(strategyInfo.paramDic["MaxLossCount"]) && returnVal)
            {
                returnVal = false;
                result = BinanceAPI.GetJson("v3/klines", "symbol=" + strategyInfo.paramDic["PairCoins"] + "&" +
                                "interval=1m" + "&" + "limit=" + int.Parse(strategyInfo.paramDic["TimeWindowQuarter"]) * 5);
                objQuarter = JsonConvert.DeserializeObject<string[][]>(result);
                quarterNeg = 0;
                quarterPos = 0;
                foreach (string[] item in objQuarter)
                {
                    dif = decimal.Parse(item[4]) - decimal.Parse(item[1]);
                    if (dif > 0)
                        quarterPos += dif;
                    if (dif < 0)
                        quarterNeg += Math.Abs(dif);
                }
                positiveNegetiveRatio = quarterPos / (quarterNeg + quarterPos);
                if (positiveNegetiveRatio >= decimal.Parse(strategyInfo.paramDic["LossPositiveNegetiveRatio"]))
                {
                    returnVal = true;
                }
                ServiceInfo.LogStrategyAction(strategyInfo, "FEASIBILITY", strategyInfo.name + ",Feasibility(*3 Time) Result for " +
                            strategyInfo.paramDic["PairCoins"] + "=" + returnVal.ToString() +
                            ", positive Negetive Ratio=" + positiveNegetiveRatio);
            }
            return (returnVal);
        }

        public override void DoJob()
        {
            ServiceInfo.LogStrategyAction(strategyInfo, "Start Thread", strategyInfo.name + ", Started");
            string state = "BUY";
            bool readyToSell = false;
            string result = "";
            bool readyToBuy = false;
            decimal buyPrice = 0;
            decimal maxReachedPrice = 0;
            decimal totalProfit = 0;
            decimal totalLoss = 0;
            OrderStatus statusObj = null;
            OrderStatus sellStatusObj = null;
            while (true)
            {
                try
                {
                    if (state == "BUY")
                    {
                        //-- Get Current Price
                        result = BinanceAPI.GetJson("v3/ticker/price", "symbol=" + strategyInfo.paramDic["PairCoins"]);
                        Price priceObj = JsonConvert.DeserializeObject<Price>(result);
                        decimal difference = decimal.Parse(priceObj.price) - decimal.Parse(strategyInfo.paramDic["BuyPrice"]);
                        ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ",Buy Check Current Price for " +
                            strategyInfo.paramDic["PairCoins"] + "=" + priceObj.price +
                            ", Difference to Target=" + difference);
                        //-- Check if Ready to Buy
                        if (readyToBuy == false &&
                            difference < 0 &&
                            Math.Abs(difference) > (30 * decimal.Parse(strategyInfo.paramDic["LowBuyWatchPeriod"]) / 100))
                        {
                            readyToBuy = true;
                            ServiceInfo.LogStrategyAction(strategyInfo, "READY", strategyInfo.name + ",Ready to Buy " +
                            strategyInfo.paramDic["PairCoins"] + "=" + priceObj.price +
                            ", Difference to Target=" + difference);
                        }
                        // -- Buy If Conditions Match 
                        if (readyToBuy == true &&
                            difference > 0 &&
                            difference > (20 * decimal.Parse(strategyInfo.paramDic["HighSellWatchPeriod"]) / 100))
                        {
                            if (MarketProcessFeasibility(decimal.Parse(priceObj.price)))
                            {
                                statusObj = BuyUntilSuccess();
                                buyPrice = decimal.Parse(statusObj.price);
                                state = "SELL";
                                readyToBuy = false;
                                readyToSell = false;
                                lossCounter = 0;
                                maxReachedPrice = 0;
                                strategyInfo.paramDic["ScheduleTime"] = (int.Parse(strategyInfo.paramDic["ScheduleTime"]) / 2).ToString();
                            }
                            else if (difference > (50 * decimal.Parse(strategyInfo.paramDic["HighSellWatchPeriod"]) / 100))
                            {
                                strategyInfo.paramDic["BuyPrice"] = (decimal.Parse(priceObj.price) +
                                                decimal.Parse(strategyInfo.paramDic["ReloadGap"])).ToString();
                            }
                        }
                        //-- Change Buy Price Point
                        if (difference < 0 &&
                            Math.Abs(difference) > (decimal.Parse("1.2") * decimal.Parse(strategyInfo.paramDic["LowBuyWatchPeriod"])))
                        {
                            strategyInfo.paramDic["BuyPrice"] = (decimal.Parse(strategyInfo.paramDic["BuyPrice"]) -
                                decimal.Parse(strategyInfo.paramDic["LowBuyWatchPeriod"])).ToString();
                            ServiceInfo.LogStrategyAction(strategyInfo, "CHANGE", strategyInfo.name + ",Change Buy Price Point for " +
                            strategyInfo.paramDic["PairCoins"] + " ,Price=" + priceObj.price +
                            ", Target Changed to=" + strategyInfo.paramDic["BuyPrice"]);
                        }
                    }
                    else if (state == "SELL")
                    {
                        //-- Get Current Price
                        result = BinanceAPI.GetJson("v3/ticker/price", "symbol=" + strategyInfo.paramDic["PairCoins"]);
                        Price priceObj = JsonConvert.DeserializeObject<Price>(result);
                        decimal difference = decimal.Parse(priceObj.price) - decimal.Parse(statusObj.price);
                        decimal currentLossProfit = (decimal.Parse(priceObj.price) * decimal.Parse(statusObj.origQty)) -
                            (decimal.Parse(statusObj.price) * decimal.Parse(statusObj.origQty)) -
                            (decimal.Parse("1.2") * decimal.Parse(strategyInfo.paramDic["Amount"]) * decimal.Parse(strategyInfo.paramDic["FeeRate"]));
                        ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ",Sell Check Current Price for " +
                            strategyInfo.paramDic["PairCoins"] + "=" + priceObj.price +
                            ", Difference to Target=" + difference);
                        //-- Check pull back and prevent more loss
                        if (readyToSell == true && 
                            difference > 0 &&
                            difference > maxReachedPrice)
                        {
                            maxReachedPrice = difference;
                        }
                        if (readyToSell == true &&
                            (maxReachedPrice - decimal.Parse(priceObj.price)) > (80 * decimal.Parse(strategyInfo.paramDic["HighSellWatchPeriod"])/100) && 
                            difference < 0)
                        {
                            sellStatusObj = SellUntilSuccess(statusObj);
                            if (currentLossProfit > 0)
                                totalProfit += currentLossProfit;
                            else
                                totalLoss += Math.Abs(currentLossProfit);
                            ServiceInfo.LogStrategyAction(strategyInfo, "PREVENT", strategyInfo.name + ",Prevent Loss Limit Reached for " +
                            strategyInfo.paramDic["PairCoins"] + " ,Price=" + sellStatusObj.price +
                            ", Loss Amount=" + currentLossProfit);
                            if (totalLoss > decimal.Parse(strategyInfo.paramDic["TolerableLossLimit"]))
                            {
                                state = "STOP";
                            }
                            else if (totalProfit > decimal.Parse(strategyInfo.paramDic["TolerableProfitLimit"]))
                            {
                                state = "STOP";
                            }
                            else if (bool.Parse(strategyInfo.paramDic["AutoReload"]) == false)
                            {
                                state = "STOP";
                            }
                            else
                            {
                                state = "BUY";
                                readyToBuy = false;
                                readyToSell = false;
                                strategyInfo.paramDic["BuyPrice"] = (decimal.Parse(sellStatusObj.price) +
                                    decimal.Parse(strategyInfo.paramDic["ReloadGap"])).ToString();
                            }
                            strategyInfo.paramDic["ScheduleTime"] = (int.Parse(strategyInfo.paramDic["ScheduleTime"]) * 2).ToString();
                        }
                        //-- Check Loss Limit
                        //if (readyToSell == false && 
                        //    Math.Abs(currentLossProfit) > decimal.Parse(strategyInfo.paramDic["LossLimit"]))
                        if (difference < 0 &&
                            Math.Abs(difference) > decimal.Parse(strategyInfo.paramDic["HighSellWatchPeriod"]))
                        {
                            sellStatusObj = SellUntilSuccess(statusObj);
                            if (currentLossProfit > 0)
                                totalProfit += currentLossProfit;
                            else
                                totalLoss += Math.Abs(currentLossProfit);
                            lossCounter++;
                            ServiceInfo.LogStrategyAction(strategyInfo, "LOSS", strategyInfo.name + ",Loss Limit Reached for " +
                            strategyInfo.paramDic["PairCoins"] + " ,Price=" + sellStatusObj.price +
                            ", Loss Amount=" + currentLossProfit);
                            if (totalLoss > decimal.Parse(strategyInfo.paramDic["TolerableLossLimit"]))
                            {
                                state = "STOP";
                            }
                            else if (totalProfit > decimal.Parse(strategyInfo.paramDic["TolerableProfitLimit"]))
                            {
                                state = "STOP";
                            }
                            else if (bool.Parse(strategyInfo.paramDic["AutoReload"]) == false)
                            {
                                state = "STOP";
                            }
                            else
                            {
                                state = "BUY";
                                readyToBuy = false;
                                readyToSell = false;
                                strategyInfo.paramDic["BuyPrice"] = (decimal.Parse(sellStatusObj.price) +
                                    decimal.Parse(strategyInfo.paramDic["ReloadGap"])).ToString();
                            }
                            strategyInfo.paramDic["ScheduleTime"] = (int.Parse(strategyInfo.paramDic["ScheduleTime"]) * 2).ToString();
                        }
                        //-- Check Profit 
                        if (readyToSell == false &&
                            currentLossProfit > decimal.Parse(strategyInfo.paramDic["AcceptableProfitLimit"]))
                        {
                            readyToSell = true;
                            ServiceInfo.LogStrategyAction(strategyInfo, "READY", strategyInfo.name + ",Ready to Sell " +
                            strategyInfo.paramDic["PairCoins"] + "=" + priceObj.price +
                            ", Difference to Target=" + difference);
                        }
                        //-- Change Profit Border
                        if (readyToSell == true &&
                            difference > (decimal.Parse("1.1") * decimal.Parse(strategyInfo.paramDic["HighSellWatchPeriod"])))
                        {
                            statusObj.price = (decimal.Parse(statusObj.price) + decimal.Parse(strategyInfo.paramDic["HighSellWatchPeriod"])).ToString();
                            ServiceInfo.LogStrategyAction(strategyInfo, "CHANGE", strategyInfo.name + ",Change Sell Price Point for " +
                            strategyInfo.paramDic["PairCoins"] + " ,Price=" + priceObj.price +
                            ", Target Changed to=" + statusObj.price);
                        }
                        //-- Save Profit
                        if (readyToSell == true && difference < 0)
                        {
                            sellStatusObj = SellUntilSuccess(statusObj);
                            decimal profit = (decimal.Parse(sellStatusObj.price) * decimal.Parse(statusObj.origQty)) -
                            (buyPrice * decimal.Parse(statusObj.origQty)) -
                            (2 * decimal.Parse(strategyInfo.paramDic["Amount"]) * decimal.Parse(strategyInfo.paramDic["FeeRate"]));
                            if (profit > 0)
                                totalProfit += profit;
                            else
                                totalLoss += Math.Abs(profit);
                            ServiceInfo.LogStrategyAction(strategyInfo, "PROFIT", strategyInfo.name + ",Profit Limit Reached for " +
                            strategyInfo.paramDic["PairCoins"] + " ,Price=" + sellStatusObj.price +
                            ", Profit Amount=" + profit);
                            if (totalProfit > decimal.Parse(strategyInfo.paramDic["TolerableProfitLimit"]))
                            {
                                state = "STOP";
                            }
                            else if (bool.Parse(strategyInfo.paramDic["AutoReload"]) == false)
                            {
                                state = "STOP";
                            }
                            else
                            {
                                state = "BUY";
                                readyToBuy = false;
                                strategyInfo.paramDic["BuyPrice"] = (decimal.Parse(sellStatusObj.price) +
                                    decimal.Parse(strategyInfo.paramDic["ReloadGap"])).ToString();
                            }
                            strategyInfo.paramDic["ScheduleTime"] = (int.Parse(strategyInfo.paramDic["ScheduleTime"]) * 2).ToString();
                        }
                    }
                    //-- Wait for next loop
                    ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ", Next One Min Loop" + ", Status=" + state
                         + ", Total Profit=" + totalProfit
                         + ", Total Loss=" + totalLoss);
                    strategyInfo.mre.WaitOne(int.Parse(strategyInfo.paramDic["ScheduleTime"]) * 1000);
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
            parameters.Add("Amount");
            parameters.Add("FeeRate");
            parameters.Add("BuyGapAmount");
            parameters.Add("SellGapAmount");
            parameters.Add("ScheduleTime");
            parameters.Add("BuyPrice");
            parameters.Add("HighSellWatchPeriod");
            parameters.Add("LowBuyWatchPeriod");
            parameters.Add("TolerableLossLimit");
            parameters.Add("TolerableProfitLimit");
            parameters.Add("AcceptableProfitLimit");
            parameters.Add("AccumulationIgnore");
            parameters.Add("TimeWindowQuarter");
            parameters.Add("PositiveNegetiveRatio");
            parameters.Add("NegetivePositiveRatio");
            parameters.Add("LossPositiveNegetiveRatio");
            parameters.Add("MaxLossCount");
            parameters.Add("StartLossCount");
            parameters.Add("AutoReload");
            parameters.Add("ReloadGap");
            parameters.Add("Key");
            parameters.Add("Secret");
            return (parameters);
        }

    }
}
