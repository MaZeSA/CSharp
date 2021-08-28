using System;
using System.Drawing;
using System.Windows.Forms;

namespace BarleyBreak
{
    public partial class GameFild : UserControl, Scale
    {
        public delegate void StopGameHandler();
        public event StopGameHandler StopGame;

        public GameFild()
        {
            InitializeComponent();

        }
        bool startGame = false;
        DateTime StartTime { set; get; }

        public void LoadImage(ImageData imageData)
        {
            tableLayoutPanel1.RowCount = imageData.Y;
            tableLayoutPanel1.ColumnCount = imageData.X;
            pictureBox1.Width = imageData.Width /*+ (imageData.X * 2)*/;
            pictureBox1.Height = imageData.Height /*+ (imageData.Y * 2)*/;
            pictureBox1.Image = imageData.OriginalImage;

            label1.Text = imageData.Name;
            label2.Text = $"({imageData.Y}x{imageData.X})";

            int counter = 1;
            for (int i = 0; i < imageData.Y; i++)
            {
                for (int t = 0; t < imageData.X; t++)
                {
                    var item = new PictureBoxUser(counter++) { Width = imageData.Images[0, 0].Width, Height = imageData.Images[0, 0].Height, Image = imageData.Images[i, t] };
                    item.ChangePicture += Item_ChangePicture;
                    tableLayoutPanel1.Controls.Add(item, t, i);
                }
            }

            timer1.Interval = 700;
            timer1.Start();
            StartTime = DateTime.Now;
        }

        void CreateWhiteItem()
        {
            var item2 = tableLayoutPanel1.GetControlFromPosition(tableLayoutPanel1.ColumnCount-1, tableLayoutPanel1.RowCount-1) as PictureBoxUser;
            item2.SetActivity();
        }

       void Shuffle()
        {
            var rnd = new Random();
            int rand;
            foreach (var t in tableLayoutPanel1.Controls)
            {
                rand = rnd.Next(0, tableLayoutPanel1.Controls.Count);
                (t as PictureBoxUser).ChangeRowColum(tableLayoutPanel1.Controls[rand] as PictureBoxUser);

                System.Threading.Thread.Sleep(20);
            }
        }

        private void Item_ChangePicture()
        {
            if (startGame)
            {
                if ((tableLayoutPanel1.GetControlFromPosition(0, 0) as PictureBoxUser).Id == 1) //для меньшої кількості перевірок
                {
                    int control = 1;
                    for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
                    {
                        for (int t = 0; t < tableLayoutPanel1.ColumnCount; t++)
                        {
                            if (control == (tableLayoutPanel1.GetControlFromPosition(t, i) as PictureBoxUser).Id)
                            { control++; }
                            else
                            { return; }
                        }
                    }
                    Victory();
                }
            }
        }

        void Victory()
        {
            tableLayoutPanel1.Visible = false;
            pictureBox1.Visible = true;
            pictureBox2.Enabled = false;
            
            MessageBox.Show("Ви перемогли!\n " + "Пройшло: " + (DateTime.Now - StartTime ).TotalMinutes + " хвилин","Вітання!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        void Scale.Resize(Size size)
        {
            this.Size = size;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Shuffle();
            CreateWhiteItem();
            startGame = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StopGame?.Invoke();
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
        }
    }
}
