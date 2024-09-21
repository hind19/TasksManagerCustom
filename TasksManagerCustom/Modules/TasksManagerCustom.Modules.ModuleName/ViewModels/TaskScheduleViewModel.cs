using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using TasksManager.Core.Enums;
using TasksManager.Core.EventModels;
using TasksManager.Core.Events;
using TasksManager.TasksScheduleModule.Models;

namespace TasksManager.Modules.TaskScheduleModule.ViewModels
{
    internal class TaskScheduleViewModel :BindableBase
    {
        #region Fields
        private ObservableCollection<DataGridTaskModel> _curentTasksList;
        #endregion

        #region Constructors
        public TaskScheduleViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<CategoryOrProjectChangedEvent>().Subscribe(OnCategotyProjectChanged);
        }

        #endregion

        #region Properties
        public ObservableCollection<DataGridTaskModel> CurrentTasksList
        {
            get
            {
                return _curentTasksList;
            }
            set
            {
                SetProperty(ref _curentTasksList, value);
            }
        }

        #endregion

        #region Methods
        private void OnCategotyProjectChanged(Tuple<HierarchicalCollectionModel, CategoryProjectEnum> tuple)
        {
            if(tuple.Item2 == CategoryProjectEnum.Category)
            {
                //TODO: TasksQueryService

            }
            else if (tuple.Item2 == CategoryProjectEnum.Project)
            {

            }
            else
            {
                return;
            }

        }
        #endregion

       
    }
}
