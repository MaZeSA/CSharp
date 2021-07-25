using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parent
{
    public partial class Child : Form
    {
       public Form1 FormParent { set; get; }// сенсу мало та умова
        public Child(string name)
        {
            InitializeComponent();
            textBox1.Text = name;
        }
        public Child(Person person)
        {
            InitializeComponent();
            textBox1.Text = person.Name;
            textBox2.Text = person.Surname;
            textBox3.Text = person.Phone;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Person person = new Person
            {
                Name = textBox1.Text,
                Surname = textBox2.Text,
                Phone = textBox3.Text
            };
            FormParent.SetPerson(person);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
