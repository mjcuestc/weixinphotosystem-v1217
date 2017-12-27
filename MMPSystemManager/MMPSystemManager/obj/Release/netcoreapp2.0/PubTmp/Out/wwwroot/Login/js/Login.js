$(document).ready(function () {

    $("#submit").click(function () {
        var username = $("#username").val();
        var password = $("#password").val();
        var package = { "username": username, "password": password };
        
        $.post(
            "../Api/SystemApi/Login",
            package,
            function (data) {

                if (data == true) {
                    var kk = "欢迎" + username + ",点击 确定 跳转";
                    alert(kk);
                    window.location.href = '../Index/home/home.html';
                }
                else {
                    alert("登录失败，请重新登录");
                    window.location.href = 'Login.html';
                }
            },
            "json"
        )
    })
});

