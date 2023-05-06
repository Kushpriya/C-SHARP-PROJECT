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
using static System.Net.Mime.MediaTypeNames;

namespace project3
{
    public partial class menu : Form
    {
        SqlConnection conn = new SqlConnection(
        @"data source= .\SQLEXPRESS;
        initial catalog = project3;
        user id = sa;
        password= kist@123");//this password may be different
        public menu()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            studentForm co=new studentForm();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(global.isGuestModeOn)
            {
                try
                {
                    conn.Open();
                    global.ID = 42;
                    int currenID = global.ID;
                    for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                    {

                        int id = (Convert.ToInt32(dataGridView2.Rows[i].Cells["ID"].Value));
                        string fname = Convert.ToString(dataGridView2.Rows[i].Cells["Food_Name"].Value);
                        string category = Convert.ToString(dataGridView2.Rows[i].Cells["Category"].Value);
                        int quan = Convert.ToInt32(dataGridView2.Rows[i].Cells["Quantity"].Value);
                        int price = Convert.ToInt32(dataGridView2.Rows[i].Cells["Price"].Value);

                        //Query
                        string x = "insert into orderedBill (ID,Name,Category,Quantity,Price) values('" + id + "','" + fname + "',' " + category + " ','" + quan + "','" + price + "')";
                        string y = "insert into Sales (PersonID,foodID,Category,Quantity,Price) values('" + currenID + "','" + id + "',' " + category + " ','" + quan + "','" + price + "')";
                        SqlCommand cmd = new SqlCommand(x, conn);
                        SqlCommand sqlCommand = new SqlCommand(y, conn);
                        cmd.ExecuteNonQuery();
                        sqlCommand.ExecuteNonQuery();

                    }

                    bill co = new bill(dataGridView2);
                    this.Hide();
                    co.ShowDialog();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    conn.Open();
                    int currenID = global.ID;
                    for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                    {

                        int id = (Convert.ToInt32(dataGridView2.Rows[i].Cells["ID"].Value));
                        string fname = Convert.ToString(dataGridView2.Rows[i].Cells["Food_Name"].Value);
                        string category = Convert.ToString(dataGridView2.Rows[i].Cells["Category"].Value);
                        int quan = Convert.ToInt32(dataGridView2.Rows[i].Cells["Quantity"].Value);
                        int price = Convert.ToInt32(dataGridView2.Rows[i].Cells["Price"].Value);

                        //Query
                        string x = "insert into orderedBill (ID,Name,Category,Quantity,Price) values('" + id + "','" + fname + "',' " + category + " ','" + quan + "','" + price + "')";
                        string y = "insert into Sales (PersonID,foodID,Category,Quantity,Price) values('" + currenID + "','" + id + "',' " + category + " ','" + quan + "','" + price + "')";
                        SqlCommand cmd = new SqlCommand(x, conn);
                        SqlCommand sqlCommand = new SqlCommand(y, conn);
                        cmd.ExecuteNonQuery();
                        sqlCommand.ExecuteNonQuery();

                    }

                    bill co = new bill(dataGridView2);
                    this.Hide();
                    co.ShowDialog();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                if (comboBox1.Text == "veg")
                {
                    string query = "select * from veg";
                    SqlCommand sqlCommand = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
                else if (comboBox1.Text == "non-veg")
                {
                    string query = "select * from non_veg";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    sqlDataAdapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
                else if (comboBox1.Text == "drinks")
                {
                    string query = "select * from drinks";
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
            catch (Exception ex)
            {
                MessageBox.Show("Failed to login to the database");
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                int price = Convert.ToInt32(textBox3.Text) * Convert.ToInt32(numericUpDown1.Value);
                this.dataGridView2.Rows.Add(textBox1.Text, textBox2.Text, comboBox1.Text, numericUpDown1.Value, Convert.ToString(price));
                global.Tprice += price;
                numericUpDown1.Value = 1;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please click on the menu first");
            }
        }

        private void menu_Load(object sender, EventArgs e)
        {
            
           
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
