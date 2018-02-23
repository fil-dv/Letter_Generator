using MyLetterManager;
using MyLetterManager.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsFace.OtherForms
{
    public partial class Priority_Form : Form
    {
        public Priority_Form()
        {
            InitializeComponent();
            InitControls();
            PriorityManager.CreateConnect();
        }

        private void InitControls()
        {
            comboBox_priority.Items.Add("251");
            comboBox_priority.Items.Add("255");
            button_open_file_priority.Enabled = false;
        }

        private void button_open_file_priority_Click(object sender, EventArgs e)
        {
            if (comboBox_priority.Text.Length < 1)
            {
                MessageBox.Show("Выберите приоритет.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Stream myStream = null;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open Text File";
            ofd.Filter = "TXT files|*.txt";
            ofd.InitialDirectory = @"C:\";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = ofd.OpenFile()) != null)
                    {                        
                        string prior = comboBox_priority.SelectedItem.ToString();
                        List<Deal> pinList = new List<Deal>();
                        using (myStream)
                        {
                            var lines = File.ReadLines(ofd.FileName);
                            foreach (var line in lines)
                            {
                                Deal deal = new Deal { DealId = Convert.ToDecimal(line) };
                                pinList.Add(deal);
                            }
                        }
                        string priorValue = comboBox_priority.SelectedItem.ToString();
                        int allCount = pinList.Count;
                        int readyForUpdate = PriorityManager.CheckUpdatePriority(pinList, priorValue);
                        MessageBox.Show("Дел в файле - " + allCount + ", приоритет будет поднят по " + readyForUpdate, priorValue);
                    }                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удается прочитать файл. " + ex.Message);
                }
            }
        }

        private void comboBox_priority_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_priority.Text.Length > 0)
            {
                button_open_file_priority.Enabled = true;
            }
            else
            {
                button_open_file_priority.Enabled = false;
            }
        }
    }
}
