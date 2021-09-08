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

namespace TextEditor
{
    public partial class Child : Form
    {
        public RichTextBox richTextBox { get => richTextBox1; }
        public Child()
        {
            InitializeComponent();
        }
        public Child(string path)
        {
            InitializeComponent();
            LoadDocument(path);
        }

        void LoadDocument(string path)
        {
            try
            {
                RichTextBoxStreamType richTextBoxStreamType = RichTextBoxStreamType.PlainText;
                if (Path.GetExtension(path) == ".rtf")
                    richTextBoxStreamType = RichTextBoxStreamType.RichText;

                richTextBox1.LoadFile(path, richTextBoxStreamType);

                this.Text = Path.GetFileName(path);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


       public bool CheckSave()
        {
            if (richTextBox1.Text.Length > 0)
            {
                if ((MessageBox.Show("Save File? \n"+ this.Text, "No entry", MessageBoxButtons.YesNo)) == DialogResult.Yes)
                {
                    return Save(saveFileDefaultDialog);
                }
                else
                {
                    return true;
                }
            }
            return true;
        }

       public bool Save(SaveFileDialog saveDialog)
        {
            try
            {
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    if (Path.GetExtension(saveDialog.FileName) == ".rtf")
                    {
                        richTextBox1.SaveFile(saveDialog.FileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        richTextBox1.SaveFile(saveDialog.FileName, RichTextBoxStreamType.UnicodePlainText);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        private void Child_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CheckSave())
            {
                e.Cancel = true;
            }
        }
    }
}
