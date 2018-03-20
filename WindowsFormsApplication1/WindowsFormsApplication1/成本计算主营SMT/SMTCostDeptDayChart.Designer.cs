namespace SMTCost
{
    partial class SMTCostDeptDayChart
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
            this.SMTCostinglayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.comboBoxDept = new System.Windows.Forms.ComboBox();
            this.chartControlDeptDay = new DevExpress.XtraCharts.ChartControl();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem部门 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SMTCostinglayoutControl1ConvertedLayout)).BeginInit();
            this.SMTCostinglayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlDeptDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem部门)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton查询
            // 
            this.simpleButton查询.Location = new System.Drawing.Point(317, 12);
            this.simpleButton查询.Name = "simpleButton查询";
            this.simpleButton查询.Size = new System.Drawing.Size(130, 22);
            this.simpleButton查询.StyleController = this.SMTCostinglayoutControl1ConvertedLayout;
            this.simpleButton查询.TabIndex = 0;
            this.simpleButton查询.Text = "查询";
            this.simpleButton查询.Click += new System.EventHandler(this.simpleButton查询_Click);
            // 
            // SMTCostinglayoutControl1ConvertedLayout
            // 
            this.SMTCostinglayoutControl1ConvertedLayout.Controls.Add(this.comboBoxDept);
            this.SMTCostinglayoutControl1ConvertedLayout.Controls.Add(this.chartControlDeptDay);
            this.SMTCostinglayoutControl1ConvertedLayout.Controls.Add(this.dateTimePicker1);
            this.SMTCostinglayoutControl1ConvertedLayout.Controls.Add(this.simpleButton查询);
            this.SMTCostinglayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SMTCostinglayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.SMTCostinglayoutControl1ConvertedLayout.Name = "SMTCostinglayoutControl1ConvertedLayout";
            this.SMTCostinglayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.SMTCostinglayoutControl1ConvertedLayout.Size = new System.Drawing.Size(459, 493);
            this.SMTCostinglayoutControl1ConvertedLayout.TabIndex = 3;
            // 
            // comboBoxDept
            // 
            this.comboBoxDept.FormattingEnabled = true;
            this.comboBoxDept.Location = new System.Drawing.Point(185, 12);
            this.comboBoxDept.Name = "comboBoxDept";
            this.comboBoxDept.Size = new System.Drawing.Size(128, 22);
            this.comboBoxDept.TabIndex = 6;
            this.comboBoxDept.SelectedValueChanged += new System.EventHandler(this.comboBoxDept_SelectedValueChanged);
            // 
            // chartControlDeptDay
            // 
            this.chartControlDeptDay.CrosshairOptions.ShowArgumentLabels = true;
            this.chartControlDeptDay.DataBindings = null;
            this.chartControlDeptDay.Legend.Name = "Default Legend";
            this.chartControlDeptDay.Location = new System.Drawing.Point(12, 38);
            this.chartControlDeptDay.Name = "chartControlDeptDay";
            this.chartControlDeptDay.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControlDeptDay.Size = new System.Drawing.Size(435, 443);
            this.chartControlDeptDay.TabIndex = 5;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(51, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(91, 22);
            this.dateTimePicker1.TabIndex = 4;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.layoutControlItem1,
            this.layoutControlItem部门});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(459, 493);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dateTimePicker1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(134, 24);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(134, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(134, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "月份：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButton查询;
            this.layoutControlItem3.Location = new System.Drawing.Point(305, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(134, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(134, 26);
            this.layoutControlItem3.Name = "simpleButton1item";
            this.layoutControlItem3.Size = new System.Drawing.Size(134, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chartControlDeptDay;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(439, 447);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem部门
            // 
            this.layoutControlItem部门.Control = this.comboBoxDept;
            this.layoutControlItem部门.Location = new System.Drawing.Point(134, 0);
            this.layoutControlItem部门.MaxSize = new System.Drawing.Size(171, 25);
            this.layoutControlItem部门.MinSize = new System.Drawing.Size(171, 25);
            this.layoutControlItem部门.Name = "layoutControlItem部门";
            this.layoutControlItem部门.Size = new System.Drawing.Size(171, 26);
            this.layoutControlItem部门.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem部门.Text = "部门：";
            this.layoutControlItem部门.TextSize = new System.Drawing.Size(36, 14);
            // 
            // SMTCostDeptDayChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 493);
            this.Controls.Add(this.SMTCostinglayoutControl1ConvertedLayout);
            this.Name = "SMTCostDeptDayChart";
            this.Text = "主营SMT各部门日成本报表";
            this.Load += new System.EventHandler(this.SMTCosting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SMTCostinglayoutControl1ConvertedLayout)).EndInit();
            this.SMTCostinglayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartControlDeptDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem部门)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton查询;
        private DevExpress.XtraLayout.LayoutControl SMTCostinglayoutControl1ConvertedLayout;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
        private DevExpress.XtraCharts.ChartControl chartControlDeptDay;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.ComboBox comboBoxDept;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem部门;
    }
}