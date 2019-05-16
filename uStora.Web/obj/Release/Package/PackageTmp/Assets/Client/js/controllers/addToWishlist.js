var wishlist = {
    init: function () {
        wishlist.registerEvents();
    },
    registerEvents: function () {
        $('i#btnAddWishlistDisable').off('click').on('click', function (e) {
            e.preventDefault();
            toastr.warning('Vui lòng đăng nhập để sử dụng tính năng này.');
        });
        $('i#btnAddWishlist').off('click').on('click', function (e) {
            e.preventDefault();
            var res = confirm("Bạn muốn thêm sản phẩm này vào danh mục yêu thích?");
            if (res) {
                var productId = parseInt($(this).data('id'));
                wishlist.addWishlist(productId);
            }
        });

    },
    addWishlist: function (productId) {
        $.ajax({
            url: '/Wishlist/Add',
            type: 'POST',
            dataType: 'json',
            data: {
                productId: productId
            },
            success: function (res) {
                if (res.status == 1)
                    toastr.success('Đã thêm vào mục yêu thích.');
                else
                    if (res.status == 2)
                        toastr.warning(res.message);
                    else
                        toastr.error('Thêm vào mục yêu thích không thành công.');
            }
        });
    }

}
wishlist.init();