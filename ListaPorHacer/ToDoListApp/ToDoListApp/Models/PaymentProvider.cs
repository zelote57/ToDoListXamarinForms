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

    public class PaymentData
    {
        public string OrderId { get; set; }
        public string Amount { get; set; }
        public string Email { get; set; }
        public string Currency { get; set; }
        public string Mode { get; set; }
        public string CardType { get; set; }
    }
}

//const val UNKNOWN_ERROR = 1

//// Timeout error
//const val TIMEOUT_ERROR = 2

//// No connection error
//const val NO_CONNECTION_ERROR = 3

//// Server error
//const val SERVER_ERROR = 4

//// Payment cancelled error
//const val PAYMENT_CANCELLED_ERROR = 5

//// Payment refused error
//const val PAYMENT_REFUSED_ERROR = 6