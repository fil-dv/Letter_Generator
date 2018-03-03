namespace WinFormsFace.OtherForms
{
    partial class Priority_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Priority_Form));
            this.comboBox_priority = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_open_file_priority = new System.Windows.Forms.Button();
            this.label_update = new System.Windows.Forms.Label();
            this.label_all = new System.Windows.Forms.Label();
            this.button_upd_prior = new System.Windows.Forms.Button();
            this.button_priority_report = new System.Windows.Forms.Button();
            this.statusStrip_prior = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_251 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_255 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.statusStrip_prior.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_priority
            // 
            this.comboBox_priority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_priority.FormattingEnabled = true;
            this.comboBox_priority.Location = new System.Drawing.Point(146, 28);
            this.comboBox_priority.Name = "comboBox_priority";
            this.comboBox_priority.Size = new System.Drawing.Size(72, 21);
            this.comboBox_priority.TabIndex = 0;
            this.comboBox_priority.SelectedIndexChanged += new System.EventHandler(this.comboBox_priority_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Приоритет:";
            // 
            // button_open_file_priority
            // 
            this.button_open_file_priority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_open_file_priority.Location = new System.Drawing.Point(67, 70);
            this.button_open_file_priority.Name = "button_open_file_priority";
            this.button_open_file_priority.Size = new System.Drawing.Size(151, 23);
            this.button_open_file_priority.TabIndex = 3;
            this.button_open_file_priority.Text = "Загрузить файл";
            this.button_open_file_priority.UseVisualStyleBackColor = true;
            this.button_open_file_priority.Click += new System.EventHandler(this.button_open_file_priority_Click);
            // 
            // label_update
            // 
            this.label_update.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_update.AutoSize = true;
            this.label_update.Location = new System.Drawing.Point(49, 188);
            this.label_update.Name = "label_update";
            this.label_update.Size = new System.Drawing.Size(0, 13);
            this.label_update.TabIndex = 4;
            // 
            // label_all
            // 
            this.label_all.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_all.AutoSize = true;
            this.label_all.Location = new System.Drawing.Point(91, 123);
            this.label_all.Name = "label_all";
            this.label_all.Size = new System.Drawing.Size(0, 13);
            this.label_all.TabIndex = 5;
            // 
            // button_upd_prior
            // 
            this.button_upd_prior.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_upd_prior.Enabled = false;
            this.button_upd_prior.Location = new System.Drawing.Point(67, 239);
            this.button_upd_prior.Name = "button_upd_prior";
            this.button_upd_prior.Size = new System.Drawing.Size(151, 23);
            this.button_upd_prior.TabIndex = 6;
            this.button_upd_prior.Text = "Поднять приоритет";
            this.button_upd_prior.UseVisualStyleBackColor = true;
            this.button_upd_prior.Click += new System.EventHandler(this.button_upd_prior_Click);
            // 
            // button_priority_report
            // 
            this.button_priority_report.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_priority_report.Location = new System.Drawing.Point(34, 322);
            this.button_priority_report.Name = "button_priority_report";
            this.button_priority_report.Size = new System.Drawing.Size(213, 23);
            this.button_priority_report.TabIndex = 7;
            this.button_priority_report.Text = "Выгрузить отчет за указанный период";
            this.button_priority_report.UseVisualStyleBackColor = true;
            this.button_priority_report.Click += new System.EventHandler(this.button_priority_report_Click);
            // 
            // statusStrip_prior
            // 
            this.statusStrip_prior.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel1,
            this.toolStrip_251,
            this.toolStripStatusLabel2,
            this.toolStrip_255});
            this.statusStrip_prior.Location = new System.Drawing.Point(0, 370);
            this.statusStrip_prior.Name = "statusStrip_prior";
            this.statusStrip_prior.Size = new System.Drawing.Size(280, 22);
            this.statusStrip_prior.TabIndex = 8;
            this.statusStrip_prior.Text = "statusStrip1";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(55, 17);
            this.toolStripStatusLabel3.Text = "Сегодня:";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(36, 3, 0, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(28, 17);
            this.toolStripStatusLabel1.Text = "251:";
            // 
            // toolStrip_251
            // 
            this.toolStrip_251.Name = "toolStrip_251";
            this.toolStrip_251.Size = new System.Drawing.Size(13, 17);
            this.toolStrip_251.Text = "0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Margin = new System.Windows.Forms.Padding(36, 3, 0, 2);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(28, 17);
            this.toolStripStatusLabel2.Text = "255:";
            // 
            // toolStrip_255
            // 
            this.toolStrip_255.Name = "toolStrip_255";
            this.toolStrip_255.Size = new System.Drawing.Size(13, 17);
            this.toolStrip_255.Text = "0";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(153, 296);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(94, 20);
            this.dateTimePicker2.TabIndex = 10;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(34, 296);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(94, 20);
            this.dateTimePicker1.TabIndex = 11;
            // 
            // Priority_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 392);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.statusStrip_prior);
            this.Controls.Add(this.button_priority_report);
            this.Controls.Add(this.button_upd_prior);
            this.Controls.Add(this.label_all);
            this.Controls.Add(this.label_update);
            this.Controls.Add(this.button_open_file_priority);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_priority);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Priority_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Приоритеты";
            this.statusStrip_prior.ResumeLayout(false);
            this.statusStrip_prior.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_priority;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_open_file_priority;
        private System.Windows.Forms.Label label_update;
        private System.Windows.Forms.Label label_all;
        private System.Windows.Forms.Button button_upd_prior;
        private System.Windows.Forms.Button button_priority_report;
        private System.Windows.Forms.StatusStrip statusStrip_prior;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStrip_251;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStrip_255;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}