///<reference path="MApplication.Index.js" />

MApplication.Index.Category = {
    list: function (id) {
        MApplication.Index.Loader.show();
        $.ajax({
            type: 'GET',
            url: "/Category/List",
            data: {id:id},
            cache: false,
            success: function (result) {
                $("#category-list").html(result.html);
                MApplication.Index.Result.show(result);
                MApplication.Index.Loader.hide();
            },
            error: function (xhr, status, error) {
                alert("An error occured!" + error);
                MApplication.Index.Loader.hide();
            }
        });
    },
    
    create: function () {
        MApplication.Index.Loader.show();
        var form = $("#form-category");
        $.ajax({
            url: "/Category/Add",
            type: 'POST',
            dataType: "json",
            data: form.serialize(),
            cache: false,
            success: function (result) {
                if (result.isSuccess) {
                    MApplication.Index.Result.show(result);
                    setTimeout(function () { window.location.replace("/Category/Index/"); }, 2000);
                }
                MApplication.Index.Loader.hide();
            },
            error: function (xhr, status, error) {
                alert("An error occured!");
                MApplication.Index.Loader.hide();
            }
        });
    },
    edit: function () {
        MApplication.Index.Loader.show();
        var form = $("#form-category");
        $.ajax({
            url: "/Category/Edit",
            type: 'POST',
            dataType: "json",
            data: form.serialize(),
            cache: false,
            success: function (result) {
                if (result.isSuccess) {
                    MApplication.Index.Result.show(result);
                    setTimeout(function () { window.location.replace("/Category/Index/"); }, 3000);
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
            url: "/Category/Delete",
            data: { id:id  },
            cache: false,
            success: function (result) {
                MApplication.Index.Loader.hide();
                if (result.isSuccess) {
                    MApplication.Index.Result.show(result);
                    setTimeout(function () { window.location.replace("/Category/Index/"); }, 3000);
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