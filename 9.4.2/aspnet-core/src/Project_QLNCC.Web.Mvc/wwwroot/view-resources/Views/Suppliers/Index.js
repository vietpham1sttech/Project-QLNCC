(function () {
    const baseUrl = abp.appPath + "api/services/app/Supplier/";

    function loadData() {
        let keyword = document.getElementById("keyword").value;
        let isActive = document.getElementById("filterIsActive").value;
        abp.ui.setBusy('#data');

        abp.ajax({
            url: baseUrl + "GetAll",
            type: 'GET',
            data: {
                keyword: keyword,
                isActive: isActive === "" ? null : isActive === "true"
            }
        }).done(function (res) {
            let html = "";
            if (res && res.items.length > 0) {
                res.items.forEach(item => {
                    const statusBadge = item.isActive
                        ? '<span class="badge badge-success">Hoạt động</span>'
                        : '<span class="badge badge-danger">Ngừng hoạt động</span>';

                    html += `
                        <tr>
                            <td class="text-center font-weight-bold">${item.code || '---'}</td>
                            <td>${item.name}</td>
                            <td class="text-center">${item.taxCode || '---'}</td>
                            <td>${item.contactPerson || '---'}</td>
                            <td>
                                <small><i class="fas fa-envelope mr-1 text-secondary"></i>${item.email}</small><br>
                                <small><i class="fas fa-phone mr-1 text-secondary"></i>${item.phone || '---'}</small>
                            </td>
                            <td class="text-center">${statusBadge}</td>
                            <td class="text-center">
                                <button class="btn btn-sm btn-info mr-1" onclick='edit(${JSON.stringify(item)})' title="Sửa">
                                    <i class="fas fa-edit"></i>
                                </button>
                                <button class="btn btn-sm btn-danger" onclick="remove(${item.id})" title="Xoá">
                                    <i class="fas fa-trash"></i>
                                </button>
                            </td>
                        </tr>`;
                });
            } else {
                html = "<tr><td colspan='7' class='text-center text-muted p-4'>Không tìm thấy nhà cung cấp nào</td></tr>";
            }
            document.getElementById("data").innerHTML = html;
        }).always(function () {
            abp.ui.clearBusy('#data');
        });
    }

    function getDataFromForm() {
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

    // Hàm xử lý hiển thị lỗi tiếng Việt từ Server
    // Hàm xử lý hiển thị lỗi tiếng Việt hoàn toàn
    function handleServerError(error) {
        let message = "Dữ liệu không hợp lệ";
        let details = "";

        if (error && (error.details || error.message)) {
            let rawDetails = error.details || error.message;

            // MẸO: Thay thế câu dẫn mặc định của hệ thống bằng tiếng Việt
            details = rawDetails.replace(
                "The following errors were detected during validation.",
                "Các lỗi sau đã được phát hiện trong quá trình kiểm tra:"
            );
        }

        abp.message.error(details, message);
    }

    function create() {
        let data = getDataFromForm();

        // Kiểm tra nhanh phía Client để không cần chờ Server
        if (!data.code || !data.name || !data.email) {
            abp.message.warn("Vui lòng nhập đầy đủ các trường bắt buộc Mã, Tên và Email!", "Thông tin còn thiếu");
            return;
        }

        abp.ui.setBusy();
        abp.ajax({
            url: baseUrl + "Create",
            type: 'POST',
            data: JSON.stringify(data),
            abpHandleError: false // Tắt popup mặc định của ABP
        }).done(function () {
            abp.notify.success("Thêm nhà cung cấp thành công!");
            loadData();
            resetForm();
        }).fail(function (err) {
            handleServerError(err); // Hiển thị lỗi tiếng Việt tự định nghĩa
        }).always(function () {
            abp.ui.clearBusy();
        });
    }

    function update() {
        let id = document.getElementById("id").value;
        if (!id) return;
        let data = getDataFromForm();
        data.id = parseInt(id);

        abp.ui.setBusy();
        abp.ajax({
            url: baseUrl + "Update",
            type: 'PUT',
            data: JSON.stringify(data),
            abpHandleError: false // Tắt popup mặc định của ABP
        }).done(function () {
            abp.notify.info("Đã cập nhật thông tin thành công!");
            loadData();
            resetForm();
        }).fail(function (err) {
            handleServerError(err);
        }).always(function () {
            abp.ui.clearBusy();
        });
    }

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
        document.getElementById("formTitle").innerHTML = `<span class="text-info">Đang sửa nhà cung cấp: ${item.name}</span>`;
        document.getElementById("btnUpdate").disabled = false;
        document.getElementById("btnAdd").disabled = true;
        window.scrollTo({ top: 0, behavior: 'smooth' });
    }

    function remove(id) {
        abp.message.confirm(
            "Nhà cung cấp này sẽ bị xoá khỏi hệ thống!",
            "Xác nhận xoá?",
            function (isConfirmed) {
                if (isConfirmed) {
                    abp.ui.setBusy();
                    abp.ajax({
                        url: baseUrl + "Delete?id=" + id,
                        type: 'DELETE'
                    }).done(function () {
                        abp.notify.error("Đã xoá nhà cung cấp thành công!");
                        loadData();
                    }).always(function () {
                        abp.ui.clearBusy();
                    });
                }
            }
        );
    }

    function resetForm() {
        const fields = ["id", "code", "name", "taxCode", "contactPerson", "phone", "email", "address"];
        fields.forEach(f => document.getElementById(f).value = "");
        document.getElementById("isActive").value = "true";
        document.getElementById("formTitle").innerText = "Thêm nhà cung cấp mới";
        document.getElementById("btnUpdate").disabled = true;
        document.getElementById("btnAdd").disabled = false;
    }

    window.loadData = loadData;
    window.create = create;
    window.update = update;
    window.edit = edit;
    window.remove = remove;
    window.resetForm = resetForm;
    loadData();
})();