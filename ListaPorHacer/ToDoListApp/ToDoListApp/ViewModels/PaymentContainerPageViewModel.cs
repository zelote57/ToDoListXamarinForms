using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;
using Prism.Commands;

namespace ToDoListApp.ViewModels
{
    class PaymentContainerPageViewModel : Base.BaseViewModel
    {
        public PaymentContainerPageViewModel(INavigationService navigationService, 
            IPageDialogService dialogService) 
            : base(navigationService, dialogService)
        {
        }

        public override Task InitializeAsync(INavigationParameters parameters)
        {
            return base.InitializeAsync(parameters);
        }

        private void LoadMainView()
        {
            _navigationService.GoBackAsync();
        }

        public DelegateCommand LoadMainViewCommand => new DelegateCommand(LoadMainView).ObservesCanExecute(() => IsNotBusy);
    }
}
