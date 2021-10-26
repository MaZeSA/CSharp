using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyboardTrainer
{
    internal sealed partial class MainWindow : Window
    {
        TrainerUI TrainerUI { set; get; }
        public MainWindow()
        {
            InitializeComponent();
            TrainerUI = new TrainerUI();
            TrainerUI.CreateDictionary();
           
            foreach(var keyboardButton in TrainerUI.KeyboardButtons)
            {
                ((stackParent as StackPanel).Children[keyboardButton.Value.RowNumer] as StackPanel).Children.Add(keyboardButton.Value);
            }

            TrainerUI.CapsLock = Keyboard.GetKeyStates(Key.CapsLock) == KeyStates.Toggled;
            diffSlider.Focus();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.System)
                TrainerUI.DownKey(e.SystemKey);
            else 
                TrainerUI.DownKey(e.Key);

            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
                TrainerUI.Shift = true;
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.System)
                TrainerUI.UpKey(e.SystemKey);
            else
            TrainerUI.UpKey(e.Key);

            if (e.Key == Key.CapsLock)
                TrainerUI.CapsLock = !TrainerUI.CapsLock;

            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
                TrainerUI.Shift = false;
        }

        int counter = 0;
        int corectCounter = 0; 
        private void Window_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (controlDockPanel.IsEnabled) return;

            var r = new Run();
            r.Text = e.Text;
            r.Background = Brushes.LightBlue;

            if (e.Text != uptextBlock.Text[counter].ToString())
            {
                r.Background = Brushes.Gray;
                failTextBlock.Text = (int.Parse(failTextBlock.Text) + 1).ToString();
            }
            else
                corectCounter++;

            textInput.Inlines.Add(r);

            counter++;
            speedTextBlock.Text = Math.Round(corectCounter / (DateTime.Now - startTime).TotalMinutes).ToString();

            if(counter == TrainerUI.StringLenght)
            {
                MessageBox.Show($"Result: \nSpeed: {speedTextBlock.Text} chars/min\nFails: {failTextBlock.Text}");
                controlDockPanel.IsEnabled = true;
            }
        }

        DateTime startTime;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            controlDockPanel.IsEnabled = false;
               startTime = DateTime.Now;
            corectCounter = counter = 0;

            speedTextBlock.Text = failTextBlock.Text = "0";
            textInput.Text = "";
            uptextBlock.Text = TrainerUI.Generator((int)diffSlider.Value, sensCheckBox.IsEnabled);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            controlDockPanel.IsEnabled = true;
        }
    }
}