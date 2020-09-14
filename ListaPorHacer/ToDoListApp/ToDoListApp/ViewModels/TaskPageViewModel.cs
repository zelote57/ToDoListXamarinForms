using MvvmHelpers;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.Core;
using ToDoListApp.Core.Domain;
using ToDoListApp.Core.Model;

namespace ToDoListApp.ViewModels
{
    public class TaskPageViewModel : Base.BaseViewModel
    {
        public TaskPageViewModel(INavigationService navigationService,
            IPageDialogService dialogService,
            IUnitOfWork uow) 
            : base(navigationService, dialogService)
        {
            _uow = uow;
        }

        private IUnitOfWork _uow;

        #region Props

        public Tasks MyTask { get; set; }
        public bool MustDelete { get; set; }
        public StatusType SelectedStatusType { get; set; }
        public ObservableRangeCollection<StatusType> StatusTypesList { get; set; }

        #endregion

        public override Task InitializeAsync(INavigationParameters parameters)
        {
            ClearProp();
            FillStatusTypes();

            MyTask = (Tasks)parameters[nameof(Tasks)];

            SelectedStatusType = StatusTypesList.FirstOrDefault(i => i.Code == MyTask.Status);

            return base.InitializeAsync(parameters);
        }

        #region Logic

        private async Task UpdateTaskAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            MyTask.Status = SelectedStatusType.Code;

            var dbTask = await _uow.Tasks.GetById(MyTask.Id);

            await _uow.Tasks.Remove(dbTask);

            if (!MustDelete)
                await _uow.Tasks.Add(MyTask);

            IsBusy = false;
            LoadMainView();
        }

        private void ClearProp()
        {
            MyTask = null;
            MustDelete = false;
            SelectedStatusType = new StatusType();
        }

        private void FillStatusTypes()
        {
            StatusTypesList = new ObservableRangeCollection<StatusType>();
            StatusTypesList.Add(new StatusType
            {
                Code = "C",
                Name = "Completado"
            });
            StatusTypesList.Add(new StatusType
            {
                Code = "P",
                Name = "Pendiente"
            });
        }

        private void LoadMainView()
        {
            _navigationService.GoBackAsync();
        }

        #endregion

        #region Commands
        public DelegateCommand LoadMainViewCommand => new DelegateCommand(LoadMainView).ObservesCanExecute(() => IsNotBusy);

        public DelegateCommand UpdateTaskCommand => new DelegateCommand(async () => await UpdateTaskAsync()).ObservesCanExecute(() => IsNotBusy);

        #endregion
    }
}
