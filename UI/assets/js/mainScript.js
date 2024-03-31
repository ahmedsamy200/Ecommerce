function AddToCart(ProdID, quantity) {
    $.ajax({
        url: "/Cart/AddToCart?productID=" + ProdID +
        "&quantity=" + quantity +
        "&savedForLater=" + false,
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $('.action-loader').show();
        },
        success: function (result) {
            if (result == "success") {
                $('.action-loader').hide();
                Notiflix.Notify.Success('تم اضافه عنصر الي السله');
                CountItemInCart();
                $('.invalidQuantity').hide();

            }
            else if (result == "failed") {
                $('.action-loader').hide();
                Notiflix.Notify.Failure('عذرا تم خطا ما');

            }
            else if (result == "GoToLogin") {
                $('.action-loader').hide();
                window.location.href = "/account/login";

            }
        }
    });
}

function AddToFavorate(ProdID) {
    $.ajax({
        url: "/Cart/AddToFavorate?productID=" + ProdID +
        "&quantity=" + 1 +
        "&savedForLater=" + true,
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $('.action-loader').show();
        },
        success: function (result) {

            if (result.savedInFavorate)
            {
                $('.action-loader').hide();
                Notiflix.Notify.Success('تم اضافه عنصر الي المفضله');
                $(".heart" + result.productID + "").css("font-weight", "900");
                CountItemInFavorate();
            }
            else if (result.savedInFavorate == false && result.userID != 0) {
                $('.action-loader').hide();
                Notiflix.Notify.Failure('تم حذف العنصر من المفضله');
                $(".heart" + result.productID + "").css("font-weight", "400");
                CountItemInFavorate();

            }
            else if (result.userID == 0) {
                $('.action-loader').hide();
                window.location = "/account/login";
            }

        }
    });
}

function CountItemInCart() {
    $.ajax({
        url: "/Cart/CountItemInCart",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('.cart-count').text(result);
        }
    });
}

function CountItemInFavorate() {
    $.ajax({
        url: "/Cart/CountItemInFavorate",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('.Favotrite-count').text(result);
        }
    });
}

function CheakFavorate() {
    $.ajax({
        url: "/Cart/CheakFavorate",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {
                $(".heart" + item.productID + "").css("font-weight", "900");
            });
        }
    });
}

$(document).ready(function () {


        $(window).scroll(function () {
            if ($(this).scrollTop() > 100) {
                $('.back-top').fadeIn();
            } else {
                $('.back-top').fadeOut();
            }
        });

        $('.back-top').click(function () {
        $("html, body").animate({ scrollTop: 0 }, 600);
        return false;
    });
		
	});