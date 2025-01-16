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
    public partial class MainWindow : Window
    {
        Building building1;
        Building building2;
        private void Runner(Building building, StackPanel floorPanel, Label currDir, Label currFloor)
        {
            DispatcherTimer elevatorTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            elevatorTimer.Tick += (s, e) => building.Next();
            elevatorTimer.Start();

            DispatcherTimer displayTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(200)
            };
            displayTimer.Tick += (s, e) =>
            {
                bool dirDisp = false;
                for (int i = 1; i <= 6; i++)
                {
                    var childStackPanel = floorPanel.Children[6 - i] as StackPanel;
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

                if (dirDisp && building.elevator.dirUp) currDir.Content = "↑";
                else if (dirDisp && !building.elevator.dirUp) currDir.Content = "↓";
                else currDir.Content = "";
                currFloor.Content = building.elevator.currentFloor.ToString();

                var childPanel = floorPanel.Children[6 - building.elevator.currentFloor] as StackPanel;
                if (childPanel != null && childPanel.Children.Count > 1)
                {
                    var label = childPanel.Children[1] as Label;
                    if (label != null)
                    {
                        label.Background = Brushes.Green;
                    }
                }

            };
            displayTimer.Start();

        }
        public MainWindow()
        {
            InitializeComponent();
            building1 = new Building(6);
            building2 = new Building(6);
            Runner(building1, floorPanel, currDir, currFloor);
            Runner(building2, floorPanel2, currDir2, currFloor2);
        }

        private void B6_Click(object sender, RoutedEventArgs e) { this.building1.HandleKeyPress(6); }
        private void B5_Click(object sender, RoutedEventArgs e) { this.building1.HandleKeyPress(5); }
        private void B4_Click(object sender, RoutedEventArgs e) { this.building1.HandleKeyPress(4); }
        private void B3_Click(object sender, RoutedEventArgs e) { this.building1.HandleKeyPress(3); }
        private void B2_Click(object sender, RoutedEventArgs e) { this.building1.HandleKeyPress(2); }
        private void B1_Click(object sender, RoutedEventArgs e) { this.building1.HandleKeyPress(1); }


        private void B6_Click2(object sender, RoutedEventArgs e) { this.building2.HandleKeyPress(6); }
        private void B5_Click2(object sender, RoutedEventArgs e) { this.building2.HandleKeyPress(5); }
        private void B4_Click2(object sender, RoutedEventArgs e) { this.building2.HandleKeyPress(4); }
        private void B3_Click2(object sender, RoutedEventArgs e) { this.building2.HandleKeyPress(3); }
        private void B2_Click2(object sender, RoutedEventArgs e) { this.building2.HandleKeyPress(2); }
        private void B1_Click2(object sender, RoutedEventArgs e) { this.building2.HandleKeyPress(1); }


    }
}