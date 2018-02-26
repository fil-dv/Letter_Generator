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
            this.SuspendLayout();
            // 
            // comboBox_priority
            // 
            this.comboBox_priority.FormattingEnabled = true;
            this.comboBox_priority.Location = new System.Drawing.Point(124, 28);
            this.comboBox_priority.Name = "comboBox_priority";
            this.comboBox_priority.Size = new System.Drawing.Size(72, 21);
            this.comboBox_priority.TabIndex = 0;
            this.comboBox_priority.SelectedIndexChanged += new System.EventHandler(this.comboBox_priority_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Приоритет:";
            // 
            // button_open_file_priority
            // 
            this.button_open_file_priority.Location = new System.Drawing.Point(45, 70);
            this.button_open_file_priority.Name = "button_open_file_priority";
            this.button_open_file_priority.Size = new System.Drawing.Size(151, 23);
            this.button_open_file_priority.TabIndex = 3;
            this.button_open_file_priority.Text = "Загрузить файл";
            this.button_open_file_priority.UseVisualStyleBackColor = true;
            this.button_open_file_priority.Click += new System.EventHandler(this.button_open_file_priority_Click);
            // 
            // label_update
            // 
            this.label_update.AutoSize = true;
            this.label_update.Location = new System.Drawing.Point(28, 156);
            this.label_update.Name = "label_update";
            this.label_update.Size = new System.Drawing.Size(0, 13);
            this.label_update.TabIndex = 4;
            // 
            // label_all
            // 
            this.label_all.AutoSize = true;
            this.label_all.Location = new System.Drawing.Point(61, 121);
            this.label_all.Name = "label_all";
            this.label_all.Size = new System.Drawing.Size(0, 13);
            this.label_all.TabIndex = 5;
            // 
            // button_upd_prior
            // 
            this.button_upd_prior.Enabled = false;
            this.button_upd_prior.Location = new System.Drawing.Point(45, 205);
            this.button_upd_prior.Name = "button_upd_prior";
            this.button_upd_prior.Size = new System.Drawing.Size(151, 23);
            this.button_upd_prior.TabIndex = 6;
            this.button_upd_prior.Text = "Поднять приоритет";
            this.button_upd_prior.UseVisualStyleBackColor = true;
            this.button_upd_prior.Click += new System.EventHandler(this.button_upd_prior_Click);
            // 
            // Priority_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 257);
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
    }
}