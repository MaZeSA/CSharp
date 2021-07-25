using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parent
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Person person1 { get; set; }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Child child = (person1 == null) ? new Child(textBox1.Text) : new Child(person1);
            child.FormParent = this;
            child.ShowDialog();
        }

        public void SetPerson(Person person)
        {
            person1 = person;

            textBox1.Text = person.Name;
            label3.Text = person.Surname;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(person1.Phone);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Text|*.txt|Format Text|*.rtf";
            saveDialog.AddExtension = true;

            if(saveDialog.ShowDialog() == DialogResult.OK)
            {
                string saveString = $" {person1.Name} | {person1.Surname} | {person1.Phone} ";
                File.WriteAllText(saveDialog.FileName, saveString);
            }
        }
    }
}
