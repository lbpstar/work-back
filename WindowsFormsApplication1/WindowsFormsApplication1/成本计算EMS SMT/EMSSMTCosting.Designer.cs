namespace SMTCost
{
    partial class EMSSMTCosting
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
            this.simpleButton数据检查 = new DevExpress.XtraEditors.SimpleButton();
            this.SMTCostinglayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButton清空 = new DevExpress.XtraEditors.SimpleButton();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.simpleButton成本计算 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SMTCostinglayoutControl1ConvertedLayout)).BeginInit();
            this.SMTCostinglayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton数据检查
            // 
            this.simpleButton数据检查.Location = new System.Drawing.Point(12, 36);
            this.simpleButton数据检查.Name = "simpleButton数据检查";
            this.simpleButton数据检查.Size = new System.Drawing.Size(130, 22);
            this.simpleButton数据检查.StyleController = this.SMTCostinglayoutControl1ConvertedLayout;
            this.simpleButton数据检查.TabIndex = 0;
            this.simpleButton数据检查.Text = "数据完整性检查";
            this.simpleButton数据检查.Click += new System.EventHandler(this.simpleButton数据检查_Click);
            // 
            // SMTCostinglayoutControl1ConvertedLayout
            // 
            this.SMTCostinglayoutControl1ConvertedLayout.Controls.Add(this.simpleButton清空);
            this.SMTCostinglayoutControl1ConvertedLayout.Controls.Add(this.dateTimePicker1);
            this.SMTCostinglayoutControl1ConvertedLayout.Controls.Add(this.gridControl1);
            this.SMTCostinglayoutControl1ConvertedLayout.Controls.Add(this.simpleButton成本计算);
            this.SMTCostinglayoutControl1ConvertedLayout.Controls.Add(this.simpleButton数据检查);
            this.SMTCostinglayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SMTCostinglayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.SMTCostinglayoutControl1ConvertedLayout.Name = "SMTCostinglayoutControl1ConvertedLayout";
            this.SMTCostinglayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.SMTCostinglayoutControl1ConvertedLayout.Size = new System.Drawing.Size(459, 493);
            this.SMTCostinglayoutControl1ConvertedLayout.TabIndex = 3;
            // 
            // simpleButton清空
            // 
            this.simpleButton清空.Location = new System.Drawing.Point(146, 36);
            this.simpleButton清空.Name = "simpleButton清空";
            this.simpleButton清空.Size = new System.Drawing.Size(135, 22);
            this.simpleButton清空.StyleController = this.SMTCostinglayoutControl1ConvertedLayout;
            this.simpleButton清空.TabIndex = 5;
            this.simpleButton清空.Text = "清空本月计算结果";
            this.simpleButton清空.Click += new System.EventHandler(this.simpleButton清空_Click);
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
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 62);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(435, 419);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // simpleButton成本计算
            // 
            this.simpleButton成本计算.Location = new System.Drawing.Point(285, 36);
            this.simpleButton成本计算.Name = "simpleButton成本计算";
            this.simpleButton成本计算.Size = new System.Drawing.Size(162, 22);
            this.simpleButton成本计算.StyleController = this.SMTCostinglayoutControl1ConvertedLayout;
            this.simpleButton成本计算.TabIndex = 1;
            this.simpleButton成本计算.Text = "成本计算";
            this.simpleButton成本计算.Click += new System.EventHandler(this.simpleButton成本计算_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(459, 493);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem1.Name = "gridControl1item";
            this.layoutControlItem1.Size = new System.Drawing.Size(439, 423);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButton成本计算;
            this.layoutControlItem2.Location = new System.Drawing.Point(273, 24);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(166, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(166, 26);
            this.layoutControlItem2.Name = "simpleButton2item";
            this.layoutControlItem2.Size = new System.Drawing.Size(166, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButton数据检查;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(134, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(134, 26);
            this.layoutControlItem3.Name = "simpleButton1item";
            this.layoutControlItem3.Size = new System.Drawing.Size(134, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dateTimePicker1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(134, 24);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(134, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(134, 24);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "月份：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(36, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(134, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(305, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.simpleButton清空;
            this.layoutControlItem5.Location = new System.Drawing.Point(134, 24);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(139, 26);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(139, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(139, 26);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // EMSSMTCosting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 493);
            this.Controls.Add(this.SMTCostinglayoutControl1ConvertedLayout);
            this.Name = "EMSSMTCosting";
            this.Text = "EMS SMT成本计算";
            this.Load += new System.EventHandler(this.SMTCosting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SMTCostinglayoutControl1ConvertedLayout)).EndInit();
            this.SMTCostinglayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton数据检查;
        private DevExpress.XtraEditors.SimpleButton simpleButton成本计算;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControl SMTCostinglayoutControl1ConvertedLayout;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
        private DevExpress.XtraEditors.SimpleButton simpleButton清空;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}