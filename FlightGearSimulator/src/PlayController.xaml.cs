using System;
using System.Windows;
using System.Windows.Input;

namespace FlightGearSimulator.src
{
    /// <summary>
    /// Interaction logic for PlayController.xaml
    /// </summary>
    public partial class PlayController : Window
    {
        readonly PlayerViewModel vm;
        private float prev_speed = 1;
        public PlayController()
        {
            InitializeComponent();
            vm = new PlayerViewModel(new FlightModel()); // TODO: (new TCPConnection())
            vm.FG_Speed = 0;
            DataContext = vm;
        }

        private void PlayController_Play(object sender, MouseButtonEventArgs e)
        {
            if (vm.FG_Speed == 0)
            {
                vm.FG_Speed = prev_speed;
            }
        }

        private void PlayController_FastFordward(object sender, MouseButtonEventArgs e)
        {
            if (vm.FG_Speed == 0)
            {
                prev_speed += 0.1f;
            } else
            {
                vm.FG_Speed += 0.1f;
            }
            Console.WriteLine(vm.model.Speed);
        }

        private void PlayController_MoveBack(object sender, MouseButtonEventArgs e)
        {
            if (vm.FG_Speed == 0)
            {
                prev_speed -= 0.1f;
                if (prev_speed < 0)
                {
                    prev_speed = 0;
                }
            }
            else if (vm.FG_Speed > 0) 
            {
                vm.FG_Speed -= 0.1f;
                if (vm.FG_Speed < 0)
                {
                    vm.FG_Speed = 0;
                }
            }
        }

        private void PlayController_Pause(object sender, MouseButtonEventArgs e)
        {
            prev_speed = vm.FG_Speed;
            vm.FG_Speed = 0;
        }

        private void PlayController_Stop(object sender, MouseButtonEventArgs e)
        {
            prev_speed = 1;
            vm.FG_CurrentTime = 0;
            vm.FG_Speed = 0;
        }
    }
}
