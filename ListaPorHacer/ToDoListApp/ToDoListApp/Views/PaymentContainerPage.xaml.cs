using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoListApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentContainerPage : ContentPage
    {
        public PaymentContainerPage()
        {
            var urlSite = "";
            
            InitializeComponent();
            
            InitWebView(urlSite);

        }

        public void InitWebView(string urlSite)
        {
            WvPayment.Source = urlSite;
            //WvPayment.CanGoForward();
        }
    }
}