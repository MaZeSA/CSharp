using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Anketa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBoxOthers.Visible = false;
            comboBox1.SelectedIndex = 1;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBoxAge.Text = (DateTime.Now.Year - dateTimePicker1.Value.Year).ToString();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            textBoxOthers.Visible = checkBoxOther.Checked;
        }

        private void rB_Male_CheckedChanged(object sender, EventArgs e)
        {
            var rb = (sender as RadioButton);
            if (rb.Checked)
            {
                pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject(rb.Name);
            }
        }
       
        private void button1Save_Click(object sender, EventArgs e)
        {
            Save(GetUser());
        }

        User GetUser()
        {
            User user = new User
            {
                Name = textBox1.Text,
                Surname = textBoxSurname.Text,
                BirthDate = dateTimePicker1.Value,
                Country = comboBox1.SelectedIndex,
                Sex = rB_Female.Checked == true ? rB_Female.Name : rB_Male.Name,
                Other = textBoxOthers.Text
            };

            foreach (var item in this.Controls)
            {
                if (item is CheckBox)
                {
                    var checkButton = item as CheckBox;
                    if (checkButton.Name == "checkBoxOther" && string.IsNullOrWhiteSpace(textBoxOthers.Text))
                    {
                        continue;
                    }

                    if (checkButton.Checked)
                    {
                        user.HobyListCheck.Add(checkButton.Name);
                    }
                }
            }
            return user;
        }

        void Save(User user)
        {
            try { 
            File.WriteAllText("users.jsn", JsonConvert.SerializeObject(user, Formatting.Indented));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void buttonRead_Click(object sender, EventArgs e)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<User>(File.ReadAllText("users.jsn"));
                Read(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Read(User user)
        {
            textBox1.Text = user.Name;
            textBoxSurname.Text = user.Surname;
            dateTimePicker1.Value = user.BirthDate;
            comboBox1.SelectedIndex = user.Country;
            var rb = this.Controls.Find(user.Sex, false);
            ((RadioButton)rb.First()).Checked = true;

            foreach (var item in user.HobyListCheck)
            {
                ((this.Controls.Find(item, false)).First() as CheckBox).Checked = true;
            }

            textBoxOthers.Text = user.Other;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            db_Client db_Client = new db_Client();
            db_Client.PushToDB(GetUser());
        }
    }
}
