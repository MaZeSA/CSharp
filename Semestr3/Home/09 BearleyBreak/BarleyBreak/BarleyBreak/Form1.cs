using System;
using System.Drawing;
using System.Windows.Forms;

namespace BarleyBreak
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            StartLoad();
        }

        void StartLoad()
        {
            this.Controls.Clear();
            var userControl = new StartMenu();
            userControl.SelectedImage += UserControl_SelectedImage;

            Controls.Add(userControl);

            this.Width += 1; 
            this.Width -= 1;
        }

        private void UserControl_SelectedImage(ImageData imageData)
        {
            this.Controls.Clear();
            var userControl = new GameFild();
            userControl.StopGame += UserControl_StopGame;
            this.Controls.Add(userControl);
            userControl.LoadImage(imageData);

            this.Width -= 1; 
            this.Width += 1;
        }

        private void UserControl_StopGame()
        {
            StartLoad();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            (this.Controls[0] as Scale).Resize(new Size( this.Size.Width - 15, this.Size.Height - 38));
        }
    }
}
