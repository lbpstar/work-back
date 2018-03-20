namespace SMTCost
{
    partial class InDirectLabourLevelPriceQuery
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.simpleButton查询 = new DevExpress.XtraEditors.SimpleButton();
            this.DirectLabourPriceQuerylayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DirectLabourPriceQuerylayoutControl1ConvertedLayout)).BeginInit();
            this.DirectLabourPriceQuerylayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridControl1.Location = new System.Drawing.Point(12, 38);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(298, 600);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(51, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(103, 22);
            this.dateTimePicker1.TabIndex = 3;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // simpleButton查询
            // 
            this.simpleButton查询.Location = new System.Drawing.Point(158, 12);
            this.simpleButton查询.Name = "simpleButton查询";
            this.simpleButton查询.Size = new System.Drawing.Size(72, 22);
            this.simpleButton查询.StyleController = this.DirectLabourPriceQuerylayoutControl1ConvertedLayout;
            this.simpleButton查询.TabIndex = 5;
            this.simpleButton查询.Text = "查询";
            this.simpleButton查询.Click += new System.EventHandler(this.simpleButton查询_Click);
            // 
            // DirectLabourPriceQuerylayoutControl1ConvertedLayout
            // 
            this.DirectLabourPriceQuerylayoutControl1ConvertedLayout.Controls.Add(this.dateTimePicker1);
            this.DirectLabourPriceQuerylayoutControl1ConvertedLayout.Controls.Add(this.simpleButton查询);
            this.DirectLabourPriceQuerylayoutControl1ConvertedLayout.Controls.Add(this.gridControl1);
            this.DirectLabourPriceQuerylayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DirectLabourPriceQuerylayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.DirectLabourPriceQuerylayoutControl1ConvertedLayout.Name = "DirectLabourPriceQuerylayoutControl1ConvertedLayout";
            this.DirectLabourPriceQuerylayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.DirectLabourPriceQuerylayoutControl1ConvertedLayout.Size = new System.Drawing.Size(322, 650);
            this.DirectLabourPriceQuerylayoutControl1ConvertedLayout.TabIndex = 7;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(322, 650);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateTimePicker1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(146, 24);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(146, 24);
            this.layoutControlItem1.Name = "dateTimePicker1item";
            this.layoutControlItem1.Size = new System.Drawing.Size(146, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "年月：";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButton查询;
            this.layoutControlItem3.Location = new System.Drawing.Point(146, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(76, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(76, 26);
            this.layoutControlItem3.Name = "simpleButton查询item";
            this.layoutControlItem3.Size = new System.Drawing.Size(156, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridControl1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "gridControl1item";
            this.layoutControlItem4.Size = new System.Drawing.Size(302, 604);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // InDirectLabourLevelPriceQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 650);
            this.Controls.Add(this.DirectLabourPriceQuerylayoutControl1ConvertedLayout);
            this.Name = "InDirectLabourLevelPriceQuery";
            this.Text = "间接人工费率-查询";
            this.Load += new System.EventHandler(this.InDirectLabourLevelPriceQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DirectLabourPriceQuerylayoutControl1ConvertedLayout)).EndInit();
            this.DirectLabourPriceQuerylayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private DevExpress.XtraEditors.SimpleButton simpleButton查询;
        private DevExpress.XtraLayout.LayoutControl DirectLabourPriceQuerylayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
    }
}