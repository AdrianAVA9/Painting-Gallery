using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PaintingGallery.UI.Helper
{
    public static class EmptyInputValidator
    {
        private static List<string> Types = new List<string> { "TextBox", "DatePicker" };

        public static bool Validate(UIElementCollection elementCollection)
        {
            return AreEmpty(SelectRequiredType(elementCollection));
        }

        private static bool AreEmpty(List<UIElement> uIElementCollection)
        {
            bool areEmpty = false;
            string text = "";

            foreach (var uIElement in uIElementCollection)
            {

                switch (uIElement.GetType().Name)
                {
                    case "TextBox":
                        text = ((TextBox)uIElement).Text;
                        break;

                    case "DatePicker":
                        text = ((DatePicker)uIElement).Text;
                        break;
                }

                if (string.IsNullOrEmpty(text))
                    areEmpty = true;
            }

            return areEmpty;
        }

        private static List<UIElement> SelectRequiredType(UIElementCollection elementCollection)
        {
            List<UIElement> textBoxes = new List<UIElement>();

            for (int i = 0; i < elementCollection.Count; i++)
            {
                if (Types.Contains(elementCollection[i].GetType().Name))
                {
                    textBoxes.Add(elementCollection[i]);
                }
            }

            return textBoxes;
        }
    }


    public static class DateTimeValidator
    {
        public static bool InvalidDates(List<DatePicker> datePickers)
        {
            bool areErrors = false;

            foreach (var datePicker in datePickers)
            {
                if (DateTime.Parse(datePicker.Text) > DateTime.Now)
                {
                    areErrors = true;
                }
            }

            return areErrors;
        }

        public static bool CompareDates(DatePicker olderDate, DatePicker lessAncientDate)
        {
            return (DateTime.Parse(olderDate.Text) >= DateTime.Parse(lessAncientDate.Text));
        }
    }
}
