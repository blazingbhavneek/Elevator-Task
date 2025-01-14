using System;

namespace Elevator{

    class Floor{
        public bool up = false;
        public bool down = false;

        public bool isTarget = false;
        void goUp(){
            this.up = true;
        }

        void goDown(){
            this.down = true;
        }

        void resetUp(){
            this.up = false;
        }

        void resetDown(){
            this.down = false;
        }
    }

    class Elevator{
        public int currentFloor = 1;
        public bool dirUp = true;
        public int maxFloor;

        Random random = new Random();

        public Elevator(int x){
            maxFloor = x;
        }

        public int whichFloor(){
            return this.random.Next(1, maxFloor+1);
        }
        
    }
    class Building{

        public Floor[] floors;
        public Elevator elevator;

        public int numFloors;
        Random randomReq = new Random();
        public Building(int numFloors){
            this.numFloors = numFloors;
            floors = new Floor[numFloors+1];
            elevator = new Elevator(numFloors);
            for(int i=1; i<=numFloors; i++){
                floors[i] = new Floor();
            }

            floors[1].up = true;
        }

        public void next(){
            Console.WriteLine(elevator.currentFloor + " " + elevator.dirUp);

            int nxtReq = randomReq.Next(1, this.numFloors+1);
            if(nxtReq!=elevator.currentFloor){
                int upDown = randomReq.Next(1, 3);
                if(upDown==1 && nxtReq!=1) floors[nxtReq].down = true;
                if(nxtReq!=this.numFloors) floors[nxtReq].up = true;
            }

            Console.Write("Target: ");
            for(int i=1; i<=this.numFloors; i++){
                if(floors[i].isTarget || floors[i].up || floors[i].down){
                    Console.Write(i);
                }

                if(floors[i].isTarget){
                    Console.Write("-t");
                }

                if(floors[i].up){
                    Console.Write("-u");
                }

                if(floors[i].down){
                    Console.Write("-d");
                }

                Console.Write(", ");
            }
            Console.WriteLine("");

            if(elevator.dirUp){
                if(floors[elevator.currentFloor].up){
                    // Console.WriteLine("Picked up a Guest from floor: " + elevator.currentFloor);
                    floors[elevator.currentFloor].up=false;
                    int nxt = elevator.whichFloor();
                    if(nxt!=elevator.currentFloor){
                        // Console.WriteLine("New guest wants to go to: " + nxt);
                        floors[nxt].isTarget=true;
                    }
                }

                if(floors[elevator.currentFloor].isTarget){
                    // Console.WriteLine("Dropped a Guest to floor: " + elevator.currentFloor);
                    floors[elevator.currentFloor].isTarget = false;
                    int nxt = elevator.whichFloor();
                    if(nxt!=elevator.currentFloor){
                        // Console.WriteLine("A new guest boards the elevator and wants to go to: " + nxt);
                        floors[nxt].isTarget=true;
                    }
                }

                bool nextUp = false;
                for(int i=elevator.currentFloor+1; i<=numFloors; i++){
                    if(floors[i].up){
                        elevator.currentFloor++;
                        // Console.WriteLine("Elevator Moves to floor: " + elevator.currentFloor);
                        nextUp = true;
                        break;
                    }
                }
                if(!nextUp){
                    elevator.dirUp=false;
                }
            }
            else{
                if(floors[elevator.currentFloor].down){
                    // Console.WriteLine("Picked up a Guest from floor: " + elevator.currentFloor);
                    floors[elevator.currentFloor].down = false;
                    int nxt = elevator.whichFloor();
                    if(nxt!=elevator.currentFloor){
                        // Console.WriteLine("New guest wants to go to: " + nxt);
                        floors[nxt].isTarget=true;
                    }
                }

                if(floors[elevator.currentFloor].isTarget){
                    // Console.WriteLine("Dropped a Guest to floor: " + elevator.currentFloor);
                    floors[elevator.currentFloor].isTarget = false;
                    int nxt = elevator.whichFloor();
                    if(nxt!=elevator.currentFloor){
                        // Console.WriteLine("A new guest boards the elevator and wants to go to: " + nxt);
                        floors[nxt].isTarget=true;
                    }
                }

                bool nextDown = false;
                for(int i=elevator.currentFloor-1; i>=1; i--){
                    if(floors[i].down){
                        elevator.currentFloor--;
                        // Console.WriteLine("Elevator Moves to floor: " + elevator.currentFloor);
                        nextDown = true;
                        break;
                    }
                }
                if(!nextDown){
                    elevator.dirUp=true;
                }
            }
            
            Console.WriteLine(elevator.currentFloor + " " + elevator.dirUp);
            Console.WriteLine("");
            Console.WriteLine("");
        }
        static void Main(string[] args){
            Building building = new Building(5);  // Example with 5 floors

            Timer timer = new Timer(_ => building.next(), null, 0, 3000);  // Run every 1 second

            Console.WriteLine("Press 'q' to quit.");
            while (Console.ReadKey(intercept: true).Key != ConsoleKey.Q) { }

            timer.Dispose();
            Console.WriteLine("\nExiting simulation.");
        }
    }
}