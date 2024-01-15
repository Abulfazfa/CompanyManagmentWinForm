using Business.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Utilities.Helpers;
using WindowsFormsApp3.Models;
using WindowsFormsApp3.Services;

namespace WindowsFormsApp3
{
	public partial class EmployeeForm : Form
	{
		private readonly EmployeeService employeeService;
		private readonly DepartmentService departmentService;
		private readonly User _user;
		private readonly CommandService _commandService;
		//public EmployeeForm()
		//{
		//	InitializeComponent();
		//	employeeService = new EmployeeService();
		//	departmentService = new DepartmentService();
		//}

		public EmployeeForm(User appUser)
		{
			InitializeComponent();
			departmentService = new DepartmentService();
			employeeService = new EmployeeService();
			_user = appUser;
			_commandService = new CommandService();
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
				insertDepName.Text = departmentService.GetById(employee.DepartmentId).Name;
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
					DepartmentName = departmentService.GetById(employee.DepartmentId).Name,
					employee.CreatingTime
				})
				.ToList();

			dgv.DataSource = employeesWithDepartments;
		}

		private void btnSave_Click_1(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(insertName.Text) || string.IsNullOrEmpty(insertSurname.Text) || string.IsNullOrEmpty(insertAddress.Text) || string.IsNullOrEmpty(insertAge.Text) || string.IsNullOrEmpty(insertDepName.Text))
			{
				MessageBox.Show(MessageConstants.InvalidOrNull);
				return;
			}
			string name = insertName.Text;
			string surname = insertSurname.Text;
			string address = insertAddress.Text;
			int age = int.Parse(insertAge.Text);
			string depName = insertDepName.Text;

			Department existingDepartment = departmentService.Get(d => d.Name == depName);

			if (existingDepartment != null)
			{
				var emp = new Employee()
				{
					Name = name,
					Surname = surname,
					Address = address,
					Age = age,
					DepartmentId = existingDepartment.Id,
				};
				var result = employeeService.Create(emp);
				PopulateDataGridView();
				if (result)
				{
					_commandService.Create(new Command() { Username = _user.Username, UsedFor = $"ADD Department {emp.Name}" });
					MessageBox.Show(MessageConstants.SubmitSuccess);
				}
				else
				{
					MessageBox.Show(MessageConstants.ErrorHappen);
				}
			}
			else
			{
				MessageBox.Show(MessageConstants.DepartmentNotFind);
			}
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(insertName.Text) || string.IsNullOrEmpty(insertSurname.Text) || string.IsNullOrEmpty(insertAddress.Text) || string.IsNullOrEmpty(insertAge.Text) || string.IsNullOrEmpty(insertDepName.Text))
			{
				MessageBox.Show(MessageConstants.InvalidOrNull);
				return;
			}

			string name = insertName.Text;
			string surname = insertSurname.Text;
			string address = insertAddress.Text;
			int age = int.Parse(insertAge.Text);
			int employeeId = int.Parse(IdBox.Text);
			var department = departmentService.Get(d => d.Name == insertDepName.Text);
			if (department == null)
			{
				MessageBox.Show(MessageConstants.ValidDepartmentNotice);
			}
			else
			{
				Models.Employee employee = new Employee()
				{
					Name = name,
					Surname = surname,
					Address = address,
					Age = age,
					DepartmentId = department.Id,
				};
				var result = employeeService.Update(employeeId, employee);
				btnSave.Enabled = true;
				btnUpdate.Enabled = false;
				btnDelete.Enabled = false;
				PopulateDataGridView();
				if (result)
				{
					_commandService.Create(new Command() { Username = _user.Username, UsedFor = $"UPDATE Department {employee.Name}" });
					MessageBox.Show(MessageConstants.SubmitSuccess);
				}
				else { MessageBox.Show(MessageConstants.ErrorHappen); }
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			int employeeId = int.Parse(IdBox.Text);
			var empName = employeeService.GetById(employeeId).Name;
			var result = employeeService.Delete(employeeId);
			PopulateDataGridView();
			btnSave.Enabled = true;
			btnUpdate.Enabled = false;

			btnDelete.Enabled = false;
			if (result)
			{
				_commandService.Create(new Command()
				{
					Username = _user.Username,
					UsedFor = $"REMOVE Department {empName}"
				});
				MessageBox.Show(MessageConstants.RemovedSuccess);
			}
			else { MessageBox.Show(MessageConstants.ErrorHappen); }
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			List<Models.Employee> employees = employeeService.GetAll();
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
			dgv.DataSource = employees.Select(employee => new
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
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			insertName.Text = null;
			insertDepName.Text = null;
			insertAge.Text = null;
			insertAddress.Text = null;
			insertSurname.Text = null;
			btnSave.Enabled = true;
			btnUpdate.Enabled = false;
			btnDelete.Enabled = false;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Sections form2 = new Sections(_user);
			form2.Show();
			this.Close();
		}
	}
}
