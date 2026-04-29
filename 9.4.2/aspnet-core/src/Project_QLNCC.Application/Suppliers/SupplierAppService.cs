using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Project_QLNCC.Authorization;
using Project_QLNCC.Suppliers.Dto;
using System.Linq;
using System.Threading.Tasks;

namespace Project_QLNCC.Suppliers
{
    [AbpAuthorize(PermissionNames.Pages_Suppliers)]
    public class SupplierAppService : AsyncCrudAppService<
        Supplier,
        SupplierDto,
        int,
        SearchSupplierInput,
        CreateSupplierInput,
        UpdateSupplierInput>,
        ISupplierAppService
    {
        public SupplierAppService(IRepository<Supplier, int> repository)
            : base(repository)
        {
        }

        // 🔍 Custom search
        protected override IQueryable<Supplier> CreateFilteredQuery(SearchSupplierInput input)
        {
            var query = base.CreateFilteredQuery(input);

            if (!string.IsNullOrWhiteSpace(input.Keyword))
            {
                query = query.Where(x =>
                x.Name.Contains(input.Keyword) ||
                (x.Phone != null && x.Phone.Contains(input.Keyword))
            );
            }

            return query;
        }
        
        public override async Task<SupplierDto> CreateAsync(CreateSupplierInput input)
        {
            // Kiểm tra trùng email trước khi tạo
            await CheckEmailExisted(input.Email);
            return await base.CreateAsync(input);
        }

        public override async Task<SupplierDto> UpdateAsync(UpdateSupplierInput input)
        {
            // Kiểm tra trùng email (loại trừ chính nó) trước khi cập nhật
            await CheckEmailExisted(input.Email, input.Id);
            return await base.UpdateAsync(input);
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {
            await base.DeleteAsync(input);
        }
        // Hàm kiểm tra trùng email của tài khoản
        private async Task CheckEmailExisted(string email, int? id = null)
        {
            if (string.IsNullOrWhiteSpace(email)) return;

            // Tìm xem có NCC nào khác đã dùng email này chưa
            var isExisted = await Repository.GetAll()
                .AnyAsync(x => x.Email == email && (!id.HasValue || x.Id != id.Value));

            if (isExisted)
            {
                // ABP sẽ tự bắt lỗi này và hiện hộp thoại thông báo màu đỏ ở Frontend
                throw new UserFriendlyException("Dữ liệu không hợp lệ", $"Email '{email}' đã tồn tại trong hệ thống!");
            }
        }
    }
}