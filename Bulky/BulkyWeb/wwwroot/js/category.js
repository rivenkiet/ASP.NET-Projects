var dataTable

$(document).ready(function () {
    console.log("test")
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        ajax: { url: '/admin/category/getall' },
        columns: [
            { data: 'name', "width": "30%" },
            { data: 'displayOrder', "width": "30%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group " role="group">
                            <a href="/Admin/Category/Edit/${data}" class="btn btn-primary mx-2">
								<i class="bi bi-pencil"></i> Edit
							</a>
							<a onClick=Delete("/admin/category/delete/${data}") asp-route-id="@item.Id" class="btn btn-danger mx-2">
								<i class="bi bi-trash3"></i> Delete
							</a>
                        </div>`
                },
                "width": "40%"
            }
        ]
    });
}

function Delete(url) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel!",
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload()
                    toastr.success(data.message)
                }
            })
        }
    });
}
