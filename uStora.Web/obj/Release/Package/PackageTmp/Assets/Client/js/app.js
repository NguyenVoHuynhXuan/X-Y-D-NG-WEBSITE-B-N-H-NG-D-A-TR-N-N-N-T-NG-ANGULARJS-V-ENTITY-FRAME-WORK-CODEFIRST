$(function () {
    $(document).ready(function () {

        var $loading = $('#overlay');
        $(document)
          .ajaxStart(function () {
              $loading.addClass('open');
          })
          .ajaxStop(function () {
              $loading.removeClass('open');
          });
        $('#contact-form').bootstrapValidator({
            // To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            fields: {
                fullname: {
                    validators: {
                        stringLength: {
                            min: 2,
                            message: "nhập nhiều hơn 2 ký tự",
                        },
                        notEmpty: {
                            message: 'Vui lòng nhập họ tên'
                        }
                    }
                },

                email: {
                    validators: {
                        notEmpty: {
                            message: 'Vui lòng nhập địa chỉ Email'
                        },
                        emailAddress: {
                            message: 'Địa chỉ Email không hợp lệ'
                        }
                    }
                },

                comment: {
                    validators: {
                        stringLength: {
                            min: 10,
                            message: 'Vui lòng nhập nội dung nhiều hơn 10 ký tự'
                        },
                        notEmpty: {
                            message: 'Vui lòng nhập nội dung tin nhắn'
                        }
                    }
                }
            }
        }).on('success.form.bv', function (e) {
            AddFeedback();
            $('#contact-form').data('bootstrapValidator').resetForm();
        });
    });
    function ResetValue() {
        $('#fullname').val(""),
        $('#phone').val(""),
        $('#address').val(""),
        $('#email').val(""),
        $('#website').val(""),
        $('#comment').val("")
    }
    function AddFeedback() {
        var obj = {
            Name: $('#fullname').val(),
            Phone: $('#phone').val(),
            Address: $('#address').val(),
            Email: $('#email').val(),
            Website: $('#website').val(),
            Message: $('#comment').val()
        }
        $.ajax({
            url: '/Contact/SendFeedback',
            data: JSON.stringify(obj),
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",

            success: function (result) {
                ResetValue();
                $('#success_message').removeClass('hide');
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
})