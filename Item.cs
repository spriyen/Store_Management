using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hardware_Shop
{
    public partial class Item : Form
    {
        public Item()
        {
            InitializeComponent();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        private void Item_Load(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {
            Item item = new Item();
            item.Show();
            this.Hide(); 
        }
        private void label3_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Billing billing = new Billing();
            billing.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox8.Image = new Bitmap(openFileDialog.FileName);
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox8.BackgroundImage = null;
                pictureBox8.Image = new Bitmap(openFileDialog.FileName);
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            pictureBox8.Image = null;
            pictureBox8.BackgroundImage = Image.FromFile("C:\\Users\\Dell\\source\\repos\\Hardware Shop\\Resources\\iiii.png");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ConString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HardwareStore;Integrated Security=True";

            try
            {
                // Fill DataSet
                string Query = "SELECT * FROM Item";
                SqlDataAdapter adapter = new SqlDataAdapter(Query, ConString);
                DataSet set = new DataSet();
                adapter.Fill(set, "Ite");

                //Adding New Row to DataSet           
                DataRow row = set.Tables["Ite"].NewRow();
                row["Name"] = textBox1.Text;
                row["Category"] = comboBox1.Text;
                row["Price"] = textBox2.Text;
                row["Used"] = comboBox2.Text;

                Image image = pictureBox8.Image;
                if (image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.Save(ms, image.RawFormat);
                        byte[] imageBytes = ms.ToArray(); 
                        row["Image"] = imageBytes;
                    }
                }
                else
                {
                    MessageBox.Show("Please Upload Image !...");
                    row["Image"] = DBNull.Value;
                }

                set.Tables["Ite"].Rows.Add(row);
                dataGridView1.DataSource = set.Tables["Ite"];
               
                // Updating Database Table
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(set, "Ite");

                MessageBox.Show("DataSet Saved to Database Successfully");
            }
            catch (Exception)
            {
                MessageBox.Show("Not Saved in DB");
            }

        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string ConString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HardwareStore;Integrated Security=True";

            try
            {
                string Query = "SELECT * FROM Item";
                SqlDataAdapter adapter = new SqlDataAdapter(Query, ConString);
                DataSet set = new DataSet();
                adapter.Fill(set, "Ite");

                // Delete the last row from DataSet
                int lastRowIndex = set.Tables["Ite"].Rows.Count - 1;
                if (lastRowIndex >= 0)
                {
                    set.Tables["Ite"].Rows[lastRowIndex].Delete();
                }

                // Update in the original database
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(set, "Ite");

                dataGridView1.DataSource = set.Tables["Ite"];

                MessageBox.Show("Last row deleted and DataSet saved to the database successfully");
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurred. Data not deleted.");
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
