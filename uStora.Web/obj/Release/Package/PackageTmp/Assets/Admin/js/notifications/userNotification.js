$(function () {
    // Click on notification icon for show notification
    $('li.noti-user').off('click').on('click', function (e) {
        updateNotification();
    });

    // update notification 
    function updateNotification() {
        $('#user-content').empty();
        $('#user-content').append($('<li>Đang tải dữ liệu...</li>'));

        $.ajax({
            type: 'GET',
            url: '/Admin/GetNotificationUsers',
            success: function (response) {
                $('#user-content').empty();
                if (response.data.length == 0) {
                    $('span.user-count').addClass('hide');
                    $('li.user-status').html('Không có thông báo !');
                    $('#all-user').addClass('hide');
                }
                else {
                    if (response.status) {
                        $('span.user-count').addClass('hide');
                    } else {
                        $('span.user-count').removeClass('hide');
                        $.each(response.data, function (index, value) {
                            if (value.IsViewed == false) {
                                count += 1;
                            }
                            $('li.user-status').html('Bạn có ' + response.data.length + ' thành viên đã đăng ký.');
                            var html = "<li class='count-item' title='Thông báo mới.'><a href='/admin#!/user_detail/" +
                           value.Id + "'>" + "<i class='fa fa-user text-aqua'></i>" +
                           "" + value.FullName + " đã đăng ký thành viên.</a> </li>";
                            $('#user-content').append($(html));
                        });
                        $('span.user-count').html(count);
                    }

                    var count = 0;
                    $('#all-user').removeClass('hide');
                    $.each(response.data, function (index, value) {
                        if (value.IsViewed == false) {
                            count += 1;
                            $('li.user-status').html('Bạn có ' + response.data.length + ' thành viên đã đăng ký.');
                            var html = "<li class='count-item' title='Thông báo mới.'><a href='/admin#!/user_detail/" +
                            value.Id + "'>" + "<i class='fa fa-user text-aqua'></i>" +
                            "" + value.FullName + " đã đăng ký thành viên.</a> </li>";
                            $('#user-content').append(html);
                        }
                        else {
                            $('li.user-status').html('Bạn có ' + response.data.length + ' thành viên đã đăng ký.');
                            var html = "<li class='count-item'><a href='/admin#!/user_detail/" +
                            value.Id + "'>" + "<i class='fa fa-user text-aqua'></i>" +
                            "" + value.FullName + " đã đăng ký thành viên.</a> </li>";
                            $('#user-content').append(html);
                        }
                    });
                    $('span.user-count').html(count);
                }

            },
            error: function (error) {
                console.log(error);
            }
        })
    }

    var userHub = $.connection.userHub;
    $.connection.hub.start().done(function () {
        console.log('userHub hub is started');
        updateNotification();
    });

    userHub.client.newuser = function (message) {
        if (message && message.toLowerCase() == "user") {
            updateNotification();
        }
    }

})