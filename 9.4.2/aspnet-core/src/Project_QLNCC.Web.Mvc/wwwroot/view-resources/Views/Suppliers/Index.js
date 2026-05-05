(function () {
    const supplierServiceUrl = abp.appPath + "api/services/app/Supplier/";

    // ===================== LOAD DATA =====================
    function loadData() {
        const keyword = document.getElementById("keyword").value;
        const isActiveValue = document.getElementById("filterIsActive").value;
        const address = document.getElementById("filterAddress").value;

        abp.ui.setBusy('#data');

        abp.ajax({
            url: supplierServiceUrl + "GetAll",
            type: 'GET',
            data: {
                keyword: keyword,
                isActive: isActiveValue === "" ? null : isActiveValue === "true",
                address: address === "" ? null : address
            }
        }).done(renderTable)
            .always(() => abp.ui.clearBusy('#data'));
    }

    // ===================== RENDER =====================
    function renderTable(result) {
        let html = "";

        if (result && result.items.length > 0) {
            result.items.forEach(item => {
                const status = item.isActive
                    ? '<span class="badge badge-success">Hoạt động</span>'
                    : '<span class="badge badge-danger">Ngừng hoạt động</span>';

                html += `
                <tr>
                    <td class="text-center font-weight-bold">${item.code || '---'}</td>
                    <td>${item.name}</td>
                    <td class="text-center">${item.taxCode || '---'}</td>
                    <td>${item.contactPerson || '---'}</td>
                   <td>
                        <small>
                            <i class="fas fa-envelope text-secondary mr-1"></i>${item.email}
                        </small><br>
                        <small>
                            <i class="fas fa-phone text-secondary mr-1"></i>${item.phone || '---'}
                        </small>
                    </td>
                    <td class="text-center">${status}</td>
                    <td class="text-center">
                        <button class="btn btn-sm btn-info mr-1"
                            onclick='edit(${JSON.stringify(item)})'>
                            <i class="fas fa-edit"></i>
                        </button>
                        <button class="btn btn-sm btn-danger"
                            onclick="remove(${item.id})">
                            <i class="fas fa-trash"></i>
                        </button>
                    </td>
                </tr>`;
            });
        } else {
            html = `<tr>
                        <td colspan="7" class="text-center text-muted p-4">
                            Không có dữ liệu
                        </td>
                    </tr>`;
        }

        document.getElementById("data").innerHTML = html;
    }

    // ===================== FORM =====================
    function getFormData() {
        return {
            code: document.getElementById("code").value.trim(),
            name: document.getElementById("name").value.trim(),
            taxCode: document.getElementById("taxCode").value.trim(),
            contactPerson: document.getElementById("contactPerson").value.trim(),
            phone: document.getElementById("phone").value.trim(),
            email: document.getElementById("email").value.trim(),
            address: document.getElementById("address").value.trim(),
            isActive: document.getElementById("isActive").value === "true"
        };
    }

    function resetForm() {
        const fields = ["id", "code", "name", "taxCode", "contactPerson", "phone", "email", "address"];
        fields.forEach(f => document.getElementById(f).value = "");
        document.getElementById("isActive").value = "true";
        document.getElementById("formTitle").innerText = "Thêm nhà cung cấp mới";
        document.getElementById("btnAdd").disabled = false;
        document.getElementById("btnUpdate").disabled = true;
        document.getElementById("keyword").value = "";
        document.getElementById("filterIsActive").value = "";
    }

    // ===================== CREATE =====================
    function create() {
        const data = getFormData();

        if (!data.code || !data.name || !data.email) {
            abp.message.warn("Vui lòng nhập Mã, Tên và Email!");
            return;
        }

        abp.ui.setBusy();

        abp.ajax({
            url: supplierServiceUrl + "Create",
            type: 'POST',
            data: JSON.stringify(data),
            abpHandleError: false
        }).done(function () {
            abp.notify.success("Thêm thành công!");
            loadData();
            resetForm();
        }).fail(handleError)
            .always(() => abp.ui.clearBusy());
    }

    // ===================== UPDATE =====================
    function update() {
        const id = document.getElementById("id").value;
        if (!id) return;

        const data = getFormData();
        data.id = parseInt(id);

        abp.ui.setBusy();

        abp.ajax({
            url: supplierServiceUrl + "Update",
            type: 'PUT',
            data: JSON.stringify(data),
            abpHandleError: false
        }).done(function () {
            abp.notify.info("Cập nhật thành công!");
            loadData();
            resetForm();
        }).fail(handleError)
            .always(() => abp.ui.clearBusy());
    }

    // ===================== EDIT =====================
    function edit(item) {
        document.getElementById("id").value = item.id;
        document.getElementById("code").value = item.code;
        document.getElementById("name").value = item.name;
        document.getElementById("taxCode").value = item.taxCode || "";
        document.getElementById("contactPerson").value = item.contactPerson || "";
        document.getElementById("phone").value = item.phone || "";
        document.getElementById("email").value = item.email;
        document.getElementById("address").value = item.address || "";
        document.getElementById("isActive").value = item.isActive.toString();

        document.getElementById("formTitle").innerHTML =
            `<span class="text-info">Đang sửa: ${item.name}</span>`;

        document.getElementById("btnAdd").disabled = true;
        document.getElementById("btnUpdate").disabled = false;

        window.scrollTo({ top: 0, behavior: 'smooth' });
    }

    // ===================== DELETE =====================
    function remove(id) {
        abp.message.confirm(
            "Bạn có chắc muốn xoá?",
            "Xác nhận",
            function (confirmed) {
                if (!confirmed) return;

                abp.ui.setBusy();

                abp.ajax({
                    url: supplierServiceUrl + "Delete?id=" + id,
                    type: 'DELETE'
                }).done(function () {
                    abp.notify.success("Xoá thành công!");
                    loadData();
                }).always(() => abp.ui.clearBusy());
            }
        );
    }

    // ===================== ERROR =====================
    function handleError(error) {
        let details = error.details || error.message || "Có lỗi xảy ra";

        details = details.replace(
            "The following errors were detected during validation.",
            "Lỗi dữ liệu:"
        );

        abp.message.error(details, "Lỗi");
    }

    // ===================== EXPORT =====================
    window.loadData = loadData;
    window.create = create;
    window.update = update;
    window.edit = edit;
    window.remove = remove;
    window.resetForm = resetForm;

    loadData();

})();