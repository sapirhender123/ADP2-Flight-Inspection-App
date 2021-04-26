using System.Windows.Controls;
using System.Windows.Input;

namespace FIApp.Views
{
    /// <summary>
    /// Interaction logic for PlayControldler.xaml
    /// </summary>
    public partial class PlayController : UserControl
    {
        readonly PlayerViewModel vm;
        private decimal prev_speed = 1;
        private bool playing = false;
        public PlayController()
        {
            InitializeComponent();
            vm = new PlayerViewModel(model); // TODO: (new TCPConnection())
            vm.FG_Speed = 0;
            this.DataContext = vm;
        }

        private void PlayController_Play(object sender, MouseButtonEventArgs e)
        {
            if (!playing)
            {
                vm.FG_Speed = prev_speed;
                playing = true;
            }
        } 

        private void PlayController_FastFordward(object sender, MouseButtonEventArgs e)
        {
            if (!playing)
            {
                prev_speed += (decimal)0.1F;
            }
            else
            {
                vm.FG_Speed += (decimal)0.1F;
            }
        }

        private void PlayController_MoveBack(object sender, MouseButtonEventArgs e)
        {
            if (!playing)
            {
                if (prev_speed >= (decimal)0.1F)
                {
                    prev_speed -= (decimal)0.1F;
                }
            }
            else
            {
                if (vm.FG_Speed >= (decimal)0.1F)
                {
                    vm.FG_Speed -= (decimal)0.1F;
                }
            }
        }

        private void PlayController_Pause(object sender, MouseButtonEventArgs e)
        {
            prev_speed = vm.FG_Speed;
            vm.FG_Speed = 0;
            playing = false;
        }

        private void PlayController_Stop(object sender, MouseButtonEventArgs e)
        {
            prev_speed = 1;
            vm.FG_CurrentTime = 0;
            vm.FG_Speed = 0;
            playing = false;
        }
    }
}
