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
        private readonly SupplierValidator _validator;

        public SupplierAppService(IRepository<Supplier, int> repository, SupplierValidator validator)
            : base(repository)
        {
            _validator = validator;
        }

        // Hàm tìm kiếm theo trường thông tin Name, email, số điện thoại
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
            if (!string.IsNullOrWhiteSpace(input.Address))
            {
                query = query.Where(x => x.Address != null && x.Address.Contains(input.Address));
            }
            return query;
        }

        // Hàm Create Supplier
        public override async Task<SupplierDto> CreateAsync(CreateSupplierInput input)
        {
            await _validator.Validate(input.Code, input.Email, input.TaxCode);
            return await base.CreateAsync(input);
        }

        // Hàm cập nhật thông tin Supplier
        public override async Task<SupplierDto> UpdateAsync(UpdateSupplierInput input)
        {
            await _validator.Validate(input.Code, input.Email, input.TaxCode, input.Id);
            return await base.UpdateAsync(input);
        }

        // Hàm xoá Supplier
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            await base.DeleteAsync(input);
        }

        public async Task<SupplierDto> Get(EntityDto<int> input)
        {
            var supplier = await Repository.GetAsync(input.Id);
            return ObjectMapper.Map<SupplierDto>(supplier);
        }

    }
}