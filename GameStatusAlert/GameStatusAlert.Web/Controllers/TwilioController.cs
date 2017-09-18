using GameStatusAlert.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStatusAlert.Web.Controllers
{
    public class TwilioController : Controller
    {
        [HttpPost]
        public void SendSms(string phoneNumber, string body)
        {
            TwilioBll.SendSms(phoneNumber, body);
        }
    }
}