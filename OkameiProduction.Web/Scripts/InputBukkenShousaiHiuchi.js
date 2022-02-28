//InputBukkenShousaiHiuchi.js
var url_getZairyouListItems = gApplicationPath + '/api/InputBukkenShousaiHiuchiApi/GetZairyouSuggestItems';
var url_getToukyuuListItems = gApplicationPath + '/api/InputBukkenShousaiHiuchiApi/GetToukyuuSuggestItems';
var url_SaveHiuchiData = gApplicationPath + '/api/InputBukkenShousaiHiuchiApi/SaveHiuchiData';
var url_importHiuchiCsv = gApplicationPath + '/api/InputBukkenShousaiHiuchiApi/ImportHiuchiCsv';
var url_exportHiuchiPdf = gApplicationPath + '/api/InputBukkenShousaiHiuchiApi/HiuchiPdfExport';

function finalize_Hiuchi() {
    undindKeyPressEvent('#HiuchiSubEntry');
    $('#HiuchiKakou').focus();
}

function initialize_Hiuchi() {

    //addEvents
    bindKeyPressEvent('#HiuchiSubEntry');

    $('#HiuchiSubEntry #btnClose').click(function () {
        showConfirmMessage("Q003", function () {
            ModalForm.Close();
        });
    });

    $('#HiuchiSubEntry #btnSaveHiuchi').click(function () {
        if (checkErrorOnSave('#HiuchiSubEntry')) {
            var models = createModels_Hiuchi();
            if (checkAll_Hiuchi(models)) {
                showConfirmMessage('Q101', function () {
                    btnSaveHiuchiClick(models);
                    return true;
                });
            }
        }
        return false;
    });

    $('#HiuchiSubEntry [id^="btnPdf"]').click(function () {
        btnExportPdfHiuchi(this);
    });

    $('#HiuchiSubEntry #btnFileOpen').click(function () {
        $('#HiuchiSubEntry #inputFileHiuchi').click();
    });

    $('#HiuchiSubEntry #inputFileHiuchi').change(function () {
        inputFileHiuchiChange(this);
    });

    //addValidate
    setNumericValidate('#HiuchiSubEntry #Honsuu11', 3, 0);
    setNumericValidate('#HiuchiSubEntry #Honsuu12', 3, 0);
    setNumericValidate('#HiuchiSubEntry #Honsuu13', 3, 0);
    setNumericValidate('#HiuchiSubEntry #Honsuu21', 3, 0);
    setNumericValidate('#HiuchiSubEntry #Honsuu22', 3, 0);
    setNumericValidate('#HiuchiSubEntry #Honsuu23', 3, 0);
    setNumericValidate('#HiuchiSubEntry #Honsuu31', 3, 0);
    setNumericValidate('#HiuchiSubEntry #Honsuu32', 3, 0);
    setNumericValidate('#HiuchiSubEntry #Honsuu33', 3, 0);
    setNumericValidate('#HiuchiSubEntry #Honsuu41', 3, 0);
    setNumericValidate('#HiuchiSubEntry #Honsuu42', 3, 0);
    setNumericValidate('#HiuchiSubEntry #Honsuu43', 3, 0);

    //setScreen
    //Set the value of the checkbox
    $('#HiuchiSubEntry input[type=checkbox]').each(function () {
        $(this).prop('checked', $(this).data('dbvalue') == "1").change();
    });

    setZairyouSuggestList();
    setToukyuuSuggestList();

    if (eMode == 'Delete') {
        setDisabledAll('#HiuchiSubEntry', '.js-modal-close, #HiuchiSubEntry #btnClose');
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

    setSuggestList('#HiuchiSubEntry #Zairyou11', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Zairyou12', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Zairyou13', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Zairyou21', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Zairyou22', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Zairyou23', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Zairyou31', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Zairyou32', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Zairyou33', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Zairyou41', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Zairyou42', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Zairyou43', undefined, undefined, data);
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

    setSuggestList('#HiuchiSubEntry #Toukyuu11', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Toukyuu12', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Toukyuu13', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Toukyuu21', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Toukyuu22', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Toukyuu23', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Toukyuu31', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Toukyuu32', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Toukyuu33', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Toukyuu41', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Toukyuu42', undefined, undefined, data);
    setSuggestList('#HiuchiSubEntry #Toukyuu43', undefined, undefined, data);
}

function createModels_Hiuchi() {
    var model1 = {
        Sou: $('#HiuchiSubEntry #Sou1').val(),
        SouSumi: $('#HiuchiSubEntry #Sou1Sumi').prop('checked') ? 1 : 0,
        Zairyou1: $('#HiuchiSubEntry #Zairyou11').val().trim(),
        Toukyuu1: $('#HiuchiSubEntry #Toukyuu11').val().trim(),
        Honsuu1: $('#HiuchiSubEntry #Honsuu11').val().trim(),
        Zairyou2: $('#HiuchiSubEntry #Zairyou12').val().trim(),
        Toukyuu2: $('#HiuchiSubEntry #Toukyuu12').val().trim(),
        Honsuu2: $('#HiuchiSubEntry #Honsuu12').val().trim(),
        Zairyou3: $('#HiuchiSubEntry #Zairyou13').val().trim(),
        Toukyuu3: $('#HiuchiSubEntry #Toukyuu13').val().trim(),
        Honsuu3: $('#HiuchiSubEntry #Honsuu13').val().trim(),
    }
    var model2 = {
        Sou: $('#HiuchiSubEntry #Sou2').val(),
        SouSumi: $('#HiuchiSubEntry #Sou2Sumi').prop('checked') ? 1 : 0,
        Zairyou1: $('#HiuchiSubEntry #Zairyou21').val().trim(),
        Toukyuu1: $('#HiuchiSubEntry #Toukyuu21').val().trim(),
        Honsuu1: $('#HiuchiSubEntry #Honsuu21').val().trim(),
        Zairyou2: $('#HiuchiSubEntry #Zairyou22').val().trim(),
        Toukyuu2: $('#HiuchiSubEntry #Toukyuu22').val().trim(),
        Honsuu2: $('#HiuchiSubEntry #Honsuu22').val().trim(),
        Zairyou3: $('#HiuchiSubEntry #Zairyou23').val().trim(),
        Toukyuu3: $('#HiuchiSubEntry #Toukyuu23').val().trim(),
        Honsuu3: $('#HiuchiSubEntry #Honsuu23').val().trim(),
    }
    var model3 = {
        Sou: $('#HiuchiSubEntry #Sou3').val(),
        SouSumi: $('#HiuchiSubEntry #Sou3Sumi').prop('checked') ? 1 : 0,
        Zairyou1: $('#HiuchiSubEntry #Zairyou31').val().trim(),
        Toukyuu1: $('#HiuchiSubEntry #Toukyuu31').val().trim(),
        Honsuu1: $('#HiuchiSubEntry #Honsuu31').val().trim(),
        Zairyou2: $('#HiuchiSubEntry #Zairyou32').val().trim(),
        Toukyuu2: $('#HiuchiSubEntry #Toukyuu32').val().trim(),
        Honsuu2: $('#HiuchiSubEntry #Honsuu32').val().trim(),
        Zairyou3: $('#HiuchiSubEntry #Zairyou33').val().trim(),
        Toukyuu3: $('#HiuchiSubEntry #Toukyuu33').val().trim(),
        Honsuu3: $('#HiuchiSubEntry #Honsuu33').val().trim(),
    }
    var model4 = {
        Sou: $('#HiuchiSubEntry #Sou4').val(),
        SouSumi: $('#HiuchiSubEntry #Sou4Sumi').prop('checked') ? 1 : 0,
        Zairyou1: $('#HiuchiSubEntry #Zairyou41').val().trim(),
        Toukyuu1: $('#HiuchiSubEntry #Toukyuu41').val().trim(),
        Honsuu1: $('#HiuchiSubEntry #Honsuu41').val().trim(),
        Zairyou2: $('#HiuchiSubEntry #Zairyou42').val().trim(),
        Toukyuu2: $('#HiuchiSubEntry #Toukyuu42').val().trim(),
        Honsuu2: $('#HiuchiSubEntry #Honsuu42').val().trim(),
        Zairyou3: $('#HiuchiSubEntry #Zairyou43').val().trim(),
        Toukyuu3: $('#HiuchiSubEntry #Toukyuu43').val().trim(),
        Honsuu3: $('#HiuchiSubEntry #Honsuu43').val().trim(),
    }

    return new Array(model1, model2, model3, model4);
}

function checkAll_Hiuchi(models) {
    var count = 0;
    for (var i = 1; i <= models.length; i++) {
        var model = models[i - 1];

        if (!model.Sou) {
            //その段の他項目が1つでも入力されていたら必須項目。空白はエラー。
            if (model.Zairyou1 || model.Toukyuu1 || model.Honsuu1 ||
                model.Zairyou2 || model.Toukyuu2 || model.Honsuu2 ||
                model.Zairyou3 || model.Toukyuu3 || model.Honsuu3) {
                $('#HiuchiSubEntry #Sou' + i).focus();
                showMessage('E102');
                return false;
            }
        }
        else {
            //その段の層が入力されていたら1行目の材料は必須項目。空白はエラー。
            if (!model.Zairyou1) {
                $('#HiuchiSubEntry #Zairyou' + i + '1').focus();
                showMessage('E102');
                return false;
            }
        }

        if (model.Zairyou1) {
            //その行の材料が入力されていたら必須項目。空白はエラー。
            if (!model.Toukyuu1) {
                $('#HiuchiSubEntry #Toukyuu' + i + '1').focus();
                showMessage('E102');
                return false;
            }
            if (!model.Honsuu1) {
                $('#HiuchiSubEntry #Honsuu' + i + '1').focus();
                showMessage('E102');
                return false;
            }
        }

        if (model.Zairyou2) {
            //その行の材料が入力されていたら必須項目。空白はエラー。
            if (!model.Toukyuu2) {
                $('#HiuchiSubEntry #Toukyuu' + i + '2').focus();
                showMessage('E102');
                return false;
            }
            if (!model.Honsuu2) {
                $('#HiuchiSubEntry #Honsuu' + i + '2').focus();
                showMessage('E102');
                return false;
            }
        }

        if (model.Zairyou3) {
            //その行の材料が入力されていたら必須項目。空白はエラー。
            if (!model.Toukyuu3) {
                $('#HiuchiSubEntry #Toukyuu' + i + '3').focus();
                showMessage('E102');
                return false;
            }
            if (!model.Honsuu3) {
                $('#HiuchiSubEntry #Honsuu' + i + '3').focus();
                showMessage('E102');
                return false;
            }
        }

        if (model.Sou)
        {
            count++;
        }
    }
    //If not all are entered, delete them from the database.
    //return count > 0;
    return true;
}

function btnSaveHiuchiClick(models) {
    var dbModel = {};

    var count = 0;
    for (var i = 1; i <= models.length; i++) {
        var model = models[i - 1];

        if (model.Sou) {
            dbModel['Sou' + i] = model.Sou;
            dbModel['Sou' + i + 'Sumi'] = model.SouSumi;
            dbModel['Zairyou' + i + '1'] = model.Zairyou1;
            dbModel['Toukyuu' + i + '1'] = model.Toukyuu1;
            dbModel['Honsuu' + i + '1'] = model.Honsuu1;

            if (model.Zairyou2) {
                dbModel['Zairyou' + i + '2'] = model.Zairyou2;
                dbModel['Toukyuu' + i + '2'] = model.Toukyuu2;
                dbModel['Honsuu' + i + '2'] = model.Honsuu2;
            }
            if (model.Zairyou3) {
                dbModel['Zairyou' + i + '3'] = model.Zairyou3;
                dbModel['Toukyuu' + i + '3'] = model.Toukyuu3;
                dbModel['Honsuu' + i + '3'] = model.Honsuu3;
            }
            count++;
        }
    }

    dbModel.BukkenNO = $('#HiuchiSubEntry #HiuchiBukkenNO').val();
    dbModel.HiddenUpdateDateTime = $('#HiuchiSubEntry #HiddenHiuchiUpdateDateTime').val();
    dbModel.UserID = $('#user-id').text();
    dbModel.SouCount = count;

    var result = calltoApiController(url_SaveHiuchiData, dbModel);
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

function btnExportPdfHiuchi(e) {
    var id = e.id;
    var row = id.slice(-1);

    var model =
    {
        BukkenNO: $('#HiuchiSubEntry #HiuchiBukkenNO').val(),
        BukkenName: $('#HiuchiSubEntry #HiuchiBukkenName').val(),
        KoumutenName: $('#KoumutenName').val(),
        SouName: $('#HiuchiSubEntry #Sou' + row + ' option:selected').text(),
        Zairyou1: $('#HiuchiSubEntry #Zairyou' + row + '1').val(),
        Toukyuu1: $('#HiuchiSubEntry #Toukyuu' + row + '1').val(),
        Honsuu1: $('#HiuchiSubEntry #Honsuu' + row + '1').val(),
        Zairyou2: $('#HiuchiSubEntry #Zairyou' + row + '2').val(),
        Toukyuu2: $('#HiuchiSubEntry #Toukyuu' + row + '2').val(),
        Honsuu2: $('#HiuchiSubEntry #Honsuu' + row + '2').val(),
        Zairyou3: $('#HiuchiSubEntry #Zairyou' + row + '3').val(),
        Toukyuu3: $('#HiuchiSubEntry #Toukyuu' + row + '3').val(),
        Honsuu3: $('#HiuchiSubEntry #Honsuu' + row + '3').val(),
    };

    if (model.SouName && model.Zairyou1) {
        showConfirmMessage('Q204', function () {
            model.FileName = '火打材ラベル_' + model.BukkenName + '.pdf';
            calltoApiController_FileDownLoadHandle(url_exportHiuchiPdf, model);
        });
    }
}

function inputFileHiuchiChange(e) {
    var files = $(e).prop('files')
    for (var i = 0; i < files.length; i++) {
        if (files[i].name.slice(-4) != '.csv') continue;

        var fileData = new FormData();
        fileData.append('file', files[i]);

        showLoadingMessage();
        calltoApiController_FileUploadHandle(url_importHiuchiCsv, fileData,
            function (result) {
                if (!result) return;
                if (result.MessageID) {
                    showMessage(result);
                    return;
                }

                for (key in result) {
                    if (key == "BukkenNO" || key == "BukkenName" || key == "HiddenUpdateDateTime") {
                    }
                    else if (key == "Sou1Sumi" || key == "Sou2Sumi" || key == "Sou3Sumi" || key == "Sou4Sumi") {
                        $('#HiuchiSubEntry #' + key).prop('checked', result[key] == "1")
                    }
                    else {
                        $('#HiuchiSubEntry #' + key).val(result[key]);
                    }
                }

                setZairyouSuggestList();
                setToukyuuSuggestList();
                closeLoadingMessage();
            });
    }

    $(e).val(null);
}