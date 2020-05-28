///<reference path="MApplication.Index.js" />

MApplication.Index.Product = {
    list: function () {
        MApplication.Index.Loader.show();
        $.ajax({
            type: 'GET',
            url: "/Product/List",
            data: null,
            cache: false,
            success: function (result) {
                $("#product-list").html(result.html);
                MApplication.Index.Result.show(result);
                MApplication.Index.Loader.hide();
            },
            error: function (xhr, status, error) {
                alert("An error occured!" + error);
                MApplication.Index.Loader.hide();
            }
        });
    },

    
    edit: function () {
        MApplication.Index.Loader.show();
        var form = $("#form-prod");
        $.ajax({
            url: "/Product/Edit",
            type: 'POST',
            dataType: "json",
            data: form.serialize(),
            cache: false,
            success: function (result) {
                if (result.isSuccess) {
                    MApplication.Index.Result.show(result);
                    setTimeout(function () { window.location.replace("/Product/Index/"); }, 3000);
                }
                MApplication.Index.Loader.hide();
            },
            error: function (xhr, status, error) {
                alert("Bir problem oluştu!");
                MApplication.Index.Loader.hide();
            }
        });
    },
    delete: function (id) {
        MApplication.Index.Loader.show();
        $.ajax({
            type: 'POST',
            url: "/Product/Delete",
            data: { id: id },
            cache: false,
            success: function (result) {
                MApplication.Index.Loader.hide();
                if (result.isSuccess) {
                    MApplication.Index.Result.show(result);
                    setTimeout(function () { window.location.replace("/Product/Index/"); }, 3000);
                }
                else {
                    MApplication.Index.Loader.hide();
                }
            },
            error: function (xhr, status, error) {
                alert("An Error Occurred!");
                MApplication.Index.Loader.hide();
            }
        });
    },
};