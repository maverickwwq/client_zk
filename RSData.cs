using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dispatch;

namespace client_zk
{
    public class RSData
    {
        
        public string CommType = "";//通讯类型的定义
        public string CommTime = "";//通讯时间的定义
        public string CommDept = "";//通讯机房名称：甲机房7830、乙机房7831，中控机房7850

        //调令相关的操作变量
        public Order order = null;//下发的调度令
        public List<OrderOp> orderOpList = null;//调度指令集
        public List<OrderRecord> orderRecordList = null;//调令跟踪记录
        public List<OrderAndOp> orderAndOpList = null;//调令综合信息

        //查询请求的操作变量
        public int pageSize = 0;//每页记录数
        public int pageIndex = 0;//当前页码
        public int totalPages = 0;//总页数
        public int totalRecords = 0;//记录总条数
        public string queryOrderODID = "";//查询调令的OD_ID
        public string queryStartTime = "";//查询收到调令的开始时间
        public string queryEndTime = "";//查询收到调令的结束时间
        public string queryOrderStatus = "";//查询调令的状态
        

        public RSData()
        {
        }
    }
}
