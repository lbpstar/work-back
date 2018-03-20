namespace SMTCost
{
    partial class ChangePass
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textEditNew = new DevExpress.XtraEditors.TextEdit();
            this.DeptUpdatelayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.textEditAgain = new DevExpress.XtraEditors.TextEdit();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.textEditNew.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeptUpdatelayoutControl1ConvertedLayout)).BeginInit();
            this.DeptUpdatelayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAgain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // textEditNew
            // 
            this.textEditNew.Location = new System.Drawing.Point(87, 12);
            this.textEditNew.Name = "textEditNew";
            this.textEditNew.Properties.PasswordChar = '*';
            this.textEditNew.Size = new System.Drawing.Size(179, 20);
            this.textEditNew.StyleController = this.DeptUpdatelayoutControl1ConvertedLayout;
            this.textEditNew.TabIndex = 1;
            // 
            // DeptUpdatelayoutControl1ConvertedLayout
            // 
            this.DeptUpdatelayoutControl1ConvertedLayout.Controls.Add(this.textEditAgain);
            this.DeptUpdatelayoutControl1ConvertedLayout.Controls.Add(this.textEditNew);
            this.DeptUpdatelayoutControl1ConvertedLayout.Controls.Add(this.simpleButtonCancel);
            this.DeptUpdatelayoutControl1ConvertedLayout.Controls.Add(this.simpleButtonOK);
            this.DeptUpdatelayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeptUpdatelayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.DeptUpdatelayoutControl1ConvertedLayout.Name = "DeptUpdatelayoutControl1ConvertedLayout";
            this.DeptUpdatelayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.DeptUpdatelayoutControl1ConvertedLayout.Size = new System.Drawing.Size(278, 232);
            this.DeptUpdatelayoutControl1ConvertedLayout.TabIndex = 7;
            // 
            // textEditAgain
            // 
            this.textEditAgain.Location = new System.Drawing.Point(87, 36);
            this.textEditAgain.Name = "textEditAgain";
            this.textEditAgain.Properties.PasswordChar = '*';
            this.textEditAgain.Size = new System.Drawing.Size(179, 20);
            this.textEditAgain.StyleController = this.DeptUpdatelayoutControl1ConvertedLayout;
            this.textEditAgain.TabIndex = 6;
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.Location = new System.Drawing.Point(145, 60);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(121, 22);
            this.simpleButtonCancel.StyleController = this.DeptUpdatelayoutControl1ConvertedLayout;
            this.simpleButtonCancel.TabIndex = 5;
            this.simpleButtonCancel.Text = "取消";
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButtonCancel_Click);
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(12, 60);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(129, 22);
            this.simpleButtonOK.StyleController = this.DeptUpdatelayoutControl1ConvertedLayout;
            this.simpleButtonOK.TabIndex = 4;
            this.simpleButtonOK.Text = "确认修改";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(278, 232);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textEditNew;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "textEditNameitem";
            this.layoutControlItem2.Size = new System.Drawing.Size(258, 24);
            this.layoutControlItem2.Text = "新密码：";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButtonCancel;
            this.layoutControlItem3.Location = new System.Drawing.Point(133, 48);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(0, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(37, 26);
            this.layoutControlItem3.Name = "simpleButtonCancelitem";
            this.layoutControlItem3.Size = new System.Drawing.Size(125, 164);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButtonOK;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(133, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(133, 26);
            this.layoutControlItem4.Name = "simpleButtonOKitem";
            this.layoutControlItem4.Size = new System.Drawing.Size(133, 164);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.textEditAgain;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(258, 24);
            this.layoutControlItem5.Text = "再输入一次：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(72, 14);
            // 
            // ChangePass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 232);
            this.Controls.Add(this.DeptUpdatelayoutControl1ConvertedLayout);
            this.Name = "ChangePass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改密码";
            ((System.ComponentModel.ISupportInitialize)(this.textEditNew.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeptUpdatelayoutControl1ConvertedLayout)).EndInit();
            this.DeptUpdatelayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditAgain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit textEditNew;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
        private DevExpress.XtraLayout.LayoutControl DeptUpdatelayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.TextEdit textEditAgain;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}