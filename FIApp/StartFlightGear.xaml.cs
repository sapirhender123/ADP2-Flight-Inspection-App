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
        private Model m;
        public StartFlightGear()
        {
            InitializeComponent();
            m = new Model();
            vm = new StartFlightGearViewModel(m);
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo("C:\\Program Files\\FlightGear 2020.3\\bin\\fgfs.exe");
            psi.Arguments = $"--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small --fdm=null";
            psi.WorkingDirectory = "C:\\Program Files\\FlightGear 2020.3\\data";
            Process.Start(psi);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            m.connect();
            m.start();
        }
    }
}
