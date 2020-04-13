$(document).ready(function () {
    $('#approverequestTable').DataTable({
        "ajax": {
            "url": "../User/GetRequest/",
            "dataSrc": "",
            "type": "Post",
            "data": function (d) {
                d.RequestId = reqid
            }
        },

        "columns": [
        {
            "title": "User Name",
            "data": "UserName"
            },
        {
            "title": "Product Name",
            "data": "Product1"
        },
        {
            "title": "Request Time",
            "data": "RequestTime",
            render: function (d) {
                return moment(d).format("DD:MM:YYYY HH:mm");
            }
        },
        {
            "title": "Quantity",
            "data": "Quantity"
        }]
    });
});

$("#approveRequest").on("click",  function () {
  
    $.ajax({
        traditional: true,
        type: "POST",
        url: "/User/ApproveRequestInsert/",
        data: JSON.stringify({ RequestGUID : guid }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.result == 'Redirect')
                window.location = response.url;
        }
    });
});
