using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingHouseWhithOpponent
{
    interface IHasExteriorDoor
    {
        string DoorDescription { get;  }
        Location DoorLocationn { get; set; }
    }
}
