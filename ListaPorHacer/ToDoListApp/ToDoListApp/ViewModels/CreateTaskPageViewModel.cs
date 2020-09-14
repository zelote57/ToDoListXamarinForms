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
    class CreateTaskPageViewModel : Base.BaseViewModel
    {
        public CreateTaskPageViewModel(INavigationService navigationService,
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

            MyTask = new Tasks();
            return base.InitializeAsync(parameters);
        }

        #region Logic

        private async Task CreateTaskAsync()
        {
            if (IsBusy)
                return;

            if (string.IsNullOrEmpty(MyTask.TaskName))
            {
                await _dialogService.DisplayAlertAsync("Error", "Debe ingresar nombre de la Tarea", "OK");
                return;
            }

            if (string.IsNullOrEmpty(SelectedStatusType?.Code))
            {
                await _dialogService.DisplayAlertAsync("Error", "Debe seleccionar estador de la Tarea", "OK");
                return;
            }

            IsBusy = true;

            var dbTask = await _uow.Tasks.GetLast();

            MyTask.Id = dbTask.Id + 1;
            MyTask.Status = SelectedStatusType.Code;
            
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

        public DelegateCommand CreateTaskCommand => new DelegateCommand(async () => await CreateTaskAsync()).ObservesCanExecute(() => IsNotBusy);

        #endregion
    }
}
