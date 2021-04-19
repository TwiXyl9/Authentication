// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
$("#CheckAll").change(function () {
    $('input[id="simple_check"]').prop('checked', this.checked);
});
// Write your JavaScript code.
