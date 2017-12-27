$(document).ready(function () {

    var thisURL = document.URL;
    var getval = thisURL.split('?')[1];
    var showval = getval.split("=")[1]; 
    
    var package = {"adminid":showval};
    $.post("../../../Api/SystemApi/Search",
        package,
        function (data_return) {

            var data = data_return;
            $("#Account").val(data[0].AdminNumber);
            $("#Password").val(data[0].AdminPasswd);
            $("#AdminId").val(data[0].AdminId);
            $("#AdminGrade").val(data[0].AdminGrade);
            $("#AdminName").val(data[0].AdminName);
            $("#AdminContactPhone").val(data[0].AdminContactPhone);
            $("#AdminContactEmail").val(data[0].AdminContactEmail);
            $("#AdminPicture").val(data[0].AdminPicture);
            $("#AdminIdPict").val(data[0].AdminIdPict);
            $("#Remark").val(data[0].Remark);
        });


    $("#submit").click
        (
        function () {
            var Account = $("#Account").val();
            var password = $("#Password").val();
            var Id = $("#AdminId").val();
            var AdminGrade = $("#AdminGrade").val();
            var AdminName = $("#AdminName").val();
            var AdminContactPhone = $("#AdminContactPhone").val();
            var AdminContactEmail = $("#AdminContactEmail").val();
            var AdminPicture = $("#AdminPicture").val();
            var AdminIdPict = $("#AdminIdPict").val();
            var Remark = $("#Remark").val();

            var package_data = {
                "admin": Account, "AdminId": Id,
                "AdminGrade": AdminGrade, "AdminName": AdminName,
                "AdminPasswd": password, "AdminContactPhone": AdminContactPhone,
                "AdminContactEmail": AdminContactEmail, "AdminPicture": AdminPicture,
                "AdminIdPict": AdminIdPict, "Remark": Remark
            };
            $.post("../../../Api/SystemApi/Update",
                package_data,
                function (data) {
                    if (data == true) {
                        alert("Success");
                        window.location.href = '../admin.html';
                    }
                    else {
                        alert("Failure ");
                    }
                },
                "json"
                )
        }
    );

    $("#change").click
        (
        function () {
            $(".admin_info_detail").attr("disabled",false);
        }
        );
}
);