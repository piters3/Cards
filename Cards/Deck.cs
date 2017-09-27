using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    class Deck
    {
        private string[] _colors;
        private string[] _figures;
        List<Card> _deck;

        public Deck()
        {
            //{ "Spade", "Club", "Diamond", "Heart" };
            //{ "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King", "Ace" };

            //{ "Wino", "Żołądź", "Dzwonek" };
            //{ "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2" };
            _colors = new string[] { "Wino", "Żołądź", "Dzwonek", "Serce" };
            _figures = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Walet", "Dama", "Król", "As" };
            _deck = new List<Card>();
            CreateDeck();
        }

        public void CreateDeck()
        {
            for (int i = 0; i < _colors.Length; i++)
            {
                for (int j = 0; j < _figures.Length; j++)
                {
                    _deck.Add(new Card { ColorNumber = i, FigureNumber = j + 2, Color = _colors[i], Figure = _figures[j] });
                }
            }

            _deck.Add(new Card { FigureNumber = 15, Figure = "Joker" });
            _deck.Add(new Card { FigureNumber = 15, Figure = "Joker" });
        }

        public List<Card> Shuffle()
        {
            var rnd = new Random();
            var result = _deck.OrderBy(item => rnd.Next()).ToList();
            return result;
        }
    }
}
