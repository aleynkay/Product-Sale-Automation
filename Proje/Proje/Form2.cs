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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        ProductSalesAutomationEntities db = new ProductSalesAutomationEntities();
        void List()
        {
            dataGridView1.DataSource = db.SalesList().ToList();

            comboBoxName.DisplayMember = "ProductName";
            comboBoxName.ValueMember = "ProductID";
            comboBoxName.DataSource = db.Products.Where(x => x.Situation == true && x.Stock > 0).ToList();

            var Customer = db.Customers.Where(x => x.Situation == true).Select(x => new
            {
                x.CustomerId,
                nameSurname = x.Name + " " + x.Surname
            }).ToList();

            comboBoxcustomer.DisplayMember = "nameSurname";
            comboBoxcustomer.ValueMember = "CustomerId";
            comboBoxcustomer.DataSource = Customer.ToList();

            textID.Text = "";
            maskedtxtunitp.Text = "";
            maskedtexttotalp.Text = "";
            numpiece.Value = 0;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            List();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        Products p;
        Sales s;
        private void button2_Click(object sender, EventArgs e)
        {
            s = new Sales();
            s.Product = int.Parse(comboBoxName.SelectedValue.ToString());
            s.Customer = int.Parse(comboBoxcustomer.SelectedValue.ToString());
            s.Piece = int.Parse(numpiece.Value.ToString());
            s.Date = DateTime.Today;

            p = db.Products.Find(s.Product);

            if (p.Stock < s.Piece)
            {
                MessageBox.Show("We do not have the product stock in the quantity you want:(((");
            }
            else
            {
                s.TotalPrice = s.Piece * p.SalePrice;
                maskedtxtunitp.Text = p.SalePrice.ToString();
                maskedtexttotalp.Text = s.TotalPrice.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(maskedtexttotalp.Text == "")
            {
                MessageBox.Show("Please calculate first!!!");
            }
            else
            {
                p.Stock = p.Stock - s.Piece;
                db.Sales.Add(s);
                db.SaveChanges();
                MessageBox.Show("Sale was made:)))");
            }
            List();
        }
    }
}
