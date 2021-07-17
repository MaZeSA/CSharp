using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BestOil
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetBindings();
        }

        void SetBindings()
        {

            comboBox1.DataSource = Fuels;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Price";
            comboBox1.SelectedIndex = 1;
            toolStripStatusLabel2.Text = allSum.ToString();

            textBox2.DataBindings.Add("Enabled", radioButton1, "Checked");
            textBox3.DataBindings.Add("Enabled", radioButton2, "Checked");
            textBoxFuelPrice.DataBindings.Add("Text", comboBox1, "SelectedValue");

            textBox5.DataBindings.Add("Enabled", checkBox1, "Checked"); 
            textBox7.DataBindings.Add("Enabled", checkBox2, "Checked"); 
            textBox9.DataBindings.Add("Enabled", checkBox3, "Checked"); 
            textBox11.DataBindings.Add("Enabled", checkBox4, "Checked");
        }

        public List<Fuel> Fuels = new List<Fuel>
        {
            new Fuel{Name = "A 80", Price = 24.5}, 
            new Fuel{Name = "A 92", Price = 25.8},
            new Fuel{Name = "A 95", Price = 27.2},
            new Fuel{Name = "Disel", Price = 28.1},
        };

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            double sum = 0;
            foreach(TextBox item in panel2.Controls)
            {
                var index = panel2.Controls.IndexOf(item);
                if (((panel3.Controls[index]) as CheckBox).Checked)
                {
                    if (!string.IsNullOrWhiteSpace(item.Text))
                    {
                        sum += Convert.ToDouble(item.Text) * Convert.ToDouble(((panel1.Controls[index]) as TextBox).Text);
                    }
                }
            }
            label9.Text = string.Format("{0:0.00}", sum);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (number != 8)
            {
                if (!Char.IsDigit(number))
                {
                    e.Handled = true;
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox5_TextChanged(null, null);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;

            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (radioButton1.Checked)
                {
                    label6.Text = (Convert.ToInt32(textBox.Text) * (double)comboBox1.SelectedValue).ToString();
                }
                else
                {
                    label6.Text = string.Format("{0:0.00}", (Convert.ToInt32(textBox.Text) / (double)comboBox1.SelectedValue));
                }
            }
            else
            {
                label6.Text = "00.00";
            }
        }

        private void radioButton1_Checkedhanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Name == "radioButton1")
            {
                groupBox3.Text = "До оплати";
                label7.Text = "грн.";  
                textBox2_TextChanged(textBox2, e);
            }
            else
            {
                groupBox3.Text = "До видачі";
                label7.Text = "л.";
                textBox2_TextChanged(textBox3, e);
            }
        }

        double allSum = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            var sum = Convert.ToDouble(label6.Text);
            if(radioButton2.Checked)
            {
                sum = (double)comboBox1.SelectedValue * sum;
            }
            sum = Convert.ToDouble(label9.Text) + sum;

            label10.Text = string.Format("{0:0.00}", sum);

            allSum += sum;

            timer1.Start();

            toolStripStatusLabel2.Text = allSum.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Очистити форму?", "Новий клієнт", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Controls.Clear();
                InitializeComponent();
                SetBindings();
                timer1.Stop();
            }
        }
    }
}
