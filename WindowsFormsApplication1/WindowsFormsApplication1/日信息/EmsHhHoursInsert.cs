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
    public partial class EmsHhHoursInsert : DevExpress.XtraEditors.XtraForm
    {
        public EmsHhHoursInsert()
        {
            InitializeComponent();
        }
        private static EmsHhHoursInsert ehiform = null;

        public static EmsHhHoursInsert GetInstance()
        {
            if (ehiform == null || ehiform.IsDisposed)
            {
                ehiform = new EmsHhHoursInsert();
            }
            return ehiform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into cost_ems_hh_hours(cdate,hours) values('" + dateEditDate.Text.ToString() + "',ltrim(rtrim(" + textEditHours.Text.ToString() + ")))";
            strsql2 = "select cdate from cost_ems_hh_hours where cdate ='" + dateEditDate.Text.ToString() + "'";
            if (textEditHours.Text.ToString() != "")
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("此日期产出工时已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
                        EmsHhHoursQuery.RefreshEX();
                    }
                }

            }
            else
            {
                MessageBox.Show("产出工时不能为空！");
            }
            conn.Close();
        }

        private void EmsHhHoursInsert_Load(object sender, EventArgs e)
        {
            dateEditDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}