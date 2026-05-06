(function () {

    $.fn.dataTable.defaults.buttons = [];

    var _supplierService = abp.services.app.supplier;
    var _$table = $('#SuppliersTable');

    var dataTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        processing: true,

        ajax: function (data, callback) {
            var isActiveVal = $('#filterIsActive').val();
            _supplierService.getAll({
                keyword: $('#keyword').val(),
                isActive: isActiveVal === '' ? null : isActiveVal === 'true'
            }).done(function (result) {
                callback({
                    recordsTotal: result.totalCount,
                    recordsFiltered: result.totalCount,
                    data: result.items
                });
            });
        },

        columns: [
            { data: "code" },
            { data: "name" },
            { data: "email" },
            { data: "phone" },
            {
                data: "isActive",
                render: d => d
                    ? '<span class="badge badge-success">Hoạt động</span>'
                    : '<span class="badge badge-danger">Ngưng</span>'
            },
            {
                data: null,
                orderable: false,
                render: function (data) {
                    return `
                        <button class="btn btn-sm btn-info" onclick="edit(${data.id})">
                            <i class="fa fa-edit"></i>
                        </button>
                        <button class="btn btn-sm btn-danger" onclick="deleteSupplier(${data.id})">
                            <i class="fa fa-trash"></i>
                        </button>
                    `;
                }
            }
        ]
    });

    $('#GetSuppliersButton').click(function () {
        dataTable.ajax.reload();
    });

    $('#keyword').keyup(function (e) {
        if (e.keyCode === 13) dataTable.ajax.reload();
    });

    $('#filterIsActive').change(function () {
        dataTable.ajax.reload();
    });

    $('#ResetFilterButton').click(function () {
        $('#keyword').val('');
        $('#filterIsActive').val('');
        dataTable.ajax.reload();
    });

    $('#CreateNewSupplierButton').click(function () {
        $('#SupplierCreateModal').modal('show');
    });

    window.edit = function (id) {
        abp.ajax({
            url: '/Suppliers/EditModal?id=' + id,
            type: 'GET',
            dataType: 'html',
            success: function (content) {
                $('#SupplierEditModal .modal-content').html(content);
                $('#SupplierEditModal').modal('show');
            },
            error: function (xhr) {
                console.log("STATUS:", xhr.status);
                console.log("RESPONSE:", xhr.responseText);
            }
        });
    };

    window.deleteSupplier = function (id) {
        abp.message.confirm(
            "Bạn có chắc muốn xóa Supplier?",
            "Xác nhận",
            function (result) {
                if (result) {
                    _supplierService.delete({ id: id }).done(function () {
                        abp.notify.success("Đã xóa");
                        dataTable.ajax.reload();
                    });
                }
            }
        );
    };

})();