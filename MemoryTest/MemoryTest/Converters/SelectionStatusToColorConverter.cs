using System;
using System.Globalization;
using Xamarin.Forms;

namespace MemoryTest.Converters
{
    public class SelectionStatusToColorConverter : IValueConverter
    {
        // bool --> Color
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (value is bool) ? (bool)value : false;
           // string resourceName = boolValue ? UiConstants.OnlineColorResourceName : UiConstants.OfflineColorResourceName;
           // var result = (Color)Application.Current.Resources[resourceName];
            return boolValue?Color.Silver:Color.White;
        }

        // string --> bool
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    
    }
}
