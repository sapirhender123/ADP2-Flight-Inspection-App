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

        private void PlayController_Play(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("PlayController_Play");
            vm.FG_Speed = prev_speed;
        }

        private void PlayController_FastFordward(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("PlayController_FastFordward");
            vm.FG_Speed += 0.1f;
            Console.WriteLine(vm.model.Speed);
        }

        private void PlayController_MoveBack(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("PlayController_MoveBack");
            vm.FG_Speed -= 0.1f;
            Console.WriteLine(vm.model.Speed);
        }

        private void PlayController_Pause(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("PlayController_Pause");
            prev_speed = vm.FG_Speed;
            vm.FG_Speed = 0;
        }

        private void PlayController_Stop(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("PlayController_Stop");
            vm.FG_Time = 0;
        }
    }
}
