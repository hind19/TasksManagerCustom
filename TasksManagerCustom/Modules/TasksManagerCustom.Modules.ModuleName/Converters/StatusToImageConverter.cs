using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TasksManager.Shared.Enums;
using TasksManager.Shared.GlobalConstants;

namespace TasksManager.TasksScheduleModule.Converters
{
    public class StatusToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return null;

            var status = (TaskStatusEnum)value;
            string imagePath;

            switch (status)
            {
                case TaskStatusEnum.NotStarted:
                    imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GlobalConstants.ImagesFolderName,  GlobalConstants.NotStartedFileName);
                    break;
                case TaskStatusEnum.InProgress:
                    imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GlobalConstants.ImagesFolderName,  GlobalConstants.InProgressFileName);
                    break;
                case TaskStatusEnum.ReadyForReview:
                    imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GlobalConstants.ImagesFolderName, GlobalConstants.ReadyForReviewFileName);
                    break;
                case TaskStatusEnum.Completed:
                    imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GlobalConstants.ImagesFolderName, GlobalConstants.CompletedFileName);
                    break;
                case TaskStatusEnum.Paused:
                    imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GlobalConstants.ImagesFolderName, GlobalConstants.PausedFileName);
                    break;
                case TaskStatusEnum.Cancelled:
                    imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GlobalConstants.ImagesFolderName, GlobalConstants.CancelledFileName);
                    break;
                default:
                    return null;
            }

            if(!File.Exists(imagePath))
                return null;

            return new BitmapImage(new Uri(imagePath));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
