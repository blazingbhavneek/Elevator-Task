using System.Net.NetworkInformation;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Elevator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    
    public partial class MainWindow : Window
    {
        public int ConsoleLines = 1;
        public void consoleApp(string text)
        {
            ConsoleTextBox.AppendText(ConsoleLines + ": " + text + "\n");
            ConsoleLines++;
            if (ConsoleTextBox.LineCount > 20)
            {
                var lines = ConsoleTextBox.Text.Split('\n');
                var newText = string.Join("\n", lines, 1, lines.Length - 1);
                ConsoleTextBox.Text = newText;
                ConsoleTextBox.CaretIndex = ConsoleTextBox.Text.Length;
            }
            ConsoleTextBox.ScrollToEnd();
        }
        public static Tuple<StackPanel, StackPanel, Label, Label> GenerateElevatorUI(int floors, Building building, string columnName, int buildingNum)
        {
            StackPanel columnPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(1)
            };

            StackPanel statusPanel = new StackPanel
            {
                Height = 60,
                Width = 40,
                Background = Brushes.Black,
                Margin = new Thickness(1)
            };

            Label buildingNumLabel = new Label
            {
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 16,
                Content = "Elevator \n #" + buildingNum 
            };

            Label currDir = new Label
            {
                Name = $"{columnName}_currDir",
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Foreground = Brushes.White,
                Content = "↑"
            };

            Label currFloor = new Label
            {
                Name = $"{columnName}_currFloor",
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                Content = "1"
            };

            statusPanel.Children.Add(currDir);
            statusPanel.Children.Add(currFloor);
            columnPanel.Children.Add(buildingNumLabel);
            columnPanel.Children.Add(statusPanel);

            StackPanel floorPanel = new StackPanel
            {
                Name = $"{columnName}_floorPanel",
                Orientation = Orientation.Vertical
            };

            for (int i = floors; i >= 1; i--)
            {

                int currentFloor = i;
                StackPanel floorRow = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                Button floorButton = new Button
                {
                    Name = $"{columnName}_B{i}",
                    Content = i.ToString(),
                    Width = 20,
                    Height = 30,
                    Margin = new Thickness(5),
                    FontSize = 18
                };

                floorButton.Click += (sender, e) => building.HandleKeyPress(currentFloor);

                Label floorIndicator = new Label
                {
                    Name = $"{columnName}_I{i}",
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(2),
                    Width = 20,
                    Height = 20
                };

                floorRow.Children.Add(floorButton);
                floorRow.Children.Add(floorIndicator);
                floorPanel.Children.Add(floorRow);
            }

            columnPanel.Children.Add(floorPanel);
            var ret = new Tuple<StackPanel, StackPanel, Label, Label>(columnPanel, floorPanel, currDir, currFloor);
            return ret;
        }
        private void Runner(Building building, StackPanel floorPanelVar, Label currDirVar, Label currFloorVar)
        {

            DispatcherTimer elevatorTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            elevatorTimer.Tick += (s, e) =>
            {
                String res = building.Next();
                if(res != "")
                {
                    consoleApp(res);
                }
            };

            DispatcherTimer displayTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(50)
            };

            displayTimer.Tick += (s, e) =>
            {
                bool dirDisp = false;
                for (int i = 1; i <= building.numFloors; i++)
                {
                    var childStackPanel = floorPanelVar.Children[building.numFloors - i] as StackPanel;
                    if (building.floors[i].isTarget)
                    {
                        dirDisp = true;
                        if (childStackPanel != null && childStackPanel.Children.Count > 1)
                        {
                            var label = childStackPanel.Children[1] as Label;
                            if (label != null)
                            {
                                label.Background = Brushes.Yellow;
                            }
                        }
                    }

                    else
                    {
                        if (childStackPanel != null && childStackPanel.Children.Count > 1)
                        {
                            var label = childStackPanel.Children[1] as Label;
                            if (label != null)
                            {
                                label.Background = Brushes.Transparent;
                            }
                        }
                    }
                }

                if (dirDisp && building.elevator.dirUp) currDirVar.Content = "↑";
                else if (dirDisp && !building.elevator.dirUp) currDirVar.Content = "↓";
                else currDirVar.Content = "";
                currFloorVar.Content = building.elevator.currentFloor.ToString();

                var childPanel = floorPanelVar.Children[building.numFloors - building.elevator.currentFloor] as StackPanel;
                if (childPanel != null && childPanel.Children.Count > 1)
                {
                    var label = childPanel.Children[1] as Label;
                    if (label != null)
                    {
                        label.Background = Brushes.Green;
                    }
                }

            };
            elevatorTimer.Start();
            displayTimer.Start();
        }

        public void createElevators(int x, int floors)
        {
            for (int i = 1; i <= x; i++)
            {
                Building building = new Building(floors, i);
                var col = GenerateElevatorUI(floors, building, $"col{i}", i);
                flexGrid.Children.Add(col.Item1);
                Runner(building, col.Item2, col.Item3, col.Item4);
            }
        }
        public MainWindow()
        {
            int numFloors = 6;
            int numElevators = 5;
            InitializeComponent();
            Main.Width = 100 * numElevators;
            createElevators(numElevators, numFloors);
        }
    }
}