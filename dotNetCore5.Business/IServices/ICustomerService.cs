using System.Collections.Generic;
using System.Threading.Tasks;
using dotNetCore5.Business.Dtos;

namespace dotNetCore5.Business.IServices
{
    public interface ICustomerService
    {
        /// <summary>
        /// 取得客戶清單
        /// </summary>
        /// <param name="customerIds">客戶編號(多筆)</param>
        /// <returns></returns>
        Task<IEnumerable<CustomersDto>> GetCustomerListAsync(IEnumerable<string> customerIds);

        /// <summary>
        /// 新增客戶資料
        /// </summary>
        /// <param name="customersCreateDto">客戶CreateDto</param>
        /// <returns></returns>
        Task<int> CreateCustomerAsync(CustomersCreateDto customersCreateDto);

        /// <summary>
        /// 更新客戶資料
        /// </summary>
        /// <param name="customersUpdateDto">客戶UpdateDto</param>
        /// <returns></returns>
        Task<int> UpdateCustomerAsync(CustomersUpdateDto customersUpdateDto);

        /// <summary>
        /// 刪除客戶資料
        /// </summary>
        /// <param name="customerId">客戶編號</param>
        /// <returns></returns>
        Task<int> DeleteCustomerAsync(string customerId);
    }
}
