using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hardware_Shop
{
    public partial class Billing : Form
    {
        private int sum = 0;
        public Billing()
        {
            InitializeComponent();     
        }
        public void sendDatatoBilling(DataRow row)
        {
            string name = row["Name"].ToString();
            decimal price = Convert.ToDecimal(row["Price"]);
            string used = row["Used"].ToString();
            sum = (int)(sum + price);
            textBox1.Text = sum.ToString();
            dataGridView1.Rows.Add(name,price.ToString(),used);
        }
       
        private void Billing_Load(object sender, EventArgs e)
        {

        }
        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        private void label4_Click_1(object sender, EventArgs e)
        {
            Billing billing = new Billing();
            billing.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Item item = new Item();
            item.Show();         
            this.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {
        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
