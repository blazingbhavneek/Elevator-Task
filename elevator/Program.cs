using System;

namespace Elevator
{

    class Floor
    {
        public bool isTarget = false;
    }

    class Elevator
    {
        public int currentFloor = 1;
        public bool dirUp = true;

    }
    class Building
    {

        public Floor[] floors;
        public Elevator elevator;

        public int numFloors;
        public Building(int numFloors)
        {
            this.numFloors = numFloors;
            this.floors = new Floor[numFloors + 1];
            this.elevator = new Elevator();
            for (int i = 1; i <= numFloors; i++)
            {
                this.floors[i] = new Floor();
            }
        }

        public void handleKeyPress(int x)
        {
            this.floors[x].isTarget = true;
        }
        public void next()
        {


            for (int i = 1; i <= this.numFloors; i++)
            {
                if (elevator.currentFloor == i) Console.Write("e");
                else if (floors[i].isTarget) Console.Write("t");
                else Console.Write(i);
                Console.Write(", ");
            }
            Console.WriteLine("");


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
                if (floors[elevator.currentFloor].down)
                {
                    floors[elevator.currentFloor].down = false;
                }

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

            Console.WriteLine("");
        }
        static void Main(string[] args)
        {
            Building building = new Building(10);  // Example with 5 floors

            Timer timer = new Timer(_ => building.next(), null, 0, 1000);  // Run every 1 second

            Console.WriteLine("Press 'q' to quit.");

            int storedKey = 0
            while (true)
            {
                var keyInfo = Console.ReadKey(intercept: true);  // Capture key press
                if (keyInfo.Key == ConsoleKey.Q)
                {
                    break;  // Exit the loop when 'q' is pressed
                }
                else if (keyInfo.Key >= ConsoleKey.D1 && keyInfo.Key <= ConsoleKey.D9)
                {
                    // Get the number corresponding to the pressed key (1-9)
                    storedKey = keyInfo.Key - ConsoleKey.D1 + 1;
                    building.handleKeyPress(storedKey);  // Call the handleKeyPress method to handle the key
                }
            }

            timer.Dispose();
            Console.WriteLine("\nExiting simulation.");
        }
    }
}