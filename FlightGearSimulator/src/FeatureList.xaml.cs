using FlightGearSimulator.src;
using FlightGearSimulator.src.FlApp;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace FlightGearSimulator.FIApp.Views
{
    /// <summary>
    /// Interaction logic for FeatureList.xaml
    /// </summary>
    public partial class FeatureList : UserControl
    {
        public FeatureList()
        {
            InitializeComponent();
            FeatureSelection.Items.Add("Choose a feature");
            FeatureSelection.SelectedIndex = 0;
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
