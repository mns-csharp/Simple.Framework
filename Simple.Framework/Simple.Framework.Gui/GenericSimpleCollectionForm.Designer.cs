namespace Simple.Framework.Gui
{
    partial class GenericSimpleCollectionForm<T> : BaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenericSimpleCollectionForm<T>));
            this.myListBox = new System.Windows.Forms.ListBox();
            this.myLabel = new System.Windows.Forms.Label();
            this.myAddButton = new System.Windows.Forms.Button();
            this.myRemoveButton = new System.Windows.Forms.Button();
            this.myCloseButton = new System.Windows.Forms.Button();
            this.myTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // myListBox
            // 
            this.myListBox.FormattingEnabled = true;
            this.myListBox.ItemHeight = 14;
            this.myListBox.Location = new System.Drawing.Point(63, 32);
            this.myListBox.Name = "myListBox";
            this.myListBox.Size = new System.Drawing.Size(307, 228);
            this.myListBox.TabIndex = 1;
            // 
            // myLabel
            // 
            this.myLabel.AutoSize = true;
            this.myLabel.Location = new System.Drawing.Point(15, 32);
            this.myLabel.Name = "myLabel";
            this.myLabel.Size = new System.Drawing.Size(38, 14);
            this.myLabel.TabIndex = 2;
            this.myLabel.Text = "Name";
            // 
            // myAddButton
            // 
            this.myAddButton.Location = new System.Drawing.Point(378, 33);
            this.myAddButton.Name = "myAddButton";
            this.myAddButton.Size = new System.Drawing.Size(87, 25);
            this.myAddButton.TabIndex = 3;
            this.myAddButton.Text = "Add";
            this.myAddButton.UseVisualStyleBackColor = true;
            // 
            // myRemoveButton
            // 
            this.myRemoveButton.Location = new System.Drawing.Point(378, 66);
            this.myRemoveButton.Name = "myRemoveButton";
            this.myRemoveButton.Size = new System.Drawing.Size(87, 25);
            this.myRemoveButton.TabIndex = 4;
            this.myRemoveButton.Text = "Remove";
            this.myRemoveButton.UseVisualStyleBackColor = true;
            // 
            // myCloseButton
            // 
            this.myCloseButton.Location = new System.Drawing.Point(378, 98);
            this.myCloseButton.Name = "myCloseButton";
            this.myCloseButton.Size = new System.Drawing.Size(87, 25);
            this.myCloseButton.TabIndex = 5;
            this.myCloseButton.Text = "Close";
            this.myCloseButton.UseVisualStyleBackColor = true;
            // 
            // myTextBox
            // 
            this.myTextBox.Location = new System.Drawing.Point(63, 4);
            this.myTextBox.Name = "myTextBox";
            this.myTextBox.Size = new System.Drawing.Size(400, 22);
            this.myTextBox.TabIndex = 6;
            // 
            // GenericSimpleCollectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.ClientSize = new System.Drawing.Size(532, 290);
            this.Controls.Add(this.myTextBox);
            this.Controls.Add(this.myCloseButton);
            this.Controls.Add(this.myRemoveButton);
            this.Controls.Add(this.myAddButton);
            this.Controls.Add(this.myLabel);
            this.Controls.Add(this.myListBox);
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$earth.Icon")));
            this.Name = "GenericSimpleCollectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GenericSimpleCollectionForm";
            this.Controls.SetChildIndex(this.myListBox, 0);
            this.Controls.SetChildIndex(this.myLabel, 0);
            this.Controls.SetChildIndex(this.myAddButton, 0);
            this.Controls.SetChildIndex(this.myRemoveButton, 0);
            this.Controls.SetChildIndex(this.myCloseButton, 0);
            this.Controls.SetChildIndex(this.myTextBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox myListBox;
        private System.Windows.Forms.Label myLabel;
        private System.Windows.Forms.Button myAddButton;
        private System.Windows.Forms.Button myRemoveButton;
        private System.Windows.Forms.Button myCloseButton;
        private System.Windows.Forms.TextBox myTextBox;

        
    }
}