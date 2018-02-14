namespace WinFormsFace.OtherForms
{
    partial class Form_regs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_regs));
            this.button_add_regs = new System.Windows.Forms.Button();
            this.listBox_regs = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button_add_regs
            // 
            this.button_add_regs.Location = new System.Drawing.Point(292, 36);
            this.button_add_regs.Name = "button_add_regs";
            this.button_add_regs.Size = new System.Drawing.Size(75, 23);
            this.button_add_regs.TabIndex = 1;
            this.button_add_regs.Text = "Ok";
            this.button_add_regs.UseVisualStyleBackColor = true;
          
            // 
            // listBox_regs
            // 
            this.listBox_regs.FormattingEnabled = true;
            this.listBox_regs.Location = new System.Drawing.Point(0, 0);
            this.listBox_regs.Name = "listBox_regs";
            this.listBox_regs.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox_regs.Size = new System.Drawing.Size(367, 30);
            this.listBox_regs.TabIndex = 2;
            // 
            // Form_regs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(500, 500);
            this.ClientSize = new System.Drawing.Size(369, 106);
            this.Controls.Add(this.listBox_regs);
            this.Controls.Add(this.button_add_regs);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(500, 500);
            this.Name = "Form_regs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_add_regs;
        private System.Windows.Forms.ListBox listBox_regs;
    }
}