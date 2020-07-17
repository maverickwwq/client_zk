using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using client_zk;
using System.Windows.Forms;
using System.Timers;
using Newtonsoft.Json;


namespace client_zk
{
    class network
    {
            static private int svr_port = 0;
            static private string svr_ip = "";
            static private int messageCount;
            static private string message="";
            static private byte[] messageBuf = new byte[10000];
            static private Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Socket socketLtn=new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            public static UTF8Encoding u8 =new UTF8Encoding();

            static public void networkInitialize(object f)
            {
                Form1 tmp = (Form1)f;
                svr_port = GlobalVarForApp.server_port;
                svr_ip = GlobalVarForApp.server_ip;
                System.Timers.Timer aTimer = new System.Timers.Timer(2000);
                aTimer.AutoReset = true;
                aTimer.Enabled = true;

                bool state = true;
                try
                {
                    listenSocket.Connect(new IPEndPoint(IPAddress.Parse(svr_ip), svr_port));
                }
                catch (Exception e)
                {
                    state = false;
                    MessageBox.Show("网络初始化异常");
                }
                finally
                {
                    GlobalVarForApp.networkStatusBool = state;                   
                }
               
            }

            private static void OnTimedEvent(Object source, ElapsedEventArgs e)
            {
                Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                                  e.SignalTime);
            }

            public static void receiveDataProc(Object f)
            {
                //listenSocket.Connect(new IPEndPoint(IPAddress.Parse(svr_ip), svr_port));
                Form1 tmp=(Form1) f;
                while(GlobalVarForApp.networkStatusBool){

                    messageCount = listenSocket.Receive(messageBuf);
                    message+=u8.GetString(messageBuf);
                    int a=message.IndexOf("DataEnd");
                    if (a != -1)
                    {
                        try
                        {
                            GlobalVarForApp.receiveMessageQueue.Enqueue(JsonConvert.DeserializeObject<RSData>(message.Substring(0, a)));
                            message = message.Substring(a + 7);
                            tmp.messageHandle();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }
                }
                return;
            }

            public static void sendRSDataProc(RSData data)
            {
                //listenSocket.Connect(new IPEndPoint(IPAddress.Parse(svr_ip), svr_port));
                //Form1 tmp = (Form1)f;
                while (GlobalVarForApp.networkStatusBool)       //  listenSocket is volid ??
                {


 /*                 messageCount = listenSocket.Receive(messageBuf);
                    message += u8.GetString(messageBuf);
                    int a = message.IndexOf("DataEnd");
                    if (a != -1)
                    {
                        try
                        {
                            GlobalVarForApp.receiveMessageQueue.Enqueue(JsonConvert.DeserializeObject<RSData>(message.Substring(0, a)));
                            message = message.Substring(a + 7);
                            tmp.messageHandle();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }*/
                }
                return;
            }


            //        public bool sendData(Socket 

            public static void thread(Object f)
            {
                networkInitialize(f);
                try
                {//程序在这里循环 监听 网络消息
                    while (true)
                    {
                        //接收服务器发送的消息
                        //判断服务器发送的消息类型
                        //对不同类型的消息进行分类处理
                        MessageBox.Show("listen");
                        network.receiveDataProc(f);

                    }
                }
                catch (Exception e)
                {
                    //MessageBox.Show("Network error!");
                }
                finally
                {
                    //System.Environment.Exit(0);
                }
            }
        }
}
