$(function() {
    $("input[name='product-type']").click(function () {
        $('#key-price').css('display', ($(this).val() === 'a') ? 'block':'none');
        $('#hard-case-price').css('display', ($(this).val() === 'b') ? 'block':'none');
    });
});

function goBack() {
    window.history.back();
}