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

        static public event Action<bool> RegisrersSelected;
        Button _button_add_regs = new Button();
        List<CheckBox> _checkBoxeslist = new List<CheckBox>();

        public Form_regs()
        {            
            InitializeComponent();
            SetUpForm();
            if (Mediator.CurrentCreditor != null)
            {
                this.Text = Mediator.CurrentCreditor;
            }
            // InitListView();  
            InitButton();            
            FeellCheckBoxList();
            InitRegs();
        }

        private void InitButton()
        {
            this.Controls.Add(_button_add_regs);
            _button_add_regs.Text = "Ok";
            _button_add_regs.Enabled = false;
        }

        private void FeellCheckBoxList()
        {            
            try
            {                
                for (int i = 0; i < Mediator.RegList.Count; i++)
                {
                    CheckBox cb = new CheckBox();
                    cb.Left = 10;
                    cb.Top = 10 + (i * 24);
                    cb.Width = this.Width - 95;
                    _checkBoxeslist.Add(cb);
                }
                CreateHandlers();                             
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception from WinFormsFace.OtherForms.Form_regs.InitCheckBoxList() " + ex.Message);
            }
        }

        private void SetUpForm()
        {     
            this.Height = Mediator.RegList.Count * 24;

            if (this.Height >= 500)
            {
                this.AutoScroll = true;
                this.VerticalScroll.Enabled = true;
                this.VerticalScroll.Visible = true;                               
            }
            _button_add_regs.Width = 70;
            _button_add_regs.Left = this.Width - 100;
            _button_add_regs.Top = (int)(this.Height * 0.1); ;
            _button_add_regs.Height = (int)(this.Height * 0.9);   
        }

        void InitRegs()
        {
            List<Reg> regList = Mediator.RegList;
            try
            {
                for (int i = 0; i < regList.Count; i++)
                {
                    _checkBoxeslist[i].Text = "ID = " + regList[i].Id + ", " + regList[i].Name;
                    _checkBoxeslist[i].Name = regList[i].Id.ToString();
                    this.Controls.Add(_checkBoxeslist[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception from WinFormsFace.Form_main.InitConditions() " + ex.Message);
            }
        }

        private void CreateHandlers()
        {
            try
            {
                foreach (var item in _checkBoxeslist)
                {
                    item.CheckStateChanged += Item_CheckStateChanged; 
                }
                _button_add_regs.Click += _button_add_regs_Click;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception from WinFormsFace.OtherForms.Form_regs.CreateHandlers() " + ex.Message);
            }
        }        
        

        private void _button_add_regs_Click(object sender, EventArgs e)
        {
            List<CheckBox> list = _checkBoxeslist.Where(c => c.Checked == true).ToList();
            if (list.Count > 0)
            {
                List<Reg> regList = new List<Reg>();
                foreach (var item in list)
                {
                    Reg reg = new Reg { Id = Convert.ToDecimal(item.Name) };
                    regList.Add(reg);                    
                }
                Mediator.SelectedRegList = regList;
                if (RegisrersSelected != null)
                {
                    RegisrersSelected(true);
                }
            }
            this.Close();           
        }

        private void Item_CheckStateChanged(object sender, EventArgs e)
        {
            List<CheckBox> list = _checkBoxeslist.Where(c => c.Checked == true).ToList();
            if (list.Count > 0)
            {
                _button_add_regs.Enabled = true;
            }
            else
            {
                _button_add_regs.Enabled = false;
            }
        }

        //private void InitListView()
        //{
        //    listBox_regs.Location = new Point(10, 20);
        //    listBox_regs.Width = this.Width - 36;

        //    decimal creditorId = LetterManager.GetCreditorIdByTrimedAlias(Mediator.CurrentCreditor);
        //    List<Reg> regList = LetterManager.GetRegListByCreditorId(creditorId);

        //    listBox_regs.Height = regList.Count * 13 + 20;
        //    if (listBox_regs.Height > 450)
        //    {
        //       // listBox_regs.scr = true;
        //    }
        //    this.Height = listBox_regs.Height + 150;
        //    button_add_regs.Location = new Point(270, listBox_regs.Height + 50);

        //    foreach (var item in regList)
        //    {
        //        listBox_regs.Items.Add(item.Name + ", ID - " + item.Id);
        //    }
        //}

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
