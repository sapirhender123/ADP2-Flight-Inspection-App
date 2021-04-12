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
            vm.AD_DLLPluginPath = Path.Combine(pluginsPath, String.Concat(e.AddedItems[0], ".dll"));
            // Load the anomalies to the list box
            // read from csv
            InitializeAnomalies();
        }

        public void InitializePluginsSelection()
        {
            AnomalyList.Items.Clear();
            PluginSelection.Items.Clear();
            PluginSelection.Items.Insert(0, "Anomaly detection algorithm");
            PluginSelection.SelectedIndex = 0;
            foreach (string i in plugins)
            {
                // Add each DLL as an option using only its filename (without full path or extention)
                PluginSelection.Items.Add(Path.GetFileNameWithoutExtension(i));
            }
        }

        public void InitializeAnomalies()
        {
            if (vm.AD_CurrentFeature == null)
            {
                return;
            }

            List<String> items = new List<String>();
            items.Insert(0, "Anomalies");
            
            if (!File.Exists("anomalyOutputFile.csv"))
            {
                return;
            }

            using (var reader = new StreamReader("anomalyOutputFile.csv"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line.Contains(vm.AD_CurrentFeature)) {
                        items.Add(line);
                    }
                }
            }
            items.Remove("Anomalies");
            vm.AnomalyListItems = items;
        }
    }
}
