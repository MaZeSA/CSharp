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
    public partial class Form1 : Form
    {
        public Form1()
        {
            Loading();
        }
    
        void Loading()
        {
            InitializeComponent();
            for (int i = 2; i <= 80; i += 2)
            {
                toolStripComboBox1.Items.Add(i);
            }
            toolStripComboBox1.Text = "12";

            foreach(var f in FontFamily.Families)
            {
               toolStripComboBox2.Items.Add(f.Name);
            }
            toolStripComboBox2.Text = richTextBox1.Font.Name;

        }

        private void NewDocToolSMI_Click(object sender, EventArgs e)
        {
            if (CheckSave())
            {
                this.Controls.Clear();
                Loading();
            }
        }
      
        bool CheckSave()
        {
            if (richTextBox1.Text.Length > 0)
            {
                if ((MessageBox.Show("Save File?", "No entry", MessageBoxButtons.YesNo)) == DialogResult.Yes)
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
      
        bool Save(SaveFileDialog saveDialog)
        {
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(saveDialog.FileName) == ".txt")
                {
                    richTextBox1.SaveFile(saveDialog.FileName, RichTextBoxStreamType.UnicodePlainText);
                }
                else
                {
                    richTextBox1.SaveFile(saveDialog.FileName);
                }
                return true;
            }
            return false;
        }

        private void OpenDocToolSMI_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openDialog.Filter = "All files|*.*|Text documents|*.txt|RTF|*.rtf";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.LoadFile(openDialog.FileName);
            }
        }

        private void SaveDocToolSMI_Click(object sender, EventArgs e)
        {
            Save(saveFileDefaultDialog);
        }

        private void SaveAsDocToolSMI_Click(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = "Text|*.txt|Source Code|*.cs|HyperText Markup Language|*.html|Rich Text Format|*.rtf)",
                AddExtension = true,
                DefaultExt = ".txt",
                FileName = "Text.txt"
            };

            Save(saveDialog);
        }
   
        private void CloseToolSMI_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SelectFontToolStrip_Click(object sender, EventArgs e)
        {
            var fontDialog = new FontDialog();
            fontDialog.ShowColor = true;

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                if (richTextBox1.SelectedText.Length > 0)
                {
                    richTextBox1.SelectionFont = fontDialog.Font;
                    richTextBox1.SelectionColor = fontDialog.Color;
                }
                else
                {
                    richTextBox1.Font = fontDialog.Font;
                    richTextBox1.ForeColor = fontDialog.Color;
                }
            }
        }

        private void ItalicToolStrip_Click(object sender, EventArgs e)
        {
            SetFontStyle(FontStyle.Italic);
        }

        private void BoldToolStrip_Click(object sender, EventArgs e)
        {
            SetFontStyle(FontStyle.Bold);
        }

        private void UnderlineToolStrip_Click(object sender, EventArgs e)
        {
            SetFontStyle(FontStyle.Underline);
        }
       
        void SetFontStyle(FontStyle fontStyle)
        {
            if (richTextBox1.SelectedText.Length > 0)
            {
                if (richTextBox1.SelectionFont.Style.HasFlag(fontStyle))
                {
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style & ~fontStyle);
                }
                else
                {
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style | fontStyle);
                }
            }
            else
            {
                if (richTextBox1.SelectionFont.Style == fontStyle)
                {
                    richTextBox1.Font = new Font(richTextBox1.Font, richTextBox1.SelectionFont.Style & ~fontStyle);
                }
                else
                {
                    richTextBox1.Font = new Font(richTextBox1.Font, fontStyle);
                }
            }
        }

        private void AlignmentLToolStrip_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void AlignmentCentrToolStrip_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void AlignmentRToolStrip_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void Past_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = Clipboard.GetText();
        }

        private void Cut_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length == 0)
            {
                Clipboard.SetText(richTextBox1.Text);
                richTextBox1.Text = "";
            }
            else
            {
                Clipboard.SetText(richTextBox1.SelectedText);
                richTextBox1.SelectedText = "";
            }
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length == 0)
            {
                Clipboard.SetText(richTextBox1.Text);
            }
            else
            {
                Clipboard.SetText(richTextBox1.SelectedText);
            }
        }

        private void SelectAll_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }
     
        private void CleanAll_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }
      
        private void FontSizeUpToolStrip_Click(object sender, EventArgs e)
        {
            toolStripComboBox1.Text = (float.Parse(toolStripComboBox1.Text) + 1).ToString(); 
        }
        
        private void FontSizeDownToolStrip_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox1.Text == "1") return;
            toolStripComboBox1.Text = (float.Parse(toolStripComboBox1.Text) - 1).ToString(); 
        }

        private void ComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    
        private void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length > 0)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, float.Parse(toolStripComboBox1.Text), richTextBox1.SelectionFont.Style);
            }
            else
            {
                richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, float.Parse(toolStripComboBox1.Text), richTextBox1.Font.Style);
            }
        }

        private void SetBackColorToolStrip_Click(object sender, EventArgs e)
        {
            var color = new ColorDialog();

            if (color.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionBackColor = color.Color;
            }
            else
            {
                richTextBox1.BackColor = color.Color;
            }
        }

        private void SetFontColorToolStrip_Click(object sender, EventArgs e)
        {
            var color = new ColorDialog();

            if (color.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = color.Color;
            }
            else
            {
                richTextBox1.ForeColor = color.Color;
            }
        }

        private void SetBulletToolStrip_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionBullet = !richTextBox1.SelectionBullet;
        }

        private void FontFamilyComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length > 0)
            {
                richTextBox1.SelectionFont = new Font(new FontFamily(toolStripComboBox2.Text), richTextBox1.SelectionFont.Size, richTextBox1.SelectionFont.Style);
            }
            else
            {
                richTextBox1.Font = new Font(new FontFamily(toolStripComboBox2.Text), richTextBox1.Font.Size, richTextBox1.Font.Style);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CheckSave())
            {
                e.Cancel = true;
            }
        }
    }
}
