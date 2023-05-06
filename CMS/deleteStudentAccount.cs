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
    public partial class deleteStudentAccount : Form
    {
        SqlConnection conn = new SqlConnection(
       @"data source= .\SQLEXPRESS;
        initial catalog = project3;
        user id = sa;
        password= kist@123");//this password may be different
        public deleteStudentAccount()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "select ID,name from users where AccountDeleted = 0";
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Failed to login to the database");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow data = dataGridView1.CurrentRow;
                if (data.Cells["id"] != null)
                {

                    string id = data.Cells["id"].Value.ToString();
                    conn.Open();
                    string query1 = "update users set AccountDeleted = 1 where id=" + id + "";
                    SqlCommand cmd1 = new SqlCommand(query1, conn);
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("Account has been deleted");
                    conn.Close();

                }
                else
                {
                    MessageBox.Show("No valid data selected");
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Failed to delete the account" + ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            adminMenu co = new adminMenu();
            this.Close();
            this.Hide();
            co.ShowDialog();
        }

        private void deleteStudentAccount_Load(object sender, EventArgs e)
        {

        }
    }
}
