(function ($) {
    // Gọi AppService Supplier từ ABP
    var _supplierService = abp.services.app.supplier,
        l = abp.localization.getSource('Project_QLNCC'),
        _$modal = $('#SupplierCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#SuppliersTable');

    // Khởi tạo DataTable
    var _$suppliersTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _supplierService.getAll,
            inputFilter: function () {
                return {
                    keyword: $('#keyword').val(),
                    isActive: $('#filterIsActive').val() === ''
                        ? null
                        : $('#filterIsActive').val() === 'true'
                };
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$suppliersTable.draw(false)
            }
        ],
        responsive: {
            details: {
                type: 'column'
            }
        },
        columnDefs: [
            {
                targets: 0,
                className: 'control',
                defaultContent: '',
            },
            {
                targets: 1,
                data: 'code',
                sortable: false
            },
            {
                targets: 2,
                data: 'name',
                sortable: false
            },
            {
                targets: 3,
                data: 'email',
                sortable: false
            },
            {
                targets: 4,
                data: 'phone',
                sortable: false
            },
            {
                targets: 5,
                data: 'isActive',
                sortable: false,
                render: data => data
                    ? '<span class="badge badge-success">Active</span>'
                    : '<span class="badge badge-danger">Inactive</span>'
            },
            {
                targets: 6,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm bg-secondary edit-supplier" data-supplier-id="${row.id}" data-toggle="modal" data-target="#SupplierEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger delete-supplier" data-supplier-id="${row.id}" data-supplier-name="${row.name}">`,
                        `       <i class="fas fa-trash"></i> ${l('Delete')}`,
                        '   </button>'
                    ].join('');
                }
            }
        ]
    });

    // =========================
    // XÓA SUPPLIER
    // =========================
    $(document).on('click', '.delete-supplier', function () {
        var supplierId = $(this).attr('data-supplier-id');
        var supplierName = $(this).attr('data-supplier-name');

        deleteSupplier(supplierId, supplierName);
    });

    // Hàm xử lý xóa
    function deleteSupplier(supplierId, supplierName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                supplierName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _supplierService.delete({
                        id: supplierId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$suppliersTable.ajax.reload();
                    });
                }
            }
        );
    }

    // =========================
    // EDIT SUPPLIER
    // =========================
    $(document).on('click', '.edit-supplier', function (e) {
        var supplierId = $(this).attr('data-supplier-id');

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Suppliers/EditModal?id=' + supplierId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#SupplierEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        });
    });

    // =========================
    // CREATE MODAL
    // =========================
    $(document).on('click', 'a[data-target="#SupplierCreateModal"]', (e) => {
        $('.nav-tabs a[href="#supplier-details"]').tab('show');
    });

    abp.event.on('supplier.edited', (data) => {
        _$suppliersTable.ajax.reload();
    });

    // =========================
    // XỬ LÝ MODAL
    // =========================
    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    // =========================
    // SEARCH
    // =========================
    $('#GetSuppliersButton, .btn-search').on('click', (e) => {
        _$suppliersTable.ajax.reload();
    });

    $('#keyword, .txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$suppliersTable.ajax.reload();
            return false;
        }
    });

    // =========================
    // FILTER ACTIVE
    // =========================
    $('#filterIsActive').on('change', () => {
        _$suppliersTable.ajax.reload();
    });

    $('#ResetFilterButton').on('click', () => {
        $('#keyword').val('');
        $('#filterIsActive').val('');
        _$suppliersTable.ajax.reload();
    });

})(jQuery);