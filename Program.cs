using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Dispatch.BaseUtil;
using System.Threading;
using System.Configuration;

namespace client_zk
{
    public static class GlobalVarForApp
    {       //存放程序的全局变量
        public static string client_type="";
        public static Queue<RSData> receiveMessageQueue = new Queue<RSData>();         //消息接收队列
        public static Queue<RSData> sendMessageQueue = new Queue<RSData>();            //消息发送队列
//        public static List<OrderInfo> orInfo = new List<OrderInfo>();                                         //存放未处理完成的所有调度令的信息
        public static List<OrderInfo> tbdOrdersInfo = new List<OrderInfo>();                              //存放未处理完成的所有调度令的信息
        public static string currentUserStr="";
        public static bool networkStatusBool =false;
        public static int server_port = 0;         //服务器ip及端口号
        public static string server_ip = "";
    }

    static class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
                       
            //读取配置文件
            var setting = ConfigurationManager.AppSettings; 
            if (setting["server_IP"] != null  &&  setting["server_Port"]!=null &&setting["client_type"]!=null)
            {
                GlobalVarForApp.client_type=setting["client_type"];
                GlobalVarForApp.server_ip = setting["server_IP"];
                GlobalVarForApp.server_port = Convert.ToInt32(setting["server_Port"]);
            }
            else
            {
                MessageBox.Show("配置文件读取失败,程序无法启动");
                appLog.exceptionRecord("配置文件读取失败,程序无法启动");
                return;
            }
            //开启网络通信线程
            Form1 f = new Form1();
            Thread network_thread = new Thread(network.thread);
            network_thread.Start(f);

            MessageBox.Show("Hello world");


            Application.Run(f);
            

        }
    }
}
