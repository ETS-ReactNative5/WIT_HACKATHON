$(document).ready(function () {
    // get current URL path and assign 'active' class
    var pathname = window.location.pathname;
    $('.nav > li > a[href="' + pathname + '"]').parent().addClass('active');
    $('#collapseUtilities  > .collapse-inner > a[href = "' + pathname + '"]').addClass('active').parent().parent().addClass('show').prev().removeClass('collapsed');
    $('#collapseTreeUtilities  > .collapse-inner > a[href = "' + pathname + '"]').addClass('active').parent().parent().addClass('show').prev().removeClass('collapsed');
    $('#collapseTree2Utilities  > .collapse-inner > a[href = "' + pathname + '"]').addClass('active').parent().parent().addClass('show').prev().removeClass('collapsed');
    $('.sidebar.toggled > .nav-item > .collapse').removeClass('collapsed').removeClass('show');
    $('#inlisted-alert').hide();
});