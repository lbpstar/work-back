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
using DevExpress.XtraBars;

namespace SMTCost
{
    public partial class TempEmpAtt : DevExpress.XtraEditors.XtraForm
    {
        public TempEmpAtt()
        {
            InitializeComponent();
        }
        private static TempEmpAtt reform = null;
        public static TempEmpAtt GetInstance()
        {
            if (reform == null || reform.IsDisposed)
            {
                reform = new TempEmpAtt();
            }
            return reform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpAttQuery Frm = TempEmpAttQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem退出_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            //TempEmpQuery.Delete();
            //TempEmpQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpQuery.RefreshEX();
        }

        private void barButtonItem考勤员审核_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpAttQuery.Check(2);
        }

        private void barButtonItem主管审批_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpAttQuery.Check(3);
        }

        private void barButtonItem提报工时_ItemClick(object sender, ItemClickEventArgs e)
        {
            string cno = "", cname = "", cfrom = "", shift = "", begin_date = "", end_date = "", begin_time = "", end_time = "", begin_apply = "", end_apply = "", dept = "", rest_hours = "", hours = "", normal_hours = "", overtime_hours = "", reason = "", status = "",ng_type = "",s_begin="",s_end="", s_rest_hours="",s_overtime_rest_hours="",overtime_begin="";
            TempEmpAttQuery.GetInfo(ref cno, ref cname, ref dept, ref cfrom, ref shift, ref begin_date, ref end_date, ref begin_time, ref end_time, ref begin_apply, ref end_apply, ref rest_hours, ref hours, ref normal_hours, ref overtime_hours, ref reason, ref status,ref ng_type, ref s_begin, ref s_end, ref s_rest_hours, ref s_overtime_rest_hours, ref overtime_begin);
            bool right = TempEmpAttQuery.SubmitRight();
            if(right)
            {
                if (cno != "")
                {
                    if (begin_time != "" && end_time != "")
                    {
                        TempEmpAttUpdate Frm = new TempEmpAttUpdate();
                        Frm.TopLevel = false;
                        Frm.Parent = this;
                        Frm.Show();
                        Frm.BringToFront();
                    }
                    else
                    {
                        MessageBox.Show("打卡记录不完整，请异常提报！");
                    }


                }
                else
                {
                    MessageBox.Show("没有选中要提报的打卡记录！");
                }
            }
            else
            {
                MessageBox.Show("没有权限！");
            }
            
        }

        private void TempEmpAtt_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            TempEmpAttQuery Frm = TempEmpAttQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool right = CheckRight();
            if(right)
            {
                TempEmpAttUpdate Frm = new TempEmpAttUpdate();
                Frm.Text = "异常提报";
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
            else
            {
                MessageBox.Show("没有异常提报的权限！");
            }
            
        }
        /// <summary>
        /// 异常提报权限
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private bool CheckRight()
        {
            ConnDB conn = new ConnDB();
            string sql;
            bool right = false;
            sql = "select m.permission from COST_USER i left join COST_USER_ROLE r on i.CID = r.USER_ID and r.HAVE_RIGHT = 'true' left join COST_ROLE_PERMISSION p on r.ROLE_ID = p.ROLE_ID and p.HAVE_RIGHT = 'true' left join COST_MODULE_PERMISSION m on p.PERMISSION_ID = m.CID where i.CNAME = '" + Logon.GetCname() + "' and m.module_name = '临时工考勤'";
            DataSet ds = conn.ReturnDataSet(sql);
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                if (ds.Tables[0].Rows[j][0].ToString() == "异常提报")
                {
                    right = true;
                    break;
                }
                else
                    right = false;

            }
            conn.Close();
            return right;
        }
        private void barButtonItem考勤员反审_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpAttQuery.Reject(2);
        }

        private void barButtonItem主管反审_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpAttQuery.Reject(3);
        }

        private void barButtonItem驳回提报_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpAttQuery.Reject(1);
        }
    }
}