$(function () {
    $('a[href="#search"]').on('click', function(event) {
        event.preventDefault();
        $('#search').addClass('open');
        $('#search > form > input[type="text"]').focus();
    });
    
    $('#search, #search button.close, #btnSearch').on('click keyup', function (event) {
        if (event.target == this || event.target.className == 'close' || event.keyCode == 27 || event.keyCode == 13) {
            $(this).removeClass('open');
        }
    });
    
    
    // $(window).resize(function(){
    //     var boxWidth = $(window).width();
    //         $('input[type="search"]').css({"width": boxWidth});
    //         return false;
    // })
    
    ////Do not include! This prevents the form from submitting for DEMO purposes only!
    //$('form').submit(function(event) {
    //    event.preventDefault();
    //    return false;
    //});
});