
/* Loading Effect START*/
function showLoader() {
    $("#ApplicationLoader").show();
    
}
function hideLoader() {
    $("#ApplicationLoader").hide();
}
/* Loading Effect END*/

function errorNotify(identity, message) {
    $("#" + identity).notify(message, { position: "right", autoHide: false });
}

function hideAllNotify() {
    $('.notifyjs-hidable').trigger('notify-hide');
}

$(document).ready(function () {
    applyTriggerNotify();
    
});

function isNumber(evt, element) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    if (
        (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
        (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
        (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function applyTriggerNotify() {
    $('input[type=text]').on('keyup', function (event) {
        
        if ($("#" + event.target.id).parent().children().first().attr("class") == "notifyjs-wrapper notifyjs-hidable") {
            $("#" + event.target.id).parent().children().first().trigger("notify-hide");
        }
    });
    $("select").on('change', function (event) {
        
        if ($("#" + event.target.id).parent().children().first().attr("class") == "notifyjs-wrapper notifyjs-hidable") {
            $("#" + event.target.id).parent().children().first().trigger("notify-hide");
        }
    });
    //$('input[type=text]').on('keyup', function (event) {
    //    
    //    if ($("#" + event.target.id).parent().children().first().attr("class") == "notifyjs-wrapper notifyjs-hidable") {
    //        $("#" + event.target.id).parent().children().first().trigger("notify-hide");
    //    }
    //});
    $('.numberAndDecimal').keypress(function (event) {
        return isNumber(event, this)
    });
    $(".onlyNumber").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });
}

function GotoTop() {
    $('html, body').animate({ scrollTop: '0px' }, 1000);
}

function GotoDown() {
    $('html, body').animate({ scrollTop: '1000px' }, 1000);
}
function isValidEmailAddress(emailAddress) {
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
    return pattern.test(emailAddress);
};