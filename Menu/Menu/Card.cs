using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;

namespace MiniGame_TeamProjekt
{
    public class Card
    {
        private static int ID = 0;
        private int _cardID;
        private int _numberForImage;
        private BitmapImage _bitmapImage;

        public Card (int numberForImage)
        {
            _cardID = ID++;
            _numberForImage = numberForImage;
            BitmapImage bitmapImage = new BitmapImage();
            _bitmapImage = GetImage();
        }

        public int CardId
        {
            get { return _cardID; }
            set { _cardID = value; }
        }
        public int NumberForImage
        {
            get { return _numberForImage; }
            set { _numberForImage = value; }
        }
        public BitmapImage BitmapImage
        {
            get { return _bitmapImage; }
            set { _bitmapImage = value; }
        }

        public BitmapImage GetImage()
        {
            // number from listNumbers -> mainWindow
            switch (_numberForImage)
            {
                case 0:
                    // create image
                    BitmapImage imageHouse = new BitmapImage();
                    imageHouse.BeginInit();
                    imageHouse.UriSource = new Uri(@"C:\Users\lwnzun0\Downloads\01_image-House.jpg");
                    imageHouse.EndInit();
                    return imageHouse;
                case 1:
                    BitmapImage imageFlower = new BitmapImage();
                    imageFlower.BeginInit();
                    imageFlower.UriSource = new Uri(@"C:\Users\lwnzun0\Downloads\02_image-Flower.jpg");
                    imageFlower.EndInit();
                    return imageFlower;
                case 2:
                    BitmapImage imageNotes = new BitmapImage();
                    imageNotes.BeginInit();
                    imageNotes.UriSource = new Uri(@"C:\Users\lwnzun0\Downloads\03_image-Notes.jpg");
                    imageNotes.EndInit();
                    return imageNotes;
                case 3:
                    BitmapImage imageIcecream = new BitmapImage();
                    imageIcecream.BeginInit();
                    imageIcecream.UriSource = new Uri(@"C:\Users\lwnzun0\Downloads\04_image-Icecream.jpg");
                    imageIcecream.EndInit();
                    return imageIcecream;
                case 4:
                    BitmapImage imageCactus = new BitmapImage();
                    imageCactus.BeginInit();
                    imageCactus.UriSource = new Uri(@"C:\Users\lwnzun0\Downloads\05_image-Cactus.png");
                    imageCactus.EndInit();
                    return imageCactus;
                case 5:
                    BitmapImage imageGhost = new BitmapImage();
                    imageGhost.BeginInit();
                    imageGhost.UriSource = new Uri(@"C:\Users\lwnzun0\Downloads\06_image-Ghost.png");
                    imageGhost.EndInit();
                    return imageGhost;
                case 6:
                    BitmapImage imageBird = new BitmapImage();
                    imageBird.BeginInit();
                    imageBird.UriSource = new Uri(@"C:\Users\lwnzun0\Downloads\09_image-Bird.png");
                    imageBird.EndInit();
                    return imageBird;
                case 7:
                    BitmapImage imageDice = new BitmapImage();
                    imageDice.BeginInit();
                    imageDice.UriSource = new Uri(@"C:\Users\lwnzun0\Downloads\10_image-Dice.png");
                    imageDice.EndInit();
                    return imageDice;
                case 8:
                    BitmapImage imageRocket = new BitmapImage();
                    imageRocket.BeginInit();
                    imageRocket.UriSource = new Uri(@"C:\Users\lwnzun0\Downloads\11_image-Rocket.png");
                    imageRocket.EndInit();
                    return imageRocket;
                case 9:
                    BitmapImage imageHeart = new BitmapImage();
                    imageHeart.BeginInit();
                    imageHeart.UriSource = new Uri(@"C:\Users\lwnzun0\Downloads\12_image-Heart.png");
                    imageHeart.EndInit();
                    return imageHeart;
                default:
                    BitmapImage imageHeartTwo = new BitmapImage();
                    imageHeartTwo.BeginInit();
                    imageHeartTwo.UriSource = new Uri(@"C:\Users\lwnzun0\Downloads\12_image-Heart.png");
                    imageHeartTwo.EndInit();
                    return imageHeartTwo;
            }
        }
    }
}
