using System;
using System.Windows.Forms;
using AdoForm.BLL.Servises;
using System.Collections.ObjectModel;
using AdoForm.BLL.Model;
using System.Collections.Generic;

namespace AdoForm3LayerClient
{
    public partial class Form1 : Form
    {
        private readonly IAdoFormServises adoFormServises;
        public ObservableCollection<EmployeeDTO> Employees { get; set; } = new ObservableCollection<EmployeeDTO>(); 
        public IEnumerable<PositionDTO> Positions { get; set; } = new ObservableCollection<PositionDTO>(); 
        public IEnumerable<TaskDTO> Tasks { get; set; } = new ObservableCollection<TaskDTO>();

        public Form1(IAdoFormServises _adoFormServises)
        {
            InitializeComponent();
            adoFormServises = _adoFormServises;
            
        }
        private void UpdateBooks(IAdoFormServises _adoFormServises)
        {
            Employees.Clear();
            var temp = _adoFormServises.GetEmployees();
            foreach (var item in temp)
            {
                Employees.Add(item);
            }
        }

        void LoadPositions()
        {
            Status("Loading Positions...");
            comboBox1.DataSource = null;
            Positions = adoFormServises.GetPositions();
            comboBox1.DataSource = Positions;
            comboBox1.DisplayMember = "Title";

            Status("Loading Tasks...");
            comboBox2.DataSource = null;
            Tasks = adoFormServises.GetTasks();
            comboBox2.DataSource = Tasks;
            comboBox2.DisplayMember = "Title";
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            Status("Loading...");
            listBox1.DataSource = null;
            UpdateBooks(adoFormServises);

            listBox1.DataSource = Employees;
            listBox1.DisplayMember = "GetDispay";
            
            LoadPositions();

            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedItem = listBox1.Items[listBox1.Items.Count - 1];
            }
            buttonNew.Enabled = true;

            Status();
        }
        void Status(string msg= "OK")
        {
            toolStripStatusLabel2.Text = msg;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex >= 0)
            {
                buttonRemove.Enabled = true;
                buttonUpdate.Enabled = true;
                panel5.Controls.Clear();
                panel5.Controls.Add(new UserControlEmployee(listBox1.SelectedItem as EmployeeDTO, Positions, Tasks));
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            Status("Remove...");
            panel5.Controls.Clear();
            adoFormServises.RemoveEmployee(listBox1.SelectedItem as EmployeeDTO);
            buttonLoad_Click(sender, e);
            Status();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            adoFormServises.AddEmployee((panel5.Controls[0] as UserControlEmployee).EmployeeDTO);
            newMode = true;
            buttonLoad_Click( sender,  e);
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            panel5.Controls.Clear();
            panel5.Controls.Add(new UserControlEmployee(new EmployeeDTO { Position = new PositionDTO(), Tasks = new List<TaskDTO>() }, Positions, Tasks));
            buttonAdd.Enabled = true;

            newMode = false;
        }
        private void buttonUpdate_EnabledChanged(object sender, EventArgs e)
        {
            buttonAdd.Enabled = !buttonUpdate.Enabled;
        }

        bool newMode { set => buttonUpdate.Enabled = buttonRemove.Enabled = value; }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Status("Update...");
            if ((panel5.Controls[0] as UserControlEmployee).Valid())
            {
                int s = listBox1.SelectedIndex;
                adoFormServises.UpdateEmployee((panel5.Controls[0] as UserControlEmployee).EmployeeDTO);
                buttonLoad_Click(sender, e);
                listBox1.SelectedIndex = s;
            }
            Status();
        }
    }
}
