using System;
using System.Drawing;
using System.Windows.Forms;

namespace BarleyBreak
{
    public partial class PictureBoxUser : UserControl
    {
        public delegate void ChangePictureHandler();
        public event ChangePictureHandler ChangePicture;
        public PictureBoxUser(int id)
        {
            InitializeComponent();
            Id = id;
         }

        public Image Image { set => pictureBox1.Image = value; get => pictureBox1.Image; }
        public int Id { get; }
        public int Row { set => (this.Parent as TableLayoutPanel).SetRow(this, value); get => (this.Parent as TableLayoutPanel).GetRow(this); }
        public int Colum { set => (this.Parent as TableLayoutPanel).SetColumn(this, value); get => (this.Parent as TableLayoutPanel).GetColumn(this); }
        new public bool AllowDrop { set => pictureBox1.AllowDrop = value; get => pictureBox1.AllowDrop; }

        public void SetActivity()
        {
            AllowDrop = true;
            pictureBox1.BackColor = Color.LawnGreen;
            Image = null; pictureBox1.MouseDown -= pictureBox1_MouseDown;
        }

        public void ChangeRowColum(PictureBoxUser item1)
        {
            int r = Row, c = Colum;

            Row = item1.Row;
            Colum = item1.Colum;

            item1.Row = r;
            item1.Colum = c;
            
            ChangePicture?.Invoke();
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            PictureBoxUser pictureBoxUser = e.Data.GetData(typeof(PictureBoxUser)) as PictureBoxUser;

            if ((pictureBoxUser.Row != Row && pictureBoxUser.Colum != Colum) || (Math.Abs(Colum - pictureBoxUser.Colum) > 1 || Math.Abs(Row - pictureBoxUser.Row) > 1))
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            ChangeRowColum(e.Data.GetData(typeof(PictureBoxUser)) as PictureBoxUser);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.DoDragDrop(this, DragDropEffects.Move);
        }
    }
}
