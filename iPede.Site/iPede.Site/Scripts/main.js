//Should be set before calling updateCartAddButtons()
var cartUrl = '/Cart';

//Callback for functions which updates the cart.
//It basically correctly loads the products cart button's classes
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

//Loads the products cart button's classes
function updateCart() {
    var getCartDataUrl = cartUrl + '/GetData'
    $.ajax({
        url: getCartDataUrl,
        cache: false,
        type: 'POST',
        success: updateCartCallback
    });
}

//Updates cart icon (for example, with a short products list)
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
    //Add Buy Product functionality for each button with "cart-add-button" class.
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
        $("#site-modal .modal-content").html($("#modal-loading-content").html());
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
    
});

