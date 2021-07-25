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

namespace CreateFile
{
    public partial class Form1 : Form
    {
        string pathToDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); 

        public Form1()
        {
            InitializeComponent();
            LoadFile();
        }

        void LoadFile()
        {
            var dirInfo = new DirectoryInfo(pathToDesktop);
            var files = dirInfo.GetFiles();
            listBox1.DataSource = files;
            listBox1.ValueMember = "FullName";
            listBox1.DisplayMember = "Name";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                return;
            }
            textBox1.Text = Path.GetFileNameWithoutExtension((listBox1.SelectedItem as FileInfo).FullName);
            textBox2.Text = (listBox1.SelectedItem as FileInfo).Extension; 
            textBox3.Text = ((listBox1.SelectedItem as FileInfo).Length/1024).ToString(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                File.Create(Path.Combine(pathToDesktop, textBox4.Text));
                listBox1.DataSource = null;
                LoadFile();
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}
