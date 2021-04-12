using Microsoft.VisualBasic.FileIO;
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
            foreach (string header in model.features)
            {
                FeatureSelection.Items.Add(model.features[idx]);
                idx++;
            }
        }

        // change the selection and update
        private void Feature_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // change the feature
            model.CurrentFeature = (string)e.AddedItems[0];
        }
    }
}
