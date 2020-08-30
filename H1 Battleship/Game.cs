using System;
using System.Text;
using System.Threading;

namespace H1_Battleship
{
    public class Game
    {
        #region Fields
        private bool yourTurn;
        private bool run = true;
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
            while (run)
            {
                if (yourTurn)
                {
                    GUI.Shoot(myPlayfield, oponentPlayfield);
                    yourTurn = false;
                }
                else
                {
                    ComputerShoot(myPlayfield, oponentPlayfield);
                    yourTurn = true;
                }
            }
        }

        public void ComputerShoot(Playfield myPlayfield, Playfield oponentPlayfield)
        {
            char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            Random random = new Random();

            int[] shootHere = new int[] { random.Next(0, myPlayfield.Width), random.Next(0, myPlayfield.Height) };

            if (oponentPlayfield.AttemptedShootings.Contains($"{shootHere[0]},{shootHere[1]}"))
            {
                Console.WriteLine("Computer misses");
            }
            else
            {
                oponentPlayfield.AttemptedShootings.Add($"{shootHere[0]},{shootHere[1]}");
                if (myPlayfield.Fields[shootHere[0], shootHere[1]].ShipHere && myPlayfield.Fields[shootHere[0], shootHere[1]].ShipAlive)
                {
                    Console.WriteLine("The computer hit a ship on {0}{1}", alphabet[shootHere[0]], shootHere[1]);
                }
                else
                {
                Console.WriteLine("Computer missed");
                }
            }

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

                //Console.OutputEncoding = Encoding.UTF8;
                //Console.BackgroundColor = ConsoleColor.Blue;

                //for (int i = 0; i < playfield.Width; i++)
                //{
                //    Console.Write(string.Format("   {0}", alphabet[i]));
                //}

                //for (int i = 0; i < playfield.Height; i++)
                //{
                //    Console.Write(string.Format("\n{0}  ", i));
                //    for (int j = 0; j < playfield.Width; j++)
                //    {
                //        if (fields[i, j].ShipHere && fields[i, j].ShipAlive)
                //        {
                //            Console.Write(string.Format("  {0} ", "\u26F5"));
                //        }
                //        Console.Write(string.Format("{0}", " "));
                //    }
                //}

                //Console.WriteLine("\n");
                //Console.BackgroundColor = ConsoleColor.Black;

                for (int i = 0; i < playfield.Height; i++)
                {
                    for (int j = 0; j < playfield.Width; j++)
                    {
                        if (fields[i, j].ShipHere && fields[i, j].ShipAlive)
                        {
                            Console.WriteLine($"You have part of a ship on ({j}){alphabet[j]}{i}");
                        }
                    }
                }

                Console.WriteLine("You have {0} ships", playfield.Ships.Count);
            }
            /// <summary>
            /// Choose a place to shoot
            /// </summary>
            public static void Shoot(Playfield myPlayfield, Playfield oponentPlayfield)
            {
                Console.WriteLine("Pick a point to shoot (Seperate by comma, example. H,5)");
                string[] shootHere = Console.ReadLine().Trim().Split(',');
                if (myPlayfield.AttemptedShootings.Contains($"{shootHere[0]},{shootHere[1]}"))
                {
                    Console.WriteLine("You've already shot here, nothing new happened");
                    Thread.Sleep(1250);
                }
                else
                {
                    myPlayfield.AttemptedShootings.Add($"{shootHere[0]},{shootHere[1]}");
                    if (oponentPlayfield.Fields[ConvertCharToInt(shootHere[0].ToLower().ToCharArray()), Convert.ToInt32(shootHere[1])].ShipHere && oponentPlayfield.Fields[ConvertCharToInt(shootHere[0].ToLower().ToCharArray()), Convert.ToInt32(shootHere[1])].ShipAlive)
                    {
                        Console.WriteLine("You hit a ship!");
                        Thread.Sleep(1250);
                    }
                    else
                    {
                        Console.WriteLine("You missed");
                        Thread.Sleep(1250);
                    }
                }
            }

            public static int ConvertCharToInt(char[] _char)
            {
                char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
                int index = -1;

                foreach (char character in alphabet)
                {
                    index++;
                    if (_char[0].Equals(character))
                    {
                        break;
                    }
                }

                return index;
            }
            #endregion
        }
        #endregion
    }
}
