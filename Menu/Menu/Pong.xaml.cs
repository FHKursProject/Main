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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Menu
{
    /// <summary>
    /// Interaktionslogik für Pong.xaml
    /// </summary>
    public partial class Pong : Window
    {
        private bool P1Up = false;
        private bool P2Up = false;
        private bool P1Down = false;
        private bool P2Down = false;

        private double PositionX = 390;
        private double PositionY = 205;
        private double SpeedX = -0.1;
        private double SpeedY = 0.1;
        private double StartSpeedX = 0.1;
        private double StartSpeedY = 0.1;
        private double acceleration = 1;

        Random random = new Random();
        private int BackBefore = 1;

        DispatcherTimer timerplayers = new DispatcherTimer();
        DispatcherTimer timerball = new DispatcherTimer();
        public Pong()
        {
            InitializeComponent();
            timerplayers.Interval = TimeSpan.FromMilliseconds(5);
            timerplayers.Tick += Moveplayer;
            timerball.Interval = TimeSpan.FromMilliseconds(50);
            timerball.Tick += MoveBall;
        }

        public void Moveplayer(object sender, EventArgs e)
        {
            if (P1Up)
            {
                if (Canvas.GetTop(P1) >= 0)
                {
                    Canvas.SetTop(P1, Canvas.GetTop(P1) - 10);
                }
            }
            if (P1Down)
            {
                if (Canvas.GetTop(P1) <= Gamefield.ActualHeight - 100)
                {
                    Canvas.SetTop(P1, Canvas.GetTop(P1) + 10);
                }
            }
            if (P2Up)
            {
                if (Canvas.GetTop(P2) >= 0)
                {
                    Canvas.SetTop(P2, Canvas.GetTop(P2) - 10);
                }
            }
            if (P2Down)
            {
                if (Canvas.GetTop(P2) <= Gamefield.ActualHeight - 100)
                {
                    Canvas.SetTop(P2, Canvas.GetTop(P2) + 10);
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    P1Up = true;
                    break;
                case Key.S:
                    P1Down = true;
                    break;
                case Key.Up:
                    P2Up = true;
                    break;
                case Key.Down:
                    P2Down = true;
                    break;
                default:
                    break;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    P1Up = false;
                    break;
                case Key.S:
                    P1Down = false;
                    break;
                case Key.Up:
                    P2Up = false;
                    break;
                case Key.Down:
                    P2Down = false;
                    break;
                default:
                    break;
            }
        }

        //Ball
        private void MoveBall(object sender, EventArgs e)
        {
            var xleft = Canvas.GetLeft(Ball);
            var ytop = Canvas.GetTop(Ball);



            //Hit PaddleLeft
            if (xleft <= P1.Width && ytop >= Canvas.GetTop(P1) && ytop + Ball.Height <= Canvas.GetTop(P1) + P1.Height)
            {
                SpeedX = -SpeedX;
            }



            //Hit PaddleRight
            if (xleft + Ball.Width >= Gamefield.ActualWidth - (Canvas.GetRight(P2) + P2.Width) && ytop + P2.Width >= Canvas.GetTop(P2) && ytop + Ball.Height <= Canvas.GetTop(P2) + P2.Height)
            {
                SpeedX = -SpeedX;
            }



            //Hit Wall
            if (ytop >= Gamefield.ActualHeight - Ball.Height || ytop <= 0.0)
            {
                SpeedY = -SpeedY;
            }



            //LeftPlayer makes Goal
            if (xleft >= 10 + Gamefield.ActualWidth)
            {
                SpeedX = StartSpeedX;
                SpeedY = StartSpeedY;
                Canvas.SetLeft(Ball, PositionX);
                Canvas.SetTop(Ball, PositionY);
                PointsP1.Content = Convert.ToInt32(PointsP1.Content) + 1;
                return;
            }



            //RightPlayer makes Goal
            if (xleft <= -10 - Ball.Width)
            {
                SpeedX = StartSpeedX;
                SpeedY = StartSpeedY;
                Canvas.SetLeft(Ball, PositionX);
                Canvas.SetTop(Ball, PositionY);
                PointsP2.Content = Convert.ToInt32(PointsP2.Content) + 1;
                return;
            }



            //Accelerate Ball
            SpeedX *= acceleration;
            SpeedY *= acceleration;



            //Ball Movement
            xleft += SpeedX * timerball.Interval.TotalMilliseconds;
            ytop += SpeedY * timerball.Interval.TotalMilliseconds;
            Canvas.SetLeft(Ball, xleft);
            Canvas.SetTop(Ball, ytop);

            //Win
            if (Convert.ToInt32(PointsP1.Content) == 10)
            {
                MessageBox.Show("Player 1 wins. Player 2 sucks.");
                PointsP1.Content = "0";
                PointsP2.Content = "0";
                Stop();
            }
            if (Convert.ToInt32(PointsP2.Content) == 10)
            {
                MessageBox.Show("Player 2 wins. Player 1 sucks.");
                PointsP1.Content = "0";
                PointsP2.Content = "0";
                Stop();
            }
        }

        private void Easy_Clicked(object sender, RoutedEventArgs e)
        {
            //start pong
            timerplayers.Start();
            timerball.Start();

            //set Speed without acceleration
            StartSpeedX = 0.2;
            StartSpeedY = 0.2;
            acceleration = 1.0;

            //hide buttons
            Easy.Visibility = Visibility.Hidden;
            Medium.Visibility = Visibility.Hidden;
            Hard.Visibility = Visibility.Hidden;
            BackgroundChange.Visibility = Visibility.Hidden;
        }

        private void Medium_Clicked(object sender, RoutedEventArgs e)
        {
            //start pong
            timerplayers.Start();
            timerball.Start();

            //set speed with acceleration
            StartSpeedX = 0.15;
            StartSpeedY = 0.15;
            acceleration = 1.02;

            //hide buttons
            Easy.Visibility = Visibility.Hidden;
            Medium.Visibility = Visibility.Hidden;
            Hard.Visibility = Visibility.Hidden;
            BackgroundChange.Visibility = Visibility.Hidden;
        }

        private void Hard_Clicked(object sender, RoutedEventArgs e)
        {
            //start pong
            timerplayers.Start();
            timerball.Start();

            //set speed with acceleration
            StartSpeedX = 0.20;
            StartSpeedY = 0.20;
            acceleration = 1.03;

            //hide buttons
            Easy.Visibility = Visibility.Hidden;
            Medium.Visibility = Visibility.Hidden;
            Hard.Visibility = Visibility.Hidden;
            BackgroundChange.Visibility = Visibility.Hidden;
        }

        private void ChangeBackground(object sender, RoutedEventArgs e)
        {
            int rand = random.Next(0, 4);

            switch (rand)
            {
                case 0:
                    ImageBrush wall = new ImageBrush();
                    wall.ImageSource = new BitmapImage(new Uri(@"C:\\Users\FLATLEO\OneDrive - Hilti\Pictures\Wall.jpg"));
                    Gamefield.Background = wall;
                    ImageBrush ball = new ImageBrush();
                    ball.ImageSource = new BitmapImage(new Uri(@"C:\\Users\FLATLEO\OneDrive - Hilti\Pictures\Maurerkelle.jpg"));
                    Ball.Fill = ball;
                    break;
                case 1:
                    ImageBrush space = new ImageBrush();
                    space.ImageSource = new BitmapImage(new Uri(@"C:\\Users\FLATLEO\OneDrive - Hilti\Pictures\SpaceBackground.jpg"));
                    Gamefield.Background = space;
                    ImageBrush moon = new ImageBrush();
                    moon.ImageSource = new BitmapImage(new Uri(@"C:\\Users\FLATLEO\OneDrive - Hilti\Pictures\Moon.jpg"));
                    Ball.Fill = moon;
                    break;
                case 2:
                    ImageBrush cats = new ImageBrush();
                    cats.ImageSource = new BitmapImage(new Uri(@"C:\\Users\FLATLEO\OneDrive - Hilti\Pictures\Cats.jpg"));
                    Gamefield.Background = cats;
                    ImageBrush mouse = new ImageBrush();
                    mouse.ImageSource = new BitmapImage(new Uri(@"C:\\Users\FLATLEO\OneDrive - Hilti\Pictures\mouse.jpg"));
                    Ball.Fill = mouse;
                    break;
                case 3:
                    ImageBrush cat = new ImageBrush();
                    cat.ImageSource = new BitmapImage(new Uri(@"C:\\Users\FLATLEO\OneDrive - Hilti\Pictures\Cat.jpg"));
                    Gamefield.Background = cat;
                    ImageBrush borat = new ImageBrush();
                    borat.ImageSource = new BitmapImage(new Uri(@"C:\\Users\FLATLEO\OneDrive - Hilti\Pictures\Borat.jpeg"));
                    Ball.Fill = borat;
                    break;
                default:
                    break;
            }
        }

        public void Stop()
        {
            timerplayers.Stop();
            timerball.Stop();
            Easy.Visibility = Visibility.Visible;
            Medium.Visibility = Visibility.Visible;
            Hard.Visibility = Visibility.Visible;
            BackgroundChange.Visibility = Visibility.Visible;
        }
    }
}
