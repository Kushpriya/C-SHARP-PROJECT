using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project3
{
    public partial class studentForm : Form
    {
        public studentForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            menu co = new menu();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            home co = new home();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            studentRecord co = new studentRecord();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            studentChangePassword co = new studentChangePassword();     
            this.Hide();
            this.Close();
            co.ShowDialog();
        }

        private void studentForm_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
