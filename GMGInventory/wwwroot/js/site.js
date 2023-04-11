$(document).ready(function () {
    $('.select2option').select2({
        allowClear: true,
        placeholder: {
            id: "",
            text: "<empty>"
        },
        width: 'resolve'
    });

    $("select").on("select2:close", function (e) {
        if ($(this).closest("form").length)
            $(this).valid();
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

    var ajax_time = Date.now();

    $(document).ajaxStart(function (event, jqxhr, settings) {
        ajax_time = Date.now();
    });

    $(document).ajaxSend(function (event, jqxhr, settings) {
        $.LoadingOverlay("show", {
            image: "",
            fontawesome: "fa fa-compact-disc fa-spin",
            progress: true
        });
    });

    $(document).ajaxSuccess(function (event, jqxhr, settings) {
    });

    $(document).ajaxComplete(function (event, jqxhr, settings) {
        $.LoadingOverlay("hide");
        reloadInputNumberRule();
    });

    $(document).ajaxStop(function (event, jqxhr, settings) {
        $.LoadingOverlay("hide");
        reloadInputNumberRule();

        var ajax_run_time = Date.now() - ajax_time;
        var ajax_run_hh = Math.floor(ajax_run_time / 1000 / 60 / 60);
        ajax_run_time -= ajax_run_hh * 1000 * 60 * 60;
        var ajax_run_mm = Math.floor(ajax_run_time / 1000 / 60);
        ajax_run_time -= ajax_run_mm * 1000 * 60;
        var ajax_run_ss = Math.floor(ajax_run_time / 1000);
        ajax_run_time -= ajax_run_ss * 1000;
        console.log("Ajax Load Time > " + String(ajax_run_hh).padStart(2, "0") + ":" + String(ajax_run_mm).padStart(2, "0") + ":" + String(ajax_run_ss).padStart(2, "0") + ":" + String(ajax_run_time).padStart(3, "0"));
    });

    $.validator.addMethod('lessThanOrEqual', function (value, element, param) {
        return this.optional(element) || parseInt(value) <= parseInt($(param).val());
    }, 'Should not be greater than');

    $.validator.addMethod('notEqualTo', function (value, element, param) {
        return this.optional(element) || parseInt(value) !== parseInt($(param).val());
    }, 'Should not be equal');

    $.validator.addMethod('isNotZero', function (value, element, param) {
        return this.optional(element) || parseInt(value) !== 0;
    }, 'Should not be 0');
});

function toastrNoDuplicate(message, title, type) {
    var hasDuplicate = false;
    $.each($(".toast-message"), function (i, v) {
        if (message == $(v).text()) {
            hasDuplicate = true;
        }
    });
    if (!hasDuplicate) {
        if (type == "success") toastr.success(message, title);
        if (type == "error") toastr.error(message, title);
        if (type == "warning") toastr.warning(message, title);
        if (type == "info") toastr.info(message, title);
    }
}

function validateShowErrors(m, l) {
    $.each(l, function (i, v) {
        toastrNoDuplicate(v.message, "Required field &lt;" + $(v.element.labels[0]).text() + "&gt;", "warning");
    });
    this.defaultShowErrors();
}

function userIsInRole(role) {
    var isInRole = false;
    $(".current-user-role").each(function () {
        if ($(this).val() === role)
            isInRole = true;
    });
    return isInRole;
}

function reloadInputNumberRule() {
    $("input[step='0.01']").on("focusout", function () {
        $(this).val(Round2Decimal($(this).val()));
    });

    $("input[type='number']").on("focusout", function () {
        if ($(this).val() == "")
            $(this).val("0");
    });

    $("input[type='number']").on("keyup", function () {
        if (parseFloat($(this).val()) > parseFloat($(this).attr("max")))
            $(this).val($(this).attr("max"));
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

function DatatablesSearchOnEnter(table, table_elem) {
    $(table + ' .dataTables_filter input').unbind();
    $(table + ' .dataTables_filter input').keyup(function (e) {
        if (e.keyCode == 13) {
            table_elem.search($(this).val()).draw();
        }
    });
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

var temp_xhr;
function jQueryCommonError(xhr, status, error) {
    if (xhr.status == 503) {
        toastr.warning("Looks like you got signed out! Please reload the page", xhr.statusText);
    } else if (xhr.status == 403) {
        toastr.warning("You are not allowed to do this action.", xhr.statusText);
    } else if (error.trim() != "") {
        toastr.error(error, "Error");
    } else {
        toastr.warning(xhr.status, xhr.statusText);
    }
    hideLoader();
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

function Round2Decimal(num) {
    return parseFloat(Math.round(parseFloat(num) * 100) / 100, 2).toFixed(2);
}

function FormatStringDecimal(num) {
    return parseFloat(num).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ","); // add comma every 3 digits
}

function FormatStringInteger(num) {
    return parseFloat(num).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ","); // add comma every 3 digits
}

function DateForInput(date) {
    var d = new Date(Date.parse(date));
    var day = d.getDate();
    var month = d.getMonth() + 1;
    var year = d.getFullYear();

    if (month < 10) month = "0" + month;
    if (day < 10) day = "0" + day;

    return year + "-" + month + "-" + day;
}

function cellDTNumberValue(i) {
    if (typeof i == 'string') {
        if (i.includes("input"))
            return $(i).val() * 1;
        else
            return i.replace(/[\$,]/g, '') * 1;
    } else if (typeof i == 'number') {
        return i;
    } else {
        return 0;
    }
}

function columnDTTotalInteger(api, column) {
    var total = api.column(column).data().reduce(function (a, b) {
        return cellDTNumberValue(a) + cellDTNumberValue(b);
    }, 0);

    $(api.column(column).footer()).html(FormatStringInteger(total));
}

function columnDTTotalDecimal(api, column) {
    var total = api.column(column).data().reduce(function (a, b) {
        return cellDTNumberValue(a) + cellDTNumberValue(b);
    }, 0);

    $(api.column(column).footer()).html(FormatStringDecimal(total));
}

function formatDateEn(date) {
    return date.toLocaleDateString('en-us', { day: 'numeric', month: 'long', year: 'numeric' });
}
