namespace client_zk
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.toBeDone_gpb = new System.Windows.Forms.GroupBox();
            this.tbd_lb = new System.Windows.Forms.ListBox();
            this.hadDone_gpb = new System.Windows.Forms.GroupBox();
            this.hd_lb = new System.Windows.Forms.ListBox();
            this.orderHistory_btn = new System.Windows.Forms.Button();
            this.OrderDeliver = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.OrInfo_btn = new System.Windows.Forms.Button();
            this.orderInfo_dgv = new System.Windows.Forms.DataGridView();
            this.netStatus_lb = new System.Windows.Forms.Label();
            this.toBeDone_gpb.SuspendLayout();
            this.hadDone_gpb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderInfo_dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // toBeDone_gpb
            // 
            this.toBeDone_gpb.Controls.Add(this.tbd_lb);
            this.toBeDone_gpb.Location = new System.Drawing.Point(12, 26);
            this.toBeDone_gpb.Name = "toBeDone_gpb";
            this.toBeDone_gpb.Size = new System.Drawing.Size(240, 308);
            this.toBeDone_gpb.TabIndex = 0;
            this.toBeDone_gpb.TabStop = false;
            this.toBeDone_gpb.Text = "待处理";
            // 
            // tbd_lb
            // 
            this.tbd_lb.FormattingEnabled = true;
            this.tbd_lb.ItemHeight = 12;
            this.tbd_lb.Location = new System.Drawing.Point(7, 15);
            this.tbd_lb.Name = "tbd_lb";
            this.tbd_lb.Size = new System.Drawing.Size(227, 280);
            this.tbd_lb.TabIndex = 0;
            this.tbd_lb.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tbd_lb_drawItem);
            this.tbd_lb.SelectedIndexChanged += new System.EventHandler(this.tbd_lb_SelectedIndexChanged);
            // 
            // hadDone_gpb
            // 
            this.hadDone_gpb.Controls.Add(this.hd_lb);
            this.hadDone_gpb.Location = new System.Drawing.Point(13, 352);
            this.hadDone_gpb.Name = "hadDone_gpb";
            this.hadDone_gpb.Size = new System.Drawing.Size(239, 303);
            this.hadDone_gpb.TabIndex = 1;
            this.hadDone_gpb.TabStop = false;
            this.hadDone_gpb.Text = "已处理";
            // 
            // hd_lb
            // 
            this.hd_lb.FormattingEnabled = true;
            this.hd_lb.ItemHeight = 12;
            this.hd_lb.Location = new System.Drawing.Point(6, 15);
            this.hd_lb.Name = "hd_lb";
            this.hd_lb.Size = new System.Drawing.Size(227, 280);
            this.hd_lb.TabIndex = 0;
            this.hd_lb.SelectedIndexChanged += new System.EventHandler(this.hd_lb_SelectedIndexChanged);
            // 
            // orderHistory_btn
            // 
            this.orderHistory_btn.Location = new System.Drawing.Point(19, 674);
            this.orderHistory_btn.Name = "orderHistory_btn";
            this.orderHistory_btn.Size = new System.Drawing.Size(233, 29);
            this.orderHistory_btn.TabIndex = 2;
            this.orderHistory_btn.Text = "历史记录";
            this.orderHistory_btn.UseVisualStyleBackColor = true;
            this.orderHistory_btn.Click += new System.EventHandler(this.orderHistory_btn_Click);
            // 
            // OrderDeliver
            // 
            this.OrderDeliver.Location = new System.Drawing.Point(19, 709);
            this.OrderDeliver.Name = "OrderDeliver";
            this.OrderDeliver.Size = new System.Drawing.Size(233, 28);
            this.OrderDeliver.TabIndex = 3;
            this.OrderDeliver.Text = "新调度令";
            this.OrderDeliver.UseVisualStyleBackColor = true;
            this.OrderDeliver.Click += new System.EventHandler(this.OrderDeliver_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(811, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(333, 386);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(333, 434);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "label3";
            // 
            // OrInfo_btn
            // 
            this.OrInfo_btn.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OrInfo_btn.Location = new System.Drawing.Point(335, 507);
            this.OrInfo_btn.Name = "OrInfo_btn";
            this.OrInfo_btn.Size = new System.Drawing.Size(190, 76);
            this.OrInfo_btn.TabIndex = 26;
            this.OrInfo_btn.Text = "下发";
            this.OrInfo_btn.UseVisualStyleBackColor = true;
            this.OrInfo_btn.Click += new System.EventHandler(this.OrInfo_btn_Click);
            // 
            // orderInfo_dgv
            // 
            this.orderInfo_dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.orderInfo_dgv.Location = new System.Drawing.Point(252, 97);
            this.orderInfo_dgv.Name = "orderInfo_dgv";
            this.orderInfo_dgv.RowTemplate.Height = 23;
            this.orderInfo_dgv.Size = new System.Drawing.Size(1172, 271);
            this.orderInfo_dgv.TabIndex = 27;
            this.orderInfo_dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // netStatus_lb
            // 
            this.netStatus_lb.AutoSize = true;
            this.netStatus_lb.Location = new System.Drawing.Point(17, 805);
            this.netStatus_lb.Name = "netStatus_lb";
            this.netStatus_lb.Size = new System.Drawing.Size(41, 12);
            this.netStatus_lb.TabIndex = 28;
            this.netStatus_lb.Text = "label4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 782);
            this.Controls.Add(this.netStatus_lb);
            this.Controls.Add(this.orderInfo_dgv);
            this.Controls.Add(this.OrInfo_btn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OrderDeliver);
            this.Controls.Add(this.orderHistory_btn);
            this.Controls.Add(this.hadDone_gpb);
            this.Controls.Add(this.toBeDone_gpb);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toBeDone_gpb.ResumeLayout(false);
            this.hadDone_gpb.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.orderInfo_dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox toBeDone_gpb;
        private System.Windows.Forms.GroupBox hadDone_gpb;
        public  System.Windows.Forms.ListBox tbd_lb;
        private System.Windows.Forms.ListBox hd_lb;
        private System.Windows.Forms.Button orderHistory_btn;
        private System.Windows.Forms.Button OrderDeliver;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button OrInfo_btn;
        private System.Windows.Forms.DataGridView orderInfo_dgv;
        private System.Windows.Forms.Label netStatus_lb;
    }
}

