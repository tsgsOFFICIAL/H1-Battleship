using System;
using System.Collections.Generic;
using System.Linq;

namespace H1_Battleship
{
    public class Playfield
    {
        #region Fields
        private byte width;
        private byte height;
        private byte maxShips = 5;
        private Field[,] fields;
        private List<string> attemptedShootings = new List<string>();
        private List<Ship> ships = new List<Ship>();
        #endregion

        #region Properties
        /// <summary>
        /// This is the width of the playfield
        /// </summary>
        public byte Width
        {
            get
            {
                return width;
            }
        }
        /// <summary>
        /// This is the height of the playfield
        /// </summary>
        public byte Height
        {
            get
            {
                return height;
            }
        }
        /// <summary>
        /// This is all the fields of the playground
        /// </summary>
        public Field[,] Fields
        {
            get
            {
                return fields;
            }
        }
        /// <summary>
        /// Get the list of ships
        /// </summary>
        public List<Ship> Ships
        {
            get
            {
                return ships;
            }
        }
        /// <summary>
        /// A complete list of all positions that have been shot at
        /// </summary>
        public List<string> AttemptedShootings
        {
            get
            {
                return attemptedShootings;
            }
            set
            {
                attemptedShootings.Add(value.ToString());
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// This initialises a new playfield object
        /// </summary>
        /// <param name="width">This is the width of the playfield</param>
        /// <param name="height">This is the height of the playfield</param>
        public Playfield(byte width, byte height)
        {
            this.width = width;
            this.height = height;
            fields = new Field[height, width];
            GenerateFields();
            GenerateShips();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Generate fields
        /// </summary>
        private void GenerateFields()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    fields[i, j] = new Field();
                }
            }
        }

        /// <summary>
        /// Generate new ships
        /// </summary>
        private void GenerateShips()
        {
            List<string> availableShips = Enum.GetNames(typeof(Field.ShipTypes)).ToList();

            byte shipsCreated = 0;
            while (shipsCreated < maxShips)
            {
                Random random = new Random(); //Generate and initialise a new Random object

                byte shipType = (byte)random.Next(0, 5); //Generate a random number representing the shipType, see Field.ShipTypes for more
                byte shipLength = GetShipLength(shipType); //Get the shipLength from the shipType

                byte rotation = (byte)random.Next(0, 1 + 1); //rotation 0 = horisontal, rotation 1 = vertical

                if (rotation == 0)
                {
                    try
                    {
                        byte x = (byte)random.Next(0, width - shipLength); //Generate a random number than represents the x value (horisontal)
                        byte y = (byte)random.Next(0, height); //Generate a random number that represents the y value (vertical)

                        if (CheckShipPosition(x, y, rotation, shipLength))
                        {
                            if (availableShips.Contains(GetShipType(shipType)))
                            {
                                CreateShip(x, y, rotation, shipType);
                                ships.Add(new Ship((Field.ShipTypes)Enum.Parse(typeof(Field.ShipTypes), GetShipType(shipType))));
                                availableShips.Remove(GetShipType(shipType));
                                shipsCreated++;
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                else //rotation == 1
                {
                    try
                    {
                        byte x = (byte)random.Next(0, width); //Generate a random number than represents the x value (horisontal)
                        byte y = (byte)random.Next(0, height - shipLength); //Generate a random number that represents the y value (vertical)

                        if (CheckShipPosition(x, y, rotation, shipLength))
                        {
                            if (availableShips.Contains(GetShipType(shipType)))
                            {
                                CreateShip(x, y, rotation, shipType);
                                ships.Add(new Ship((Field.ShipTypes)Enum.Parse(typeof(Field.ShipTypes), GetShipType(shipType))));
                                availableShips.Remove(GetShipType(shipType));
                                shipsCreated++;
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        /// <summary>
        /// Check the ships position
        /// </summary>
        /// <param name="x">Column position</param>
        /// <param name="y">Row position</param>
        /// <param name="rotation">Rotation (1 is vertical, 0 is horisontal)</param>
        /// <param name="shipType">Shiptype refers to the type of ship, see Field.ShipTypes for more</param>
        /// <returns></returns>
        private bool CheckShipPosition(byte x, byte y, byte rotation, byte shipLength)
        {
            try
            {
                if (rotation == 0)
                {
                    for (int i = 0; i < shipLength; i++)
                    {
                        if (fields[y, i + x].ShipHere)
                        {
                            return false;
                        }
                    }
                }
                else //rotation == 1
                {
                    for (int i = 0; i < shipLength; i++)
                    {
                        if (fields[i + y, x].ShipHere)
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private byte GetShipLength(byte shipType)
        {
            byte shipLength = 0;
            switch (shipType)
            {
                case 0:
                    shipLength = 5;
                    break;
                case 1:
                    shipLength = 4;
                    break;
                case 2:
                    shipLength = 3;
                    break;
                case 3:
                    shipLength = 3;
                    break;
                case 4:
                    shipLength = 2;
                    break;
            }
            return shipLength;
        }

        /// <summary>
        /// Return the name of the ship
        /// </summary>
        /// <param name="shipType"></param>
        /// <returns></returns>
        private string GetShipType(byte shipType)
        {
            return Enum.GetName(typeof(Field.ShipTypes), shipType);
        }

        private void CreateShip(byte x, byte y, byte rotation, byte shipType)
        {
            string shipTypeString = GetShipType(shipType);
            byte shipLength = GetShipLength(shipType);
            if (rotation == 0)
            {
                for (int i = 0; i < shipLength; i++)
                {
                    fields[y, i + x].ShipHere = true;
                    fields[y, i + x].ShipAlive = true;
                    fields[y, i + x].ShipType = shipTypeString;
                }
            }
            else
            {
                for (int i = 0; i < shipLength; i++)
                {
                    fields[i + y, x].ShipHere = true;
                    fields[i + y, x].ShipAlive = true;
                    fields[i + y, x].ShipType = shipTypeString;
                }
            }
        }
        #endregion
    }
}
