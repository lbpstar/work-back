namespace SMTCost
{
    partial class LineTypeQuery
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
            this.LineTypeQuerylayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.comboBoxSaleType = new System.Windows.Forms.ComboBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.LineTypeQuerylayoutControl1ConvertedLayout)).BeginInit();
            this.LineTypeQuerylayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton查询
            // 
            this.simpleButton查询.Location = new System.Drawing.Point(265, 12);
            this.simpleButton查询.Name = "simpleButton查询";
            this.simpleButton查询.Size = new System.Drawing.Size(125, 22);
            this.simpleButton查询.StyleController = this.LineTypeQuerylayoutControl1ConvertedLayout;
            this.simpleButton查询.TabIndex = 2;
            this.simpleButton查询.Text = "查询";
            this.simpleButton查询.Click += new System.EventHandler(this.simpleButton查询_Click);
            // 
            // LineTypeQuerylayoutControl1ConvertedLayout
            // 
            this.LineTypeQuerylayoutControl1ConvertedLayout.Controls.Add(this.comboBoxSaleType);
            this.LineTypeQuerylayoutControl1ConvertedLayout.Controls.Add(this.gridControl1);
            this.LineTypeQuerylayoutControl1ConvertedLayout.Controls.Add(this.simpleButton查询);
            this.LineTypeQuerylayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LineTypeQuerylayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.LineTypeQuerylayoutControl1ConvertedLayout.Name = "LineTypeQuerylayoutControl1ConvertedLayout";
            this.LineTypeQuerylayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.LineTypeQuerylayoutControl1ConvertedLayout.Size = new System.Drawing.Size(402, 554);
            this.LineTypeQuerylayoutControl1ConvertedLayout.TabIndex = 5;
            // 
            // comboBoxSaleType
            // 
            this.comboBoxSaleType.FormattingEnabled = true;
            this.comboBoxSaleType.Location = new System.Drawing.Point(63, 12);
            this.comboBoxSaleType.Name = "comboBoxSaleType";
            this.comboBoxSaleType.Size = new System.Drawing.Size(198, 22);
            this.comboBoxSaleType.TabIndex = 4;
            this.comboBoxSaleType.SelectionChangeCommitted += new System.EventHandler(this.comboBoxSaleType_SelectionChangeCommitted);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 38);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(378, 504);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridView1_SelectionChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(402, 554);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.comboBoxSaleType;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(253, 25);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(253, 25);
            this.layoutControlItem1.Name = "comboBoxSaleTypeitem";
            this.layoutControlItem1.Size = new System.Drawing.Size(253, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "营业类型";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "gridControl1item";
            this.layoutControlItem2.Size = new System.Drawing.Size(382, 508);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButton查询;
            this.layoutControlItem3.Location = new System.Drawing.Point(253, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(129, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(129, 26);
            this.layoutControlItem3.Name = "simpleButton查询item";
            this.layoutControlItem3.Size = new System.Drawing.Size(129, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // LineTypeQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 554);
            this.Controls.Add(this.LineTypeQuerylayoutControl1ConvertedLayout);
            this.Name = "LineTypeQuery";
            this.Text = "线体对照表-查询";
            this.Load += new System.EventHandler(this.LineTypeQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LineTypeQuerylayoutControl1ConvertedLayout)).EndInit();
            this.LineTypeQuerylayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton simpleButton查询;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ComboBox comboBoxSaleType;
        private DevExpress.XtraLayout.LayoutControl LineTypeQuerylayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
    }
}