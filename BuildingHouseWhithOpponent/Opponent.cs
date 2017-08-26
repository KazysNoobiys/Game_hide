using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingHouseWhithOpponent
{
    class Opponent
    {
        Location myLocation;
        Random random;

        public Opponent(Location startLocation)
        {
            myLocation = startLocation;
            this.random = new Random();
        }
        public void Move()
        {
            bool hiding = false;
            while (!hiding)
            {
                if (myLocation is IHasExteriorDoor)
                {
                    if (random.Next(2) == 1)
                    {
                        IHasExteriorDoor myExteriorLocation = myLocation as IHasExteriorDoor;
                        myLocation = myExteriorLocation.DoorLocationn;
                    }
                }
                int rand = random.Next(myLocation.Exits.Length);
                myLocation = myLocation.Exits[rand];
                if (myLocation is IHidingPlace)
                    hiding = true;
            }
        }
        public bool Check(Location locationToCheck)
        {
            if (locationToCheck == myLocation)
                return true;
            else
                return false;
        }
    }
}
