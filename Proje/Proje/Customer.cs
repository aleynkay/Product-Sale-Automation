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
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }

        ProductSalesAutomationEntities db = new ProductSalesAutomationEntities();

        void List()
        {
            var customers = from x in db.Customers.Where(y => y.Situation == true)
                           select new
                           {
                               x.CustomerId,
                               x.Name,
                               x.Surname,
                               x.Address,
                               x.Tel
                           };
            dataGridView1.DataSource = customers.ToList();
            textID.Text = "";
            textNme.Text = "";
            textSurnme.Text = "";
            textaddress.Text = "";
            maskedtxttelno.Text = "";
            textsearch.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            List();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            List();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textNme.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textSurnme.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textaddress.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            maskedtxttelno.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void buttonsave_Click(object sender, EventArgs e)
        {
            if (textNme.Text == "" || textSurnme.Text == "" || textaddress.Text == "" || maskedtxttelno.Text == "")
            {
                MessageBox.Show("Please enter all required fields!!!!!!!");
            }
            else
            {
                var x = new Customers();
                x.Name = textNme.Text;
                x.Surname = textSurnme.Text;
                x.Address = textaddress.Text;
                x.Tel = maskedtxttelno.Text;
                x.Situation = true;
                db.Customers.Add(x);
                db.SaveChanges();
                MessageBox.Show("Successful registration done :)))))");
            }
            List();
        }

        private void buttondelete_Click(object sender, EventArgs e)
        {
            if (textID.Text == "")
            {
                MessageBox.Show("Please select the customer which you want delete!!!!!");
            }
            else
            {
                int id = int.Parse(textID.Text);
                var x = db.Customers.Find(id);
                x.Situation = false;
                db.SaveChanges();
                MessageBox.Show("Successful customer delete :)))))");
            }
            List();
        }

        private void buttonupdate_Click(object sender, EventArgs e)
        {
            if (textNme.Text == "" || textSurnme.Text == "" || textaddress.Text == "" || maskedtxttelno.Text == "")
            {
                MessageBox.Show("Please select the customer which you want update and enter all required fields!!!!!");
            }
            else
            {
                int id = int.Parse(textID.Text);
                var x = db.Customers.Find(id);
                x.Name = textNme.Text;
                x.Surname = textSurnme.Text;
                x.Address = textaddress.Text;
                x.Tel = maskedtxttelno.Text;
                x.Situation = true;
                db.SaveChanges();
                MessageBox.Show("Successful update :)))))");
            }
            List();
        }

        private void buttonsearch_Click(object sender, EventArgs e)
        {
            if (textsearch.Text == "")
            {
                MessageBox.Show("please enter the customer name which you want to search");
            }
            else
            {
                var customer = from x in db.Customers.Where(y => y.Situation == true && y.Name == textsearch.Text)
                               select new
                               {
                                   x.CustomerId,
                                   x.Name,
                                   x.Surname,
                                   x.Address,
                                   x.Tel
                               };
                dataGridView1.DataSource = customer.ToList();
            }
        }
    }
}
