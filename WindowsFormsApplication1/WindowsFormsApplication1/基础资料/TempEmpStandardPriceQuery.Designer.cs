namespace SMTCost
{
    partial class TempEmpStandardPriceQuery
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
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerBegin = new System.Windows.Forms.DateTimePicker();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.WaterElectricityQuerylayoutControl1ConvertedLayout)).BeginInit();
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton查询
            // 
            this.simpleButton查询.Location = new System.Drawing.Point(12, 60);
            this.simpleButton查询.Name = "simpleButton查询";
            this.simpleButton查询.Size = new System.Drawing.Size(320, 22);
            this.simpleButton查询.StyleController = this.WaterElectricityQuerylayoutControl1ConvertedLayout;
            this.simpleButton查询.TabIndex = 2;
            this.simpleButton查询.Text = "查询";
            this.simpleButton查询.Click += new System.EventHandler(this.simpleButton查询_Click);
            // 
            // WaterElectricityQuerylayoutControl1ConvertedLayout
            // 
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.Controls.Add(this.dateTimePickerEnd);
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.Controls.Add(this.dateTimePickerBegin);
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.Controls.Add(this.gridControl1);
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.Controls.Add(this.simpleButton查询);
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.Name = "WaterElectricityQuerylayoutControl1ConvertedLayout";
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.Size = new System.Drawing.Size(518, 747);
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.TabIndex = 13;
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.CustomFormat = "yyyy-MM-dd";
            this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEnd.Location = new System.Drawing.Point(75, 36);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(255, 22);
            this.dateTimePickerEnd.TabIndex = 20;
            // 
            // dateTimePickerBegin
            // 
            this.dateTimePickerBegin.CustomFormat = "yyyy-MM-dd";
            this.dateTimePickerBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerBegin.Location = new System.Drawing.Point(75, 12);
            this.dateTimePickerBegin.Name = "dateTimePickerBegin";
            this.dateTimePickerBegin.Size = new System.Drawing.Size(255, 22);
            this.dateTimePickerBegin.TabIndex = 11;
            this.dateTimePickerBegin.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 86);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(494, 649);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(518, 747);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateTimePickerBegin;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(322, 24);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(322, 24);
            this.layoutControlItem1.Name = "dateTimePicker1item";
            this.layoutControlItem1.Size = new System.Drawing.Size(498, 24);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "起始日期：";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 74);
            this.layoutControlItem2.Name = "gridControl1item";
            this.layoutControlItem2.Size = new System.Drawing.Size(498, 653);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButton查询;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(324, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(324, 26);
            this.layoutControlItem3.Name = "simpleButton查询item";
            this.layoutControlItem3.Size = new System.Drawing.Size(498, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dateTimePickerEnd;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(322, 24);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(322, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(498, 24);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "截止日期：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 14);
            // 
            // TempEmpStandardPriceQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 747);
            this.Controls.Add(this.WaterElectricityQuerylayoutControl1ConvertedLayout);
            this.Name = "TempEmpStandardPriceQuery";
            this.Text = "最低工资标准-查询";
            this.Load += new System.EventHandler(this.TempEmpQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.WaterElectricityQuerylayoutControl1ConvertedLayout)).EndInit();
            this.WaterElectricityQuerylayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton simpleButton查询;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.DateTimePicker dateTimePickerBegin;
        private DevExpress.XtraLayout.LayoutControl WaterElectricityQuerylayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}