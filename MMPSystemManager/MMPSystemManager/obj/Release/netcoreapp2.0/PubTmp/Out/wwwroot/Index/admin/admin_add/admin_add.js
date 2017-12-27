$(document).ready(function()
    {


        $("#submit").click
            (
            function () {
                var Account = $("#Account").val();
                var password = $("#Password").val();
                var Id = $("#AdminId").val();
                var AdminGrade = $("#AdminGrade").val();
                var AdminName_first = $("#AdminName_first").val();
                var AdminName_last = $("#AdminName_last").val();
                var AdminName = AdminName_first + AdminName_last;
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
                $.post
                    (
                    "../../../Api/SystemApi/Insert",
                    package_data,
                    function (data) {
                        if(data==true)
                        {
                            alert("Success");
                            window.location.href = '../admin.html';
                        }
                        else
                        {
                            alert("Failure ");
                            window.location.href = './admin_add.html';
                        }
                    },
                    "json"
                    )
            }
            );
    }
);