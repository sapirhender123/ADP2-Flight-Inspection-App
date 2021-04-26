using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using FIApp.ViewModels;

namespace FIApp.Views
{
    /// <summary>
    /// Interaction logic for StartFlightGear.xaml
    /// </summary>
    public partial class StartFlightGear : UserControl
    {
        private StartFlightGearViewModel vm;
        public StartFlightGear()
        {
            InitializeComponent();
            vm = new StartFlightGearViewModel(model);
            DataContext = vm;
        }

        //choose a csv file to upload
        private void button_click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                string path = ofd.FileName;
                vm.VM_CsvPath = path;
                //print the chosen path for approval
                MessageBox.Show(path);
            }

        }

        //open the flightGear simulator
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            //pass necessary data to flightGear: the program's executable's path, communication details
            ProcessStartInfo psi = new ProcessStartInfo("C:\\Program Files\\FlightGear 2020.3\\bin\\fgfs.exe");
            psi.Arguments = $"--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small --fdm=null";
            psi.WorkingDirectory = "C:\\Program Files\\FlightGear 2020.3\\data";
            Process.Start(psi);
        }

        //start the flight inspection process
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            vm.startFlight();
        }

    }
}