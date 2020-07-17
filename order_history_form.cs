using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace client_zk
{
    public partial class order_history_form : Form
    {
        public order_history_form()
        {
            InitializeComponent();


            orderHistory_dgv.ReadOnly = true;
            orderHistory_dgv.AllowUserToAddRows = false;
            orderHistory_dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            orderHistory_dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            orderHistory_dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            orderHistory_dgv.RowHeadersVisible = false;
            orderHistory_dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            orderHistory_dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            orderHistory_dgv.ColumnHeadersDefaultCellStyle.Font = new Font(orderHistory_dgv.Font, FontStyle.Bold);
            orderHistory_dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            //           orderHistory_dgv.GridColor = Color.Black;
            orderHistory_dgv.ColumnCount = 10;
            orderHistory_dgv.Columns[9].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //序号 单号 下发日期 机号 天线号 频率 周期 开始日期 结束日期

            orderHistory_dgv.Columns[0].Name = "序号";
            orderHistory_dgv.Columns[1].Name = "单号";
            orderHistory_dgv.Columns[2].Name = "下发日期";
            orderHistory_dgv.Columns[3].Name = "机号";
            orderHistory_dgv.Columns[4].Name = "天线号";
            orderHistory_dgv.Columns[5].Name = "频率";
            orderHistory_dgv.Columns[6].Name = "周期";
            orderHistory_dgv.Columns[7].Name = "开始日期";
            orderHistory_dgv.Columns[8].Name = "结束日期";

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void order_history_form_Load(object sender, EventArgs e)
        {

        }
    }
}
