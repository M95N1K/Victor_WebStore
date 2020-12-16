﻿Catalog = {
    _properties: {
        getViewLink: ""
    },
    init: properties => {
        $.extend(Catalog._properties, properties);
        $(".pagination li a").click(Catalog.clickOnPage);
    },

    clickOnPage: function (event) {
        event.preventDefault();

        const button = $(this);

        if (button.prop("href").length > 0) {
            const page = button.data("page");
            const container = $("#catalog_items_container");

            container.LoadingOverlay("show");

            const data = button.data();
            let query = "";

            for (let key in data)
                if (data.hasOwnProperty(key))
                    query += key + "=" + data[key] + "&";

            $.get(Catalog._properties.getViewLink + "?" + query)
                .done(catalogHtml => {
                    container.html(catalogHtml);
                    container.LoadingOverlay("hide");

                    $(".pagination li").removeClass("active");
                    $(".pagination li a").prop("href", "#");

                    $(`.pagination li a[data-page=${page}]`)
                        .removeAttr("href")
                        .parent().addClass("active");
                })
                .fail(() => {
                    container.LoadingOverlay("hide");
                    console.log("clickOnPage fail");
                });
        }
    }
}