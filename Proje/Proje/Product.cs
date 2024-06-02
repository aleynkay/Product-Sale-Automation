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
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
        }
        
        ProductSalesAutomationEntities db = new ProductSalesAutomationEntities();
        
        void List()
        {
            var products = from x in db.Products.Where(y=>y.Situation==true)
                        select new
                        {
                            x.ProductID,
                            x.ProductName,
                            x.Stock,
                            x.PurchasePrice,
                            x.SalePrice
                           };
            dataGridView1.DataSource = products.ToList();
            textID.Text = "";
            textNme.Text = "";
            numstock.Value = 0;
            maskedtxtpurchase.Text = "";
            maskedtxtsales.Text = "";
            textsearch.Text = "";
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Product_Load(object sender, EventArgs e)
        {
            List();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List();
        }

        private void buttonsave_Click(object sender, EventArgs e)
        {
            if (textNme.Text == "" || maskedtxtpurchase.Text == "" || maskedtxtsales.Text == "")
            {
                MessageBox.Show("Please enter all required fields!!!!!!!");
            }
            else
            {
                var x = new Products();
                x.ProductName = textNme.Text;
                x.Stock = int.Parse(numstock.Value.ToString());
                x.PurchasePrice = int.Parse(maskedtxtpurchase.Text);
                x.SalePrice = int.Parse(maskedtxtsales.Text);
                x.Situation = true;
                db.Products.Add(x);
                db.SaveChanges();
                MessageBox.Show("Successful registration done :)))))");
            }
            List();
        }

        private void buttondelete_Click(object sender, EventArgs e)
        {
            if(textID.Text == "")
            {
                MessageBox.Show("Please select the products which you want delete!!!!!");
            }
            else
            {
                int id = int.Parse(textID.Text);
                var x = db.Products.Find(id);
                x.Situation = false;
                db.SaveChanges();
                MessageBox.Show("Successful product delete :)))))");
            }
            List();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textNme.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            numstock.Value = int.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString());
            maskedtxtpurchase.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            maskedtxtsales.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void buttonupdate_Click(object sender, EventArgs e)
        {
            if (textNme.Text == "" || maskedtxtpurchase.Text == "" || maskedtxtsales.Text == "")
            {
                MessageBox.Show("Please select the products which you want update and enter all required fields!!!!!");
            }
            else
            {
                int id = int.Parse(textID.Text);
                var x = db.Products.Find(id);
                x.ProductName = textNme.Text;
                x.Stock = int.Parse(numstock.Value.ToString());
                x.PurchasePrice = int.Parse(maskedtxtpurchase.Text);
                x.SalePrice = int.Parse(maskedtxtsales.Text);
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
                MessageBox.Show("please enter the product name which you want to search");
            }
            else
            {
                var products = from x in db.Products.Where(y => y.Situation == true && y.ProductName == textsearch.Text)
                               select new
                               {
                                   x.ProductID,
                                   x.ProductName,
                                   x.Stock,
                                   x.PurchasePrice,
                                   x.SalePrice
                               };
                dataGridView1.DataSource = products.ToList();
            }
        }
    }
}
