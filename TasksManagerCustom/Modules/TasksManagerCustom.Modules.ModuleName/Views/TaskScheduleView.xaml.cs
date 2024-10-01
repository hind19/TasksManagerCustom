using System;
using System.Windows.Controls;
using TasksManager.Modules.TaskScheduleModule.ViewModels;
using TasksManager.TasksScheduleModule.Models;

namespace TasksManager.Modules.TaskScheduleModule.Views
{
    /// <summary>
    /// Interaction logic for TaskScheduleView.xaml
    /// </summary>
    public partial class TaskScheduleView : UserControl
    {
        public TaskScheduleView()
        {
            InitializeComponent();
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var context = basicFrame.DataContext as TaskScheduleViewModel;
            if (context is not null) 
            {
                var image = sender as Image;
                if (image is not null) 
                {
                    var model = image.DataContext as DataGridTaskModel;
                    if(model is not null)
                    {
                        context.CompleteOrResetTask(model);
                        return;
                    }
                }
            }
            
            throw new InvalidCastException();
        }
    }
}
