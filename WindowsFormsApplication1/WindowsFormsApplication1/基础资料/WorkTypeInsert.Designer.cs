namespace SMTCost
{
    partial class WorkTypeInsert
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
            this.WorkTypeInsertlayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButtonExit = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkTypeInsertlayoutControl1ConvertedLayout)).BeginInit();
            this.WorkTypeInsertlayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // textEditName
            // 
            this.textEditName.Location = new System.Drawing.Point(51, 12);
            this.textEditName.Name = "textEditName";
            this.textEditName.Size = new System.Drawing.Size(156, 20);
            this.textEditName.StyleController = this.WorkTypeInsertlayoutControl1ConvertedLayout;
            this.textEditName.TabIndex = 1;
            // 
            // WorkTypeInsertlayoutControl1ConvertedLayout
            // 
            this.WorkTypeInsertlayoutControl1ConvertedLayout.Controls.Add(this.textEditName);
            this.WorkTypeInsertlayoutControl1ConvertedLayout.Controls.Add(this.simpleButtonExit);
            this.WorkTypeInsertlayoutControl1ConvertedLayout.Controls.Add(this.simpleButtonOK);
            this.WorkTypeInsertlayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WorkTypeInsertlayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.WorkTypeInsertlayoutControl1ConvertedLayout.Name = "WorkTypeInsertlayoutControl1ConvertedLayout";
            this.WorkTypeInsertlayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.WorkTypeInsertlayoutControl1ConvertedLayout.Size = new System.Drawing.Size(219, 174);
            this.WorkTypeInsertlayoutControl1ConvertedLayout.TabIndex = 4;
            // 
            // simpleButtonExit
            // 
            this.simpleButtonExit.Location = new System.Drawing.Point(113, 54);
            this.simpleButtonExit.Name = "simpleButtonExit";
            this.simpleButtonExit.Size = new System.Drawing.Size(94, 22);
            this.simpleButtonExit.StyleController = this.WorkTypeInsertlayoutControl1ConvertedLayout;
            this.simpleButtonExit.TabIndex = 3;
            this.simpleButtonExit.Text = "退出";
            this.simpleButtonExit.Click += new System.EventHandler(this.simpleButtonExit_Click);
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(12, 54);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(97, 22);
            this.simpleButtonOK.StyleController = this.WorkTypeInsertlayoutControl1ConvertedLayout;
            this.simpleButtonOK.TabIndex = 2;
            this.simpleButtonOK.Text = "确认添加";
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
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(219, 174);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEditName;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "textEditNameitem";
            this.layoutControlItem1.Size = new System.Drawing.Size(199, 24);
            this.layoutControlItem1.Text = "名称：";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButtonExit;
            this.layoutControlItem2.Location = new System.Drawing.Point(101, 42);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(98, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(98, 26);
            this.layoutControlItem2.Name = "simpleButtonExititem";
            this.layoutControlItem2.Size = new System.Drawing.Size(98, 112);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButtonOK;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 42);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(101, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(101, 26);
            this.layoutControlItem3.Name = "simpleButtonOKitem";
            this.layoutControlItem3.Size = new System.Drawing.Size(101, 112);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 24);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 18);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 18);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(199, 18);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // WorkTypeInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 174);
            this.Controls.Add(this.WorkTypeInsertlayoutControl1ConvertedLayout);
            this.Name = "WorkTypeInsert";
            this.Text = "上班类型-新增";
            this.Load += new System.EventHandler(this.SaleTypeInsert_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkTypeInsertlayoutControl1ConvertedLayout)).EndInit();
            this.WorkTypeInsertlayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit textEditName;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraEditors.SimpleButton simpleButtonExit;
        private DevExpress.XtraLayout.LayoutControl WorkTypeInsertlayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
    }
}