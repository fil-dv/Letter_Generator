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
            this.comboBox_adr = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_template = new System.Windows.Forms.TextBox();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLabel_template = new System.Windows.Forms.ToolStripStatusLabel();
            this.button_in_queue = new System.Windows.Forms.Button();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox_ready_regs = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_remove_reg = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button_plus = new System.Windows.Forms.Button();
            this.button_menus = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_creditors
            // 
            this.comboBox_creditors.FormattingEnabled = true;
            this.comboBox_creditors.Location = new System.Drawing.Point(23, 51);
            this.comboBox_creditors.Name = "comboBox_creditors";
            this.comboBox_creditors.Size = new System.Drawing.Size(232, 21);
            this.comboBox_creditors.TabIndex = 0;
            this.comboBox_creditors.SelectedIndexChanged += new System.EventHandler(this.comboBox_creditors_SelectedIndexChanged);
            // 
            // comboBox_regs
            // 
            this.comboBox_regs.FormattingEnabled = true;
            this.comboBox_regs.Location = new System.Drawing.Point(281, 50);
            this.comboBox_regs.Name = "comboBox_regs";
            this.comboBox_regs.Size = new System.Drawing.Size(347, 21);
            this.comboBox_regs.TabIndex = 1;
            this.comboBox_regs.SelectedIndexChanged += new System.EventHandler(this.comboBox_regs_SelectedIndexChanged);
            this.comboBox_regs.TextChanged += new System.EventHandler(this.comboBox_regs_TextChanged);
            // 
            // button_add_reg
            // 
            this.button_add_reg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_add_reg.Location = new System.Drawing.Point(553, 77);
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
            this.menuStrip1.Size = new System.Drawing.Size(657, 24);
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
            this.очиститьВсёToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.очиститьВсёToolStripMenuItem.Text = "Очистить всё";
            this.очиститьВсёToolStripMenuItem.Click += new System.EventHandler(this.Clean_up_ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripLabel_pins,
            this.toolStripStatusLabel2,
            this.toolStripLabel_template,
            this.toolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 565);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(657, 22);
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
            // comboBox_adr
            // 
            this.comboBox_adr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_adr.FormattingEnabled = true;
            this.comboBox_adr.Location = new System.Drawing.Point(431, 180);
            this.comboBox_adr.Name = "comboBox_adr";
            this.comboBox_adr.Size = new System.Drawing.Size(196, 21);
            this.comboBox_adr.TabIndex = 5;
            this.comboBox_adr.SelectedIndexChanged += new System.EventHandler(this.comboBox_adr_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(428, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Тип адреса:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(278, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Реестр:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Контрагент:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(428, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "ID шаблона:";
            // 
            // textBox_template
            // 
            this.textBox_template.Location = new System.Drawing.Point(431, 232);
            this.textBox_template.Name = "textBox_template";
            this.textBox_template.Size = new System.Drawing.Size(195, 20);
            this.textBox_template.TabIndex = 10;
            this.textBox_template.TextChanged += new System.EventHandler(this.textBox_template_TextChanged);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(76, 17);
            this.toolStripStatusLabel2.Text = "       Шаблон:";
            // 
            // toolStripLabel_template
            // 
            this.toolStripLabel_template.Name = "toolStripLabel_template";
            this.toolStripLabel_template.Size = new System.Drawing.Size(0, 17);
            // 
            // button_in_queue
            // 
            this.button_in_queue.Location = new System.Drawing.Point(432, 403);
            this.button_in_queue.Name = "button_in_queue";
            this.button_in_queue.Size = new System.Drawing.Size(195, 23);
            this.button_in_queue.TabIndex = 11;
            this.button_in_queue.Text = "Проверить очередь на генерацию";
            this.button_in_queue.UseVisualStyleBackColor = true;
            this.button_in_queue.Click += new System.EventHandler(this.button_in_queue_Click);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(0, 17);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(432, 441);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(196, 69);
            this.button1.TabIndex = 12;
            this.button1.Text = "Отправить на генерацию";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // comboBox_ready_regs
            // 
            this.comboBox_ready_regs.FormattingEnabled = true;
            this.comboBox_ready_regs.Location = new System.Drawing.Point(280, 106);
            this.comboBox_ready_regs.Name = "comboBox_ready_regs";
            this.comboBox_ready_regs.Size = new System.Drawing.Size(347, 21);
            this.comboBox_ready_regs.TabIndex = 13;
            this.comboBox_ready_regs.SelectedIndexChanged += new System.EventHandler(this.comboBox_ready_regs_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(277, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Выбранные реестры:";
            // 
            // button_remove_reg
            // 
            this.button_remove_reg.Location = new System.Drawing.Point(552, 133);
            this.button_remove_reg.Name = "button_remove_reg";
            this.button_remove_reg.Size = new System.Drawing.Size(75, 23);
            this.button_remove_reg.TabIndex = 15;
            this.button_remove_reg.Text = "Убрать";
            this.button_remove_reg.UseVisualStyleBackColor = true;
            this.button_remove_reg.Click += new System.EventHandler(this.button_remove_reg_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(462, 292);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Посмотреть список пинов:";
            // 
            // button_plus
            // 
            this.button_plus.Location = new System.Drawing.Point(431, 320);
            this.button_plus.Name = "button_plus";
            this.button_plus.Size = new System.Drawing.Size(95, 23);
            this.button_plus.TabIndex = 17;
            this.button_plus.Text = "+";
            this.button_plus.UseVisualStyleBackColor = true;
            // 
            // button_menus
            // 
            this.button_menus.Location = new System.Drawing.Point(534, 320);
            this.button_menus.Name = "button_menus";
            this.button_menus.Size = new System.Drawing.Size(94, 23);
            this.button_menus.TabIndex = 18;
            this.button_menus.Text = "-";
            this.button_menus.UseVisualStyleBackColor = true;
            // 
            // Form_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 587);
            this.Controls.Add(this.button_menus);
            this.Controls.Add(this.button_plus);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button_remove_reg);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox_ready_regs);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_in_queue);
            this.Controls.Add(this.textBox_template);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_adr);
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
        private System.Windows.Forms.ComboBox comboBox_adr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_template;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLabel_template;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.Button button_in_queue;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox_ready_regs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_remove_reg;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_plus;
        private System.Windows.Forms.Button button_menus;
    }
}

