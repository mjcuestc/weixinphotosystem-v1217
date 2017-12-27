$(document).ready(function () {

    $("#btn3").click(function () {
        var telnumber = $("#telnumber").val();
        var package = {
            "telnumber": telnumber 
        };

        $.post
            (
            "../Api/SystemApi/Research",
            package,
            function (data_return) {
                var data = data_return;
                var User_table = document.getElementById("table_t");
                for (var i = 0; i < data.length; i++) {
                    var row = User_table.insertRow(User_table.rows.length);

                    var c1 = row.insertCell(0);
                    c1.innerHTML = data[i].UserName;
                    var c2 = row.insertCell(1);
                    c2.innerHTML = data[i].UserContactPhone;
                    var c3 = row.insertCell(2);
                    c3.innerHTML = data[i].UserReservationPos;
                    var c4 = row.insertCell(3);
                    c4.innerHTML = data[i].UserReservationTime;
                    var c5 = row.insertCell(4);
                    c5.innerHTML = '<div class="delete"><button type="button" class="delete_btu" onclick="delete_fun(this)"/>删除</div>';
                    
                }
                }
            );
    });
});

function delete_fun(user_info) {
    var delete_id = user_info.parentNode.parentNode.parentNode.childNodes[0].textContent;
    var package = { "Id": delete_id };
    $.post("../Api/SystemApi/Delete",
        package,
        function (data_return) {
            var data = data_return;

            if (data == true) {
                alert("删除成功");
            }
            else {
                alert("删除失败");
            }
        }
    )
}
