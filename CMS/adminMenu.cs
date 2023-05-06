using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project3
{
    public partial class adminMenu : Form
    {

        public adminMenu()
        {
            InitializeComponent();

        }




        private void button1_Click(object sender, EventArgs e)
        {
            addAccount co = new addAccount();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            home co = new home();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updateMenu co = new updateMenu();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            recordView co = new recordView();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            adminChangePassword co = new adminChangePassword();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            deleteStudentAccount co = new deleteStudentAccount();
            this.Close();
            this.Hide();
            co.ShowDialog();
        }

        private void adminMenu_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            home co = new home();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            deleteStudentAccount co = new deleteStudentAccount();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            adminChangePassword co = new adminChangePassword();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            recordView co = new recordView();
            this.Hide();
            this.Close();
            co.ShowDialog();
        }
    }
}

