$(document).ready(function () {
    reloadToolTip();

    $('.select2option').select2({
        allowClear: true,
        placeholder: {
            id: "",
            text: "<empty>"
        },
        width: 'resolve'
    });

    $("form").attr("autocomplete", "off");

    $('#logout-link').click(function (e) {
        //swal({
        //    title: "Logging out",
        //    text: "You are about to log out, are you sure you want to continue?",
        //    icon: "info",
        //    buttons: ["Cancel", "Yes"],
        //}).then((confirm) => {
        //    if (confirm) {
        $('#logoutForm').submit();
        //    }
        //});
    });

    $(document).ajaxSend(function (event, jqxhr, settings) {
        $.LoadingOverlay("show", {
            image: "",
            fontawesome: "fa fa-compact-disc fa-spin"
        });
    });

    $(document).ajaxComplete(function (event, jqxhr, settings) {
        $.LoadingOverlay("hide");
        reloadToolTip();
    });
});

function reloadToolTip() {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));
}

function showLoader() {
    $.LoadingOverlay("show", {
        image: "",
        fontawesome: "fa fa-cog fa-spin"
    });
}

function hideLoader() {
    $.LoadingOverlay("hide");
}


function jQueryCommonError(xhr, status, error) {
    if (xhr.status == 503) {
        toastr.warning("Looks like you got signed out! Please reload the page", xhr.statusText);
    } else if (xhr.status == 403) {
        toastr.warning("You are not allowed to do this action.", xhr.statusText);
    } else {
        toastr.error(error);
    }
}

jQuery.fn.extend({
    hideOverlay: function () {
        if (!this.hasClass("d-none")) { this.addClass("d-none"); }
        if (this.hasClass("d-flex")) { this.removeClass("d-flex"); }
    },
    showOverlay: function () {
        if (this.hasClass("d-none")) { this.removeClass("d-none"); }
        if (!this.hasClass("d-flex")) { this.addClass("d-flex"); }
    }
});