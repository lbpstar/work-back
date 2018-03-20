namespace SMTCost
{
    partial class PointCount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PointCount));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barButtonItemERP数据 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem汇总 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItemCustom = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem退出 = new DevExpress.XtraBars.BarButtonItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStaticItem1,
            this.barButtonItemERP数据,
            this.barButtonItem汇总,
            this.barButtonItem退出,
            this.barSubItemCustom});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 10;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "ToolsSaleType";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemERP数据),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem汇总),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemCustom),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, false, this.barButtonItem退出, false)});
            this.bar1.Text = "Tools";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "生产点数：";
            this.barStaticItem1.Id = 0;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barButtonItemERP数据
            // 
            this.barButtonItemERP数据.Caption = "ERP数据";
            this.barButtonItemERP数据.Id = 1;
            this.barButtonItemERP数据.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemERP数据.ImageOptions.Image")));
            this.barButtonItemERP数据.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemERP数据.ImageOptions.LargeImage")));
            this.barButtonItemERP数据.Name = "barButtonItemERP数据";
            this.barButtonItemERP数据.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemERP数据.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemERP数据_ItemClick);
            // 
            // barButtonItem汇总
            // 
            this.barButtonItem汇总.Caption = "汇总数据";
            this.barButtonItem汇总.Id = 5;
            this.barButtonItem汇总.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem汇总.ImageOptions.Image")));
            this.barButtonItem汇总.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem汇总.ImageOptions.LargeImage")));
            this.barButtonItem汇总.Name = "barButtonItem汇总";
            this.barButtonItem汇总.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem汇总.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem汇总_ItemClick);
            // 
            // barSubItemCustom
            // 
            this.barSubItemCustom.Caption = "选项";
            this.barSubItemCustom.Id = 8;
            this.barSubItemCustom.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barSubItemCustom.ImageOptions.Image")));
            this.barSubItemCustom.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barSubItemCustom.ImageOptions.LargeImage")));
            this.barSubItemCustom.Name = "barSubItemCustom";
            this.barSubItemCustom.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem退出
            // 
            this.barButtonItem退出.Caption = "退出";
            this.barButtonItem退出.Id = 6;
            this.barButtonItem退出.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem退出.ImageOptions.Image")));
            this.barButtonItem退出.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem退出.ImageOptions.LargeImage")));
            this.barButtonItem退出.Name = "barButtonItem退出";
            this.barButtonItem退出.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem退出.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem退出_ItemClick);
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(687, 52);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 394);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(687, 22);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 52);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 342);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(687, 52);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 342);
            // 
            // PointCount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 416);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.LookAndFeel.SkinName = "VS2010";
            this.Name = "PointCount";
            this.Text = "生产点数";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SteelNetRate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemERP数据;
        private DevExpress.XtraBars.BarButtonItem barButtonItem汇总;
        private DevExpress.XtraBars.BarButtonItem barButtonItem退出;
        private DevExpress.XtraBars.BarSubItem barSubItemCustom;
    }
}