using System;
using System.IO;
using System.Threading;

namespace H1_Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            Game Batteship = new Game();

            ShowRotatingMenu();

            while (true)
            {
                ShowMainMenu();
                switch (Console.ReadKey(false).KeyChar)
                {
                    case '1': //Play
                        Batteship.Start();
                        break;
                    case '2': //Exit
                        Environment.Exit(0);
                        break;
                }
            }
        }

        static void ShowMainMenu()
        {
            Console.Clear();
            string[] msg = ReadAsciiFromFile($@"message/1.txt");
            foreach (string line in msg)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();
            Console.WriteLine("Press (1) to play");
            Console.WriteLine("Press (2) to exit");
        }

        static void ShowRotatingMenu()
        {
            for (int i = 0; i < 4; i++)
            {
                string[] msg = ReadAsciiFromFile($@"message/{i + 1}.txt");
                foreach (string line in msg)
                {
                    Console.WriteLine(line);
                }
                Thread.Sleep(100);
                Console.Clear();
            }
        }

        static string[] ReadAsciiFromFile(string file)
        {
            return File.ReadAllLines(file);
        }

    }
}
