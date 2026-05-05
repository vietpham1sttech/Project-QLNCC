using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_QLNCC.Suppliers
{
    public class SupplierValidator : ITransientDependency
    {
        private readonly IRepository<Supplier, int> _repository;
        public SupplierValidator(IRepository<Supplier, int> repository)
        {
            _repository = repository;
        }
        public async Task Validate(string code, string email, string taxCode, int? id = null)
        {
            code = code?.Trim().ToUpper();
            email = email?.Trim().ToLower();
            taxCode = taxCode?.Trim();

            var existed = await _repository.GetAll()
                .Where(x => !id.HasValue || x.Id != id.Value)
                .FirstOrDefaultAsync(x =>
                    x.Code == code ||
                    x.Email == email ||
                    x.TaxCode == taxCode
                );

            if (existed != null)
            {
                if (existed.Code == code)
                    throw new UserFriendlyException("Dữ liệu không hợp lệ", $"Mã '{code}' đã tồn tại!");

                if (existed.Email == email)
                    throw new UserFriendlyException("Dữ liệu không hợp lệ", $"Email '{email}' đã tồn tại!");

                if (existed.TaxCode == taxCode)
                    throw new UserFriendlyException("Dữ liệu không hợp lệ", $"Mã số thuế '{taxCode}' đã tồn tại!");
            }
        }
    }
}
