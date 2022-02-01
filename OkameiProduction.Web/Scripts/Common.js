var gAbsolutePath = ""; //value is set in "_Layout.cshtml"
var gCommonApiUrl = "/api/CommonApi/";
var gCustomValidate = function (ctrl) { return true; }


var querySerialize = function (data) {
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

function bindDataTables(table, dispLength) {
    var t = $(table);

    t.DataTable({
        "language": {
            "url": "http://cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Japanese.json"
        },
        //lengthMenu: [20, 25],
        displayLength: dispLength ? dispLength : 20,
        paging: true,
        scrollX: true,
        scrollY: false,
        searching: false,
        ordering: false,
        lengthChange: false,
        autowidth: false
    });
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
            Authorization: 'Basic ' + btoa('ogUzkq=EopiYA,U33yzf' + ':' + 'e>gW0BXP85@7-#*~k1@a')
        },
        success: function (data) {
            result = JSON.parse(data);
            result.status = true;
        },
        error: function (data) {
            alert(data.status + ":" + data.statusText);
            result.status = false;
        }
    });
    return result;
}

function showConfirmMessage(msgid, callback) {

    var model = {
        MessageID: msgid,
    };
    var msgdata = calltoApiController(gAbsolutePath + gCommonApiUrl + "GetMessage", model);
    if (!msgdata || !msgdata.status) {
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
    Swal.fire({
        icon: msg.MessageIcon,
        title: msg.MessageID,
        text: msg.MessageText1,
    }).then(callback);
}

// require ----->
function setRequired(selector) {
    $(selector).attr('validate-required', "true");
}
function removeRequired(selector) {
    $(selector).removeAttr('validate-required');
}

// date type  ----->
function setDateTypeValidate(selector) {
    $(selector).attr('validate-datetype', "true");
}
function removeDateTypeValidate(selector) {
    $(selector).removeAttr('validate-datetype');
}

// compare date  ----->
function setCompareDateValidate(selector, comparisonTargetName) {
    $(selector).attr({
        'validate-comaredate': "true",
        'comparison-target': comparisonTargetName
    });
}
function removeCompareDateValidate(selector) {
    $(selector).removeAttr('validate-comaredate').removeAttr('comparison-target');
}

// doublebyte length  ----->
function setDoubleByteValidate(selector) {
    $(selector).attr('validate-doublebyte', "true");
}
function removeDoubleByteValidate(selector) {
    $(selector).removeAttr('validate-doublebyte');
}

// is halfwidth ----->
function setIsHalfWidthValidate(selector) {
    $(selector).attr('validate-halfwidth', "true");
}
function removeIsHalfWidthValidate(selector) {
    $(selector).removeAttr('validate-halfwidth');
}
//<--------------------

function checkCommon(ctrl) {

    ctrl = $(ctrl);

    var required = ctrl.attr("validate-required");
    if (required && !ctrl.val()) {
        var msgdata = calltoApiController(gAbsolutePath + gCommonApiUrl + "GetMessage", { MessageID: "E102" });
        if (!msgdata || !msgdata.status) {
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
        };

        if (model.IsDateType || model.IsCompareDate || model.IsHalfWidth || model.IsDoubleByte) {

            model.InputValue1 = ctrl.val();

            if (model.IsCompareDate) {
                var comparisonTarget = ctrl.attr('comparison-target');
                model.ComparisonValue = $(comparisonTarget).val();
            }

            if (model.IsDoubleByte) {
                model.MaxLength = ctrl.attr('maxlength');
            }

            var result = calltoApiController(gAbsolutePath + gCommonApiUrl + 'CheckValid', model);
            if (!result || !result.status) {
                return false;
            }
            if (model.IsDateType) {
                if (result.ReturnValue && result.ReturnValue != "") {
                    ctrl.val(result.ReturnValue);
                }
            }
            if (result && result.MessageID) {
                return result;
            }
        }
    }

    //カスタムチェック
    if (!gCustomValidate(ctrl)) {
        return false;
    }
    return true;
}

function checkErrorOnSave(selector) {

    if (typeof selector === 'undefined') {
        selector = '#globalSubContainer';
    }
    if (selector.slice(0, 1) != "#") {
        selector = '#' + selector;
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