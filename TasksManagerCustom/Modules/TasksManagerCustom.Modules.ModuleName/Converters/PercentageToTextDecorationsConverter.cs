using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TasksManager.TasksScheduleModule.Converters
{
    public class PercentageToTextDecorationsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is null)
                return Binding.DoNothing;
            
            var percentage = (int)value;

            return percentage == 100 
                ? TextDecorations.Strikethrough
                : Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
