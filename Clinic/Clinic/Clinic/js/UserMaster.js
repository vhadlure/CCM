$(document).ready(function () {
    applyTriggerNotify();
    setTimeout(function () {
        $(".UserMM").addClass("active");
    }, 1000);

    if ($("#StateName").val() != null && $("#StateName").val() != "") {
        $("#StateAutocomplete").val($("#StateName").val());
    }
    if ($("#CountryName").val() != null && $("#CountryName").val() != "") {
        $("#CountryAutocomplete").val($("#CountryName").val());
    }
    if ($("#CityName").val() != null && $("#CityName").val() != "") {
        $("#CityAutocomplete").val($("#CityName").val());
    }
    if ($("#Gender").val() != null && $("#Gender").val() != "") {
        $("#GenderDDL").val($("#Gender").val());
    }
    if ($("#ClinicName").val() != null && $("#ClinicName").val() !== "") {
        $("#ClinicAutocomplete").val($("#ClinicName").val());
    }
});

$("#UserRegistrationFormID").submit(function (e) {
    showLoader();
    $("#StateName").val($("#StateAutocomplete").val());
    $("#CountryName").val($("#CountryAutocomplete").val());
    $("#CityName").val($("#CityAutocomplete").val());
    $("#Gender").val($("#GenderDDL").val());
    $("#ClinicName").val($("#ClinicAutocomplete").val());

    var postData = $(this).serializeArray();
    var formURL = $(this).attr("action");
    $.ajax(
    {
        url: formURL,
        type: "POST",
        data: postData,
        success: function (message, textStatus, jqXHR) {
            hideLoader();
            $.notify("User details saved successfully", "success");
            $(':input').not(':button, :submit, :reset, select').val('');
            window.location.href = "/User/Index";
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
    e.preventDefault(); //STOP default action
});

function SaveUserDetails() {
    if (userRegisterationValidation()) {
        $("#UserRegistrationFormID").submit();
    }
}
function ResetUserDetails() {
    $(':input').not(':button, :submit, :reset, :select').val('');
    $('select').val("0");
}

function OnGenderChange() {
    $("#GenderID").val($("#GenderDDL").val());
}