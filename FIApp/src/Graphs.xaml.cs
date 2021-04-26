using FlApp;
using OxyPlot;
using System.Collections.Generic;
using System.Windows.Controls;

namespace FIApp.Views
{
    /// <summary>
    /// Interaction logic for Graphs.xaml
    /// </summary>
    public partial class Graphs : UserControl
    {
        readonly GraphsViewModel vm;
        public List<DataPoint> Points {
            get
            {
                return vm.Points;
            }
        }

        public Graphs()
        {
            vm = new GraphsViewModel(model);
            InitializeComponent();

            this.DataContext = vm;
        }

        private string title;
        public string Title { 
            get {
                return title;
            }
            set {
                title = value;
                vm.Title = title;
            }
        }
    }
}
