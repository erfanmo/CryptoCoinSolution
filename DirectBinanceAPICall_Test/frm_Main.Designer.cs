namespace DirectBinanceAPICall
{
    partial class frm_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Depth = new System.Windows.Forms.Button();
            this.txt_Result = new System.Windows.Forms.TextBox();
            this.btn_AllPrices = new System.Windows.Forms.Button();
            this.btn_AccountInfo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Orders = new System.Windows.Forms.Button();
            this.btn_TestLimit = new System.Windows.Forms.Button();
            this.btn_LimitOrder = new System.Windows.Forms.Button();
            this.btn_CancelOrder = new System.Windows.Forms.Button();
            this.btn_GetOrderStatus = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Connectivity = new System.Windows.Forms.Button();
            this.btn_ServerTime = new System.Windows.Forms.Button();
            this.btn_ExchangeInfo = new System.Windows.Forms.Button();
            this.btn_RecentTrades = new System.Windows.Forms.Button();
            this.btn_OldTradeLookup = new System.Windows.Forms.Button();
            this.btn_AggregateTrades = new System.Windows.Forms.Button();
            this.btn_Candlestick = new System.Windows.Forms.Button();
            this.btn_CurrentAveragePrice = new System.Windows.Forms.Button();
            this.btn_24hrTickerPriceChange = new System.Windows.Forms.Button();
            this.btn_SymbolPrice = new System.Windows.Forms.Button();
            this.btn_SymbolBookTicker = new System.Windows.Forms.Button();
            this.btn_CancelAllOrders = new System.Windows.Forms.Button();
            this.btn_GetOpenOrders = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Key = new System.Windows.Forms.TextBox();
            this.txt_Secret = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_PairCoins = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_UnixDT = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_DT = new System.Windows.Forms.TextBox();
            this.btn_FromUnix = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_StartTime = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_Side = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_Quantity = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_Price = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_OrderId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_Depth
            // 
            this.btn_Depth.Location = new System.Drawing.Point(214, 80);
            this.btn_Depth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Depth.Name = "btn_Depth";
            this.btn_Depth.Size = new System.Drawing.Size(184, 32);
            this.btn_Depth.TabIndex = 0;
            this.btn_Depth.Text = "Get Symbol Depth";
            this.btn_Depth.UseVisualStyleBackColor = true;
            this.btn_Depth.Click += new System.EventHandler(this.btn_Depth_Click);
            // 
            // txt_Result
            // 
            this.txt_Result.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt_Result.Location = new System.Drawing.Point(898, 0);
            this.txt_Result.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_Result.Multiline = true;
            this.txt_Result.Name = "txt_Result";
            this.txt_Result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_Result.Size = new System.Drawing.Size(388, 765);
            this.txt_Result.TabIndex = 1;
            // 
            // btn_AllPrices
            // 
            this.btn_AllPrices.Location = new System.Drawing.Point(214, 42);
            this.btn_AllPrices.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_AllPrices.Name = "btn_AllPrices";
            this.btn_AllPrices.Size = new System.Drawing.Size(184, 32);
            this.btn_AllPrices.TabIndex = 2;
            this.btn_AllPrices.Text = "Get All Prices";
            this.btn_AllPrices.UseVisualStyleBackColor = true;
            this.btn_AllPrices.Click += new System.EventHandler(this.btn_AllPrices_Click);
            // 
            // btn_AccountInfo
            // 
            this.btn_AccountInfo.Location = new System.Drawing.Point(22, 42);
            this.btn_AccountInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_AccountInfo.Name = "btn_AccountInfo";
            this.btn_AccountInfo.Size = new System.Drawing.Size(184, 32);
            this.btn_AccountInfo.TabIndex = 3;
            this.btn_AccountInfo.Text = "Get Account Info";
            this.btn_AccountInfo.UseVisualStyleBackColor = true;
            this.btn_AccountInfo.Click += new System.EventHandler(this.btn_AccountInfo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(70, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Account";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(260, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Market";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(457, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Orders";
            // 
            // btn_Orders
            // 
            this.btn_Orders.Location = new System.Drawing.Point(405, 42);
            this.btn_Orders.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Orders.Name = "btn_Orders";
            this.btn_Orders.Size = new System.Drawing.Size(184, 32);
            this.btn_Orders.TabIndex = 7;
            this.btn_Orders.Text = "Get All Orders";
            this.btn_Orders.UseVisualStyleBackColor = true;
            this.btn_Orders.Click += new System.EventHandler(this.btn_Orders_Click);
            // 
            // btn_TestLimit
            // 
            this.btn_TestLimit.Location = new System.Drawing.Point(405, 80);
            this.btn_TestLimit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_TestLimit.Name = "btn_TestLimit";
            this.btn_TestLimit.Size = new System.Drawing.Size(184, 32);
            this.btn_TestLimit.TabIndex = 9;
            this.btn_TestLimit.Text = "Test Limit Order";
            this.btn_TestLimit.UseVisualStyleBackColor = true;
            this.btn_TestLimit.Click += new System.EventHandler(this.btn_TestLimit_Click);
            // 
            // btn_LimitOrder
            // 
            this.btn_LimitOrder.Location = new System.Drawing.Point(405, 118);
            this.btn_LimitOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_LimitOrder.Name = "btn_LimitOrder";
            this.btn_LimitOrder.Size = new System.Drawing.Size(184, 32);
            this.btn_LimitOrder.TabIndex = 10;
            this.btn_LimitOrder.Text = "Limit Order";
            this.btn_LimitOrder.UseVisualStyleBackColor = true;
            this.btn_LimitOrder.Click += new System.EventHandler(this.btn_LimitOrder_Click);
            // 
            // btn_CancelOrder
            // 
            this.btn_CancelOrder.Location = new System.Drawing.Point(405, 156);
            this.btn_CancelOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_CancelOrder.Name = "btn_CancelOrder";
            this.btn_CancelOrder.Size = new System.Drawing.Size(184, 32);
            this.btn_CancelOrder.TabIndex = 11;
            this.btn_CancelOrder.Text = "Cancel Order";
            this.btn_CancelOrder.UseVisualStyleBackColor = true;
            this.btn_CancelOrder.Click += new System.EventHandler(this.btn_CancelOrder_Click);
            // 
            // btn_GetOrderStatus
            // 
            this.btn_GetOrderStatus.Location = new System.Drawing.Point(405, 232);
            this.btn_GetOrderStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_GetOrderStatus.Name = "btn_GetOrderStatus";
            this.btn_GetOrderStatus.Size = new System.Drawing.Size(184, 32);
            this.btn_GetOrderStatus.TabIndex = 12;
            this.btn_GetOrderStatus.Text = "Get Order Status";
            this.btn_GetOrderStatus.UseVisualStyleBackColor = true;
            this.btn_GetOrderStatus.Click += new System.EventHandler(this.btn_GetOrderStatus_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(70, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 25);
            this.label4.TabIndex = 14;
            this.label4.Text = "General";
            // 
            // btn_Connectivity
            // 
            this.btn_Connectivity.Location = new System.Drawing.Point(22, 194);
            this.btn_Connectivity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Connectivity.Name = "btn_Connectivity";
            this.btn_Connectivity.Size = new System.Drawing.Size(184, 32);
            this.btn_Connectivity.TabIndex = 15;
            this.btn_Connectivity.Text = "Test Connectivity";
            this.btn_Connectivity.UseVisualStyleBackColor = true;
            this.btn_Connectivity.Click += new System.EventHandler(this.btn_Connectivity_Click);
            // 
            // btn_ServerTime
            // 
            this.btn_ServerTime.Location = new System.Drawing.Point(22, 232);
            this.btn_ServerTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ServerTime.Name = "btn_ServerTime";
            this.btn_ServerTime.Size = new System.Drawing.Size(184, 32);
            this.btn_ServerTime.TabIndex = 16;
            this.btn_ServerTime.Text = "Server Time";
            this.btn_ServerTime.UseVisualStyleBackColor = true;
            this.btn_ServerTime.Click += new System.EventHandler(this.btn_ServerTime_Click);
            // 
            // btn_ExchangeInfo
            // 
            this.btn_ExchangeInfo.Location = new System.Drawing.Point(214, 118);
            this.btn_ExchangeInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ExchangeInfo.Name = "btn_ExchangeInfo";
            this.btn_ExchangeInfo.Size = new System.Drawing.Size(184, 32);
            this.btn_ExchangeInfo.TabIndex = 17;
            this.btn_ExchangeInfo.Text = "Exchange Info";
            this.btn_ExchangeInfo.UseVisualStyleBackColor = true;
            this.btn_ExchangeInfo.Click += new System.EventHandler(this.btn_ExchangeInfo_Click);
            // 
            // btn_RecentTrades
            // 
            this.btn_RecentTrades.Location = new System.Drawing.Point(214, 156);
            this.btn_RecentTrades.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_RecentTrades.Name = "btn_RecentTrades";
            this.btn_RecentTrades.Size = new System.Drawing.Size(184, 32);
            this.btn_RecentTrades.TabIndex = 18;
            this.btn_RecentTrades.Text = "Recent Trades";
            this.btn_RecentTrades.UseVisualStyleBackColor = true;
            this.btn_RecentTrades.Click += new System.EventHandler(this.btn_RecentTrades_Click);
            // 
            // btn_OldTradeLookup
            // 
            this.btn_OldTradeLookup.Location = new System.Drawing.Point(214, 194);
            this.btn_OldTradeLookup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_OldTradeLookup.Name = "btn_OldTradeLookup";
            this.btn_OldTradeLookup.Size = new System.Drawing.Size(184, 32);
            this.btn_OldTradeLookup.TabIndex = 19;
            this.btn_OldTradeLookup.Text = "Old Trades Lookup";
            this.btn_OldTradeLookup.UseVisualStyleBackColor = true;
            this.btn_OldTradeLookup.Click += new System.EventHandler(this.btn_OldTradeLookup_Click);
            // 
            // btn_AggregateTrades
            // 
            this.btn_AggregateTrades.Location = new System.Drawing.Point(214, 232);
            this.btn_AggregateTrades.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_AggregateTrades.Name = "btn_AggregateTrades";
            this.btn_AggregateTrades.Size = new System.Drawing.Size(184, 32);
            this.btn_AggregateTrades.TabIndex = 20;
            this.btn_AggregateTrades.Text = "Aggregate Trades";
            this.btn_AggregateTrades.UseVisualStyleBackColor = true;
            this.btn_AggregateTrades.Click += new System.EventHandler(this.btn_AggregateTrades_Click);
            // 
            // btn_Candlestick
            // 
            this.btn_Candlestick.Location = new System.Drawing.Point(214, 270);
            this.btn_Candlestick.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Candlestick.Name = "btn_Candlestick";
            this.btn_Candlestick.Size = new System.Drawing.Size(184, 32);
            this.btn_Candlestick.TabIndex = 21;
            this.btn_Candlestick.Text = "Candlestick data";
            this.btn_Candlestick.UseVisualStyleBackColor = true;
            this.btn_Candlestick.Click += new System.EventHandler(this.btn_Candlestick_Click);
            // 
            // btn_CurrentAveragePrice
            // 
            this.btn_CurrentAveragePrice.Location = new System.Drawing.Point(214, 308);
            this.btn_CurrentAveragePrice.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_CurrentAveragePrice.Name = "btn_CurrentAveragePrice";
            this.btn_CurrentAveragePrice.Size = new System.Drawing.Size(184, 32);
            this.btn_CurrentAveragePrice.TabIndex = 22;
            this.btn_CurrentAveragePrice.Text = "Current Average Price";
            this.btn_CurrentAveragePrice.UseVisualStyleBackColor = true;
            this.btn_CurrentAveragePrice.Click += new System.EventHandler(this.btn_CurrentAveragePrice_Click);
            // 
            // btn_24hrTickerPriceChange
            // 
            this.btn_24hrTickerPriceChange.Location = new System.Drawing.Point(214, 346);
            this.btn_24hrTickerPriceChange.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_24hrTickerPriceChange.Name = "btn_24hrTickerPriceChange";
            this.btn_24hrTickerPriceChange.Size = new System.Drawing.Size(184, 32);
            this.btn_24hrTickerPriceChange.TabIndex = 23;
            this.btn_24hrTickerPriceChange.Text = "24hr Ticker Price Change";
            this.btn_24hrTickerPriceChange.UseVisualStyleBackColor = true;
            this.btn_24hrTickerPriceChange.Click += new System.EventHandler(this.btn_24hrTickerPriceChange_Click);
            // 
            // btn_SymbolPrice
            // 
            this.btn_SymbolPrice.Location = new System.Drawing.Point(214, 384);
            this.btn_SymbolPrice.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_SymbolPrice.Name = "btn_SymbolPrice";
            this.btn_SymbolPrice.Size = new System.Drawing.Size(184, 32);
            this.btn_SymbolPrice.TabIndex = 24;
            this.btn_SymbolPrice.Text = "Symbol Price";
            this.btn_SymbolPrice.UseVisualStyleBackColor = true;
            this.btn_SymbolPrice.Click += new System.EventHandler(this.btn_SymbolPrice_Click);
            // 
            // btn_SymbolBookTicker
            // 
            this.btn_SymbolBookTicker.Location = new System.Drawing.Point(214, 422);
            this.btn_SymbolBookTicker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_SymbolBookTicker.Name = "btn_SymbolBookTicker";
            this.btn_SymbolBookTicker.Size = new System.Drawing.Size(184, 32);
            this.btn_SymbolBookTicker.TabIndex = 25;
            this.btn_SymbolBookTicker.Text = "Symbol Book Ticker";
            this.btn_SymbolBookTicker.UseVisualStyleBackColor = true;
            this.btn_SymbolBookTicker.Click += new System.EventHandler(this.btn_SymbolBookTicker_Click);
            // 
            // btn_CancelAllOrders
            // 
            this.btn_CancelAllOrders.Location = new System.Drawing.Point(405, 194);
            this.btn_CancelAllOrders.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_CancelAllOrders.Name = "btn_CancelAllOrders";
            this.btn_CancelAllOrders.Size = new System.Drawing.Size(184, 32);
            this.btn_CancelAllOrders.TabIndex = 26;
            this.btn_CancelAllOrders.Text = "Cancel All Orders";
            this.btn_CancelAllOrders.UseVisualStyleBackColor = true;
            this.btn_CancelAllOrders.Click += new System.EventHandler(this.btn_CancelAllOrders_Click);
            // 
            // btn_GetOpenOrders
            // 
            this.btn_GetOpenOrders.Location = new System.Drawing.Point(405, 270);
            this.btn_GetOpenOrders.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_GetOpenOrders.Name = "btn_GetOpenOrders";
            this.btn_GetOpenOrders.Size = new System.Drawing.Size(184, 32);
            this.btn_GetOpenOrders.TabIndex = 27;
            this.btn_GetOpenOrders.Text = "Get Open Orders";
            this.btn_GetOpenOrders.UseVisualStyleBackColor = true;
            this.btn_GetOpenOrders.Click += new System.EventHandler(this.btn_GetOpenOrders_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 479);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 20);
            this.label5.TabIndex = 28;
            this.label5.Text = "Key";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 520);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 20);
            this.label6.TabIndex = 29;
            this.label6.Text = "Secret";
            // 
            // txt_Key
            // 
            this.txt_Key.Location = new System.Drawing.Point(80, 476);
            this.txt_Key.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Key.Name = "txt_Key";
            this.txt_Key.Size = new System.Drawing.Size(509, 26);
            this.txt_Key.TabIndex = 30;
            // 
            // txt_Secret
            // 
            this.txt_Secret.Location = new System.Drawing.Point(80, 518);
            this.txt_Secret.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Secret.Name = "txt_Secret";
            this.txt_Secret.Size = new System.Drawing.Size(509, 26);
            this.txt_Secret.TabIndex = 31;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(686, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 20);
            this.label10.TabIndex = 44;
            this.label10.Text = "Pair Coins";
            // 
            // txt_PairCoins
            // 
            this.txt_PairCoins.Location = new System.Drawing.Point(691, 32);
            this.txt_PairCoins.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_PairCoins.Name = "txt_PairCoins";
            this.txt_PairCoins.Size = new System.Drawing.Size(188, 26);
            this.txt_PairCoins.TabIndex = 43;
            this.txt_PairCoins.Text = "BTCUSDT";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(568, 560);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 20);
            this.label7.TabIndex = 46;
            this.label7.Text = "Unix DT";
            // 
            // txt_UnixDT
            // 
            this.txt_UnixDT.Location = new System.Drawing.Point(648, 556);
            this.txt_UnixDT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_UnixDT.Name = "txt_UnixDT";
            this.txt_UnixDT.Size = new System.Drawing.Size(230, 26);
            this.txt_UnixDT.TabIndex = 45;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(568, 648);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 20);
            this.label8.TabIndex = 50;
            this.label8.Text = "DT";
            // 
            // txt_DT
            // 
            this.txt_DT.Location = new System.Drawing.Point(609, 644);
            this.txt_DT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_DT.Name = "txt_DT";
            this.txt_DT.Size = new System.Drawing.Size(270, 26);
            this.txt_DT.TabIndex = 49;
            // 
            // btn_FromUnix
            // 
            this.btn_FromUnix.Location = new System.Drawing.Point(573, 596);
            this.btn_FromUnix.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_FromUnix.Name = "btn_FromUnix";
            this.btn_FromUnix.Size = new System.Drawing.Size(306, 41);
            this.btn_FromUnix.TabIndex = 51;
            this.btn_FromUnix.Text = "From Unix";
            this.btn_FromUnix.UseVisualStyleBackColor = true;
            this.btn_FromUnix.Click += new System.EventHandler(this.btn_FromUnix_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(686, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 20);
            this.label9.TabIndex = 53;
            this.label9.Text = "Start Time";
            // 
            // txt_StartTime
            // 
            this.txt_StartTime.Location = new System.Drawing.Point(691, 104);
            this.txt_StartTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_StartTime.Name = "txt_StartTime";
            this.txt_StartTime.Size = new System.Drawing.Size(188, 26);
            this.txt_StartTime.TabIndex = 52;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(686, 146);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 20);
            this.label11.TabIndex = 55;
            this.label11.Text = "Side";
            // 
            // txt_Side
            // 
            this.txt_Side.Location = new System.Drawing.Point(691, 175);
            this.txt_Side.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_Side.Name = "txt_Side";
            this.txt_Side.Size = new System.Drawing.Size(188, 26);
            this.txt_Side.TabIndex = 54;
            this.txt_Side.Text = "BUY";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(686, 214);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 20);
            this.label12.TabIndex = 57;
            this.label12.Text = "Quantity";
            // 
            // txt_Quantity
            // 
            this.txt_Quantity.Location = new System.Drawing.Point(691, 242);
            this.txt_Quantity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_Quantity.Name = "txt_Quantity";
            this.txt_Quantity.Size = new System.Drawing.Size(188, 26);
            this.txt_Quantity.TabIndex = 56;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(686, 281);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 20);
            this.label13.TabIndex = 59;
            this.label13.Text = "Price";
            // 
            // txt_Price
            // 
            this.txt_Price.Location = new System.Drawing.Point(691, 310);
            this.txt_Price.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_Price.Name = "txt_Price";
            this.txt_Price.Size = new System.Drawing.Size(188, 26);
            this.txt_Price.TabIndex = 58;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(686, 356);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 20);
            this.label14.TabIndex = 61;
            this.label14.Text = "Order Id";
            // 
            // txt_OrderId
            // 
            this.txt_OrderId.Location = new System.Drawing.Point(691, 385);
            this.txt_OrderId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_OrderId.Name = "txt_OrderId";
            this.txt_OrderId.Size = new System.Drawing.Size(188, 26);
            this.txt_OrderId.TabIndex = 60;
            // 
            // frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1286, 765);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txt_OrderId);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txt_Price);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txt_Quantity);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txt_Side);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txt_StartTime);
            this.Controls.Add(this.btn_FromUnix);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_DT);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_UnixDT);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txt_PairCoins);
            this.Controls.Add(this.txt_Secret);
            this.Controls.Add(this.txt_Key);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_GetOpenOrders);
            this.Controls.Add(this.btn_CancelAllOrders);
            this.Controls.Add(this.btn_SymbolBookTicker);
            this.Controls.Add(this.btn_SymbolPrice);
            this.Controls.Add(this.btn_24hrTickerPriceChange);
            this.Controls.Add(this.btn_CurrentAveragePrice);
            this.Controls.Add(this.btn_Candlestick);
            this.Controls.Add(this.btn_AggregateTrades);
            this.Controls.Add(this.btn_OldTradeLookup);
            this.Controls.Add(this.btn_RecentTrades);
            this.Controls.Add(this.btn_ExchangeInfo);
            this.Controls.Add(this.btn_ServerTime);
            this.Controls.Add(this.btn_Connectivity);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_GetOrderStatus);
            this.Controls.Add(this.btn_CancelOrder);
            this.Controls.Add(this.btn_LimitOrder);
            this.Controls.Add(this.btn_TestLimit);
            this.Controls.Add(this.btn_Orders);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_AccountInfo);
            this.Controls.Add(this.btn_AllPrices);
            this.Controls.Add(this.txt_Result);
            this.Controls.Add(this.btn_Depth);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sample";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Depth;
        private System.Windows.Forms.TextBox txt_Result;
        private System.Windows.Forms.Button btn_AllPrices;
        private System.Windows.Forms.Button btn_AccountInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Orders;
        private System.Windows.Forms.Button btn_TestLimit;
        private System.Windows.Forms.Button btn_LimitOrder;
        private System.Windows.Forms.Button btn_CancelOrder;
        private System.Windows.Forms.Button btn_GetOrderStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Connectivity;
        private System.Windows.Forms.Button btn_ServerTime;
        private System.Windows.Forms.Button btn_ExchangeInfo;
        private System.Windows.Forms.Button btn_RecentTrades;
        private System.Windows.Forms.Button btn_OldTradeLookup;
        private System.Windows.Forms.Button btn_AggregateTrades;
        private System.Windows.Forms.Button btn_Candlestick;
        private System.Windows.Forms.Button btn_CurrentAveragePrice;
        private System.Windows.Forms.Button btn_24hrTickerPriceChange;
        private System.Windows.Forms.Button btn_SymbolPrice;
        private System.Windows.Forms.Button btn_SymbolBookTicker;
        private System.Windows.Forms.Button btn_CancelAllOrders;
        private System.Windows.Forms.Button btn_GetOpenOrders;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_Key;
        private System.Windows.Forms.TextBox txt_Secret;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_PairCoins;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_UnixDT;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_DT;
        private System.Windows.Forms.Button btn_FromUnix;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_StartTime;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_Side;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_Quantity;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_Price;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_OrderId;
    }
}

