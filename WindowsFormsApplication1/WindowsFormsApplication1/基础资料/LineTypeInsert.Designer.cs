namespace SMTCost
{
    partial class LineTypeInsert
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
            this.LineTypeInsertlayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.comboBoxWorkShop = new System.Windows.Forms.ComboBox();
            this.comboBoxSaleType = new System.Windows.Forms.ComboBox();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(this.components);
            this.textEditNameMes = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LineTypeInsertlayoutControl1ConvertedLayout)).BeginInit();
            this.LineTypeInsertlayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNameMes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // textEditName
            // 
            this.textEditName.Location = new System.Drawing.Point(84, 12);
            this.textEditName.Name = "textEditName";
            this.textEditName.Size = new System.Drawing.Size(146, 20);
            this.textEditName.StyleController = this.LineTypeInsertlayoutControl1ConvertedLayout;
            this.textEditName.TabIndex = 1;
            // 
            // LineTypeInsertlayoutControl1ConvertedLayout
            // 
            this.LineTypeInsertlayoutControl1ConvertedLayout.Controls.Add(this.textEditNameMes);
            this.LineTypeInsertlayoutControl1ConvertedLayout.Controls.Add(this.comboBoxWorkShop);
            this.LineTypeInsertlayoutControl1ConvertedLayout.Controls.Add(this.comboBoxSaleType);
            this.LineTypeInsertlayoutControl1ConvertedLayout.Controls.Add(this.textEditName);
            this.LineTypeInsertlayoutControl1ConvertedLayout.Controls.Add(this.simpleButtonOK);
            this.LineTypeInsertlayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LineTypeInsertlayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.LineTypeInsertlayoutControl1ConvertedLayout.Name = "LineTypeInsertlayoutControl1ConvertedLayout";
            this.LineTypeInsertlayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.LineTypeInsertlayoutControl1ConvertedLayout.Size = new System.Drawing.Size(242, 163);
            this.LineTypeInsertlayoutControl1ConvertedLayout.TabIndex = 5;
            // 
            // comboBoxWorkShop
            // 
            this.comboBoxWorkShop.FormattingEnabled = true;
            this.comboBoxWorkShop.Location = new System.Drawing.Point(84, 85);
            this.comboBoxWorkShop.Name = "comboBoxWorkShop";
            this.comboBoxWorkShop.Size = new System.Drawing.Size(146, 22);
            this.comboBoxWorkShop.TabIndex = 5;
            // 
            // comboBoxSaleType
            // 
            this.comboBoxSaleType.FormattingEnabled = true;
            this.comboBoxSaleType.Location = new System.Drawing.Point(84, 60);
            this.comboBoxSaleType.Name = "comboBoxSaleType";
            this.comboBoxSaleType.Size = new System.Drawing.Size(146, 22);
            this.comboBoxSaleType.TabIndex = 3;
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(106, 123);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(124, 22);
            this.simpleButtonOK.StyleController = this.LineTypeInsertlayoutControl1ConvertedLayout;
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
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(242, 163);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.comboBoxSaleType;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem1.Name = "comboBoxSaleTypeitem";
            this.layoutControlItem1.Size = new System.Drawing.Size(222, 25);
            this.layoutControlItem1.Text = "营业类型：";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textEditName;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "textEditNameitem";
            this.layoutControlItem2.Size = new System.Drawing.Size(222, 24);
            this.layoutControlItem2.Text = "ERP中名称：";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButtonOK;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopRight;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 111);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(128, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(128, 26);
            this.layoutControlItem3.Name = "simpleButtonOKitem";
            this.layoutControlItem3.Size = new System.Drawing.Size(222, 32);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 98);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 13);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 13);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(222, 13);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.comboBoxWorkShop;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 73);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(222, 25);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(222, 25);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(222, 25);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = " 所属车间：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(69, 14);
            // 
            // textEditNameMes
            // 
            this.textEditNameMes.Location = new System.Drawing.Point(84, 36);
            this.textEditNameMes.Name = "textEditNameMes";
            this.textEditNameMes.Size = new System.Drawing.Size(146, 20);
            this.textEditNameMes.StyleController = this.LineTypeInsertlayoutControl1ConvertedLayout;
            this.textEditNameMes.TabIndex = 6;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.textEditNameMes;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(222, 24);
            this.layoutControlItem5.Text = "MES中名称";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(69, 14);
            // 
            // LineTypeInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 163);
            this.Controls.Add(this.LineTypeInsertlayoutControl1ConvertedLayout);
            this.Name = "LineTypeInsert";
            this.Text = "线体对照表-新增";
            this.Load += new System.EventHandler(this.LineTypeInsert_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LineTypeInsertlayoutControl1ConvertedLayout)).EndInit();
            this.LineTypeInsertlayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNameMes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit textEditName;
        private System.Windows.Forms.ComboBox comboBoxSaleType;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraLayout.LayoutControl LineTypeInsertlayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
        private System.Windows.Forms.ComboBox comboBoxWorkShop;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.TextEdit textEditNameMes;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}