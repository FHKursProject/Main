using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Menu
{
    /// <summary>
    /// Interaction logic for Color_Game.xaml
    /// </summary>
    public partial class Color_Game : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private int _points = 0;

        public Color_Game()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer;
            timer.Start();

            NextRound();
        }

        private void Timer(object sender, EventArgs args)
        {
            if (int.Parse(Time.Text) != 0)
            {
                Time.Text = (int.Parse(Time.Text) - 1).ToString();
            }
            else
            {
                EndGame();
                timer.Stop();
            }
        }

        private void EndGame()
        {
            MessageBox.Show("Ihre erreichte Punktzahl " + Points.Text, "Game Stopped!", MessageBoxButton.OK, MessageBoxImage.Information);
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private Color GetRandomizedColor()
        {
            Random randomizer = new Random();
            int randomIndex = randomizer.Next(1, 6);

            switch (randomIndex)
            {
                case 1:
                    return Colors.Red;
                case 2:
                    return Colors.Green;
                case 3:
                    return Colors.Blue;
                case 4:
                    return Colors.Yellow;
                case 5:
                    return Colors.Purple;
                case 6:
                    return Colors.White;
                default:
                    goto case 1;
            }
        }

        private string GetColorName(Brush brush)
        {
            SolidColorBrush foreground = (SolidColorBrush)brush;

            if (foreground.Color == Colors.Red)
            {
                return "Red";
            }
            else if (foreground.Color == Colors.Green)
            {
                return "Green";
            }
            else if (foreground.Color == Colors.Blue)
            {
                return "Blue";
            }
            else if (foreground.Color == Colors.Yellow)
            {
                return "Yellow";
            }
            else if (foreground.Color == Colors.Purple)
            {
                return "Purple";
            }
            else if (foreground.Color == Colors.White)
            {
                return "White";
            }
            else
            {
                throw new Exception("Color could not be definded.");
            }
        }

        private void ChangeColors(Button button)
        {
            button.Foreground = new SolidColorBrush(GetRandomizedColor());
        }

        private string RandomizeColorNames()
        {
            Random randomizer = new Random();
            int randomIndex = randomizer.Next(1, 6);

            switch (randomIndex)
            {
                case 1:
                    return "Red";
                case 2:
                    return "Blue";
                case 3:
                    return "Green";
                case 4:
                    return "Yellow";
                case 5:
                    return "Purple";
                case 6:
                    return "White";
                default:
                    goto case 1;
            }
        }

        private void NextRound()
        {
            ChangeColors(ColorPicker);
            ColorPicker.Content = RandomizeColorNames();
            ChangeColors(ColorGuesser1);
            ColorGuesser1.Content = RandomizeColorNames();
            ChangeColors(ColorGuesser2);
            ColorGuesser2.Content = RandomizeColorNames();
            ChangeColors(ColorGuesser3);
            ColorGuesser3.Content = RandomizeColorNames();
            ChangeColors(ColorGuesser4);
            ColorGuesser4.Content = RandomizeColorNames();
            ChangeColors(ColorGuesser5);
            ColorGuesser5.Content = RandomizeColorNames();
            ChangeColors(ColorGuesser6);
            ColorGuesser6.Content = RandomizeColorNames();
            AddToPoints();
        }

        private void AddToPoints()
        {
            Points.Text = "P: " + _points.ToString();
            _points++;
        }

        private bool ColorAndNameIsMatching(string colorNameButton)
        {
            if (colorNameButton == GetColorName(ColorPicker.Foreground))
            {
                return true;
            }
            return false;
        }

        private void ColorGuesser1_Click(object sender, RoutedEventArgs e)
        {
            if (ColorAndNameIsMatching(ColorGuesser1.Content.ToString()))
            {
                NextRound();
            }
        }

        private void ColorGuesser4_Click(object sender, RoutedEventArgs e)
        {
            if (ColorAndNameIsMatching(ColorGuesser4.Content.ToString()))
            {
                NextRound();
            }
        }

        private void ColorGuesser2_Click(object sender, RoutedEventArgs e)
        {
            if (ColorAndNameIsMatching(ColorGuesser2.Content.ToString()))
            {
                NextRound();
            }
        }

        private void ColorGuesser5_Click(object sender, RoutedEventArgs e)
        {
            if (ColorAndNameIsMatching(ColorGuesser5.Content.ToString()))
            {
                NextRound();
            }
        }

        private void ColorGuesser3_Click(object sender, RoutedEventArgs e)
        {
            if (ColorAndNameIsMatching(ColorGuesser3.Content.ToString()))
            {
                NextRound();
            }
        }

        private void ColorGuesser6_Click(object sender, RoutedEventArgs e)
        {
            if (ColorAndNameIsMatching(ColorGuesser6.Content.ToString()))
            {
                NextRound();
            }
        }

        private void ColorNotHere_Click(object sender, RoutedEventArgs e)
        {
            if (!ColorAndNameIsMatching(ColorGuesser1.Content.ToString()) && !ColorAndNameIsMatching(ColorGuesser2.Content.ToString()) && !ColorAndNameIsMatching(ColorGuesser3.Content.ToString()) && !ColorAndNameIsMatching(ColorGuesser4.Content.ToString()) && !ColorAndNameIsMatching(ColorGuesser5.Content.ToString()) && !ColorAndNameIsMatching(ColorGuesser6.Content.ToString()))
            {
                NextRound();
            }
        }
    }
}