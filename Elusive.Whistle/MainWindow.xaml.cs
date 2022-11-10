namespace Elusive.Whistle
{
    using Elusive.Whistle.Core.Controls;
    using Elusive.Whistle.Core.Model;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private const int CardsPerPlayer = 12;
        private const int NumberOfPlayers = 4;

        private Deck _dealer;

        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        } 
        #endregion

        #region Methods

        /// <summary>
        /// Initialize a new game.
        /// </summary>
        private void NewGame()
        {
            // create new game instance for our main window gameshape control
            GameShape.Game = new Game();

            // call some setup methods to initialize the game components
            SetupDealerDeck();
            SetupPlayerHandDecks();
            SetupTrickDecks();
        }

        /// <summary>
        /// Deals the next hand.
        /// </summary>
        private void DealNextHand()
        {
            // collection cards if any are out
            if (_dealer.Cards.Count < 52)
            {
                CollectCards();
            }
            _dealer.Shuffle(5);

            // deal 13 cards to each of the four players
            for (var cardCount = 0; cardCount < CardsPerPlayer; cardCount++)
            {
                _dealer.Draw(Player1Hand.Deck, 1);
                _dealer.Draw(Player2Hand.Deck, 1);
                _dealer.Draw(Player3Hand.Deck, 1);
                _dealer.Draw(Player4Hand.Deck, 1);
            }

            // turn over human player [4] hand
            Player4Hand.Deck.Sort();
            Player4Hand.Deck.MakeAllCardsDragable(true);
            Player4Hand.Deck.FlipAllCards();

        }

        /// <summary>
        /// Collects the cards.
        /// </summary>
        private void CollectCards()
        {
            Player1Hand.Deck.Draw(_dealer, Player1Hand.Deck.Cards.Count);
            Player2Hand.Deck.Draw(_dealer, Player2Hand.Deck.Cards.Count);
            Player3Hand.Deck.Draw(_dealer, Player3Hand.Deck.Cards.Count);
            Player4Hand.Deck.FlipAllCards();    // flip user cards face down
            Player4Hand.Deck.Draw(_dealer, Player4Hand.Deck.Cards.Count);
            Player1Trick.Deck.FlipAllCards();
            Player1Trick.Deck.Draw(_dealer, Player1Trick.Deck.Cards.Count); 
            Player2Trick.Deck.FlipAllCards();
            Player2Trick.Deck.Draw(_dealer, Player1Trick.Deck.Cards.Count);
            Player3Trick.Deck.FlipAllCards();
            Player3Trick.Deck.Draw(_dealer, Player1Trick.Deck.Cards.Count);
            Player4Trick.Deck.FlipAllCards();
            Player4Trick.Deck.Draw(_dealer, Player1Trick.Deck.Cards.Count);
        }

        private void PlayCard(Deck playerHand, Deck playerTrick, Card cardToPlay)
        {
            
        }

        /*
         *  Setup Methods
         *      initialize game/deck/hand objects etc.
         */

        private void SetupDealerDeck()
        {
            _dealer = new Deck(1, 13, GameShape.Game);

            _dealer.Shuffle(5);
            _dealer.MakeAllCardsDragable(false);
            _dealer.Enabled = true;
            _dealer.FlipAllCards();

            Dealer.Deck = _dealer;
            GameShape.DeckShapes.Add(Dealer);
            Dealer.DeckMouseLeftButtonDown += new MouseButtonEventHandler(Dealer_DeckMouseLeftButtonDown);
        }

        private void SetupTrickDecks()
        {
            Player1Trick.Deck = new Deck(GameShape.Game) { Enabled = true };
            Player2Trick.Deck = new Deck(GameShape.Game) { Enabled = true };
            Player3Trick.Deck = new Deck(GameShape.Game) { Enabled = true };
            Player4Trick.Deck = new Deck(GameShape.Game) { Enabled = true };

            Player1Trick.Deck.MakeAllCardsDragable(false);
            Player2Trick.Deck.MakeAllCardsDragable(false);
            Player3Trick.Deck.MakeAllCardsDragable(false);
            Player4Trick.Deck.MakeAllCardsDragable(false);

            GameShape.DeckShapes.Add(Player1Trick);
            GameShape.DeckShapes.Add(Player2Trick);
            GameShape.DeckShapes.Add(Player3Trick);
            GameShape.DeckShapes.Add(Player4Trick);
        }

        private void SetupPlayerHandDecks()
        {
            Player1Hand.Deck = new Deck(GameShape.Game) { Enabled = true };
            Player2Hand.Deck = new Deck(GameShape.Game) { Enabled = true };
            Player3Hand.Deck = new Deck(GameShape.Game) { Enabled = true };
            Player4Hand.Deck = new Deck(GameShape.Game) { Enabled = true };

            Player1Hand.Deck.MakeAllCardsDragable(true);
            Player2Hand.Deck.MakeAllCardsDragable(true);
            Player3Hand.Deck.MakeAllCardsDragable(true);
            Player4Hand.Deck.MakeAllCardsDragable(true);

            GameShape.DeckShapes.Add(Player1Hand);
            GameShape.DeckShapes.Add(Player2Hand);
            GameShape.DeckShapes.Add(Player3Hand);
            GameShape.DeckShapes.Add(Player4Hand);
        }

        #endregion

        #region Event Handlers

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            NewGame();

            GameShape.CardMouseLeftButtonDown += new MouseButtonEventHandler(GameShape_CardMouseLeftButtonDown);
            GameShape.CardDrag += new CardDragEventHandler(GameShape_CardDrag);

        }

        private void Dealer_DeckMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CollectCards();
        }

        private void GameShape_CardDrag(CardShape cardShape, DeckShape oldDeckShape, DeckShape newDeckShape)
        {
            // check for lead suit renege

            if (((newDeckShape.Deck.TopCard == null) && (cardShape.Card.Number == 1)) ||
                ((newDeckShape.Deck.TopCard != null) && (cardShape.Card.Suit == newDeckShape.Deck.TopCard.Suit) && (cardShape.Card.Number - 1 == newDeckShape.Deck.TopCard.Number)))
            {
                //Move card to stack
                cardShape.Card.Deck = newDeckShape.Deck;

                //Flip the first remaining card in the old deck
                if (oldDeckShape.Deck.TopCard != null)
                {
                    oldDeckShape.Deck.TopCard.Visible = true;
                    oldDeckShape.Deck.TopCard.Enabled = true;
                    oldDeckShape.Deck.TopCard.IsDragable = true;
                }
            }
        }

        private void GameShape_CardMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // click on player 4 hand will move the clicked card to the player 4 trick
            var card = (CardShape)sender;
            var gameShape = GameShape.GetGameShape(card.Card.Deck.Game);
            var cardShape = gameShape.GetCardShape(card.Card);
            var oldDeckShape = gameShape.GetDeckShape(card.Card.Deck);

            if (oldDeckShape.Name == "Player4Hand")
            {
                card.Card.Deck = Player4Trick.Deck;
            }

            gameShape.GetDeckShape(card.Card.Deck).UpdateCardShapes();
            Canvas.SetZIndex(oldDeckShape, 0);
        }

        #endregion


        private void MainWindow_DealButton_Click(object sender, RoutedEventArgs e)
        {
            // prompt user to confirm deal new game
            var result = MessageBox.Show("Deal a new hand?", "Confirm New Deal", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No) return;

            // deal new hand
            DealNextHand();
        }
    }
}
