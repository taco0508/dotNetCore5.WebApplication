using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotNetCore5.Business.Dtos;
using dotNetCore5.Business.IServices;
using dotNetCore5.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace dotNetCore5.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;

        public CustomerController(
            IMapper mapper,
            ICustomerService customerService)
        {
            this._mapper = mapper;
            this._customerService = customerService;
        }

        /// <summary>
        /// 取得客戶清單(單筆)
        /// </summary>
        /// <param name="customerId">客戶編號</param>
        /// <returns></returns>
        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomersViewModel>> GetCustomerListAsync(string customerId)
        {
            var data = await this._customerService.GetCustomerListAsync(new[] { customerId });
            var result = this._mapper.Map<IEnumerable<CustomersDto>, IEnumerable<CustomersViewModel>>(data);
            return Ok(result);
        }

        /// <summary>
        /// 取得客戶清單(多筆)
        /// </summary>
        /// <param name="customerIds">客戶編號(多筆)</param>
        /// <returns></returns>
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<CustomersViewModel>>> GetCustomerListAsync([FromBody] IEnumerable<string> customerIds)
        {
            var data = await this._customerService.GetCustomerListAsync(customerIds);
            var result = this._mapper.Map<IEnumerable<CustomersViewModel>>(data);

            var o_result = result.OrderBy(x => x.Address);

            return Ok(o_result);
        }

        /// <summary>
        /// 新增客戶資料
        /// </summary>
        /// <param name="customersCreateViewModel">客戶CreateViewModel</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateCustomerAsync([FromBody] CustomersCreateViewModel customersCreateViewModel)
        {
            var data = this._mapper.Map<CustomersCreateDto>(customersCreateViewModel);
            var result = await this._customerService.CreateCustomerAsync(data);
            return Ok(result);
        }

        /// <summary>
        /// 更新客戶資料
        /// </summary>
        /// <param name="customersUpdateViewModel">客戶UpdateViewModel</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<int>> UpdateCustomerAsync([FromBody] CustomersUpdateViewModel customersUpdateViewModel)
        {
            var data = this._mapper.Map<CustomersUpdateDto>(customersUpdateViewModel);
            var result = await this._customerService.UpdateCustomerAsync(data);
            return Ok(result);
        }

        /// <summary>
        /// 刪除客戶資料
        /// </summary>
        /// <param name="customerId">客戶編號</param>
        /// <returns></returns>
        [HttpDelete("{customerId}")]
        public async Task<ActionResult<int>> DeleteCustomerAsync(string customerId)
        {
            var result = await this._customerService.DeleteCustomerAsync(customerId);
            return Ok(result);
        }
    }
}
