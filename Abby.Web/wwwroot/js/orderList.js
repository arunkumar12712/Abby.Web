$(document).ready(function () {
  dataTable =  $('#DT_Load').DataTable({
        "ajax": {
            "url": "/api/Order",
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
        "width":"100%"

    });
});

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });

            //Swal.fire(
            //    'Deleted!',
            //    'Your file has been deleted.',
            //    'success'
            //)
        }
    });

}