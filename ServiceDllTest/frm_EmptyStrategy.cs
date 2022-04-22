using CommonObjects;
using FrameworkConnectionInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace ServiceDllTest
{
    public partial class frm_EmptyStrategy : Form
    {

        public frm_EmptyStrategy()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> paramList = new List<string>();
            //-- Mandatory
            paramList.Add("Name=" + "SampleStrategy");
            paramList.Add("StrategyName=" + "EmptyStrategy");
            paramList.Add("JustLog=" + "True");
            //-- Params
            paramList.Add("PairCoins=BTCUSDT");
            paramList.Add("ScheduleTime=4");
            paramList.Add("Key=");
            paramList.Add("Secret=");
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
            paramList.Add("Name=SampleStrategy");

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

    }
}
