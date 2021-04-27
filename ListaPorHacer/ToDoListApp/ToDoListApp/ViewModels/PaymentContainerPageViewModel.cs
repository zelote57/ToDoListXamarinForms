using System;
using System.Net;
using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;
using Prism.Commands;
using Refit;
using ToDoListApp.Core.Services.Data;
using ToDoListApp.Models;

namespace ToDoListApp.ViewModels
{
    class PaymentContainerPageViewModel : Base.BaseViewModel
    {
        // Merchant server url
        // FIXME: change by the right payment server
        private const string SERVER_URL = "http://192.168.1.142:9095/";

        // Environment TEST or PRODUCTION, refer to documentation for more information
        // FIXME: change by your targeted environment
        private const string PAYMENT_MODE = "TEST";

        private const string CALLBACK_URL_PREFIX = "http://webview";

        public PaymentContainerPageViewModel(INavigationService navigationService,
            IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
        }

        public override Task InitializeAsync(INavigationParameters parameters)
        {
            GetUrlForPayment();
            return base.InitializeAsync(parameters);
        }

        public string GetUrlForPayment()
        {
            var result = string.Empty;
            try
            {
                var payload = new PaymentDataRequest
                {
                    OrderId = "2",
                    Amount = "170",
                    Email = "gustavo@bertonisolutions.com",
                    Currency = "604", // PEN
                    Mode = PAYMENT_MODE,
                    Language = "EN",
                    CardType = "VISA"
                };

                var apiService = RestService.For<IIzipayApi>(SERVER_URL);
                var response = apiService.GetRedirectionUrl(payload).Result;
                var httpStatus = response.StatusCode;

                if (!response.IsSuccessStatusCode)
                {
                    _dialogService.DisplayAlertAsync("Info", response.Error.Message, "OK");
                }
                if (httpStatus == HttpStatusCode.NoContent)
                {
                    _dialogService.DisplayAlertAsync("Info", "No se encontraron resultados", "OK");
                }
                PaymentResponse res = response.Content;

                if (res != null)
                    result = res.RedirectionUrl;
            }
            catch (Exception e)
            {
                _dialogService.DisplayAlertAsync("Error", e.Message, "Ok");
            }

            return result;
        }

        public bool IsCallbackUrl(string url)
        {
            return url.Contains(CALLBACK_URL_PREFIX);
        }

        private void LoadMainView()
        {
            _navigationService.GoBackAsync();
        }

        public DelegateCommand LoadMainViewCommand => new DelegateCommand(LoadMainView).ObservesCanExecute(() => IsNotBusy);
    }
}
