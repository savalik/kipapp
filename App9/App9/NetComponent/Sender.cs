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
        public static void PrepareAndSend(object sendingEvent)
        {
           MemoryStream stream = SerializeToMemory(Items.GetItems);
           Send(stream, (byte)sendingEvent);
        }

        private static MemoryStream SerializeToMemory(List<Items> items)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, items);
            stream.Position = 0;
            return stream;
        }

        public static void Send(MemoryStream binaryStream, byte sendingEvent)
        {
            TcpClient client = null;
            try
            {
                //IPAddress address = IPAddress.Parse("192.168.0.37");
                IPAddress address = IPReceiver.GetIP();
                IPEndPoint ipPoint = new IPEndPoint(address, 8005);

                client = new TcpClient();
                client.Connect(ipPoint);

                using (MemoryStream ms = new MemoryStream())
                {
                    binaryStream.CopyTo(ms);
                    byte[] messsize = BitConverter.GetBytes(ms.Length);
                    byte[] receivedEvent = new byte[1];
                    receivedEvent[0] = sendingEvent;
                    NetworkStream ns = client.GetStream();
                    ns.Write(receivedEvent, 0, 1);
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
