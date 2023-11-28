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

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private readonly EmployeeService _employeeService;
        public Form1()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var text = _employeeService.GetAll();
            panel1.Text = text.Count().ToString();
        }
    }
}
