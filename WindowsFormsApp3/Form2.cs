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

namespace WindowsFormsApp3
{
    public partial class Form2 : Form
    {
        private readonly AppUser _appUser;

        public Form2()
        {
            InitializeComponent();
        }
        public Form2(AppUser appUser)
        {
            InitializeComponent();
            _appUser = appUser;
            DisplayUserName();
        }

        private void DisplayUserName()
        {
            if (_appUser != null)
            {
                label1.Text = _appUser.Username;
            }
        }
    }
}
