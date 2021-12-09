using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Menu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ColorGame_Click(object sender, RoutedEventArgs e)
        {
            Color_Game colorGame = new Color_Game();
            this.Close();
            colorGame.Show();
        }

        private void Memory_Click(object sender, RoutedEventArgs e)
        {
            //needs Content
        }

        private void ReactionGame_Click(object sender, RoutedEventArgs e)
        {
            //needs Content
        }

        private void AimExercise_Click(object sender, RoutedEventArgs e)
        {
            //needs Content
        }
    }
}
