(function () {

    const baseUrl = abp.appPath + "api/services/app/Supplier/";

    function loadData() {
        let keyword = document.getElementById("keyword").value;
        abp.ui.setBusy('#data');

        abp.ajax({
            url: baseUrl + "GetAll",
            type: 'GET',
            data: { keyword: keyword }
        }).done(function (res) {
            let html = "";
            if (res && res.items.length > 0) {
                res.items.forEach(item => {
                    html += `
                        <tr>
                            <td><b>${item.name}</b></td>
                            <td>${item.phone || '---'}</td>
                            <td>${item.email}</td>
                            <td>${item.address || '---'}</td>
                            <td class="text-center">
                                <button class="btn btn-sm btn-info mr-1"
                                    onclick="edit(${item.id}, \`${item.name}\`, \`${item.phone || ''}\`, \`${item.email}\`, \`${item.address || ''}\`)">
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
                html = "<tr><td colspan='5' class='text-center text-muted'>Không tìm thấy dữ liệu</td></tr>";
            }
            document.getElementById("data").innerHTML = html;
        }).always(function () {
            abp.ui.clearBusy('#data');
        });
    }

    function create() {
        let data = {
            name: document.getElementById("name").value,
            phone: document.getElementById("phone").value,
            email: document.getElementById("email").value,
            address: document.getElementById("address").value
        };

        if (!data.name || !data.email) {
            abp.message.warn("Vui lòng nhập đầy đủ!");
            return;
        }

        abp.ui.setBusy();
        abp.ajax({
            url: baseUrl + "Create",
            type: 'POST',
            data: JSON.stringify(data)
        }).done(function () {
            abp.notify.success("Thêm thành công!");
            loadData();
            resetForm();
        }).always(function () {
            abp.ui.clearBusy();
        });
    }

    function update() {
        let id = document.getElementById("id").value;
        if (!id) return;

        let data = {
            id: parseInt(id),
            name: document.getElementById("name").value,
            phone: document.getElementById("phone").value,
            email: document.getElementById("email").value,
            address: document.getElementById("address").value
        };

        abp.ui.setBusy();
        abp.ajax({
            url: baseUrl + "Update",
            type: 'PUT',
            data: JSON.stringify(data)
        }).done(function () {
            abp.notify.info("Cập nhật thành công!");
            loadData();
            resetForm();
        }).always(function () {
            abp.ui.clearBusy();
        });
    }

    function edit(id, name, phone, email, address) {
        document.getElementById("id").value = id;
        document.getElementById("name").value = name;
        document.getElementById("phone").value = phone;
        document.getElementById("email").value = email;
        document.getElementById("address").value = address;

        document.getElementById("formTitle").innerText = "Đang chỉnh sửa nhà cung cấp: " + name;

        document.getElementById("btnUpdate").disabled = false;
        document.getElementById("btnAdd").disabled = true;

        window.scrollTo({ top: 0, behavior: 'smooth' });
    }

    function remove(id) {
        abp.message.confirm(
            "Dữ liệu này sẽ không thể khôi phục!",
            "Bạn có chắc muốn xoá?",
            function (isConfirmed) {
                if (isConfirmed) {
                    abp.ui.setBusy();
                    abp.ajax({
                        url: baseUrl + "Delete?id=" + id,
                        type: 'DELETE'
                    }).done(function () {
                        abp.notify.error("Đã xoá!");
                        loadData();
                    }).always(function () {
                        abp.ui.clearBusy();
                    });
                }
            }
        );
    }

    function resetForm() {
        document.getElementById("id").value = "";
        document.getElementById("name").value = "";
        document.getElementById("phone").value = "";
        document.getElementById("email").value = "";
        document.getElementById("address").value = "";

        document.getElementById("formTitle").innerText = "Thêm nhà cung cấp";

        document.getElementById("btnUpdate").disabled = true;
        document.getElementById("btnAdd").disabled = false;
    }

    // 👇 QUAN TRỌNG: cho HTML gọi được
    window.loadData = loadData;
    window.create = create;
    window.update = update;
    window.edit = edit;
    window.remove = remove;
    window.resetForm = resetForm;

    // chạy lần đầu
    loadData();

})();