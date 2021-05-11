using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using dotNetCore5.Business.Dtos;
using dotNetCore5.Business.Infrastructure.Mappings;
using dotNetCore5.Business.IServices;
using dotNetCore5.Business.Services;
using dotNetCore5.DataAccess.DbModel;
using dotNetCore5.DataAccess.IRepositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace dotNetCore5.Business.Tests
{
    [TestClass]
    public class CustomerServiceTests
    {
        private IMapper _mapper;
        private ICustomerRepository _customerRepository;

        public CustomerServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ServiceMappingProfile>();
            });

            this._mapper = config.CreateMapper();
            this._customerRepository = Substitute.For<ICustomerRepository>();
        }

        private ICustomerService GetSystemUnderTest()
        {
            return new CustomerService(this._mapper, this._customerRepository);
        }

        //---------------------------------------------------------------

        [TestMethod]
        [Owner("Taco")]
        [TestCategory("CustomerService")]
        [TestProperty("CustomerService", "GetCustomerListAsync")]
        public async Task GetCustomerListAsync_輸入customerIds_應該回傳資料()
        {
            // arrange
            var customerIds = new[]
            {
                "ALFKI",
                "ANATR",
                "ANTON"
            };

            IEnumerable<CustomersDbModel> customersDataModels = new[]
            {
                new CustomersDbModel
                {
                    CustomerID = "ALFKI",
                    Country = "Germany",
                    City = "Berlin"
                },
                new CustomersDbModel
                {
                    CustomerID = "ANATR",
                    Country = "Mexico",
                    City = "Mexico D.F."
                },
                new CustomersDbModel
                {
                    CustomerID = "ANTON",
                    Country = "Mexico",
                    City = "Mexico D.F."
                },
            };

            this._customerRepository.GetCustomerListAsync(customerIds).Returns(customersDataModels);

            var sut = GetSystemUnderTest();

            // expected
            IEnumerable<CustomersDto> expected = new[]
            {
                new CustomersDto
                {
                    CustomerID = "ALFKI",
                    Country = "Germany",
                    City = "Berlin"
                },
                new CustomersDto
                {
                    CustomerID = "ANATR",
                    Country = "Mexico",
                    City = "Mexico D.F."
                },
                new CustomersDto
                {
                    CustomerID = "ANTON",
                    Country = "Mexico",
                    City = "Mexico D.F."
                },
            };

            // act
            var actual = await sut.GetCustomerListAsync(customerIds);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        [Owner("Taco")]
        [TestCategory("CustomerService")]
        [TestProperty("CustomerService", "GetCustomerListAsync")]
        public async Task GetCustomerListAsync_customerIds_null_應該回傳ArgumentNullException()
        {
            // arrange
            string[] customerIds = null;

            var sut = GetSystemUnderTest();

            // act
            Action actual = async () => await sut.GetCustomerListAsync(customerIds);

            // assert
            actual.Should().Throw<ArgumentNullException>()
                  .And.ParamName.Should().Be(nameof(customerIds));
        }

        [TestMethod]
        [Owner("Taco")]
        [TestCategory("CustomerService")]
        [TestProperty("CustomerService", "GetCustomerListAsync")]
        public async Task CreateCustomerAsync_新增成功_應該回傳1()
        {
            // arrange
            var customersCreateDto = new CustomersCreateDto
            {
                CustomerID = "Taco1",
                CompanyName = "Alfreds Futterkiste"
            };

            this._customerRepository.CreateCustomerAsync(Arg.Is<CustomersCreateDbModel>
            (
                c => c.CustomerID == "Taco1" && c.CompanyName == "Alfreds Futterkiste"
            )).Returns(1);

            var sut = GetSystemUnderTest();

            // expected
            int expected = 1;

            // act
            var actual = await sut.CreateCustomerAsync(customersCreateDto);

            // assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [Owner("Taco")]
        [TestCategory("CustomerService")]
        [TestProperty("CustomerService", "UpdateCustomerAsync")]
        public async Task UpdateCustomerAsync_更新成功_應該回傳1() 
        {
            // arrange
            var customersUpdateDto = new CustomersUpdateDto
            {
                CustomerID = "Taco1",
                CompanyName = "Alfreds Futterkiste"
            };

            var customersUpdateDbModel = new CustomersUpdateDbModel
            {
                CustomerID = "Taco1",
                CompanyName = "Alfreds Futterkiste"
            };

            this._customerRepository.UpdateCustomerAsync(Arg.Is<CustomersUpdateDbModel>
            (
                c => c.CustomerID == "Taco1" && c.CompanyName == "Alfreds Futterkiste"
            )).Returns(1);

            var sut = GetSystemUnderTest();

            // expected
            int expected = 1;

            // act
            var actual = await sut.UpdateCustomerAsync(customersUpdateDto);

            // assert
            actual.Should().Be(expected);
        }
    }
}
