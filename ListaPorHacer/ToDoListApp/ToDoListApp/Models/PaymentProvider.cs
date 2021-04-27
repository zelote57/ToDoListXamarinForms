using System.Runtime.InteropServices.ComTypes;

namespace ToDoListApp.Models
{
    public enum PaymentErrorCode
    {
        UNKNOWN_ERROR = 1,
        TIMEOUT_ERROR = 2,
        NO_CONNECTION_ERROR = 3,
        SERVER_ERROR = 4,
        PAYMENT_CANCELLED_ERROR = 5,
        PAYMENT_REFUSED_ERROR = 6
    }

    public class PaymentProvider
    {
        private const int WEBVIEW_ACTIVITY_CODE_RESULT = 981;

    }

    public class PaymentResult
    {
        public int? ErrorCode { get; set; }
        public string Cause { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class PaymentDataRequest
    {
        public string OrderId { get; set; }
        public string Amount { get; set; }
        public string Email { get; set; }
        public string Currency { get; set; }
        public string Mode { get; set; }
        public string Language { get; set; }
        public string CardType { get; set; }
    }
    public class PaymentResponse
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string RedirectionUrl { get; set; }
    }
}
