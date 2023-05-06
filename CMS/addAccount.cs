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
    public partial class addAccount : Form
    {
        SqlConnection conn = new SqlConnection(
       @"data source= .\SQLEXPRESS;
        initial catalog = project3;
        Integrated Security=true");//this password may be different
        public addAccount()
        {
            InitializeComponent();
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        public static void Email(string htmlString, string user, int userID)
        {


            string receiver = user;
            try
            {

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("romanshrestha686@gmail.com");
                message.To.Add(new MailAddress(receiver));
                message.Subject = "Your account login password";
                message.IsBodyHtml = true; //to make message body as html
                message.Body = "Your account has been created ypur ID is " + userID + " and your login password is " + htmlString + " you can change the password after you are logged in";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("romanshrestha686@gmail.com", "qkhzullxpmaesacd");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception)
            {
                MessageBox.Show(receiver);
                MessageBox.Show("error while sending mail");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            long pass = GenerateRandom();
            string pas = pass.ToString();
            int state = 0;
            try
            {
                conn.Open();
                {
                    string query = " insert into users (Name,Password,Email,AccountDeleted) values ('" + textBox2.Text + "','" + pas + "','" + textBox4.Text + "','" + state + "')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();

                    string getid = "Select top(1) id from users order by id desc";
                    cmd = new SqlCommand(getid, conn);
                    int id = Convert.ToInt32(cmd.ExecuteScalar());
                    MessageBox.Show("account has been created.\n ID: " + id);

                    // password sending
                    string query1 = "Select top(1) Password from users order by id desc";
                    SqlCommand cmd1 = new SqlCommand(query1, conn);
                    cmd1.ExecuteNonQuery();
                    int generatedPassword = Convert.ToInt32(cmd1.ExecuteScalar());
                    //int yo = GetId();



                    string newpass = Convert.ToString(generatedPassword);
                    MessageBox.Show(newpass);
                    try
                    {
                        string getEmail = textBox4.Text;
                        Email(newpass, getEmail, id);
                        MessageBox.Show("Your account has been created and your password and ID has been sent to your gmail account");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Failed to send password to your email. Please check your email address and try again!");
                    }

                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to login to the database");
            }

            adminMenu co = new adminMenu();
            this.Hide();
            co.ShowDialog();
        }



        private long GenerateRandom()
        {
            Random random = new Random();
            long no = random.Next(1000, 10000);
            return no;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            adminMenu co = new adminMenu();
            this.Hide();
            co.ShowDialog();
        }

        private void addAccount_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
