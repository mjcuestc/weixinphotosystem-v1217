$(document).ready(function () {

    $("#Home,#Admin,#User").on("mouseover mouseout",
        function () {
            $(this).toggleClass("menu_tag_n");
        });
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


        });

    $("#Home").on("click",
        function () {
            window.location = "../home/home.html";
        });

    $("#Admin").on("click",
        function () {
            window.location = "../admin/admin.html";
        });

    $("#User").on("click",
        function () {
            window.location = "../user/user.html";
        }
    );

    $(".search_ico").on("click",
        function () {

            var accountStatus_name = $("#seach_name").val(); 
            var accountStatus = $("#search_text").val();

            
            var url = '../UI_form/Search/Search.html?' + accountStatus_name + '&' + accountStatus;
            window.location.href = url;
        }
    );

}
);
