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
    public partial class PointCountSum : DevExpress.XtraEditors.XtraForm
    {
        public PointCountSum()
        {
            InitializeComponent();
        }
        private static PointCountSum ilaform = null;

        public static PointCountSum GetInstance()
        {
            if (ilaform == null || ilaform.IsDisposed)
            {
                ilaform = new PointCountSum();
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
            if (saletypeid > 0)
            {
                strsql = "select CONVERT(varchar(100), cdate, 23) as 日期,s.cname 营业类型,pointcount 点数 from cost_pointcount_sum p left join cost_saletype s on p.saletype_id = s.cid  where cdate like '%" + dateTimePickerMonth.Text + "%' and p.saletype_id = " + saletypeid;

            }
            else
            {
                strsql = "select CONVERT(varchar(100), cdate, 23) as 日期,s.cname 营业类型,pointcount 点数 from cost_pointcount_sum p left join cost_saletype s on p.saletype_id = s.cid where cdate like '%" + dateTimePickerMonth.Text + "%'";

            }

            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            //gridView1.Columns[0].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            conn.Close();
        }
        private void Import()//已经被淘汰
        {
            ConnDB conn = new ConnDB();
            string strsql,strsql2;
            int rows;
            //strsql = "select CONVERT(varchar(100), cdate, 23) as cdate,linetype,pointcount from openquery (LINKERP, 'select 日期 as cdate, 生产线别 as linetype,sum(生产总点数) as pointcount from CUX_SMT_PROD_RP where 工单类型 = ''正常'' and to_char(日期,''yyyy-mm'')=''" + dateTimePickerMonth.Text + "'' group by 日期, 生产线别 order by 日期')";
            strsql = "insert into cost_pointcount_sum(cdate,saletype_id,pointcount) select cdate,saletype_id,sum(pointcount) from cost_pointcount p left join cost_linetype l on p.line_type_name = l.cname where cdate like '%" + dateTimePickerMonth.Text + "%' group by cdate,l.saletype_id";
            strsql2 = "select * from cost_pointcount_sum where cdate like '%" + dateTimePickerMonth.Text + "%'";
            rows = conn.ReturnRecordCount(strsql2);
            if (rows > 0)
            {
                MessageBox.Show("该月点数汇总已经存在，要重新生成，请先清空该月数据！");
            }
            else
            {
                bool isok = conn.EditDatabase(strsql);
                if (isok)
                {
                    MessageBox.Show("生成成功！");
                    showDetailLocal();
                }
                else
                {
                    MessageBox.Show("失败！");
                }
            }
            conn.Close();
        }
        private void Import2()
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            //strsql = "select CONVERT(varchar(100), cdate, 23) as cdate,linetype,pointcount from openquery (LINKERP, 'select 日期 as cdate, 生产线别 as linetype,sum(生产总点数) as pointcount from CUX_SMT_PROD_RP where 工单类型 = ''正常'' and to_char(日期,''yyyy-mm'')=''" + dateTimePickerMonth.Text + "'' group by 日期, 生产线别 order by 日期')";
            //strsql = "insert into cost_pointcount_sum(cdate,saletype_id,pointcount) select CONVERT(varchar(100), cdate, 23) as cdate,saletypeid = case when saletype = 'HYT' then 2 else 10 end,pointcount from openquery (LINKERP, 'select 日期 as cdate, organization_code as saletype,sum(生产总点数) as pointcount from CUX_SMT_PROD_RP where 工单类型 = ''正常'' and (装配件描述  not like ''%F1专用%'' and   装配件描述 not like ''%F1 TB%'') and to_char(日期,''yyyy-mm'')=''" + dateTimePickerMonth.Text + "'' group by 日期, organization_code order by 日期')";
            strsql = "insert into cost_pointcount_sum(cdate, saletype_id, pointcount) ";
            strsql = strsql + "select CONVERT(varchar(100), cdate, 23) as cdate, saletypeid, sum(cast(生产总点数 as decimal(18, 2)))  pointcount from ";
            strsql = strsql + "(select cdate,工单号,saletype, 装配件描述, 生产总点数 =case when saletype = 'shl' and 工单号 like '%-ws' then cast(生产总点数 as decimal(18,2))*3.56 else 生产总点数 end, saletypeid = case when(saletype = 'HCL' AND CODE2 IS null and 装配件描述 not like '【HBTG专用%') OR CODE2 = 'HCL' then 2 else 10 end from ";
            strsql = strsql + "(select cdate,工单号,saletype, 装配件描述, 生产总点数 from openquery (LINKERPNEW, 'select 日期 as cdate,工单号, organization_code as saletype,装配件描述,生产总点数  from CUX_SMT_PROD_RP where (装配件描述  not like ''%F1专用%'' and   装配件描述 not like ''%F1 TB%'') and to_char(日期,''yyyy-mm'')=''" + dateTimePickerMonth.Text + "'' ')) i ";
            strsql = strsql + "left join (select d.*, o.CODE, O2.CODE CODE2 from COST_SPECIAL_DEAL d left join cost_organization o on d.ORGANIZATION_ID = o.CID left join cost_organization o2 on d.TO_ORGANIZATION_ID = o2.CID where d.YYYYMM = '" + dateTimePickerMonth.Text + "') j ";
            strsql = strsql + "on i.装配件描述 like '%' + j.TASK_NAME + '%' and i.saletype = j.CODE) t ";
            strsql = strsql + "group by cdate, saletypeid order by cdate";

            strsql2 = "select * from cost_pointcount_sum where cdate like '%" + dateTimePickerMonth.Text + "%'";
            rows = conn.ReturnRecordCount(strsql2);
            if (rows > 0)
            {
                MessageBox.Show("该月点数汇总已经存在，要重新导入，请先清空该月数据！");
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
            strsql = "delete from cost_pointcount_sum where cdate like '%" + dateTimePickerMonth.Text + "%'";
            
            bool isok = conn.EditDatabase(strsql);
            if (isok)
            {
                MessageBox.Show("点数汇总已成功清空！");
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

        private void simpleButtonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void PointCountSum_Load(object sender, EventArgs e)
        {
            Common.BasicDataBind("cost_saletype", comboBoxSaleType);
            showDetailLocal();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePickerMonth.Value = startMonth;

            dateTimePickerMonth.Focus();
            SendKeys.Send("{RIGHT} ");
        }

        private void simpleButtonSum_Click(object sender, EventArgs e)
        {
            Import();
        }

        private void simpleButtonImport_Click(object sender, EventArgs e)
        {
            Import2();
        }

        private void dateTimePickerMonth_ValueChanged(object sender, EventArgs e)
        {
            showDetailLocal();
        }
    }
}