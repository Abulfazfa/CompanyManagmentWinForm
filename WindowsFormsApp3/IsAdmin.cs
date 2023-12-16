﻿using System;
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
    public partial class IsAdmin : Form
    {
        private readonly AppUserService appUserService;
        public IsAdmin()
        {
            InitializeComponent();
            appUserService = new AppUserService();
        }

        private void submit_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            var appUser = appUserService.GetByName(username);
            if (appUser.Password == password)
            {
                UserForm user = new UserForm();
                user.Show();
                this.Close();
            }
            else { MessageBox.Show("Password or username isn't right"); }
        }
    }
}