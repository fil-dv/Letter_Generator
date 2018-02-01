using MyLetterManager;
using MyLetterManager.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsFace
{
    public partial class Form_main : Form
    {
        public Form_main()
        {
            InitializeComponent();
            LetterManager.CreateConnect();
            ResetControls();
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

        void ResetControls()
        {
            LetterManager.ResetData();
            ClearCreditorsCombo();
            ClearRegsCombo();
            toolStripLabel_pins.Text = "0";
            button_add_reg.Enabled = false;
            InitCreditorsCombo();
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
    }
}
