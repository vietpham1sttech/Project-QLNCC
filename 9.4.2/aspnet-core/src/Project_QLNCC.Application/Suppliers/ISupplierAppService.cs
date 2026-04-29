using Abp.Application.Services;
using Project_QLNCC.Suppliers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_QLNCC.Suppliers
{
    public interface ISupplierAppService : IAsyncCrudAppService<SupplierDto, int, SearchSupplierInput, CreateSupplierInput, UpdateSupplierInput>
    {
    }
}
