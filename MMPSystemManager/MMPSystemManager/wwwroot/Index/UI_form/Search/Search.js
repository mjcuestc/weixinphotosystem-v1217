$(document).ready(function () {
    $("#Home,#Admin,#User").on("mouseover mouseout",
        function () {
            $(this).toggleClass("menu_tag_n");
        }
    );
    $("#Logo").on("click",
        function () {
            if ($(".menu_tag p").css("display") == "none") {
                $(".menu").css("width", "135px");
                $(".menu_tag p").css('display', 'block', 'margin-top', '-16px');
                $(".content").css('width', '89%');

            }
            else {
                $(".menu").css("width", "65px");
                $(".menu_tag p").css("display", "none");
                $(".content").css('width', '93%');
            }


        }
    );

    $("#Home").on("click",
        function () {
            window.location = "../../home/home.html";
        }
    );
    $("#Admin").on("click",
        function () {
            window.location = "../../admin/admin.html";
        }
    );
    $("#User").on("click",
        function () {
            window.location = "../../user/user.html";
        }
    );

    $(".search_ico").on("click",
        function () {

            var accountStatus_name = $("#seach_name").val();
            var accountStatus = $("#search_text").val();

            var url = '../../UI_form/Search/Search.html?' + accountStatus_name + '&' +accountStatus;
            window.location.href = url;
        }
    );


    var thisURL = document.URL;
    var getval = thisURL.split('?')[1];
    var accountStatus_name = getval.split("&")[0];
    var accountStatus = getval.split("&")[1];
    var package = {};
    package[accountStatus_name] = accountStatus;

    if (accountStatus_name == "adminid" || accountStatus_name == "adminname") {

        $.post("../../../Api/SystemApi/Search",
            package,
            function (data_return) {

                var data = data_return;
                if (data != '') {
                    $(".admin_table").css('display', 'block', 'height', '100%', 'width', '100%', 'background-color', '#FFFFFF');
                    var Admin_table = document.getElementById("table_t_admin");
                    for (var i = 0; i < data.length; i++) {
                        var row = Admin_table.insertRow(Admin_table.rows.length);

                        var c1 = row.insertCell(0);
                        c1.innerHTML = data[i].AdminNumber;
                        var c1 = row.insertCell(1);
                        c1.innerHTML = data[i].AdminName;
                        var c2 = row.insertCell(2);
                        c2.innerHTML = data[i].AdminContactPhone;
                        var c3 = row.insertCell(3);
                        c3.innerHTML = data[i].AdminLoginTime;
                        var c4 = row.insertCell(4);
                        if (data[i].AdminOnline == "True") {
                            c4.innerHTML = "是";
                        }
                        else {
                            c4.innerHTML = "否";
                        }
                        var c5 = row.insertCell(5);
                        c5.innerHTML = '<div class="delete"><button type="button" class="delete_btu" onclick="delete_fun(this)"/>删除</div><div class="admin_detail"><button type="button" class="deltail_btu" onclick="detail_fun_admin(this)"/>详情</div>';
                    }
                }
                else
                {
                    $(".no_result").css('display', 'block','height', '100%', 'width', '100%', 'background-color', '#FFFFFF');
                }
            }
        );
    }
    else
    {
        $.post("../../../Api/SystemApi/Search",
            package,
            function (data_return) {
                
                var data = data_return;
                if (data != '') {
                    $(".user_table").css('display', 'block', 'height', '100%', 'width', '100%', 'background-color', '#FFFFFF');
                    var User_table = document.getElementById("table_t_user");
                    for (var i = 0; i < data.length; i++) {
                        var row = User_table.insertRow(User_table.rows.length);

                        var c1 = row.insertCell(0);
                        c1.innerHTML = data[i].UserName;
                        var c2 = row.insertCell(1);
                        c2.innerHTML = data[i].UserContactPhone;
                        var c3 = row.insertCell(2);
                        c3.innerHTML = data[i].UserWechatName;
                        var c3 = row.insertCell(3);
                        c3.innerHTML = data[i].UserContactEmail;
                        var c3 = row.insertCell(4);
                        c3.innerHTML = '<div class="delete"><button type="button" class="delete_btu" onclick="delete_fun(this)"/>删除</div><div class="admin_detail"><button type="button" class="delete_btu" onclick="detail_fun_user(this)"/>详情</div>';
                    }

                }
                else {
                    $(".no_result").css('display', 'block', 'height', '100%', 'width', '100%', 'background-color', '#FFFFFF');
                }
            });
        
    }
    

}
);


function delete_fun(choose_info) {


    var delete_id = choose_info.parentNode.parentNode.parentNode.childNodes[0].textContent;
    //admin_info.parentNode.parentNode.parentNode.remove(admin_info.parentNode.parentNode); //IE不支持


    //兼容性解决
    var userAgent = navigator.userAgent;
    var isOpera = userAgent.indexOf("Opera") > -1; //判断是否Opera浏览器
    var isFF = userAgent.indexOf("Firefox") > -1; //判断是否Firefox浏览器
    var isSafari = userAgent.indexOf("Safari") > -1 && userAgent.indexOf("Chrome") == -1; //判断是否Safari浏览器
    var isChrome = userAgent.indexOf("Chrome") > -1 && userAgent.indexOf("Safari") > -1; //判断Chrome浏览器
    var isIE = userAgent.indexOf("Windows NT") > -1 && userAgent.indexOf("Gecko") > -1 && !isOpera & !isChrome;  //判断是否IE浏览器
    var isEdge = userAgent.indexOf("Edge") > -1; //判断是否IE的Edge浏览器

    if (isIE) {
        choose_info.parentNode.parentNode.parentNode.removeNode(choose_info.parentNode.parentNode);
    } else {
        choose_info.parentNode.parentNode.parentNode.remove(choose_info.parentNode.parentNode);
    }

    var package = { "Id": delete_id };
    $.post("../../../Api/SystemApi/Delete",
        package,
        function (data_return) {

            var data = data_return;

            if (data == true) {
                alert("删除成功");
            }
            else {
                alert("删除失败");
            }
        })
}


function detail_fun_admin(admin_info) {
    var detail_id = admin_info.parentNode.parentNode.parentNode.childNodes[0].textContent;
    window.location.href = '../../admin/admin_detail/admin_detail.html?id=' + detail_id;
}




function detail_fun_user(user_info) {
    var detail_id = user_info.parentNode.parentNode.parentNode.childNodes[0].textContent;
    window.location.href = '../../user/user_detail/user_detail.html?id=' + detail_id;
}