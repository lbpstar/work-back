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
    public partial class WorkTypeInsert : DevExpress.XtraEditors.XtraForm
    {
        public WorkTypeInsert()
        {
            InitializeComponent();
        }
        private static WorkTypeInsert wtiform = null;

        public static WorkTypeInsert GetInstance()
        {
            if (wtiform == null || wtiform.IsDisposed)
            {
                wtiform = new WorkTypeInsert();
            }
            return wtiform;
        }

        private void SaleTypeInsert_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into cost_work_type(CNAME,forbidden) values(LTRIM(rtrim('" + textEditName.Text.ToString() + "')),0)";
            strsql2 = "select cname from cost_work_type where cname = LTRIM(rtrim('" + textEditName.Text.ToString() + "'))";
            if (textEditName.Text.ToString().Trim() != "")
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("该上班类型已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
                        WorkTypeQuery.RefreshEX();
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
        
                SaleType Frm = SaleType.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();

        }
    }
}