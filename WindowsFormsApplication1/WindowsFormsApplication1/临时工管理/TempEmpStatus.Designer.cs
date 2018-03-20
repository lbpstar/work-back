namespace SMTCost
{
    partial class TempEmpStatus
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
            this.simpleButton查询 = new DevExpress.XtraEditors.SimpleButton();
            this.WaterElectricityQuerylayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.comboBoxGroup = new System.Windows.Forms.ComboBox();
            this.dateTimePickerMonth = new System.Windows.Forms.DateTimePicker();
            this.simpleButtonExport = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.WaterElectricityQuerylayoutControl1ConvertedLayout)).BeginInit();
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton查询
            // 
            this.simpleButton查询.Location = new System.Drawing.Point(12, 37);
            this.simpleButton查询.Name = "simpleButton查询";
            this.simpleButton查询.Size = new System.Drawing.Size(165, 22);
            this.simpleButton查询.StyleController = this.WaterElectricityQuerylayoutControl1ConvertedLayout;
            this.simpleButton查询.TabIndex = 2;
            this.simpleButton查询.Text = "查询";
            this.simpleButton查询.Click += new System.EventHandler(this.simpleButton查询_Click);
            // 
            // WaterElectricityQuerylayoutControl1ConvertedLayout
            // 
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.Controls.Add(this.comboBoxGroup);
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.Controls.Add(this.dateTimePickerMonth);
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.Controls.Add(this.simpleButtonExport);
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.Controls.Add(this.gridControl1);
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.Controls.Add(this.simpleButton查询);
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.Name = "WaterElectricityQuerylayoutControl1ConvertedLayout";
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(899, 504, 450, 400);
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.Size = new System.Drawing.Size(535, 748);
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.TabIndex = 13;
            // 
            // comboBoxGroup
            // 
            this.comboBoxGroup.FormattingEnabled = true;
            this.comboBoxGroup.Items.AddRange(new object[] {
            "部门",
            "供应商"});
            this.comboBoxGroup.Location = new System.Drawing.Point(225, 12);
            this.comboBoxGroup.Name = "comboBoxGroup";
            this.comboBoxGroup.Size = new System.Drawing.Size(106, 22);
            this.comboBoxGroup.TabIndex = 23;
            // 
            // dateTimePickerMonth
            // 
            this.dateTimePickerMonth.CustomFormat = "yyyy-MM";
            this.dateTimePickerMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerMonth.Location = new System.Drawing.Point(55, 12);
            this.dateTimePickerMonth.Name = "dateTimePickerMonth";
            this.dateTimePickerMonth.ShowUpDown = true;
            this.dateTimePickerMonth.Size = new System.Drawing.Size(123, 22);
            this.dateTimePickerMonth.TabIndex = 22;
            // 
            // simpleButtonExport
            // 
            this.simpleButtonExport.Location = new System.Drawing.Point(181, 37);
            this.simpleButtonExport.MaximumSize = new System.Drawing.Size(150, 0);
            this.simpleButtonExport.Name = "simpleButtonExport";
            this.simpleButtonExport.Size = new System.Drawing.Size(150, 22);
            this.simpleButtonExport.StyleController = this.WaterElectricityQuerylayoutControl1ConvertedLayout;
            this.simpleButtonExport.TabIndex = 21;
            this.simpleButtonExport.Text = "导出到Excel";
            this.simpleButtonExport.Visible = false;
            this.simpleButtonExport.Click += new System.EventHandler(this.simpleButtonExport_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 63);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(511, 673);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem9,
            this.layoutControlItem7,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(535, 748);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 51);
            this.layoutControlItem2.Name = "gridControl1item";
            this.layoutControlItem2.Size = new System.Drawing.Size(515, 677);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButton查询;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(169, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(169, 26);
            this.layoutControlItem3.Name = "simpleButton查询item";
            this.layoutControlItem3.Size = new System.Drawing.Size(169, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.simpleButtonExport;
            this.layoutControlItem9.Location = new System.Drawing.Point(169, 25);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(159, 26);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(159, 26);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(346, 26);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            this.layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.dateTimePickerMonth;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(170, 24);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(87, 24);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(170, 25);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "月份：";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(40, 14);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.comboBoxGroup;
            this.layoutControlItem1.Location = new System.Drawing.Point(170, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(153, 25);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(67, 25);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(345, 25);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = " 分组：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(40, 14);
            // 
            // TempEmpStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 748);
            this.Controls.Add(this.WaterElectricityQuerylayoutControl1ConvertedLayout);
            this.Name = "TempEmpStatus";
            this.Text = "人员状态分布";
            this.Load += new System.EventHandler(this.TempEmpQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.WaterElectricityQuerylayoutControl1ConvertedLayout)).EndInit();
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton simpleButton查询;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControl WaterElectricityQuerylayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonExport;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private System.Windows.Forms.DateTimePicker dateTimePickerMonth;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private System.Windows.Forms.ComboBox comboBoxGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}