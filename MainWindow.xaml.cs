using Horoscope.ViewModel;
using System.Windows;

namespace Horoscope
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HoroscopeViewModel horoscopeViewModel;
        public MainWindow()
        {
            InitializeComponent();
            horoscopeViewModel = new HoroscopeViewModel();
            DataContext = horoscopeViewModel;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
