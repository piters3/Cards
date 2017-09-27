using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cards
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeWindow();

            Deck deck = new Deck();
            War war = new War(deck.Shuffle());

            war.DisableSounds();
            war.Play();

        }

        private static void InitializeWindow()
        {
            Console.CursorVisible = false;

            for (var percentComplete = 0; percentComplete <= 100; percentComplete++)
            {
                var title = ($"Ładownie gry {percentComplete} %");
                Console.Title = title;
                Thread.Sleep(50);
                PrintLoadingSymbol(percentComplete);
            }

            Console.SetWindowSize(80, 40);
            Console.SetBufferSize(80, 59);

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            Console.Title = "Wojna - gra karciana";
        }

        private static void PrintLoadingSymbol(int i)
        {
            char[] Symbols = { '|', '/', '-', '\\' };

            Console.CursorLeft = Console.WindowWidth / 2;
            Console.CursorTop = Console.WindowHeight / 2;

            Console.Write(Symbols[i % Symbols.Length]);

            i++;
            if (i == Symbols.Length)
            {
                i = 0;
            }
        }    
    }
}
