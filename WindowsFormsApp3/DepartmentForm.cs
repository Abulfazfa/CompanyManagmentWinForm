using Business.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp3.Models;
using WindowsFormsApp3.Repositories;
using WindowsFormsApp3.Services;

namespace WindowsFormsApp3
{
    public partial class DepartmentForm : Form
    {
        private readonly DepartmentService departmentService;
        private readonly User _user;
        private readonly CommandRepository _commandService;
        public DepartmentForm()
        {
            InitializeComponent();
        }
        public DepartmentForm(User appUser)
        {
            InitializeComponent();
            departmentService = new DepartmentService();
            _commandService = new CommandRepository();
            _user = appUser;
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
                    Capacity = capacity,
                    MemberCount = 0
                }) ;
                PopulateDataGridView();
                if (result)
                {
                    _commandService.Create(new Command() { Username = _user.Username, UsedFor = $"ADD Department {name}", ApplyDate = DateTime.Now });
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
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            PopulateDataGridView();
            if (result) 
            {
                //_commandService.Create(new Command() { Username = _user.Username, UsedFor = $"UPDATE Department {existingDepartment.Name}" });
                MessageBox.Show("Submitted successfully.");
            }
            else { MessageBox.Show("Something goes wrong."); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int departmentId = int.Parse(IdBox.Text);
            var departmentName = departmentService.GetById(departmentId).Name;
            var result = departmentService.Delete(departmentId);
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            PopulateDataGridView();
            if (result)
            {
                _commandService.Create(new Command() { Username = _user.Username, UsedFor = $"REMOVE Department {departmentName}", ApplyDate = DateTime.Now });
                MessageBox.Show("Removed successfully.");
            }
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            insertName.Text = null;
            insertCapacity.Text = null;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
    }
}
