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


        public static Tuple<StackPanel, StackPanel, Label, Label> GenerateElevatorUI(int floors, Building building, string columnName)
        {
            StackPanel columnPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(10)
            };

            StackPanel statusPanel = new StackPanel
            {
                Height = 120,
                Width = 80,
                Background = Brushes.Black,
                Margin = new Thickness(10)
            };

            Label currDir = new Label
            {
                Name = $"{columnName}_currDir",
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 32,
                Foreground = Brushes.White,
                Content = "↑"
            };

            Label currFloor = new Label
            {
                Name = $"{columnName}_currFloor",
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 48,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                Content = "1"
            };

            statusPanel.Children.Add(currDir);
            statusPanel.Children.Add(currFloor);
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
                    Width = 40,
                    Height = 60,
                    Margin = new Thickness(20),
                    FontSize = 30
                };

                floorButton.Click += (sender, e) => building.HandleKeyPress(currentFloor);

                Label floorIndicator = new Label
                {
                    Name = $"{columnName}_I{i}",
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(2),
                    Width = 50,
                    Height = 50
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

            elevatorTimer.Tick += (s, e) => building.Next();

            DispatcherTimer displayTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(200)
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
                Building building = new Building(floors);
                var col = GenerateElevatorUI(floors, building, $"col{i}");
                flexGrid.Children.Add(col.Item1);
                Runner(building, col.Item2, col.Item3, col.Item4);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            createElevators(5, 5);
        }


    }
}