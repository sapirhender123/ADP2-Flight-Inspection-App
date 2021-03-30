using System.Windows;

namespace FlightGearSimulator.src
{
    /// <summary>
    /// Interaction logic for PlayController.xaml
    /// </summary>
    public partial class PlayController : Window
    {
        readonly PlayerViewModel vm;
        public PlayController()
        {
            InitializeComponent();
            vm = new PlayerViewModel(null); // TODO: new PlayerModel(new TCPConnection())
            DataContext = vm;
        }
    }
}
