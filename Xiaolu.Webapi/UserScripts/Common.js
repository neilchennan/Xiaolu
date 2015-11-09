function LockMainPage() {
}

function UnLockMainPage() {
}

function AjaxPostAction(url, params, onSuccess, onFail, triggerButton) {
    LockMainPage();
    $.ajax({
        type: "POST",
        async: false,
        dataType: "json",
        url: url,
        data: params,
        success: function (data) {
            if (data.IsSuccess) {
                $.messager.popup(data.Message);
                if (onSuccess) {
                    onSuccess.call();
                }
            }
            else {
                $.messager.alert(data.Message);
                if (onFail) {
                    onFail.call(this, triggerButton);
                }
            }
            UnLockMainPage();
        },
        error: function (xmlHttpRequest, texStatus, errorThrown) {
            alert(errorThrown);
            UnLockMainPage();
        }
    });
}

function AjaxPutAction(url, params, onSuccess, onFail, triggerButton) {
    AjaxAction("PUT", url, params, onSuccess, onFail, triggerButton);
}

function AjaxAction(type, url, params, onSuccess, onFail, triggerButton) {
    LockMainPage();
    $.ajax({
        type: type,
        async: false,
        dataType: "json",
        url: url,
        data: params,
        success: function (data) {
            if (data.IsSuccess) {
                $.messager.popup(data.Message);
                if (onSuccess) {
                    onSuccess.call();
                }
            }
            else {
                $.messager.alert(data.Message);
                if (onFail) {
                    onFail.call(this, triggerButton);
                }
            }
            UnLockMainPage();
        },
        error: function (xmlHttpRequest, texStatus, errorThrown) {
            alert(errorThrown);
            UnLockMainPage();
        }
    });
}

String.prototype.format = function (args) {
    if (arguments.length > 0) {
        var result = this;
        if (arguments.length == 1 && typeof (args) == "object") {
            for (var key in args) {
                var reg = new RegExp("({" + key + "})", "g");
                result = result.replace(reg, args[key]);
            }
        }
        else {
            for (var i = 0; i < arguments.length; i++) {
                if (arguments[i] == undefined) {
                    return "";
                }
                else {
                    var reg = new RegExp("({[" + i + "]})", "g");
                    result = result.replace(reg, arguments[i]);
                }
            }
        }
        return result;
    }
    else {
        return this;
    }
}