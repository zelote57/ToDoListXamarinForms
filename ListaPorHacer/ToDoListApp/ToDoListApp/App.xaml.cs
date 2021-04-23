using Prism;
using Prism.Ioc;
using Prism.Unity;
using ToDoListApp.Core;
using ToDoListApp.Persistence;
using Xamarin.Forms;

namespace ToDoListApp
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) {}

        protected async override void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync(nameof(Views.MainPage));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterScreens(containerRegistry);
            RegisterServices(containerRegistry);
        }

        private void RegisterScreens(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.MainPage>();
            containerRegistry.RegisterForNavigation<Views.TaskPage>();
            containerRegistry.RegisterForNavigation<Views.CreateTaskPage>();
            containerRegistry.RegisterForNavigation<Views.PaymentContainerPage>();

            containerRegistry.RegisterForNavigation<Views.MainPage, ViewModels.MainPageViewModel>();
            containerRegistry.RegisterForNavigation<Views.TaskPage, ViewModels.TaskPageViewModel>();
            containerRegistry.RegisterForNavigation<Views.CreateTaskPage, ViewModels.CreateTaskPageViewModel>();
            containerRegistry.RegisterForNavigation<Views.PaymentContainerPage, ViewModels.PaymentContainerPageViewModel>();
        }
        
        private void RegisterServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<TareasDatabase>();
            containerRegistry.RegisterSingleton<IUnitOfWork, UnitOfWork>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
