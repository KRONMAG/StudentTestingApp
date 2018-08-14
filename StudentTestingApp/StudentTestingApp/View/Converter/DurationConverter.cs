using System;
using System.Globalization;
using Xamarin.Forms;

namespace StudentTestingApp.View.Converter
{
    class DurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? "неограниченна" : $"{(int)value} сек.";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}