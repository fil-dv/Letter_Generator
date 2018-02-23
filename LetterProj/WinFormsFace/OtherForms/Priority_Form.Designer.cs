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
            // Priority_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 280);
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
    }
}