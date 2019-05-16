$(function () {
    // Click on notification icon for show notification
    $('li.noti-feedback').off('click').on('click', function (e) {
        updateNotification();
    });

    // update notification 
    function updateNotification() {
        $('#feedback-content').empty();
        $('#feedback-content').append($('<li>Đang tải dữ liệu...</li>'));

        $.ajax({
            type: 'GET',
            url: '/Admin/GetNotificationFeedbacks',
            success: function (response) {
                $('#feedback-content').empty();
                if (response.data.length == 0) {
                    $('span.feedback-count').addClass('hide');
                    $('li.feedback-status').html('Không có thông báo phản hồi!');
                    $('#all-feedback').addClass('hide');
                }
                else {
                    if (response.status) {
                        $('span.feedback-count').addClass('hide');
                    } else {
                        $('span.feedback-count').removeClass('hide');
                        $.each(response.data, function (index, value) {
                            if (value.Status) {
                                count += 1;
                            }
                            $('li.feedback-status').html('Bạn có ' + response.data.length + ' thông báo.');
                            var html = "<li class='count-item'><a href='/admin#!/detail/" + value.ID + "'>" +
                                "<i class='fa fa-feed text-aqua' title='Thông báo mới.'></i>" +
                                " Phản hồi từ " + value.Name + " .</a> </li>";
                            $('#feedback-content').append($(html));
                        });
                        $('span.feedback-count').html(count);
                    }
                   
                    var count = 0;
                    $('#all-feedback').removeClass('hide');
                    $.each(response.data, function (index, value) {
                        if (value.Status) {
                            count += 1;
                            $('li.feedback-status').html('Bạn có ' + response.data.length + ' thông báo.');
                            var html = "<li class='count-item'  title='Thông báo mới.'><a href='/admin#!/detail/" + value.ID + "'>" +
                                "<i class='fa fa-feed text-aqua'></i>" +
                                " Phản hồi từ " + value.Name + " <i class='fa fa-eye-slash'></i></a> </li>";
                            $('#feedback-content').append(html);
                        }
                        else {
                            $('li.feedback-status').html('Bạn có ' + response.data.length + ' thông báo.');
                            var html = "<li class='count-item'><a href='/admin#!/detail/" + value.ID + "'>" +
                                "<i class='fa fa-feed text-aqua'></i>" +
                                " Phản hồi từ " + value.Name + " .</a> </li>";
                            $('#feedback-content').append(html);
                        }
                    });
                    $('span.feedback-count').html(count);
                }

            },
            error: function (error) {
                console.log(error);
            }
        })
    }

    var feedbackHub = $.connection.feedbackHub;
    $.connection.hub.start().done(function () {
        console.log('feedbackHub hub is started');
        updateNotification();
    });

    feedbackHub.client.newfeedback = function (message) {
        if (message && message.toLowerCase() == "feedback") {
            updateNotification();
        }
    }

})