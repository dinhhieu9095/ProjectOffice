var rootUrl = '';
var keySize = 128;
var iterations = 1000;
var pw = 'KTKSNB2019LACVIET';
var CommonUtils = {
    RootURL: function (val) {
        return baseUri + val;
    },
    GetQueryStringByParameter: function (sParam) {
        var sPageURL = window.location.search.substring(1);
        var sURLVariables = sPageURL.split('&');
        for (var i = 0; i < sURLVariables.length; i++) {
            var sParameterName = sURLVariables[i].split('=');
            if (sParameterName[0] == sParam) {
                return sParameterName[1];
            }
        }
    },
    Permission: function (val) {

    },
   
    getUrlParameter: function (url, parameter_name) {
        var url = new URL(url);
        return url.searchParams.get(parameter_name);
    },
    RootFolder: function (val) {
        return rootUrl + val;
    },
    showConfirm: function(messages, buttonCancel, buttonConfirm, callback, title) {
    bootbox.confirm({
            title: title == null ? ' ' : title,
            message: messages,
            buttons: {
                confirm: {
                    label: buttonConfirm,
                    className: "btn-circle btn-danger mr5",
                    callback: function () {
                        return true;
                    }
                },
                cancel: {
                    label: buttonCancel,
                    className: "btn-circle btn-default fr",
                    callback: function () {
                        return false;
                    }
                }
            },
            callback: callback
        });
$(".bootbox").find(".modal-dialog").addClass("maxh-200 maxw-600");
},
    AddDays: function (date, amount) {
        var tzOff = date.getTimezoneOffset() * 60 * 1000,
        t = date.getTime(),
        d = new Date(),
        tzOff2;

t += (1000 * 60 * 60 * 24) * amount;
d.setTime(t);

tzOff2 = d.getTimezoneOffset() * 60 * 1000;
if (tzOff != tzOff2) {
    var diff = tzOff2 - tzOff;
    t += diff;
    d.setTime(t);
}

return d;
    },
    decrypt: function (transitmessage) {
        try {
            var iv = CryptoJS.enc.Hex.parse('e84ad660c4721ae0e84ad660c4721ae0');
            var Pass = CryptoJS.enc.Utf8.parse(pw);
            var Salt = CryptoJS.enc.Utf8.parse(pw);
            var ps = Pass.toString(CryptoJS.enc.Utf8);
            var key = CryptoJS.PBKDF2(Pass.toString(CryptoJS.enc.Utf8), Salt, {
                keySize: 128 / 32,
                iterations: iterations
            });
            var cipherParams = CryptoJS.lib.CipherParams.create({
                ciphertext: CryptoJS.enc.Base64.parse(transitmessage)
            });

            var decrypted = CryptoJS.AES.decrypt(cipherParams, key, { mode: CryptoJS.mode.CBC, iv: iv, padding: CryptoJS.pad.Pkcs7 });
            var str = decrypted.toString(CryptoJS.enc.Utf8);
            var data = JSON.parse(str);
            return data;
        } catch (e) {
            return null;
        }
    },
    encrypt: function (transitmessage) {
        try {
            var iv = CryptoJS.enc.Hex.parse('e84ad660c4721ae0e84ad660c4721ae0');
            var Pass = CryptoJS.enc.Utf8.parse(pw);
            var Salt = CryptoJS.enc.Utf8.parse(pw);
            var ps = Pass.toString(CryptoJS.enc.Utf8);
            var key = CryptoJS.PBKDF2(Pass.toString(CryptoJS.enc.Utf8), Salt, {
                keySize: 128 / 32,
                iterations: iterations
            });
            var encrypted = CryptoJS.AES.encrypt(transitmessage, key, {
                iv: iv,
                padding: CryptoJS.pad.Pkcs7,
                mode: CryptoJS.mode.CBC

            });

            return encrypted.toString();
        } catch (e) {
            return null;
        }
    }
};
var guidEmpty = "00000000-0000-0000-0000-000000000000";