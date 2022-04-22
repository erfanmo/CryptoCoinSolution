using CommonObjects;
using FrameworkBaseService;
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

namespace ServiceTest
{
    public partial class frm_MohebiStrategy : Form
    {

        ServiceOperation serviceOperation = null;

        public frm_MohebiStrategy(ServiceOperation _serviceOperation)
        {
            InitializeComponent();
            serviceOperation = _serviceOperation;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> paramList = new List<string>();
            //-- Mandatory
            paramList.Add("Name=" + "SampleStrategyMohebi");
            paramList.Add("StrategyName=" + "MohebiStrategy");
            paramList.Add("JustLog=" + "True");
            //-- Params
            paramList.Add("PairCoins=BTCUSDT");
            paramList.Add("Key=");
            paramList.Add("Secret=");
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
            paramList.Add("Name=" + "SampleStrategyMohebi");

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
    }
}
