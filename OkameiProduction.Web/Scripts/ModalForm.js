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
        var funcfinalize;

        // ----->

        if (target == 'HiuchiSubEntry') {
            url = gApplicationPath + '/InputBukkenShousai/HiuchiSubEntry';
            funcInitialize = initialize_Hiuchi;
        }

        //<-----

        var model = {
            BukkenNO: txtBukkenNO.val(),
            BukkenName: txtBukkenName.val(),
        };
        if (model.BukkenNO == "") return;

        return { model, url, funcInitialize, funcfinalize };
    }

    $('.js-modal-open').each(function () {
        $(this).on('click', function () {

            var target = $(this).data('target');
            var option = getModalSettings(target);
            if (!option) return;

            $.post(option.url, option.model, function (content) {
                modalcontent = $(content);
                modalcontent.appendTo('body');

                var modal = document.getElementById(target);
                $(modal).fadeIn();
                $('body').css('overflow-y', 'hidden');

                option.funcInitialize();
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
