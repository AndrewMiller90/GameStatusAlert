using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace GameStatusAlert.Web.Controllers
{
    public class TwilioController : Controller
    {
        private static string accountSid = "ACed800b2e4f67503025c29a37f3034a27";
        private static string authToken = "28dab42a1f95e2e7b6392803296f3a73";
        private static string twilioPhoneNumber = "+18127273802";
        static TwilioController() {
            TwilioClient.Init(accountSid, authToken);
        }
        // GET: Twilio
        public void SendSms(string phoneNumber, string body)
        {
            var message = MessageResource.Create(
                to: new PhoneNumber(phoneNumber),
                from: new PhoneNumber(twilioPhoneNumber),
                body: body
            );
        }
    }
}