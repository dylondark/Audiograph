using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Audiograph
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frmAPIHistory : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is not null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAPIHistory));
            btnClose = new Button();
            btnClose.Click += new EventHandler(ExitForm);
            ltvStatus = new ListView();
            Type = new ColumnHeader();
            Method = new ColumnHeader();
            Status = new ColumnHeader();
            Latency = new ColumnHeader();
            Time = new ColumnHeader();
            Thread = new ColumnHeader();
            tmrListUpdate = new Timer(components);
            tmrListUpdate.Tick += new EventHandler(AddListView);
            btnClear = new Button();
            btnClear.Click += new EventHandler(ClearHistory);
            SuspendLayout();
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Location = new Point(697, 526);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 0;
            btnClose.Text = "&Close";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // ltvStatus
            // 
            ltvStatus.Columns.AddRange(new ColumnHeader[] { Type, Method, Status, Latency, Time, Thread });
            ltvStatus.FullRowSelect = true;
            ltvStatus.GridLines = true;
            ltvStatus.HideSelection = false;
            ltvStatus.Location = new Point(12, 12);
            ltvStatus.Name = "ltvStatus";
            ltvStatus.Size = new Size(760, 508);
            ltvStatus.TabIndex = 1;
            ltvStatus.UseCompatibleStateImageBehavior = false;
            ltvStatus.View = View.Details;
            // 
            // Type
            // 
            Type.Text = "Type";
            // 
            // Method
            // 
            Method.Text = "Method";
            Method.Width = 120;
            // 
            // Status
            // 
            Status.Text = "Status";
            Status.Width = 240;
            // 
            // Latency
            // 
            Latency.Text = "Latency";
            // 
            // Time
            // 
            Time.Text = "Time";
            Time.Width = 150;
            // 
            // Thread
            // 
            Thread.Text = "Thread";
            Thread.Width = 90;
            // 
            // tmrListUpdate
            // 
            tmrListUpdate.Enabled = true;
            tmrListUpdate.Interval = 15;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnClear.Location = new Point(12, 526);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 23);
            btnClear.TabIndex = 2;
            btnClear.Text = "C&lear";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // frmAPIHistory
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(btnClear);
            Controls.Add(ltvStatus);
            Controls.Add(btnClose);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmAPIHistory";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "API Call History";
            Resize += new EventHandler(ResizeOps);
            ResumeLayout(false);

        }
        internal Button btnClose;
        internal ListView ltvStatus;
        internal ColumnHeader Type;
        internal ColumnHeader Method;
        internal ColumnHeader Status;
        internal ColumnHeader Latency;
        internal ColumnHeader Time;
        internal ColumnHeader Thread;
        internal Timer tmrListUpdate;
        internal Button btnClear;
    }
}