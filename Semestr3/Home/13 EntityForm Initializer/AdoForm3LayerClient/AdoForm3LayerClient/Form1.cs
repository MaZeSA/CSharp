using System;
using System.Windows.Forms;
using EntityForm.BLL.Servises;
using System.Collections.ObjectModel;
using EntityForm.BLL.Model;
using System.Collections.Generic;

namespace EntityForm3LayerClient
{
    public partial class Form1 : Form
    {
        private readonly IEntityFormServises adoFormServises;
        public ObservableCollection<EmployeeDTO> Employees { get; set; } = new ObservableCollection<EmployeeDTO>(); 
        public IEnumerable<PositionDTO> Positions { get; set; } = new ObservableCollection<PositionDTO>(); 
        public IEnumerable<TaskDTO> Tasks { get; set; } = new ObservableCollection<TaskDTO>();

        public Form1(IEntityFormServises _adoFormServises)
        {
            InitializeComponent();
            adoFormServises = _adoFormServises;
            
        }
        private void UpdateBooks(IEntityFormServises _adoFormServises)
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
            comboBoxPosition.DataSource = null;
            Positions = adoFormServises.GetPositions();
            comboBoxPosition.DataSource = Positions;
            comboBoxPosition.DisplayMember = "Title";

            Status("Loading Tasks...");
            comboBoxTask.DataSource = null;
            Tasks = adoFormServises.GetTasks();
            comboBoxTask.DataSource = Tasks;
            comboBoxTask.DisplayMember = "Title";
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
            buttonAddEmployee.Enabled = buttonAddTask.Enabled =
                buttonAddPsition.Enabled = buttonNew.Enabled = true;

            Status("Connected");

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
            if ((panel5.Controls[0] as UserControlEmployee).Valid())
            {
                adoFormServises.AddEmployee((panel5.Controls[0] as UserControlEmployee).EmployeeDTO);
                newMode = true;
                buttonLoad_Click(sender, e);
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            panel5.Controls.Clear();
            var pos = new UserControlPosition(new PositionDTO { Title =""}, PositionDTO.Event.Add);
            pos.EventPosition += Pos_EventPosition;
            panel5.Controls.Add(pos);

        }

        private void Pos_EventPosition(PositionDTO positionDTO, PositionDTO.Event @event)
        {
            Status("Event Position");
            switch(@event)
            {
                case PositionDTO.Event.Add:
                    {
                        adoFormServises.AddPosition(positionDTO);
                        break;
                    }
                case PositionDTO.Event.Update:
                    {
                        adoFormServises.UpdatePosition(positionDTO);
                        break;
                    }
                case PositionDTO.Event.Remove:
                    {
                        adoFormServises.RemovePosition(positionDTO);
                        break;
                    }
            }
            LoadPositions();
            panel5.Controls.Clear();
            Status("Positions Loaded");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel5.Controls.Clear();
            var pos = new UserControlPosition(comboBoxPosition.SelectedItem as PositionDTO, PositionDTO.Event.Update);
            pos.EventPosition += Pos_EventPosition;
            panel5.Controls.Add(pos);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel5.Controls.Clear();
            var pos = new UserControlTasks(new TaskDTO { Title = "", Priority=0 }, TaskDTO.Event.Add);
            pos.EventTask += Pos_EventTask;
            panel5.Controls.Add(pos);
        }

        private void Pos_EventTask(TaskDTO taskDTO, TaskDTO.Event @event)
        {
            Status("Event Tack");
            switch (@event)
            {
                case TaskDTO.Event.Add:
                    {
                        adoFormServises.AddTask(taskDTO);
                        break;
                    }
                case TaskDTO.Event.Update:
                    {
                        adoFormServises.UpdateTask(taskDTO);
                        break;
                    }
                case TaskDTO.Event.Remove:
                    {
                        adoFormServises.RemoveTask(taskDTO);
                        break;
                    }
            }
            LoadPositions();
            panel5.Controls.Clear();
            Status("Tack Loaded");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel5.Controls.Clear();
            var task = new UserControlTasks(comboBoxTask.SelectedItem as TaskDTO, TaskDTO.Event.Update);
            task.EventTask += Pos_EventTask;
            panel5.Controls.Add(task);
        }

        private void buttonEmployeeAdd_Click_1(object sender, EventArgs e)
        {
            buttonNew_Click(sender, e);
        }

        private void panel5_ControlAdded(object sender, ControlEventArgs e)
        {
            if(panel5.Controls[0] is UserControlEmployee)
            {
                panel4.Enabled = true;
            }
            else
            {
                panel4.Enabled = false;
            }
        }
    }
}
