using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Menu
{
    /// <summary>
    /// Interaktionslogik für ReactionGame.xaml
    /// </summary>
    public partial class ReactionGame : Window
    {
        MainWindow _mainWindow = null;
        public ReactionGame(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        Color color = new Color();
        Stopwatch stopwatch = new Stopwatch();
        Random random = new Random(Guid.NewGuid().GetHashCode());

        public Color SwitchColor()
        {         
            switch (random.Next(0,7))
            {
                case 0: color = Color.FromRgb(0, 0, 255); break; //blue
                case 1: color = Color.FromRgb(255, 0, 0); break; //red
                case 2: color = Color.FromRgb(255, 128, 0); break; //orange
                case 3: color = Color.FromRgb(255, 255, 0); break; // yellow
                case 4: color = Color.FromRgb(0, 255, 0); break; //green
                case 5: color = Color.FromRgb(0, 255, 255); break; //blue
                case 6: color = Color.FromRgb(127, 0, 255); break; //pink
                case 7: color = Color.FromRgb(96, 96, 96); break; //gray
                default: break;
            }     
            return color;
        }

        async void Start_Click(object sender, RoutedEventArgs e)
        {
            Start.Visibility = Visibility.Hidden;
            bool isGreen = false;
            Color color1 = SwitchColor();
            rightRect.Fill = new LinearGradientBrush(color1, color1, 0);
            while (!isGreen)
            {
                if (color1 == Color.FromRgb(0, 255, 0))
                {
                    rightRect.Fill = new LinearGradientBrush(color1, color1, 0);
                    ReactionCount();
                    isGreen = true;
                }
                else
                {
                    await Task.Delay(1000);
                    color1 = SwitchColor();
                    rightRect.Fill = new LinearGradientBrush(color1, color1, 0);
                    counter++;
                }
            }
        }

        int counter = 0;
        

        public void ReactionCount()
        {
            stopwatch.Start();
        }

        private void ClickHere_Click(object sender, RoutedEventArgs e)
        {
            stopwatch.Stop();
            stopwatch.ToString();
            reactionTime.Content = stopwatch.Elapsed.ToString() + " " + counter;
        }

        private void ReactionGame_IsNotVisible(object sender, DependencyPropertyChangedEventArgs e)
        {
            _mainWindow.Visibility = Visibility.Visible;
        }
    }
}