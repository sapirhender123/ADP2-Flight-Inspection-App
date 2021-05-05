using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using FIApp.ViewModels;
using System.IO;
using System;
using System.Security.AccessControl;

namespace FIApp.Views
{
    /// <summary>
    /// Interaction logic for StartFlightGear.xaml
    /// </summary>
    public partial class StartFlightGear : UserControl
    {
        bool fileSelected = false;
        bool flightGearStarted = false;
        bool appStarted = false;

        private StartFlightGearViewModel vm;
        public StartFlightGear()
        {
            InitializeComponent();
            vm = new StartFlightGearViewModel(model);
            DataContext = vm;
        }

        // Create a NON READ ONLY directory
        private void createDirectory(string name)
        {
            if (!Directory.Exists(name))
            {
                DirectorySecurity securityRules = new DirectorySecurity();
                securityRules.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));
                DirectoryInfo di = Directory.CreateDirectory(name, securityRules);
            }
        }

        //choose a csv file to upload
        private void fileSelection_click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                createDirectory("input");
                createDirectory("output");

                string path = ofd.FileName;
                vm.VM_CsvPath = path;
                //print the chosen path for approval
                MessageBox.Show(path);

                // Copy selected file to input directory under the name "anomaly_flight.csv"
                // Required by LearnNormalDLL.dll
                string anomalyFlight = "input\\anomaly_flight.csv";
                File.Copy(path, anomalyFlight, true); // True means override is allowed

                // Copy selected file to input directory under the name "reg_flight.csv"
                // Required by LearnNormalDLL.dll
                string regFlight = Path.Combine("input\\reg_flight.csv");
                string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
                string srcRegFlight = string.Format("{0}Resources\\reg_flight.csv", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
                File.Copy(srcRegFlight, regFlight, true); // True means override is allowed
            }

            fileSelected = true;
        }

        //open the flightGear simulator
        private void loadFlightGear_Click(object sender, RoutedEventArgs e)
        {
            if (!fileSelected)
            {
                MessageBox.Show("Choose a csv file to upload first");
                return;
            }
            //pass necessary data to flightGear: the program's executable's path, communication details
            ProcessStartInfo psi = new ProcessStartInfo("C:\\Program Files\\FlightGear 2020.3\\bin\\fgfs.exe");
            psi.Arguments = $"--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small --fdm=null";
            psi.WorkingDirectory = "C:\\Program Files\\FlightGear 2020.3\\data";
            Process.Start(psi);

            flightGearStarted = true;
        }

        //start the flight inspection process
        private void startApp_Click(object sender, RoutedEventArgs e)
        {
            if (appStarted)
            {
                return;
            }

            if (!flightGearStarted)
            {
                MessageBox.Show("Please click to open Flight Gear first");
                return;
            }

            flightGearStarted = true;
            vm.startFlight();
            appStarted = true;
        }

    }
}