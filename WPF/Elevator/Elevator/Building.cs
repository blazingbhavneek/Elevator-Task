using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Elevator
{

    public class Floor
    {
        public bool isTarget;

        public Floor()
        {
            this.isTarget = false;
        }
    }

    public class ElevatorBox
    {
        public int currentFloor = 1;
        public bool dirUp = true;

    }
    public class Building
    {

        public Floor[] floors;
        public ElevatorBox elevator;

        public int numFloors;
        public int buildingNum;
        public bool overrideColor = false;
        public bool isMoving = false;
        public DateTime lastKeyPress;

        public async void HighlightButton()
        {
            overrideColor= true;
            await Task.Delay(200);
            overrideColor = false;
        }
        public Building(int numberOfFloors, int buildingNum)
        {
            numFloors = numberOfFloors;
            lastKeyPress = DateTime.Now;
            floors = new Floor[numFloors + 1];
            elevator = new ElevatorBox();

            for (int i = 0; i <= numFloors; i++)
            {
                floors[i] = new Floor();
            }

            this.buildingNum = buildingNum;
        }

        public void HandleKeyPress(int x)
        {
            floors[x].isTarget = true;
            lastKeyPress = DateTime.Now;
            if (this.elevator.currentFloor==x && this.floors[x].isTarget) HighlightButton();
        }
        public string Next()
        {
            bool moved = false;
            if((DateTime.Now - this.lastKeyPress).TotalMilliseconds < 500 && !this.isMoving)
            {
                return "";
            }

            if (elevator.dirUp)
            {
                if (floors[elevator.currentFloor].isTarget)
                {
                    floors[elevator.currentFloor].isTarget = false;
                }

                bool nextUp = false;
                for (int i = elevator.currentFloor + 1; i <= numFloors; i++)
                {
                    if (floors[i].isTarget)
                    {
                        elevator.currentFloor++;
                        nextUp = true;
                        moved = true;
                        break;
                    }
                }

                if (!nextUp)
                {
                    elevator.dirUp = false;

                    for (int i = elevator.currentFloor - 1; i >= 1; i--)
                    {
                        if (floors[i].isTarget)
                        {
                            elevator.currentFloor--;
                            moved = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                if (floors[elevator.currentFloor].isTarget)
                {
                    floors[elevator.currentFloor].isTarget = false;
                }

                bool nextDown = false;
                for (int i = elevator.currentFloor - 1; i >= 1; i--)
                {
                    if (floors[i].isTarget)
                    {
                        elevator.currentFloor--;
                        nextDown = true;
                        moved = true;
                        break;
                    }
                }

                if (!nextDown)
                {
                    elevator.dirUp = true;
                    for (int i = elevator.currentFloor + 1; i <= numFloors; i++)
                    {
                        if (floors[i].isTarget)
                        {
                            elevator.currentFloor++;
                            moved = true;
                            break;
                        }
                    }
                }
            }

            if (moved){
                this.isMoving = true;
                return DateTime.Now.ToString("HH:mm:ss.fff") + " - Elevator #" + buildingNum + " Moved to Floor: " + elevator.currentFloor; 
            }
            this.isMoving = false;
            return "";
        }


    }
}