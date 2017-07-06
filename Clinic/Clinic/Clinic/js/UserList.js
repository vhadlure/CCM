function AddNewUser() {
    showLoader();
    window.location.href = "/User/AddUser";
}

$(document).ready(function () {
    applyTriggerNotify();

    setTimeout(function () {
        $(".UserMM").addClass("active");
    }, 1000);

    var theTable = $('#userListGrid');
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

var userListGrid = $("#userListGrid").grid({
    uiLibrary: "bootstrap",
    responsive: true,
    dataSource: {
        url: "/User/GetAllUserDetails_ForList",
    },
    autoLoad: true,
    columns: [
   { field: "UserID", title: "ID", width: 40 },
   { field: "FullName", title: "Name", width: 100 },
   { field: "AddressLine", title: "Address", width: 100 },
   { field: "MobileNumber", title: "Mobile Number", width: 40 },
   { field: "GenderStr", title: "Gender", width: 50 },
    ],
    pager: { enable: true, limit: 10, sizes: [10, 20, 50, 100] }
});