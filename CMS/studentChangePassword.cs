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
    public partial class studentChangePassword : Form
    {
        SqlConnection conn = new SqlConnection(
        @"data source= .\SQLEXPRESS;
        initial catalog = project3;
        user id = sa;
        password= kist@123");//this password may be different
        public studentChangePassword()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            studentForm studentForm = new studentForm();
            this.Hide();
            this.Close();
            studentForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                string password = "";
              
                string oldpass = textBox1.Text;



                conn.Open();

                string passwordQuery = "SELECT password FROM users WHERE id = "+global.ID;

                SqlCommand cmd1 = new SqlCommand(passwordQuery, conn);


                SqlDataReader reader2 = cmd1.ExecuteReader();
                if (reader2.Read())
                {
                    password = reader2.GetValue(0).ToString();
                }
                reader2.Close();

                conn.Close();
                if(password == oldpass && textBox2.Text == textBox3.Text)
                {
                    conn.Open();
                    {
                        string newPassword = textBox2.Text;
                        int newID = global.ID;
                       
                        string query = "update users set password ='" + newPassword + "' where id = ' " + newID + " '";
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
            studentForm co = new studentForm();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }

        private void studentChangePassword_Load(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.Location = new Point(100, 300);
        }
    }
}
