function SendSms(phoneNumber, message) {
    $.ajax({
        type: "post",
        url: '@Url.Action("SendSms", "Twilio")',
        data: JSON.stringify({ 'phoneNumber': phoneNumber, 'body': message }),
        contentType: "application/json; charset=utf-8",
    });
}