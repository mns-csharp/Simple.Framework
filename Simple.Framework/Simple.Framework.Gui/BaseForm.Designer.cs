namespace Simple.Framework.Gui
{
    partial class BaseForm
    {
        
        #region MyRegion
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
        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.baseStatusStrip = new System.Windows.Forms.StatusStrip();
            this.unusedFirstToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.baseTotalToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.baseMessageToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.baseStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // baseStatusStrip
            // 
            this.baseStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unusedFirstToolStripStatusLabel,
            this.baseTotalToolStripStatusLabel,
            this.toolStripStatusLabel,
            this.baseMessageToolStripStatusLabel});
            this.baseStatusStrip.Location = new System.Drawing.Point(0, 272);
            this.baseStatusStrip.Name = "baseStatusStrip";
            this.baseStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.baseStatusStrip.Size = new System.Drawing.Size(341, 22);
            this.baseStatusStrip.TabIndex = 0;
            this.baseStatusStrip.Text = "statusStrip1";
            // 
            // unusedFirstToolStripStatusLabel
            // 
            this.unusedFirstToolStripStatusLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.unusedFirstToolStripStatusLabel.Name = "unusedFirstToolStripStatusLabel";
            this.unusedFirstToolStripStatusLabel.Size = new System.Drawing.Size(78, 17);
            this.unusedFirstToolStripStatusLabel.Text = "Total Count :";
            // 
            // baseTotalToolStripStatusLabel
            // 
            this.baseTotalToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.baseTotalToolStripStatusLabel.Name = "baseTotalToolStripStatusLabel";
            this.baseTotalToolStripStatusLabel.Size = new System.Drawing.Size(31, 17);
            this.baseTotalToolStripStatusLabel.Text = "1000";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(37, 17);
            this.toolStripStatusLabel.Text = "          ";
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // baseMessageToolStripStatusLabel
            // 
            this.baseMessageToolStripStatusLabel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.baseMessageToolStripStatusLabel.Name = "baseMessageToolStripStatusLabel";
            this.baseMessageToolStripStatusLabel.Size = new System.Drawing.Size(12, 17);
            this.baseMessageToolStripStatusLabel.Text = ".";
            this.baseMessageToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 294);
            this.Controls.Add(this.baseStatusStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BaseForm";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BaseForm_KeyPress);
            this.baseStatusStrip.ResumeLayout(false);
            this.baseStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion        

        private System.Windows.Forms.StatusStrip baseStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel unusedFirstToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel baseTotalToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel baseMessageToolStripStatusLabel;
    }
}