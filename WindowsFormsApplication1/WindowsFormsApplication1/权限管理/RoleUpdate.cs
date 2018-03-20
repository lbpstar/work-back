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
    public partial class RoleUpdate : DevExpress.XtraEditors.XtraForm
    {
        public RoleUpdate()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql = "update cost_role set cname = '" + textEditNew.Text.ToString().Trim() + "' where cname = '" + textEditOld.Text.ToString().Trim() + "'";
            string sql2 = "select cname from cost_role where cname = '" + textEditNew.Text.ToString().Trim() + "'";
            if (textEditNew.Text.ToString().Trim() != "")
            {
                int rows = conn.ReturnRecordCount(sql2);
                if (rows > 0)
                {
                    MessageBox.Show("该角色已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(sql);
                    if (isok)
                    {
                        MessageBox.Show("修改成功！");
                        RoleQuery.RefreshEX();
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
                MessageBox.Show("新值不能为空！");
            }
            conn.Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();

            //DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutAction action = new DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutAction() { Caption = "Confirm", Description = "Close the application?" };
            //Predicate<DialogResult> predicate = canCloseFunc;
            //DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutCommand command1 = new DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutCommand() { Text = "Close", Result = System.Windows.Forms.DialogResult.Yes };
            //DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutCommand command2 = new DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutCommand() { Text = "Cancel", Result = System.Windows.Forms.DialogResult.No };
            //action.Commands.Add(command1);
            //action.Commands.Add(command2);
            //FlyoutProperties properties = new FlyoutProperties();
            //properties.ButtonSize = new Size(100, 40);
            //properties.Style = FlyoutStyle.Popup;
            //if (FlyoutDialog.Show(this, action, properties, predicate) == System.Windows.Forms.DialogResult.Yes) { }
            //else { }
        }

        private void SaleTypeUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutAction action = new DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutAction() { Caption = "Confirm", Description = "Close the application?" };
            //Predicate<DialogResult> predicate = canCloseFunc;
            //DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutCommand command1 = new DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutCommand() { Text = "Close", Result = System.Windows.Forms.DialogResult.Yes };
            //DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutCommand command2 = new DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutCommand() { Text = "Cancel", Result = System.Windows.Forms.DialogResult.No };
            //action.Commands.Add(command1);
            //action.Commands.Add(command2);
            //FlyoutProperties properties = new FlyoutProperties();
            //properties.ButtonSize = new Size(100, 40);
            //properties.Style = FlyoutStyle.MessageBox;
            //if (FlyoutDialog.Show(this, action, properties, predicate) == System.Windows.Forms.DialogResult.Yes) e.Cancel = false;
            //else e.Cancel = true;

        }
        private static bool canCloseFunc(DialogResult parameter)
        {
            return parameter != DialogResult.Cancel;
        }

        private void RoleUpdate_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            string cname = RoleQuery.GetCname();
            if (cname != "")
            {
                textEditOld.Text = cname;
            }
        }
    }
}