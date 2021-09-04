using AdoForm.BLL.Model;
using AdoForm.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoForm3LayerClient
{
    public partial class UserControlEmployee : UserControl
    {
        readonly int id = -1;
        public EmployeeDTO EmployeeDTO {set =>Map(value);
            get 
            {
                var emloyee = new EmployeeDTO { 
                    Id = id,
                    Name = textBox1.Text,
                    Surname = textBox2.Text,
                    Age = int.Parse(textBox3.Text),
                    Salary = double.Parse(textBox4.Text),
                    Position = new PositionDTO { Title = comboBox1.SelectedValue.ToString()  }
                };

                var list = new List<TaskDTO>();
                foreach (var r in checkedListBox1.CheckedItems)
                {
                    list.Add(new TaskDTO { Title = r.ToString() });
                }
                emloyee.Tasks = list;

                return emloyee;
            }
        }
        public UserControlEmployee(EmployeeDTO employeeDTO, IEnumerable<PositionDTO> positionDTOs, IEnumerable<TaskDTO> taskDTOs)
        {
            InitializeComponent();
            EmployeeDTO = employeeDTO;

            id = employeeDTO.Id;
            comboBox1.DataSource = null;
            comboBox1.DataSource = positionDTOs;
            comboBox1.DisplayMember = "Title";
            comboBox1.ValueMember = "Title";

            comboBox1.Text = employeeDTO.Position.Title;

            foreach(var i in taskDTOs)
            {
                checkedListBox1.Items.Add(i.Title);

                if (employeeDTO.Tasks != null)
                {
                    foreach (var t in employeeDTO.Tasks)
                    {
                        if (i.Title == t.Title)
                        {
                            checkedListBox1.SetItemChecked(checkedListBox1.Items.Count-1, true);
                            break;
                        }
                    }
                }
            }
        }

        void Map(EmployeeDTO employeeDTO)
        {
            textBox1.Text = employeeDTO.Name;
            textBox2.Text = employeeDTO.Surname;
            textBox3.Text = employeeDTO.Age.ToString();
            textBox4.Text = employeeDTO.Salary.ToString();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 44)
            {
                e.Handled = true;
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

       public bool Valid()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox3.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                textBox4.Focus();
                return false;
            }
            if(comboBox1.SelectedItem is null)
            {
                comboBox1.Focus();
                return false;
            }
            if(checkedListBox1.CheckedItems.Count <= 0)
            {
                checkedListBox1.Focus();
                return false;
            }
            return true;
        }


    }
}
