using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace FIApp
{
    static class Globals
    {
        public static Model model = new Model();
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us"); // Change exception language
            Globals.model.helper();
            InitializeComponent();
        }
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
