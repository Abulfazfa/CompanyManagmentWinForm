using Business.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp3.Models;


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
            this.departmentsTableAdapter.Fill(this.spotifyDataSet.Departments);
            searchCount.KeyPress += insertCount_KeyPress;
            insertCapacity.KeyPress += insertCapacity_KeyPress;
            searchCapacity.KeyPress += insertCapacity_KeyPress;
            PopulateDataGridView();
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;

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
            dgv.AutoGenerateColumns = false;
            dgv.DataSource = departmentService.GetAll();
        }
        
        private void dgv_DoubleClick(object sender, EventArgs e)
        {
            if (dgv.CurrentRow.Index != -1)
            {
                int departmentId = Convert.ToInt32(dgv.CurrentRow.Cells["Id"].Value);
                var department = departmentService.Get(d => d.Id == departmentId);
                insertName.Text = department.Name;
                insertCapacity.Text = department.Capacity.ToString();
                IdBox.Text = department.Id.ToString();

                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true; 
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string name = insertName.Text;
            int capacity = int.Parse(insertCapacity.Text);

            Department existingDepartment = departmentService.Get(d => d.Name == name);

            if (existingDepartment == null)
            {
                var result = departmentService.Create(new Department()
                {
                    Name = name,
                    Capacity = capacity
                });
                if (result)
                {
                    MessageBox.Show("Submitted successfully.");
                }
                else
                {
                    MessageBox.Show("Something goes wrong.");
                }
            }
            else
            {
                MessageBox.Show("Department with this name already exists. Please choose a different name.");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string name = insertName.Text;
            int capacity = int.Parse(insertCapacity.Text);
            int departmentId = int.Parse(IdBox.Text);
            Department existingDepartment = new Department()
            {
                Name = name,
                Capacity = capacity,
            };
            var result = departmentService.Update(departmentId, existingDepartment);
            if (result) MessageBox.Show("Submitted successfully.");
            else { MessageBox.Show("Something goes wrong."); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int departmentId = int.Parse(IdBox.Text);
            var result = departmentService.Delete(departmentId);
            if (result) MessageBox.Show("Removed successfully.");
            else { MessageBox.Show("Something goes wrong."); }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<Department> departments = departmentService.GetAll();
            if (!string.IsNullOrEmpty(searchName.Text)) { 
                string name = searchName.Text; 
                departments = departments.Where(d => d.Name == name).ToList();
            }
            if (!string.IsNullOrEmpty(searchCapacity.Text)) { 
                int capacity = Convert.ToInt32(searchCapacity.Text);
                departments = departments.Where(d => d.Capacity == capacity).ToList();
            }
            if (!string.IsNullOrEmpty(searchCount.Text)) { 
                int count = Convert.ToInt32(searchCount.Text);
                departments = departments.Where(d => d.MemberCount == count).ToList();
            }
            dgv.DataSource = departments;
        }
    }
}
