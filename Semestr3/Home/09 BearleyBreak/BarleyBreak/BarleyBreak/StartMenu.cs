using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace BarleyBreak
{
    public partial class StartMenu : UserControl, Scale
    {
        public delegate void StartGameHandler(ImageData imageData);
        public event StartGameHandler SelectedImage;

        const string PathColection = "images";
        public StartMenu()
        {
            InitializeComponent();
            Images = new List<Image>();

            LoadColection();

            if (panel1.Controls.Count > 0)
            {
                PictureBox1_Click(panel1.Controls[0], null);
            }
            textBox1.Text = pictureBox1.Width.ToString();
            textBox2.Text = pictureBox1.Height.ToString();

        }

        List<Image> Images { set; get; }

        void LoadColection()
        {
            try
            {
                var paths = Directory.EnumerateFiles(PathColection);
                int left = 3;
                foreach (var item in paths.Where(x => Path.GetExtension(x) == ".jpg" || Path.GetExtension(x) == ".png"))
                {
                    try
                    {
                        var picturBox = new PictureBox { Height = 100, Width = 150, Tag = Path.GetFileNameWithoutExtension(item), Left = left, Top = 10, Margin = new Padding(3, 2, 3, 2), Image = Image.FromFile(item), SizeMode = PictureBoxSizeMode.Zoom };
                        picturBox.Click += PictureBox1_Click;
                        panel1.Controls.Add(picturBox);
                        left += 150;
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = (sender as PictureBox).Image;
            label1.Text = (sender as PictureBox).Tag.ToString();
        }

        void Scale.Resize(Size size)
        {
            this.Size = size;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            pictureBox1.Width = int.Parse(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            pictureBox1.Height = int.Parse(textBox2.Text);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.Refresh();

            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            Pen p = new Pen(Color.Red);

            int temp = pictureBox1.Height / (int)numericUpDown1.Value;
            int point = temp;

            for (int i = 0; i < numericUpDown1.Value - 1; i++)
            {
                var p1 = new Point(0, point);
                var p2 = new Point(pictureBox1.Width, point);
                point += temp;

                g.DrawLine(p, p1, p2);
            }

            temp = pictureBox1.Width / (int)numericUpDown2.Value;
            point = temp;
            for (int i = 0; i < numericUpDown2.Value - 1; i++)
            {
                var p1 = new Point(point, 0);
                var p2 = new Point(point, pictureBox1.Height);
                point += temp;

                g.DrawLine(p, p1, p2);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int y = (int)numericUpDown1.Value;
            int x = (int)numericUpDown2.Value;

            var img = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);

            var imdat = new ImageData
            {
                Name = label1.Text,
                Images = Сutter.Cut(img, x, y),
                OriginalImage = img,
                Height = pictureBox1.Height,
                Width = pictureBox1.Width,
                X = x,
                Y = y
            };

            SelectedImage?.Invoke(imdat);
        }

        private void panel5_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] str = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (Path.GetExtension(str[0]) == ".png" || Path.GetExtension(str[0]) == ".jpg")
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void panel5_DragDrop(object sender, DragEventArgs e)
        {
            string[] str = (string[])e.Data.GetData(DataFormats.FileDrop);
            pictureBox1.Image = Image.FromFile(str[0]);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if(int.Parse((sender as TextBox).Text) < 300)
            {
                (sender as TextBox).Text = (200).ToString();
            }
        }
    }
}
