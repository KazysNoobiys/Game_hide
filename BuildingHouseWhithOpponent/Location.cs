using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingHouseWhithOpponent
{
    abstract class Location
    {
        public string Name { get; private set; }
        public Location(string name)
        {
            Name = name;
        }
        public Location[] Exits;
        public virtual string Description
        {
            get
            {
                string description = "Вы находитесь в " + Name + ". Вы видите двери ведущие в:";
                for (int i = 0; i < Exits.Length; i++)
                {
                    description += " " + Exits[i].Name;
                    if (i != Exits.Length - 1)
                        description += ", ";
                }
                return description += ". ";
            }
        }
    }
    class Room : Location
    {
        public Room(string name, string decoration) : base(name)
        {
            this.decoration += " " + decoration;
        }
        public override string Description
        {
            get
            {
                string description = base.Description;
                description += " Здесь вы видите:" + decoration + ". ";
                return description;
            }
        }
        private string decoration { get; set; }
    }
    class Outside : Location
    {
        public Outside(string name, bool hot) : base(name)
        {
            this.hot = hot;
        }
        public override string Description
        {
            get
            {
                string descripton = base.Description;
                if (hot)
                    descripton += "Здесь очень жарко! ";
                return descripton;
            }
        }
        private bool hot { get; set; }
    }
    class RoomWhisDoor : RoomWhithHidingPlace, IHasExteriorDoor
    {
        public RoomWhisDoor(string name, string decoration,string doorDescription, string hidingPlace) : base(name, decoration, hidingPlace)
        {
            this.doorDescriprion = doorDescription;
        }

        string doorDescriprion;
        public string DoorDescription
        {
            get
            {
                return doorDescriprion;
            }

        }
        public override string Description
        {
            get
            {
                return base.Description + " Вы видите дверь: " + DoorDescription;
            }
        }
        public Location DoorLocationn { get; set; }
    }
    class OutsideWhisDoor : OutsideWhithHidingPlace , IHasExteriorDoor
    {
        public OutsideWhisDoor(string name, bool hot, string doorDescriprion,string hidingPlace) : base(name, hot, hidingPlace)
        {
            this.doorDescriprion = doorDescriprion;
        }
        string doorDescriprion;
        public Location DoorLocationn { get;  set; }

        public override string Description
        {
            get
            {
                return base.Description + " Вы видите дверь: " + DoorDescription;
            }
        }
        public string DoorDescription
        {
            get
            {
                return doorDescriprion;
            }
        }
    }
}
