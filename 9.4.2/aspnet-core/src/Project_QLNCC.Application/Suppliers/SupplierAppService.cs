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

        // Hàm tìm kiếm theo trường thông tin tên, email, số điện thoại
        protected override IQueryable<Supplier> CreateFilteredQuery(SearchSupplierInput input)
        {
            var query = base.CreateFilteredQuery(input);
            if (!string.IsNullOrWhiteSpace(input.Keyword))
            {
                query = query.Where(x =>
                    x.Code.Contains(input.Keyword) ||
                    x.Name.Contains(input.Keyword) ||
                    x.Email.Contains(input.Keyword) ||
                   (x.TaxCode != null && x.TaxCode.Contains(input.Keyword)) ||
                   (x.Phone != null && x.Phone.Contains(input.Keyword))
                );
            }
            if (input.IsActive.HasValue)
            {
                query = query.Where(x => x.IsActive == input.IsActive);
            }
            return query;
        }

        // Hàm thêm mới nhà cung cấp
        public override async Task<SupplierDto> CreateAsync(CreateSupplierInput input)
        {
            await CheckEmailExisted(input.Email);
            await CheckCodeExisted(input.Code);
            await CheckTaxCodeExisted(input.TaxCode);
            return await base.CreateAsync(input);
        }

        // Hàm cập nhật thông tin nhà cung cấp
        public override async Task<SupplierDto> UpdateAsync(UpdateSupplierInput input)
        {
            await CheckEmailExisted(input.Email, input.Id);
            await CheckCodeExisted(input.Code, input.Id);
            await CheckTaxCodeExisted(input.TaxCode, input.Id);
            return await base.UpdateAsync(input);
        }

        // Hàm xoá nhà cung cấp
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            await base.DeleteAsync(input);
        }

        // Hàm kiểm tra trùng email của tài khoản
        private async Task CheckEmailExisted(string email, int? id = null)
        {
            if (string.IsNullOrWhiteSpace(email)) return;

            // Tìm xem có NCC nào khác đã dùng email này chưa
            var isExisted = await Repository.GetAll().AnyAsync(x => x.Email == email && (!id.HasValue || x.Id != id.Value));

            if (isExisted)
            {
                throw new UserFriendlyException("Dữ liệu không hợp lệ", $"Email '{email}' đã tồn tại!");
            }
        }

        // Hàm kiểm tra trùng mã nhà cung cấp
        private async Task CheckCodeExisted(string code, int? id = null)
        {
            if (string.IsNullOrWhiteSpace(code)) return;
            // Tìm xem có NCC nào khác đã dùng mã nhà cung cấp này chưa
            var isExisted = await Repository.GetAll().AnyAsync(x => x.Code == code && (!id.HasValue || x.Id != id.Value));

            if (isExisted)
            {
                throw new UserFriendlyException("Dữ liệu không hợp lệ", $"Mã nhà cung cấp '{code}' đã tồn tại!");
            }
        }

        // Hàm kiểm tra trùng mã số thuế
        private async Task CheckTaxCodeExisted(string taxCode, int? id = null)
        {
            if (string.IsNullOrWhiteSpace(taxCode)) return;

            var isExisted = await Repository.GetAll()
                .AnyAsync(x => x.TaxCode == taxCode && (!id.HasValue || x.Id != id.Value));

            if (isExisted)
            {
                throw new UserFriendlyException("Dữ liệu không hợp lệ", $"Mã số thuế '{taxCode}' đã tồn tại!");
            }
        }
    }
}