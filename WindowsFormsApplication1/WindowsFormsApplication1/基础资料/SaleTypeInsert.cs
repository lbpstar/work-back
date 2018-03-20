using System;
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
    public partial class SaleTypeInsert : DevExpress.XtraEditors.XtraForm
    {
        public SaleTypeInsert()
        {
            InitializeComponent();
        }
        private static SaleTypeInsert stiform = null;

        public static SaleTypeInsert GetInstance()
        {
            if (stiform == null || stiform.IsDisposed)
            {
                stiform = new SaleTypeInsert();
            }
            return stiform;
        }

        private void SaleTypeInsert_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into cost_saletype(CNAME) values(LTRIM(rtrim('" + textEditName.Text.ToString() + "')))";
            strsql2 = "select cname from cost_saletype where cname = LTRIM(rtrim('" + textEditName.Text.ToString() + "'))";
            if (textEditName.Text.ToString().Trim() != "")
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("该营业类型已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
                        SaleTypeQuery.RefreshEX();
                        this.Close();
                    }
                }

            }
            else
            {
                MessageBox.Show("名称不能为空！");
            }
            conn.Close();
        }
        //protected override void WndProc(ref System.Windows.Forms.Message m)
        //{
        //    base.WndProc(ref m);//基类执行 
        //    if (m.Msg == 132)//鼠标的移动消息（包括非窗口的移动） 
        //    {
        //        //基类执行后m有了返回值,鼠标在窗口的每个地方的返回值都不同 
        //        if ((IntPtr)2 == m.Result)//如果返回值是2，则说明鼠标是在标题拦 
        //        {
        //            //将返回值改为1(窗口的客户区)，这样系统就以为是 
        //            //在客户区拖动的鼠标，窗体就不会移动 
        //            m.Result = (IntPtr)1;
        //        }
        //    }
        //}

        private void simpleButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textEdit1_MouseDown_1(object sender, MouseEventArgs e)
        {

            //textEdit1.Focus();
            //textEdit1.SelectionStart = 2;
            //int i = textEdit1.SelectionStart;  //设置起始位置 

            //textEdit1.Focus();
            //textEdit1.ScrollToCaret();
            //int j =  textEdit1.SelectionLength;  //设置长度
           // MessageBox.Show(textEdit1.SelectionStart.ToString());

        }
    }
}