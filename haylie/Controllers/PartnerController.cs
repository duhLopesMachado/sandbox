using haylie.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace haylie.Controllers
{
    public class PartnerController : ApiController
    {
        BasicServices _basicServices = new BasicServices();
        UserServices _userServices = new UserServices();

        [HttpGet]
        public BaseResponse GetAll(string p_bot, string p_param = "")
        {
            BaseResponse result = new BaseResponse();
            try
            {
                int l_loguser = _userServices.loadByToken(p_bot, HttpUtility.HtmlDecode(HttpUtility.UrlDecode(p_param))).id;

                //result.Value = JsonConvert.SerializeObject(_basicServices.getPartners(p_bot, l_loguser));

                result.Value = JsonConvert.SerializeObject(new { partners = _basicServices.getPartners(p_bot, l_loguser) });
                result.Message = "Listagem de fornecedores cadastrados";
            }
            catch (Exception e)
            {
                result.Sucess = false;
                result.Message = e.Message;
            }
            return result;
        }
        [HttpGet]
        [HttpPost]
        public BaseResponse Save(string p_bot, int p_id, string p_company, string p_cnpj, string p_name, string p_mail, string p_phone, string p_param = "")
        {
            //http://localhost:33910/api/partner/save?p_id=ajkhsdjaksd

            BaseResponse result = new BaseResponse();
            try
            {
                int l_loguser = _userServices.loadByToken(p_bot, HttpUtility.HtmlDecode(HttpUtility.UrlDecode(p_param))).id;

                string l_name = HttpUtility.HtmlDecode(HttpUtility.UrlDecode(p_name));
                string l_cnpj = HttpUtility.HtmlDecode(HttpUtility.UrlDecode(p_cnpj));
                string l_mail = HttpUtility.HtmlDecode(HttpUtility.UrlDecode(p_mail));
                string l_phone = HttpUtility.HtmlDecode(HttpUtility.UrlDecode(p_phone));

                string l_token = HttpUtility.HtmlDecode(HttpUtility.UrlDecode(p_param));

                result.Value = _basicServices.save_partner(p_bot, p_id, p_company, l_cnpj, l_name, l_mail, l_phone, l_loguser);
                result.Message = "Fornecedor registrado com sucesso";
            }
            catch (Exception e)
            {
                result.Sucess = false;
                result.Message = e.Message;
            }

            return result;
        }
        

    }
}
