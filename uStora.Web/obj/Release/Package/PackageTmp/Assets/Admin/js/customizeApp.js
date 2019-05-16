$(document).ready(function () {
    $(".list-inline img").click(function () {
        $(this).closest("li").fadeOut(function () {
            $(this).remove();
        });
    });
})