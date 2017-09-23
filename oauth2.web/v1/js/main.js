var selectedBranch = "";
var services = {
    callback: null,
    get: function (method, info, onSuccess, onError) {
        $.ajax({
            url: "/services/data.ashx?method=" + method,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: 'POST',
            data: info,
            responseType: "json",
            success: function (result) {debugger;
                if (typeof (onSuccess) == "function") {
                    result = result.replace(/\n/g, ' ').replace(/\r/g, ' ');
                    data = $.parseJSON('[' + result + ']');
                    onSuccess(data);
                } else
                    services.OnComplete(result);
            },
            error: function (result) {
                debugger;
                if (typeof (onError) == "function") {
                    onError(result)
                } else
                    services.OnFail(result);
            }
        });
    },
    save: function (method, info) {
        $.ajax({
            url: "/services/data.ashx?method=" + method,
            data: info,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: services.OnComplete,
            error: services.OnFail
        });
        return false;
    },
    OnComplete: function (result) {
        consolelog($.parseJSON('{' + result + '}'));
        showmessage('Success');
    },
    OnFail: function (result) {
        consolelog($.parseJSON('{' + result + '}'));
        showmessage('Request Failed');
    }
}

function loadurl(url) {
    $.get(url, function (data) {
        $('#content-holder').html(data);
    });
}
function kFormatter(num) {
    return num > 999 ? (num / 1000).toFixed(1) + 'k' : num
}
function mFormatter(num) {
    return num > 99999 ? (num / 1000000).toFixed(2)  : num
}
function mFormatterNoFixed(num) {
    return num > 99999 ? (num / 1000000).toFixed(0) : num
}
Number.prototype.formatMoney = function (c, d, t) {
    var n = this,
        c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d == undefined ? "." : d,
        t = t == undefined ? "," : t,
        s = n < 0 ? "-" : "",
        i = String(parseInt(n = Math.abs(Number(n) || 0).toFixed(c))),
        j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};
Date.prototype.addMonths = function (value) {
    var n = this.getDate();
    this.setDate(1);
    this.setMonth(this.getMonth() + value);
    this.setDate(Math.min(n, this.getDaysInMonth()));
    return this;
};
Date.isLeapYear = function (year) {
    return (((year % 4 === 0) && (year % 100 !== 0)) || (year % 400 === 0));
};

Date.getDaysInMonth = function (year, month) {
    return [31, (Date.isLeapYear(year) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][month];
};

Date.prototype.isLeapYear = function () {
    return Date.isLeapYear(this.getFullYear());
};

Date.prototype.getDaysInMonth = function () {
    return Date.getDaysInMonth(this.getFullYear(), this.getMonth());
};
