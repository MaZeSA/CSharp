using EntityForm.BLL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityForm3LayerClient
{
    public partial class UserControlPosition : UserControl
    {
        PositionDTO positionDTO { set; get; }
        PositionDTO.Event Event { set; get; }
        public UserControlPosition(PositionDTO _positionDTO, PositionDTO.Event _event)
        {
            InitializeComponent();

            if (_event== PositionDTO.Event.Add)
            { buttonRemove.Enabled = buttonUpdate.Enabled = false; }
            else
            { buttonAdd.Enabled = false; }

            Event = _event;
            positionDTO = _positionDTO;
            textBox1.Text = _positionDTO?.Title;
        }

        public delegate void EventPsitionHandler(PositionDTO positionDTO , PositionDTO.Event @event);
        public event EventPsitionHandler EventPosition;
        private void button1_Click(object sender, EventArgs e)
        {
            positionDTO.Title = textBox1.Text;
            EventPosition?.Invoke(positionDTO, PositionDTO.Event.Add);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            positionDTO.Title = textBox1.Text;
            EventPosition?.Invoke(positionDTO, PositionDTO.Event.Update);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EventPosition?.Invoke(positionDTO, PositionDTO.Event.Remove);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            //if(textBox1.Text.Length <5 )
            //{
            //    MessageBox.Show("Title minimum 5 length");
            //    textBox1.Focus();
            //}
        }
    }
}
