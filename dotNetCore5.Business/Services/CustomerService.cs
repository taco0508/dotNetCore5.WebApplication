using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using dotNetCore5.Business.Dtos;
using dotNetCore5.Business.IServices;
using dotNetCore5.DataAccess.DbModel;
using dotNetCore5.DataAccess.IRepositories;

namespace dotNetCore5.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(
            IMapper mapper,
            ICustomerRepository customerRepository)
        {
            this._mapper = mapper;
            this._customerRepository = customerRepository;
        }

        /// <summary>
        /// 取得客戶清單
        /// </summary>
        /// <param name="customerIds">客戶編號(多筆)</param>
        /// <returns></returns>
        public async Task<IEnumerable<CustomersDto>> GetCustomerListAsync(IEnumerable<string> customerIds)
        {
            var data = await this._customerRepository.GetCustomerListAsync(customerIds);
            var result = this._mapper.Map<IEnumerable<CustomersDbModel>, IEnumerable<CustomersDto>>(data);
            return result;
        }

        /// <summary>
        /// 新增客戶
        /// </summary>
        /// <param name="customersCreateDto">客戶CreateDto</param>
        /// <returns></returns>
        public async Task<int> CreateCustomerAsync(CustomersCreateDto customersCreateDto)
        {
            var data = this._mapper.Map<CustomersCreateDto, CustomersCreateDbModel>(customersCreateDto);
            var result = await this._customerRepository.CreateCustomerAsync(data);
            return result;
        }

        /// <summary>
        /// 更新客戶資料
        /// </summary>
        /// <param name="customersUpdateDto">客戶UpdateDto</param>
        /// <returns></returns>
        public async Task<int> UpdateCustomerAsync(CustomersUpdateDto customersUpdateDto)
        {
            var data = this._mapper.Map<CustomersUpdateDto, CustomersUpdateDbModel>(customersUpdateDto);
            var result = await this._customerRepository.UpdateCustomerAsync(data);
            return result;
        }

        /// <summary>
        /// 刪除客戶資料
        /// </summary>
        /// <param name="customerId">客戶編號</param>
        /// <returns></returns>
        public async Task<int> DeleteCustomerAsync(string customerId)
        {
            var result = await this._customerRepository.DeleteCustomerAsync(customerId);
            return result;
        }
    }
}
