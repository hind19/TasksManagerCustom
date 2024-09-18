using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TasksManager.LeftPanelModule.Converters
{
    public class BoolToFontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var booleanValue = (bool)value;

            return booleanValue
                ? FontWeights.Black
                : Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
