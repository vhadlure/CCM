$(document).ready(function () {
    applyTriggerNotify();
    setTimeout(function () {
        $(".PatientMM").addClass("active");
    }, 1000);

    var theTable = $('#patientListGrid');
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

function AddNewPatient() {
    showLoader();
    window.location.href = "/Patient/PatientMaster";
}

var patientListGrid = $("#patientListGrid").grid({
    uiLibrary: "bootstrap",
    responsive: true,
    dataSource: {
        url: "/Patient/GetAllPatientDetails_ForList",
    },
    autoLoad: true,
    columns: [
   { field: "PatientID", title: "ID", width: 40 },
   { field: "FullName", title: "Name", width: 100 },
   { field: "TimeSpentstr", title: "Minutes of care", width: 100 },
   { field: "BloodGroup", title: "BloodGroup", width: 40 },
   { field: "GenderStr", title: "Gender", width: 50 },
   { field: "DOBStr", title: "DOB", width: 100 },
   { field: "age", title: "Age", width: 40 },
   { field: "PatientID", title: "Preview", width: 65, type: "icon", icon: "glyphicon-eye-open", tooltip: "Preview", events: { "click": PreviewPatientProfile } },
    ],
    pager: { enable: true, limit: 10, sizes: [10, 20, 50, 100] }
});

function PreviewPatientProfile(e) {
    window.location.href = "/Patient/PatientIndividualProfile?PatientID=" + e.data.record.PatientID;
}