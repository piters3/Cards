using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Card> deck = InitializeDeck();

            List<Card> shuffledDeck = Shuffle(deck);

            War(shuffledDeck);
        }

        private static void War(List<Card> deck)
        {
            var myCards = new Queue<Card>();
            var enemyCards = new Queue<Card>();

            GiveOutCards(deck, myCards, enemyCards);
            //PrintGivenCards(myCards, enemyCards);

            Console.WriteLine("Dowolny klawisz zaczyna grę!");
            Console.ReadKey();
            Console.Clear();

            while (myCards.Count != 0 && enemyCards.Count != 0)
            {
                var enemyCard = enemyCards.Peek();
                var myCard = myCards.Peek();

                Console.WriteLine($"Kumputer rzuca: {enemyCard.Figure} {enemyCard.Color}");
                Console.WriteLine($"Rzucasz: {myCard.Figure} {myCard.Color}");

                if (myCard.FigureNumber < enemyCard.FigureNumber)
                {
                    Console.WriteLine("\nKarty zabiera komputer");
                    //enemyCards.Enqueue(myCard);
                    //myCards.Dequeue();
                    //enemyCards.Enqueue(enemyCards.Dequeue());
                    TakeCards(myCards, enemyCards, myCard);
                }
                else if (myCard.FigureNumber == enemyCard.FigureNumber)
                {
                    Console.WriteLine("\nWalka!!!");
                    var enemySecondCard = enemyCards.ElementAt(1);
                    var mySecondCard = myCards.ElementAt(1);

                    Console.WriteLine($"Kumputer rzuca: {enemySecondCard.Figure} {enemySecondCard.Color}");
                    Console.WriteLine($"Rzucasz: {mySecondCard.Figure} {mySecondCard.Color}");

                    if (mySecondCard.FigureNumber < enemySecondCard.FigureNumber)
                    {
                        Console.WriteLine("\nKarty zabiera komputer");
                        enemyCards.Enqueue(myCard);
                        myCards.Dequeue();
                        enemyCards.Enqueue(enemyCards.Dequeue());

                        enemyCards.Enqueue(mySecondCard);
                        myCards.Dequeue();
                        enemyCards.Enqueue(enemyCards.Dequeue());
                    }
                    else
                    {
                        Console.WriteLine("\nZabierasz karty");
                        myCards.Enqueue(enemyCard);
                        enemyCards.Dequeue();
                        myCards.Enqueue(myCards.Dequeue());

                        myCards.Enqueue(enemySecondCard);
                        enemyCards.Dequeue();
                        myCards.Enqueue(myCards.Dequeue());
                    }
                }
                else
                {
                    Console.WriteLine("\nZabierasz karty");
                    TakeCards(enemyCards, myCards, enemyCard);

                    //myCards.Enqueue(enemyCard);
                    //enemyCards.Dequeue();
                    //myCards.Enqueue(myCards.Dequeue());

                }
                PrintStatistics(myCards, enemyCards);
            }

            if (myCards.Count == 0)
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

        private static void TakeCards(Queue<Card> loserCards, Queue<Card> winnerCards, Card wonCard)
        {
            winnerCards.Enqueue(wonCard);
            loserCards.Dequeue();
            winnerCards.Enqueue(winnerCards.Dequeue());
        }

        private static void PrintGivenCards(Queue<Card> myCards, Queue<Card> enemyCards)
        {
            Console.WriteLine($"Moje: \n");
            foreach (var card in myCards)
            {
                Console.WriteLine($"{card.ColorNumber} : {card.Figure} : {card.FigureNumber} : {card.Color}");
            }

            Console.WriteLine($"\n\nPrzeciwnika: \n");
            foreach (var card in enemyCards)
            {
                Console.WriteLine($"{card.ColorNumber} : {card.Figure} : {card.FigureNumber} : {card.Color}");
            }
        }

        private static void GiveOutCards(List<Card> deck, Queue<Card> myCards, Queue<Card> enemyCards)
        {
            for (int i = 0; i < deck.Count(); i++)
            {
                if (i % 2 == 0)
                {
                    myCards.Enqueue(deck[i]);
                }
                else
                {
                    enemyCards.Enqueue(deck[i]);
                }
            }
        }

        private static void PrintStatistics(Queue<Card> myCards, Queue<Card> enemyCards)
        {
            Console.WriteLine($"{null,65}Ilość kart:");
            Console.WriteLine($"{null,60}Komputer\t Ja");
            Console.WriteLine($"{enemyCards.Count,65}\t {myCards.Count}");

            Console.WriteLine($"\n{null,65}Moje karty:");

            foreach (var card in myCards)
            {
                Console.WriteLine($"{card.Figure,70} {card.Color}");
            }
            Console.ReadKey();
            Console.Clear();
        }   

        private static List<Card> InitializeDeck()
        {
            //string[] Colors = { "Spade", "Club", "Diamond", "Heart" };
            //string[] Figures = { "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King", "Ace" };

            //string[] Colors = { "Wino", "Żołądź", "Dzwonek", "Serce" };
            //string[] Figures = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Walet", "Dama", "Król", "As" };

            string[] Colors = { "Wino", "Żołądź", "Dzwonek" };
            string[] Figures = { "2", "3", "4", "5", };

            List<Card> deck = new List<Card>();

            for (int i = 0; i < Colors.Length; i++)
            {
                for (int j = 0; j < Figures.Length; j++)
                {
                    deck.Add(new Card { ColorNumber = i, FigureNumber = j+2, Color = Colors[i], Figure = Figures[j] });
                }
            }

            deck.Add(new Card { FigureNumber = 15, Figure = "Joker" });
            deck.Add(new Card { FigureNumber = 15, Figure = "Joker" });

            //foreach (var item in deck)
            //{
            //    Console.WriteLine($"{item.ColorNumber} : {item.Color} : {item.FigureNumber} : {item.Figure}");
            //}

            return deck;
        }

        private static List<Card> Shuffle(List<Card> deck)
        {
            var rnd = new Random();
            var result = deck.OrderBy(item => rnd.Next()).ToList();
            return result;
        }
    }
}
