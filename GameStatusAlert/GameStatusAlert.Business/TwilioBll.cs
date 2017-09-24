using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace GameStatusAlert.Business {
    public sealed class TwilioBll {
        private static string accountSid = ConfigurationManager.AppSettings["AccountSid"].ToString().Trim();
        private static string authToken = ConfigurationManager.AppSettings["AuthToken"].ToString().Trim();
        private static string twilioPhoneNumber = ConfigurationManager.AppSettings["PhoneNumber"].ToString().Trim();
        static TwilioBll() {
            TwilioClient.Init(accountSid, authToken);
        }
        // GET: Twilio
        public static void SendSms(string phoneNumber, string body) {
            var message = MessageResource.Create(
                to: new PhoneNumber(phoneNumber),
                from: new PhoneNumber(twilioPhoneNumber),
                body: body
            );
        }
    }
}
