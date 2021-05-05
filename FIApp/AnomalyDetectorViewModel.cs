using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;

namespace FIApp
{
    public class AnomalyItem
    {
        private string title;
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        private String imagePath;
        public String ImagePath
        {
            get { return this.imagePath; }
            set { this.imagePath = value; }
        }

    }

    class AnomalyDetectorViewModel : INotifyPropertyChanged
    {
        public Model model;
        public AnomalyDetectorViewModel(Model model)
        {
            this.model = model;
            this.model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("AD_" + e.PropertyName);
            };
            this.model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "CurrentFeature")
                {
                    ResetAnomalyList();
                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                // The "propName" was changed, notify the relevant items
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void Detect();

        public void LoadPlugin(string pluginPath)
        {
            _ = NativeMethods.CallFuncFromDLLByName(pluginPath, "Detect");
        }

        private string DLLPluginPath;
        public string AD_DLLPluginPath
        {
            get
            {
                return DLLPluginPath;
            }
            set
            {
                DLLPluginPath = value;
                LoadPlugin(DLLPluginPath);
            }
        }

        public string AD_CurrentFeature
        {
            get
            {
                return model.CurrentFeature;
            }
        }

        List<AnomalyItem> anomalyListItems;
        public List<AnomalyItem> AnomalyListItems
        {
            set
            {
                anomalyListItems = value;
                NotifyPropertyChanged("AnomalyListItems");
            }
            get
            {
                return anomalyListItems;
            }
        }

        List<String> pluginListItems;
        public List<String> PluginListItems
        {
            set
            {
                pluginListItems = value;
                NotifyPropertyChanged("PluginListItems");
            }
            get
            {
                return pluginListItems;
            }
        }

        private int selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }

            set
            {
                selectedIndex = value;
                NotifyPropertyChanged("SelectedIndex");
            }
        }

        private String selectedItem;
        public String SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                NotifyPropertyChanged("SelectedItem");
            }
        }

        private void ResetAnomalyList()
        {
            DLLPluginPath = "";
            if (File.Exists("output\\anomalyOutputFile.csv"))
            {
                File.Delete("output\\anomalyOutputFile.csv");
            }

            List<AnomalyItem> items = new List<AnomalyItem>();
            items.Add(new AnomalyItem
            {
                Title = "No anomalies found",
                ImagePath = ""
            });
            anomalyListItems = items;
            NotifyPropertyChanged("AnomalyListItems");

            SelectedIndex = 0;
            SelectedItem = "Anomaly detection algorithm";
        }

        public int AD_CurrentTime
        {
            get
            {
                return model.CurrentTime;
            }
            set
            {
                model.CurrentTime = value;
                NotifyPropertyChanged("AD_CurrentTime");
            }
        }
    }
}
