using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    public class _SearchBox
    {
        public string _msg { get; set; }

        public List<seUser> listUsers = new List<seUser>();

        public List<seMedia> listMedia = new List<seMedia>();
        public List<seAlert> listAlerts = new List<seAlert>();
        public List<seLead> listLeads = new List<seLead>();
        public List<seQuote> listQuotes = new List<seQuote>();
        public List<seMsg> listMsgs = new List<seMsg>();

        public List<seReport> listDash = new List<seReport>();
        public List<seCorona> listCorona = new List<seCorona>();
        public List<seAgenda> listAgenda = new List<seAgenda>();
        public List<seReport> listWhats = new List<seReport>();

        public List<seCompany> listCompanys = new List<seCompany>();
        public List<seClass> listClasses = new List<seClass>();
        public List<seCateg> listCategs = new List<seCateg>();
        public List<sePartner> listPartners = new List<sePartner>();
        public List<seProduct> listProducts = new List<seProduct>();
        public List<seOrder> listOrders = new List<seOrder>();
        public List<seProfile> listProfile = new List<seProfile>();

        public List<sePlan> listPlans = new List<sePlan>();
        public List<seFAQ> listFAQ = new List<seFAQ>();

        public List<seBible> listBible = new List<seBible>();
        public List<seClick> listClick = new List<seClick>();
        public List<seGameLog> listGameLog = new List<seGameLog>();

        
    }
    public class seReport {
        public string label; 
        public int total;
        public int distinct;

    }
}