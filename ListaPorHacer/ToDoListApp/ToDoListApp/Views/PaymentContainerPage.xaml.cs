using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoListApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentContainerPage : ContentPage
    {
        public PaymentContainerPage()
        {
            InitializeComponent();
            this.WvPayment.Source = "https://www.google.com/";
        }
    }
}