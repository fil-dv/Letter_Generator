using MyLetterManager;
using MyLetterManager.Mediator;
using MyLetterManager.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsFace.OtherForms;

namespace WinFormsFace
{
    public partial class Form_main : Form
    {
        List<CheckBox> _checkBoxeslist = new List<CheckBox>();

        public Form_main()
        {
            InitHendlers();
            InitializeComponent();
            LetterManager.CreateConnect();
            FillCheckBoxList();
            ResetControls();
            SetTextBoxSumSettings();        
        }

        private void InitHendlers()
        {
            LetterManager.FileLoadCompleted += LetterManager_FileLoadCompleted;
        }

        private void SetTextBoxSumSettings()
        {
            textBox_summa.Location = new Point(220, 193);
        }

        void FillCheckBoxList()
        {
            try
            {
                for (int i = 0; i < LetterManager.GetConditionsList().Count; i++)
                {
                    CheckBox cb = new CheckBox();
                    cb.Left = 50;
                    cb.Top = 170 + (i * 24);
                    cb.Width = 350;
                    _checkBoxeslist.Add(cb);
                }
                CreateHandlers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception from WinFormsFace.Form_main.FillCheckBoxList() " + ex.Message);
            }
        }

        void ClearCreditorsCombo()
        {
            comboBox_creditors.Items.Clear();
            comboBox_creditors.Text = "";
        }

        void ClearRegsCombo()
        {
            comboBox_regs.Items.Clear();
            comboBox_regs.Text = "";
        }

        void ClearReadyRegsCombo()
        {
            comboBox_ready_regs.Items.Clear();
            comboBox_ready_regs.Text = "";
        }

        void ClearTemplateCombo()
        {
            comboBox_template.Items.Clear();
            comboBox_template.Text = "";
        }

        void InitCreditorsCombo()
        {
            comboBox_creditors.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_creditors.AutoCompleteSource = AutoCompleteSource.ListItems;
            List<Creditor> creditorList = LetterManager.GetCreditorList();
            foreach (var item in creditorList)
            {
                comboBox_creditors.Items.Add(item.Alias.Trim());
            }
        }

        void InitTemplateCombo()
        {
            comboBox_template.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_template.AutoCompleteSource = AutoCompleteSource.ListItems;
            List<LetterTemplate> templateList = LetterManager.GetTemplateList();
            foreach (var item in templateList)
            {
                comboBox_template.Items.Add(item.Id.ToString() /*+ " - "  + item.Name.Trim()*/);
            }            
        }

        void InitRegsCombo()
        {
            comboBox_regs.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_regs.AutoCompleteSource = AutoCompleteSource.ListItems;
            decimal creditorId = LetterManager.GetCreditorIdByTrimedAlias(comboBox_creditors.SelectedItem.ToString());
            List<Reg> regList = LetterManager.GetRegListByCreditorId(creditorId);
            foreach (var item in regList)
            {
                comboBox_regs.Items.Add(item.Name + ", ID - " + item.Id);
            }
        }

        private void comboBox_creditors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_creditors.Text.Length > 0)
            {
                button_load_file.Enabled = false;
                comboBox_regs.Enabled = true;
                comboBox_ready_regs.Enabled = true;
            }
            else
            {
                button_load_file.Enabled = true;
            }            
            InitRegsCombo();
            comboBox_regs.Focus();
           // Mediator.CurrentCreditor = comboBox_creditors.SelectedItem.ToString();
           // InitRegForm();
        }

        private void InitRegForm()
        {
            Form_regs fr = new Form_regs();
            fr.FormBorderStyle = FormBorderStyle.FixedDialog;
            fr.MaximizeBox = false;
            fr.MinimizeBox = false;
            fr.StartPosition = FormStartPosition.CenterScreen;
            fr.ShowDialog();
        }

        private void Clean_up_ToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            ResetControls();
        }

        void InitAdrCombo()
        {
            comboBox_adr.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_adr.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox_adr.Items.Clear();
            comboBox_adr.Items.Add("Прописка");
            comboBox_adr.Items.Add("Фактический");
            comboBox_adr.Items.Add("АВР");
            comboBox_adr.Items.Add("Прописка + Фактический");
            comboBox_adr.Items.Add("Прописка + АВР");
            comboBox_adr.Items.Add("Прописка + Фактический + АВР");
            
            comboBox_adr.Items.Add("Рабочий");
           // comboBox_adr.SelectedIndex = 0;
        }

        void ResetControls()
        {
            this.Height = 271 + (LetterManager.GetConditionsList().Count * 24);
            LetterManager.ResetData();
            ClearCreditorsCombo();
            ClearRegsCombo();
            ClearReadyRegsCombo();
            ClearAdrCombo();
            ClearTemplateCombo();
            toolStripLabel_pins.Text = "0";
            toolStrip_template.Text = "";
            button_load_file.Text = "Загрузить из файла";
            button_add_reg.Enabled = false;
            button_remove_reg.Enabled = false;
            InitAdrCombo();
            InitCreditorsCombo();
            InitTemplateCombo();
            InitConditions();
            button_load_file.Enabled = false;
            comboBox_creditors.Enabled = false;
            comboBox_regs.Enabled = false;
            comboBox_ready_regs.Enabled = false;
        }

        private void ClearAdrCombo()
        {
            comboBox_adr.Items.Clear();
            comboBox_adr.Text = "";
        }

        void InitConditions()
        {
            List<Condition> conditionList = LetterManager.GetConditionsList();

            try
            {
                for (int i = 0; i < conditionList.Count; i++)
                {
                    _checkBoxeslist[i].Text = conditionList[i].Text;
                    _checkBoxeslist[i].Name = conditionList[i].Id.ToString();
                    if (conditionList[i].IsUsed)
                    {
                        _checkBoxeslist[i].Checked = true;
                    }
                    else
                    {
                        _checkBoxeslist[i].Checked = false;
                    }
                    this.Controls.Add(_checkBoxeslist[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception from WinFormsFace.Form_main.InitConditions() " + ex.Message);
            }
        }

        void CreateHandlers()
        {
            try
            {
                foreach (var item in _checkBoxeslist)
                {
                    item.CheckStateChanged += Item_CheckStateChanged;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception from WinFormsFace.Form_main.CreateHandlers() " + ex.Message);
            }
        }

        private void Item_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox cb = sender as CheckBox;
                if (cb.CheckState == CheckState.Checked)
                {
                    LetterManager.ChangeConditionUsing(cb.Name, true);
                }
                else
                {
                    LetterManager.ChangeConditionUsing(cb.Name, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception from WinFormsFace.Form_main.Item_CheckStateChanged() " + ex.Message);
            }
        }

        void CheckButtonAddEnable()
        {
            if (comboBox_regs.Text.Length > 0) button_add_reg.Enabled = true;
            else button_add_reg.Enabled = false;
        }

        void CheckButtonRemoveEnable()
        {
            if (comboBox_ready_regs.Items.Count > 0)
            {
                button_remove_reg.Enabled = true;
            }
            else
            {
                button_remove_reg.Enabled = false;
            }
        }

        

        private void comboBox_regs_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckButtonAddEnable();
            if (button_add_reg.Enabled)
            {
                button_add_reg.Focus();
            }

        }

        private void comboBox_regs_TextChanged(object sender, EventArgs e)
        {
            CheckButtonAddEnable();
        }

        bool CheckIsRegNameCorrect(ref string regName, ref decimal id)
        {
            bool isExist = false;
            
            string[] arr = regName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string lastItem = "";
            if (arr.Count() > 0)
            {
                lastItem = arr[arr.Count() - 1];
                id = Convert.ToUInt32(lastItem);
            }           
            if (LetterManager.IsRegExist(id))
            {
                isExist = true;
            }
            return isExist;
        }

        private void button_add_reg_Click(object sender, EventArgs e)
        {
            decimal id = -1;
            string regName = comboBox_regs.SelectedItem.ToString();
            if (CheckIsRegNameCorrect(ref regName, ref id))
            {
                if (!CheckReadyToLoadData())
                {
                    MessageBox.Show("Не указан тип адреса или шаблон письма.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                string template = comboBox_template.SelectedItem.ToString();
                AdressType adrtype = (AdressType)comboBox_adr.SelectedIndex;
                RecordToInsert record = new RecordToInsert();
                record.Reestr = new Reg();
                record.Reestr.Id = id;
                record.AdrType = adrtype;
                record.TemplateId = template;
                LetterManager.ChangeRegForGenerate(record, Operation.Insert);
                //RefreshToolStripPin();
                comboBox_ready_regs.Items.Add(regName);
                comboBox_regs.Items.Remove(regName);
                comboBox_regs.Text = "";
                button_add_reg.Enabled = false;        
            }
        }

        void RefreshToolStripPin()
        {
            int countForGenerate = LetterManager.GetPinCountToGenerate();
            toolStripLabel_pins.Text = countForGenerate.ToString();
        }

        private void comboBox_adr_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckControlsEnables();
            if (comboBox_adr.Text.Length > 1)
            {
                comboBox_template.Focus();
            }
        }

        private void CheckControlsEnables()
        {
            if (comboBox_adr.Text.Length < 1 || comboBox_template.Text.Length < 1)
            {
                button_load_file.Enabled = false;
                comboBox_creditors.Enabled = false;
            }
            else
            {
                button_load_file.Enabled = true;
                comboBox_creditors.Enabled = true;
            }
        }

        private void button_in_queue_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Писем в очереди на генерацию: " + LetterManager.GetCountInQueueToGeneration().ToString(), "Letters", MessageBoxButtons.OK);
        }

        private void comboBox_ready_regs_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckButtonRemoveEnable();
        }

        private void button_remove_reg_Click(object sender, EventArgs e)
        {
            string reg = comboBox_ready_regs.SelectedItem.ToString();
            decimal id = -1;
            if (CheckIsRegNameCorrect(ref reg, ref id))
            {
                comboBox_ready_regs.Items.Remove(reg);
                RecordToInsert rec = new RecordToInsert();
                rec.Reestr = new Reg();
                rec.Reestr.Id = id;
                LetterManager.ChangeRegForGenerate(rec, Operation.Remove);
                RefreshToolStripPin();
                comboBox_regs.Items.Add(reg);
                comboBox_ready_regs.Text = "";
                button_remove_reg.Enabled = false;
            }
        }

        private void comboBox_template_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CheckControlsEnables();
                decimal templateId = Convert.ToDecimal(comboBox_template.Text);
                string templateName = "";
                if (LetterManager.IsTemplateExists(templateId, ref templateName))
                {
                    toolStrip_template.Text = templateName;
                }
                else
                {
                    comboBox_template.Text = "";
                    toolStrip_template.Text = "";
                }
                comboBox_creditors.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception from WinFormsFace.Form_main.comboBox_template_SelectedIndexChanged " + ex.Message);   
            }
        }

        private void button_load_file_Click(object sender, EventArgs e)
        {
            comboBox_creditors.Enabled = false;
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
                        if (!CheckReadyToLoadData())
                        {
                            MessageBox.Show("Не указан тип адреса или шаблон письма.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        string template = comboBox_template.SelectedItem.ToString();
                        AdressType adrtype = (AdressType)comboBox_adr.SelectedIndex;
                        List<RecordToInsert> recList = new List<RecordToInsert>();

                        using (myStream)
                        {
                            var lines = File.ReadLines(ofd.FileName);
                            foreach (var line in lines)
                            {
                                RecordToInsert rec = new RecordToInsert { DealId = line, TemplateId = template, AdrType = adrtype };
                                recList.Add(rec);
                            }
                        }
                        LetterManager.AddPinFromFile(recList);
                    }
                    else
                    {
                        comboBox_creditors.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удается прочитать файл. " + ex.Message);
                }
            }               
        }

        private void LetterManager_FileLoadCompleted(bool obj)
        {
            RefreshToolStripPin();
        }

        private bool CheckReadyToLoadData()
        {
            if (comboBox_adr.Text.Length < 1 || comboBox_template.Text.Length < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void textBox_summa_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox_summa.Text, "[^0-9]"))
            {
                textBox_summa.Text = textBox_summa.Text.Remove(textBox_summa.Text.Length - 1);
            }
        }
    }
}
