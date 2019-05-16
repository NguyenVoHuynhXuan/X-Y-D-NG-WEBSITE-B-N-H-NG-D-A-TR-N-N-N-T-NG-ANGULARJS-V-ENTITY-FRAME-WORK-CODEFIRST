$(function(){
	var top = 45;
	var left = 5;
	$(document).ready(function(){
		checkPostion();
		addCartMobile();
		flyToCart();
		flyToCartShop();
	});

	$(window).scroll(function () {
		checkPostion();
		addCartMobile();
	});//end scroll

	$(window).resize(function () {
		checkPostion();
		addCartMobile();
		if(isMobileScreen())
		{
			top = 5;
			left= 20;
		}
	}); //end resize

	function checkPostion(){
		if ($(this).scrollTop() > 100 ) {
			$('.col-sm-6.shopping-cart').addClass('shopping-cart-fixed');
			$('span.cart-title').addClass('hide-text');
			$('fa fa-shopping-cart').addClass('icon-center');
			$('.shopping-item').css({"background-color":"#dbd9d9"});
			return false;

		} else {
			$('.col-sm-6.shopping-cart').removeClass('shopping-cart-fixed');
			$('span.cart-title').removeClass('hide-text');
			$('fa fa-shopping-cart').removeClass('icon-center');
			$('.shopping-item').css({"background-color":"#FFF"});
			return false;
		}
	} //end checkPostion

	function isMobileScreen(){
		if($(window).width() <= 767)
			return true;
		else
			return false;
	}//end isMobileScreen

	function addCartMobile(){
		if($(window).width() <= 767 && $(this).scrollTop() > 100 )
		{
			$('.col-sm-6.shopping-cart').addClass('cart-mobile');
			return false;
		}
		else
		{
			$('.col-sm-6.shopping-cart').removeClass('cart-mobile');
			return false;
		}
	}//end addCartMobile

	function flyToCart(){
		if(isMobileScreen()){
			top = 5;
			left= 20;
			$('.add-to-cart').on('click',function(event)
			{
				event.preventDefault();
				var cart = $('.shopping-cart-fixed');
				var imgtodrag = $(this).parents('.single-product').find("img").eq(0);
				if (imgtodrag) {
					var imgclone = imgtodrag.clone()
					.offset({
						top: imgtodrag.offset().top,
						left: imgtodrag.offset().left
					})
					.css({
						'opacity': '0.5',
						'position': 'absolute',
						'height': '150px',
						'width': '150px',
						'z-index': '100'
					})
					.appendTo($('body'))
					.animate({
						'top': cart.offset().top + top,
						'left': cart.offset().left+ left,
						'width': 40,
						'height': 43
					}, 1000, 'easeInOutExpo');

					setTimeout(function () {
						cart.effect("shake", {
							times: 2
						}, 200);
					}, 1500);
				//product-count

				imgclone.animate({
					'width': 0,
					'height': 0
				}, function () {
					$(this).detach()
				});
			}
			});
		}
		else
		{
			top = 45;
			left= 5;
			$('.add-to-cart').on('click',function(event){
				event.preventDefault();
				var cart = $('.shopping-cart-fixed');
				var imgtodrag = $(this).parents('.single-product').find("img").eq(0);
				if (imgtodrag) {
					var imgclone = imgtodrag.clone()
					.offset({
						top: imgtodrag.offset().top,
						left: imgtodrag.offset().left
					})
					.css({
						'opacity': '0.5',
						'position': 'absolute',
						'height': '150px',
						'width': '150px',
						'z-index': '100'
					})
					.appendTo($('body'))
					.animate({
						'top': cart.offset().top + top,
						'left': cart.offset().left+ left,
						'width': 40,
						'height': 43
					}, 1000, 'easeInOutExpo');

					setTimeout(function () {
						cart.effect("shake", {
							times: 2
						}, 200);
					}, 1500);
				//product-count

				imgclone.animate({
					'width': 0,
					'height': 0
				}, function () {
					$(this).detach()
				});
			}
		});
		}
		
	}//end flyToCart

	function flyToCartShop() {
	    if (isMobileScreen()) {
	        top = 5;
	        left = 20;
	        $('.add_to_cart_button').on('click', function (event) {
	            event.preventDefault();
	            var cart = $('.shopping-cart-fixed');
	            var imgtodrag = $(this).parents('.single-shop-product').find("img").eq(0);
	            if (imgtodrag) {
	                var imgclone = imgtodrag.clone()
					.offset({
					    top: imgtodrag.offset().top,
					    left: imgtodrag.offset().left
					})
					.css({
					    'opacity': '0.5',
					    'position': 'absolute',
					    'height': '150px',
					    'width': '150px',
					    'z-index': '100'
					})
					.appendTo($('body'))
					.animate({
					    'top': cart.offset().top + top,
					    'left': cart.offset().left + left,
					    'width': 40,
					    'height': 43
					}, 1000, 'easeInOutExpo');

	                setTimeout(function () {
	                    cart.effect("shake", {
	                        times: 2
	                    }, 200);
	                }, 1500);
	                //product-count

	                imgclone.animate({
	                    'width': 0,
	                    'height': 0
	                }, function () {
	                    $(this).detach()
	                });
	            }
	        });
	    }
	    else {
	        top = 45;
	        left = 5;
	        $('.add_to_cart_button').on('click', function (event) {
	            event.preventDefault();
	            var cart = $('.shopping-cart-fixed');
	            var imgtodrag = $(this).parents('.single-shop-product').find("img").eq(0);
	            if (imgtodrag) {
	                var imgclone = imgtodrag.clone()
					.offset({
					    top: imgtodrag.offset().top,
					    left: imgtodrag.offset().left
					})
					.css({
					    'opacity': '0.5',
					    'position': 'absolute',
					    'height': '150px',
					    'width': '150px',
					    'z-index': '100'
					})
					.appendTo($('body'))
					.animate({
					    'top': cart.offset().top + top,
					    'left': cart.offset().left + left,
					    'width': 40,
					    'height': 43
					}, 1000, 'easeInOutExpo');

	                setTimeout(function () {
	                    cart.effect("shake", {
	                        times: 2
	                    }, 200);
	                }, 1500);
	                //product-count

	                imgclone.animate({
	                    'width': 0,
	                    'height': 0
	                }, function () {
	                    $(this).detach()
	                });
	            }
	        });
	    }

	}//end flyToCart

	
})