var gApplicationPath = ""; //value is set in "_Layout.cshtml"
var gCommonApiUrl = "/api/CommonApi/";
var gCustomValidate = function (ctrl) { return true; }

function setDropDownList(selector, url, key) {
    var ddl = $(selector);
    ddl.children().remove();
    ddl.append('<option></option>');

    if (key != "") {
        var ret = calltoApiController(url, key);
        if (!ret) {
            return;
        }
        if (ret.MessageID) {
            showMessage(ret);
            return;
        }
        ret.forEach(function (item) {
            ddl.append('<option value=' + item.Value + '>' + item.DisplayText + '</option>');
        });
    }
}

function setDisabledAll(selector) {
    $(selector + ' :input:not(:hidden)').prop('disabled', true);
    $('.main-content-footer :button').prop('disabled', false);
}

function querySerialize(data) {
    var key, value, type, i, max;
    var encode = window.encodeURIComponent;
    var query = '';

    for (key in data) {
        value = data[key];
        type = typeof (value) === 'object' && value instanceof Array ? 'array' : typeof (value);
        switch (type) {
            case 'undefined':
                // only key
                query += key;
                break;
            case 'array':
                // array
                for (i = 0, max = value.length; i < max; i++) {
                    query += key + '[]';
                    query += '=';
                    query += encode(value[i]);
                    query += '&';
                }
                query = query.substr(0, query.length - 1);
                break;
            case 'object':
                // hash
                for (i in value) {
                    query += key + '[' + i + ']';
                    query += '=';
                    query += encode(value[i]);
                    query += '&';
                }
                query = query.substr(0, query.length - 1);
                break;
            default:
                query += key;
                query += '=';
                query += encode(value);
                break;
        }
        query += '&';
    }
    query = query.substr(0, query.length - 1);
    return '?' + encodeURI(query);
};

function bindDataTables(table, option) {
    var t = $(table);

    var obj = {
        "language": {
            "url": "http://cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Japanese.json"
        },
        displayLength: 20,
        paging: true,
        scrollX: true,
        scrollY: false,
        searching: false,
        ordering: false,
        lengthChange: false,
        autowidth: false,
        destroy: true,
        dom: "<'row'<'col-sm-12'l>>" +
            "<'row'<'col-sm-12'f>>" +
            "<'row'<'col-sm-12'i>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-12'p>>",
    };

    if (option) {
        for (key in option) {
            obj[key] = option[key];
        }
    }

    return t.DataTable(obj);
}

function calltoApiController(url, model) { 
    var result;
    $.ajax({
        url: url,
        method: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(model),
        async: false,
        headers:
        {
            Authorization: getApiAuthorization()
        },
        success: function (data) {
             
            result = JSON.parse(data);
        },
        error: function (data, ajaxOption, terror) {
            alert(data.status + ":" + data.statusText + ":" + terror);
        }
    });
    return result;
}
//ptk test and work 2022/02/09
function calltoApiController_FileDownLoadHandle(url, model) {  
    var result;
    $.ajax({
        url: url,
        method: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(model),
        async: false,
        headers:
        {
            Authorization: getApiAuthorization()
        },
        xhr: function () {
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 2) {
                    if (xhr.status == 200) {
                        xhr.responseType = "blob";
                    } else {
                        xhr.responseType = "text";
                    }
                }
            };
            return xhr;
        },
        success: function (data) { 
            //Convert the Byte Data to BLOB object.
            var blob = new Blob([data], { type: "application/octetstream" });

            //Check the Browser type and download the File.
            var isIE = false || !!document.documentMode;
            if (isIE) {
                window.navigator.msSaveBlob(blob, fileName);
            } else {
                var url = window.URL || window.webkitURL;
                link = url.createObjectURL(blob);
                var a = $("<a />");
                a.attr("download", model.fileName);
                a.attr("href", link);
                $("body").append(a);
                a[0].click();
                $("body").remove(a);
            }
        },
        error: function (data, ajaxOption, terror) {
            alert(data.status + ":" + data.statusText + ":" + terror); 
        }
    });
    return result;
}

function callSendFileToServer(url, fileData, progresscall, callback, callbackerror) {
    var obj = {
        url: url,
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: fileData,
        timeout: 600000,
        headers: {
            Authorization: getApiAuthorization(),
        },
        success: function (data) {
            if (callback) callback(JSON.parse(data));            
        },
        error: function (err) {
            alert(err.status + ":" + err.statusText);
            if (callbackerror) callbackerror();
        }
    };
    if (progresscall) {
        obj.xhr = function () {
            var xhrobj = $.ajaxSettings.xhr();
            if (xhrobj.upload) {
                xhrobj.upload.addEventListener('progress', function (event) {
                    var percent = 0;
                    var position = event.loaded || event.position;
                    var total = event.total;
                    if (event.lengthComputable) {
                        percent = parseInt(position / total * 10000) / 100;
                    }
                    progresscall(percent);
                }, false);
            }
            return xhrobj;
        }
    }
    var jqXHR = $.ajax(obj);
}

function getApiAuthorization() {
    return 'Basic ' + btoa('ogUzkq=EopiYA,U33yzf' + ':' + 'e>gW0BXP85@7-#*~k1@a');
}

function showConfirmMessage(msgid, callback) {
    var model = {
        MessageID: msgid,
    };
    var msgdata = calltoApiController(gApplicationPath + gCommonApiUrl + "GetMessage", model);
    if (!msgdata) {
        return false;
    }
    Swal.fire({
        title: msgdata.MessageID,
        text: msgdata.MessageText1,
        icon: msgdata.MessageIcon,
        showCancelButton: true,
        confirmButtonText: 'はい',
        cancelButtonText: 'いいえ'
    }).then((result) => {
        if (result.value) {
            callback();
        }
    })
}

function showMessage(msg, callback) {
    if (!msg.MessageID)
    {
        var model = {
            MessageID: msg,
        };
        var ret = calltoApiController(gApplicationPath + gCommonApiUrl + "GetMessage", model);
        if (!ret) {
            return false;
        }
        msg = ret;
    }

    Swal.fire({
        icon: msg.MessageIcon,
        title: msg.MessageID,
        text: msg.MessageText1,
    }).then(callback);
}

// require ----->
function setRequired(selector) {
    $(selector).attr('validate-required', 'true');
}
function removeRequired(selector) {
    $(selector).removeAttr('validate-required');
}

// date type  ----->
function setDateTypeValidate(selector) {
    $(selector).attr('validate-datetype', 'true');
}
function removeDateTypeValidate(selector) {
    $(selector).removeAttr('validate-datetype');
}

// compare date  ----->
function setCompareDateValidate(selector, comparisonTargetName) {
    $(selector).attr({
        'validate-comaredate': 'true',
        'comparison-target': comparisonTargetName
    });
}
function removeCompareDateValidate(selector) {
    $(selector).removeAttr('validate-comaredate').removeAttr('comparison-target');
}

// doublebyte  ----->
function setDoubleByteValidate(selector, isDoublebyteonly) {
    $(selector).attr('validate-doublebyte', 'true');
    if (isDoublebyteonly) {
        $(selector).attr('isDoublebyteonly', isDoublebyteonly);
    }
}
function removeDoubleByteValidate(selector) {
    $(selector).removeAttr('validate-doublebyte').removeAttr('isDoublebyteonly');
}

// is halfwidth ----->
function setIsHalfWidthValidate(selector) {
    $(selector).attr('validate-halfwidth', 'true');
}
function removeIsHalfWidthValidate(selector) {
    $(selector).removeAttr('validate-halfwidth');
}
//<--------------------

// numeric ----->
function setNumericValidate(selector, integerdigits, decimaldigits) {
    $(selector).attr('validate-numeric', 'true')
        .attr('integerdigits', integerdigits)
        .attr('decimaldigits', decimaldigits);
}
function removeNumericValidate(selector) {
    $(selector).removeAttr('validate-numeric')
        .removeAttr('integerdigits')
        .removeAttr('decimaldigits');
}
//<--------------------

function checkCommon(ctrl) {

    ctrl = $(ctrl);

    var required = ctrl.attr("validate-required");
    if (required && !ctrl.val()) {
        var result = calltoApiController(gApplicationPath + gCommonApiUrl + "GetMessage", { MessageID: "E102" });
        if (!result) {
            return false;
        }
        return result;
    }

    if (ctrl.val()) {
        var model = {
            IsDateType: ctrl.attr("validate-datetype"),
            IsCompareDate: ctrl.attr("validate-comaredate"),
            IsHalfWidth: ctrl.attr("validate-halfwidth"),
            IsDoubleByte: ctrl.attr("validate-doublebyte"),
            IsNumeric: ctrl.attr("validate-numeric"),
        };

        if (model.IsDateType || model.IsCompareDate || model.IsHalfWidth || model.IsDoubleByte || model.IsNumeric) {

            model.InputValue1 = ctrl.val();

            if (model.IsCompareDate) {
                var comparisonTarget = ctrl.attr('comparison-target');
                model.ComparisonValue = $(comparisonTarget).val();
            }

            if (model.IsDoubleByte) {
                model.MaxLength = ctrl.attr('maxlength');
                model.IsDoubleByteOnly = ctrl.attr('isDoublebyteonly');
            }

            if (model.IsNumeric) {
                model.Integerdigits = ctrl.attr('integerdigits');
                model.Decimaldigits = ctrl.attr('decimaldigits');
            }

            var result = calltoApiController(gApplicationPath + gCommonApiUrl + 'CheckValid', model);
            if (!result) {
                return false;
            }
            if (model.IsDateType || model.IsNumeric) {
                if (result.ReturnValue && result.ReturnValue != "") {
                    ctrl.val(result.ReturnValue);
                }
            }
            if (result.MessageID) {
                return result;
            }
        }
    }

    //custom check
    if (!gCustomValidate(ctrl)) {
        return false;
    }
    return true;
}

function checkErrorOnSave(selector) {

    if (typeof selector === 'undefined') {
        selector = '#globalSubContainer';
    }

    var success = true;
    $(selector + ' :input:not(button):not(:hidden):not(:disabled):not([readonly])').each(function () {
        var result = checkCommon(this);
        if (!result) {
            success = false;
            return false;
        }
        if (result && result.MessageID) {
            success = false;
            $(this).focus();
            showMessage(result);
            return false;
        }
    });

    return success;
}

function bindKeyPressEvent(areaid) {

    if (typeof areaid === 'undefined') {
        areaid = '#global-sub-container';
    }
    else if (areaid.slice(0, 1) != "#") {
        areaid = '#' + areaid;
    }

    var selector = areaid + ' :input:not(:hidden)';

    $(selector).keypress(function (e) {
        var c = e.which ? e.which : e.keyCode;
        if (c == 13 || c == 9) {

            if ($(e.target).attr('type') == 'button') return;
            e.preventDefault();

            var result = checkCommon($(e.target));
            if (!result) {
                return;
            }
            if (result && result.MessageID) {
                showMessage(result);
                return;
            }

            //move next
            var index = $(selector).index(this); // index:0～
            var nLength = $(selector).length;
            var cNext = "";
            var oNext;

            for (i = index; i < nLength; i++) {
                cNext = e.shiftKey ? ":lt(" + index + "):last" : ":gt(" + index + "):first";
                oNext = $(selector + cNext);

                //readonly -> skip
                if (oNext.prop("readonly")) {
                    if (e.shiftKey) index--; // previous
                    else index++;            // next
                }
                //disabled -> skip
                else if (oNext.prop("disabled")) {
                    if (e.shiftKey) index--; // previous
                    else index++;            // next
                }
                //radio button off -> skip
                else if (oNext.attr("type") == 'radio' && !oNext.prop("checked")) {
                    if (e.shiftKey) index--; // previous
                    else index++;            // next
                }
                //return button -> skip
                else if (oNext.attr("nofocus") != undefined) {
                    if (e.shiftKey) index--; // previous
                    else index++;            // next
                }
                else break;
            }
            if (index == nLength - 1) {
                if (!e.shiftKey) {
                    //last -> top
                    cNext = ":eq(0)";
                }
            }
            if (index == 0) {
                if (e.shiftKey) {
                    //top -> last
                    cNext = ":eq(" + (nLength - 1) + ")";
                }
            }
            $(selector + cNext).focus();
        }
    }); 
}

$(document).ready(function () {

    bindKeyPressEvent("#main");

});

$(document).on("drop dragover", function (e) {
    e.stopPropagation();
    e.preventDefault();
});
