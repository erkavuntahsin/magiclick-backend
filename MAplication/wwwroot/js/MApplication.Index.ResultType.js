
MApplication.Index.ResultEnum = {
    INFORMATION: 1,
    SUCCESS: 2,
    WARNING: 3,
    ERROR: 4
};

MApplication.Index.Result = {
    show: function (result) {
        var languageMessage = result.languageMessage;
        var message = result.message;
        var resultType = result.resultType;
        var isSuccess = result.isSuccess;
        toastr.options = {
            "closeButton": true,
        }
        if (resultType == MApplication.Index.ResultEnum.INFORMATION) {
            toastr["info"](languageMessage, message);
        }
        if (resultType == MApplication.Index.ResultEnum.SUCCESS) {
            toastr["success"](languageMessage, "Success!");
        }
        if (resultType == MApplication.Index.ResultEnum.WARNING) {
            toastr["warning"](languageMessage, "Warning!");
        }
        if (resultType == MApplication.Index.ResultEnum.ERROR) {
            toastr["error"](languageMessage, "Error!");
        }
        if (isSuccess) {
            toastr["success"](languageMessage, "Success!");
        }
    }
};