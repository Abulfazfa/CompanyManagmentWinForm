using Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3.Services;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private readonly EmployeeService _employeeService;
        private readonly AppUserService _appUserService;

        public Form1()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            _appUserService = new AppUserService();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            var appUser = _appUserService.GetByName(username);
            if (appUser != null)
            {
                if (appUser.Password == password)
                {
                    Form2 form2 = new Form2(appUser);
                    form2.Show();
                };
            }
            
        }
    }
}
