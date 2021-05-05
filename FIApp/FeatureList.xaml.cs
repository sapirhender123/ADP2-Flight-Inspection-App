using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Windows.Controls;

namespace FIApp.Views
{
    /// <summary>
    /// Interaction logic for FeatureList.xaml
    /// </summary>
    public partial class FeatureList : UserControl
    {
        public FeatureList()
        {
            InitializeComponent();
        }

        // change the selection and update
        private void Feature_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // change the feature
            if (File.Exists("output\\learnOutput.csv")) {
                File.Delete("output\\learnOutput.csv");
            }

            if (e.AddedItems.Count > 0)
            {
                model.CurrentFeature = (string)e.AddedItems[0];
            }
        }

        private void FeatureSelection_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            int idx = 0;
            FeatureSelection.Items.Clear();
            FeatureSelection.Items.Add("Select a feature");
            foreach (string header in model.features)
            {
                FeatureSelection.Items.Add(model.features[idx]);
                idx++;
            }
            FeatureSelection.SelectedIndex = 0;
        }
    }
}
