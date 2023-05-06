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
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            admin co = new admin();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            studentLogin co = new studentLogin();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void home_Load(object sender, EventArgs e)
        {
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = Color.FromArgb(80, 0, 0, 0);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            global.isGuestModeOn = true;
            menu co = new menu();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }
    }
}

      

