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
    public partial class PointCountRaw : DevExpress.XtraEditors.XtraForm
    {
        public PointCountRaw()
        {
            InitializeComponent();
        }
        private static PointCountRaw ilaform = null;

        public static PointCountRaw GetInstance()
        {
            if (ilaform == null || ilaform.IsDisposed)
            {
                ilaform = new PointCountRaw();
            }
            return ilaform;
        }
        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
            showDetailLocal();
        }
        private void showDetailLocal()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            int saletypeid;
            saletypeid = (int)comboBoxSaleType.SelectedValue;
            if(saletypeid >0)
            {
                strsql = "select CONVERT(varchar(100), cdate, 23) as 日期,line_type_name ERP拉别,l.cname 成本系统拉别,pointcount 点数 from cost_pointcount p left join cost_linetype l on p.line_type_name = l.cname  where cdate like '%" + dateTimePickerMonth.Text + "%' and l.saletype_id = " + saletypeid;

            }
            else
            {
                strsql = "select CONVERT(varchar(100), cdate, 23) as 日期,line_type_name ERP拉别,l.cname 成本系统拉别,pointcount 点数 from cost_pointcount p left join cost_linetype l on p.line_type_name = l.cname left join cost_saletype s on l.saletype_id = s.cid where cdate like '%" + dateTimePickerMonth.Text + "%'";

            }

            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[2].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;

            conn.Close();
        }
        private void Import()
        {
            ConnDB conn = new ConnDB();
            string strsql,strsql2;
            int rows;
            //strsql = "select CONVERT(varchar(100), cdate, 23) as cdate,linetype,pointcount from openquery (LINKERP, 'select 日期 as cdate, 生产线别 as linetype,sum(生产总点数) as pointcount from CUX_SMT_PROD_RP where 工单类型 = ''正常'' and to_char(日期,''yyyy-mm'')=''" + dateTimePickerMonth.Text + "'' group by 日期, 生产线别 order by 日期')";
            strsql = "insert into cost_pointcount(cdate,line_type_name,pointcount) select CONVERT(varchar(100), cdate, 23) as cdate,linetype,pointcount from openquery (LINKERPNEW, 'select 日期 as cdate, 生产线别 as linetype,sum(生产总点数) as pointcount from CUX_SMT_PROD_RP where  to_char(日期,''yyyy-mm'')=''" + dateTimePickerMonth.Text + "'' group by 日期, 生产线别 order by 日期')";
            strsql2 = "select * from cost_pointcount where cdate like '%" + dateTimePickerMonth.Text + "%'";
            rows = conn.ReturnRecordCount(strsql2);
            if (rows > 0)
            {
                MessageBox.Show("该月点数已经存在，要重新导入，请先清空该月数据！");
            }
            else
            {
                bool isok = conn.EditDatabase(strsql);
                if (isok)
                {
                    MessageBox.Show("导入成功！");
                    showDetailLocal();
                }
                else
                {
                    MessageBox.Show("失败！");
                }
            }
            conn.Close();
        }
        private void Clear()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            strsql = "delete from cost_pointcount where cdate like '%" + dateTimePickerMonth.Text + "%'";
            
            bool isok = conn.EditDatabase(strsql);
            if (isok)
            {
                MessageBox.Show("点数已成功清空！");
                showDetailLocal();
            }
            else
            {
                MessageBox.Show("失败！");
            }
            
            conn.Close();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {    
            this.Close();
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (gridView1.GetDataRow(e.RowHandle) == null) return;
            //if (gridView1.GetDataRow(e.RowHandle)[9] == null || gridView1.GetDataRow(e.RowHandle)[9].ToString() == "") return;
            //if (Convert.ToDecimal(gridView1.GetDataRow(e.RowHandle)[9]) > 8)
            //{
            //    e.Appearance.BackColor = Color.Pink;
            //}
            //else
            //{
            //    e.Appearance.BackColor = Color.White;
            //}
        }



        private void comboBoxSaleType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            showDetailLocal();
        }

        private void simpleButtonImport_Click(object sender, EventArgs e)
        {
            Import();
        }

        private void simpleButtonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void PointCountRaw_Load(object sender, EventArgs e)
        {
            Common.BasicDataBind("cost_saletype", comboBoxSaleType);
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePickerMonth.Value = startMonth;
            showDetailLocal();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;

            dateTimePickerMonth.Focus();
            SendKeys.Send("{RIGHT} ");
        }

        private void dateTimePickerMonth_ValueChanged(object sender, EventArgs e)
        {
            showDetailLocal();
        }
    }
}