using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyboardTrainer
{
   public class TrainerUI
    {

      public  Dictionary<Key, KeyboardButton> KeyboardButtons;
       public  void CreateDictionary()
        {
            KeyboardButtons = new Dictionary<Key, KeyboardButton>();
            int width = 50;
            var color1 = new SolidColorBrush(Color.FromRgb(240, 119, 149));
            var color2 = new SolidColorBrush(Color.FromRgb(230, 240, 119));
            var color3 = new SolidColorBrush(Color.FromRgb(127, 240, 119));
            var color4 = new SolidColorBrush(Color.FromRgb(119, 190, 240));
            var color5 = new SolidColorBrush(Color.FromRgb(219, 119, 240)); 
            var color6 = new SolidColorBrush(Color.FromRgb(181, 181, 181));

            KeyboardButtons[Key.Oem3] = new TwoCharKey("`", "~", width, color1, 0);
            KeyboardButtons[Key.D1] = new TwoCharKey("1", "!", width, color1, 0);
            KeyboardButtons[Key.D2] = new TwoCharKey("2", "@", width, color1, 0);
            KeyboardButtons[Key.D3] = new TwoCharKey("3", "#", width, color2, 0);
            KeyboardButtons[Key.D4] = new TwoCharKey("4", "$", width, color3, 0);
            KeyboardButtons[Key.D5] = new TwoCharKey("5", "%", width, color4, 0);
            KeyboardButtons[Key.D6] = new TwoCharKey("6", "^", width, color4, 0);
            KeyboardButtons[Key.D7] = new TwoCharKey("7", "&", width, color5, 0);
            KeyboardButtons[Key.D8] = new TwoCharKey("8", "*", width, color5, 0);
            KeyboardButtons[Key.D9] = new TwoCharKey("9", "(", width, color1, 0);
            KeyboardButtons[Key.D0] = new TwoCharKey("0", ")", width, color2, 0);
            KeyboardButtons[Key.OemMinus] = new TwoCharKey("-", "_", width, color3, 0);
            KeyboardButtons[Key.OemPlus] = new TwoCharKey("=", "+", width, color3, 0);
            KeyboardButtons[Key.Back] = new ControlKey("Backspace", 100, color6, 0);
            KeyboardButtons[Key.Tab] = new ControlKey("Tab", 75, color6, 1);
            KeyboardButtons[Key.Q] = new KeyboardButton("Q", color1, 1);
            KeyboardButtons[Key.W] = new KeyboardButton("W", color2, 1);
            KeyboardButtons[Key.E] = new KeyboardButton("E", color3, 1);
            KeyboardButtons[Key.R] = new KeyboardButton("R", color4, 1);
            KeyboardButtons[Key.T] = new KeyboardButton("T", color4, 1);
            KeyboardButtons[Key.Y] = new KeyboardButton("Y", color5, 1);
            KeyboardButtons[Key.U] = new KeyboardButton("U", color5, 1);
            KeyboardButtons[Key.I] = new KeyboardButton("I", color1, 1);
            KeyboardButtons[Key.O] = new KeyboardButton("O", color2, 1);
            KeyboardButtons[Key.P] = new KeyboardButton("P", color3, 1);
            KeyboardButtons[Key.OemOpenBrackets] = new TwoCharKey("[", "{", width, color3, 1);
            KeyboardButtons[Key.OemCloseBrackets] = new TwoCharKey("]", "}", width,color3, 1);
            KeyboardButtons[Key.Oem5] = new TwoCharKey("\\", "|", width, color3, 1);
            KeyboardButtons[Key.CapsLock] = new ControlKey("Caps Lock", 100, color6, 2);
            KeyboardButtons[Key.A] = new KeyboardButton("A", color1, 2);
            KeyboardButtons[Key.S] = new KeyboardButton("S", color2, 2);
            KeyboardButtons[Key.D] = new KeyboardButton("D", color3, 2);
            KeyboardButtons[Key.F] = new KeyboardButton("F", color4, 2);
            KeyboardButtons[Key.G] = new KeyboardButton("G", color4, 2);
            KeyboardButtons[Key.H] = new KeyboardButton("H", color5, 2);
            KeyboardButtons[Key.J] = new KeyboardButton("J", color5, 2);
            KeyboardButtons[Key.K] = new KeyboardButton("K", color1, 2);
            KeyboardButtons[Key.L] = new KeyboardButton("L", color2, 2);
            KeyboardButtons[Key.OemSemicolon] = new TwoCharKey(";", ":", width, color3, 2);
            KeyboardButtons[Key.OemQuotes] = new TwoCharKey("'", "\"", width, color3, 2);
            KeyboardButtons[Key.Enter] = new ControlKey("Enter", 100, color6, 2);
            KeyboardButtons[Key.LeftShift] = new ControlKey("Shift", 125, color6, 3);
            KeyboardButtons[Key.Z] = new KeyboardButton("Z", color1, 3);
            KeyboardButtons[Key.X] = new KeyboardButton("X", color2, 3);
            KeyboardButtons[Key.C] = new KeyboardButton("C", color3, 3);
            KeyboardButtons[Key.V] = new KeyboardButton("V", color4, 3);
            KeyboardButtons[Key.B] = new KeyboardButton("B", color4, 3);
            KeyboardButtons[Key.N] = new KeyboardButton("N", color5, 3);
            KeyboardButtons[Key.M] = new KeyboardButton("M", color5, 3);
            KeyboardButtons[Key.OemComma] = new TwoCharKey(",", "<", width, color1, 3);
            KeyboardButtons[Key.OemPeriod] = new TwoCharKey(".", ">", width, color2, 3);
            KeyboardButtons[Key.OemQuestion] = new TwoCharKey("/", "?", width, color3, 3);
            KeyboardButtons[Key.RightShift] = new ControlKey("Shift", 125, color6, 3);
            KeyboardButtons[Key.LeftCtrl] = new ControlKey("Ctrl", 75, color6, 4);
            KeyboardButtons[Key.LWin] = new ControlKey("Win", 75, color6, 4);
            KeyboardButtons[Key.LeftAlt] = new ControlKey("Alt", 75, color6, 4);
            KeyboardButtons[Key.Space] = new ControlKey("Space", 294, new SolidColorBrush(Color.FromRgb(229,187,114)), 4);
            KeyboardButtons[Key.RightAlt] = new ControlKey("Alt", 75, color6, 4);
            KeyboardButtons[Key.RWin] = new ControlKey("Win", 75, color6, 4);
            KeyboardButtons[Key.RightCtrl] = new ControlKey("Ctrl", 75, color6, 4);
        }

        bool caps = false;
        public bool CapsLock
        {
            set
            {
                if (value)
                    KeyboardButtons[Key.CapsLock].DownKey();
                else
                    KeyboardButtons[Key.CapsLock].UpKey();

                caps = value;
                Caps();
            }
            get => caps;
        }

        bool shift = false;
        public bool Shift 
        {
            set 
            { 
                shift = value;
                Caps();
            }
        }

        void Caps()
        {
            CapsAllKey(caps ^ shift);
        }

        private void CapsAllKey(bool caps)
        {
            foreach(KeyboardButton keyboard in KeyboardButtons.Values)
            {
                keyboard.Caps(caps);
            }
        }
        public void DownKey(Key key)
        {
            if (!KeyboardButtons.ContainsKey(key))
                return;

            KeyboardButtons[key].DownKey();
        }
        public void UpKey(Key key)
        {
            if (!KeyboardButtons.ContainsKey(key))
                return;

            KeyboardButtons[key].UpKey();
        }


       public const int StringLenght = 60;
        public string Generator(int def, bool sensitive) //47*2 |26|21
        {
            Random random = new Random();

            string ListChars = "";
            var Data = KeyboardButtons.Where(x => x.Value.GetType() == typeof(KeyboardButton)).Select(x => x.Value.Symbol.ToLower()).ToList();

            if (def > 26)
            {
                Data.AddRange(KeyboardButtons.Where(x => x.Value.GetType() == typeof(TwoCharKey)).Select(x => x.Value.Symbol));
            }
            if (def > 47)
            {
                Data.AddRange(KeyboardButtons.Where(x => x.Value.GetType() == typeof(TwoCharKey)).Select(x => x.Value.ShiftSymbol));
            }

            int index;
            for (int i = 0; i < def; i++)
            {
                index = random.Next(0, Data.Count());
                ListChars += Data[index];
                Data.RemoveAt(index);
            }

            string result = "";
            for (int i = 0; i < StringLenght; i++)
            {
                result += ListChars[random.Next(0, ListChars.Length)];
            }

            for (int i = 0; i < StringLenght / random.Next(3, 7); i++)
            {
                index = random.Next(1, result.Length - 1);
                result = result.Insert(index, " ");

                if (result.Length >= index + 1 && sensitive)
                    result = result.Insert(index + 1, result[index + 1].ToString().ToUpper());
            }

            return result.Replace("  ", " ");
        }
    }
}
