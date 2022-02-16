//InputBukkenShousaiHiuchi.js
var url_HiuchiSubEntry = gApplicationPath + '/InputBukkenShousai/HiuchiSubEntry';
var url_getZairyouListItems = gApplicationPath + '/api/InputBukkenShousaiApi/GetZairyouSuggestItems';
var url_getToukyuuListItems = gApplicationPath + '/api/InputBukkenShousaiApi/GetToukyuuSuggestItems';
var url_SaveHiuchiData = gApplicationPath + '/api/InputBukkenShousaiApi/SaveHiuchiData';

function initialize_Hiuchi() {

    //addEvents
    bindKeyPressEvent('#HiuchiSubEntry');

    $('#HiuchiSubEntry #btnClose').click(function () {
        showConfirmMessage("Q003", function () {
            $('.js-modal-close').click();
        });
    });

    $('#HiuchiSubEntry #btnSaveSub').click(function () {
        if (checkErrorOnSave('#HiuchiSubEntry')) {
            var models = createModels();
            if (checkAllHiuchi(models)) {
                showConfirmMessage('Q101', function () {
                    btnSaveSubClick(models);
                    return true;
                });
            }
        }
        return false;
    });

    $('#HiuchiSubEntry [id^="btnPdf"]').click(function () {
        var id = this.id;
        showConfirmMessage('Q204', function () {
            var row = id.slice(-1);
            alert('call Hiuchi label Pdf row=' + row);
        });
    });

    //addValidate
    setNumericValidate('#Honsuu11', 3, 0);
    setNumericValidate('#Honsuu12', 3, 0);
    setNumericValidate('#Honsuu13', 3, 0);
    setNumericValidate('#Honsuu21', 3, 0);
    setNumericValidate('#Honsuu22', 3, 0);
    setNumericValidate('#Honsuu23', 3, 0);
    setNumericValidate('#Honsuu31', 3, 0);
    setNumericValidate('#Honsuu32', 3, 0);
    setNumericValidate('#Honsuu33', 3, 0);
    setNumericValidate('#Honsuu41', 3, 0);
    setNumericValidate('#Honsuu42', 3, 0);
    setNumericValidate('#Honsuu43', 3, 0);

    //setScreen
    setZairyouSuggestList();
    setToukyuuSuggestList();

    if (eMode == 'Delete') {
        $('#HiuchiSubEntry #btnClose').focus();
    }
    else {
        $('#HiuchiSubEntry #Sou1').focus();
    }
}


function setZairyouSuggestList() {
    var data;
    var result = calltoApiController(url_getZairyouListItems);
    if (result && result.MessageID) {
        showMessage(result);
    }
    else if (result) {
        data = result;
    }

    setSuggestList('#Zairyou11', undefined, undefined, data);
    setSuggestList('#Zairyou12', undefined, undefined, data);
    setSuggestList('#Zairyou23', undefined, undefined, data);
    setSuggestList('#Zairyou21', undefined, undefined, data);
    setSuggestList('#Zairyou22', undefined, undefined, data);
    setSuggestList('#Zairyou23', undefined, undefined, data);
    setSuggestList('#Zairyou31', undefined, undefined, data);
    setSuggestList('#Zairyou32', undefined, undefined, data);
    setSuggestList('#Zairyou33', undefined, undefined, data);
    setSuggestList('#Zairyou41', undefined, undefined, data);
    setSuggestList('#Zairyou42', undefined, undefined, data);
    setSuggestList('#Zairyou43', undefined, undefined, data);
}

function setToukyuuSuggestList() {
    var data;
    var result = calltoApiController(url_getToukyuuListItems);
    if (result && result.MessageID) {
        showMessage(result);
    }
    else if (result) {
        data = result;
    }

    setSuggestList('#Toukyuu11', undefined, undefined, data);
    setSuggestList('#Toukyuu12', undefined, undefined, data);
    setSuggestList('#Toukyuu23', undefined, undefined, data);
    setSuggestList('#Toukyuu21', undefined, undefined, data);
    setSuggestList('#Toukyuu22', undefined, undefined, data);
    setSuggestList('#Toukyuu23', undefined, undefined, data);
    setSuggestList('#Toukyuu31', undefined, undefined, data);
    setSuggestList('#Toukyuu32', undefined, undefined, data);
    setSuggestList('#Toukyuu33', undefined, undefined, data);
    setSuggestList('#Toukyuu41', undefined, undefined, data);
    setSuggestList('#Toukyuu42', undefined, undefined, data);
    setSuggestList('#Toukyuu43', undefined, undefined, data);
}

function createModels() {
    var model1 = {
        Sou: $('#Sou1').val(),
        SouSumi: $('#Sou1Sumi').prop('checked') ? 1 : 0,
        Zairyou1: $('#Zairyou11').val(),
        Toukyuu1: $('#Toukyuu11').val(),
        Honsuu1: $('#Honsuu11').val(),
        Zairyou2: $('#Zairyou12').val(),
        Toukyuu2: $('#Toukyuu12').val(),
        Honsuu2: $('#Honsuu12').val(),
        Zairyou3: $('#Zairyou13').val(),
        Toukyuu3: $('#Toukyuu13').val(),
        Honsuu3: $('#Honsuu13').val(),
    }
    var model2 = {
        Sou: $('#Sou2').val(),
        SouSumi: $('#Sou2Sumi').prop('checked') ? 1 : 0,
        Zairyou1: $('#Zairyou21').val(),
        Toukyuu1: $('#Toukyuu21').val(),
        Honsuu1: $('#Honsuu21').val(),
        Zairyou2: $('#Zairyou22').val(),
        Toukyuu2: $('#Toukyuu22').val(),
        Honsuu2: $('#Honsuu22').val(),
        Zairyou3: $('#Zairyou23').val(),
        Toukyuu3: $('#Toukyuu23').val(),
        Honsuu3: $('#Honsuu23').val(),
    }
    var model3 = {
        Sou: $('#Sou3').val(),
        SouSumi: $('#Sou3Sumi').prop('checked') ? 1 : 0,
        Zairyou1: $('#Zairyou31').val(),
        Toukyuu1: $('#Toukyuu31').val(),
        Honsuu1: $('#Honsuu31').val(),
        Zairyou2: $('#Zairyou32').val(),
        Toukyuu2: $('#Toukyuu32').val(),
        Honsuu2: $('#Honsuu32').val(),
        Zairyou3: $('#Zairyou33').val(),
        Toukyuu3: $('#Toukyuu33').val(),
        Honsuu3: $('#Honsuu33').val(),
    }
    var model4 = {
        Sou: $('#Sou4').val(),
        SouSumi: $('#Sou4Sumi').prop('checked') ? 1 : 0,
        Zairyou1: $('#Zairyou41').val(),
        Toukyuu1: $('#Toukyuu41').val(),
        Honsuu1: $('#Honsuu41').val(),
        Zairyou2: $('#Zairyou42').val(),
        Toukyuu2: $('#Toukyuu42').val(),
        Honsuu2: $('#Honsuu42').val(),
        Zairyou3: $('#Zairyou43').val(),
        Toukyuu3: $('#Toukyuu43').val(),
        Honsuu3: $('#Honsuu43').val(),
    }

    return new Array(model1, model2, model3, model4);
}

function checkAllHiuchi(models) {

    for (var i = 1; i <= models.length; i++) {
        var model = models[i - 1];

        if (model.Sou == "") {
            //その段の他項目が1つでも入力されていたら必須項目。空白はエラー。
            if (model.Zairyou1 != "" || model.Toukyuu1 != "" || model.Honsuu1 != "" ||
                model.Zairyou2 != "" || model.Toukyuu2 != "" || model.Honsuu2 != "" ||
                model.Zairyou3 != "" || model.Toukyuu3 != "" || model.Honsuu3 != "") {
                $('#Sou' + i).focus();
                showMessage('E102');
                return false;
            }
        }
        else {
            //その段の層が入力されていたら1行目の材料は必須項目。空白はエラー。
            if (model.Zairyou1 == "") {
                $('#Zairyou' + i + '1').focus();
                showMessage('E102');
                return false;
            }
        }

        if (model.Zairyou1 != "") {
            //その行の材料が入力されていたら必須項目。空白はエラー。
            if (model.Toukyuu1 == "") {
                $('#Toukyuu' + i + '1').focus();
                showMessage('E102');
                return false;
            }
            if (model.Honsuu1 == "") {
                $('#Honsuu' + i + '1').focus();
                showMessage('E102');
                return false;
            }
        }

        if (model.Zairyou2 != "") {
            //その行の材料が入力されていたら必須項目。空白はエラー。
            if (model.Toukyuu2 == "") {
                $('#Toukyuu' + i + '2').focus();
                showMessage('E102');
                return false;
            }
            if (model.Honsuu2 == "") {
                $('#Honsuu' + i + '2').focus();
                showMessage('E102');
                return false;
            }
        }

        if (model.Zairyou3 != "") {
            //その行の材料が入力されていたら必須項目。空白はエラー。
            if (model.Toukyuu3 == "") {
                $('#Toukyuu' + i + '3').focus();
                showMessage('E102');
                return false;
            }
            if (model.Honsuu3 == "") {
                $('#Honsuu' + i + '3').focus();
                showMessage('E102');
                return false;
            }
        }
    }

    return true;
}

function btnSaveSubClick(models) {
    var dbModel = {};

    for (var i = 1; i <= models.length; i++) {
        var model = models[i - 1];

        dbModel['Sou' + i] = model.Sou;
        dbModel['Sou' + i + 'Sumi'] = model.SouSumi;
        dbModel['Zairyou' + i + '1'] = model.Zairyou1;
        dbModel['Toukyuu' + i + '1'] = model.Toukyuu1;
        dbModel['Honsuu' + i + '1'] = model.Honsuu1;
        dbModel['Zairyou' + i + '2'] = model.Zairyou2;
        dbModel['Toukyuu' + i + '2'] = model.Toukyuu2;
        dbModel['Honsuu' + i + '2'] = model.Honsuu2;
        dbModel['Zairyou' + i + '3'] = model.Zairyou3;
        dbModel['Toukyuu' + i + '3'] = model.Toukyuu3;
        dbModel['Honsuu' + i + '3'] = model.Honsuu3;
    }

    dbModel.HiddenUpdateDateTime = $('#HiddenUpdateDateTimeHiuchi').val();
    dbModel.UserID = $('#user-id').text();

    var result = calltoApiController(url_SaveHiuchiData, dbModel);
    if (!result) {
        return false;
    }
    if (result.MessageID) {
        showMessage(result);
        return false;
    }

    showMessage('I101', function () {
        $('.js-modal-close').click();
    });
}
