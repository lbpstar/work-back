namespace SMTCost
{
    partial class User
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(User));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barButtonItem新增 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem修改 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem查询 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem授权 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem刷新 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem删除 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem禁用 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem反禁用 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem退出 = new DevExpress.XtraBars.BarButtonItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem重置密码 = new DevExpress.XtraBars.BarButtonItem();
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
            this.barButtonItem新增,
            this.barButtonItem修改,
            this.barButtonItem删除,
            this.barButtonItem查询,
            this.barButtonItem刷新,
            this.barButtonItem退出,
            this.barButtonItem禁用,
            this.barButtonItem反禁用,
            this.barButtonItem授权,
            this.barButtonItem重置密码});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 12;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem新增),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem修改),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem查询),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem授权),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem重置密码),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem刷新),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem删除),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem禁用),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem反禁用),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, false, this.barButtonItem退出, false)});
            this.bar1.Text = "Tools";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "用户管理：";
            this.barStaticItem1.Id = 0;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barButtonItem新增
            // 
            this.barButtonItem新增.Caption = "新增";
            this.barButtonItem新增.Id = 1;
            this.barButtonItem新增.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem新增.ImageOptions.Image")));
            this.barButtonItem新增.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem新增.ImageOptions.LargeImage")));
            this.barButtonItem新增.Name = "barButtonItem新增";
            this.barButtonItem新增.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem新增.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem新增_ItemClick);
            // 
            // barButtonItem修改
            // 
            this.barButtonItem修改.Caption = "修改";
            this.barButtonItem修改.Id = 2;
            this.barButtonItem修改.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem修改.ImageOptions.Image")));
            this.barButtonItem修改.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem修改.ImageOptions.LargeImage")));
            this.barButtonItem修改.Name = "barButtonItem修改";
            this.barButtonItem修改.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem修改.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem修改_ItemClick);
            // 
            // barButtonItem查询
            // 
            this.barButtonItem查询.Caption = "查询";
            this.barButtonItem查询.Id = 4;
            this.barButtonItem查询.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem查询.ImageOptions.Image")));
            this.barButtonItem查询.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem查询.ImageOptions.LargeImage")));
            this.barButtonItem查询.Name = "barButtonItem查询";
            this.barButtonItem查询.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem查询.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem查询_ItemClick);
            // 
            // barButtonItem授权
            // 
            this.barButtonItem授权.Caption = "授权";
            this.barButtonItem授权.Id = 10;
            this.barButtonItem授权.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem授权.ImageOptions.Image")));
            this.barButtonItem授权.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem授权.ImageOptions.LargeImage")));
            this.barButtonItem授权.Name = "barButtonItem授权";
            this.barButtonItem授权.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem授权.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem授权_ItemClick);
            // 
            // barButtonItem刷新
            // 
            this.barButtonItem刷新.Caption = "刷新";
            this.barButtonItem刷新.Id = 5;
            this.barButtonItem刷新.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem刷新.ImageOptions.Image")));
            this.barButtonItem刷新.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem刷新.ImageOptions.LargeImage")));
            this.barButtonItem刷新.Name = "barButtonItem刷新";
            this.barButtonItem刷新.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem刷新.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem刷新_ItemClick);
            // 
            // barButtonItem删除
            // 
            this.barButtonItem删除.Caption = "删除";
            this.barButtonItem删除.Id = 3;
            this.barButtonItem删除.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem删除.ImageOptions.Image")));
            this.barButtonItem删除.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem删除.ImageOptions.LargeImage")));
            this.barButtonItem删除.Name = "barButtonItem删除";
            this.barButtonItem删除.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem删除.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem删除_ItemClick);
            // 
            // barButtonItem禁用
            // 
            this.barButtonItem禁用.Caption = "禁用";
            this.barButtonItem禁用.Id = 7;
            this.barButtonItem禁用.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem禁用.ImageOptions.Image")));
            this.barButtonItem禁用.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem禁用.ImageOptions.LargeImage")));
            this.barButtonItem禁用.Name = "barButtonItem禁用";
            this.barButtonItem禁用.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem禁用.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem禁用_ItemClick);
            // 
            // barButtonItem反禁用
            // 
            this.barButtonItem反禁用.Caption = "反禁用";
            this.barButtonItem反禁用.Id = 9;
            this.barButtonItem反禁用.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem反禁用.ImageOptions.Image")));
            this.barButtonItem反禁用.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem反禁用.ImageOptions.LargeImage")));
            this.barButtonItem反禁用.Name = "barButtonItem反禁用";
            this.barButtonItem反禁用.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem反禁用.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem反禁用_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(851, 52);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 394);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(851, 22);
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
            this.barDockControlRight.Location = new System.Drawing.Point(851, 52);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 342);
            // 
            // barButtonItem重置密码
            // 
            this.barButtonItem重置密码.Caption = "重置密码";
            this.barButtonItem重置密码.Id = 11;
            this.barButtonItem重置密码.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem重置密码.ImageOptions.Image")));
            this.barButtonItem重置密码.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem重置密码.ImageOptions.LargeImage")));
            this.barButtonItem重置密码.Name = "barButtonItem重置密码";
            this.barButtonItem重置密码.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem重置密码.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem重置密码_ItemClick);
            // 
            // User
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 416);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.LookAndFeel.SkinName = "VS2010";
            this.Name = "User";
            this.Text = "用户管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.User_Load);
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
        private DevExpress.XtraBars.BarButtonItem barButtonItem新增;
        private DevExpress.XtraBars.BarButtonItem barButtonItem修改;
        private DevExpress.XtraBars.BarButtonItem barButtonItem删除;
        private DevExpress.XtraBars.BarButtonItem barButtonItem查询;
        private DevExpress.XtraBars.BarButtonItem barButtonItem刷新;
        private DevExpress.XtraBars.BarButtonItem barButtonItem退出;
        private DevExpress.XtraBars.BarButtonItem barButtonItem禁用;
        private DevExpress.XtraBars.BarButtonItem barButtonItem反禁用;
        private DevExpress.XtraBars.BarButtonItem barButtonItem授权;
        private DevExpress.XtraBars.BarButtonItem barButtonItem重置密码;
    }
}