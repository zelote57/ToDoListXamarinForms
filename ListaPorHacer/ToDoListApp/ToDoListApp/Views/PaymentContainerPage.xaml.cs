using System;
using ToDoListApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoListApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentContainerPage : ContentPage
    {
        private PaymentContainerPageViewModel _viewModel;
        public PaymentContainerPage()
        {   
            InitializeComponent();
            
            _viewModel = this.BindingContext as PaymentContainerPageViewModel;
            
            var urlSite = _viewModel.GetUrlForPayment();
            
            InitWebView(urlSite);
        }

        public void InitWebView(string urlSite)
        {
            WvPayment.Source = urlSite;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                WvPayment.Navigated += async (s, e) =>
                {
                    try
                    {
                        if (_viewModel.IsCallbackUrl(e.Url))
                        {
                            if (e.Url.Contains(".success/"))
                            {
                                WvPayment.IsVisible = false;
                                await Application.Current.MainPage.DisplayAlert("Info", "Se realizó el Pago", "Ok");
                                _viewModel.LoadMainViewCommand.Execute();
                            }
                            else
                            {
                                WvPayment.IsVisible = false;
                                await Application.Current.MainPage.DisplayAlert("Info", "Sucedió un Error", "Ok");
                                _viewModel.LoadMainViewCommand.Execute();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
                    }
                };
                
            });
        }
    }
}