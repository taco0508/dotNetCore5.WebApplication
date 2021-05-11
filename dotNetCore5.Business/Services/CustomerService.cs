using System;
using System.Collections.Generic;
using System.Linq;
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
            if (customerIds is null)
            {
                throw new ArgumentNullException(nameof(customerIds));
            }

            if (!customerIds.Any()) 
            {
                throw new ArgumentException($"'{nameof(customerIds)}' 至少要有一筆資料", nameof(customerIds));
            }

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
            if (customersCreateDto is null)
            {
                throw new System.ArgumentNullException(nameof(customersCreateDto));
            }

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
            if (customersUpdateDto is null)
            {
                throw new ArgumentNullException(nameof(customersUpdateDto));
            }

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
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException($"'{nameof(customerId)}' 不得為 Null 或空白字元。", nameof(customerId));
            }

            var result = await this._customerRepository.DeleteCustomerAsync(customerId);
            return result;
        }
    }
}
