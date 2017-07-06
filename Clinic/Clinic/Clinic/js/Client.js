$(document).ready(function () {
    applyTriggerNotify();

    setTimeout(function () {
        $(".ClinicMM").addClass("active");
    }, 1000);

    var theTable = $('#clinicListGrid');
    theTable.find("tbody > tr").find("td:eq(1)").mousedown(function () {
        $(this).prev().find(":checkbox").click()
    });

    $("#filter").keyup(function () {
        $.uiTableFilter(theTable, this.value);
    })

    $('#filter-form').submit(function () {
        theTable.find("tbody > tr:visible > td:eq(1)").mousedown();
        return false;
    }).focus(); //Give focus to input field
});

function RegisterNewClinic() {
    showLoader();
    window.location.href = "/Clinic/ClinicMaster";
}

var clinicListGrid = $("#clinicListGrid").grid({
    uiLibrary: "bootstrap",
    responsive: true,
    dataSource: {
        url: "/Clinic/GetAllClinicDetails_ForList",
    },
    autoLoad: true,
    columns: [
   { field: "ClinicID", title: "ID", width: 40 },
   { field: "Name", title: "Name", width: 100 },
   { field: "Address", title: "Address", width: 100 },
   { field: "Name", title: "Patient List", width: 65, type: "icon", icon: "fa fa-user-plus", tooltip: "Patient List", events: { "click": getPatientListByClinicName } },
    ],
    pager: { enable: true, limit: 10, sizes: [10, 20, 50, 100] }
});

function getPatientListByClinicName(e) {
    window.location.href = "/Patient/PatientProfileView?ClinicName=" + e.data.record.Name;
}




