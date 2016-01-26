//Should be set before calling updateCartAddButtons()
var cartUrl = '/Cart';

function updateCartCallback(data) {
    //For each cart add button on screen
    $(".cart-add-button").each(function () {
        //Get the button
        var button = $(this);
        var productId = button.data("product-id");
        //Get its cart present button
        var cartPresentButton = $(".cart-present-button[data-product-id='" + productId + "']");
        //If the array of cart' product ids contains this button's productId
        if ($.inArray(productId, data.ProductIds) == -1) {
            //Blablabla
            button.show();
            cartPresentButton.hide();
        }
        else {
            button.hide();
            cartPresentButton.show();
        }
    });
    updateCartIcon();
}

function updateCart() {
    var getCartDataUrl = cartUrl + '/GetData'
    $.ajax({
        url: getCartDataUrl,
        cache: false,
        type: 'POST',
        success: updateCartCallback
    });
}

function updateCartIcon() {
    var cartIconUrl = cartUrl + '/Icon';
    $.ajax({
        url: cartIconUrl,
        cache: false,
        type: 'POST',
        success: function (data) {
            $("#cart-icon-div").html(data);
        }
    });
}

jQuery(function ($) {
    //Botão de busca
    new UISearch(document.getElementById('sb-search'));

    $(".cart-add-button").click(function () {
        var addToCartUrl = cartUrl + '/Add'
        var id = $(this).data("product-id");
        //Adiciona o item ao carrinho
        $.ajax({
            url: addToCartUrl,
            data: { id: id },
            type: 'POST',
            cache: false,
            success: updateCartCallback
        });
        $("#site-modal").html($("#modal-loading").html());
        $("#site-modal").modal();
        //Pega a view de item adicionado para este produto
        $.ajax({
            url: cartUrl + '/ItemAddedPartial',
            data: { id: id },
            type: 'POST',
            cache: false,
            success: function (data) {
                $("#site-modal .modal-content").html(data);
                
            },
            error: function (x) {
                alert(x.responseText);
            }
        });
        
    });
    
    //Flag dropdown (sample)
    $(".dropdown img.flag").addClass("flagvisibility");

    $(".dropdown dt a").click(function () {
        $(".dropdown dd ul").toggle();
    });

    $(".dropdown dd ul li a").click(function () {
        var text = $(this).html();
        $(".dropdown dt a span").html(text);
        $(".dropdown dd ul").hide();
        $("#result").html("Selected value is: " + getSelectedValue("sample"));
    });

    function getSelectedValue(id) {
        return $("#" + id).find("dt a span.value").html();
    }

    $(document).bind('click', function (e) {
        var $clicked = $(e.target);
        if (!$clicked.parents().hasClass("dropdown"))
            $(".dropdown dd ul").hide();
    });


    $("#flagSwitcher").click(function () {
        $(".dropdown img.flag").toggleClass("flagvisibility");
    });
    //--End of Flag dropdown (sample)

});

