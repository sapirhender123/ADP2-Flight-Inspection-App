using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;

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

        public void LoadPlugin(string pluginPath)
        {
            if (!File.Exists(pluginPath))
            {
                return;
            }

            Console.WriteLine(String.Format("Loading {0}", pluginPath));
            try
            {
                // Load DLL into memory
                Assembly plugin = Assembly.LoadFrom(pluginPath);

                // Get the type from the assembly
                Type t = plugin.GetType(String.Format("{0}.{0}", Path.GetFileNameWithoutExtension(pluginPath)));

                // Get the detect method from the DLL (declare the method prototype)
                MethodInfo DLLdetect = t.GetMethod("Detect", new Type[] { typeof(int[][]) });

                // Create a new instance of the ChordFileGenerator type
                object o = Activator.CreateInstance(t);
                // DLLdetect.Invoke(o, model.csvData);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return;
            }
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
