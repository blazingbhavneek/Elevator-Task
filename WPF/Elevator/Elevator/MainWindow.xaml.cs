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
        Building building;
        private DispatcherTimer elevatorTimer;
        private DispatcherTimer displayTimer;
        public MainWindow()
        {
            InitializeComponent();
            building = new Building(6);
            elevatorTimer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(1)
                };
            elevatorTimer.Tick += (s, e) => building.Next();
            elevatorTimer.Start();

            displayTimer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(200)
                };
            displayTimer.Tick += (s, e) =>
            {
                bool dirDisp = false;
                for (int i = 1; i <= 6; i++)
                {
                    if (this.building.floors[i].isTarget)
                    {
                        dirDisp = true;
                        var childStackPanel = floorPanel.Children[6-i] as StackPanel;
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
                        var childStackPanel = floorPanel.Children[6-i] as StackPanel;
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

                if(dirDisp && this.building.elevator.dirUp) currDir.Content = "↑";
                else if(dirDisp && !this.building.elevator.dirUp) currDir.Content = "↓";
                else currDir.Content = "";
                currFloor.Content = building.elevator.currentFloor.ToString();
                switch (building.elevator.currentFloor)
                {
                    case 1:
                        I1.Background = Brushes.Green; break;
                    case 2:
                        I2.Background = Brushes.Green; break;
                    case 3:
                        I3.Background = Brushes.Green; break;
                    case 4:
                        I4.Background = Brushes.Green; break;
                    case 5:
                        I5.Background = Brushes.Green; break;
                    case 6:
                        I6.Background = Brushes.Green; break;
                    default:
                        break;
                }
            };
            displayTimer.Start();

        }

        private void B6_Click(object sender, RoutedEventArgs e) { this.building.HandleKeyPress(6); }
        private void B5_Click(object sender, RoutedEventArgs e) { this.building.HandleKeyPress(5); }
        private void B4_Click(object sender, RoutedEventArgs e) { this.building.HandleKeyPress(4); }
        private void B3_Click(object sender, RoutedEventArgs e) { this.building.HandleKeyPress(3); }
        private void B2_Click(object sender, RoutedEventArgs e) { this.building.HandleKeyPress(2); }
        private void B1_Click(object sender, RoutedEventArgs e) { this.building.HandleKeyPress(1); }


    }
}