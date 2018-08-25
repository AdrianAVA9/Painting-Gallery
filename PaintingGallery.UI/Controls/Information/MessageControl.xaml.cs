using System;
using System.Configuration;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PaintingGallery.UI.Controls.Information
{
    /// <summary>
    /// Interaction logic for MessageControl.xaml
    /// </summary>
    public partial class MessageControl : Window
    {
        private string MessageTitle { get; set; }
        private string MessageBody { get; set; }
        private MessageType MessageType { get; set; }
        private readonly string path = ConfigurationManager.AppSettings["Path"];

        public MessageControl(string messageTitle, string messageBody, MessageType messageType)
        {
            InitializeComponent();

            MessageTitle = messageTitle;
            MessageBody = messageBody;
            MessageType = messageType;

            SetMessageControl();
        }

        private void SetMessageControl()
        {
            MessageTitleContainer.Content = MessageTitle;
            MessageBodyContainer.Content = MessageBody;

            switch (MessageType)
            {
                case MessageType.Confirmation:
                    this.Background = new SolidColorBrush(Color.FromRgb(255, 128, 0));
                    MessageIcon.Source = new BitmapImage(new Uri(path + "Attention.png", UriKind.Relative));
                    break;

                case MessageType.Error:
                    MessageContainer.Background = new SolidColorBrush(Color.FromRgb(220, 20, 60));
                    MessageIcon.Source = new BitmapImage(new Uri(path + "Cancel.png", UriKind.Relative));
                    break;

                case MessageType.Warning:
                    MessageContainer.Background = new SolidColorBrush(Color.FromRgb(255, 128, 0));
                    MessageIcon.Source = new BitmapImage(new Uri(path + "Attention.png", UriKind.Relative));
                    break;

                case MessageType.Success:
                    MessageContainer.Background = new SolidColorBrush(Color.FromRgb(46, 139, 37));
                    MessageIcon.Source = new BitmapImage(new Uri(path + "Checked.png", UriKind.Relative));
                    break;

                case MessageType.Information:
                    MessageContainer.Background = new SolidColorBrush(Color.FromRgb(64, 64, 64));
                    MessageIcon.Source = new BitmapImage(new Uri(path + "Information.png", UriKind.Relative));
                    break;
            }

        }

        public void ShowMessage()
        {
            this.ShowDialog();
        }

        private void btnCloseMessage_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public static MessageControl SuccessfulRegistration()
        {
            return new MessageControl("Successful registration", "The information was registered successfully", MessageType.Success);
        }

        public static MessageControl SuccessfulChanges()
        {
            return new MessageControl("Successful Changes", "The information was edited successfully", MessageType.Success);
        }

        public static MessageControl Error()
        {
            return new MessageControl("Error", "An error ocurred, please try againg later", MessageType.Error);
        }

        public static MessageControl ErrorRetrievingInfo()
        {
            return new MessageControl("Error", "we had an error retrieving information, please try againg later", MessageType.Error);
        }

        public static MessageControl RegistrationFailed()
        {
            return new MessageControl("Registration failed", "Please make sure the information is correct", MessageType.Information);
        }

        public static MessageControl EmptyInput()
        {
            return new MessageControl("Empty inputs", "Please complete the all information", MessageType.Information);
        }

        public static MessageControl InvalidDates()
        {
            return new MessageControl("Ivalid dates", "Please check that dates are older rather than today", MessageType.Information);
        }

        public static MessageControl SuccessfulElimination()
        {
            return new MessageControl("Successful elimination", "The information was successfuly deleted", MessageType.Success);
        }
        public static MessageControl FailedOperation()
        {
            return new MessageControl("Failed Operation", "We had an error when trying to execute the operation", MessageType.Success);
        }
        public static MessageControl InvalidDateRange()
        {
            return new MessageControl("Invalid dates", "The dates do not have coherence", MessageType.Information);
        }
    }

    public enum MessageType
    {
        Error = 1,
        Information = 2,
        Confirmation = 3,
        Warning = 4,
        Success = 5
    }
}
