using System;

namespace H1_Battleship
{
    public class Ship
    {
        #region Fields
        private byte length;
        private string type;
        #endregion

        #region Properties
        /// <summary>
        /// The length of the ship
        /// </summary>
        public byte Length
        {
            get
            {
                return length;
            }
        }
        /// <summary>
        /// The type of ship
        /// </summary>
        public string Type
        {
            get
            {
                return type;
            }
        }
        #endregion

        #region Constructors
        public Ship(Field.ShipTypes shipType)
        {
            switch (shipType)
            {
                case Field.ShipTypes.Hangar:
                    length = 5;
                    type = "Hangar";
                    break;
                case Field.ShipTypes.Battleship:
                    length = 4;
                    type = "Battleship";
                    break;
                case Field.ShipTypes.Destroyer:
                    length = 3;
                    type = "Destroyer";
                    break;
                case Field.ShipTypes.Submarine:
                    length = 3;
                    type = "Submarine";
                    break;
                case Field.ShipTypes.PartolBoat:
                    length = 2;
                    type = "PartolBoat";
                    break;
            }
        }
        #endregion

        #region Methods

        #endregion
    }
}
