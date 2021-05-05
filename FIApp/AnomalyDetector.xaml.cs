using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace FIApp.Views
{
    /// <summary>
    /// Interaction logic for AnomalyDetector.xaml
    /// </summary>
    public partial class AnomalyDetector : UserControl
    {
        readonly AnomalyDetectorViewModel vm;
        static readonly string pluginsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "plugins");
        readonly string[] plugins = Directory.GetFiles(pluginsPath, "*.dll");

        public AnomalyDetector()
        {
            InitializeComponent();

            vm = new AnomalyDetectorViewModel(model);
            this.DataContext = vm;

            InitializePluginsSelection();
        }

        // Update the detection according to the choice and load the anomalies to the list box
        private void PluginSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            vm.AD_DLLPluginPath = Path.Combine(pluginsPath, String.Concat(e.AddedItems[0], ".dll"));
            // Load the anomalies to the list box
            // read from csv
            InitializeAnomalies();
        }

        public void InitializePluginsSelection()
        {
            List<String> items = new List<String>();
            items.Add("Anomaly detection algorithm");
            foreach (string i in plugins)
            {
                // Add each DLL as an option using only its filename (without full path or extention)
                items.Add(Path.GetFileNameWithoutExtension(i));
            }

            vm.PluginListItems = items;
            PluginSelection.SelectedIndex = 0;
        }

        public void InitializeAnomalies()
        {
            if (vm.AD_CurrentFeature == null)
            {
                return;
            }

            List<AnomalyItem> items = new List<AnomalyItem>();
            
            if (!File.Exists("output\\anomalyOutputFile.csv"))
            {
                return;
            }

            string imagePath = "";
            if (vm.AD_DLLPluginPath.Contains("Circle"))
            {
                imagePath = @"resources\CircleAnomaly.png";
            } else if (vm.AD_DLLPluginPath.Contains("Regression"))
            {
                imagePath = @"resources\RegressionLineAnomaly.png";
            }

            using (var reader = new StreamReader("output\\anomalyOutputFile.csv"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line.Contains(vm.AD_CurrentFeature))
                    {
                        string []parts = line.Split(',');

                        TimeSpan t = TimeSpan.FromSeconds(Convert.ToInt32(parts[0]) / 10);
                        string timeString = string.Format("{0:D2}:{1:D2}:{2:D2}_",
                                        t.Hours,
                                        t.Minutes,
                                        t.Seconds);
                        parts[0] = timeString;
                        items.Add(new AnomalyItem { Title = String.Concat(parts), ImagePath = imagePath });
                    }
                }

                if (items.Count == 0)
                {
                    items.Add(new AnomalyItem
                    {
                        Title = "No anomalies found",
                        ImagePath = imagePath
                    });
                }
            }

            vm.AnomalyListItems = items;
        }

        private void AnomalyList_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock src = (TextBlock)e.Source;
            if (src.Text == "No anomalies found")
            {
                return;
            }

            // Parse time string from hh:mm:ss to timestep
            string time = src.Text.Split('_')[0];
            string[] parts = time.Split(':');
            int timestep = Convert.ToInt32(parts[0]) * 60 * 60 + Convert.ToInt32(parts[1]) * 60 + Convert.ToInt32(parts[2]);
            
            vm.AD_CurrentTime = timestep;
        }
    }
}
