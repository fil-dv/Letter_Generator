using MyLetterManager;
using MyLetterManager.Mediator;
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

namespace WinFormsFace.OtherForms
{
    public partial class Form_regs : Form
    {
        public Form_regs()
        {
            InitializeComponent();
            if (Mediator.CurrentCreditor != null)
            {
                this.Text = Mediator.CurrentCreditor;
            }
            InitListView();            
        }

        private void InitListView()
        {
            listBox_regs.Location = new Point(10, 20);
            listBox_regs.Width = this.Width - 36;

            decimal creditorId = LetterManager.GetCreditorIdByTrimedAlias(Mediator.CurrentCreditor);
            List<Reg> regList = LetterManager.GetRegListByCreditorId(creditorId);

            listBox_regs.Height = regList.Count * 13 + 20;
            if (listBox_regs.Height > 450)
            {
               // listBox_regs.scr = true;
            }
            this.Height = listBox_regs.Height + 150;
            button_add_regs.Location = new Point(270, listBox_regs.Height + 50);

            foreach (var item in regList)
            {
                listBox_regs.Items.Add(item.Name + ", ID - " + item.Id);
            }
        }

        //private void button_add_regs_Click(object sender, EventArgs e)
        //{
        //    foreach (var item in listBox_regs.SelectedItems)
        //    {
        //        decimal id = -1;
        //        string regName = item.ToString();
        //        if (CheckIsRegNameCorrect(ref regName, ref id))
        //        {
        //            if (id != -1)
        //            {
        //                LetterManager.AddRegForGenerate(id);
        //                int countForGenerate = LetterManager.AddRegToGenerate();
        //                //toolStripLabel_pins.Text = countForGenerate.ToString();
        //                //comboBox_ready_regs.Items.Add(regName);
        //                //comboBox_regs.Items.Remove(regName);
        //                //comboBox_regs.Text = "";
        //                //button_add_reg.Enabled = false;
        //            }
        //        }
        //    }            
        //}

        //static bool CheckIsRegNameCorrect(ref string regName, ref decimal id)
        //{
        //    bool isExist = false;

        //    string[] arr = regName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //    string lastItem = "";
        //    if (arr.Count() > 0)
        //    {
        //        lastItem = arr[arr.Count() - 1];
        //        id = Convert.ToUInt32(lastItem);
        //    }
        //    if (IsRegExist(id))
        //    {
        //        isExist = true;
        //    }
        //    return isExist;
        //}
    }
}
