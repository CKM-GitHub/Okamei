//InputBukkenShousaiTekakou.js
var url_SaveTekakouData = gApplicationPath + '/api/InputBukkenShousaiTekakouApi/SaveTekakouData';

function finalize_Tekakou() {
    undindKeyPressEvent('#TekakouSubEntry');
    $('#TekakouTime').focus();
}

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
            showConfirmMessage('Q101', function () {
                btnSaveTekakouClick();
                return true;
            });
        }
        return false;
    });

    $('#TekakouSubEntry [id^="TekakouHonsuu"]').blur(function () {
        calcTekakouTime($(this));
        calcTotalTekakouTime();
    });

    //addValidate
    setNumericValidate('#TekakouSubEntry #TekakouHonsuu1', 3, 0);
    setNumericValidate('#TekakouSubEntry #TekakouHonsuu2', 3, 0);
    setNumericValidate('#TekakouSubEntry #TekakouHonsuu3', 3, 0);
    setNumericValidate('#TekakouSubEntry #TekakouHonsuu4', 3, 0);
    setNumericValidate('#TekakouSubEntry #TekakouHonsuu5', 3, 0);
    setNumericValidate('#TekakouSubEntry #TekakouHonsuu6', 3, 0);
    setNumericValidate('#TekakouSubEntry #TekakouHonsuu7', 3, 0);
    setNumericValidate('#TekakouSubEntry #TekakouHonsuu8', 3, 0);

    //setScreen
    setTekakouUnitTime();

    if (eMode == 'Delete') {
        setDisabledAll('#TekakouSubEntry', '.js-modal-close, #TekakouSubEntry #btnClose');
        $('#TekakouSubEntry #btnClose').focus();
    }
    else {
        $('#TekakouSubEntry #TekakouHonsuu1').focus();
    }
}

function setTekakouUnitTime() {
    calcTekakouTime($('#TekakouSubEntry #TekakouUnitTime1').text('0.5'));
    calcTekakouTime($('#TekakouSubEntry #TekakouUnitTime2').text('1.0'));
    calcTekakouTime($('#TekakouSubEntry #TekakouUnitTime3').text('2.0'));
    calcTekakouTime($('#TekakouSubEntry #TekakouUnitTime4').text('4.0'));
    calcTekakouTime($('#TekakouSubEntry #TekakouUnitTime5').text('20.0'));
    calcTekakouTime($('#TekakouSubEntry #TekakouUnitTime6').text('30.0'));
    calcTekakouTime($('#TekakouSubEntry #TekakouUnitTime7').text('10.0'));
    calcTekakouTime($('#TekakouSubEntry #TekakouUnitTime8').text('20.0'));

    calcTotalTekakouTime();
}

function calcTekakouTime(ctrl) {
    var id = ctrl.attr('id');
    var row = id.slice(-1);

    var honsuu;
    var unitTime;

    if (~id.indexOf('TekakouHonsuu')) {
        honsuu = ctrl.val();
        unitTime = $('#TekakouSubEntry #TekakouUnitTime' + row).text();
    }
    else if (~id.indexOf('TekakouUnitTime')) {
        honsuu = $('#TekakouSubEntry #TekakouHonsuu' + row).val();
        unitTime = ctrl.text();
    }

    if (!honsuu || !unitTime || isNaN(honsuu) || isNaN(unitTime)) {
        $('#TekakouSubEntry #TekakouTime' + row).text('');
    }
    else {
        var time = parseFloat(honsuu) * parseFloat(unitTime);
        $('#TekakouSubEntry #TekakouTime' + row).text(time.toFixed(1));
    }
}

function calcTotalTekakouTime() {
    var sum = 0;
    $('#TekakouSubEntry [id^="TekakouTime"]').each(function () {
        var time = $(this).text();
        if (time && !isNaN(time)) {
            sum = sum + parseFloat(time);
        }
    });
    $('#TekakouSubEntry #TotalTekakouTime').text(sum.toFixed(1));
}

function btnSaveTekakouClick() {
    var model = {
        BukkenNO: $('#TekakouSubEntry #TekakouBukkenNO').val(),
        TeKakou1Honsuu: $('#TekakouSubEntry #TekakouHonsuu1').val(),
        TeKakou2Honsuu: $('#TekakouSubEntry #TekakouHonsuu2').val(),
        TeKakou3Honsuu: $('#TekakouSubEntry #TekakouHonsuu3').val(),
        TeKakou4Honsuu: $('#TekakouSubEntry #TekakouHonsuu4').val(),
        TeKakou5Honsuu: $('#TekakouSubEntry #TekakouHonsuu5').val(),
        TeKakou6Honsuu: $('#TekakouSubEntry #TekakouHonsuu6').val(),
        TeKakou7Honsuu: $('#TekakouSubEntry #TekakouHonsuu7').val(),
        TeKakou8Honsuu: $('#TekakouSubEntry #TekakouHonsuu8').val(),
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
        $('#TekakouTime').val($('#TekakouSubEntry #TotalTekakouTime').text());
        ModalForm.Close();
    });
}
