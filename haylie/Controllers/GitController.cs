using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using haylie.git;
using haylie.git.services;
using haylie.DTOs;


namespace haylie.Controllers
{
    public class GitController : ApiController
    {
        private readonly GitHubOptions _opts = new GitHubOptions();
        private readonly GitHubClient _service;

        public GitController()
        {
            _service = new GitHubClient(_opts);
        }

        // [HttpPost]
        // public async Task<HttpResponseMessage> Create(CheckoutRequest request)
        // {
        //     var result = await _service.CreateCheckout(request);

        //     return Request.CreateResponse(HttpStatusCode.OK, result);
        // }
        // [HttpPost]
        // public async Task<HttpResponseMessage> Receive()
        // {
        //     var body = await Request.Content.ReadAsStringAsync();

        //     await _service.ProcessWebhook(body);

        //     return Request.CreateResponse(HttpStatusCode.OK);
        // }

    }
}


/*
ConfigurationSettings.AppSettings["super_nf"]

*/