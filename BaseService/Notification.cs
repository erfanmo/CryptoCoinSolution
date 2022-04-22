using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FrameworkBaseService
{
    public class Notification
    {

        private string _fromAddress = "TraidingNotification@gmail.com";
        private string _fromPass = "Traider@Notify-AlertSchedule#";
        private string _host = "smtp.gmail.com";
        private string _port = "587";

        public Thread thread = null;
        private int _sleepTime = 1000;
        private ManualResetEvent _mre = new ManualResetEvent(false);

        private readonly object _lockObj = new object();

        private Queue<List<string>> _queue = new Queue<List<string>>();

        public Notification()
        {
            
        }

        public List<string> QueueAction(List<string> parameters, string action)
        {
            List<string> result = null;
            lock (_lockObj)
            {
                if (action == "ENQUEUE")
                {
                    _queue.Enqueue(parameters);
                }
                else if (action == "DEQUEUE")
                {
                    result = _queue.Dequeue();
                }
            }
            return (result);
        }

        public void DoJob()
        {
            while (true)
            {
                try
                {
                    List<string> parameters = QueueAction(null, "DEQUEUE");
                    switch (parameters[0])
                    {
                        case "EMAIL":
                            SendEmail(parameters[1], parameters[2], parameters[3]);
                            break;
                        case "SMS":

                            break;
                        case "NOTIFICATION":

                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    _mre.WaitOne(_sleepTime);
                    _mre.Reset();
                }
            }
        }

        public void SendEmail(string addressList, string subject, string body)
        {
            string[] lstAddress = addressList.Split(';');
            foreach (string toAddress in lstAddress)
            {
                try
                {
                    var fromAddress = new MailAddress(_fromAddress);
                    string fromPassword = _fromPass;
                    var smtp = new SmtpClient
                    {
                        Host = _host,
                        Port = int.Parse(_port),
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };
                    using (var message = new MailMessage(_fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                }
                catch(Exception ex)
                { }
            }
        }

    }
}
