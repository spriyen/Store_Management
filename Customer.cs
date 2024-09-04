using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hardware_Shop
{
    public partial class Customer : Form
    {
        private Billing billing;
        public Customer()
        {
            InitializeComponent();
            billing = new Billing();
        }
        private void Customer_Load(object sender, EventArgs e)
        {

        }
        private void label6_Click_1(object sender, EventArgs e)
        {
            Item item = new Item();
            item.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            billing.Show();
            this.Hide();       
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

        private void button1_Click(object sender, EventArgs e)
        {
            String sqlconn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HardwareStore;Integrated Security=True";
            try
            {
                String sqlQuerry = "Select No,Name,Price,Used,Image from Item where Category ='" + comboBox1.Text + "' and Used ='" + comboBox2.Text + "'and Price <= '" + textBox3.Text + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sqlQuerry, sqlconn);
                DataSet set = new DataSet();
                adapter.Fill(set, "Product");
                dataGridView1.DataSource = set.Tables["Product"];
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please enter the required values\n" + ex);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value != null && cell.Value is byte[])
                {
                    byte[] imageData = (byte[])cell.Value;

                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        Image image = Image.FromStream(ms);
                        pictureBox8.Image = image;
                    }
                }
                else if(cell.Value != null && cell.Value is int && e.ColumnIndex == 0 )
                {
                    int productID = (int)(cell.Value);
                    MessageBox.Show("Product Id "+productID);
                    dataGridView1.ReadOnly = true;

                    string ConString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HardwareStore;Integrated Security=True";

                    try
                    {
                        string Query = "SELECT Name, Price, Used FROM Item WHERE No = " + productID;
                        SqlDataAdapter adapter = new SqlDataAdapter(Query, ConString);
                        DataSet set = new DataSet();
                        adapter.Fill(set, "Ite");

                        DataTable existingTable = ((DataTable)dataGridView2.DataSource);
                        if (existingTable != null)
                        {
                            set.Tables["Ite"].Merge(existingTable);
                        }

                        dataGridView2.DataSource = set.Tables["Ite"];

                        // Check if rows exist before accessing
                        if (set.Tables["Ite"].Rows.Count > 0)
                        {
                            DataRow row = set.Tables["Ite"].Rows[0];
                            billing.sendDatatoBilling(row);
                        }
                        else
                        {
                            MessageBox.Show("No data found for the specified product ID.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error occurred: " + ex.Message);
                    }

                }
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
