(function () {

    var _supplierService = abp.services.app.supplier;

    function setCreateFieldError(fieldName, message) {
        var input = $('#create-field-' + fieldName);
        var errorSpan = $('#create-error-' + fieldName);
        if (message) {
            input.css('border-color', '#dc3545');
            errorSpan.text(message).show();
        } else {
            input.css('border-color', '');
            errorSpan.hide();
        }
    }

    // Xóa lỗi khi gõ lại
    $('#SupplierCreateModal').on('input', 'input[name=Code], input[name=Name], input[name=Email]', function () {
        var name = $(this).attr('name');
        setCreateFieldError(name, '');
    });

    // Reset khi đóng modal
    $('#SupplierCreateModal').on('hidden.bs.modal', function () {
        ['Code', 'Name', 'Email'].forEach(function (f) {
            setCreateFieldError(f, '');
        });
        $('#SupplierCreateModal').find('form')[0].reset();
    });

    $('#SupplierCreateModal').on('click', '.save-button', function () {

        // ĐỌC THẲNG TỪ DOM thay vì serializeFormToObject
        var code = $.trim($('#create-field-Code').val());
        var name = $.trim($('#create-field-Name').val());
        var email = $.trim($('#create-field-Email').val());

        // Reset lỗi cũ
        ['Code', 'Name', 'Email'].forEach(function (f) {
            setCreateFieldError(f, '');
        });

        var hasError = false;

        if (!code) {
            setCreateFieldError('Code', 'Mã Supplier không được để trống.');
            hasError = true;
        }
        if (!name) {
            setCreateFieldError('Name', 'Tên Supplier không được để trống.');
            hasError = true;
        }
        if (!email) {
            setCreateFieldError('Email', 'Email không được để trống.');
            hasError = true;
        }

        if (hasError) return; // dừng, không gọi API

        // Lấy toàn bộ data để gửi lên server
        var form = $('#SupplierCreateModal').find('form[name=supplierCreateForm]');
        var data = form.serializeFormToObject();
        data.IsActive = $('#SupplierCreateModal input[name=IsActive]').is(':checked');

        _supplierService.create(data).done(function () {
            abp.notify.success("Thêm thành công");
            $('#SupplierCreateModal').modal('hide');
            $('#SuppliersTable').DataTable().ajax.reload();
        }).fail(function (err) {
            var title = 'Lỗi';
            var message = 'Không thể thêm Supplier.';
            if (err.responseJSON && err.responseJSON.error) {
                var error = err.responseJSON.error;
                if (error.message) title = error.message;
                if (error.details) message = error.details;
            }
            abp.message.error(message, title);
        });

    });

})();