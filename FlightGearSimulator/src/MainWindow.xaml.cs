using FlightGearSimulator.src;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

static class Globals
{
    public static FlightModel model;
}

namespace FlightGearSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            Globals.model = new FlightModel();
            InitializeComponent();
        }
    }
}

namespace FlightGearSimulator.FIApp.Views
{
    public partial class PlayController : UserControl
    {
        FlightModel model = Globals.model;
    }
}

namespace FlightGearSimulator.FIApp.Views
{
    public partial class AnomalyDetector : UserControl
    {
        FlightModel model = Globals.model;
    }
}

namespace FlightGearSimulator.FIApp.Views
{
    public partial class FeatureList : UserControl
    {
        FlightModel model = Globals.model;
    }
}

namespace FlightGearSimulator.FIApp.Views
{
    public partial class Graphs : UserControl
    {
        FlightModel model = Globals.model;
    }
}
