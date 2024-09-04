using System;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                string sqlcon = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HardwareStore;Integrated Security=True";
                try
                {
                    using (SqlConnection connection = new SqlConnection(sqlcon))
                    {
                        connection.Open();

                        string query = "SELECT * FROM admin WHERE id = @ID";
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        adapter.SelectCommand.Parameters.AddWithValue("@ID", textBox2.Text);

                        DataSet set = new DataSet();
                        adapter.Fill(set, "adm");

                        if (set.Tables["adm"].Rows.Count > 0)
                        {
                            DataRow row = set.Tables["adm"].Rows[0];

                            if (textBox1.Text.Trim() == row["Name"].ToString().Trim())
                            {
                                Item item = new Item();
                                item.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Username Wrong");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Username or ID does not exist");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                    MessageBox.Show("Please enter Username");
                else
                    MessageBox.Show("Please enter Password");
            }

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
