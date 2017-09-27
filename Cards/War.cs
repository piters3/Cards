using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    class War
    {
        private List<Card> _deck;
        private Queue<Card> _myCards;
        private Queue<Card> _enemyCards;
        private bool _enableSounds;


        public War(List<Card> deck)
        {
            _deck = deck;
            _myCards = new Queue<Card>();
            _enemyCards = new Queue<Card>();
        }


        public void GiveOutCards()
        {
            for (int i = 0; i < _deck.Count(); i++)
            {
                if (i % 2 == 0)
                {
                    _myCards.Enqueue(_deck[i]);
                }
                else
                {
                    _enemyCards.Enqueue(_deck[i]);
                }
            }
        }


        public void Play()
        {
            GiveOutCards();

            Console.WriteLine("Dowolny klawisz zaczyna grę!");
            Console.ReadKey();
            Console.Clear();

            while (_myCards.Count != 0 && _enemyCards.Count != 0)
            {
                var enemyCard = _enemyCards.Peek();
                var myCard = _myCards.Peek();

                Console.WriteLine($"Komputer rzuca: {enemyCard.Figure} {enemyCard.Color}");
                Console.WriteLine($"Rzucasz: {myCard.Figure} {myCard.Color}");

                if (myCard.FigureNumber < enemyCard.FigureNumber)
                {              
                    Console.WriteLine("\nKarty zabiera komputer");
                    TakeCards(_myCards, _enemyCards, myCard);
                    if (_enableSounds)
                        Console.Beep(70, 100);
                }
                else if (myCard.FigureNumber == enemyCard.FigureNumber)
                {
                    Console.WriteLine("\nWalka!!!");

                    try
                    {
                        var enemySecondCard = _enemyCards.ElementAt(1);
                        var mySecondCard = _myCards.ElementAt(1);

                        var enemyThirdCard = _enemyCards.ElementAt(2);
                        var myThirdCard = _myCards.ElementAt(2);

                        Console.WriteLine($"Komputer rzuca: {enemySecondCard.Figure} {enemySecondCard.Color} (Odwrócona)");
                        Console.WriteLine($"Rzucasz: {mySecondCard.Figure} {mySecondCard.Color} (Odwrócona)");

                        Console.WriteLine($"\nKomputer rzuca: {enemyThirdCard.Figure} {enemyThirdCard.Color}");
                        Console.WriteLine($"Rzucasz: {myThirdCard.Figure} {myThirdCard.Color}");

                        if (myThirdCard.FigureNumber < enemyThirdCard.FigureNumber)
                        {
                            Console.WriteLine("\nKarty zabiera komputer");
                            TakeCards(_myCards, _enemyCards, myCard);
                            TakeCards(_myCards, _enemyCards, mySecondCard);
                            TakeCards(_myCards, _enemyCards, myThirdCard);
                            if (_enableSounds)
                                Console.Beep(70, 300);
                        }
                        else
                        {
                            Console.WriteLine("\nZabierasz karty");
                            TakeCards(_enemyCards, _myCards, enemyCard);
                            TakeCards(_enemyCards, _myCards, enemySecondCard);
                            TakeCards(_enemyCards, _myCards, enemyThirdCard);
                            if (_enableSounds)
                                Console.Beep(1500, 100);
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        if (_myCards.Count < _enemyCards.Count)
                        {
                            Console.WriteLine("Przegrałeś!!!");
                        }
                        else
                        {
                            Console.WriteLine("Wygrałeś!!!");
                        }
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("\nZabierasz karty");
                    TakeCards(_enemyCards, _myCards, enemyCard);
                    if (_enableSounds)
                        Console.Beep(1500, 100);
                }
                PrintStatistics();
            }

            if (_myCards.Count == 0)
            {
                Console.WriteLine("Przegrałeś!!!");
            }
            else
            {
                Console.WriteLine("Wygrałeś!!!");
            }
            Console.WriteLine("Dowolny klawisz zamyka okno");
            Console.ReadKey();
        }


        public void TakeCards(Queue<Card> loserCards, Queue<Card> winnerCards, Card wonCard)
        {
            winnerCards.Enqueue(wonCard);
            loserCards.Dequeue();
            winnerCards.Enqueue(winnerCards.Dequeue());
        }


        public void PrintGivenCards()
        {
            Console.WriteLine($"Moje: \n");
            foreach (var card in _myCards)
            {
                Console.WriteLine($"{card.Figure} {card.Color}");
            }

            Console.WriteLine($"\n\nPrzeciwnika: \n");
            foreach (var card in _enemyCards)
            {
                Console.WriteLine($"{card.Figure} {card.Color}");
            }
        }


        public void PrintStatistics()
        {
            Console.WriteLine($"{null,65}Ilość kart:");
            Console.WriteLine($"{null,60}Komputer\t Ja");
            Console.WriteLine($"{_enemyCards.Count,65}\t {_myCards.Count}");

            Console.WriteLine($"\n{null,65}Moje karty:");

            foreach (var card in _myCards)
            {
                Console.WriteLine($"{card.Figure,70} {card.Color}");
            }
            Console.ReadKey();
            Console.Clear();
        }

        public void EnableSounds()
        {
            _enableSounds = true;
        }

        public void DisableSounds()
        {
            _enableSounds = false;
        }
    }
}