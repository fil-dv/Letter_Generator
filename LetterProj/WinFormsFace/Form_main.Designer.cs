namespace WinFormsFace
{
    partial class Form_main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_main));
            this.comboBox_creditors = new System.Windows.Forms.ComboBox();
            this.comboBox_regs = new System.Windows.Forms.ComboBox();
            this.button_add_reg = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.очиститьВсёToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLabel_pins = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_creditors
            // 
            this.comboBox_creditors.FormattingEnabled = true;
            this.comboBox_creditors.Location = new System.Drawing.Point(25, 43);
            this.comboBox_creditors.Name = "comboBox_creditors";
            this.comboBox_creditors.Size = new System.Drawing.Size(232, 21);
            this.comboBox_creditors.TabIndex = 0;
            this.comboBox_creditors.SelectedIndexChanged += new System.EventHandler(this.comboBox_creditors_SelectedIndexChanged);
            // 
            // comboBox_regs
            // 
            this.comboBox_regs.FormattingEnabled = true;
            this.comboBox_regs.Location = new System.Drawing.Point(283, 42);
            this.comboBox_regs.Name = "comboBox_regs";
            this.comboBox_regs.Size = new System.Drawing.Size(347, 21);
            this.comboBox_regs.TabIndex = 1;
            this.comboBox_regs.SelectedIndexChanged += new System.EventHandler(this.comboBox_regs_SelectedIndexChanged);
            this.comboBox_regs.TextChanged += new System.EventHandler(this.comboBox_regs_TextChanged);
            // 
            // button_add_reg
            // 
            this.button_add_reg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_add_reg.Location = new System.Drawing.Point(660, 40);
            this.button_add_reg.Name = "button_add_reg";
            this.button_add_reg.Size = new System.Drawing.Size(75, 23);
            this.button_add_reg.TabIndex = 2;
            this.button_add_reg.Text = "Добавить";
            this.button_add_reg.UseVisualStyleBackColor = true;
            this.button_add_reg.Click += new System.EventHandler(this.button_add_reg_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(766, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.очиститьВсёToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.файлToolStripMenuItem.Text = "Письма";
            // 
            // очиститьВсёToolStripMenuItem
            // 
            this.очиститьВсёToolStripMenuItem.Name = "очиститьВсёToolStripMenuItem";
            this.очиститьВсёToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.очиститьВсёToolStripMenuItem.Text = "Очистить всё";
            this.очиститьВсёToolStripMenuItem.Click += new System.EventHandler(this.Clean_up_ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripLabel_pins});
            this.statusStrip1.Location = new System.Drawing.Point(0, 242);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(766, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip_down";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(100, 17);
            this.toolStripStatusLabel1.Text = "Выбрано пинов: ";
            // 
            // toolStripLabel_pins
            // 
            this.toolStripLabel_pins.Name = "toolStripLabel_pins";
            this.toolStripLabel_pins.Size = new System.Drawing.Size(13, 17);
            this.toolStripLabel_pins.Text = "0";
            // 
            // Form_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 264);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button_add_reg);
            this.Controls.Add(this.comboBox_regs);
            this.Controls.Add(this.comboBox_creditors);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Letters";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_creditors;
        private System.Windows.Forms.ComboBox comboBox_regs;
        private System.Windows.Forms.Button button_add_reg;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLabel_pins;
        private System.Windows.Forms.ToolStripMenuItem очиститьВсёToolStripMenuItem;
    }
}

