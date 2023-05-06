using iTextSharp.text.pdf;
using iTextSharp.text;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace project3
{
    public partial class updateMenu : Form
    {
        SqlConnection conn = new SqlConnection(
       @"data source= .\SQLEXPRESS;
        initial catalog = project3;
        user id = sa;
        password= kist@123");//this password may be different
        public updateMenu()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            adminMenu co = new adminMenu();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                {
                    int id = Int32.Parse(textBox1.Text);
                    int price = Int32.Parse(textBox3.Text);
                    string newName = textBox2.Text;
                    string type = comboBox1.Text;
                    int category = global.foodCategory(type);

                    string query = " update menu set Name= '" + newName + "', Price= " + price + " where id = " + id + " and category = " + category + "";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Menu has been updated");
                }
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occured"+ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                {
                    int price = Int32.Parse(textBox3.Text);
                    string newName = textBox2.Text;
                    string type = comboBox1.Text;
                    int category = global.foodCategory(type);

                    string query = " insert into menu (Name,Category,Price) values ('" + newName + "'," + category + "," + price + ")";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("new menu has been added");
                }
                conn.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("Failed to login to the database");
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                {
                    int id = Int32.Parse(textBox1.Text);
                    
                    string query = " delete from menu where id=" + id + "";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("This item has been deleted from the menu");
                }
                conn.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("Please enter a valid ID");
            }
            
        }

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow data = dataGridView1.CurrentRow;
            if (data.Cells["id"] != null && data.Cells["name"] != null && data.Cells["price"] != null)
            {
                string id = data.Cells["id"].Value.ToString();
                string name = data.Cells["name"].Value.ToString();
                string price = data.Cells["price"].Value.ToString();

                textBox1.Text = id;
                textBox2.Text = name;
                textBox3.Text = price;
            }
        }

        private void updateMenu_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                conn.Open();
                if (comboBox1.Text == "veg")
                {
                    string query = "select * from menu where Category = 1";
                    SqlCommand sqlCommand = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
                else if (comboBox1.Text == "non-veg")
                {
                    string query = "select * from menu where Category = 2";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    sqlDataAdapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
                else if (comboBox1.Text == "drinks")
                {
                    string query = "select * from menu where Category = 3";
                    SqlCommand sql = new SqlCommand(query, conn);
                    SqlDataAdapter sqlData = new SqlDataAdapter(sql);
                    DataTable table = new DataTable();
                    sqlData.Fill(table);
                    dataGridView1.DataSource = table;

                }
                else
                {
                    MessageBox.Show("No category selected or no such category exist");
                }
                conn.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("Failed to login to the database");
            }
        }      
    }
}
    

