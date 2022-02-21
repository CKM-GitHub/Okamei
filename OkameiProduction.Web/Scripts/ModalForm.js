//ModalForm.js

var ModalForm = {};
ModalForm.Close = function () {
    $('.js-modal-close').click();
}

$(document).ready(function () {

    var modalcontent;

    function getModalSettings(target) {

        var url;
        var funcInitialize;

        // ----->
        if (target == 'HiuchiSubEntry') {
            url = gApplicationPath + '/InputBukkenShousai/HiuchiSubEntry';
            funcInitialize = initialize_Hiuchi;
        }

        if (target == 'TekakouSubEntry') {
            url = gApplicationPath + '/InputBukkenShousai/TekakouSubEntry';
            funcInitialize = initialize_Tekakou;
        }
        //<-----

        var model = {
            BukkenNO: txtBukkenNO.val(),
            BukkenName: txtBukkenName.val(),
        };
        if (model.BukkenNO == "") return;

        return { model, url, funcInitialize };
    }

    $('.js-modal-open').each(function () {
        $(this).on('click', function () {

            var target = $(this).data('target');
            var info = getModalSettings(target);
            if (!info) return;

            $.post(info.url, info.model, function (content) {
                modalcontent = $(content);
                modalcontent.appendTo('body');

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
        $('body').css('overflow-y', 'auto');
        return false;
    });
});
