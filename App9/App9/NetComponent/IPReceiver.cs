using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace App9
{
    class IPReceiver
    {
        private static IPAddress remoteAddress;
        private static string IPaddress;

        private Label label;

        public IPReceiver(Label label)
        {
            this.label = label;
            Thread thread = new Thread(new ParameterizedThreadStart(ReceiveMessage));
            thread.Start(label);
        }

        public static void ReceiveMessage(object label)
        {
            remoteAddress = IPAddress.Parse("235.5.5.11");
            UdpClient receiver = new UdpClient(8005); // UdpClient для получения данных
            receiver.JoinMulticastGroup(remoteAddress, 20);
            IPEndPoint remoteIp = null;
            string message = "";
            try
            {
                while (true)
                {
                    byte[] data = receiver.Receive(ref remoteIp); // получаем данные
                    message = Encoding.Unicode.GetString(data);
                    Console.WriteLine(message);
                    if (message != "")
                    {
                        var vs = message.Split(':');
                        IPaddress = vs[1];
                        Label ownLabel = (Label)label;
                        ownLabel.Text = message;
                        break;
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                receiver.Close();
            }
        }

        public static IPAddress GetIP()
        {
            IPAddress ip;
            ip = IPAddress.Parse(IPaddress);
            return ip;
        }
    }
}
