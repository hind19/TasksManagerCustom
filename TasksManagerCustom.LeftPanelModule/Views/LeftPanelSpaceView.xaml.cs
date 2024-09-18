using System.Windows;
using System.Windows.Controls;
using TasksManager.LeftPanelModule.Models;
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

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var vm = sender as FrameworkElement;
            if (vm is not null)
            {
                ((LeftPanelSpaceViewModel)vm.DataContext).SelectedCategory = e.NewValue as HierarchicalCollectionModel;
    }
        }
    }
}
