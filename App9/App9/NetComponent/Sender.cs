using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using KipLib;

namespace App9
{
    public static class Sender
    {
        public static void PrepareAndSend()
        {
           MemoryStream stream = SerializeToMemory(Items.GetItems);
           Send(stream);
        }

        private static MemoryStream SerializeToMemory(List<Items> items)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, items);
            stream.Position = 0;
            return stream;
        }

        public static void Send(MemoryStream binaryStream)
        {
            TcpClient client = null;
            try
            {
                //IPAddress address = IPAddress.Parse("192.168.0.37");
                IPAddress address = IPReceiver.GetIP();
                IPEndPoint ipPoint = new IPEndPoint(address, 8005);

                client = new TcpClient();
                client.Connect(ipPoint);
                //NetworkStream stream = client.GetStream();

                using (MemoryStream ms = new MemoryStream())
                {
                    binaryStream.CopyTo(ms);
                    byte[] messsize = BitConverter.GetBytes(ms.Length);
                    NetworkStream ns = client.GetStream();
                    ns.Write(messsize, 0, messsize.Length);
                    ms.Position = 0;
                    ms.CopyTo(ns);
                }
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }

        public static void GetBack(MemoryStream stream)
        {
            Items.GetItems = DeSerializeFromMemory(stream);
            Console.WriteLine("Получено элементов: " + Items.GetItems.Count);
        }

        private static List<Items> DeSerializeFromMemory(MemoryStream stream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return (List<Items>)formatter.Deserialize(stream);
        }
    }
}


/*
Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
socket.Connect(ipPoint);

                
socket.Send(data);

responce = new byte[256];
StringBuilder builder = new StringBuilder();
int bytes = 0; 

do
{
    bytes = socket.Receive(data, data.Length, 0);
    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
}
while (socket.Available > 0);
*/

/*
Console.WriteLine("ответ сервера: " + builder.ToString());
socket.Shutdown(SocketShutdown.Both);
socket.Close();*/
