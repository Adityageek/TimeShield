function buildForm() {
    return [
        
        $('#Product').find("option:selected").text(),
        $('#Quantity').val(),
        $('#Product').val()
 
    ];
}

    function addRow(dataTable) {
    const formData = this.buildForm();
    const addedRow = dataTable.row.add(formData).draw(false);
}

$('#add').on('click', this.addRow.bind(this, dataTable));

function selectRow(dataTable) {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    } else {
        dataTable.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
}

function removeRow(dataTable) {
    dataTable.row('.selected').remove().draw(false);
}

const self = this;
$('#requestTable tbody').on('click', 'tr', function () {
    self.selectRow.bind(this, dataTable)();
});

$('#remove').on('click', this.removeRow.bind(this, dataTable));

$("body").on("click", "#requestbtn", function () {
    var products = new Array();
    $("#requestTable tbody tr").each(function () {
        var row = $(this);
        var product = {};
        product.Product1 = row.find("TD").eq(0).html();
        product.Quantity = row.find("TD").eq(1).html();
        product.ProductId = row.find("TD").eq(2).html();
        products.push(product);
    });

    $.ajax({
        type: "POST",
        url: "/User/RequestInsert",
        data: JSON.stringify(products),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.result == 'Redirect')
                window.location = response.url;
        }
    });
});