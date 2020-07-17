using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using Dispatch;
using System.Text.RegularExpressions;

namespace client_zk
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        //窗口加载
        private void Form1_Load(object sender, EventArgs e)
        {
            //主窗口大小设置
            this.Width = 1600;
            this.Height = 900;

            //左侧调度令显示框设置
            tbd_lb.DrawMode = DrawMode.OwnerDrawFixed;
            tbd_lb.DrawItem += new DrawItemEventHandler(tbd_lb_drawItem);
            tbd_lb.Font= new Font("Arial", 9);
            tbd_lb.MouseDoubleClick+=new MouseEventHandler(tbd_lb_MouseDoubleClick);
            tbd_lb.ItemHeight = 30;
            this.FormClosing += new FormClosingEventHandler(MainForm_Closing);

            //调度令表格显示格式
            orderInfo_dgv.ReadOnly = true;
            orderInfo_dgv.AllowUserToAddRows = false;
            orderInfo_dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            orderInfo_dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            orderInfo_dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            orderInfo_dgv.RowHeadersVisible = false;
            orderInfo_dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            orderInfo_dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            orderInfo_dgv.ColumnHeadersDefaultCellStyle.Font = new Font(orderInfo_dgv.Font, FontStyle.Bold);
            orderInfo_dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            //           orderInfo_dgv.GridColor = Color.Black;
            orderInfo_dgv.ColumnCount = 17;
            orderInfo_dgv.Columns[16].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            //初始化显示
            displayNullOrderInfo();
            //OrInfo_btn.Font.
        }

        private void displayNullOrderInfo()
        {
            label1.Text = "--";
            label2.Text = "--";
            label3.Text = "--";

            orderInfo_dgv.Columns[0].Name = "序号";
            orderInfo_dgv.Columns[1].Name = "机号";
            orderInfo_dgv.Columns[2].Name = "功率";
            orderInfo_dgv.Columns[3].Name = "播音时间";
            orderInfo_dgv.Columns[4].Name = "频率";
            orderInfo_dgv.Columns[5].Name = "节目";
            orderInfo_dgv.Columns[6].Name = "通道";
            orderInfo_dgv.Columns[7].Name = "天线";
            orderInfo_dgv.Columns[8].Name = "程式";
            orderInfo_dgv.Columns[9].Name = "方向";
            orderInfo_dgv.Columns[10].Name = "服务区";
            orderInfo_dgv.Columns[11].Name = "操作";
            orderInfo_dgv.Columns[12].Name = "周期";
            orderInfo_dgv.Columns[13].Name = "开始日期";
            orderInfo_dgv.Columns[14].Name = "结束日期";
            orderInfo_dgv.Columns[15].Name = "业务";
            orderInfo_dgv.Columns[16].Name = "备注";
        }

        //排序函数   对消息队列的调度令进行排序，按调令文号增序排列
        private static int CompareOrderByOrderID(OrderInfo a, OrderInfo b)
        {
            if (a.orInfo.OD_ID > b.orInfo.OD_ID)
                return 1;
            else if (a.orInfo.OD_ID < b.orInfo.OD_ID)
                return -1;
            else
                return 0;
        }

        //消息处理函数
        public void messageHandle() 
        {
            RSData tmp=new RSData();
            while (GlobalVarForApp.receiveMessageQueue.Count > 0)  //队列中有消息进行处理
            {         
                //foreach( RSData  rsdData in receiveQueue)
                //{   //
                    //"LOGIN_REPLY"     "ADD_USER_REPLY"      "DELETE_USER_REPLY"
                    //"DOWN_ORDER"      "QUERY_ORDER_REPLY"     "NEW_MESSAGE"
                tmp = GlobalVarForApp.receiveMessageQueue.Dequeue();
                    switch (tmp.CommType.Trim())
                    {
                        case "LOGIN_REPLY":
                            break;

                        case "ADD_USER_REPLY":
                            break;

                        case "DELETE_USER_REPLY":
                            break;

                        case "DOWN_ORDER":
                            OrderInfo tmpOrInfo = new OrderInfo();      //调度令信息
                            //提取调度令信息
                            tmpOrInfo.orInfo = tmp.order;
                            tmpOrInfo.orRc = tmp.orderRecordList;
                            tmpOrInfo.orOp = tmp.orderOpList;
                            tmpOrInfo.orderStatus_enum = orderStatus.unconfirmed;        //设置调度令状态信息    未接收确认状态
                            GlobalVarForApp.tbdOrdersInfo.Add(tmpOrInfo);                      //添加到调度令信息

                            //对orInfo全局变量按调度令号进行排序
                            if (GlobalVarForApp.tbdOrdersInfo.Count > 1)
                            {
                                GlobalVarForApp.tbdOrdersInfo.Sort(CompareOrderByOrderID);
                            }
                            //od_dis.od_dis_show();            //新调度令显示                           
                            tbd_lb.BeginUpdate();
                            tbd_lb.Items.Clear();
                            int count = 0;
                            foreach(OrderInfo tmpOrderInfo in GlobalVarForApp.tbdOrdersInfo)
                            {
                                count++;
                                tbd_lb.Items.Add("#"+count.ToString()+"广无调单字【"+tmpOrderInfo.orInfo.ORDER_YEAR+"】"+tmpOrderInfo.orInfo.ORDER_CODE);
                            }
                            tbd_lb.EndUpdate();
 
                            
                            //
                            //接收到新调度语音提示
                            //

                            break;


                        case "QUERY_ORDER_REPLY":
                            break;


                        case "NEW_MESSAGE":
                            break;

                        
                        default: /* 可选的 */
                            break; 

                    }   
               // }
            }

        }


        //对调度令的调度指令根据序号进行增序排列
        private static int CompareOrOpByNum(OrderOp a, OrderOp b)
        {
            if (a.NUM > b.NUM)
                return 1;
            else if (a.NUM < b.NUM)
                return -1;
            else
                return 0;
        }

        //选中某一调度令后显示处理函数
        private void tbd_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  display order info
            int selectedIndex = tbd_lb.SelectedIndex;           //鼠标选择的调度令的index
            if (selectedIndex == -1)                                       //未选中调度令则返回
                return;
            OrderInfo tmpOrIn = GlobalVarForApp.tbdOrdersInfo[selectedIndex];//

            //显示文号
            label1.Text = "广无调单字【"+tmpOrIn.orInfo.ORDER_YEAR.ToString()+"】"+tmpOrIn.orInfo.ORDER_CODE;
            label2.Text = "582丁机房填表人 "+tmpOrIn.orInfo.SENDER.Trim() ;
            label3.Text = "下发时间 "+tmpOrIn.orInfo.SEND_DATE.ToString();
            //调度 指令排序
            if(tmpOrIn.orderStatus_enum==orderStatus.unconfirmed){          //调度令未下发状态
                if (tmpOrIn.orOp.Count > 1)
                {
                    GlobalVarForApp.tbdOrdersInfo[selectedIndex].orOp.Sort(CompareOrOpByNum);
                }
            }
            //清空显示，重新显示
            orderInfo_dgv.Rows.Clear();
            OrderOp tmpOrOp=new OrderOp();
            //显示所有调度指令
            for (int j = 0; j < GlobalVarForApp.tbdOrdersInfo[selectedIndex].orOp.Count; j++)
            {
                tmpOrOp = tmpOrIn.orOp[j];
                orderInfo_dgv.Rows.Add(1);
                orderInfo_dgv.Rows[j].Cells[0].Value=tmpOrOp.NUM.ToString();        //序号    
                if (tmpOrOp.TR_ID == 7)                                                                     //机号
                    orderInfo_dgv.Rows[j].Cells[1].Value = "B01";
                else
                    orderInfo_dgv.Rows[j].Cells[1].Value="A0"+tmpOrOp.TR_ID.ToString();
                orderInfo_dgv.Rows[j].Cells[2].Value=tmpOrOp.POWER.ToString();//功率
                orderInfo_dgv.Rows[j].Cells[3].Value=tmpOrOp.START_TIME+"-"+tmpOrOp.END_TIME;//开始结束时间
                orderInfo_dgv.Rows[j].Cells[4].Value=tmpOrOp.FREQ.ToString();   //频率
                orderInfo_dgv.Rows[j].Cells[5].Value=tmpOrOp.CHANNEL.ToString();//节目
                orderInfo_dgv.Rows[j].Cells[6].Value=tmpOrOp.CHANNEL.ToString();//通道
                if (tmpOrOp.AN_ID == 11)                                                                      //天线
                {
                    orderInfo_dgv.Rows[j].Cells[7].Value = "201";
                }
                else
                    orderInfo_dgv.Rows[j].Cells[7].Value = (100 + tmpOrOp.AN_ID).ToString();
                orderInfo_dgv.Rows[j].Cells[8].Value=tmpOrOp.ANT_PROG.ToString();    //程式
                orderInfo_dgv.Rows[j].Cells[9].Value=tmpOrOp.AZIMUTH_M;                     //方向
                orderInfo_dgv.Rows[j].Cells[10].Value=tmpOrOp.SERV_AREA;                 //服务区             ？？？？？
                if (tmpOrOp.OPERATE.Trim() == "1")                                                         //操作
                    orderInfo_dgv.Rows[j].Cells[11].Value = "开";
                else
                    orderInfo_dgv.Rows[j].Cells[11].Value = "停";
                orderInfo_dgv.Rows[j].Cells[12].Value=tmpOrOp.DAYS.ToString();            //周期
                //Regex dateRegex = new Regex("d{4}{-,/,\}d{2}{-,/,\}d{2}");
                Regex dateReg = new Regex(@"[0-9]{4}[-/\\][0-9]{1,2}[-/\\][0-9]{1,2}");
                MatchCollection dateMatch = dateReg.Matches(tmpOrOp.START_DATE);
                try
                {
                    orderInfo_dgv.Rows[j].Cells[13].Value = dateMatch[0]; //开始时间
                }
                catch(Exception a){
                    Console.WriteLine(a.Message);
                }
                dateMatch = dateReg.Matches(tmpOrOp.END_DATE.ToString());             
                try
                {
                    orderInfo_dgv.Rows[j].Cells[14].Value = dateMatch[0]; //结束时间
                }
                catch(Exception a){
                    Console.WriteLine(a.Message);
                }
                orderInfo_dgv.Rows[j].Cells[15].Value="";   //业务    对内广播？对外广播？实验？
                orderInfo_dgv.Rows[j].Cells[16].Value = "";    //备注
            }
            switch (tmpOrIn.orderStatus_enum)
            {
                case orderStatus.unconfirmed:       //
                    OrInfo_btn.Text = "确认接收";
                    OrInfo_btn.Enabled = true;
                    break;
                /*case orderStatus.confirmed:         //已接收
                    OrInfo_btn.Text = "反馈执行情况";
                    OrInfo_btn.Enabled = false;
                    break;*/

                case orderStatus.noFeedbackYet:      //待反馈
                    OrInfo_btn.Text = "反馈执行情况";
                    OrInfo_btn.Enabled =true;
                    break;
                case orderStatus.feedback://已反馈未确认
                    OrInfo_btn.Text = "已完成";
                    OrInfo_btn.Enabled =false;
                    break;
            }
        }

        private void hd_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  display order info
        }

        private void orderHistory_btn_Click(object sender, EventArgs e)
        {
            order_history_form o1 = new order_history_form();
            o1.Show();
        }

        private void OrderDeliver_Click(object sender, EventArgs e)
        {
            Order newOrder = new Order(28361019,2020,"0149395","正常调度令","待下发","2020/05/15 12:29:30","董晓萌","2020/05/15 12:27:30","郝佳","无线局调度中心","王伟强","2020/05/15 12:32:00","校对人","校对时间","备注","","","","","","","","",0);
            List<OrderOp>  newOrderOp =new List<OrderOp>();
            OrderOp oop1 = new OrderOp(28362023, 28361019, 2, "正常调度令", 7, 11, "2020/05/15 0:0:0", "2020/10/27 0:0:0", "08:00:00", "10:00:00",
                6230, 450, 1, "369", "", "HR2/0.5/4", "1", 0, "西藏", "1234567", "", "1", "", "", "", "", "", "", "", "", "", "", "");
            newOrderOp.Add(oop1);
            OrderOp oop2 = new OrderOp(28362024, 28361019, 1, "正常调度令", 1, 1, "2020/05/15 0:0:0", "2020/10/27 0:0:0", "08:00:00", "10:00:00",
                6230, 100,1, "313", "", "HR2/0.5/4", "1", 0, "西藏", "1234567", "", "1", "", "", "", "", "", "", "", "", "", "", "");
            newOrderOp.Add(oop2);
            string netSimulateData_s = JsonConvert.SerializeObject(newOrderOp);
            //textBox1.Text = netSimulateData_s;
            List<OrderOp> OrderOp_r = JsonConvert.DeserializeObject<List<OrderOp>>(netSimulateData_s);
            //int orderOp_count = 0;
            MessageBox.Show(OrderOp_r.Count().ToString());
        }

        private void tbd_lb_drawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            if (e.Index != -1)
            {
                e.Graphics.DrawString(tbd_lb.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
                // If the ListBox has focus, draw a focus rectangle around the selected item.
                e.DrawFocusRectangle();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


            //点击关闭按钮后，程序关闭所有线程
        private void MainForm_Closing(object sender, CancelEventArgs e)
        {
            MessageBox.Show("This is the first thing I want know!");
            System.Environment.Exit(0);
        }

        private void OrInfo_btn_Click(object sender, EventArgs e)
        {
            int selectedIndex = tbd_lb.SelectedIndex;
            if (OrInfo_btn.Text == "确认接收")           //点击下发
            {
               //send message
               //set orderstatus set button value

                OrInfo_btn.Text = "反馈执行情况";
                GlobalVarForApp.tbdOrdersInfo[selectedIndex].orderStatus_enum = orderStatus.noFeedbackYet;
                OrInfo_btn.Enabled =true;
            }

            else if (OrInfo_btn.Text == "反馈执行情况")//点击反馈执行情况
            {
                OrInfo_btn.Text = "已反馈";
                GlobalVarForApp.tbdOrdersInfo[selectedIndex].orderStatus_enum = orderStatus.feedback;
                OrInfo_btn.Enabled = false;
                feedbackTimeSelect ft = new feedbackTimeSelect();
                ft.Show();
            }
        }

        private void tbd_lb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int i = tbd_lb.IndexFromPoint(e.Location);
            MessageBox.Show(i.ToString());
            if (i != ListBox.NoMatches)
                MessageBox.Show(tbd_lb.Items[i].ToString());
            if (tbd_lb.SelectedIndex == -1)
            {
                //do nothing
            }
            else
                MessageBox.Show("Hello Nihao");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Application exit");
            Application.Exit();
        }
     }

    static public class orInfo_btn_CN
    {
        public static string    undelivered="下发";
        public static string    delivered_unanswered="已下发，待接收";
        public static string    answered="已接收，待反馈";
        public static string    unconfirmed="确认反馈";
    }


    public enum orderStatus 
    {   
        unconfirmed,                 //未确认接收
        confirmed,                       //已确认接收未反馈
        noFeedbackYet,               //未反馈
        feedback                            //已反馈

    };
    public class OrderInfo
    {

        public Order orInfo;
        public List<OrderOp> orOp;
        public List<OrderRecord> orRc;
        public orderStatus orderStatus_enum;
    }


}
