//InputBukkenShousaiHiuchi.js
var url_HiuchiEntry = gApplicationPath + '/InputBukkenShousai/HiuchiEntry';
var url_getZairyouListItems = gApplicationPath + '/api/InputBukkenShousaiHiuchiApi/GetZairyouSuggestItems';
var url_getToukyuuListItems = gApplicationPath + '/api/InputBukkenShousaiHiuchiApi/GetToukyuuSuggestItems';

function initialize_Hiuchi() {

    //addEvents
    bindKeyPressEvent('#HiuchiEntry');

    $('#HiuchiEntry #btnClose').click(function () {
        showConfirmMessage("Q003", function () {
            $('.js-modal-close').click();
        });
    });

    $('#HiuchiEntry #btnSave').click(function () {
        alert('btnSave click')
    });

    $('#HiuchiEntry [id^="btnPdf"]').click(function () {
        alert('btnPdf click')
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
        $('#HiuchiEntry #btnClose').focus();
    }
    else {
        $('#HiuchiEntry #Sou1').focus();
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
    setSuggestList('#Zairyou13', undefined, undefined, data);

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
    setSuggestList('#Toukyuu13', undefined, undefined, data);

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
