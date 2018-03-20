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
    public partial class FestivalUpdate : DevExpress.XtraEditors.XtraForm
    {
        string cdate = "",cnote = "";
        int ctype = 0;
        public FestivalUpdate()
        {
            InitializeComponent();
        }

        private void SaleTypeUpdate_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            //绑定类型
            Dictionary<int, string> kvDictonary = new Dictionary<int, string>();
            kvDictonary.Add(0, "日常班");
            kvDictonary.Add(1, "周末");
            kvDictonary.Add(2, "节假日");

            BindingSource bs = new BindingSource();
            bs.DataSource = kvDictonary;
            comboBoxType.DataSource = bs;
            comboBoxType.ValueMember = "Key";
            comboBoxType.DisplayMember = "Value";
            FestivalQuery.GetInfo(ref cdate, ref ctype,ref cnote);
            if (cdate != "")
            {
                textEditNote.Text = cnote;
                dateEditDate.Text = cdate;
                comboBoxType.SelectedIndex = -1;
                comboBoxType.SelectedValue = ctype;
            }

            //this.WindowState = FormWindowState.Maximized;

        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql = "update cost_festival set festival_date = '" + dateEditDate.Text + "',festival_type = '" + comboBoxType.SelectedValue.ToString() + "',festival_note = '" + textEditNote.Text.ToString().Trim()  + "' where festival_date ='" + cdate + "'";
            string sql2 = "select * from cost_festival where festival_date = '" + dateEditDate.Text + "'";
            if (textEditNote.Text.ToString().Trim() != "")
            {
                int rows = conn.ReturnRecordCount(sql2);
                if (rows > 0 && dateEditDate.Text != cdate)
                {
                    MessageBox.Show("该日期已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(sql);
                    if (isok)
                    {
                        MessageBox.Show("修改成功！");
                        FestivalQuery.RefreshEX();
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