using FIApp;
using Microsoft.VisualBasic.FileIO;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace FlApp
{
    class GraphsViewModel : INotifyPropertyChanged
    {
        public FIApp.Model model;
        public event PropertyChangedEventHandler PropertyChanged;

        private string currentFeature;
        private string correlativeFeature;
        private double Xcor;
        private double Ycor;

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
        private List<DataPoint> points;
        public List<DataPoint> Points
        {
            set
            {
                points = value;
            }
            get
            {
                return points;
            }
        }
        public List<DataPoint> RegFeaturePoints { get; set; }

        public GraphsViewModel(FIApp.Model model)
        {
            this.model = model;
            this.model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("GR_" + e.PropertyName);
            };
            this.model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "CurrentFeature" || e.PropertyName == "CurrentTime")
                {
                    UpdateGraphData(e.PropertyName);
                }
            };
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                // The "propName" was changed, notify the relevant items
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }

        private void UpdateGraphData(string propName)
        {
            if (propName == "CurrentFeature" && model.CurrentFeature != "Select a feature")
            {
                currentFeature = model.CurrentFeature;

                if (!File.Exists("learnOutput.csv"))
                {
                    NativeMethods.CallFuncFromDLLByName("LearnNormalDLL.dll", "LearnNormal");
                }

                // Parse LearnNormal output -> in file learnOutput.csv
                using (TextFieldParser parser = new TextFieldParser("learnOutput.csv"))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    correlativeFeature = null;
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        // feature1,feature2,a,b
                        if (fields[0] == model.CurrentFeature)
                        {
                            correlativeFeature = fields[1];
                            Xcor = Convert.ToDouble(fields[2]);
                            Ycor = Convert.ToDouble(fields[3]);
                            break;
                        }
                    }
                }
            }

            if (Title == "Orignal Graph")
            {
                this.points = OrignalGraphPoints;
            }
            else if (correlativeFeature != null && Title == "Correlative Graph")
            {
                this.points = CorrelativePoints;
            }
            else if (correlativeFeature != null && Title == "Linear Regression")
            {
                this.points = LinearRegression;
                NotifyPropertyChanged("LinearReg");
            }
            else
            {
                this.points = new List<DataPoint> { };
            }
            NotifyPropertyChanged("Points");
        }

        private List<DataPoint> createDataPointList(string feature)
        {
            List<double> csvPoints = model.getDataByFeatureName(feature);
            List<DataPoint> points = new List<DataPoint> { };
            int ts = 0;
            foreach (double dataPoint in csvPoints)
            {
                points.Add(new DataPoint(ts, dataPoint));
                ts++;
                if (ts / 10 >= model.CurrentTime)
                {
                    break;
                }
            }

            return points;
        }

        private List<DataPoint> CalcYRegLine(double a, double b, List<double> regLineX)
        {
            List<DataPoint> points = new List<DataPoint>();
            double regLineY;
            for (int i = 0; i < regLineX.Count(); i++)
            {
                // y     = a *     x       + b    
                regLineY = a * regLineX[i] + b;
                points.Add(new DataPoint(regLineX[i], regLineY));
            }
            return points;
        }

        public List<DataPoint> OrignalGraphPoints
        {
            get
            {
                return createDataPointList(this.currentFeature);
            }
        }

        public List<DataPoint> CorrelativePoints
        {
            get
            {
                return createDataPointList(this.correlativeFeature);
            }
        }

        public List<DataPoint> LinearRegression
        {
            get
            {
                List<double> currentFeaturePoints = model.getDataByFeatureName(this.currentFeature);
                List<double> correlativeFeaturePoints = model.getDataByFeatureName(this.correlativeFeature);
                if (correlativeFeaturePoints.Count == 0)
                {
                    return new List<DataPoint> { };
                }

                int idx = 0;
                int end;
                if (model.CurrentTime > 30)
                {
                    idx = model.CurrentTime * 10 - 300;
                    if (model.CurrentTime + 30 <= model.maxTime_s)
                    {
                        end = idx + 300;
                    } else
                    {
                        end = model.maxTime_s * 10;
                    }
                } else
                {
                    end = model.CurrentTime * 10;
                }

                RegFeaturePoints = new List<DataPoint>();
                for (int i = idx; i < end; i++)
                {
                    RegFeaturePoints.Add(new DataPoint(currentFeaturePoints[i], correlativeFeaturePoints[i]));
                }
                NotifyPropertyChanged("RegFeaturePoints");

                return CalcYRegLine(Xcor, Ycor, currentFeaturePoints);
            }
        }
    }
}
