namespace SMTCost
{
    partial class FestivalInsert
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
            this.textEditNote = new DevExpress.XtraEditors.TextEdit();
            this.WorkTypeInsertlayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.simpleButtonExit = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.textEditNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkTypeInsertlayoutControl1ConvertedLayout)).BeginInit();
            this.WorkTypeInsertlayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // textEditNote
            // 
            this.textEditNote.Location = new System.Drawing.Point(51, 61);
            this.textEditNote.Name = "textEditNote";
            this.textEditNote.Size = new System.Drawing.Size(213, 20);
            this.textEditNote.StyleController = this.WorkTypeInsertlayoutControl1ConvertedLayout;
            this.textEditNote.TabIndex = 1;
            // 
            // WorkTypeInsertlayoutControl1ConvertedLayout
            // 
            this.WorkTypeInsertlayoutControl1ConvertedLayout.Controls.Add(this.comboBoxType);
            this.WorkTypeInsertlayoutControl1ConvertedLayout.Controls.Add(this.dateTimePickerDate);
            this.WorkTypeInsertlayoutControl1ConvertedLayout.Controls.Add(this.textEditNote);
            this.WorkTypeInsertlayoutControl1ConvertedLayout.Controls.Add(this.simpleButtonExit);
            this.WorkTypeInsertlayoutControl1ConvertedLayout.Controls.Add(this.simpleButtonOK);
            this.WorkTypeInsertlayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WorkTypeInsertlayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.WorkTypeInsertlayoutControl1ConvertedLayout.Name = "WorkTypeInsertlayoutControl1ConvertedLayout";
            this.WorkTypeInsertlayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.WorkTypeInsertlayoutControl1ConvertedLayout.Size = new System.Drawing.Size(276, 192);
            this.WorkTypeInsertlayoutControl1ConvertedLayout.TabIndex = 4;
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(51, 36);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(213, 22);
            this.comboBoxType.TabIndex = 5;
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.CustomFormat = "yyyy-MM-dd";
            this.dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDate.Location = new System.Drawing.Point(51, 12);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(213, 22);
            this.dateTimePickerDate.TabIndex = 4;
            // 
            // simpleButtonExit
            // 
            this.simpleButtonExit.Location = new System.Drawing.Point(139, 103);
            this.simpleButtonExit.Name = "simpleButtonExit";
            this.simpleButtonExit.Size = new System.Drawing.Size(120, 22);
            this.simpleButtonExit.StyleController = this.WorkTypeInsertlayoutControl1ConvertedLayout;
            this.simpleButtonExit.TabIndex = 3;
            this.simpleButtonExit.Text = "退出";
            this.simpleButtonExit.Click += new System.EventHandler(this.simpleButtonExit_Click);
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(12, 103);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(123, 22);
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
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem4,
            this.layoutControlItem1,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(276, 192);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButtonExit;
            this.layoutControlItem2.Location = new System.Drawing.Point(127, 91);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(124, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(124, 26);
            this.layoutControlItem2.Name = "simpleButtonExititem";
            this.layoutControlItem2.Size = new System.Drawing.Size(129, 81);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButtonOK;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 91);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(127, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(127, 26);
            this.layoutControlItem3.Name = "simpleButtonOKitem";
            this.layoutControlItem3.Size = new System.Drawing.Size(127, 81);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 73);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 18);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 18);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(256, 18);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dateTimePickerDate;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(256, 24);
            this.layoutControlItem4.Text = "日期：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEditNote;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 49);
            this.layoutControlItem1.Name = "textEditNameitem";
            this.layoutControlItem1.Size = new System.Drawing.Size(256, 24);
            this.layoutControlItem1.Text = "备注：";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.comboBoxType;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(256, 25);
            this.layoutControlItem5.Text = "类型：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(36, 14);
            // 
            // FestivalInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 192);
            this.Controls.Add(this.WorkTypeInsertlayoutControl1ConvertedLayout);
            this.Name = "FestivalInsert";
            this.Text = "节假日设置-新增";
            this.Load += new System.EventHandler(this.FestivalInsert_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEditNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkTypeInsertlayoutControl1ConvertedLayout)).EndInit();
            this.WorkTypeInsertlayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit textEditNote;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraEditors.SimpleButton simpleButtonExit;
        private DevExpress.XtraLayout.LayoutControl WorkTypeInsertlayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private System.Windows.Forms.ComboBox comboBoxType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}