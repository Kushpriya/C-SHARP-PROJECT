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

namespace project3
{
    public partial class adminChangePassword : Form
    {
        SqlConnection conn = new SqlConnection(
      @"data source= .\SQLEXPRESS;
        initial catalog = project3;
        user id = sa;
        password= kist@123");//this password may be different
        public adminChangePassword()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            adminMenu co = new adminMenu();
            this.Hide();
            co.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                string password = "";

                string oldpass = textBox1.Text;



                conn.Open();

                string passwordQuery = "SELECT password FROM admin WHERE id = 1";

                SqlCommand cmd1 = new SqlCommand(passwordQuery, conn);


                SqlDataReader reader2 = cmd1.ExecuteReader();
                if (reader2.Read())
                {
                    password = reader2.GetValue(0).ToString();
                }
                reader2.Close();

                conn.Close();
                if (password == oldpass && textBox2.Text == textBox3.Text)
                {
                    conn.Open();
                    {
                        string newPassword = textBox2.Text;
                        int newID = global.ID;

                        string query = "update admin set password ='" + newPassword + "' where id = 1";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("your password has been changed");

                    }
                    conn.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to login to the database");
            }
        }

        private void adminChangePassword_Load(object sender, EventArgs e)
        {

        }
    }
}
