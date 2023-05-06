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
    public partial class studentRecord : Form
    {
        SqlConnection conn = new SqlConnection(
       @"data source= .\SQLEXPRESS;
        initial catalog = project3;
        user id = sa;
        password= kist@123");//this password may be different
        public studentRecord()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            studentForm co = new studentForm();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }

        private void studentRecord_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "select * from sales where PersonID = "+ global.ID;
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                sqlDataAdapter.Fill(table);
                dataGridView1.DataSource = table;

       
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured"+ex);
            }
        }
    }
}
