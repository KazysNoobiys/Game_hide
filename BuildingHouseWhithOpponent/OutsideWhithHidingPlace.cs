using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingHouseWhithOpponent
{
    class OutsideWhithHidingPlace : Outside, IHidingPlace
    {
        public OutsideWhithHidingPlace(string name, bool hot, string hidingPlace) : base(name, hot)
        {
            HidingPlace = hidingPlace;
        }
        public override string Description
        {
            get
            {
                return base.Description + " Спрятаться можно: " + HidingPlace + ". ";
            }
        }
        public string HidingPlace { get; private set; }
    }
    class RoomWhithHidingPlace : Room, IHidingPlace
    {
        public RoomWhithHidingPlace(string name, string decoration,string hidingPlace) : base(name, decoration)
        {
            HidingPlace = hidingPlace;
        }
        public override string Description
        {
            get
            {
                return base.Description + " Спрятаться можно: " + HidingPlace+". ";
            }
        }
        public string HidingPlace { get; private set; }
       
    }
}
