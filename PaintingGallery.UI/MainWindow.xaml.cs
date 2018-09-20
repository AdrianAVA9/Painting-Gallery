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
<<<<<<< HEAD

            _instance = this;
        }

        private static MainWindow _instance { get; set; }

        private void BtnPatron_Click(object sender, RoutedEventArgs e)
        {
            DisplayScreen(new Screens.ShowPatrons());
        }

        //public static void DisplayMessage(MessageControl control)
        //{
        //    _instance.MainPanel.Children.Add(control);
        //    control.Visibility = Visibility.Visible;
        //    //control.ShowDialog();
        //}

        public static void DisplayScreen(UserControl userControl)
        {
            _instance.MainPanel.Children.Clear();

            _instance.MainPanel.Children.Add(userControl);
=======
        }

        private void BtnPatron_Click(object sender, RoutedEventArgs e)
        {
            DisplayScreen(new Screens.RegisterPatron());
        }

        private void DisplayScreen(UserControl userControl)
        {
            MainPanel.Children.Clear();

            MainPanel.Children.Add(userControl);
>>>>>>> 21ba783075123c3610b1f1dab7f6579cf394c4f0
        }
    }
}
