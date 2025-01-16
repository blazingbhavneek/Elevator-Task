﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Elevator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

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
            floors = new Floor[numFloors + 1];
            elevator = new Elevator();
            for (int i = 1; i <= numFloors; i++)
            {
                this.floors[i] = new Floor();
            }
        }

        public void HandleKeyPress(int x)
        {
            floors[x].isTarget = true;
        }
        public void Next()
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
        public partial class MainWindow : Window
    {
        public MainWindow()
        {
                Building building = new Building(10);  // Example with 5 floors

                Timer timer = new Timer(_ => building.Next(), null, 0, 1000);  // Run every 1 second

                Console.WriteLine("Press 'q' to quit.");

                int storedKey = 0;
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
                        building.HandleKeyPress(storedKey);  // Call the handleKeyPress method to handle the key
                    }
                }

                timer.Dispose();
                Console.WriteLine("\nExiting simulation.");
                InitializeComponent();
                myButton.FontSize = 48;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}