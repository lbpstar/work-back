﻿namespace SMTCost
{
    partial class WorkTypeUpdate
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
            this.textEditOld = new DevExpress.XtraEditors.TextEdit();
            this.WorkTypeUpdatelayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.textEditNew = new DevExpress.XtraEditors.TextEdit();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.textEditOld.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkTypeUpdatelayoutControl1ConvertedLayout)).BeginInit();
            this.WorkTypeUpdatelayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNew.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // textEditOld
            // 
            this.textEditOld.Location = new System.Drawing.Point(51, 12);
            this.textEditOld.Name = "textEditOld";
            this.textEditOld.Size = new System.Drawing.Size(204, 20);
            this.textEditOld.StyleController = this.WorkTypeUpdatelayoutControl1ConvertedLayout;
            this.textEditOld.TabIndex = 1;
            // 
            // WorkTypeUpdatelayoutControl1ConvertedLayout
            // 
            this.WorkTypeUpdatelayoutControl1ConvertedLayout.Controls.Add(this.textEditNew);
            this.WorkTypeUpdatelayoutControl1ConvertedLayout.Controls.Add(this.textEditOld);
            this.WorkTypeUpdatelayoutControl1ConvertedLayout.Controls.Add(this.simpleButtonCancel);
            this.WorkTypeUpdatelayoutControl1ConvertedLayout.Controls.Add(this.simpleButtonOK);
            this.WorkTypeUpdatelayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WorkTypeUpdatelayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.WorkTypeUpdatelayoutControl1ConvertedLayout.Name = "WorkTypeUpdatelayoutControl1ConvertedLayout";
            this.WorkTypeUpdatelayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.WorkTypeUpdatelayoutControl1ConvertedLayout.Size = new System.Drawing.Size(267, 217);
            this.WorkTypeUpdatelayoutControl1ConvertedLayout.TabIndex = 6;
            // 
            // textEditNew
            // 
            this.textEditNew.Location = new System.Drawing.Point(51, 36);
            this.textEditNew.Name = "textEditNew";
            this.textEditNew.Size = new System.Drawing.Size(204, 20);
            this.textEditNew.StyleController = this.WorkTypeUpdatelayoutControl1ConvertedLayout;
            this.textEditNew.TabIndex = 3;
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.Location = new System.Drawing.Point(134, 79);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(121, 22);
            this.simpleButtonCancel.StyleController = this.WorkTypeUpdatelayoutControl1ConvertedLayout;
            this.simpleButtonCancel.TabIndex = 5;
            this.simpleButtonCancel.Text = "取消";
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButtonCancel_Click);
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(12, 79);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(118, 22);
            this.simpleButtonOK.StyleController = this.WorkTypeUpdatelayoutControl1ConvertedLayout;
            this.simpleButtonOK.TabIndex = 4;
            this.simpleButtonOK.Text = "修改";
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
            this.layoutControlItem4,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(267, 217);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEditNew;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "textEditNewitem";
            this.layoutControlItem1.Size = new System.Drawing.Size(247, 24);
            this.layoutControlItem1.Text = "新值：";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textEditOld;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "textEditOlditem";
            this.layoutControlItem2.Size = new System.Drawing.Size(247, 24);
            this.layoutControlItem2.Text = "旧值：";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButtonCancel;
            this.layoutControlItem3.Location = new System.Drawing.Point(122, 67);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(125, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(125, 26);
            this.layoutControlItem3.Name = "simpleButtonCancelitem";
            this.layoutControlItem3.Size = new System.Drawing.Size(125, 130);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButtonOK;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 67);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(122, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(122, 26);
            this.layoutControlItem4.Name = "simpleButtonOKitem";
            this.layoutControlItem4.Size = new System.Drawing.Size(122, 130);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 48);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 19);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 19);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(247, 19);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // WorkTypeUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 217);
            this.Controls.Add(this.WorkTypeUpdatelayoutControl1ConvertedLayout);
            this.Name = "WorkTypeUpdate";
            this.Text = "上班类型-修改";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SaleTypeUpdate_FormClosing);
            this.Load += new System.EventHandler(this.SaleTypeUpdate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEditOld.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkTypeUpdatelayoutControl1ConvertedLayout)).EndInit();
            this.WorkTypeUpdatelayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditNew.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit textEditOld;
        private DevExpress.XtraEditors.TextEdit textEditNew;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraLayout.LayoutControl WorkTypeUpdatelayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
    }
}