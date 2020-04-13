const dataTable = $('#requestTable').DataTable({
    autoWidth: false,
    colReorder: true,
    order: [],
    processing: true,
    columns: [
        { title: 'Product' },
        { title: 'Quantity' },
        {title: 'ProductId'}
    ]
});
