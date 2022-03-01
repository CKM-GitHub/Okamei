//ModalForm.js

var ModalForm = {};
ModalForm.Close = function () {
    $('.js-modal-close').click();
}

$(document).ready(function () {

    function getModalSettings(target) {

        var title = target;
        var url, funcInitialize, funcFinalize;

        if (target == 'HiuchiSubEntry') {
            title = '火打';
            url = gApplicationPath + '/InputBukkenShousai/HiuchiSubEntry';
            funcInitialize = initialize_Hiuchi;
            funcFinalize = finalize_Hiuchi;
        }

        if (target == 'TekakouSubEntry') {
            title = '手加工入力';
            url = gApplicationPath + '/InputBukkenShousai/TekakouSubEntry';
            funcInitialize = initialize_Tekakou;
            funcFinalize = finalize_Tekakou;
        }

        if (target == 'MoulderSubEntry') {
            title = '加工指示（モルダー）';
            url = gApplicationPath + '/InputBukkenShousai/MoulderSubEntry';
            funcInitialize = initialize_Moulder;
            funcFinalize = finalize_Moulder;
        }

        var model = {
            BukkenNO: txtBukkenNO.val(),
            BukkenName: txtBukkenName.val(),
        };
        if (model.BukkenNO == "") {
            showMessage('E288');
            return;
        }

        return { title, url, model, funcInitialize, funcFinalize };
    }


    var modalcontent;
    var info;

    $('.js-modal-open').each(function () {
        $(this).on('click', function () {

            var target = $(this).data('target');
            info = getModalSettings(target);
            if (!info) return;

            if (info.url) {
                showLoadingMessage();

                $.post(info.url, info.model)
                    .done(function (content) {
                        modalcontent = $(content);
                        modalcontent.appendTo('body');

                        var modal = $('.js-modal');
                        modal.attr('id', target);
                        $('.modal-title-text').text(info.title);

                        closeLoadingMessage();
                        $(modal).fadeIn();
                        $('body').css('overflow-y', 'hidden');
                        if (info.funcInitialize) info.funcInitialize();
                        return false;
                    })
                    .fail(function (jqXHR, textStatus, errorThrown) {
                        closeLoadingMessage();
                        alert(jqXHR.status + ':' + jqXHR.statusText);
                    });
            }
            else {
                var modal = document.getElementById(target);
                $(modal).fadeIn();
                $('body').css('overflow-y', 'hidden');
                if (info.funcInitialize) info.funcInitialize();
                return false;
            }

            return false;
        });
    });

    $(document).on('click', '.js-modal-close', function () {
        $('.js-modal').fadeOut();
        if (modalcontent) modalcontent.remove();
        if (info.funcFinalize) info.funcFinalize();
        $('body').css('overflow-y', 'auto');
        return false;
    });
});
