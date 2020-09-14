using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;

namespace ToDoListApp.ViewModels.Base
{
    public abstract class BaseViewModel : MvvmHelpers.BaseViewModel, IInitializeAsync, IDestructible, INavigationAware
    {
        protected readonly INavigationService _navigationService;
        protected readonly IPageDialogService _dialogService;

        protected BaseViewModel(INavigationService navigationService,
            IPageDialogService dialogService
        )
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        public virtual Task InitializeAsync(INavigationParameters parameters)
        {
            return Task.FromResult(false);
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            // INavigatedAware
            // Called when the implementer has been navigated away from.
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            // INavigatedAware
            // After the page has been pushed onto the stack, and it is now visible
        }

        public void Destroy()
        {
        }
    }
}
