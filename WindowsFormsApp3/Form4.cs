using Business.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

            insertAge.KeyPress += insertNumber_KeyPress;
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
                int employeeId = Convert.ToInt32(dgv.CurrentRow.Cells["EmployeeId"].Value);
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

        private void btnSave_Click_1(object sender, EventArgs e)
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
                PopulateDataGridView();
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string name = insertName.Text;
            string surname = insertSurname.Text;
            string address = insertAddress.Text;
            int age = int.Parse(insertAge.Text);
            int employeeId = int.Parse(IdBox.Text);
            Employee employee = new Employee()
            {
                Name = name,
                Surname = surname,
                Address = address,
                Age = age,
                DepartmentId = employeeService.GetById(employeeId).DepartmentId
            };
            var result = employeeService.Update(employeeId, employee);
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            PopulateDataGridView();
            if (result) MessageBox.Show("Submitted successfully.");
            else { MessageBox.Show("Something goes wrong."); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int employeeId = int.Parse(IdBox.Text);
            var result = employeeService.Delete(employeeId);
            PopulateDataGridView();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            if (result) MessageBox.Show("Removed successfully.");
            else { MessageBox.Show("Something goes wrong."); }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<Employee> employees = employeeService.GetAll();
            if (!string.IsNullOrEmpty(searchName.Text))
            {
                string nameOrSurname = searchName.Text;
                employees = employees.Where(d => d.Name.ToUpper().Contains(nameOrSurname.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(searchAge.Text))
            {
                int age = Convert.ToInt32(searchAge.Text);
                employees = employees.Where(d => d.Age == age).ToList();
            }
            if (!string.IsNullOrEmpty(searchDepname.Text))
            {
                string depName = searchDepname.Text;
                employees = employees.Where(d => d.Department.Name == depName).ToList();
            }
            dgv.DataSource = employees;
        }
    }
}
