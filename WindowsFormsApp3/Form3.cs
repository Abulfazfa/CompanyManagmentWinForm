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
using WindowsFormsApp3.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp3
{
    public partial class Form3 : Form
    {
        private readonly DepartmentService departmentService;
        public Form3()
        {
            InitializeComponent();
            departmentService = new DepartmentService();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'spotifyDataSet.Departments' table. You can move, or remove it, as needed.
            this.departmentsTableAdapter.Fill(this.spotifyDataSet.Departments);
            insertCount.KeyPress += insertCount_KeyPress;
            insertCapacity.KeyPress += insertCapacity_KeyPress;
            PopulateDataGridView();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string name = insertName.Text;
            int capacity = int.Parse(insertCapacity.Text);
            int count = int.Parse(insertCount.Text);

            Department existingDepartment = departmentService.Get(d => d.Name == name);

            if (existingDepartment == null)
            {
                departmentService.Create(new Department()
                {
                    Name = name,
                    Capacity = capacity,
                    MemberCount = count
                });
            }
            else
            {
                MessageBox.Show("Department with this name already exists. Please choose a different name.");
            }
        }

        private void insertCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void insertCapacity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        void PopulateDataGridView()
        {
            dgv.DataSource = departmentService.GetAll();
        }


    }
}
