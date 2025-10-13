$(document).ready(function () {
    $.fn.dataTable.ext.errMode = function (settings, helpPage, message) {
        var today = new Date();
        var log = today.toISOString().replace('T', ' ').replace('Z', '').split('.')[0];
        console.error('%c %s %c %s %s', 'background-color:lightblue;font-weight:bold;color:black;', log, 'background-color:transparent;', "DataTables error:", message);
    };

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "progressBar": true,
        "positionClass": "toast-bottom-full-width",
        "preventDuplicates": true,
        "showDuration": "500",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "5000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "slideDown",
        "hideMethod": "slideUp"
    };

    $.ajaxSetup({
        error: function (xhr, status, error) {
            //if (xhr.status === 0) {
            //    toastr.error("Connection refused. Please check your server or network.", "Error");
            //} else
            if (xhr.status === 400)
                toastr.error("The request was invalid. Please try again.", "400 Bad Request");
            else if (xhr.status === 401)
                toastr.error("Please log in to access this action.", "401 Unauthorized");
            else if (xhr.status === 403)
                toastr.error("You don't have permission to do this action.", "403 Forbidden");
            else if (xhr.status === 404)
                toastr.error("Function could not be found", "404 Not Found");
            else if (xhr.status === 500)
                toastr.error("Something went wrong on our end. Please try again later.", "500 Internal Server Error");
            else if (xhr.status === 502)
                toastr.error("We’re having trouble connecting to the server. Please try again soon.", "502 Bad Gateway");
            else if (xhr.status === 503)
                toastr.error("The service is temporarily unavailable. Please check back shortly.", "503 Service Unavailable");
            else {
                toastr.error("There seems to be a problem loading some data. Please reload the page and try again.", "Unknown Error");
                console.error("Unknown error encountered while loading ajax");
                console.error("xhr");
                console.log(xhr);
                console.error("status");
                console.log(status);
                console.error("error");
                console.log(error);
            }
            hideLoader();
        }
    });

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
    var active_select_search = $(".select2-search__field").filter(":focus");;

    $(document).ajaxStart(function (event, jqxhr, settings) {
        ajax_time = Date.now();

        active_select_search = $(".select2-search__field").filter(":focus");
        active_select_search.blur();
    });

    $(document).ajaxSend(function (event, jqxhr, settings) {
        var msg = settings.loaderMessage || "";

        $.LoadingOverlay("show", {
            image: "",
            fontawesome: "fa fa-compact-disc fa-spin",
            custom: $("<div class=\"loading-message\">" + msg + "</div>"),
            textColor: "#333",
            textResizeFactor: 0.2,
            textAutoResize: true,
            progress: true,
            zIndex: 2147483645
        });
    });

    $(document).ajaxSuccess(function (event, jqxhr, settings) {
    });

    var requestURL = "";
    $(document).ajaxComplete(function (event, jqxhr, settings) {
        $.LoadingOverlay("hide");
        reloadInputNumberRule();
        requestURL = settings.url;
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

        var today = new Date();
        var log = today.toISOString().replace('T', ' ').replace('Z', '').split('.')[0];
        var logTime = String(ajax_run_hh).padStart(2, "0");
        logTime += ":" + String(ajax_run_mm).padStart(2, "0");
        logTime += ":" + String(ajax_run_ss).padStart(2, "0");
        logTime += "." + String(ajax_run_time).padStart(3, "0");
        console.info('%c %s %c %s %s %s', 'background-color:lightblue;font-weight:bold;color:black;', log, 'background-color:transparent;', "Ajax Load Time: ", logTime, requestURL);

        if (active_select_search.length > 0)
            active_select_search.focus();
    });

    $.validator.addMethod('lessThanOrEqual', function (value, element, param) {
        var val1 = value.includes(",") ? removeCommaOfNumber(value) : parseFloat(value);
        var val2 = $(param).val().includes(",") ? removeCommaOfNumber($(param).val()) : parseFloat($(param).val());
        return this.optional(element) || val1 <= val2;
    }, 'Should not be greater than');

    $.validator.addMethod('greaterThanOrEqualDate', function (value, element, param) {
        return new Date(value) >= new Date($(param).val());
    }, 'End date must be after the start date');

    $.validator.addMethod('notEqualTo', function (value, element, param) {
        return this.optional(element) || parseFloat(value) !== parseFloat($(param).val());
    }, 'Should not be equal');

    $.validator.addMethod('isNotZero', function (value, element, param) {
        return this.optional(element) || parseInt(value) !== 0 || parseFloat(value) !== 0;
    }, 'Should not be 0');

    $.validator.addMethod('rangeForMasked', function (value, element, param) {
        var val = removeCommaOfNumber(value);
        return this.optional(element) || (param[0] <= val && val <= param[1]);
    }, "Please enter a value between {0} and {1}.");

    $.validator.addMethod("summernoteRequired", function (value, element) {
        if (typeof value !== "string") return false;
        var text = $('<div>').html(value).text().trim();
        //var code = $(element).summernote('code');
        //var text = $('<div>').html(code).text().trim();
        return text.length > 0;
    }, "This field is required");

    $.validator.addMethod("summernoteMinLength", function (value, element, param) {
        if (typeof value !== "string") return false;
        var text = $('<div>').html(value).text().trim();
    //var code = $(element).summernote('code');
    //var text = $('<div>').html(code).text().trim();
        return text.length >= param;
    }, "Please enter at least {0} characters");

    $.validator.addMethod("summernoteMaxLength", function (value, element, param) {
        if (typeof value !== "string") return false;
        var text = $('<div>').html(value).text().trim();
    //var code = $(element).summernote('code');
    //var text = $('<div>').html(code).text().trim();
        return text.length <= param;
    }, "Please enter no more than {0} characters");

    //$('.mask-decimal').mask('9,999,999.00', {
    //    reverse: true
    //});
    $(".mask-decimal").on("change", function () {
        if ($(this).val().includes(".")) {
            $(this).val($.number($(this).val(), 2));
        } else {
            $(this).val($.number($(this).val(), 0));
        }
    });

    $(window).on("scroll", function () {
        checkScroll();
    });

    $(window).on("resize", function () {
        checkScroll();
    });

    var col_report = 12 / $(".col-report").length;
    if (!isFinite(col_report) || isNaN(col_report)) {
        col_report = 12;
    }
    $(".col-report").removeClass("col-md-4 col-md-6 col-md-12");
    $(".col-report").addClass("col-md-" + col_report);
    //$('.modal').on('shown.bs.modal', function () {
    //    checkFooterStickiness($(this));

    //    $(this).off('scroll').on('scroll', function () {
    //        checkFooterStickiness($(this));
    //    });
    //});
});

function ToastrAndConsoleError(title, message) {
    var today = new Date();
    var log = today.toISOString().replace('T', ' ').replace('Z', '').split('.')[0];
    console.error('%c %s %c %s %s', 'background-color:lightblue;font-weight:bold;color:black;', log, 'background-color:transparent;', title, message);
    toastr.error(message, title);
}

$.validator.setDefaults({
    errorClass: 'text-danger text-xs fw-lighter fst-italic m-0',
    highlight: function (element) {
        $(element).addClass('is-invalid');
    },
    unhighlight: function (element) {
        $(element).removeClass('is-invalid');
    },
    errorPlacement: function (error, element) {
        if (element.hasClass("select2-hidden-accessible")) {
            error.insertAfter(element.siblings(".select2"));
        } else if (element.hasClass("summernote")) {
            error.insertAfter(element.next('.note-editor'));
        } else {
            error.insertAfter(element);
        }
    },
    invalidHandler: function (event, validator) {
        var errors = validator.errorList;
        var msg = "";

        $.each(errors, function (i, v) {
            msg += "<strong>" + $(v.element).attr("name") + "</strong>: " + v.message + "<br />";
        });

        if (msg) {
            toastr.warning(msg, "Form Validation");
        }
    }
});
//function checkFooterStickiness($modal) {
//    var $footer = $modal.find('.modal-footer');
//    var footerOffset = $footer.offset().top + $footer.outerHeight();
//    var windowHeight = $(window).height() + 15;
//    if (footerOffset >= windowHeight) {
//        $footer.addClass('sticky-active');
//    } else {
//        $footer.removeClass('sticky-active');
//    }
//}

function removeCommaOfNumber(value) {
    return parseFloat(value.replace(/[\$,]/g, '') * 1);
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

var startTime;

function showLoader(msg) {
    hideReloader();

    $.LoadingOverlay("show", {
        image: "",
        fontawesome: "fa fa-cog fa-spin",
        text: msg || "",
        textColor: "#333",
        textResizeFactor: 0.2,
        textAutoResize: true,
        zIndex: 2147483645
    });

    startTime = new Date();
}

function hideLoader() {
    if (startTime) {
        const endTime = new Date();
        const timeDifference = endTime - startTime;

        const milliseconds = timeDifference % 1000;
        const totalSeconds = Math.floor(timeDifference / 1000);
        const seconds = totalSeconds % 60;
        const totalMinutes = Math.floor(totalSeconds / 60);
        const minutes = totalMinutes % 60;
        const hours = Math.floor(totalMinutes / 60);

        var log = endTime.toISOString().replace('T', ' ').replace('Z', '').split('.')[0];
        console.info('%c %s %c %s %s', 'background-color:lightblue;font-weight:bold;color:black;', log, 'background-color:transparent;', 'Loading finished. Time taken:', `${hours}h ${minutes}m ${seconds}s ${milliseconds}ms`);
        startTime = null;
    }
    $.LoadingOverlay("hide");
}

window.onerror = function (message, source, lineno, colno, error) {
    displayReloader();
};

window.onunhandledrejection = function (event) {
    displayReloader();
};

function displayReloader() {
    setTimeout(function () {
        if (startTime) {
            $(".reloader").slideDown(1000);
        }
    }, 1000);
}

function hideReloader() {
    $(".reloader").slideUp(100);
}

// Use for tabs with datatables content
var batchLoaderDisplayed = [];
var batchStartTime;

function showBatchLoader(mod) {
    if (batchLoaderDisplayed.length === 0) {
        $.LoadingOverlay("show", {
            image: "",
            fontawesome: "fa fa-cog fa-spin",
            zIndex: 2147483645
        });

        batchStartTime = new Date();
    }
    batchLoaderDisplayed.push(mod);
}

function hideBatchLoader(mod) {
    batchLoaderDisplayed = batchLoaderDisplayed.filter(s => s !== mod);
    if (batchLoaderDisplayed.length === 0) {
        if (batchStartTime) {
            const endTime = new Date();
            const timeDifference = endTime - batchStartTime;

            const milliseconds = timeDifference % 1000;
            const totalSeconds = Math.floor(timeDifference / 1000);
            const seconds = totalSeconds % 60;
            const totalMinutes = Math.floor(totalSeconds / 60);
            const minutes = totalMinutes % 60;
            const hours = Math.floor(totalMinutes / 60);

            var log = endTime.toISOString().replace('T', ' ').replace('Z', '').split('.')[0];
            console.info('%c %s %c %s %s', 'background-color:lightblue;font-weight:bold;color:black;', log, 'background-color:transparent;', 'Batch loading finished. Time taken:', `${hours}h ${minutes}m ${seconds}s ${milliseconds}ms`);
            batchStartTime = null;
        }
        $.LoadingOverlay("hide");
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

$.fn.select2.defaults.set("ajax", {
    delay: 2000,
    cache: true,
    dataType: "json",
    type: "POST",
    data: function (params) {
        return {
            search: params.term,
            page: params.page || 1
        };
    },
    processResults: function (data, params) {
        params.page = params.page || 1;
        return {
            results: data.items,
            pagination: {
                more: (params.page * 50) < data.total_count
            }
        };
    }
});

/*
$("#sample-url-query").select2({
    ajax: {
        url: "",
        data: function (params) {
            let base = $.fn.select2.defaults.defaults.ajax.data(params);
            return Object.assign(base, {
                tempId: 1
            });
        }
    }
});
*/

function ReplaceEmptyString(str, replacement) {
    if (str == null || str.trim() == "")
        return replacement;
    return str;
}
function DeductDiscount(amount, discount) {
    return Round2Decimal(((100 - discount) * amount) / 100);
}

function Round2Decimal(num) {
    return parseFloat(Math.round(parseFloat(num) * 100) / 100, 2).toFixed(2);
}

function FormatStringDecimal(num) {
    if (isNaN(num)) {
        return "0.00";
    } else {
        return parseFloat(num).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ","); // add comma every 3 digits
    }
}

function FormatStringInteger(num) {
    if (isNaN(num)) {
        return "0";
    } else {
        return parseFloat(num).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ","); // add comma every 3 digits
    }
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

function columnDTTotalDecimal(api, column, prefix) {
    var total = api.column(column).data().reduce(function (a, b) {
        return cellDTNumberValue(a) + cellDTNumberValue(b);
    }, 0);

    $(api.column(column).footer()).html((prefix || "") + FormatStringDecimal(total));
}

function formatDateEn(date) {
    return date.toLocaleDateString('en-us', { day: 'numeric', month: 'long', year: 'numeric' });
}

jQuery.extend(jQuery.fn.dataTableExt.oSort, {
    "date-g-pre": function (a) {
        return a == "" ? new Date() : new Date(a);
    },
    "date-g-asc": function (a, b) {
        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },
    "date-g-desc": function (a, b) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    }
});

checkScroll();
function checkScroll() {
    //if ($(window).width() > 992) {
    //var navHeight = $(".main-header").height();
    //if ($(window).scrollTop() > navHeight) {
    //    $(".main-header").addClass("to-stick");
    //    $("body").css("margin-top", navHeight + "px");
    //} else {
    //    $(".main-header").removeClass("to-stick");
    //    $("body").css("margin-top", "0");
    //}
    //if ($(window).scrollTop() > navHeight * 1.2) {
    //    $(".main-header").addClass("nav-sticked");
    //} else {
    //    $(".main-header").removeClass("nav-sticked");
    //}
    //}
}

function getDTParams(table) {
    var info = table.page.info();
    var order = table.order();
    var search = table.search();
    var pageSize = table.page.len();
    var skip = info.start;

    var sortColumnIndex = order[0][0];
    var sortDirection = order[0][1];

    var sortColumnName = table.settings()[0].aoColumns[sortColumnIndex].name;

    return {
        SortColumn: sortColumnName,
        SortColumnDirection: sortDirection,
        SearchValue: search,
        PageSize: pageSize,
        PageSkip: skip
    };
}

function imagePreview(that, previewerId, className, idName) {
    var cls = className || "";
    var id = idName || "";
    let files = that.files;
    document.getElementById(previewerId).innerHTML = '';

    for (let i = 0; i < files.length; i++) {
        let imager = document.createElement("img");
        imager.src = URL.createObjectURL(files[i]);
        imager.classList.add("w-100");
        imager.classList.add("mt-2");
        imager.classList.add("mb-2");

        var classNames = cls.split(" ");
        classNames.forEach((classN) => {
            imager.classList.add(classN);
        });

        imager.setAttribute("id", id);
        document.getElementById(previewerId).append(imager);
    }
}