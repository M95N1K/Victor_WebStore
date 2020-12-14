Cart = {
    _properties: {
        getCartViewLink: "",
        getCartItemCountLink: "",
        getCartItemPriceLink: "",
        incrimentLink: "",
        decrimentLink: "",
        removeItemLink: "",
        getTotalPriceLink: "",
        getTotalCountLink: "",
        addToCartLink: ""
    },

    init: function (properties) {
        $.extend(Cart._properties, properties);

        $(".add-to-cart").click(Cart.addToCart); // привязываем новое действие click
        $(".cart_quantity_up").click(Cart.incrimentProduct);// привязываем новое действие click
        $(".cart_quantity_down").click(Cart.decrimentProduct);// привязываем новое действие click
        $(".cart_quantity_delete").click(Cart.removeProduct);// привязываем новое действие click
    },

    

    addToCart: function (event) {
        event.preventDefault();

        var button = $(this);
        const id = button.data("id");

        $.get(Cart._properties.addToCartLink + "/" + id)
            .done(function () {
                Cart.showToolTip(button);
                Cart.refreshCartView();
                Cart.refreshTotalPrice();
            })
            .fail(function () { console.log("AddToCart fail!"); });
    },

    incrimentProduct: function (event) {
        event.preventDefault();
        var button = $(this);
        const id = button.data("id");

        $.get(Cart._properties.incrimentLink + "/" + id)
            .done(function () {
                Cart.refreshCartItem(id);
                Cart.refreshCartView();
                Cart.refreshTotalPrice();

            })
            .fail(function () { console.log("incrimentProduct fail!"); });

    },

    decrimentProduct: function (event) {
        event.preventDefault();
        var button = $(this);
        const id = button.data("id");

        $.get(Cart._properties.decrimentLink + "/" + id)
            .done(function () {
                Cart.refreshCartItem(id);
                Cart.refreshCartView();
                Cart.refreshTotalPrice();

            })
            .fail(function () { console.log("decrimentProduct fail!"); });

    },

    removeProduct: function (event) {
        event.preventDefault();
        var button = $(this);
        const id = button.data("id");

        $.get(Cart._properties.removeItemLink + "/" + id)
            .done(function () {
                Cart.refreshCartView();
                $("#" + id).remove();
                Cart.refreshTotalPrice();
            })
            .fail(function () { console.log("removeProduct fail!"); });
    },

    showToolTip: function (button) {
        button.tooltip({ title: "Добавление в корзину" }).tooltip("show");
        setTimeout(function () { button.tooltip("destroy"); }, 1000)
    },

    refreshTotalPrice: function () {
        $.get(Cart._properties.getTotalPriceLink)
            .done(function (cartHtml) {
                $(".total_price").html(cartHtml);
            })
            .fail(function () { console.log("refreshTotalPrice fail!"); });

        $.get(Cart._properties.getTotalCountLink)
            .done(function (cartHtml) {
                $(".total_count").html(cartHtml);
            })
            .fail(function () { console.log("refreshTotalCount fail!"); });
    },

    refreshCartView: function () {
        var container = $("#cart-container");
        $.get(Cart._properties.getCartViewLink)
            .done(function (cartHtml) {
                container.html(cartHtml);
            })
            .fail(function () { console.log("refreshCartView fail!"); });
    },

    refreshCartItem: function (id) {
        var container = $("#" + id).find(".cart_quantity_input");
        $.get(Cart._properties.getCartItemCountLink + "/" + id)
            .done(function (cartHtml) {
                container.html(cartHtml);
            })
            .fail(function () { console.log("refreshCartItem fail!"); });

        var price = $("#" + id).find(".cart_total_price");
        $.get(Cart._properties.getCartItemPriceLink + "/" + id)
            .done(function (cartHtml) {
                price.html(cartHtml);
            })
            .fail(function () { console.log("refreshCartItem fail!"); });
    }
}