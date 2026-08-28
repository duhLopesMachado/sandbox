using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using haylie.git;


namespace haylie.Controllers
{
    [RoutePrefix("api/git")]
    public class GitController : ApiController
    {
        private GitHubOptions _opts = new GitHubOptions();

        private GitHubClient _service = new GitHubClient(_opts);

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


/*
ConfigurationSettings.AppSettings["super_nf"]

*/