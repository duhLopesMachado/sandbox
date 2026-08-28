namespace haylie.Controllers
{
    [RoutePrefix("api/payment")]
    public class PaymentController : ApiController
    {
        private readonly PaymentService _service = new PaymentService();

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> Create(CheckoutRequest request)
        {
            var result = await _service.CreateCheckout(request);

            return Ok(result);
        }
        [HttpPost]
        
        [Route("webhook")]
        public async Task<IHttpActionResult> Receive()
        {
            var body = await Request.Content.ReadAsStringAsync();

            await _service.ProcessWebhook(body);

            return Ok();
        }

    }
}