using FIApp;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FIApp.ViewModels;

//create a model to be shared with the views

static class Globals
{
    public static Model model = new Model();
}

namespace FIApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}


//create and initialize a model field in the views
namespace FIApp.Views
{
    public partial class JoystickView : UserControl
    {
        Model model = Globals.model;
    }
}

namespace FIApp.Views
{
    public partial class StartFlightGear : UserControl
    {
        Model model = Globals.model;
    }
}

namespace FIApp.Views
{
    public partial class FlightInstrumentsView : UserControl
    {
        Model model = Globals.model;
    }
}

namespace FIApp.Views
{
    public partial class PlayController : UserControl
    {
        Model model = Globals.model;
    }
}

namespace FIApp.Views
{
    public partial class AnomalyDetector : UserControl
    {
        Model model = Globals.model;
    }
}

namespace FIApp.Views
{
    public partial class FeatureList: UserControl
    {
        Model model = Globals.model;
    }
}

namespace FIApp.Views
{
    public partial class Graphs: UserControl
    {
        Model model = Globals.model;
    }
}

