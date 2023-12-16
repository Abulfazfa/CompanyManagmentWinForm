﻿using System;
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
        private readonly Models.User _appUser;

        public Form2()
        {
            InitializeComponent();
        }
        public Form2(Models.User appUser)
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
            DepartmentForm form3 = new DepartmentForm(_appUser);
            form3.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EmployeeForm form4 = new EmployeeForm(_appUser);
            form4.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserForm form5 = new UserForm();
            form5.Show();
            this.Close();
        }
    }
}
