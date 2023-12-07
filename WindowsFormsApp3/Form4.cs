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

namespace WindowsFormsApp3
{
    public partial class Form4 : Form
    {
        private readonly EmployeeService employeeService;
        private readonly DepartmentService departmentService;
        public Form4()
        {
            InitializeComponent();
            employeeService = new EmployeeService();
            departmentService = new DepartmentService();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'spotifyDataSet.Departments' table. You can move, or remove it, as needed.
            
            //searchCount.KeyPress += insertCount_KeyPress;
            //insertCapacity.KeyPress += insertCapacity_KeyPress;
            //searchCapacity.KeyPress += insertCapacity_KeyPress;
            PopulateDataGridView();
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void insertNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgv_DoubleClick(object sender, EventArgs e)
        {
            if (dgv.CurrentRow.Index != -1)
            {
                int employeeId = Convert.ToInt32(dgv.CurrentRow.Cells["Id"].Value);
                var employee = employeeService.GetById(employeeId);
                insertName.Text = employee.Name;
                insertAddress.Text = employee.Address;
                insertAge.Text = employee.Age.ToString();
                insertDepName.Text = employee.Department.Name;
                insertSurname.Text = employee.Surname;
                IdBox.Text = employee.EmployeeId.ToString();

                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = insertName.Text;
            string surname = insertSurname.Text;
            string address = insertAddress.Text;
            int age = int.Parse(insertAge.Text);
            string depName = insertDepName.Text;

            Department existingDepartment = departmentService.Get(d => d.Name == depName);

            if (existingDepartment != null)
            {
                var result = employeeService.Create(new Employee()
                {
                    Name = name,
                    Surname = surname, 
                    Address = address,
                    Age = age,
                    DepartmentId = existingDepartment.Id,
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
                MessageBox.Show("Department isn't exist");
            }
        }

        //private void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    string name = insertName.Text;
        //    int capacity = int.Parse(insertCapacity.Text);
        //    int departmentId = int.Parse(IdBox.Text);
        //    Department existingDepartment = new Department()
        //    {
        //        Name = name,
        //        Capacity = capacity,
        //    };
        //    var result = departmentService.Update(departmentId, existingDepartment);
        //    if (result) MessageBox.Show("Submitted successfully.");
        //    else { MessageBox.Show("Something goes wrong."); }
        //}

        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    int departmentId = int.Parse(IdBox.Text);
        //    var result = departmentService.Delete(departmentId);
        //    if (result) MessageBox.Show("Removed successfully.");
        //    else { MessageBox.Show("Something goes wrong."); }
        //}

        //private void btnSearch_Click(object sender, EventArgs e)
        //{
        //    List<Employee> employees = employeeService.GetAll();
        //    if (!string.IsNullOrEmpty(searchName.Text))
        //    {
        //        string name = searchName.Text;
        //        departments = departments.Where(d => d.Name == name).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(searchCapacity.Text))
        //    {
        //        int capacity = Convert.ToInt32(searchCapacity.Text);
        //        departments = departments.Where(d => d.Capacity == capacity).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(searchCount.Text))
        //    {
        //        int count = Convert.ToInt32(searchCount.Text);
        //        departments = departments.Where(d => d.MemberCount == count).ToList();
        //    }
        //    dgv.DataSource = departments;
        //}

        void PopulateDataGridView()
        {
            dgv.AutoGenerateColumns = false;

            var employeesWithDepartments = employeeService.GetAll()
                .Select(employee => new
                {
                    employee.EmployeeId,
                    employee.Name,
                    employee.Surname,
                    employee.Address,
                    employee.Age,
                    DepartmentName = employee.Department.Name,
                    employee.CreatingTime
                })
                .ToList();

            dgv.DataSource = employeesWithDepartments;
        }

    }
}
