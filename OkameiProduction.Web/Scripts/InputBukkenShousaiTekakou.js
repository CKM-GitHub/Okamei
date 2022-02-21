//InputBukkenShousaiTekakou.js
var url_SaveTekakouData = gApplicationPath + '/api/InputBukkenShousaiTekakouApi/SaveTekakouData';

function initialize_Tekakou() {

    //addEvents
    bindKeyPressEvent('#TekakouSubEntry');

    $('#TekakouSubEntry #btnClose').click(function () {
        showConfirmMessage("Q003", function () {
            ModalForm.Close();
        });
    });

    $('#TekakouSubEntry #btnSaveTekakou').click(function () {
        if (checkErrorOnSave('#TekakouSubEntry')) {
            if (checkAll_Tekakou()) {
                showConfirmMessage('Q101', function () {
                    btnSaveTekakouClick();
                    return true;
                });
            }
        }
        return false;
    });

    $('#TekakouSubEntry [id^="TekakouHonsuu"]').blur(function () {
        calcTekakouTime($(this));
        calcTotalTekakouTime();
    });

    //addValidate
    setNumericValidate('#TekakouHonsuu1', 3, 0);
    setNumericValidate('#TekakouHonsuu2', 3, 0);
    setNumericValidate('#TekakouHonsuu3', 3, 0);
    setNumericValidate('#TekakouHonsuu4', 3, 0);
    setNumericValidate('#TekakouHonsuu5', 3, 0);
    setNumericValidate('#TekakouHonsuu6', 3, 0);
    setNumericValidate('#TekakouHonsuu7', 3, 0);
    setNumericValidate('#TekakouHonsuu8', 3, 0);

    //setScreen
    setTekakouUnitTime();

    if (eMode == 'Delete') {
        setDisabledAll('#TekakouSubEntry');
        $('#TekakouSubEntry #btnClose').focus();
    }
    else {
        $('#TekakouSubEntry #TekakouHonsuu1').focus();
    }
}

function setTekakouUnitTime() {
    calcTekakouTime($('#TekakouUnitTime1').text('0.5'));
    calcTekakouTime($('#TekakouUnitTime2').text('1.0'));
    calcTekakouTime($('#TekakouUnitTime3').text('2.0'));
    calcTekakouTime($('#TekakouUnitTime4').text('4.0'));
    calcTekakouTime($('#TekakouUnitTime5').text('20.0'));
    calcTekakouTime($('#TekakouUnitTime6').text('30.0'));
    calcTekakouTime($('#TekakouUnitTime7').text('10.0'));
    calcTekakouTime($('#TekakouUnitTime8').text('20.0'));

    calcTotalTekakouTime();
}

function calcTekakouTime(ctrl) {
    var id = ctrl.attr('id');

    if (id.slice(0, 13) == 'TekakouHonsuu') {
        var row = id.slice(-1);

        var honsuu = ctrl.val();
        var unitTime = $('#TekakouUnitTime' + row).text();
        if (!honsuu || !unitTime || isNaN(honsuu) || isNaN(unitTime)) {
            $('#TekakouTime' + row).text('');
        }
        else {
            var time = parseFloat(honsuu) * parseFloat(unitTime);
            $('#TekakouTime' + row).text(time.toFixed(1));
        }
    }

    return true;
}

function calcTotalTekakouTime() {
    var sum = 0;
    $('#TekakouSubEntry [id^="TekakouTime"]').each(function () {
        var time = $(this).text();
        if (time && !isNaN(time)) {
            sum = sum + parseFloat(time);
        }
    });
    $('#TotalTekakouTime').text(sum.toFixed(1));
}

function checkAll_Tekakou() {
    return true;
}

function btnSaveTekakouClick() {
    var model = {
        BukkenNO: $('#TekakouBukkenNO').val(),
        HiddenUpdateDateTime: $('#HiddenTekakouUpdateDateTime').val(),
        UserID: $('#user-id').text(),
    }

    var result = calltoApiController(url_SaveTekakouData, model);
    if (!result) {
        return false;
    }
    if (result.MessageID) {
        showMessage(result);
        return false;
    }

    showMessage('I101', function () {
        ModalForm.Close();
    });
}
