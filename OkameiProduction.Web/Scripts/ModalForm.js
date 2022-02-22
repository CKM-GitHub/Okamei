//ModalForm.js

var ModalForm = {};
ModalForm.Close = function () {
    $('.js-modal-close').click();
}

$(document).ready(function () {

    var modalcontent;
    var info;

    function getModalSettings(target) {

        var url;
        var funcInitialize;
        var funcFinalize;

        // ----->
        if (target == 'HiuchiSubEntry') {
            url = gApplicationPath + '/InputBukkenShousai/HiuchiSubEntry';
            funcInitialize = initialize_Hiuchi;
            funcFinalize = finalize_Hiuchi;
        }

        if (target == 'TekakouSubEntry') {
            url = gApplicationPath + '/InputBukkenShousai/TekakouSubEntry';
            funcInitialize = initialize_Tekakou;
            funcFinalize = finalize_Tekakou;
        }
        //<-----

        var model = {
            BukkenNO: txtBukkenNO.val(),
            BukkenName: txtBukkenName.val(),
        };
        if (model.BukkenNO == "") return;

        return { model, url, funcInitialize, funcFinalize };
    }

    $('.js-modal-open').each(function () {
        $(this).on('click', function () {

            var target = $(this).data('target');
            info = getModalSettings(target);
            if (!info) return;

            showLoadingMessage();

            $.post(info.url, info.model, function (content) {
                modalcontent = $(content);
                modalcontent.appendTo('body');
                closeLoadingMessage();

                var modal = document.getElementById(target);
                $(modal).fadeIn();
                $('body').css('overflow-y', 'hidden');

                if (info.funcInitialize) info.funcInitialize();
                return false;
            });

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
