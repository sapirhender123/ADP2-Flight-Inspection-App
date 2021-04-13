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

            int idx = 0;
            FeatureSelection.Items.Add("Select a feature");
            foreach (string header in model.features)
            {
                FeatureSelection.Items.Add(model.features[idx]);
                idx++;
            }
            FeatureSelection.SelectedIndex = 0;
        }

        // change the selection and update
        private void Feature_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // change the feature
            File.Delete("learnOutput.csv");
            model.CurrentFeature = (string)e.AddedItems[0];
        }
    }
}
