using Microsoft.Win32;
using PaintindGallery.Dtos;
using PaintingGallery.UI.Controls.Information;
using PaintingGallery.UI.Helper;
using PaintingGallery.UI.Services;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PaintingGallery.UI.Screens
{
    /// <summary>
    /// Interaction logic for RegisterPatron.xaml
    /// </summary>
    public partial class RegisterPatron : UserControl
    {
        OpenFileDialog OpenFileDialog = new OpenFileDialog();
        UIElementCollection UIElements;
        private int IdPatron { get; set; }

        public RegisterPatron()
        {
            InitializeComponent();
        }

        public RegisterPatron(int idPatron)
        {
            InitializeComponent();

            lblPatron.Content = "Patron Information";

            lblEditPatron.Visibility = System.Windows.Visibility.Visible;
            lblDeletePatron.Visibility = System.Windows.Visibility.Visible;

            BtnActionPatron.Visibility = System.Windows.Visibility.Hidden;
            BtnActionPatron.Content = "Save";

            btnCancel.Content = "Go back";

            IdPatron = idPatron;

            GetPatronInfo(idPatron);

            EnableInputs(false);
        }

        private void FillInputs(PatronDto patron)
        {
            txtIdentification.Text = patron.Identification;
            txtName.Text = patron.Name;
            txtCountry.Text = patron.Country;
            txtCity.Text = patron.City;
            txtBirthdate.Text = patron.Birthdate.ToString();
            txtDeath.Text = patron.Death.ToString();
            imageContainer.Source = new BitmapImage(new Uri(patron.Picture));
        }

        private void EnableInputs(bool enable)
        {
            txtIdentification.IsEnabled = enable;
            txtName.IsEnabled = enable;
            txtCountry.IsEnabled = enable;
            txtCity.IsEnabled = enable;
            txtBirthdate.IsEnabled = enable;
            txtDeath.IsEnabled = enable;
            btnChooseFile.IsEnabled = enable;
        }

        private async void GetPatronInfo(int id)
        {
            var patron = await (new PatronServices()).GetPatron(id);

            if (patron != null)
            {
                FillInputs(patron);
            }
            else
            {
                MessageControl.ErrorRetrievingInfo().ShowMessage();
                MainWindow.DisplayScreen(new ShowPatrons());
            }
        }

        private void btnChooseFile_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog.DefaultExt = ".png";
            OpenFileDialog.Filter = "Image files|*.jpeg; *.png; *.jpg; *.gif";

            if (OpenFileDialog.ShowDialog() == true)
            {
                imageContainer.Source = new BitmapImage(new Uri(OpenFileDialog.FileName));
                imageContainer.Tag = "hasChanged";
            }
        }

        private void BtnRegisterPatron_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (UIElements == null)
                UIElements = ScreenRegisterPatron.Children;

            if (!EmptyInputValidator.Validate(UIElements))
            {
                if (!DateTimeValidator.InvalidDates(new List<DatePicker> { txtBirthdate, txtDeath }))
                {
                    if (!DateTimeValidator.CompareDates(txtBirthdate, txtDeath))
                    {
                        ExecuteAction(BtnActionPatron.Content.ToString());
                    }
                    else
                    {
                        MessageControl.InvalidDateRange().ShowMessage();
                    }
                }
                else
                {
                    MessageControl.InvalidDates().ShowMessage();
                }
            }
            else
            {
                MessageControl.EmptyInput().ShowMessage();
            }
        }

        private PatronDto GetPatronInfo()
        {
            return new PatronDto
            {
                Id = IdPatron,
                Identification = txtIdentification.Text,
                Name = txtName.Text,
                Country = txtCountry.Text,
                City = txtCity.Text,
                Birthdate = DateTime.Parse(txtBirthdate.Text),
                Death = DateTime.Parse(txtDeath.Text)
            };
        }

        private async void ExecuteAction(string action)
        {
            try
            {
                PatronDto patron = null;
                MessageControl message = null;

                if (!action.Equals("Save"))
                {
                    patron = await new Services.PatronServices().CreatePatron(GetPatronInfo());
                    message = MessageControl.SuccessfulRegistration();
                }
                else
                {
                    patron = await new Services.PatronServices().EditPatron(GetPatronInfo());
                    message = MessageControl.SuccessfulChanges();
                }

                if (patron != null)
                {
                    if (imageContainer.Tag != null && imageContainer.Tag.ToString().Equals("hasChanged"))
                    {
                        var result = await Services.FileServices.GetInstance().UploadFile(OpenFileDialog.FileName, patron);

                        if (result == false)
                        {
                            new MessageControl("Error", "We had an error while trying to upload the image, please try again in a few minutes", MessageType.Error)
                                .ShowMessage();
                        }
                        else
                        {
                            message.ShowMessage();
                            MainWindow.DisplayScreen(new ShowPatrons());
                        }
                    }
                    else
                    {
                        message.ShowMessage();
                        MainWindow.DisplayScreen(new ShowPatrons());
                    }
                }
                else
                {
                    MessageControl.RegistrationFailed().ShowMessage();
                }
            }
            catch (Exception)
            {
                MessageControl.Error().ShowMessage();
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.DisplayScreen(new ShowPatrons());
        }

        private async void DeletePatron(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                var result = await new PatronServices().DeletePatron(IdPatron);

                if (result == true)
                {
                    MessageControl.SuccessfulElimination().ShowMessage();
                    MainWindow.DisplayScreen(new ShowPatrons());
                }
                else
                {
                    MessageControl.FailedOperation().ShowMessage();
                }
            }
            catch (Exception)
            {
                MessageControl.Error().ShowMessage();
            }
        }

        private void EditPatron(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            EnableInputs(true);

            BtnActionPatron.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
