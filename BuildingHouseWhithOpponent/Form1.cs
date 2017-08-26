using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildingHouseWhithOpponent
{
    public partial class Form1 : Form
    {
        Outside garden;
        OutsideWhisDoor backYard, frontYard;
        RoomWhisDoor livingRoom, kitchen;
        Room diningRoom, stairs;
        RoomWhithHidingPlace secondFloorHallway, masterBedroom, secondBedroom, bathroom;
        OutsideWhithHidingPlace passage;
        Location currentLocation;

        Opponent opponent;
        int Moves;

        public Form1()
        {
            InitializeComponent();
            CreatObjects();
            opponent = new Opponent(frontYard);
            ResetGame(false);
        }

        private void hide_Click(object sender, EventArgs e)
        {
            hide.Visible = false;

            for (int i = 1; i <= 10; i++)
            {
                opponent.Move();
                description.Text = i + "...";
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);
            }
            description.Text = "Я иду искать!";
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);

            goHere.Visible = true;
            exits.Visible = true;
            MoveToANewLocation(livingRoom);
        }

        private void check_Click(object sender, EventArgs e)
        {
            Moves++;
            if (opponent.Check(currentLocation))
                ResetGame(true);
            else
            {
                RedrawForm();
                description.Text += "\r\n Здесь ни кого нет";
            }
        }

        private void goHere_Click(object sender, EventArgs e)
        {
            MoveToANewLocation(currentLocation.Exits[exits.SelectedIndex]);
        }

        private void goThroughTheDoor_Click(object sender, EventArgs e)
        {
            IHasExteriorDoor exitDoor = currentLocation as IHasExteriorDoor;
            MoveToANewLocation(exitDoor.DoorLocationn);
        }

        private void CreatObjects()
        {
            garden = new Outside("сад", false);
            backYard = new OutsideWhisDoor("задний двор", true, "старая деревянная дверь красного цвета", "кусты");
            frontYard = new OutsideWhisDoor("лужайка", false, "новая белая пластиковая дверь", "старый бассейн");
            livingRoom = new RoomWhisDoor("гостиная", "диван, стол, телевизор", "новая белая пластиковая дверь", "диван");
            diningRoom = new Room("столовая", "большой стол, несколько стульев");
            kitchen = new RoomWhisDoor("кухня", "куханный стол, плита", "дубовая дверь", "кладовка");
            stairs = new Room("лестница", "деревянные перила");
            secondFloorHallway = new RoomWhithHidingPlace("коридор второго этажа", "картина с сабакой", "шкаф");
            masterBedroom = new RoomWhithHidingPlace("главная спальня", "плазменный телевизор, большая кровать", "большая кровать");
            secondBedroom = new RoomWhithHidingPlace("вторая спальня", "маленькая кровать, большое окно", "маленькая кровать");
            bathroom = new RoomWhithHidingPlace("ванная", "раковиная, туалет", "душ");
            passage = new OutsideWhithHidingPlace("проезд", false, "гараж");

            garden.Exits = new Location[] { frontYard, backYard };
            backYard.Exits = new Location[] { garden, diningRoom, passage };
            frontYard.Exits = new Location[] { garden, livingRoom, passage };
            livingRoom.Exits = new Location[] { garden, diningRoom };
            diningRoom.Exits = new Location[] { livingRoom, kitchen, stairs };
            kitchen.Exits = new Location[] { diningRoom, backYard };
            stairs.Exits = new Location[] { diningRoom, secondFloorHallway };
            secondFloorHallway.Exits = new Location[] { stairs, masterBedroom, secondBedroom, bathroom };
            masterBedroom.Exits = new Location[] { secondFloorHallway };
            secondBedroom.Exits = new Location[] { secondFloorHallway };
            bathroom.Exits = new Location[] { secondFloorHallway };
            passage.Exits = new Location[] { backYard, frontYard };

            frontYard.DoorLocationn = livingRoom;
            livingRoom.DoorLocationn = frontYard;
            kitchen.DoorLocationn = backYard;
            backYard.DoorLocationn = kitchen;
        }
        private void MoveToANewLocation(Location newlocation)
        {
            Moves++;
            currentLocation = newlocation;
            RedrawForm();
        }

        private void RedrawForm()
        {
            exits.Items.Clear();
            for (int i = 0; i < currentLocation.Exits.Length; i++)
            {
                exits.Items.Add(currentLocation.Exits[i].Name);
            }
            exits.SelectedIndex = 0;
            description.Text = currentLocation.Description + "\r\n(перемещение №" + Moves + ")";
            check.Visible = true;
            check.Enabled = false;
            if (currentLocation is IHidingPlace)
            {
                IHidingPlace temp = currentLocation as IHidingPlace;
                check.Text = "Check: " + temp.HidingPlace;
                check.Enabled = true;
            }
            goThroughTheDoor.Visible = true;
            if (currentLocation is IHasExteriorDoor)
                goThroughTheDoor.Enabled = true;
            else
                goThroughTheDoor.Enabled = false;
        }
        private void ResetGame(bool displayMessage)
        {
            if (displayMessage)
            {
                MessageBox.Show("Меня нашли за " + Moves + " ходов!");
                IHidingPlace foundPlace = currentLocation as IHidingPlace;
                description.Text = "Меня нашли за " + Moves + " ходов. Я пратался в "
                    + currentLocation.Name + " используя: " + foundPlace.HidingPlace + ".";
            }
            Moves = 0;
            hide.Visible = true;
            exits.Visible = false;
            goHere.Visible = false;
            check.Visible = false;
            goThroughTheDoor.Visible = false;
        }
    }
}
