using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project3
{
    public partial class bill : Form
    {
        SqlConnection conn = new SqlConnection(
       @"data source= .\SQLEXPRESS;
        initial catalog = project3;
        user id = sa;
        password= kist@123");//this password may be different
        public bill(DataGridView dataGridView1)
        {
            InitializeComponent();
        }

        public void saveDataToPDF()
        {

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            save.FileName = "DataGridViewExport.pdf";
            //string order = global.loginEmail;

            if (dataGridView2.Rows.Count > 0)
            {
                if (save.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(save.FileName, FileMode.Create))
                    {
                        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();

                        // Add a table to the PDF document
                        PdfPTable pdfTable = new PdfPTable(dataGridView2.ColumnCount);
                        pdfTable.DefaultCell.Padding = 3;
                        pdfTable.WidthPercentage = 100;
                        pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                        // Add the headers from the DataGridView to the PDF table
                        foreach (DataGridViewColumn column in dataGridView2.Columns)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                            cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                            pdfTable.AddCell(cell);
                        }

                        // Add the rows from the DataGridView to the PDF table
                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (cell.Value != null)
                                {
                                    pdfTable.AddCell(cell.Value.ToString());
                                }
                                else
                                {
                                    pdfTable.AddCell("");
                                }
                            }
                        }

                        // Add the PDF table to the PDF document
                        pdfDoc.Add(pdfTable);
                        pdfDoc.Close();
                        stream.Close();
                    }

                    MessageBox.Show("Data exported to PDF successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No data found to export!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void sendPDFMail()
        {
            string order = global.loginEmail;
            // Get the file name from the text box
            string fileName = textBox3.Text + ".pdf";

            // Check if the file exists on the desktop
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + fileName;
            if (!File.Exists(filePath))
            {
                MessageBox.Show("The file " + fileName + " was not found on the desktop.");
                return;
            }

            // Create a new MailMessage object
            MailMessage mail = new MailMessage();

            // Set the sender and recipient email addresses
            mail.From = new MailAddress("sudarshankapalisk@gmail.com");
            mail.To.Add(order);

            // Set the email subject and body
            mail.Subject = "Your Order Detail";
            mail.Body = "Here is a PDF file of your ordered list.";

            // Attach the file to the email
            Attachment attachment = new Attachment(filePath);
            mail.Attachments.Add(attachment);

            // Create a new SmtpClient object
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            // Set the credentials for the email account
            smtp.Credentials = new System.Net.NetworkCredential("romanshrestha686@gmail.com", "qkhzullxpmaesacd");

            try
            {
                // Send the email
                smtp.Send(mail);
                MessageBox.Show("The email was sent successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while sending the email: " + ex.Message);
            }
        }

        public void clearTableAndClose()
        {
            try
            {
                conn.Close();
                conn.Open();
                string query2 = "truncate table orderedBill";
                SqlCommand cmd2 = new SqlCommand(query2, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd2);
                DataTable table1 = new DataTable();
                sqlDataAdapter.Fill(table1);
                dataGridView2.DataSource = table1;

                textBox1.Text = Convert.ToString(global.Tprice);
                conn.Close();
                MessageBox.Show("Thank you for using our application");
                global.Tprice = 0;
                if(global.isGuestModeOn == true)
                {
                    home co = new home();
                    this.Hide();
                    co.ShowDialog();
                }
                else
                {
                    studentForm co = new studentForm();
                    this.Hide();
                    co.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error"+ex);
            }
            
        }

        public void dueAmountCase()
        {
            int orderPrice = Convert.ToInt32(textBox2.Text);
            decimal due = global.Tprice - orderPrice;
            try
            {
                conn.Open();
                {
                    string query = "insert into Due (PersonID,Amount) values('" + global.ID + "','" + due + "')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Due amount is : " + due);

                    saveDataToPDF();

                    //string order = global.loginEmail;

                    sendPDFMail();
                    clearTableAndClose();
                    // insert query should go here
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error recording due amount ");
            }
            conn.Close();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal left;
            int orderPrice = Convert.ToInt32(textBox2.Text);

            if(global.isGuestModeOn == true)
            {
                if(global.Tprice <= orderPrice)
                {
                    left = orderPrice - global.Tprice;
                    MessageBox.Show("your return amount is " + left);
                    clearTableAndClose();
                    global.isGuestModeOn = false;

                }
                else
                {
                    MessageBox.Show("Liability service is not available in Guest Mode");
                }
            }

            else
            {
                if (global.Tprice <= orderPrice)
                {
                    left = orderPrice - global.Tprice;
                    MessageBox.Show("your return amount is " + left);
                    saveDataToPDF();
                    sendPDFMail();
                    clearTableAndClose();

                }
                else
                {
                    dueAmountCase();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bill_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "select * from orderedBill";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                sqlDataAdapter.Fill(table);
                dataGridView2.DataSource = table;

                textBox1.Text = Convert.ToString(global.Tprice);
                conn.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("An error occured");
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            menu co = new menu();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }
    }
}
