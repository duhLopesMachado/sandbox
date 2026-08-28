
namespace haylie.DTOs
{
    public class CheckoutRequest
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public string PlanName { get; set; }

        public int PlanId { get; set; }
    }
    public class CheckoutResponse
    {
        public bool Success { get; set; }

        public string CheckoutUrl { get; set; }

        public string PreferenceId { get; set; }

        public string Message { get; set; }
    }
}