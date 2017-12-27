$(document).ready(function(){
	
    $("#btn1").click(function () {
        var username = $("#username").val();
        var telephone = $("#telephone").val();
        var scenic = $("#scenic").val();
        var position = $("#position").val();
        var reserpos = scenic + position;
        var resdate1 = $("#resdate").val();
        var timequantum = $("#timequantum").val();
        var resdate = resdate1 + timequantum;
        var package = {
            "username": username, "telephone": telephone,
            "reserpos": reserpos, "resdate": resdate
        };

        $.post
            (       
            "../Api/SystemApi/Insert",
            package,
            function (data) {
                
                if (document.getElementById("agreement").checked == true && resdate1 != "") {
                    if (data == true) {
                        alert("您预定成功");
                        window.location.href = 'reser.html';
                    }
                    else {
                        alert("预订失败，请重新预约");
                        window.location.href = 'reser.html';
                    }
                }
                else {
                    alert("预订失败，请重新预约");
                    window.location.href = 'reser.html';
                }
            },
            
            "json"
            )
    });
});
