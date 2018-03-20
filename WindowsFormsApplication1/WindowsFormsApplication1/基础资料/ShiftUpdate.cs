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
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;

namespace SMTCost
{
    public partial class ShiftUpdate : DevExpress.XtraEditors.XtraForm
    {
        string cname = "";
        public ShiftUpdate()
        {
            InitializeComponent();
        }

        private void SaleTypeUpdate_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            string cbegin = "",cend = "",overtime_begin = "";
            decimal rest_hours = 0, overtime_rest_hours = 0;
            ShiftQuery.GetInfo(ref cname, ref cbegin, ref cend,ref rest_hours,ref overtime_rest_hours,ref overtime_begin);
            if (cname != "")
            {
                textEditName.Text = cname;
                dateTimePickerBegin.Text = cbegin;
                dateTimePickerEnd.Text = cend;
                textEditRest.Text = rest_hours.ToString();
                textEditOvertimeRest.Text = overtime_rest_hours.ToString();
                dateTimePickerOvertimeBegin.Text = overtime_begin;
            }

            //this.WindowState = FormWindowState.Maximized;

        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql = "update cost_shift set cname = '" + textEditName.Text.ToString().Trim() + "',cbegin = '" + dateTimePickerBegin.Text + "',cend = '" + dateTimePickerEnd.Text + "',rest_hours = '" + textEditRest.Text.ToString().Trim() + "',overtime_rest_hours = '" + textEditOvertimeRest.Text.ToString().Trim() + "',overtime_begin = '" + dateTimePickerOvertimeBegin.Text.ToString() + "' where cname ='" + cname + "'";
            string sql2 = "select cname from cost_shift where cname = '" + textEditName.Text.ToString().Trim() + "'";
            if (textEditName.Text.ToString().Trim() != "")
            {
                int rows = conn.ReturnRecordCount(sql2);
                if (rows > 0 && textEditName.Text.ToString().Trim() != cname)
                {
                    MessageBox.Show("该考勤班次已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(sql);
                    if (isok)
                    {
                        MessageBox.Show("修改成功！");
                        ShiftQuery.RefreshEX();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("失败！");
                    }
                }
            }
            else
            {
                MessageBox.Show("名称不能为空！");
            }
            conn.Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaleTypeUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

    }
}