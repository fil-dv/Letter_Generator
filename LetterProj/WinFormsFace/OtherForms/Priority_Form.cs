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
        string _pathToFile = "";
        public Priority_Form()
        {
            InitializeComponent();
            InitControls();
            PriorityManager.CreateConnect();
            EventSubscription();
        }

        void Reset()
        {
            label_all.Text = "";
            label_update.Text = "";
        }

        private void EventSubscription()
        {
            PriorityManager.FileLoadCompleted += PriorityManager_FileLoadCompleted;
            label_all.TextChanged += Label_all_TextChanged;
            label_update.TextChanged += Label_update_TextChanged;
        }

        private void Label_update_TextChanged(object sender, EventArgs e)
        {
            SetButtonEnable();
        }

        private void Label_all_TextChanged(object sender, EventArgs e)
        {
            SetButtonEnable();
        }

        private void SetButtonEnable()
        {
            if ((label_all.Text.Length + label_update.Text.Length) > 37) // 13 + 23
            {
                button_upd_prior.Enabled = true;
            }
            else
            {
                button_upd_prior.Enabled = false;
            }
        }


        private void PriorityManager_FileLoadCompleted(bool obj)
        {            
            int readyForUpdate = -1;
            if (InvokeRequired)
            {
                Action action = () =>
                {
                    string priorValue = comboBox_priority.SelectedItem.ToString();
                    readyForUpdate = PriorityManager.CheckUpdatePriority(priorValue);
                    label_update.Text = "Приоритет будет поднят для: " + readyForUpdate.ToString();
                };

                Invoke(action); 
            }
            else
            {
                string priorValue = comboBox_priority.SelectedItem.ToString();
                readyForUpdate = PriorityManager.CheckUpdatePriority(priorValue);
                label_update.Text = "Приоритет будет поднят для: " + readyForUpdate.ToString();
            }
        }



        private void InitControls()
        {
            comboBox_priority.Items.Add("251");
            comboBox_priority.Items.Add("255");
            button_open_file_priority.Enabled = false;
        }

        private void button_open_file_priority_Click(object sender, EventArgs e)
        {
            Reset();
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
                    _pathToFile = ofd.FileName;
                    if ((myStream = ofd.OpenFile()) != null)
                    {   
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
                        int allCount = pinList.Count;
                        label_all.Text = "Дел в файле: " + allCount.ToString();
                        PriorityManager.AddPinFromFile(pinList);
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

        private void button_upd_prior_Click(object sender, EventArgs e)
        {
            string priorValue = comboBox_priority.SelectedItem.ToString();
            PriorityManager.UpdatePriority(priorValue);
        }

        private void button_priority_report_Click(object sender, EventArgs e)
        {
            //string pathToFolder = _pathToFile.Substring(0, _pathToFile.LastIndexOf("\\")) + "\\";
            //PriorityManager.CreateExcelReport(pathToFolder);
            PriorityManager.CreateExcelReport();
        }
    }
}
