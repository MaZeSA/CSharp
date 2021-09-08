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
    
        Child ActivForm { get => ActiveMdiChild as Child; }

        void Loading()
        {
            InitializeComponent();

            for (int i = 2; i <= 80; i += 2)
            {
                toolStripComboBox1.Items.Add(i);
            }
            toolStripComboBox1.Text = "12";

            foreach (var f in FontFamily.Families)
            {
                toolStripComboBox2.Items.Add(f.Name);
            }

            OpenNewWindow();
        }

        void OpenNewWindow()
        {
            CreateNewChild(new Child());
        }
        void OpenNewWindow(string path)
        {
            CreateNewChild(new Child(path));
        }


        int id = 1;
        void CreateNewChild(Child newChild)
        {
            if (MdiChildren.Length == 0)
            {
                windowToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
            }

            newChild.MdiParent = this;
            newChild.Text = newChild.Text + " Form - " + id++.ToString();
            
            ToolStripMenuItem newItem = new ToolStripMenuItem(); // створили пункт меню
            newItem.Text = newChild.Text;
         
            newItem.Click += (o, s) =>
            {
                newChild.Focus();
                if (newChild.WindowState == FormWindowState.Minimized)
                    newChild.WindowState = FormWindowState.Normal;
            };

            newChild.FormClosed += (o, s) =>
             {
                 windowToolStripMenuItem.DropDownItems.Remove(newItem);
                 if (MdiChildren.Length == 1)
                 {
                     windowToolStripMenuItem.DropDownItems.RemoveAt(windowToolStripMenuItem.DropDownItems.Count - 1);
                 }
             };
            newChild.Activated += (o, s) =>
                    {
                        foreach (var item in windowToolStripMenuItem.DropDownItems)
                        {
                            if (item is ToolStripMenuItem)
                                (item as ToolStripMenuItem).Checked = false;
                        }
                        newItem.Checked = true;
                    };

            windowToolStripMenuItem.DropDownItems.Add(newItem); // додали щойно створений пункт
            
            newChild.Show();
        }

        private void NewDocToolSMI_Click(object sender, EventArgs e)
        {
            OpenNewWindow();
        }
  
        private void OpenDocToolSMI_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openDialog.Filter = "All files|*.*|Text documents|*.txt|RTF|*.rtf";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                OpenNewWindow(openDialog.FileName);
            }
        }

        private void SaveDocToolSMI_Click(object sender, EventArgs e)
        {
           ActivForm.Save(ActivForm.saveFileDefaultDialog);
        }

        private void SaveAsDocToolSMI_Click(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = "Text|*.txt|Source Code|*.cs|HyperText Markup Language|*.html|Rich Text Format|*.rtf",
                AddExtension = true,
                DefaultExt = ".txt",
                FileName = "Text.txt"
            };

            ActivForm.Save(saveDialog);
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
                if (ActivForm.richTextBox.SelectedText.Length > 0)
                {
                    ActivForm.richTextBox.SelectionFont = fontDialog.Font;
                    ActivForm.richTextBox.SelectionColor = fontDialog.Color;
                }
                else
                {
                    ActivForm.richTextBox.Font = fontDialog.Font;
                    ActivForm.richTextBox.ForeColor = fontDialog.Color;
                }
            }
        }

        private void FontStyleToolStrip_Click(object sender, EventArgs e)
        {
            this.SetFontStyle((FontStyle)Enum.Parse(typeof(FontStyle), (sender as ToolStripButton).Tag as string));
        }
       
        void SetFontStyle(FontStyle fontStyle)
        {
            if (ActivForm.richTextBox.SelectedText.Length > 0)
            {
                if (ActivForm.richTextBox.SelectionFont.Style.HasFlag(fontStyle))
                {
                    ActivForm.richTextBox.SelectionFont = new Font(ActivForm.richTextBox.SelectionFont, ActivForm.richTextBox.SelectionFont.Style & ~fontStyle);
                }
                else
                {
                    ActivForm.richTextBox.SelectionFont = new Font(ActivForm.richTextBox.SelectionFont, ActivForm.richTextBox.SelectionFont.Style | fontStyle);
                }
            }
            else
            {
                if (ActivForm.richTextBox.SelectionFont.Style == fontStyle)
                {
                    ActivForm.richTextBox.Font = new Font(ActivForm.richTextBox.Font, ActivForm.richTextBox.SelectionFont.Style & ~fontStyle);
                }
                else
                {
                    ActivForm.richTextBox.Font = new Font(ActivForm.richTextBox.Font, fontStyle);
                }
            }
        }

        private void AlignmentToolStrip_Click(object sender, EventArgs e)
        {

            ActivForm.richTextBox.SelectionAlignment = (HorizontalAlignment)Enum.Parse(typeof(HorizontalAlignment), (sender as ToolStripButton).Text);
        }

        private void Past_Click(object sender, EventArgs e)
        {
            ActivForm.richTextBox.SelectedText = Clipboard.GetText();
        }

        private void Cut_Click(object sender, EventArgs e)
        {
            if (ActivForm.richTextBox.SelectedText.Length == 0)
            {
                Clipboard.SetText(ActivForm.richTextBox.Text);
                ActivForm.richTextBox.Text = "";
            }
            else
            {
                Clipboard.SetText(ActivForm.richTextBox.SelectedText);
                ActivForm.richTextBox.SelectedText = "";
            }
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            if (ActivForm.richTextBox.SelectedText.Length == 0)
            {
                Clipboard.SetText(ActivForm.richTextBox.Text);
            }
            else
            {
                Clipboard.SetText(ActivForm.richTextBox.SelectedText);
            }
        }

        private void SelectAll_Click(object sender, EventArgs e)
        {
            ActivForm.richTextBox.SelectAll();
        }
     
        private void CleanAll_Click(object sender, EventArgs e)
        {
            ActivForm.richTextBox.Text = "";
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
            if (ActivForm is null)
                return;

            if (ActivForm.richTextBox.SelectedText.Length > 0)
            {
                ActivForm.richTextBox.SelectionFont = new Font(ActivForm.richTextBox.SelectionFont.FontFamily, float.Parse(toolStripComboBox1.Text), ActivForm.richTextBox.SelectionFont.Style);
            }
            else
            {
                ActivForm.richTextBox.Font = new Font(ActivForm.richTextBox.Font.FontFamily, float.Parse(toolStripComboBox1.Text), ActivForm.richTextBox.Font.Style);
            }
        }

        private void SetBackColorToolStrip_Click(object sender, EventArgs e)
        {
            var color = new ColorDialog();

            if (color.ShowDialog() == DialogResult.OK)
            {
                ActivForm.richTextBox.SelectionBackColor = color.Color;
            }
            else
            {
                ActivForm.richTextBox.BackColor = color.Color;
            }
        }

        private void SetFontColorToolStrip_Click(object sender, EventArgs e)
        {
            var color = new ColorDialog();

            if (color.ShowDialog() == DialogResult.OK)
            {
                ActivForm.richTextBox.SelectionColor = color.Color;
            }
            else
            {
                ActivForm.richTextBox.ForeColor = color.Color;
            }
        }

        private void SetBulletToolStrip_Click(object sender, EventArgs e)
        {
            ActivForm.richTextBox.SelectionBullet = !ActivForm.richTextBox.SelectionBullet;
        }

        private void FontFamilyComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActivForm is null)
                return;

            if (ActivForm.richTextBox.SelectedText.Length > 0)
            {
                ActivForm.richTextBox.SelectionFont = new Font(new FontFamily(toolStripComboBox2.Text), ActivForm.richTextBox.SelectionFont.Size, ActivForm.richTextBox.SelectionFont.Style);
            }
            else
            {
                ActivForm.richTextBox.Font = new Font(new FontFamily(toolStripComboBox2.Text), ActivForm.richTextBox.Font.Size, ActivForm.richTextBox.Font.Style);
            }
        }

        private void LayoutMdiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi((MdiLayout)Enum.Parse(typeof(MdiLayout), (sender as ToolStripMenuItem).Tag as string));
        }
        private void AllWindowStateAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in this.MdiChildren)
            {
                item.WindowState = (FormWindowState)Enum.Parse(typeof(FormWindowState), (sender as ToolStripMenuItem).Tag as string);
            }
        }

        private void Form1_MdiChildActivate(object sender, EventArgs e)
        {
            toolStripComboBox2.Text = ActivForm?.richTextBox.Font.Name;
        }
    }
}
