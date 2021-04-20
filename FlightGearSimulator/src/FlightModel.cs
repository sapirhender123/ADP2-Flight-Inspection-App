using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace FlightGearSimulator.src
{
    // TODO: Actually implement, this is only a stub
    public interface IFlightModel
    {
        event PropertyChangedEventHandler PropertyChanged;
    }

    public class FlightModel : IFlightModel
    {
        string currentFeature;
        public string CurrentFeature
        {
            set
            {
                this.currentFeature = value;
                NotifyPropertyChanged("CurrentFeature");
            }
            get
            {
                return this.currentFeature;
            }
        }

        public int maxTime_s;
        public FlightModel() {
            parseCSV("reg_flight.csv");
            (new Thread(() =>
            {
                while (true)
                {
                    if (Speed == 0)
                    {
                        Thread.Sleep(1000);
                        NotifyPropertyChanged("CurrentTime");
                        continue;
                    }
                    Thread.Sleep(Convert.ToInt32(1000 / Speed));
                    CurrentTime++;
                    if (CurrentTime >= maxTime_s)
                    {
                        CurrentTime = maxTime_s;
                    }

                    NotifyPropertyChanged("CurrentTime");
                }
            })).Start();
        }

        public int CurrentTime { get; set; }
        public float Speed { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private string[] csvHeaders;
        private SortedDictionary<string, List<double>> csvData;

        public void parseCSV(string path)
        {
            csvData = new SortedDictionary<string, List<double>>();
            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                csvHeaders = parser.ReadFields();
                int idx = 0;
                foreach (string header in csvHeaders)
                {
                    csvHeaders[idx] = String.Concat(Convert.ToString(idx), "_", header);
                    csvData[csvHeaders[idx]] = new List<double>();
                    idx++;
                }

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    int columnIndex = 0;
                    foreach (string field in fields)
                    {
                        csvData[csvHeaders[columnIndex]].Add(Convert.ToDouble(field));
                        columnIndex++;
                    }
                }

                maxTime_s = csvData[csvHeaders[0]].Count / 10;
                Console.WriteLine(String.Format("Simulation time: {0} seconds", maxTime_s));
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                // The "propName" was changed, notify the relevant items
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }

        public List<double> getDataByFeatureName(string feature)
        {
            if (feature == null)
            {
                return new List<double> { };
            }

            return csvData[feature];
        }
    }
}
