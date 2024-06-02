using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textusrname.Text == "" || textpasswd.Text == "")
                MessageBox.Show("Please enter username and password!!!!");
            else
            {
                if (textusrname.Text == "admin" && textpasswd.Text == "12345")
                {
                    Menu frm = new Menu();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("INCORRECT USERNAME OR PASSWORD");
                }
                textusrname.Text = "";
                textpasswd.Text = "";
            }
        }
    }
}
