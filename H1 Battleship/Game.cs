using System;
using System.Text;
using System.Threading;

namespace H1_Battleship
{
    public class Game
    {
        #region Fields
        private bool yourTurn;
        private bool run;
        private Playfield myPlayfield;
        private Playfield oponentPlayfield;
        #endregion

        #region Constructors
        public Game()
        {
            yourTurn = true;
            run = true;
        }
        #endregion

        #region Methods
        public void Start()
        {
            byte[] dimensions = GUI.Setup();
            myPlayfield = new Playfield(dimensions[0], dimensions[1]);
            oponentPlayfield = new Playfield(dimensions[0], dimensions[1]);
            GUI.ShowPlayfield(myPlayfield);
            Console.ReadKey();
        }
        #endregion

        #region GUI Class
        public class GUI
        {
            #region Methods
            /// <summary>
            /// Clear the GUI
            /// </summary>
            public static void Clear()
            {
                Console.Clear();
            }
            /// <summary>
            /// Ask the initial questions before starting
            /// </summary>
            public static byte[] Setup()
            {
                Clear();

                byte width = 0;
                byte height = 0;

                Console.WriteLine($"Welcome {Environment.UserName}!");
                Console.WriteLine("Please specify how big the playfield needs to be, it needs to be between 10x10 and 20x20");

                Console.Write("Width: ");

                try
                {
                    width = Convert.ToByte(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Thats not a valid number");
                }

                if (!(width < 10 || width > 20)) //Check if width is between 5 and 20
                {
                    Console.Write("Height: ");

                    try
                    {
                        height = Convert.ToByte(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Thats not a valid number");
                    }
                    if (height < 10 || height > 20) //Check if height is between 5 and 20
                    {
                        Console.WriteLine($"The height needs to be between 10 - 20, not {height}");
                        Thread.Sleep(1250);
                        Setup();
                    }
                }
                else
                {
                    Console.WriteLine($"The width needs to be between 10 - 20, not {width}");
                    Thread.Sleep(1250);
                    Setup();
                }
                byte[] dimensions = new byte[] { width, height };
                return dimensions;
            }

            /// <summary>
            /// Show the playfield
            /// </summary>
            public static void ShowPlayfield(Playfield playfield)
            {
                Field[,] fields = playfield.Fields;
                char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

                Console.OutputEncoding = Encoding.UTF8;
                Console.BackgroundColor = ConsoleColor.Blue;

                for (int i = 0; i < playfield.Width; i++)
                {
                    Console.Write(string.Format("   {0}", alphabet[i]));
                }

                for (int i = 0; i < playfield.Height; i++)
                {
                    Console.Write(string.Format("\n{0}  ", i));
                    for (int j = 0; j < playfield.Width; j++)
                    {
                        if (fields[i, j].ShipHere && fields[i, j].ShipAlive)
                        {
                            Console.Write(string.Format("  {0} ", "\u26F5"));
                        }
                        Console.Write(string.Format("{0}", " "));
                    }
                }

                Console.WriteLine("\n");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("You have {0} ships", playfield.Ships.Count);
            }
            #endregion
        }
        #endregion
    }
}
