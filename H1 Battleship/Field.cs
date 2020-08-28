using System;
using System.Collections.Generic;

namespace H1_Battleship
{
    public class Field
    {
        #region Fields
        private bool shipHere;
        private bool shipAlive;
        private string shipType;
        #endregion

        #region Properties
        /// <summary>
        /// Each ship has its own properties, such as length
        /// </summary>
        public enum ShipTypes
        {
            Hangar,
            Battleship,
            Destroyer,
            Submarine,
            PartolBoat
        }
        /// <summary>
        /// Is there a ship on this field
        /// </summary>
        public bool ShipHere
        {
            get
            {
                return shipHere;
            }
            set
            {
                shipHere = value;
            }
        }
        /// <summary>
        /// Is the ship alive on this field
        /// </summary>
        public bool ShipAlive
        {
            get
            {
                return shipAlive;
            }
            set
            {
                shipAlive = value;
            }
        }
        /// <summary>
        /// What type of ship is this
        /// </summary>
        public string ShipType
        {
            get
            {
                return shipType;
            }
            set
            {
                shipType = value;
            }
        }
        #endregion

        #region Constructors
        public Field()
        {
            shipHere = false;
            shipAlive = false;
            shipType = "";
        }
        #endregion
    }
}
