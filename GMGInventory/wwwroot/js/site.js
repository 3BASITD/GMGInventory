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

    $(document).ajaxStart(function (event, jqxhr, settings) {
    });

    $(document).ajaxSend(function (event, jqxhr, settings) {
        $.LoadingOverlay("show", {
            image: "",
            fontawesome: "fa fa-compact-disc fa-spin"
        });
    });

    $(document).ajaxSuccess(function (event, jqxhr, settings) {
    });

    $(document).ajaxComplete(function (event, jqxhr, settings) {
        $.LoadingOverlay("hide");
        reloadToolTip();
    });

    $(document).ajaxStop(function (event, jqxhr, settings) {
        $.LoadingOverlay("hide");
        reloadToolTip();
    });

    $.validator.addMethod('lessThan', function (value, element, param) {
        return this.optional(element) || parseInt(value) < parseInt($(param).val());
    }, 'Should not be greater than');

    $.validator.addMethod('notEqualTo', function (value, element, param) {
        return this.optional(element) || parseInt(value) !== parseInt($(param).val());
    }, 'Should not be equal');
});

function reloadToolTip() {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl, {
        trigger: "hover"
    }));
}

function resetToolTip() {
    let tooltip = document.getElementsByClassName("tooltip");
    //tooltip[0].parentNode.removeChild(tooltip[0]);
    $.each(tooltip, function (i, item) {
        item.parentNode.removeChild(item);
    });
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


// Use for tabs with datatables content
var tabLoaderDisplayed = [];

function ShowTabLoader(mod) {
    if (tabLoaderDisplayed.length === 0) {
        $.LoadingOverlay("show", {
            image: "",
            fontawesome: "fa fa-cog fa-spin"
        });
    }
    tabLoaderDisplayed.push(mod);
}

function HideTabLoader(mod) {
    tabLoaderDisplayed = tabLoaderDisplayed.filter(s => s !== mod);
    if (tabLoaderDisplayed.length === 0) {
        $.LoadingOverlay("hide");
    }
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

function select2_search($el, term) {
    $el.select2('open');

    // Get the search box within the dropdown or the selection
    // Dropdown = single, Selection = multiple
    var $search = $el.data('select2').dropdown.$search || $el.data('select2').selection.$search;
    // This is undocumented and may change in the future

    $search.val(term);
    $search.trigger('keyup');

    // Timer for delayed trigger while waiting ajax to load
    setTimeout(function () { $('.select2-results__option').trigger("mouseup"); }, 500);
}

function ReplaceEmptyString(str, replacement) {
    if (str == null || str.trim() == "")
        return replacement;
    return str;
}