using System;
using System.Collections.Generic;

namespace H1_Battleship
{
    public class Ship
    {
        #region Fields
        private byte length;
        #endregion

        #region Properties
        public byte Length
        {
            get
            {
                return length;
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
                    break;
                case Field.ShipTypes.Battleship:
                    length = 4;
                    break;
                case Field.ShipTypes.Destroyer:
                    length = 3;
                    break;
                case Field.ShipTypes.Submarine:
                    length = 3;
                    break;
                case Field.ShipTypes.PartolBoat:
                    length = 2;
                    break;
            }
        }
        #endregion

        #region Methods

        #endregion
    }
}
