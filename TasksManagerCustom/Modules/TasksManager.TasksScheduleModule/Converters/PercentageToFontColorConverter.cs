using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TasksManager.TasksScheduleModule.Converters
{
    public class PercentageToFontColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Brushes.Black;

            var percentage = (int)value;

            return percentage == 100
                ? Brushes.LightGray
                : Brushes.Black;


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
