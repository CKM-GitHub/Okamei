//InputBukkenShousai.js

function addEvents() {
    btnShoudakusho.click(function () {
        showConfirmMessage("Q205", function () {
            if (eMode == 'Edit') {
                var fileName = '加工承諾書_' + txtBukkenName.val() + '.xlsx'; 
                var model = 
                {
                    FileName: fileName,   
                    TantouSitenCD: txtSitenCD.val(),
                    BukkenName: txtBukkenName.val(),
                    KoumutenName: $('#KoumutenName').val(),
                    Nouki: txtNouki.val(),
                    TantouEigyouCD: ddlTantouEigyou.val(), 
                }; 
                calltoApiController_FileDownLoadHandle(url_exportAgreementForm, model);
                //location.href = url_homePage;url_exportAgreementForm 
            }
            else {
                location.href = url_previousPage;
            }
        });
    });
    btnJissekiNippou.click(function () {
        showConfirmMessage("Q204", function () {
            if (eMode == 'Edit') {
                //プレカット実績日報_ああああああああ.pdf
                var fileName = 'プレカット実績日報_' + txtBukkenName.val() + '.pdf';
                var model =
                {
                    FileName: fileName,
                    TantouSitenCD: txtSitenCD.val(),
                    TantouEigyouCD: ddlTantouEigyou.val(),
                    BukkenName: txtBukkenName.val(),
                    KoumutenName: $('#KoumutenName').val(),
                    KakouTubosuu: $('#KakouTubosuu').val(),
                    BukkenNO: txtBukkenNO.val(), 
                };
                calltoApiController_FileDownLoadHandle(url_exportPurecattoForm, model);
                //location.href = url_homePage;url_exportAgreementForm 
            }
            else {
                location.href = url_previousPage;
            }
        });
    });
    btnReturn.click(function () {
        showConfirmMessage("Q003", function () {
            if (eMode == 'New') {
                location.href = url_homePage;
            }
            else {
                location.href = url_previousPage;
            }
        });
    });

    btnSave.click(function () {
        if (checkErrorOnSave('#main') && checkBukkenNO()) {
            showConfirmMessage(eMode == 'Delete' ? 'Q102' : 'Q101', function () {
                btnSaveClick();
            });
        }
    });

    btnSet.click(function () {
        getNewBukkenNO();
    });

    txtSitenCD.change(function () {
        var key = $(this).val();
        setDropDownList('#TantouEigyouCD', url_getTantouEigyouListItems, key);
        setSuggestList('#KoumutenName', url_getKoumutenListItems, key);
        if (eMode == 'New') {
            txtBukkenNO.val("");
        }
    });

    chkNoukiMitei.change(function () {
        var checked = $(this).is(':checked');
        txtNouki.prop('disabled', checked);
        if (checked) {
            txtNouki.val("");
        }
    });

    $('input[name="UpFileOption"]:radio').change(function () {
        var val = $(this).val();
        if (!val || val == "8") {
            dragDropArea.addClass('close');
        }
    });

    $('#btnUpload').click(function () {
        var shurui = $('input[name="UpFileOption"]:radio:checked').val();
        if (!checkBukkenNO() || shurui == "8") {
            dragDropArea.addClass('close');
            return;
        }
        dragDropArea.toggleClass('close');
    });

    $('input[name="DownFileOption"]:radio').change(function () {
        createBukkenFileTable();
    });

    $('#btnDownload').click(function () {
        downloadFiles();
    });

    $('#BukkenComment').keydown(function (e) {
        var c = e.which ? e.which : e.keyCode;
        if (c == 13) {
            var model = {
                BukkenNO: txtBukkenNO.val(),
                BukkenComment: $(this).val(),
                UserID: $('#user-id').text()
            }
            var result = calltoApiController(url_SaveBukkenComment, model);
            if (!result) return;
            if (result.MessageID) {
                showMessage(result);
                return;
            }
            $(this).val("");
            createBukkenCommentTable();
        }
    });

    //drop area
    $('.drop_area').on('dragenter', function (e) {
        e.stopPropagation();
        e.preventDefault();
        $(this).addClass('selected');
    }).on('dragover', function (e) {
        e.stopPropagation();
        e.preventDefault();
        $(this).addClass('selected');
    }).on('dragover', function (e) {
        e.stopPropagation();
        e.preventDefault();
        $(this).addClass('selected');
    }).on('dragleave', function (e) {
        e.stopPropagation();
        e.preventDefault();
        $(this).removeClass('selected');
    }).on('drop', function (e) {
        e.preventDefault();
        var files = e.originalEvent.dataTransfer.files;
        dropFiles(files, $('#fileStatus'));
        $(this).removeClass('selected');
    });

    $('#inputFile').on('change' ,function (e) {
        var files = $(this).prop('files')
        dropFiles(files, $('#fileStatus'));
        $(this).val(null);
    });
}

function addValidate() {
    setRequired('#TantouSitenCD');
    setRequired('#BukkenName');
    setRequired('#KoumutenName');
    setRequired('#KakouTubosuu');
    setRequired('#Nouki');
    setRequired('#TantouEigyouCD');
    setRequired('#JuchuuDate');

    setDateTypeValidate('#Nouki')
    setDateTypeValidate('#UnsouKuraireDate')
    setDateTypeValidate('#ZairyouNouki')
    setDateTypeValidate('#JuchuuDate')
    setDateTypeValidate('#FusezuTeishutuDate')
    setDateTypeValidate('#KakouShouninDate')
    setDateTypeValidate('#KidasiDate')
    setDateTypeValidate('#KakousijishoHakkouDate')
    setDateTypeValidate('#KannouDate')
    setDateTypeValidate('#CancelDate')

    setDoubleByteValidate('#BukkenName', true);
    setDoubleByteValidate('#Juusho');
    setDoubleByteValidate('#TokuchuuzaiComment');
    setDoubleByteValidate('#BukkenComment');

    setNumericValidate('#KakouTubosuu', 3, 2)
    setNumericValidate('#KakouNissuu', 3, 0);
    setNumericValidate('#HagarazaiSuu', 3, 0);
    setNumericValidate('#YukaGouhanSuu', 3, 0);
    setNumericValidate('#NoziGouhanSuu', 3, 0);
    setNumericValidate('#TekakouTime', 3, 1);
    setNumericValidate('#HundeggerTime', 3, 1);
}

function setScreen() {
    //Set the value of the checkbox
    $('input[type=checkbox]').each(function () {
        $(this).prop('checked', $(this).data('dbvalue') == "1").change();
    });

    if (eMode == 'Edit' || eMode == 'Delete') {
        //when browser back
        if (txtBukkenNO.val() == "") {
            $('#main :input:not(button)').val('');
            $('#main a').addClass('not-available');
            setDisabledAll('#main', true);
            btnReturn.prop('disabled', false).focus();
            return;
        }
    }

    createBukkenFileTable();
    createBukkenCommentTable();
    clearFileInfo();

    if (eMode == 'New') {
        btnSet.show();
        txtSitenCD.removeClass('size50').addClass('size39');
    }
    else if (eMode == 'Edit') {
        setSuggestList('#KoumutenName', url_getKoumutenListItems, txtSitenCD.val());
    }
    else if (eMode == 'Delete') {
        setDisabledAll('#main');
        btnSave.text('削除');
    }
}

function createBukkenFileTable() {
    var data;

    var model = {
        BukkenNO: txtBukkenNO.val(),
        BukkenFileShurui: $('input[name="DownFileOption"]:radio:checked').val(),
    }
    var result = calltoApiController(url_getBukkenFile, model);
    if (result && result.MessageID) {
        showMessage(result);
    }
    else if (result) {
        data = result;
    }

    var option = {};

    option.data = data;
    option.columns = [
        { data: 'BukkenFileShurui', className: 'BukkenFileShurui' },
        { data: 'InsertDatetime' },
        { data: 'BukkenFileName', className: 'BukkenFileName' },
        { data: 'InsertOperator', className: 'InsertOperator' },
        { data: 'BukkenFileRows' },
        {
            "data": null,
            render: function (data, type, row) {
                return '<a class="delete-link" onclick="deleteBukkenFile(' + data.BukkenFileRows.toString() + ');">削除</a>';
            }
        },
        {
            "data": null,
            render: function (data, type, row) {
                return '<input type="checkbox" data-bukkenfilerows="' + data.BukkenFileRows.toString() + '"/>';
            }
        },
    ];
    option.columnDefs = [
        { targets: [4], visible: false },
        { targets: '_all', visible: true },
    ];

    if (eMode == 'Delete') {
        option.drawCallback = function () { setDisabledAll('#tblBukkenFile'); }
    }

    option.paging = false;
    option.info = false;

    bindDataTables($('#tblBukkenFile'), option);
}

function createBukkenCommentTable() {
    var data;

    var model = {
        BukkenNO: txtBukkenNO.val(),
    }
    var result = calltoApiController(url_getBukkenComment, model);
    if (result && result.MessageID) {
        showMessage(result);
    }
    else if (result) {
        data = result;
    }

    var option = {};

    option.data = data;
    option.columns = [
        { data: 'InsertDatetime' },
        { data: 'BukkenComment', className: 'BukkenComment' },
        { data: 'InsertOperator', className: 'InsertOperator' },
        { data: 'BukkenCommentRows' },
        {
            "data": null,
            render: function (data, type, row) {
                return '<a class="delete-link" onclick="deleteBukkenComment(' + data.BukkenCommentRows.toString() + ');">削除</a>';
            }
        }
    ];
    option.columnDefs = [
        { targets: [3], visible: false },
        { targets: '_all', visible: true },
    ];

    if (eMode == 'Delete') {
        option.drawCallback = function() { setDisabledAll('#tblBukkenComment'); }
    }

    option.paging = false;
    option.info = false;

    bindDataTables($('#tblBukkenComment'), option);
}

function deleteBukkenFile(row) {
    var model = {
        BukkenNO: txtBukkenNO.val(),
        BukkenFileRows: row,
    }
    var result = calltoApiController(url_deleteBukkenFile, model);
    if (!result) return;
    if (result.MessageID) {
        showMessage(result);
        return;
    }
    createBukkenFileTable();
}

function deleteBukkenComment(row) {
    var model = {
        BukkenNO: txtBukkenNO.val(),
        BukkenCommentRows: row,
    }
    var result = calltoApiController(url_deleteBukkenComment, model);
    if (!result) return;
    if (result.MessageID) {
        showMessage(result);
        return;
    }
    createBukkenCommentTable();
}

function getNewBukkenNO() {

    if (txtBukkenNO.val() != "") {
        txtBukkenName.focus();
        return;
    }

    if (txtSitenCD.val() == "") {
        txtSitenCD.focus();
        showMessage('E102');
        return;
    }

    var result = calltoApiController(url_getBukkenNO, txtSitenCD.val());
    if (!result) {
        return;
    }
    if (result.MessageID) {
        showMessage(result);
        return;
    }
    if (result.NewBukkenNO) {
        txtBukkenNO.val(result.NewBukkenNO);
        txtBukkenName.focus();
    }
}

function checkBukkenNO() {
    if (txtBukkenNO.val() == "") {
        txtSitenCD.focus();
        showMessage('E288', function () { txtSitenCD.focus(); });
        return false;
    }
    return true;
}

function btnSaveClick() {
    var model = {
        Mode: eMode,
        BukkenNO: txtBukkenNO.val(),
        TantouSitenCD: txtSitenCD.val(),
        BukkenName: txtBukkenName.val(),
        Juusho: $('#Juusho').val(),
        KoumutenName: $('#KoumutenName').val(),
        KakouTubosuu: $('#KakouTubosuu').val(),
        NoukiMiteiKBN: chkNoukiMitei.is(':checked') ? 1 : 0,
        Nouki: txtNouki.val(),
        UnsouKuraireDate: $('#UnsouKuraireDate').val(),
        KubunCD: $('#KubunCD').val(),
        TantouEigyouCD: ddlTantouEigyou.val(),
        TantouPcCD: $('#TantouPcCD').val(),
        TantouCadCD: $('#TantouCadCD').val(),
        NyuuryokusakiCD: $('#NyuuryokusakiCD').val(),
        TokuchuuzaiUmu: $('#TokuchuuzaiUmu').val(),
        ZairyouNouki: $('#ZairyouNouki').val(),
        TokuchuuzaiComment: $('#TokuchuuzaiComment').val(),
        //Page 2.
        JuchuuDate: $('#JuchuuDate').val(),
        FusezuTeishutuDate: $('#FusezuTeishutuDate').val(),
        KakouShouninDate: $('#KakouShouninDate').val(),
        KidasiDate: $('#KidasiDate').val(),
        KakousijishoHakkouDate: $('#KakousijishoHakkouDate').val(),
        KannouDate: $('#KannouDate').val(),
        CancelDate: $('#CancelDate').val(),
        KakouNissuu: $('#KakouNissuu').val(),
        UpdateDateTime: $('#UpdateDateTime').val(),
        KanamonoCD: $('#KanamonoCD').val(),
        OukazaiKakou: $('#OukazaiKakou').val(),
        OukazaiSumi: $('#OukazaiSumi').is(':checked') ? 1 : 0,
        KabeKakou: $('#KabeKakou').val(),
        KabeSumi: $('#KabeSumi').is(':checked') ? 1 : 0,
        HasirazaiKakou: $('#HasirazaiKakou').val(),
        HasirazaiSumi: $('#HasirazaiSumi').is(':checked') ? 1 : 0,
        HiuchiKakou: $('#HiuchiKakou').val(),
        HiuchiSumi: $('#HiuchiSumi').is(':checked') ? 1 : 0,
        HagarazaiKakou: $('#HagarazaiKakou').val(),
        HagarazaiSumi: $('#HagarazaiSumi').is(':checked') ? 1 : 0,
        HagarazaiSuu: $('#HagarazaiSuu').val(),
        YukaKakou: $('#YukaKakou').val(),
        YukaSumi: $('#YukaSumi').is(':checked') ? 1 : 0,
        YukaGouhanShurui: $('#YukaGouhanShurui').val(),
        YukaGouhanSuu: $('#YukaGouhanSuu').val(),
        NoziKakou: $('#NoziKakou').val(),
        NoziSumi: $('#NoziSumi').is(':checked') ? 1 : 0,
        NoziGouhanShurui: $('#NoziGouhanShurui').val(),
        NoziGouhanSuu: $('#NoziGouhanSuu').val(),
        TekakouKakou: $('#TekakouKakou').val(),
        TekakouSumi: $('#TekakouSumi').is(':checked') ? 1 : 0,
        TekakouTime: $('#TekakouTime').val(),
        HundeggerKakou: $('#HundeggerKakou').val(),
        HundeggerSumi: $('#HundeggerSumi').is(':checked') ? 1 : 0,
        HundeggerTime: $('#HundeggerTime').val(),
        //Page 3.
        BukkenFileShurui: $('input[name="UpFileOption"]:radio:checked').val(),
        //Page 4.
        BukkenComment: $('#BukkenComment').val(),
        HiddenUpdateDateTime: $('#HiddenUpdateDateTime').val(),
        UserID: $('#user-id').text(),
    };

    var result = calltoApiController(url_SaveData, model);
    if (!result) {
        return false;
    }
    if (result.MessageID) {
        showMessage(result);
        return false;
    }

    showMessage(eMode == 'Delete' ? 'I102' : 'I101', function () {
        if (eMode == 'New') {
            window.location.reload();
        }
        else {
            location.href = url_previousPage;
        }
    });
}



//upload files -------------------->
function clearFileInfo() {
    $('#fileStatus').nextAll().remove();
    formData = new FormData();
    fileCount = 0;
}

function sendFileToServer(fileData, status) {
    fileData.delete('BukkenNO');
    fileData.delete('BukkenFileShurui');
    fileData.delete('UserID');

    fileData.append('BukkenNO', txtBukkenNO.val());
    fileData.append('BukkenFileShurui', $('input[name="UpFileOption"]:radio:checked').val());
    fileData.append('UserID', $('#user-id').text());

    calltoApiController_FileUploadHandle(url_uploadFiles, fileData,
        function (result) { //success function
            clearFileInfo();

            if (!result) return;
            if (result.MessageID) {
                showMessage(result);
                return;
            }
            createBukkenFileTable();
        },
        function () { //error function
            clearFileInfo();
        },
        function (percent) { //progress bar function
            status.setProgress(percent);
        });
}

function createFileStatusbar() {
    this.statusbar = $('<div class="statusbar"></div>');
    this.progressBar = $('<div class="progressBar"><div></div></div>').appendTo(this.statusbar);

    this.setProgress = function (percent) {
        var progressBarWidth = percent * this.progressBar.width() / 100;
        this.progressBar.find('div').animate({ width: progressBarWidth }, 10).html(percent + "% ");
    }
}

function dropFiles(files, obj) {
    for (var i = 0; i < files.length; i++) {
        fileCount++;
        var key = 'file' + fileCount.toString();
        formData.append(key, files[i]);
    }
    var status = new createFileStatusbar();
    obj.after(status.statusbar);
    sendFileToServer(formData, status);
}




//download files
function downloadFiles() {

    var rowcsv = "";
    $('#tblBukkenFile input[type=checkbox]:checked').each(function () {
        rowcsv += $(this).data('bukkenfilerows') + ",";
    });

    if (rowcsv == "") {
        showMessage('E289');
        return;
    }

    var model = {
        BukkenNO: txtBukkenNO.val(),
        BukkenFileRowsCsv: rowcsv.slice(0, -1)
    }

    const link = document.getElementById("downloadBukkenFile");
    link.href = url_downloadFiles + querySerialize(model);
    link.click();

    $('#tblBukkenFile input[type=checkbox]:checked').prop('checked', false);
}
