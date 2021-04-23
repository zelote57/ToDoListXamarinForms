using MvvmHelpers;
using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;
using ToDoListApp.Core;
using ToDoListApp.Core.Domain;
using System.Linq;
using Prism.Commands;

namespace ToDoListApp.ViewModels
{
    public class MainPageViewModel : Base.BaseViewModel
    {
        public MainPageViewModel(INavigationService navigationService,
            IPageDialogService dialogService,
            IUnitOfWork uow)
            : base(navigationService, dialogService)
        {
            _uow = uow;
            
        }

        private IUnitOfWork _uow;

        #region Props

        public ObservableRangeCollection<Tasks> Tasks { get; set; }

        #endregion

        public override Task InitializeAsync(INavigationParameters parameters)
        {
            LoadToDoListCommand.Execute();
            return base.InitializeAsync(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadToDoListCommand.Execute();
            base.OnNavigatedTo(parameters);
        }

        #region Logic

        private async Task LoadToDoList()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            
            Tasks = new ObservableRangeCollection<Tasks>();
            var TasList = await _uow.Tasks.GetAll();

            IsBusy = false;

            if (TasList.Count > 0)
            {
                Tasks.ReplaceRange(TasList);
            }

        }

        private void OnTaskTapped(Tasks selectedTask)
        {
            var parameter = new NavigationParameters { { nameof(Tasks), selectedTask } };
            _navigationService.NavigateAsync(nameof(Views.TaskPage), parameter);
        }

        private async Task CreateTask()
        {
            await _navigationService.NavigateAsync(nameof(Views.CreateTaskPage));
        }

        private async Task Pay()
        {
            await _navigationService.NavigateAsync(nameof(Views.PaymentContainerPage));
        }

        private void ClearProp()
        {
            Tasks = new ObservableRangeCollection<Tasks>();
        }

        #endregion

        #region Commands

        public DelegateCommand<Tasks> OnTaskTappedCommand => new DelegateCommand<Tasks>(OnTaskTapped).ObservesCanExecute(() => IsNotBusy);

        public DelegateCommand LoadToDoListCommand => new DelegateCommand(async () => await LoadToDoList()).ObservesCanExecute(() => IsNotBusy);
        public DelegateCommand CreateTaskCommand => new DelegateCommand(async () => await CreateTask()).ObservesCanExecute(() => IsNotBusy);
        public DelegateCommand PayCommand => new DelegateCommand(async () => await Pay()).ObservesCanExecute(() => IsNotBusy);

        #endregion

    }
}
