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

        public void consoleApp(string text, TextBox ConsoleTextBox)
        {
            ConsoleTextBox.AppendText(text + "\n");
            if (ConsoleTextBox.LineCount > 20)
            {
                var lines = ConsoleTextBox.Text.Split('\n');
                var newText = string.Join("\n", lines, 1, lines.Length - 1);
                ConsoleTextBox.Text = newText;
                ConsoleTextBox.CaretIndex = ConsoleTextBox.Text.Length;
            }
            ConsoleTextBox.ScrollToEnd();
        }
        public async void HighlightButton()
        {
            overrideColor = true;
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

        public void HandleKeyPress(int x, TextBox ConsoleTextBox)
        {
            floors[x].isTarget = true;
            lastKeyPress = DateTime.Now;
            if (this.elevator.currentFloor == x && this.floors[x].isTarget) HighlightButton();
            this.consoleApp($"[{buildingNum}号機]" + "現在階 < " + "選択[" + x + "]", ConsoleTextBox);
        }
        public string Next()
        {
            bool moved = false;
            if ((DateTime.Now - this.lastKeyPress).TotalMilliseconds < 400 && !this.isMoving)
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

            if (!moved)
            {
                this.isMoving = false;
                return "";
            }

            string ret = $"[{buildingNum}号機]";
            this.isMoving = true;
            if (floors[elevator.currentFloor].isTarget)
            {

                bool otherTargets = false;
                for (int i = 1; i <= numFloors; i++)
                {
                    if (floors[i].isTarget && i!= elevator.currentFloor)
                    {
                        otherTargets = true;
                    }
                }
                ret += $"{elevator.currentFloor}階でエレベーター1000ms停止";

                if (!otherTargets)
                {
                    ret += $"\n[{buildingNum}号機]{elevator.currentFloor}階でエレベーター停止";
                }

                return ret;
            }

            return "";
        }
    }
}