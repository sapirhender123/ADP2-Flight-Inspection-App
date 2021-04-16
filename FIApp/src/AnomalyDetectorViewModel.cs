﻿using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;

namespace FIApp
{
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
            if (!File.Exists("reg_flight.csv") || !File.Exists("anomaly_flight.csv"))
            {
                Console.WriteLine("Missing files");
                MessageBox.Show("Missing files", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }
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
    }
}
