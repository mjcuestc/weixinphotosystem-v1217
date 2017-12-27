$(document).ready(function () {

    var package = {"admin":"100"};
    $.post("../../Api/SystemApi/Getall",
        package,
        function (data_return) {

            var data = data_return;
            var Admin_table = document.getElementById("table_t");
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
                    c4.innerHTML = "ÊÇ";
                }
                else {
                    c4.innerHTML = "·ñ";
                }
                var c5 = row.insertCell(5);
                c5.innerHTML = '<div class="delete"><button type="button" class="delete_btu" onclick="delete_fun(this)"/>É¾³ý</div><div class="admin_detail"><button type="button" class="deltail_btu" onclick="detail_fun(this)"/>ÏêÇé</div>';
            }

        });

    

    $("#Add").click
        (
        function () {
            window.location.href = './admin_add/admin_add.html';
        }
    );


});


function delete_fun(admin_info)
{


    var delete_id = admin_info.parentNode.parentNode.parentNode.childNodes[0].textContent;
    //admin_info.parentNode.parentNode.parentNode.remove(admin_info.parentNode.parentNode); //IE²»Ö§³Ö


    //¼æÈÝÐÔ½â¾ö
    var userAgent = navigator.userAgent;
    var isOpera = userAgent.indexOf("Opera") > -1; //ÅÐ¶ÏÊÇ·ñOperaä¯ÀÀÆ÷
    var isFF = userAgent.indexOf("Firefox") > -1; //ÅÐ¶ÏÊÇ·ñFirefoxä¯ÀÀÆ÷
    var isSafari = userAgent.indexOf("Safari") > -1 && userAgent.indexOf("Chrome") == -1; //ÅÐ¶ÏÊÇ·ñSafariä¯ÀÀÆ÷
    var isChrome = userAgent.indexOf("Chrome") > -1 && userAgent.indexOf("Safari") > -1; //ÅÐ¶ÏChromeä¯ÀÀÆ÷
    var isIE = userAgent.indexOf("Windows NT") > -1 && userAgent.indexOf("Gecko") > -1 && !isOpera & !isChrome;  //ÅÐ¶ÏÊÇ·ñIEä¯ÀÀÆ÷
    var isEdge = userAgent.indexOf("Edge") > -1; //ÅÐ¶ÏÊÇ·ñIEµÄEdgeä¯ÀÀÆ÷

    if (isIE) {
        admin_info.parentNode.parentNode.parentNode.removeNode(admin_info.parentNode.parentNode);
    } else {
        admin_info.parentNode.parentNode.parentNode.remove(admin_info.parentNode.parentNode);
    }

    var package = {"Id":delete_id};
    $.post("../../Api/SystemApi/Delete",
        package,
        function (data_return) {

            var data = data_return;
                
                if (data == true) {
                    alert("É¾³ý³É¹¦");
                }
                else {
                    alert("É¾³ýÊ§°Ü");
                }
        })
}


function detail_fun(admin_info)
{
    var detail_id = admin_info.parentNode.parentNode.parentNode.childNodes[0].textContent;
    window.location.href = './admin_detail/admin_detail.html?id=' + detail_id;
}