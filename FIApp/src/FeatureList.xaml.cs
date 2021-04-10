using Microsoft.VisualBasic.FileIO;
using System;
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

            using (TextFieldParser parser = new TextFieldParser("anomaly_flight.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                string [] csvHeaders = parser.ReadFields();
                int idx = 0;
                foreach (string header in csvHeaders)
                {
                    csvHeaders[idx] = String.Concat(Convert.ToString(idx), "_", header);
                    FeatureSelection.Items.Add(csvHeaders[idx]);
                    idx++;
                }
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
