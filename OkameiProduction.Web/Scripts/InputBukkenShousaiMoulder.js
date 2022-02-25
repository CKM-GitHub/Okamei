//InputBukkenShousaiMoulder.js
var url_SaveMoulderData = gApplicationPath + '/api/InputBukkenShousaiMoulderApi/SaveMoulderData';

function finalize_Moulder() {
    $('#btnKakousiji').focus();
}

function initialize_Moulder() {

    //addEvents
    bindKeyPressEvent('#MoulderSubEntry');

    $('#MoulderSubEntry #btnClose').click(function () {
        showConfirmMessage("Q003", function () {
            ModalForm.Close();
        });
    });

    $('#MoulderSubEntry #btnAdd').click(function () {
        btnAddMoulderClick();
    });

    $('#MoulderSubEntry #btnSaveMoulder').click(function () {
        if (checkErrorOnSave('#MoulderSubEntry')) {
            showConfirmMessage('Q101', function () {
                return btnSaveMoulderClick();
            });
        }
        return false;
    });

    //addValidate
    setNumericValidate('#MoulderSubEntry [name="MoulderHaba"]', 5, 0);
    setNumericValidate('#MoulderSubEntry [name="MoulderNari"]', 5, 0);
    setNumericValidate('#MoulderSubEntry [name="MoulderNagasa"]', 5, 0);
    setNumericValidate('#MoulderSubEntry [name="MoulderHonsuu"]', 5, 0);

    //setScreen
    $('#MoulderSubEntry input[type=checkbox]').each(function () {
        $(this).prop('checked', $(this).data('dbvalue') == "1");
    });

    if (eMode == 'Delete') {
        setDisabledAll('#MoulderSubEntry', '.js-modal-close, #MoulderSubEntry #btnClose');
        $('#MoulderSubEntry #btnClose').focus();
    }
    else {
        moveFocus($('#tblBukkenMoulder [name="MoulderHinmoku"]').first());
    }
}

function moveFocus(obj) {
    setTimeout(function () { obj.focus(); }, 500);
}

function btnAddMoulderClick() {
    const tr = $('.templatetable tr').clone();
    $('#tblBukkenMoulder tbody').append(tr);
    $(tr).find('[name="MoulderHinmoku"]').focus();
}

function checkAll_Moulder(model, tr) {

    if (model.MoulderHinmoku) {
        if (!model.MoulderZairyou) {
            showMessage('E102', function () { moveFocus($(tr).find('[name="MoulderZairyou"]')); });
            return false;
        }
        if (!model.MoulderHaba) {
            showMessage('E102', function () { moveFocus($(tr).find('[name="MoulderHaba"]')); });
            return false;
        }
        if (!model.MoulderNari) {
            showMessage('E102', function () { moveFocus($(tr).find('[name="MoulderNari"]')); });
            return false;
        }
        if (!model.MoulderNagasa) {
            showMessage('E102', function () { moveFocus($(tr).find('[name="MoulderNagasa"]')); });
            return false;
        }
        if (!model.MoulderHonsuu) {
            showMessage('E102', function () { moveFocus($(tr).find('[name="MoulderHonsuu"]')); });
            return false;
        }
    }
    else {
        if (model.MoulderZairyou || model.MoulderHaba || model.MoulderNari || model.MoulderNagasa || model.MoulderHonsuu) {            
            showMessage('E102', function () { moveFocus($(tr).find('[name="MoulderHinmoku"]')); });
            return false;
        }
    }
    return true;
}

function btnSaveMoulderClick() {

    var error = false;
    var array = [];
    $.each($("#tblBukkenMoulder tbody tr"), function (i, tr) {
        var data = {};
        $(tr).find("select, input").each(function () {
            var key = $(this).attr('name');
            if (key == 'MoulderSumi') {
                data[key] = $(this).is(':checked') ? 1 : 0;
            }
            else {
                data[key] = $(this).val();
            }
        });

        if (!checkAll_Moulder(data, tr)) {
            error = true;
            return false;
        }

        if (data.MoulderHinmoku) array.push(data);
    });

    if (error) return false;

    var model = {
        BukkenNO: $('#MoulderSubEntry #MoulderBukkenNO').val(),
        HiddenUpdateDateTime: $('#HiddenMoulderUpdateDateTime').val(),
        UserID: $('#user-id').text(),
        RecordsJson: JSON.stringify(array)
    }

    var result = calltoApiController(url_SaveMoulderData, model);
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
