$(document).ready(function () {
    $("#studentDataTable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Home/GetStudents",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "firstName", "name": "FirstName", "autoWidth": true },
            { "data": "lastName", "name": "LastName", "autoWidth": true },
            { "data": "class", "name": "Class", "autoWidth": true },
            { "data": "section", "name": "Section", "autoWidth": true },
            { "data": "phoneNumber", "name": "PhoneNumber", "autoWidth": true },
            { "data": "address", "name": "Address", "autoWidth": true },
            { "data": "dateOfAdmission", "name": "DateOfAdmission", "autoWidth": true },
           
            {
                "render": function (data,row) 
                {
                     return "<a href='Student/Delete' class='btn btn-danger' onclick=DeleteCustomer('" + row.id+ "'); >🎁</a>";


               }
            },
           
        ]
    });
});  