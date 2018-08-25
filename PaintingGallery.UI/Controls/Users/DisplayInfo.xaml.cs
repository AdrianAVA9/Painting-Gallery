using PaintingGallery.UI.Screens;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PaintingGallery.UI.Controls.Users
{
    /// <summary>
    /// Interaction logic for DisplayInfo.xaml
    /// </summary>
    public partial class DisplayInfo : UserControl
    {
        private DisplayInfo()
        {
            InitializeComponent();
        }

        public static DisplayInfo GetInfoContainer(string name, int id, string pictureUrl)
        {
            var infoContainer = new DisplayInfo();

            infoContainer.CardTitle.Content = name;

            infoContainer.CardPicture.Source = new BitmapImage(new System.Uri(pictureUrl));

            infoContainer.ViewMoreButton.Tag = id;

            return infoContainer;
        }

        private void ViewMore(object sender, System.Windows.RoutedEventArgs e)
        {
            var id = ((Button)sender).Tag;

            MainWindow.DisplayScreen(new RegisterPatron(int.Parse(id.ToString())));

        }
    }
}
