using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using ToDoListApp.Models;

namespace ToDoListApp.Core.Services.Data
{
    public interface IIzipayApi
    {
        [Post("")]
        Task<ApiResponse<PaymentResponse>> GetRedirectionUrl([Body] PaymentDataRequest paymentData);
    }
}
