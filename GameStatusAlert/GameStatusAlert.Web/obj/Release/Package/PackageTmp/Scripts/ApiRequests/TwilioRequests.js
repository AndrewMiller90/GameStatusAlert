function SendSms(phoneNumber, message) {
    $.ajax({
        type: "post",
        url: '/Twilio/SendSms',
        data: JSON.stringify({ 'phoneNumber': phoneNumber, 'body': message }),
        contentType: "application/json; charset=utf-8",
    });
}