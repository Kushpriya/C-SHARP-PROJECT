using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace project3
{
    public partial class studentLogin : Form
    {
        SqlConnection conn = new SqlConnection(
       @"data source= .\SQLEXPRESS;
        initial catalog = project3;
        user id = sa;
        password= kist@123");//this password may be different
        public studentLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            home co = new home();
            this.Hide();
            co.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            global.ID = Convert.ToInt32(textBox2.Text);
            string email = "";
            string password = "";
            string id = textBox2.Text;
            string doesAccountExist = "";

            try
            {
                conn.Open();

                string selectQuery = "SELECT email FROM users WHERE id = ' " + id + " '";
                string passwordQuery = "SELECT password FROM users WHERE id = ' " + id + " '";
                string accountQuery = "SELECT AccountDeleted FROM users WHERE id = ' " + id + " '";

                SqlCommand cmd = new SqlCommand(selectQuery, conn);
                SqlCommand cmd1 = new SqlCommand(passwordQuery, conn);
                SqlCommand cmd2 = new SqlCommand(accountQuery, conn);

                SqlDataReader reader1 = cmd.ExecuteReader();
                if (reader1.Read())
                {
                    email = reader1.GetValue(0).ToString();
                }
                reader1.Close();
                global.loginEmail = email;
                SqlDataReader reader2 = cmd1.ExecuteReader();
                if (reader2.Read())
                {
                    password = reader2.GetValue(0).ToString();
                }
                reader2.Close();
                SqlDataReader reader3 = cmd2.ExecuteReader();
                if (reader3.Read())
                {
                    doesAccountExist = reader3.GetValue(0).ToString();

                }
                reader3.Close();

                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to login to the database");
            }

            if (password == textBox3.Text && doesAccountExist == "False")
            {
                studentForm co = new studentForm();
                this.Hide();
                co.ShowDialog();
                // The entered email and password match the data from the database.
                // Continue with the rest of the code...
            }
            else if (doesAccountExist == "True")
            {
                MessageBox.Show("This account has been deleted by admin");
            }
            else
            {
                MessageBox.Show("The email or password is incorrect. Please try again.");
            }

        }
        private long GenerateRandom()
        {
            Random random = new Random();
            long no = random.Next(1000, 10000);
            return no;
        }
        public void Email(string msg)
        {
            string id = textBox2.Text;
            string email = "";
            conn.Open();
            string selectQuery = "SELECT email FROM users WHERE id = ' " + id + " '";
            SqlCommand cmd = new SqlCommand(selectQuery, conn);
            SqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.Read())
            {
                email = reader1.GetValue(0).ToString();
            }
            reader1.Close();
            conn.Close();
            try
            {
                string userEmail = email;
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("sudarshankapalisk@gmail.com");//youremail->write your email address here
                message.To.Add(new MailAddress(email));
                message.Subject = "Password recovery";
                message.IsBodyHtml = true; //to make message body as html
                message.Body = msg;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("sudarshankapalisk@gmail.com", "");//yourpassword->write your app password here
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured while sending email!!!");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            long pass = GenerateRandom();
            string pas = pass.ToString();
            conn.Open();
            {
                string id = textBox2.Text;
                string query = " update users set password= '" + pass + "' where id = " + id + "";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            conn.Close();

            try
            {
                
                Email(pas);
                MessageBox.Show("new password has been sent to your email");
            }
           catch (Exception)
            {
                MessageBox.Show("An error occured while sending email!!!");
            }
        }

        private void studentLogin_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
