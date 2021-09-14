using EntityForm.BLL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityForm3LayerClient
{
    public partial class UserControlTasks : UserControl
    {

        TaskDTO  taskDTO { set; get; }
        TaskDTO.Event Event { set; get; }
        public UserControlTasks(TaskDTO _taskDTO, TaskDTO.Event _event)
        {
            InitializeComponent();

            if (_event == TaskDTO.Event.Add)
            { buttonRemove.Enabled = buttonUpdate.Enabled = false; }
            else
            { buttonAdd.Enabled = false; }

            Event = _event;
            taskDTO = _taskDTO;
            textBox1.Text = _taskDTO?.Title;
            numericUpDown1.Value = Convert.ToDecimal(_taskDTO?.Priority);
        }

        public delegate void EventTaskHandler(TaskDTO positionDTO, TaskDTO.Event @event);
        public event EventTaskHandler EventTask;
      
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            taskDTO.Title = textBox1.Text;
            taskDTO.Priority = Convert.ToInt32(numericUpDown1.Value);
            EventTask?.Invoke(taskDTO, TaskDTO.Event.Update);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            EventTask?.Invoke(taskDTO, TaskDTO.Event.Remove);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            taskDTO.Title = textBox1.Text;
            taskDTO.Priority = Convert.ToInt32(numericUpDown1.Value);
            EventTask?.Invoke(taskDTO, TaskDTO.Event.Add);
        }

        private void textBox1_Leave_1(object sender, EventArgs e)
        {
            //if (textBox1.Text.Length < 5)
            //{
            //    MessageBox.Show("Title minimum 5 length");
            //    textBox1.Focus();
            //}
        }
    }
}
