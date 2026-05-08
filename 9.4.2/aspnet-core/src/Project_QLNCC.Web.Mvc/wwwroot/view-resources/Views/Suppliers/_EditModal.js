(function ($) {
    // =========================
    // KHAI BÁO BIẾN
    // =========================
    // Gọi AppService Supplier từ ABP
    var _supplierService = abp.services.app.supplier,
        l = abp.localization.getSource('Project_QLNCC'),
        _$modal = $('#SupplierEditModal'),
        _$form = _$modal.find('form');

    // =========================
    // HIỂN THỊ LỖI FIELD
    // =========================
    function setFieldError(fieldName, message) {
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

    // =========================
    // CLEAR TOÀN BỘ LỖI
    // =========================
    function clearErrors() {
        ['Code', 'Name', 'Email'].forEach(function (f) {
            setFieldError(f, '');
        });
    }

    // =========================
    // VALIDATE FORM
    // =========================
    function validate(data) {
        var hasError = false;

        if (!data.Code || !data.Code.trim()) {
            setFieldError('Code', 'Id Supplier required.');
            hasError = true;
        }
        if (!data.Name || !data.Name.trim()) {
            setFieldError('Name', 'Name Supplier required.');
            hasError = true;
        }
        if (!data.Email || !data.Email.trim()) {
            setFieldError('Email', 'Email required.');
            hasError = true;
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(data.Email)) {
            setFieldError('Email', 'Email format invalid. Example: example@gmail.com');
            hasError = true;
        }

        return !hasError;
    }

    // =========================
    // HÀM UPDATE
    // =========================
    function save() {
        var data = _$form.serializeFormToObject();
        data.Id = parseInt(data.Id);
        data.IsActive = _$modal.find('#IsActiveCheck').is(':checked');

        clearErrors();

        if (!validate(data)) {
            return;
        }

        abp.ui.setBusy(_$form);
        _supplierService.update(data).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('supplier.edited', data);
        }).fail(function (err) {
            var title = 'Error';
            var message = 'Can not update Supplier.';
            if (err.responseJSON && err.responseJSON.error) {
                var error = err.responseJSON.error;
                if (error.message) title = error.message;
                if (error.details) message = error.details;
            }
            abp.message.error(message, title);
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }

    // =========================
    // CLICK NÚT SAVE
    // =========================
    _$form.closest('div.modal-content').find('.save-button').on('click', function (e) {
        e.preventDefault();
        save();
    });

    // =========================
    // NHẤN ENTER ĐỂ UPDATE
    // =========================
    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    // =========================
    // XÓA LỖI KHI USER NHẬP
    // =========================
    _$form.on('input', '#edit-field-Code, #edit-field-Name, #edit-field-Email', function () {
        var fieldName = this.id.replace('edit-field-', '');
        setFieldError(fieldName, '');
    });

    // =========================
    // EVENT MODAL
    // =========================
    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    }).on('hidden.bs.modal', function () {
        clearErrors();
        _$form[0].reset();
    });

})(jQuery);