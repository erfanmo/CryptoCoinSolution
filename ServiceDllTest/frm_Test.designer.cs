namespace ServiceDllTest
{
    partial class frm_Test
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
            this.btn_Test = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Key = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Secret = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_HighSellWatchPeriod = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_LowBuyWatchPeriod = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_FeeRate = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_Params = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_AcceptableProfitLimit = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_AcceptableLossLimit = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_PairCoins = new System.Windows.Forms.TextBox();
            this.txt_Schedule = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chk_JustLog = new System.Windows.Forms.CheckBox();
            this.chk_AutoReload = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_AutoReloadCount = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_AutoOrderCount = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_AutoOrderAmount = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_AutoOrderWatchPeriod = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txt_Wait = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_MaxActiveOrder = new System.Windows.Forms.TextBox();
            this.chk_OrderInstedLoss = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txt_HigherPriceAmount = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txt_AutoOrderCoefficient = new System.Windows.Forms.TextBox();
            this.chk_LossCount = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txt_LossCount = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_AutoOrderLockPeriod = new System.Windows.Forms.TextBox();
            this.chk_ProfitLossNotification = new System.Windows.Forms.CheckBox();
            this.chk_PeriodicalNotification = new System.Windows.Forms.CheckBox();
            this.txt_PeriodicalNotify = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txt_EmailDelivery = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txt_MobileDelivery = new System.Windows.Forms.TextBox();
            this.chk_Email = new System.Windows.Forms.CheckBox();
            this.chk_SMS = new System.Windows.Forms.CheckBox();
            this.chk_Notification = new System.Windows.Forms.CheckBox();
            this.button15 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Test
            // 
            this.btn_Test.Location = new System.Drawing.Point(687, 8);
            this.btn_Test.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Test.Name = "btn_Test";
            this.btn_Test.Size = new System.Drawing.Size(234, 35);
            this.btn_Test.TabIndex = 1;
            this.btn_Test.Text = "Dll Test";
            this.btn_Test.UseVisualStyleBackColor = true;
            this.btn_Test.Click += new System.EventHandler(this.btn_Test_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(687, 141);
            this.button8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(234, 35);
            this.button8.TabIndex = 16;
            this.button8.Text = "Terminate Strategy";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(687, 96);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(234, 35);
            this.button4.TabIndex = 15;
            this.button4.Text = "Start Strategy";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(687, 53);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(234, 35);
            this.button3.TabIndex = 14;
            this.button3.Text = "Register Strategy";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(687, 348);
            this.button11.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(234, 35);
            this.button11.TabIndex = 22;
            this.button11.Text = "Get Active Strategies";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(687, 303);
            this.button10.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(234, 35);
            this.button10.TabIndex = 20;
            this.button10.Text = "Get Strategy List";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(687, 303);
            this.button6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(234, 35);
            this.button6.TabIndex = 21;
            this.button6.Text = "Get Strategy List";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(687, 258);
            this.button9.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(234, 35);
            this.button9.TabIndex = 18;
            this.button9.Text = "Get Strategy Params";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(687, 258);
            this.button7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(234, 35);
            this.button7.TabIndex = 19;
            this.button7.Text = "Get Strategy Params";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(687, 213);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(234, 35);
            this.button5.TabIndex = 17;
            this.button5.Text = "Get Monitoring";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // txt_Name
            // 
            this.txt_Name.Location = new System.Drawing.Point(75, 47);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(181, 26);
            this.txt_Name.TabIndex = 23;
            this.txt_Name.Text = "WatchOrderSample";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 24;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 20);
            this.label2.TabIndex = 26;
            this.label2.Text = "Key";
            // 
            // txt_Key
            // 
            this.txt_Key.Location = new System.Drawing.Point(75, 206);
            this.txt_Key.Name = "txt_Key";
            this.txt_Key.Size = new System.Drawing.Size(497, 26);
            this.txt_Key.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 28;
            this.label3.Text = "Secret";
            // 
            // txt_Secret
            // 
            this.txt_Secret.Location = new System.Drawing.Point(75, 238);
            this.txt_Secret.Name = "txt_Secret";
            this.txt_Secret.Size = new System.Drawing.Size(497, 26);
            this.txt_Secret.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 20);
            this.label4.TabIndex = 30;
            this.label4.Text = "High Sell Watch Period";
            // 
            // txt_HighSellWatchPeriod
            // 
            this.txt_HighSellWatchPeriod.Location = new System.Drawing.Point(22, 105);
            this.txt_HighSellWatchPeriod.Name = "txt_HighSellWatchPeriod";
            this.txt_HighSellWatchPeriod.Size = new System.Drawing.Size(187, 26);
            this.txt_HighSellWatchPeriod.TabIndex = 29;
            this.txt_HighSellWatchPeriod.Text = "100";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(211, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 20);
            this.label5.TabIndex = 32;
            this.label5.Text = "Low Buy Watch Period";
            // 
            // txt_LowBuyWatchPeriod
            // 
            this.txt_LowBuyWatchPeriod.Location = new System.Drawing.Point(215, 105);
            this.txt_LowBuyWatchPeriod.Name = "txt_LowBuyWatchPeriod";
            this.txt_LowBuyWatchPeriod.Size = new System.Drawing.Size(187, 26);
            this.txt_LowBuyWatchPeriod.TabIndex = 31;
            this.txt_LowBuyWatchPeriod.Text = "100";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(399, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 20);
            this.label6.TabIndex = 34;
            this.label6.Text = "Fee Rate";
            // 
            // txt_FeeRate
            // 
            this.txt_FeeRate.Location = new System.Drawing.Point(481, 12);
            this.txt_FeeRate.Name = "txt_FeeRate";
            this.txt_FeeRate.Size = new System.Drawing.Size(91, 26);
            this.txt_FeeRate.TabIndex = 33;
            this.txt_FeeRate.Text = "0.001";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 177);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 20);
            this.label7.TabIndex = 36;
            this.label7.Text = "Params";
            // 
            // txt_Params
            // 
            this.txt_Params.Location = new System.Drawing.Point(75, 174);
            this.txt_Params.Name = "txt_Params";
            this.txt_Params.Size = new System.Drawing.Size(497, 26);
            this.txt_Params.TabIndex = 35;
            this.txt_Params.Text = "50_34000;50_33600;50_33100;50_32600";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(240, 140);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 20);
            this.label8.TabIndex = 38;
            this.label8.Text = "Profit Limit";
            // 
            // txt_AcceptableProfitLimit
            // 
            this.txt_AcceptableProfitLimit.Location = new System.Drawing.Point(329, 137);
            this.txt_AcceptableProfitLimit.Name = "txt_AcceptableProfitLimit";
            this.txt_AcceptableProfitLimit.Size = new System.Drawing.Size(64, 26);
            this.txt_AcceptableProfitLimit.TabIndex = 37;
            this.txt_AcceptableProfitLimit.Text = "0.10";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(404, 140);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 20);
            this.label9.TabIndex = 40;
            this.label9.Text = "Loss Limit";
            // 
            // txt_AcceptableLossLimit
            // 
            this.txt_AcceptableLossLimit.Location = new System.Drawing.Point(490, 137);
            this.txt_AcceptableLossLimit.Name = "txt_AcceptableLossLimit";
            this.txt_AcceptableLossLimit.Size = new System.Drawing.Size(81, 26);
            this.txt_AcceptableLossLimit.TabIndex = 39;
            this.txt_AcceptableLossLimit.Text = "1.5";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(126, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 20);
            this.label10.TabIndex = 42;
            this.label10.Text = "Pair Coins";
            // 
            // txt_PairCoins
            // 
            this.txt_PairCoins.Location = new System.Drawing.Point(211, 12);
            this.txt_PairCoins.Name = "txt_PairCoins";
            this.txt_PairCoins.Size = new System.Drawing.Size(168, 26);
            this.txt_PairCoins.TabIndex = 41;
            this.txt_PairCoins.Text = "BTCUSDT";
            // 
            // txt_Schedule
            // 
            this.txt_Schedule.Location = new System.Drawing.Point(408, 105);
            this.txt_Schedule.Name = "txt_Schedule";
            this.txt_Schedule.Size = new System.Drawing.Size(164, 26);
            this.txt_Schedule.TabIndex = 43;
            this.txt_Schedule.Text = "4";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(404, 82);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 20);
            this.label11.TabIndex = 44;
            this.label11.Text = "Schedule";
            // 
            // chk_JustLog
            // 
            this.chk_JustLog.AutoSize = true;
            this.chk_JustLog.Checked = true;
            this.chk_JustLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_JustLog.Location = new System.Drawing.Point(22, 14);
            this.chk_JustLog.Name = "chk_JustLog";
            this.chk_JustLog.Size = new System.Drawing.Size(96, 24);
            this.chk_JustLog.TabIndex = 45;
            this.chk_JustLog.Text = "Just Log";
            this.chk_JustLog.UseVisualStyleBackColor = true;
            // 
            // chk_AutoReload
            // 
            this.chk_AutoReload.AutoSize = true;
            this.chk_AutoReload.Checked = true;
            this.chk_AutoReload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_AutoReload.Location = new System.Drawing.Point(7, 276);
            this.chk_AutoReload.Name = "chk_AutoReload";
            this.chk_AutoReload.Size = new System.Drawing.Size(124, 24);
            this.chk_AutoReload.TabIndex = 46;
            this.chk_AutoReload.Text = "Auto Reload";
            this.chk_AutoReload.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 308);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(145, 20);
            this.label12.TabIndex = 48;
            this.label12.Text = "Auto Reload Count";
            // 
            // txt_AutoReloadCount
            // 
            this.txt_AutoReloadCount.Location = new System.Drawing.Point(7, 331);
            this.txt_AutoReloadCount.Name = "txt_AutoReloadCount";
            this.txt_AutoReloadCount.Size = new System.Drawing.Size(141, 26);
            this.txt_AutoReloadCount.TabIndex = 47;
            this.txt_AutoReloadCount.Text = "120";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(150, 308);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(134, 20);
            this.label13.TabIndex = 50;
            this.label13.Text = "Auto Order Count";
            // 
            // txt_AutoOrderCount
            // 
            this.txt_AutoOrderCount.Location = new System.Drawing.Point(154, 331);
            this.txt_AutoOrderCount.Name = "txt_AutoOrderCount";
            this.txt_AutoOrderCount.Size = new System.Drawing.Size(130, 26);
            this.txt_AutoOrderCount.TabIndex = 49;
            this.txt_AutoOrderCount.Text = "4";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(288, 308);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(147, 20);
            this.label14.TabIndex = 52;
            this.label14.Text = "Auto Order Amount";
            // 
            // txt_AutoOrderAmount
            // 
            this.txt_AutoOrderAmount.Location = new System.Drawing.Point(292, 331);
            this.txt_AutoOrderAmount.Name = "txt_AutoOrderAmount";
            this.txt_AutoOrderAmount.Size = new System.Drawing.Size(141, 26);
            this.txt_AutoOrderAmount.TabIndex = 51;
            this.txt_AutoOrderAmount.Text = "50";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(435, 308);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(136, 20);
            this.label15.TabIndex = 54;
            this.label15.Text = "Auto Order Period";
            // 
            // txt_AutoOrderWatchPeriod
            // 
            this.txt_AutoOrderWatchPeriod.Location = new System.Drawing.Point(439, 331);
            this.txt_AutoOrderWatchPeriod.Name = "txt_AutoOrderWatchPeriod";
            this.txt_AutoOrderWatchPeriod.Size = new System.Drawing.Size(141, 26);
            this.txt_AutoOrderWatchPeriod.TabIndex = 53;
            this.txt_AutoOrderWatchPeriod.Text = "300";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(262, 50);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(251, 20);
            this.label16.TabIndex = 56;
            this.label16.Text = "Wait Time after Order (* Schedule)";
            // 
            // txt_Wait
            // 
            this.txt_Wait.Location = new System.Drawing.Point(519, 47);
            this.txt_Wait.Name = "txt_Wait";
            this.txt_Wait.Size = new System.Drawing.Size(52, 26);
            this.txt_Wait.TabIndex = 55;
            this.txt_Wait.Text = "2";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(21, 140);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(137, 20);
            this.label17.TabIndex = 58;
            this.label17.Text = "Max Active Orders";
            // 
            // txt_MaxActiveOrder
            // 
            this.txt_MaxActiveOrder.Location = new System.Drawing.Point(165, 137);
            this.txt_MaxActiveOrder.Name = "txt_MaxActiveOrder";
            this.txt_MaxActiveOrder.Size = new System.Drawing.Size(62, 26);
            this.txt_MaxActiveOrder.TabIndex = 57;
            this.txt_MaxActiveOrder.Text = "2";
            // 
            // chk_OrderInstedLoss
            // 
            this.chk_OrderInstedLoss.AutoSize = true;
            this.chk_OrderInstedLoss.Checked = true;
            this.chk_OrderInstedLoss.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_OrderInstedLoss.Location = new System.Drawing.Point(7, 397);
            this.chk_OrderInstedLoss.Name = "chk_OrderInstedLoss";
            this.chk_OrderInstedLoss.Size = new System.Drawing.Size(244, 24);
            this.chk_OrderInstedLoss.TabIndex = 59;
            this.chk_OrderInstedLoss.Text = "Register Order Insted of Loss";
            this.chk_OrderInstedLoss.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(5, 430);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(295, 20);
            this.label18.TabIndex = 61;
            this.label18.Text = "Register Order with Higher Price Amount";
            // 
            // txt_HigherPriceAmount
            // 
            this.txt_HigherPriceAmount.Location = new System.Drawing.Point(306, 427);
            this.txt_HigherPriceAmount.Name = "txt_HigherPriceAmount";
            this.txt_HigherPriceAmount.Size = new System.Drawing.Size(67, 26);
            this.txt_HigherPriceAmount.TabIndex = 60;
            this.txt_HigherPriceAmount.Text = "300";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 366);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(167, 20);
            this.label19.TabIndex = 63;
            this.label19.Text = "Auto Order Coefficient";
            // 
            // txt_AutoOrderCoefficient
            // 
            this.txt_AutoOrderCoefficient.Location = new System.Drawing.Point(170, 363);
            this.txt_AutoOrderCoefficient.Name = "txt_AutoOrderCoefficient";
            this.txt_AutoOrderCoefficient.Size = new System.Drawing.Size(61, 26);
            this.txt_AutoOrderCoefficient.TabIndex = 62;
            this.txt_AutoOrderCoefficient.Text = "100";
            // 
            // chk_LossCount
            // 
            this.chk_LossCount.AutoSize = true;
            this.chk_LossCount.Checked = true;
            this.chk_LossCount.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_LossCount.Location = new System.Drawing.Point(403, 397);
            this.chk_LossCount.Name = "chk_LossCount";
            this.chk_LossCount.Size = new System.Drawing.Size(171, 24);
            this.chk_LossCount.TabIndex = 64;
            this.chk_LossCount.Text = "Control Loss Count";
            this.chk_LossCount.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(399, 430);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(90, 20);
            this.label20.TabIndex = 66;
            this.label20.Text = "Loss Count";
            // 
            // txt_LossCount
            // 
            this.txt_LossCount.Location = new System.Drawing.Point(490, 427);
            this.txt_LossCount.Name = "txt_LossCount";
            this.txt_LossCount.Size = new System.Drawing.Size(61, 26);
            this.txt_LossCount.TabIndex = 65;
            this.txt_LossCount.Text = "1";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(308, 363);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(125, 20);
            this.label21.TabIndex = 68;
            this.label21.Text = "Auto Order Lock";
            // 
            // txt_AutoOrderLockPeriod
            // 
            this.txt_AutoOrderLockPeriod.Location = new System.Drawing.Point(439, 360);
            this.txt_AutoOrderLockPeriod.Name = "txt_AutoOrderLockPeriod";
            this.txt_AutoOrderLockPeriod.Size = new System.Drawing.Size(141, 26);
            this.txt_AutoOrderLockPeriod.TabIndex = 67;
            this.txt_AutoOrderLockPeriod.Text = "400";
            // 
            // chk_ProfitLossNotification
            // 
            this.chk_ProfitLossNotification.AutoSize = true;
            this.chk_ProfitLossNotification.Checked = true;
            this.chk_ProfitLossNotification.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_ProfitLossNotification.Location = new System.Drawing.Point(7, 466);
            this.chk_ProfitLossNotification.Name = "chk_ProfitLossNotification";
            this.chk_ProfitLossNotification.Size = new System.Drawing.Size(193, 24);
            this.chk_ProfitLossNotification.TabIndex = 69;
            this.chk_ProfitLossNotification.Text = "Profit Loss Notification";
            this.chk_ProfitLossNotification.UseVisualStyleBackColor = true;
            // 
            // chk_PeriodicalNotification
            // 
            this.chk_PeriodicalNotification.AutoSize = true;
            this.chk_PeriodicalNotification.Checked = true;
            this.chk_PeriodicalNotification.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_PeriodicalNotification.Location = new System.Drawing.Point(209, 466);
            this.chk_PeriodicalNotification.Name = "chk_PeriodicalNotification";
            this.chk_PeriodicalNotification.Size = new System.Drawing.Size(227, 24);
            this.chk_PeriodicalNotification.TabIndex = 70;
            this.chk_PeriodicalNotification.Text = "Periodical Notification every";
            this.chk_PeriodicalNotification.UseVisualStyleBackColor = true;
            // 
            // txt_PeriodicalNotify
            // 
            this.txt_PeriodicalNotify.Location = new System.Drawing.Point(443, 464);
            this.txt_PeriodicalNotify.Name = "txt_PeriodicalNotify";
            this.txt_PeriodicalNotify.Size = new System.Drawing.Size(61, 26);
            this.txt_PeriodicalNotify.TabIndex = 71;
            this.txt_PeriodicalNotify.Text = "3";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(510, 467);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(52, 20);
            this.label22.TabIndex = 72;
            this.label22.Text = "Hours";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(3, 499);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(107, 20);
            this.label23.TabIndex = 74;
            this.label23.Text = "Email Delivery";
            // 
            // txt_EmailDelivery
            // 
            this.txt_EmailDelivery.Location = new System.Drawing.Point(123, 496);
            this.txt_EmailDelivery.Name = "txt_EmailDelivery";
            this.txt_EmailDelivery.Size = new System.Drawing.Size(464, 26);
            this.txt_EmailDelivery.TabIndex = 73;
            this.txt_EmailDelivery.Text = "mor.hosseini@gmail.com;m-hosseinia@agri-bank.com";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(3, 531);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(114, 20);
            this.label24.TabIndex = 76;
            this.label24.Text = "Mobile Delivery";
            // 
            // txt_MobileDelivery
            // 
            this.txt_MobileDelivery.Location = new System.Drawing.Point(123, 528);
            this.txt_MobileDelivery.Name = "txt_MobileDelivery";
            this.txt_MobileDelivery.Size = new System.Drawing.Size(464, 26);
            this.txt_MobileDelivery.TabIndex = 75;
            // 
            // chk_Email
            // 
            this.chk_Email.AutoSize = true;
            this.chk_Email.Checked = true;
            this.chk_Email.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Email.Location = new System.Drawing.Point(9, 563);
            this.chk_Email.Name = "chk_Email";
            this.chk_Email.Size = new System.Drawing.Size(74, 24);
            this.chk_Email.TabIndex = 77;
            this.chk_Email.Text = "Email";
            this.chk_Email.UseVisualStyleBackColor = true;
            // 
            // chk_SMS
            // 
            this.chk_SMS.AutoSize = true;
            this.chk_SMS.Location = new System.Drawing.Point(100, 563);
            this.chk_SMS.Name = "chk_SMS";
            this.chk_SMS.Size = new System.Drawing.Size(70, 24);
            this.chk_SMS.TabIndex = 78;
            this.chk_SMS.Text = "SMS";
            this.chk_SMS.UseVisualStyleBackColor = true;
            // 
            // chk_Notification
            // 
            this.chk_Notification.AutoSize = true;
            this.chk_Notification.Location = new System.Drawing.Point(186, 563);
            this.chk_Notification.Name = "chk_Notification";
            this.chk_Notification.Size = new System.Drawing.Size(114, 24);
            this.chk_Notification.TabIndex = 79;
            this.chk_Notification.Text = "Notification";
            this.chk_Notification.UseVisualStyleBackColor = true;
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button15.Location = new System.Drawing.Point(687, 556);
            this.button15.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(234, 35);
            this.button15.TabIndex = 83;
            this.button15.Text = "Safi Strategy";
            this.button15.UseVisualStyleBackColor = false;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button14
            // 
            this.button14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button14.Location = new System.Drawing.Point(687, 511);
            this.button14.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(234, 35);
            this.button14.TabIndex = 82;
            this.button14.Text = "Mohebi Strategy";
            this.button14.UseVisualStyleBackColor = false;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button13.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button13.Location = new System.Drawing.Point(687, 466);
            this.button13.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(234, 35);
            this.button13.TabIndex = 81;
            this.button13.Text = "Norouzi Strategy";
            this.button13.UseVisualStyleBackColor = false;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(687, 421);
            this.button12.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(234, 35);
            this.button12.TabIndex = 80;
            this.button12.Text = "Empty Strategy";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // frm_Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 602);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.chk_Notification);
            this.Controls.Add(this.chk_SMS);
            this.Controls.Add(this.chk_Email);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.txt_MobileDelivery);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.txt_EmailDelivery);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.txt_PeriodicalNotify);
            this.Controls.Add(this.chk_PeriodicalNotification);
            this.Controls.Add(this.chk_ProfitLossNotification);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txt_AutoOrderLockPeriod);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.txt_LossCount);
            this.Controls.Add(this.chk_LossCount);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txt_AutoOrderCoefficient);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txt_HigherPriceAmount);
            this.Controls.Add(this.chk_OrderInstedLoss);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txt_MaxActiveOrder);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txt_Wait);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txt_AutoOrderWatchPeriod);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txt_AutoOrderAmount);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txt_AutoOrderCount);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txt_AutoReloadCount);
            this.Controls.Add(this.chk_AutoReload);
            this.Controls.Add(this.chk_JustLog);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txt_Schedule);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txt_PairCoins);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txt_AcceptableLossLimit);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_AcceptableProfitLimit);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_Params);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_FeeRate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_LowBuyWatchPeriod);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_HighSellWatchPeriod);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_Secret);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_Key);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Name);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btn_Test);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frm_Test";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dll Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Test;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Key;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Secret;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_HighSellWatchPeriod;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_LowBuyWatchPeriod;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_FeeRate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_Params;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_AcceptableProfitLimit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_AcceptableLossLimit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_PairCoins;
        private System.Windows.Forms.TextBox txt_Schedule;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chk_JustLog;
        private System.Windows.Forms.CheckBox chk_AutoReload;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_AutoReloadCount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_AutoOrderCount;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_AutoOrderAmount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_AutoOrderWatchPeriod;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txt_Wait;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_MaxActiveOrder;
        private System.Windows.Forms.CheckBox chk_OrderInstedLoss;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txt_HigherPriceAmount;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txt_AutoOrderCoefficient;
        private System.Windows.Forms.CheckBox chk_LossCount;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txt_LossCount;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txt_AutoOrderLockPeriod;
        private System.Windows.Forms.CheckBox chk_ProfitLossNotification;
        private System.Windows.Forms.CheckBox chk_PeriodicalNotification;
        private System.Windows.Forms.TextBox txt_PeriodicalNotify;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txt_EmailDelivery;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txt_MobileDelivery;
        private System.Windows.Forms.CheckBox chk_Email;
        private System.Windows.Forms.CheckBox chk_SMS;
        private System.Windows.Forms.CheckBox chk_Notification;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button12;
    }
}

