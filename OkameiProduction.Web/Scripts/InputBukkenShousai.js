//InputBukkenShousai.js

function addEvents() {
    $('#btnReturn').click(function () {
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
        //setDropDownList('#KoumutenName', url_getKoumutenListItems, key);
        setAutocomplete('ul[aria-labelledby="KoumutenName"]', url_getKoumutenListItems, key);
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
        showBukkenFileTable();
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
            showBukkenCommentTable();
        }
    });

    //jquery on ---------->
    //autocomplete
    $(document).on('ontouched click', '.autocomplete', function () {
        var text = $(this).data('autocomplete');
        var target = $(this).data('target');
        $('input[name="' + target + '"]').val(text).focus();
    });

    //drag area
    $('#dragDropArea').on('dragenter', function (e) {
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

    clearFileInfo();

    showBukkenFileTable();
    showBukkenCommentTable();

    if (eMode != 'New') {
        btnSet.hide();
        txtSitenCD.removeClass('size39').addClass('size50');
    }
    if (eMode == 'Edit') {
        setAutocomplete('ul[aria-labelledby="KoumutenName"]', url_getKoumutenListItems, txtSitenCD.val());
    }
    if (eMode == 'Delete') {
        setDisabledAll('#main');
        btnSave.text('削除');
    }
}

function showBukkenFileTable() {
    var model = {
        BukkenNO: txtBukkenNO.val(),
        BukkenFileShurui: $('input[name="DownFileOption"]:radio:checked').val(),
    }
    var data = calltoApiController(url_getBukkenFile, model);
    if (!data) return;
    if (data.MessageID) {
        showMessage(data);
        return;
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

    option.paging = false;
    option.info = false;

    bindDataTables($('#tblBukkenFile'), option);
}

function showBukkenCommentTable() {
    var model = {
        BukkenNO: txtBukkenNO.val(),
    }
    var data = calltoApiController(url_getBukkenComment, model);
    if (!data) return;
    if (data.MessageID) {
        showMessage(data);
        return;
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

    option.paging = false;
    option.info = false;

    bindDataTables($('#tblBukkenComment'), option);
}

function deleteBukkenFile(row) {
    var model = {
        BukkenNO: txtBukkenNO.val(),
        BukkenFileRows: row,
    }
    var data = calltoApiController(url_deleteBukkenFile, model);
    if (!data) return;
    if (data.MessageID) {
        showMessage(data);
        return;
    }
    showBukkenFileTable();
}

function deleteBukkenComment(row) {
    var model = {
        BukkenNO: txtBukkenNO.val(),
        BukkenCommentRows: row,
    }
    var data = calltoApiController(url_deleteBukkenComment, model);
    if (!data) return;
    if (data.MessageID) {
        showMessage(data);
        return;
    }
    showBukkenCommentTable();
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

    var ret = calltoApiController(url_getBukkenNO, txtSitenCD.val());
    if (!ret) {
        return;
    }
    if (ret.MessageID) {
        showMessage(ret);
        return;
    }
    if (ret.NewBukkenNO) {
        txtBukkenNO.val(ret.NewBukkenNO);
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
        UpdateDatetime: $('#UpdateDatetime').val(),
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
        HiddenUpdateDatetime: $('#HiddenUpdateDatetime').val(),
        UserID: $('#user-id').text(),
    };

    var ret = calltoApiController(url_SaveData, model);
    if (!ret) {
        return false;
    }
    if (ret.MessageID) {
        showMessage(ret);
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



//autocomplete -------------------->
function setAutocomplete(selector, url, key) {
    var ddl = $(selector);
    var target = ddl.attr('aria-labelledby');
    ddl.children().remove();

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
            var data = item.DisplayText;
            ddl.append('<li class="autocomplete" data-autocomplete="' + data + '" data-target="' + target + '"><a href="#">' + data + '</a></li>');
        });
    }
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

    callSendFileToServer(url_uploadFiles, fileData,
        function (percent) {
            status.setProgress(percent);
        },
        function (result) {
            clearFileInfo();

            if (!result) return;
            if (result.MessageID) {
                showMessage(result);
                return;
            }
            showBukkenFileTable();
        },
        function () {
            clearFileInfo();
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
    $('.table input[type=checkbox]:checked').each(function () {
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
}
