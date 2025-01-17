using System;
using System.Windows;

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
        public Building(int numberOfFloors)
        {
            numFloors = numberOfFloors;

            floors = new Floor[numFloors + 1];
            elevator = new ElevatorBox();

            for (int i = 0; i <= numFloors; i++)
            {
                floors[i] = new Floor();
            }
        }

        public void HandleKeyPress(int x)
        {
            floors[x].isTarget = true;
        }
        public void Next()
        {

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
                        break;
                    }
                }
                if (!nextUp)
                {
                    elevator.dirUp = false;
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
                        break;
                    }
                }
                if (!nextDown)
                {
                    elevator.dirUp = true;
                }
            }

        }

    }
}