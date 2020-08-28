using System;
using System.Collections.Generic;

namespace H1_Battleship
{
    public class Playfield
    {
        #region Fields
        private byte width;
        private byte height;
        private byte maxShips = 5;
        private Field[,] fields;
        private List<Ship> ships;
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

        private void GenerateShips()
        {
            byte shipsCreated = 0;
            while (shipsCreated < maxShips)
            {
                Random random = new Random();

                byte shipType = (byte)random.Next(0, 5); //Generate a random number representing the shipType, see Field.ShipTypes for more
                byte x = (byte)random.Next(0, width); //Generate a random number than represents the x value (horisontal)
                byte y = (byte)random.Next(0, height); //Generate a random number that represents the y value (vertical)
                byte rotation = (byte)random.Next(0, 1 + 1); //rotation 0 = horisontal, rotation 1 = vertical
                if (CheckShipPosition(x, y, rotation, shipType))
                {
                    CreateShip(x, y, rotation, shipType);
                    shipsCreated++;
                }
            }
        }

        private bool CheckShipPosition(byte x, byte y, byte rotation, byte shipType)
        {
            byte shipLength = GetShipLength(shipType);
            try
            {
                if (rotation == 0)
                {
                    for (int i = x; i < shipLength; i++)
                    {
                        if (fields[y, i].ShipHere)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    for (int i = y; i < shipLength; i++)
                    {
                        if (fields[i, x].ShipHere)
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
                for (int i = x; i < shipLength; i++)
                {
                    fields[y, i].ShipHere = true;
                    fields[y, i].ShipAlive = true;
                    fields[y, i].ShipType = shipTypeString;
                }
            }
            else
            {
                for (int i = y; i < shipLength; i++)
                {
                    fields[i, x].ShipHere = true;
                    fields[i, x].ShipAlive = true;
                    fields[i, x].ShipType = shipTypeString;
                }
            }
        }
        #endregion
    }
}
