using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
    /// Interaktionslogik für AimExercise.xaml
    /// </summary>
    public partial class AimExercise : Window
    {
        public AimExercise()
        {
            InitializeComponent();
        }

        Stopwatch stopwatch = new Stopwatch();


        private async void Start_Click(object sender, RoutedEventArgs e)
        {
            Timers.Visibility = Visibility.Visible;   
            Timers.Text = "3";
            await Task.Delay(1000);
            Timers.Text = "2";
            await Task.Delay(1000);
            Timers.Text = "1";
            await Task.Delay(1000);
            Aim.Visibility = Visibility.Visible;
            stopwatch.Start();
        }

        int counter = 0;

        private void Aim_MouseEnter(object sender, MouseEventArgs e)
        {
            Random number = new Random();
            int size = number.Next(50, 100);
            Aim.Width = size;
            Aim.Height = size;
            int posA = number.Next(-400,400);
            int posB = number.Next(-225, 225);
            Aim.Margin = new Thickness(posA, posB, 0, 0);
            counter++;
            if (counter == 15)
            {
                Aim.Visibility = Visibility.Hidden;
                stopwatch.Stop();
                stopwatch.ToString();
                TImebox.Text = stopwatch.Elapsed.ToString();
            }
        }
    }
}
