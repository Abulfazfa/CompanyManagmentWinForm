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
using Utilities.Helpers;
using WindowsFormsApp3.Models;
using WindowsFormsApp3.Services;

namespace WindowsFormsApp3
{
	public partial class UserForm : Form
	{
		private readonly AppUserService appUserService;
		private readonly User appUser;

		public UserForm(User user)
		{
			InitializeComponent();
			appUserService = new AppUserService();
			appUser = user;
		}

		private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void dgv_DoubleClick(object sender, EventArgs e)
		{
			if (dgv.CurrentRow.Index != -1)
			{
				int userId = Convert.ToInt32(dgv.CurrentRow.Cells["Id"].Value);
				var user = appUserService.Get(d => d.Id == userId);
				insertName.Text = user.Username;
				insertPassword.Text = user.Password;
				IdBox.Text = userId.ToString();

				btnSave.Enabled = false;
				btnUpdate.Enabled = true;
				btnDelete.Enabled = true;
			}
		}

		private void User_Load(object sender, EventArgs e)
		{
			PopulateDataGridView();
		}

		void PopulateDataGridView()
		{
			dgv.AutoGenerateColumns = false;
			dgv.DataSource = appUserService.GetAll();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(insertName.Text) || string.IsNullOrEmpty(insertPassword.Text))
			{
				MessageBox.Show(MessageConstants.InvalidOrNull);
				return;
			}

			string name = insertName.Text;
			string password = insertPassword.Text;

			User existuser = appUserService.GetByName(name);
			if (existuser == null && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(password))
			{
				var result = appUserService.Create(new User()
				{
					Username = name,
					Password = password
				});
				PopulateDataGridView();
				if (result)
				{
					MessageBox.Show(MessageConstants.SubmitSuccess);
				}
				else
				{
					MessageBox.Show(MessageConstants.ErrorHappen);
				}
			}
			else
			{
				MessageBox.Show(MessageConstants.ExistUserNotice);
			}
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(insertName.Text) || string.IsNullOrEmpty(insertPassword.Text))
			{
				MessageBox.Show(MessageConstants.InvalidOrNull);
				return;
			}
			string name = insertName.Text;
			string password = insertPassword.Text;
			int userId = Convert.ToInt32(IdBox.Text);

			User existUser = new User()
			{
				Username = name,
				Password = password
			};

			var result = appUserService.Update(userId, existUser);
			btnSave.Enabled = true;
			btnUpdate.Enabled = false;
			btnDelete.Enabled = false;
			PopulateDataGridView();
			if (result)
			{
				MessageBox.Show(MessageConstants.SubmitSuccess);
			}
			else { MessageBox.Show(MessageConstants.ErrorHappen); }
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			int userId = int.Parse(IdBox.Text);
			var username = appUserService.GetByName(insertName.Text).Username;
			var result = appUserService.Delete(userId);
			btnSave.Enabled = true;
			btnUpdate.Enabled = false;
			btnDelete.Enabled = false;
			PopulateDataGridView();
			if (result)
			{
				MessageBox.Show(MessageConstants.RemovedSuccess);
			}
			else { MessageBox.Show(MessageConstants.ErrorHappen); }
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			insertName.Text = null;
			insertPassword.Text = null;
			btnSave.Enabled = true;
			btnUpdate.Enabled = false;
			btnDelete.Enabled = false;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Sections form2 = new Sections(appUser);
			form2.Show();
			this.Close();
		}
	}
}
