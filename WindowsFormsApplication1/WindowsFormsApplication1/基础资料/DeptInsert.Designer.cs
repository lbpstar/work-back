namespace SMTCost
{
    partial class DeptInsert
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
            this.textEditName = new DevExpress.XtraEditors.TextEdit();
            this.DeptInsertlayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.comboBoxSaleType = new System.Windows.Forms.ComboBox();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeptInsertlayoutControl1ConvertedLayout)).BeginInit();
            this.DeptInsertlayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // textEditName
            // 
            this.textEditName.Location = new System.Drawing.Point(75, 12);
            this.textEditName.Name = "textEditName";
            this.textEditName.Size = new System.Drawing.Size(141, 20);
            this.textEditName.StyleController = this.DeptInsertlayoutControl1ConvertedLayout;
            this.textEditName.TabIndex = 1;
            // 
            // DeptInsertlayoutControl1ConvertedLayout
            // 
            this.DeptInsertlayoutControl1ConvertedLayout.Controls.Add(this.comboBoxSaleType);
            this.DeptInsertlayoutControl1ConvertedLayout.Controls.Add(this.textEditName);
            this.DeptInsertlayoutControl1ConvertedLayout.Controls.Add(this.simpleButtonOK);
            this.DeptInsertlayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeptInsertlayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.DeptInsertlayoutControl1ConvertedLayout.Name = "DeptInsertlayoutControl1ConvertedLayout";
            this.DeptInsertlayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.DeptInsertlayoutControl1ConvertedLayout.Size = new System.Drawing.Size(228, 186);
            this.DeptInsertlayoutControl1ConvertedLayout.TabIndex = 5;
            // 
            // comboBoxSaleType
            // 
            this.comboBoxSaleType.FormattingEnabled = true;
            this.comboBoxSaleType.Location = new System.Drawing.Point(75, 36);
            this.comboBoxSaleType.Name = "comboBoxSaleType";
            this.comboBoxSaleType.Size = new System.Drawing.Size(141, 22);
            this.comboBoxSaleType.TabIndex = 3;
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(68, 74);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(148, 22);
            this.simpleButtonOK.StyleController = this.DeptInsertlayoutControl1ConvertedLayout;
            this.simpleButtonOK.TabIndex = 4;
            this.simpleButtonOK.Text = "确定";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.emptySpaceItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(228, 186);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.comboBoxSaleType;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "comboBoxSaleTypeitem";
            this.layoutControlItem1.Size = new System.Drawing.Size(208, 25);
            this.layoutControlItem1.Text = "营业类型：";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textEditName;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "textEditNameitem";
            this.layoutControlItem2.Size = new System.Drawing.Size(208, 24);
            this.layoutControlItem2.Text = "名称：";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButtonOK;
            this.layoutControlItem3.Location = new System.Drawing.Point(56, 62);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(152, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(152, 26);
            this.layoutControlItem3.Name = "simpleButtonOKitem";
            this.layoutControlItem3.Size = new System.Drawing.Size(152, 104);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 62);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(56, 104);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 49);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(0, 13);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(10, 13);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(208, 13);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // DeptInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 186);
            this.Controls.Add(this.DeptInsertlayoutControl1ConvertedLayout);
            this.Name = "DeptInsert";
            this.Text = "部门-新增";
            this.Load += new System.EventHandler(this.DeptInsert_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeptInsertlayoutControl1ConvertedLayout)).EndInit();
            this.DeptInsertlayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit textEditName;
        private System.Windows.Forms.ComboBox comboBoxSaleType;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraLayout.LayoutControl DeptInsertlayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
    }
}