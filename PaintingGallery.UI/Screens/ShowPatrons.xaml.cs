using PaintingGallery.UI.Controls.Information;
using PaintingGallery.UI.Controls.Users;
using PaintingGallery.UI.Services;
using System.Windows.Controls;

namespace PaintingGallery.UI.Screens
{
    /// <summary>
    /// Interaction logic for ShowPatrons.xaml
    /// </summary>
    public partial class ShowPatrons : UserControl
    {
        private PatronServices _services { get; set; }

        public ShowPatrons()
        {
            InitializeComponent();
            _services = new PatronServices();
            ShowInfo();
        }

        public async void ShowInfo()
        {
            try
            {
                ControlInfoContainer.Children.Clear();
                lblAreTherePatronsToShow.Visibility = System.Windows.Visibility.Hidden;

                var listPatrons = await _services.GetPatrons();

                if (listPatrons != null)
                {
                    foreach (var patron in listPatrons)
                        ControlInfoContainer.Children.Add(DisplayInfo.GetInfoContainer(patron.Name, patron.Id, patron.Picture));
                }
                else
                {
                    lblAreTherePatronsToShow.Visibility = System.Windows.Visibility.Visible;
                }
            }
            catch (System.Exception ex)
            {
                new MessageControl("Error", ex.Message, MessageType.Error);
            }
        }

        private void BtnAddNewPatron_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.DisplayScreen(new RegisterPatron());
        }
    }
}
