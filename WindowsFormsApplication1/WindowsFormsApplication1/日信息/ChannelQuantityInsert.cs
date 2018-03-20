﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SMTCost
{
    public partial class ChannelQuantityInsert : DevExpress.XtraEditors.XtraForm
    {
        public ChannelQuantityInsert()
        {
            InitializeComponent();
        }
        private static ChannelQuantityInsert ehiform = null;

        public static ChannelQuantityInsert GetInstance()
        {
            if (ehiform == null || ehiform.IsDisposed)
            {
                ehiform = new ChannelQuantityInsert();
            }
            return ehiform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into cost_product_quantity(cdate,type,product_quantity) values('" + dateEditDate.Text.ToString() + "',2,"  + textEditQuantity.Text.ToString().Trim() + ")";
            strsql2 = "select cdate from cost_product_quantity where cdate ='" + dateEditDate.Text.ToString() + "' and type = 2";
            if (textEditQuantity.Text.ToString() != "")
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("此日期记录已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
                        ChannelQuantityQuery.RefreshEX();
                        this.Close();
                    }
                }

            }
            else
            {
                MessageBox.Show("信道数量不能为空！");
            }
            conn.Close();
        }

        private void ChannelQuantityInsert_Load(object sender, EventArgs e)
        {
            dateEditDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}