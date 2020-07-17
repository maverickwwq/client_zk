using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;
using client_zk;

namespace client_zk
{
    
   public  class SocketClient
    {
        public static Socket client;
        public static IPAddress serverIPAddress = IPAddress.Parse(GlobalVarForApp.server_ip);
        public static int serverPort = Convert.ToInt32(GlobalVarForApp.server_port);
        public static Thread connectThread,receiveMessageThread,sendMessageThread,handleMessageThread;//连接线程
        public static Queue<string> receiveMessageQueue = new Queue<string>();//收到的消息队列
        public static Queue<RSData> sendMessageQueue = new Queue<RSData>();//需要发送的消息队列
        public static bool socketRun = true;
        public static Form mainForm;
        public SocketClient()
        {
            connectThread = new Thread(ConnectServer);
            connectThread.Start();
        }

        //连接服务器的线程
        public static void ConnectServer()
        {
            while (socketRun)
            {
                try
                {
                    client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    client.Connect(new IPEndPoint(serverIPAddress, serverPort)); //配置服务器IP与端口  
                    Console.WriteLine("连接服务器成功");
                    receiveMessageThread = new Thread(ReceiveMessage);
                    sendMessageThread = new Thread(SendMessage);
                    handleMessageThread = new Thread(HandleMessage);
                    receiveMessageThread.Start();
                    sendMessageThread.Start();
                    handleMessageThread.Start();
                    connectThread.Suspend();//挂起该线程
                }
                catch(Exception ex)
                {
                    Thread.Sleep(3000);//连接失败3秒后重试
                }  
            }
        }
        
        //监听服务器数据的线程
        public static void ReceiveMessage()
        {
            string jsonStr = "";
            while (socketRun)
            {
                try
                {
                    //通过clientSocket接收数据  
                    byte[] result = new byte[4096];
                    int receiveNum = client.Receive(result);
                    string receiveData = Encoding.UTF8.GetString(result, 0, receiveNum);
                    if (receiveNum != 0)
                    {
                        jsonStr += receiveData;
                        if (jsonStr.IndexOf("DataEnd") != -1)
                        {
                            string messageStr = jsonStr.Substring(0, jsonStr.IndexOf("DataEnd"));
                            lock (receiveMessageQueue) { receiveMessageQueue.Enqueue(messageStr); }
                            Console.WriteLine("收到服务器数据：" + messageStr);
                            if (jsonStr.IndexOf("DataEnd") + 7 != jsonStr.Length)
                                jsonStr = jsonStr.Substring(jsonStr.IndexOf("DataEnd") + 7, jsonStr.Length);
                            else jsonStr = "";
                        }
                    }
                    else Thread.Sleep(2000);
                }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        if (client != null && client.Connected)
                        {
                            client.Shutdown(SocketShutdown.Both);
                            client.Close();
                            Console.WriteLine("与服务器断开连接");
                            break;
                        }
                        jsonStr = "";
                    }
              }
            if (socketRun && connectThread.ThreadState != ThreadState.Running)
                connectThread.Resume();//启动连接服务器的线程
        }
        //发送客户端数据的线程
        public static void SendMessage()
        {
            while (socketRun)
            {
                RSData rsData = null;
                lock (sendMessageQueue)
                {
                    if (sendMessageQueue.Count > 0)
                        rsData = sendMessageQueue.Dequeue();
                }
                if (rsData != null)
                {
                    string sendStr = Newtonsoft.Json.JsonConvert.SerializeObject(rsData) + "DataEnd";
                    //获取字符长度
                    int m_length = sendStr.Length;
                    byte[] data=new byte[m_length];
                    //转为字节流
                    data = Encoding.UTF8.GetBytes(sendStr);
                    try
                    {
                        //将字节流发给服务端
                        int i = client.Send(data);
                    }
                    catch (Exception ex)
                    {
                        if (client != null && client.Connected)
                        {
                            client.Shutdown(SocketShutdown.Both);
                            client.Close();
                            Console.WriteLine("与服务器断开连接");
                            break;
                        }
                        Console.WriteLine(ex.Message);
                    }
                }
                else Thread.Sleep(2000);
            }
             if (socketRun && connectThread.ThreadState != ThreadState.Running)
                connectThread.Resume();//启动连接服务器的线程
        }

        //处理服务器数据的线程
        public static void HandleMessage()
        {
            while (socketRun)
            {
                string jsonStr = "";
                lock (receiveMessageQueue)
                {
                    if (receiveMessageQueue.Count > 0)
                        jsonStr = receiveMessageQueue.Dequeue();
                }
                //将收到的数据分配给各个线程进行处理
                if (jsonStr.Equals("") == false)
                {
                    RSData receiveRsData = Newtonsoft.Json.JsonConvert.DeserializeObject<RSData>(jsonStr);
                    /*                 if (receiveRsData.CommType.Equals(CommUtil.COMM_TYPE_DOWN_ORDER))
                                     {
                                         //
                                     }
                                 }
                                 else Thread.Sleep(2000);*/
                }
            }
        }
    }
}
