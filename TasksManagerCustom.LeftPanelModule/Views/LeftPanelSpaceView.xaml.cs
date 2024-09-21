using System.Windows;
using System.Windows.Controls;
using TasksManager.Core.EventModels;
using TasksManager.LeftPanelModule.ViewModels;

namespace TasksManager.LeftPanelModule.Views
{
    /// <summary>
    /// Interaction logic for LeftPanelSpaceView.xaml
    /// </summary>
    public partial class LeftPanelSpaceView : UserControl
    {
        public LeftPanelSpaceView()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var control = sender as FrameworkElement;
            if (control is not null)
            {
                 var category = control.DataContext as HierarchicalCollectionModel;
                 var viewModel = this.DataContext as LeftPanelSpaceViewModel;
                if (viewModel is not null)
                {
                    viewModel.SelectedCategory = category!;
                }
            }
        }
    }
}
