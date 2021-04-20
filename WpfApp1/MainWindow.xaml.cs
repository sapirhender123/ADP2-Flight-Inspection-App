using FIApp;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FIApp.ViewModels;

static class Globals
{
    public static Model model = new Model();
}

namespace FIApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
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
    public partial class JoystickView : UserControl
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

