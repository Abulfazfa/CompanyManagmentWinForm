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
using WindowsFormsApp3.Services;

namespace WindowsFormsApp3
{
    public partial class CommandForm : Form
    {
		private readonly CommandService _commandService;
        public CommandForm()
        {
            InitializeComponent();
			_commandService = new CommandService();
			PopulateDataGridView();

		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			List<Command> commands = _commandService.GetAll();
			if (!string.IsNullOrEmpty(searchName.Text))
			{
				string name = searchName.Text;
				commands = commands.Where(d => d.Username.ToUpper().Contains(name.ToUpper())).ToList();
			}
			if (!string.IsNullOrEmpty(searchCapacity.Text))
			{
				string usedFor = searchCapacity.Text;
				commands = commands.Where(d => d.UsedFor.ToUpper().Contains(usedFor.ToUpper())).ToList();
			}
			
			dgv.DataSource = commands;
		}

		void PopulateDataGridView()
		{
			dgv.AutoGenerateColumns = false;
			dgv.DataSource = _commandService.GetAll();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Sections form2 = new Sections();
			form2.Show();
			this.Close();
		}
	}
}
