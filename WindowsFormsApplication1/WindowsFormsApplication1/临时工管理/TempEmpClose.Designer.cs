namespace SMTCost
{
    partial class TempEmpClose
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
            this.WaterElectricityInsertlayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButtonOpen = new DevExpress.XtraEditors.SimpleButton();
            this.dateTimePickerMonth = new System.Windows.Forms.DateTimePicker();
            this.simpleButtonClose = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.WaterElectricityInsertlayoutControl1ConvertedLayout)).BeginInit();
            this.WaterElectricityInsertlayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // WaterElectricityInsertlayoutControl1ConvertedLayout
            // 
            this.WaterElectricityInsertlayoutControl1ConvertedLayout.Controls.Add(this.simpleButtonOpen);
            this.WaterElectricityInsertlayoutControl1ConvertedLayout.Controls.Add(this.dateTimePickerMonth);
            this.WaterElectricityInsertlayoutControl1ConvertedLayout.Controls.Add(this.simpleButtonClose);
            this.WaterElectricityInsertlayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WaterElectricityInsertlayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.WaterElectricityInsertlayoutControl1ConvertedLayout.Name = "WaterElectricityInsertlayoutControl1ConvertedLayout";
            this.WaterElectricityInsertlayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.WaterElectricityInsertlayoutControl1ConvertedLayout.Size = new System.Drawing.Size(215, 162);
            this.WaterElectricityInsertlayoutControl1ConvertedLayout.TabIndex = 12;
            // 
            // simpleButtonOpen
            // 
            this.simpleButtonOpen.Location = new System.Drawing.Point(12, 75);
            this.simpleButtonOpen.Name = "simpleButtonOpen";
            this.simpleButtonOpen.Size = new System.Drawing.Size(191, 22);
            this.simpleButtonOpen.StyleController = this.WaterElectricityInsertlayoutControl1ConvertedLayout;
            this.simpleButtonOpen.TabIndex = 10;
            this.simpleButtonOpen.Text = "反关帐";
            this.simpleButtonOpen.Click += new System.EventHandler(this.simpleButtonOpen_Click);
            // 
            // dateTimePickerMonth
            // 
            this.dateTimePickerMonth.CustomFormat = "yyyy-MM";
            this.dateTimePickerMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerMonth.Location = new System.Drawing.Point(51, 12);
            this.dateTimePickerMonth.Name = "dateTimePickerMonth";
            this.dateTimePickerMonth.ShowUpDown = true;
            this.dateTimePickerMonth.Size = new System.Drawing.Size(152, 22);
            this.dateTimePickerMonth.TabIndex = 9;
            // 
            // simpleButtonClose
            // 
            this.simpleButtonClose.Location = new System.Drawing.Point(12, 49);
            this.simpleButtonClose.Name = "simpleButtonClose";
            this.simpleButtonClose.Size = new System.Drawing.Size(191, 22);
            this.simpleButtonClose.StyleController = this.WaterElectricityInsertlayoutControl1ConvertedLayout;
            this.simpleButtonClose.TabIndex = 4;
            this.simpleButtonClose.Text = "关帐";
            this.simpleButtonClose.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(215, 162);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 24);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 13);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 13);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(195, 13);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateTimePickerMonth;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(195, 24);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(195, 24);
            this.layoutControlItem1.Name = "dateTimePicker1item";
            this.layoutControlItem1.Size = new System.Drawing.Size(195, 24);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "月份：";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButtonOpen;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 63);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(195, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(195, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(195, 79);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.simpleButtonClose;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 37);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(195, 26);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(195, 26);
            this.layoutControlItem5.Name = "simpleButtonOKitem";
            this.layoutControlItem5.Size = new System.Drawing.Size(195, 26);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // TempEmpClose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 162);
            this.Controls.Add(this.WaterElectricityInsertlayoutControl1ConvertedLayout);
            this.Name = "TempEmpClose";
            this.Text = "月末关帐";
            this.Load += new System.EventHandler(this.TempEmpInsert_Load);
            ((System.ComponentModel.ISupportInitialize)(this.WaterElectricityInsertlayoutControl1ConvertedLayout)).EndInit();
            this.WaterElectricityInsertlayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton simpleButtonClose;
        private System.Windows.Forms.DateTimePicker dateTimePickerMonth;
        private DevExpress.XtraLayout.LayoutControl WaterElectricityInsertlayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOpen;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}