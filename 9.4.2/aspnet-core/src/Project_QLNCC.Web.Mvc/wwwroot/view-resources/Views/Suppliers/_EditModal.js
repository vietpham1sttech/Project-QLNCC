(function () {

    var _supplierService = abp.services.app.supplier;

    // Hàm dùng chung: hiện/ẩn lỗi inline cho Edit modal
    function setEditFieldError(fieldName, message) {
        var input = $('#edit-field-' + fieldName);
        var errorSpan = $('#edit-error-' + fieldName);
        if (message) {
            input.addClass('is-invalid').css('border-color', '#dc3545');
            errorSpan.text(message).show();
        } else {
            input.removeClass('is-invalid').css('border-color', '');
            errorSpan.hide();
        }
    }

    // Xóa lỗi khi người dùng gõ lại
    $(document).on('input', '#edit-field-Code, #edit-field-Name, #edit-field-Email', function () {
        var fieldName = this.id.replace('edit-field-', '');
        setEditFieldError(fieldName, '');
    });

    $(document).on('click', '#SupplierEditModal .save-button', function () {

        var form = $('#SupplierEditModal').find('form[name=supplierEditForm]');
        var data = form.serializeFormToObject();

        data.Id = parseInt(data.Id);
        data.IsActive = $('#IsActiveCheck').is(':checked');

        // Reset lỗi cũ
        ['Code', 'Name', 'Email'].forEach(function (f) {
            setEditFieldError(f, '');
        });

        // Validate
        var hasError = false;

        if (!data.Code || !data.Code.trim()) {
            setEditFieldError('Code', 'Mã Supplier không được để trống.');
            hasError = true;
        }
        if (!data.Name || !data.Name.trim()) {
            setEditFieldError('Name', 'Tên Supplier không được để trống.');
            hasError = true;
        }
        if (!data.Email || !data.Email.trim()) {
            setEditFieldError('Email', 'Email không được để trống.');
            hasError = true;
        }

        if (hasError) return;

        _supplierService.update(data).done(function () {
            abp.notify.success("Cập nhật thành công");
            $('#SupplierEditModal').modal('hide');
            $('#SuppliersTable').DataTable().ajax.reload();
        }).fail(function (err) {
            var title = 'Lỗi';
            var message = 'Không thể cập nhật Supplier.';

            if (err.responseJSON && err.responseJSON.error) {
                var error = err.responseJSON.error;
                if (error.message) title = error.message;
                if (error.details) message = error.details;
            }

            abp.message.error(message, title);
        });
    });

})();