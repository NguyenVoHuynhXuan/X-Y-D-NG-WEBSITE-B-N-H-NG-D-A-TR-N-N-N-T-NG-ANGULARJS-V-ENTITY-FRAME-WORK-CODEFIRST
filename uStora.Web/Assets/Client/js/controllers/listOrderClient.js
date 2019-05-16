var listOrder = {
    init: function () {

        listOrder.eventRegisters();
    },
    eventRegisters: function () {
        listOrder.loadOrderList();
    },
    loadOrderList: function () {
        $.ajax({
            url: '/ShoppingCart/GetListOrder',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var $loading = $('#overlay');
                    var template = $('#templateListCart').html();
                    var html = '';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        $loading.addClass('open');
                        html += Mustache.render(template, {
                            ProductName: item.Name,
                            ProductId: item.ProductId,
                            Image: item.Image,
                            FPrice: numeral(item.Price).format('0,0'),
                            Quantity: item.Quantity,
                            Alias: item.Alias,
                            PaymentStatus: (item.PaymentStatus == 0 ? "Chờ duyệt" : "Đang chuyển hàng")
                        });
                    });
                    setTimeout(function () {
                        $loading.removeClass('open');
                        $('#listCartBody').html(html);
                    }, 500);

                    cart.registerEvents();
                }
            }
        });
    }
}
listOrder.init();

