Cart = {
    _properties: {
        getCartViewLink: "",
        getCartViewItemLink: "",
        incrimentLink: "",
        decrimentLink: "",
        removeItemLink: "",
        getTotalPriceLink: "",
        addToCartLink: ""
    },

    init: function (properties) {
        $.extend(Cart._properties, properties);

        Cart.addAction();
    },

    addAction: function () {
        $(".add-to-cart").unbind("click"); // отвязываем действие click
        $(".add-to-cart").click(Cart.addToCart); // привязываем новое действие click
        $(".cart_quantity_up").unbind("click"); // отвязываем действие click
        $(".cart_quantity_up").click(Cart.incrimentProduct);// привязываем новое действие click
        $(".cart_quantity_down").unbind("click"); // отвязываем действие click
        $(".cart_quantity_down").click(Cart.decrimentProduct);// привязываем новое действие click
        $(".cart_quantity_delete").unbind("click"); // отвязываем действие click
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
            .fail(function () { console.log("AddToCart fail!");});
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
        setTimeout(function () { button.tooltip("destroy");},1000)
    },

    refreshTotalPrice: function () {
        var container = $("#total_price_vc");
        $.get(Cart._properties.getTotalPriceLink)
            .done(function (cartHtml) {
                container.html(cartHtml);
            })
            .fail(function () { console.log("refreshTotalPrice fail!"); });
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
        var container = $("#" + id);
        $.get(Cart._properties.getCartViewItemLink + "/" + id)
            .done(function (cartHtml) {
                container.html(cartHtml);
                Cart.addAction();
            })
            .fail(function () { console.log("refreshCartItem fail!"); });
    }
}