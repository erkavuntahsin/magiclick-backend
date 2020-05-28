basic olarak ajax işlemlerini sergilediğim bir js parçası

RTG.LKBK.Account = {
    ready: function () {
        RTG.LKBK.Account.register();
    },
    //create: function () {
    //    RTG.CMP.Schedule.create();
    //},
    register: function (guid) {
        RTG.LKBK.Loader.show();
        $.ajax({
            type: 'GET',
            url: "/Account/CreateRegister",
            data: { guid: guid },
            cache: false,
            success: function (result) {
                $("#register-account").html(result.html);
                RTG.LKBK.Result.show(result);
                $('.tooltips').tooltip();
                RTG.LKBK.Loader.hide();
                $("#username").keyup(function () {
                    var eMailColumn = $('#username');
                    if (eMailColumn.val().length === 0) {
                        $('#login-user-name').prop('required', true).show();
                    }
                    else {
                        $('#login-user-name').val("").prop('required', false).hide();

                    }
                });
            },
            error: function (xhr, status, error) {
                alert("An Error Occurred!");
                RTG.LKBK.Loader.hide();
            }
        });
    },
    create: function () {
        var form = $('#registerAccount');
        console.log($('#registerAccount').valid());
        RTG.LKBK.Loader.show();
        $.ajax({
            type: 'POST',
            url: "/Account/Register",
            dataType: "json",
            data: JSON.stringify(form.serialize()),
            cache: false,
            success: function (result) {
                RTG.LKBK.Loader.hide();
                console.log(result);
                RTG.LKBK.Result.show(result);
                if (result.isSuccess == true) {
                    setTimeout(function () {
                        window.location.replace("/Account/Login");
                    }, 2000);
                }
            },
            error: function (xhr, status, error) {
                RTG.LKBK.Result.ajaxErrorShow(xhr, status, error);
                RTG.LKBK.Loader.hide();
            }
        });
    },
    forgotPassword: function () {
        RTG.LKBK.Loader.show();
        $.ajax({
            type: 'GET',
            url: "/Account/CreateForgotPassword",
            data: null,
            cache: false,
            success: function (result) {
                $("#forgotPassword-account").html(result.html);
                RTG.LKBK.Result.show(result);
                $('.tooltips').tooltip();
                RTG.LKBK.Loader.hide();
            },
            error: function (xhr, status, error) {
                RTG.LKBK.Result.ajaxErrorShow(xhr, status, error);
                RTG.LKBK.Loader.hide();
            }
        });
    },
    resetPassword: function (url , formId) {
        var form = $('#'+ formId);
        RTG.LKBK.Loader.show();
        $.ajax({
            type: 'POST',
            url: url,
            dataType: "json",
            data: JSON.stringify(form.serialize()),
            cache: false,
            success: function (result) {
                console.log(result);
                RTG.LKBK.Loader.hide();
                RTG.LKBK.Result.show(result);
                if (result.isSuccess == true)
                {
                    setTimeout(function () {
                        window.location.replace("/Account/Login");
                    }, 2000);
                }
            },
            error: function (xhr, status, error) {
                RTG.LKBK.Loader.hide();
                alert("An Error Occurred!");
            }
        });
    },
    changePassword: function (id, guid) {
        RTG.LKBK.Loader.show();
        $.ajax({
            type: 'GET',
            url: "/Account/CreateChangePassword",
            data: {
                Id: id,
                Guid:guid
            },
            cache: false,
            success: function (result) {
                $("#changePassword-account").html(result.html);
                RTG.LKBK.Result.show(result);
                $('.tooltips').tooltip();
                RTG.LKBK.Loader.hide();
            },
            error: function (xhr, status, error) {
                RTG.LKBK.Loader.hide();
                RTG.LKBK.Result.ajaxErrorShow(xhr, status, error);
            }
        });
    },
    userNotFound: function () {
        RTG.LKBK.Loader.show();
        $.ajax({
            type: 'GET',
            url: "/Account/CreateUserNotFound",
            data: null,
            cache: false,
            success: function (result) {
                $("#userNotFound-account").html(result.html);
                RTG.LKBK.Result.show(result);
                $('.tooltips').tooltip();
                RTG.LKBK.Loader.hide();
            },
            error: function (xhr, status, error) {
                RTG.LKBK.Result.ajaxErrorShow(xhr, status, error);
                RTG.LKBK.Loader.hide();
            }
        });
    },
    companyNotFound: function () {
        RTG.LKBK.Loader.show();
        $.ajax({
            type: 'GET',
            url: "/Account/CreateCompanyNotFound",
            data: null,
            cache: false,
            success: function (result) {
                $("#companyNotFound-account").html(result.html);
                RTG.LKBK.Result.show(result);
                $('.tooltips').tooltip();
                RTG.LKBK.Loader.hide();
            },
            error: function (xhr, status, error) {
                RTG.LKBK.Result.ajaxErrorShow(xhr, status, error);
                RTG.LKBK.Loader.hide();
            }
        });
    },
    createLogin: function (ReturnUrl) {
        RTG.LKBK.Loader.show();
        $.ajax({
            type: 'GET',
            url: "/Account/CreateLogin",
            data: { ReturnUrl: ReturnUrl},
            cache: false,
            success: function (result) {
                RTG.LKBK.Loader.hide();
                $("#login-account").html(result.html);

                
            },
            error: function (xhr, status, error) {
                alert("An Error Occurred!");
                RTG.LKBK.Loader.hide();
            }
        });
    },
    login: function () {
        var form = $('#loginAccount');
        RTG.LKBK.Loader.show();
        $.ajax({
            type: 'POST',
            url: "/Account/Login",
            dataType: "json",
            data: form.serialize(),
            cache: false,
            success: function (result) {
                RTG.LKBK.Loader.hide();
                RTG.LKBK.Result.show(result);

                if (result.isSuccess == true) {
                    setInterval(function () {
                        result.data == null || result.data == undefined ? window.location.replace("/Home/Index") : window.location.replace(result.data);
                    }, 2000);
                }
            },
            error: function (xhr, status, error) {
                RTG.LKBK.Result.ajaxErrorShow(xhr, status, error);
                RTG.LKBK.Loader.hide();
            }
        });
    },
    logout: function () {
        RTG.LKBK.Loader.show();
                $.ajax({
                    type: 'GET',
                    url: "/Account/Logout",
                    data: null,
                    cache: false,
                    crossDomain: true,
                    success: function (result) {
                        RTG.LKBK.Loader.hide();
                        //window.location.replace("https://dev-478510.oktapreview.com/login/signout");
                    },
                    error: function (xhr, status, error) {
                        RTG.LKBK.Result.ajaxErrorShow(xhr, status, error);
                        RTG.LKBK.Loader.hide();
                    }
                });
    }

}
