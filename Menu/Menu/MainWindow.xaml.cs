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
using System.Threading;
using System.Windows.Threading;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace MiniGame_TeamProjekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Card> listCards;
        private List<Image> listImage;
        private List<Button> listButton;
        private List<Card> openCardList;
        private Card _firstCard = null;
        private Card _secondCard = null;
        private Button _firstButton = null;
        private Button _secondButton = null;
        private static int _matchCount = 0;
        private bool _playerOneRN = true;
        private static int _pointsPlOne = 0;
        private static int _pointsPlTwo = 0;
        private static int _aiPlayer = 1; // 0 -> ai isn't playing, 1 -> ai is playing

        public bool PlayerOne
        {
            get { 
                if (_playerOneRN)
                {
                    txt_player.Text = "Player 1";
                }
                else { txt_player.Text = "Player 2"; }
                return _playerOneRN;
            }
            set {
                _playerOneRN = value;
                if (_playerOneRN)
                {
                    txt_player.Text = "Player 1";
                }
                else { txt_player.Text = "Player 2"; }
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            listButton = new List<Button> {
                btn_01Card, btn_02Card, btn_03Card, btn_04Card, btn_05Card, btn_06Card, btn_07Card, btn_08Card, btn_09Card, btn_10Card,
                btn_11Card, btn_12Card, btn_13Card, btn_14Card, btn_15Card, btn_16Card, btn_17Card, btn_18Card, btn_19Card, btn_20Card
            };

            // each number represents an image which cen be found in funktion -> GetImage
            List<int> listNumbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            // so that we can give them a new image (its easier then if we wrote everything for each image) -> two buttons will have the same image
            listImage = new List<Image> { 
                img_01Card, img_02Card, img_03Card, img_04Card, img_05Card, img_06Card, img_07Card, img_08Card, img_09Card, img_10Card,
                img_11Card, img_12Card, img_13Card, img_14Card, img_15Card, img_16Card, img_17Card, img_18Card, img_19Card, img_20Card 
            };
            listCards = new List<Card>();

            if (_aiPlayer == 1) {
                openCardList = new List<Card>();
            }

            Random random = new Random();
            // to get an index for listImage
            int imageCount = 0;

            do {
                int index = random.Next(0, listNumbers.Count - 1); // get a random index of listNumbers 
                int numberForImage = listNumbers[index]; // get number/ value out of listNumbers
                Image image = listImage[imageCount]; // take next image out of listImage
                
                Card card = new Card(numberForImage);
                listCards.Add(card);
                image.Source = card.BitmapImage; // take image from card and add
                
                listNumbers.Remove(numberForImage); // so it doesnt get used again
                imageCount++; // add to get next image in listImage
                
            } while (imageCount < 20); // when the last image was sorted, stop
        }

        public bool IsThisSecondCard(Card card) // -> for AreCardsEqual, gets card from btn_00Card_Click
        {
            // save card to the right place
            if (_firstCard == null)
            {
                _firstCard = card;
                return false;
            }
            else {
                _secondCard = card;
                return true;
            }
        }

        public void SaveClickedButton(Button button) // -> for AreCardsEqual, gets button from btn_00Card_Click
        {
            if (_firstButton == null)
            {
                _firstButton = button;
            }
            else { _secondButton = button; }
        }

        public async void AreCardsEqual() // -> compare cards and hide them
        {
            // enable all buttons
            foreach (Button button in listButton) {
                button.IsEnabled = false;
            }

            await Task.Delay(1000);
             
            if (_firstButton == null || _secondButton == null)
            {
                string message = "One of the cards is null";
                MessageBox.Show(message, "Error", MessageBoxButton.OK);
            }
            else if (_firstCard.NumberForImage == _secondCard.NumberForImage && _firstCard.CardId != _secondCard.CardId)
            {
                _matchCount += 1;

                // hide all cards
                foreach (Image image in listImage) 
                {
                    image.Visibility = Visibility.Hidden;
                }

                // hide button with both cards
                _firstButton.Visibility = Visibility.Hidden;
                _secondButton.Visibility = Visibility.Hidden;

                // who got the point
                if (PlayerOne)
                {
                    _pointsPlOne++;
                    txt_playerOne.Text = Convert.ToString(_pointsPlOne);
                }
                else{
                    _pointsPlTwo++;
                    txt_playerTwo.Text = Convert.ToString(_pointsPlTwo);
                }

                // remove cards from list
                Card c3 = null;
                Card c4 = null;

                foreach (Card removeCard in openCardList)
                {
                    if (removeCard.CardId == _firstCard.CardId)
                    {
                        c3 = removeCard;
                    }
                    else if (removeCard.CardId == _secondCard.CardId)
                    {
                        c4 = removeCard;
                    }
                }
                openCardList.Remove(c3);
                openCardList.Remove(c4);

                // remove matched buttons, cards, items from their lists
                int index1 = listButton.IndexOf(_firstButton);
                int index2 = listButton.IndexOf(_secondButton);
                Button b1 = listButton[index1];
                Button b2 = listButton[index2];
                listButton.Remove(b1);
                listButton.Remove(b2);
                Card c1 = listCards[index1];
                Card c2 = listCards[index2];
                listCards.Remove(c1);
                listCards.Remove(c2);
                Image i1 = listImage[index1];
                Image i2 = listImage[index2];
                listImage.Remove(i1);
                listImage.Remove(i2);
            }
            else {
                
                // hide all cards/ images
                foreach (Image image in listImage) {
                    image.Visibility = Visibility.Hidden;
                }

                // which player is next
                if (PlayerOne)
                {
                    PlayerOne = false;
                }
                else { PlayerOne = true; }
            }

            // reset everything
            _firstButton = null;
            _firstCard = null;
            _secondButton = null;
            _secondCard = null;

            foreach (Button button in listButton)
            {
                button.IsEnabled = true;
            }

            // for match to be over
            if (_matchCount == 10)
            {
                string message;
                if (_pointsPlOne < _pointsPlTwo)
                {
                    message = "Player Two has won!";
                }
                else if (_pointsPlTwo < _pointsPlOne)
                {
                    message = "Player One has won!";
                }
                else {
                    message = "There's been a tie!";
                }
                MessageBox.Show("The game has ended!\n" + message, "Finished");
                this.Close();
            }
            else {
                // who playes second player
                if (_aiPlayer == 1)
                {
                    if (PlayerOne == false)
                    {
                        ComputerPlayer();
                    }
                }
            }
        }

        private Card GetCard(int idNumberOfCard) // -> to get the right card for a button
        {
            foreach (Card card in listCards)
            {
                if (card.CardId == idNumberOfCard)
                {
                    return card;
                }
            }
            return null;
        }

        public /*async*/ void ComputerPlayer()
        {
            bool playerHasCards = false;
            Random random = new Random();
            int firstRandom = 0;
            int secondRandom = 1;
            Card card02 = null;
            
            // first button
            firstRandom = random.Next(0, listButton.Count - 1);
            _firstButton = listButton[firstRandom];
            Card card01 = listCards[firstRandom];
            ComputerPlayerClicked(_firstButton, card01);
            if (!openCardList.Contains(card01))
            {
                openCardList.Add(card01);
            }

            // find matching card
            foreach (Card card in openCardList)
            {
                if (card.CardId != card01.CardId && card.NumberForImage == card01.NumberForImage)
                {
                    _secondButton = listButton[listCards.IndexOf(card)];
                    ComputerPlayerClicked(_secondButton, card);
                    playerHasCards = true;
                    break;
                }
            }

            // or random choose random card
            if (playerHasCards == false)
            {
                int number = 0;
                do {
                    secondRandom = random.Next(0, listButton.Count - 1);
                    if (listButton.Count == 2)
                    {
                        secondRandom = number++;
                    }
                    card02 = listCards[secondRandom];

                } while (card02.CardId == card01.CardId);

                _secondButton = listButton[secondRandom];
                
                ComputerPlayerClicked(_secondButton, card02);
                if (!openCardList.Contains(card02))
                {
                    openCardList.Add(card02);
                }
            }
        }
        private void ComputerPlayerClicked(Button button, Card card)
        {
            button.IsEnabled = false;
            int buttonIndex = listButton.IndexOf(button);
            Image image = listImage[buttonIndex];
            image.Visibility = Visibility.Visible;     
            SaveClickedButton(button);               
            Card myCard = GetCard(card.CardId);               
            bool isItSecondCard = IsThisSecondCard(myCard); 
            if (isItSecondCard)                            
            {
                AreCardsEqual();
            }
        }

        private void btn_01Card_Click(object sender, RoutedEventArgs e)
        {
            btn_01Card.IsEnabled = false;
            img_01Card.Visibility = Visibility.Visible;     // Visible
            SaveClickedButton(btn_01Card);                  // save button 
            Card myCard = GetCard(0);                       // get card
            bool isItSecondCard = IsThisSecondCard(myCard); // first or second car
            if (isItSecondCard)                             // if second
            {
                AreCardsEqual();
            }
        }
        private void btn_02Card_Click(object sender, RoutedEventArgs e)
        {
            btn_02Card.IsEnabled = false;
            img_02Card.Visibility = Visibility.Visible;
            SaveClickedButton(btn_02Card);
            Card myCard = GetCard(1);
            bool isItSecondCard = IsThisSecondCard(myCard);
            if (isItSecondCard)
            {
                AreCardsEqual();
            }
        }
        private void btn_03Card_Click(object sender, RoutedEventArgs e)
        {
            btn_03Card.IsEnabled = false;
            img_03Card.Visibility = Visibility.Visible;
            SaveClickedButton(btn_03Card);
            Card myCard = GetCard(2);
            bool isItSecondCard = IsThisSecondCard(myCard);
            if (isItSecondCard)
            {
                AreCardsEqual();
            }
        }
        private void btn_04Card_Click(object sender, RoutedEventArgs e)
        {
            btn_04Card.IsEnabled = false;
            img_04Card.Visibility = Visibility.Visible;
            SaveClickedButton(btn_04Card);
            Card myCard = GetCard(3);
            bool isItSecondCard = IsThisSecondCard(myCard);
            if (isItSecondCard)
            {
                AreCardsEqual();
            }
        }
        private void btn_05Card_Click(object sender, RoutedEventArgs e)
        {
            btn_05Card.IsEnabled = false;
            img_05Card.Visibility = Visibility.Visible;
            SaveClickedButton(btn_05Card);
            Card myCard = GetCard(4);
            bool isItSecondCard = IsThisSecondCard(myCard);
            if (isItSecondCard)
            {
                AreCardsEqual();
            }
        }
        private void btn_06Card_Click(object sender, RoutedEventArgs e)
        {
            btn_06Card.IsEnabled = false;
            img_06Card.Visibility = Visibility.Visible;
            SaveClickedButton(btn_06Card);
            Card myCard = GetCard(5);
            bool isItSecondCard = IsThisSecondCard(myCard);
            if (isItSecondCard)
            {
                AreCardsEqual();
            }
        }
        private void btn_07Card_Click(object sender, RoutedEventArgs e)
        {
            btn_07Card.IsEnabled = false;
            img_07Card.Visibility = Visibility.Visible;
            SaveClickedButton(btn_07Card);
            Card myCard = GetCard(6);
            bool isItSecondCard = IsThisSecondCard(myCard);
            if (isItSecondCard)
            {
                AreCardsEqual();
            }
        }
        private void btn_08Card_Click(object sender, RoutedEventArgs e)
        {
            btn_08Card.IsEnabled = false;
            img_08Card.Visibility = Visibility.Visible;
            SaveClickedButton(btn_08Card);
            Card myCard = GetCard(7);
            bool isItSecondCard = IsThisSecondCard(myCard);
            if (isItSecondCard)
            {
                AreCardsEqual();
            }
        }
        private void btn_09Card_Click(object sender, RoutedEventArgs e)
        {
            btn_09Card.IsEnabled = false;
            img_09Card.Visibility = Visibility.Visible;
            SaveClickedButton(btn_09Card);
            Card myCard = GetCard(8);
            bool isItSecondCard = IsThisSecondCard(myCard);
            if (isItSecondCard)
            {
                AreCardsEqual();
            }
        }
        private void btn_10Card_Click(object sender, RoutedEventArgs e)
        {
            btn_10Card.IsEnabled = false;
            img_10Card.Visibility = Visibility.Visible;
            SaveClickedButton(btn_10Card);
            Card myCard = GetCard(9);
            bool isItSecondCard = IsThisSecondCard(myCard);
            if (isItSecondCard)
            {
                AreCardsEqual();
            }
        }
        private void btn_11Card_Click(object sender, RoutedEventArgs e)
        {
            btn_11Card.IsEnabled = false;
            img_11Card.Visibility = Visibility.Visible;
            SaveClickedButton(btn_11Card);
            Card myCard = GetCard(10);
            bool isItSecondCard = IsThisSecondCard(myCard);
            if (isItSecondCard)
            {
                AreCardsEqual();
            }
        }
        private void btn_12Card_Click(object sender, RoutedEventArgs e)
        {
            btn_12Card.IsEnabled = false;
            img_12Card.Visibility = Visibility.Visible;
            SaveClickedButton(btn_12Card);
            Card myCard = GetCard(11);
            bool isItSecondCard = IsThisSecondCard(myCard);
            if (isItSecondCard)
            {
                AreCardsEqual();
            }
        }
        private void btn_13Card_Click(object sender, RoutedEventArgs e)
        {
            btn_13Card.IsEnabled = false;
            img_13Card.Visibility = Visibility.Visible;
            SaveClickedButton(btn_13Card);
            Card myCard = GetCard(12);
            bool isItSecondCard = IsThisSecondCard(myCard);
            if (isItSecondCard)
            {
                AreCardsEqual();
            }
        }
        private void btn_14Card_Click(object sender, RoutedEventArgs e)
        {
            btn_14Card.IsEnabled = false;
            img_14Card.Visibility = Visibility.Visible;
            SaveClickedButton(btn_14Card);
            Card myCard = GetCard(13);
            bool isItSecondCard = IsThisSecondCard(myCard);
            if (isItSecondCard)
            {
                AreCardsEqual();
            }
        }
        private void btn_15Card_Click(object sender, RoutedEventArgs e)
        {
            btn_15Card.IsEnabled = false;
            img_15Card.Visibility = Visibility.Visible;
            SaveClickedButton(btn_15Card);
            Card myCard = GetCard(14);
            bool isItSecondCard = IsThisSecondCard(myCard);
            if (isItSecondCard)
            {
                AreCardsEqual();
            }
        }
        private void btn_16Card_Click(object sender, RoutedEventArgs e)
        {
            btn_16Card.IsEnabled = false;
            img_16Card.Visibility = Visibility.Visible;
            SaveClickedButton(btn_16Card);
            Card myCard = GetCard(15);
            bool isItSecondCard = IsThisSecondCard(myCard);
            if (isItSecondCard)
            {
                AreCardsEqual();
            }
        }
        private void btn_17Card_click(object sender, RoutedEventArgs e)
        {
            btn_17Card.IsEnabled = false;
            img_17Card.Visibility = Visibility.Visible;
            SaveClickedButton(btn_17Card);
            Card myCard = GetCard(16);
            bool isItSecondCard = IsThisSecondCard(myCard);
            if (isItSecondCard)
            {
                AreCardsEqual();
            }
        }
        private void btn_18Card_Click(object sender, RoutedEventArgs e)
        {
            btn_18Card.IsEnabled = false;
            img_18Card.Visibility = Visibility.Visible;
            SaveClickedButton(btn_18Card);
            Card myCard = GetCard(17);
            bool isItSecondCard = IsThisSecondCard(myCard);
            if (isItSecondCard)
            {
                AreCardsEqual();
            }
        }
        private void btn_19Card_Click(object sender, RoutedEventArgs e)
        {
            btn_19Card.IsEnabled = false;
            img_19Card.Visibility = Visibility.Visible;
            SaveClickedButton(btn_19Card);
            Card myCard = GetCard(18);
            bool isItSecondCard = IsThisSecondCard(myCard);
            if (isItSecondCard)
            {
                AreCardsEqual();
            }
        }
        private void btn_20Card_Click(object sender, RoutedEventArgs e)
        {
            btn_20Card.IsEnabled = false;
            img_20Card.Visibility = Visibility.Visible;
            SaveClickedButton(btn_20Card);
            Card myCard = GetCard(19);
            bool isItSecondCard = IsThisSecondCard(myCard);
            if (isItSecondCard)
            {
                AreCardsEqual();
            }
        }
    }
}
