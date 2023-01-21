$(document).ready(function () {
    debugger
    var url = window.location.search;
    if (url.includes("cancelled")) {
        loadList("cancelled");
    }
    else {
        if (url.includes("completed")) {
            loadList("completed");
        }
        else {
            if (url.includes("ready")) {
                loadList("ready");
            }
            else {
                loadList("inprocess");
            }
        }
    }

    //alert('call');
});

function loadList(param) {
   
    dataTable = $('#DT_Load').DataTable({
        "ajax": {
            "url": "/api/Order?status=" + param,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "25%" },
            { "data": "pickupName", "width": "15%" },
            { "data": "applicationUser.email", "width": "15%" },
            { "data": "orderTotal", "width": "15%" },
            { "data": "pickUpTime", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div>
                                <a href="OrderDetails?id=${data}" class="btn btn-success text-white"
                                style="cursor:pointer;width:100px;"><i class="bi-pencil-square"></i></a>
                            </div>`
                }
            }
        ],
        "width": "100%"
    });
}