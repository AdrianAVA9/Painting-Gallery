using System.Windows;
using System.Windows.Controls;

namespace PaintingGallery.UI
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

        private void BtnPatron_Click(object sender, RoutedEventArgs e)
        {
            DisplayScreen(new Screens.RegisterPatron());
        }

        private void DisplayScreen(UserControl userControl)
        {
            MainPanel.Children.Clear();

            MainPanel.Children.Add(userControl);
        }
    }
}
