namespace SMTCost
{
    partial class PositionInsert
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
            this.PositionInsertlayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.comboBoxPersonType = new System.Windows.Forms.ComboBox();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PositionInsertlayoutControl1ConvertedLayout)).BeginInit();
            this.PositionInsertlayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // textEditName
            // 
            this.textEditName.Location = new System.Drawing.Point(75, 12);
            this.textEditName.Name = "textEditName";
            this.textEditName.Size = new System.Drawing.Size(152, 20);
            this.textEditName.StyleController = this.PositionInsertlayoutControl1ConvertedLayout;
            this.textEditName.TabIndex = 1;
            // 
            // PositionInsertlayoutControl1ConvertedLayout
            // 
            this.PositionInsertlayoutControl1ConvertedLayout.Controls.Add(this.comboBoxPersonType);
            this.PositionInsertlayoutControl1ConvertedLayout.Controls.Add(this.textEditName);
            this.PositionInsertlayoutControl1ConvertedLayout.Controls.Add(this.simpleButtonOK);
            this.PositionInsertlayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PositionInsertlayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.PositionInsertlayoutControl1ConvertedLayout.Name = "PositionInsertlayoutControl1ConvertedLayout";
            this.PositionInsertlayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.PositionInsertlayoutControl1ConvertedLayout.Size = new System.Drawing.Size(239, 175);
            this.PositionInsertlayoutControl1ConvertedLayout.TabIndex = 5;
            // 
            // comboBoxPersonType
            // 
            this.comboBoxPersonType.FormattingEnabled = true;
            this.comboBoxPersonType.Location = new System.Drawing.Point(75, 36);
            this.comboBoxPersonType.Name = "comboBoxPersonType";
            this.comboBoxPersonType.Size = new System.Drawing.Size(152, 22);
            this.comboBoxPersonType.TabIndex = 3;
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(113, 71);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(114, 22);
            this.simpleButtonOK.StyleController = this.PositionInsertlayoutControl1ConvertedLayout;
            this.simpleButtonOK.TabIndex = 4;
            this.simpleButtonOK.Text = "确定";
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(239, 175);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.comboBoxPersonType;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "comboBoxPersonTypeitem";
            this.layoutControlItem1.Size = new System.Drawing.Size(219, 25);
            this.layoutControlItem1.Text = "人力类型：";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textEditName;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "textEditNameitem";
            this.layoutControlItem2.Size = new System.Drawing.Size(219, 24);
            this.layoutControlItem2.Text = "名称：";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButtonOK;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopRight;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 59);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(118, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(118, 26);
            this.layoutControlItem3.Name = "simpleButtonOKitem";
            this.layoutControlItem3.Size = new System.Drawing.Size(219, 96);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 49);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 10);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 10);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(219, 10);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // PositionInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 175);
            this.Controls.Add(this.PositionInsertlayoutControl1ConvertedLayout);
            this.Name = "PositionInsert";
            this.Text = "职位-新增";
            this.Load += new System.EventHandler(this.PositionInsert_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PositionInsertlayoutControl1ConvertedLayout)).EndInit();
            this.PositionInsertlayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit textEditName;
        private System.Windows.Forms.ComboBox comboBoxPersonType;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraLayout.LayoutControl PositionInsertlayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
    }
}