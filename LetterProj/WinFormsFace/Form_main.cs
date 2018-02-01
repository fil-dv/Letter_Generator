using MyLetterManager;
using MyLetterManager.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsFace
{
    public partial class Form_main : Form
    {
        List<CheckBox> _checkBoxeslist = new List<CheckBox>();

        public Form_main()
        {
            InitializeComponent();
            LetterManager.CreateConnect();
            FillCheckBoxList();
            ResetControls();            
        }

        void FillCheckBoxList()
        {
            try
            {
                for (int i = 0; i < LetterManager.GetConditionsList().Count; i++)
                {
                    CheckBox cb = new CheckBox();
                    cb.Left = 50;
                    cb.Top = 130 + (i * 24);
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

        void InitRegsCombo()
        {
            decimal creditorId = LetterManager.GetCreditorIdByTrimedAlias(comboBox_creditors.SelectedItem.ToString());
            List<Reg> regList = LetterManager.GetRegListByCreditorId(creditorId);
            foreach (var item in regList)
            {
                comboBox_regs.Items.Add(item.Name + ", ID - " + item.Id);
            }
        }

        private void comboBox_creditors_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitRegsCombo();
        }

        private void Clean_up_ToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            ResetControls();
        }

        void InitAdrCombo()
        {
            comboBox_adr.Items.Clear();
            comboBox_adr.Items.Add("Прописка");
            comboBox_adr.Items.Add("Фактический");
            comboBox_adr.Items.Add("Временной регистрации");
            comboBox_adr.Items.Add("Рабочий");
            comboBox_adr.SelectedIndex = 0;
        }

        void ResetControls()
        {
            this.Height = 221 + (LetterManager.GetConditionsList().Count * 24);
            LetterManager.ResetData();
            ClearCreditorsCombo();
            ClearRegsCombo();
            toolStripLabel_pins.Text = "0";
            button_add_reg.Enabled = false;
            InitAdrCombo();
            InitCreditorsCombo();
            InitConditions();            
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

        private void comboBox_regs_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckButtonAddEnable();
        }

        private void comboBox_regs_TextChanged(object sender, EventArgs e)
        {
            CheckButtonAddEnable();
        }

        private void button_add_reg_Click(object sender, EventArgs e)
        {
            decimal id = -1;
            string str = comboBox_regs.SelectedItem.ToString();
            string[] arr = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string lastItem = "";
            if (arr.Count() > 0)
            {
                lastItem = arr[arr.Count() - 1];
                id = Convert.ToUInt32(lastItem);
            }

            if (LetterManager.IsRegExist(id))
            {
                LetterManager.AddRegForGenerate(id);
                int countForGenerate = LetterManager.CheckDealsCountToGenerate();
                toolStripLabel_pins.Text = countForGenerate.ToString();
            }
        }

        private void comboBox_adr_SelectedIndexChanged(object sender, EventArgs e)
        {
            LetterManager.SetAdressType(comboBox_adr.SelectedItem.ToString());
        }

        private void textBox_template_TextChanged(object sender, EventArgs e)
        {
            string id = textBox_template.Text;
            textBox_template.Text = Regex.Replace(id, "[^0-9 ]", "");
            if (id.Length > 3)
            {
                toolStripLabel_template.Text = LetterManager.GetTemplateName(id);
            }
            else
            {
                toolStripLabel_template.Text = "";
            }
            
        }

    }
}
