function GetGameStateByName(region, name, onSuccess) {
    $.ajax({
        type: "post",
        url: '/Api/GetGameStateByName',
        data: JSON.stringify({ 'region': region, 'name': name }),
        contentType: "application/json",
        dataType: "json",
        async: true,
        success: onSuccess,
        error: function (jqXHR, textStatus, errorThrown) {
            //TODO: Write Erorr Handling
            alert(textStatus + ": " + errorThrown);
        }
    });
}
function GetGameStateById(region, summonerId, onSuccess) {
    $.ajax({
        type: "post",
        url: '/Api/GetGameStateById',
        data: JSON.stringify({ 'region': region, 'summonerId': summonerId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: onSuccess,
        error: function (jqXHR, textStatus, errorThrown) {
            //TODO: Write Error Handling
            alert(textStatus + ": " + errorThrown);
        }
    });
}