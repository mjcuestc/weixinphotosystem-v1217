$(document).ready(function () {

    var thisURL = document.URL;
    var getval = thisURL.split('?')[1];
    var showval = getval.split("=")[1];

    var package = { "username": showval };
    $.post("../../../Api/SystemApi/Search",
        package,
        function (data_return) {

            var data = data_return;
            $("#UserNumber").val(data[0].UserNumber);
            $("#UserName").val(data[0].UserName);
            $("#UserWechatName").val(data[0].UserWechatName);
            $("#UserId").val(data[0].UserId);
            $("#UserContactPhone").val(data[0].UserContactPhone);
            $("#UserContactEmail").val(data[0].UserContactEmail);
            $("#UserFacepict").val(data[0].UserFacepict);
            $("#UserPicTime").val(data[0].UserPicTime);
            $("#Remark").val(data[0].Remark);
        });

}
);