using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3.Models;
using WindowsFormsApp3.Services;

namespace WindowsFormsApp3
{
    public partial class Form2 : Form
    {
        private readonly User _appUser;

        public Form2()
        {
            InitializeComponent();
        }
        public Form2(User appUser)
        {
            InitializeComponent();
            _appUser = appUser;
            DisplayUserName();
        }

        private void DisplayUserName()
        {
            if (_appUser != null)
            {
                username.Text = _appUser.Username;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            Form2 form2 = new Form2();
            form2.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            Form2 form2 = new Form2();
            form2.Close();
        }
    }
}
