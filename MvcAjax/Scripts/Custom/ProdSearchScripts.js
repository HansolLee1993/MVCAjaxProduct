/// <reference path="jquery-1.10.2.js" />
/// <reference path="jquery-ui-1.11.3.js" />
/// <reference path="jQuery.tmpl.js" />

// short version
$(function () {


    //$("#productSearch").submit(function (event) {
    //    event.preventDefault();
    //    var form = $(this);
    //    $.getJSON(form.attr("action"), form.serialize(), function (data) {
    //        $("#product-list").empty();
    //        $("#productTemplate").tmpl(data).appendTo("#product-list");
    //    });
    //});


    $("#productSearch").submit(function (event) {
        event.preventDefault();

        var form = $(this);
        $.ajax({
            url: form.attr("action"),
            type: "GET",
            data: form.serialize(),
            beforeSend: function () {
                $("#product-list").html("");
                $("#ajax-loader").show();
            },
            complete: function () {
                $("#ajax-loader").hide();
            },
            error: searchFailed,
            success: function (data) {
                $("#productTemplate").tmpl(data).appendTo("#product-list");
            }
        });
    });

    $("input[data-autocomplete-source]").each(function () {
        var target = $(this);
        target.autocomplete({ source: target.attr("data-autocomplete-source") });
    });
});

function searchFailed() {
    $("#searchresults").html("<p style='color:red'>Search failed!</p>");
}



