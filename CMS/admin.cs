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

namespace project3
{
    public partial class admin : Form
    {
        SqlConnection conn = new SqlConnection(
      @"data source= .\SQLEXPRESS;
        initial catalog = project3;
        user id = sa;
        password= kist@123");//this password may be different
        public admin()
        {
            InitializeComponent();
        }
        public void Email(string msg)
        {
            try
            {
                string userEmail = textBox1.Text;
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("youremail");//youremail->write your email address here
                message.To.Add(new MailAddress(userEmail));
                message.Subject = "Password recovery";
                message.IsBodyHtml = true; //to make message body as html
                message.Body = msg;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("youremail", "yourpassword");//yourpassword->write your app password here
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured while sending email!!!");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string email = "";
            string password = "";
            try
            {
                conn.Open();

                string selectQuery = "SELECT email FROM admin WHERE id = 1";
                string passwordQuery = "SELECT password FROM admin WHERE id = 1";

                SqlCommand cmd = new SqlCommand(selectQuery, conn);
                SqlCommand cmd1 = new SqlCommand(passwordQuery, conn);

                SqlDataReader reader1 = cmd.ExecuteReader();
                if (reader1.Read())
                {
                    email = reader1.GetValue(0).ToString();
                }
                reader1.Close();

                SqlDataReader reader2 = cmd1.ExecuteReader();
                if (reader2.Read())
                {
                    password = reader2.GetValue(0).ToString();
                }
                reader2.Close();

                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to login to the database");
            }

            if (email == textBox1.Text && password == textBox2.Text)
            {
                adminMenu co = new adminMenu();
                this.Close();
                this.Hide();
                co.ShowDialog();
                // The entered email and password match the data from the database.
                // Continue with the rest of the code...
            }
            else
            {
                MessageBox.Show("The email or password is incorrect. Please try again.");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            home co = new home();
            this.Hide();
            co.ShowDialog();
        }

        private void admin_Load(object sender, EventArgs e)
        {

        }
        private long GenerateRandom()
        {
            Random random = new Random();
            long no = random.Next(1000, 10000);
            return no;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            long pass = GenerateRandom();
            string pas = pass.ToString();
            try
            {
                conn.Open();
                {
                    string id = textBox2.Text;
                    string query = " update admin set password= '" + pass + "' where id = 1";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                Email(pas);
                MessageBox.Show("new password has been sent to your email");
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured while sending email!!!");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            
        }
    }
}
