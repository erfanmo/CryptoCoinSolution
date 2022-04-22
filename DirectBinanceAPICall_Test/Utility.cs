using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DirectBinanceAPICall
{
    public class Utility
    {
        public static bool LogToFile(string input, string method)
        {
            try
            {
                string format = "yyyy_MM_dd_hhmmsstt";
                string fileName = Environment.CurrentDirectory+"\\Log\\"+ method + "_" + DateTime.Now.ToString(format) + ".txt";
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.WriteLine(input);
                    writer.Close();
                }
            }
            catch (Exception exp)
            {
                return false;
            }
            return true;
        }

        public static string GetUserCountryByIp(string ip)
        {
            IpInfo ipInfo = new IpInfo();
            try
            {
                string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
            }
            catch (Exception)
            {
                ipInfo.Country = null;
            }

            return ipInfo.Country;
        }

        public static string GetIPAddress()
        {
            String address = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);

            return address;
        }

        public static IpInfo GetIPInfo()
        {
            string _myLocalIP = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
            string _myPublicIP = Utility.GetIPAddress();
            IpInfo ipInfo = new IpInfo();
            try
            {
                string info = new WebClient().DownloadString("http://ipinfo.io/" + _myPublicIP);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                ipInfo.LocalIp = _myLocalIP;
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
            }
            catch (Exception)
            {
                ipInfo.Country = null;
            }

            return ipInfo;
        }
    }

    public class IpInfo
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }
        
        [JsonProperty("localip")]
        public string LocalIp { get; set; }

        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("loc")]
        public string Loc { get; set; }

        [JsonProperty("org")]
        public string Org { get; set; }

        [JsonProperty("postal")]
        public string Postal { get; set; }
    }

    public interface INotification
    {
        void Notify(string subject, string message);
    }

    public class MailService : INotification
    {
        readonly string _address;
        public MailService(string address)
        {
            _address = address;
        }

        public void Notify(string subject, string message)
        {
            try
            {

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("xxxxx@gmail.com", "xxxxx"),
                    EnableSsl = true,
                };

                smtpClient.Send("xxxxx@gmail.com", _address, subject, message);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    public class SmsService : INotification
    {
        readonly string _phoneNumber;
        public SmsService(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

        public void Notify(string subject, string message)
        {
            // send sms
        }
    }

    public class MailSmsService: INotification
    {
        readonly string _address;
        readonly string _phoneNumber;
        public MailSmsService(string address, string phoneNumber)
        {
            _address = address;
            _phoneNumber = phoneNumber;
        }

        public void Notify(string subject, string message)
        {
            // send mail and sms
        }
    }
}
