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
                    Interval = TimeSpan.FromMilliseconds(100)
                };
            displayTimer.Tick += (s, e) =>
            {
                currFloor.Content = building.elevator.currentFloor.ToString();
            };
            displayTimer.Start();

        }

        private void B6_Click(object sender, RoutedEventArgs e)
        {
            this.building.HandleKeyPress(6);
        }
    }
}