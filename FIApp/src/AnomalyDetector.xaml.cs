using System;
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

        // Update the detection according to the choice
        private void PluginSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.AD_DLLPluginPath = Path.Combine(pluginsPath, String.Concat(e.AddedItems[0], ".dll"));
        }

        public void InitializePluginsSelection()
        {
            PluginSelection.Items.Insert(0, "Anomaly detection algorithm");
            PluginSelection.SelectedIndex = 0;
            foreach (string i in plugins)
            {
                // Add each DLL as an option using only its filename (without full path or extention)
                PluginSelection.Items.Add(Path.GetFileNameWithoutExtension(i));
            }
        }
    }
}