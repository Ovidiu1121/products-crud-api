using Moq;
using ProductsCrudApi.Products.Model;
using ProductsCrudApi.Products.Repository.interfaces;
using ProductsCrudApi.Products.Service;
using ProductsCrudApi.Products.Service.Interfaces;
using ProductsCrudApi.System.Constant;
using ProductsCrudApi.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests
{
    public class TestQueryService
    {

        Mock<IProductRepository> _mock;
        IProductQueryService _service;

        public TestQueryService()
        {
            _mock=new Mock<IProductRepository>();
            _service=new ProductQueryService(_mock.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {

            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Product>());

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetAllProducts());

            Assert.Equal(Constants.NO_PRODUCTS_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetAll_ReturnProducts()
        {

            var products = TestProductFactory.CreateProducts(5);

            _mock.Setup(repo=>repo.GetAllAsync()).ReturnsAsync(products);

            var result= await _service.GetAllProducts();

            Assert.NotNull(result);
            Assert.Contains(products[1], result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {

            _mock.Setup(repo=>repo.GetByIdAsync(1)).ReturnsAsync((Product)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetById(1));

            Assert.Equal(Constants.PRODUCT_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetById_ReturnProduct()
        {
            var product=TestProductFactory.CreateProduct(5);

            _mock.Setup(repo=>repo.GetByIdAsync(1)).ReturnsAsync(product);

            var result=await _service.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(product,result);


        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {

            _mock.Setup(repo=>repo.GetByNameAsync("")).ReturnsAsync((Product)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByName(""));

            Assert.Equal(Constants.PRODUCT_DOES_NOT_EXIST,exception.Message);

        }

        [Fact]
        public async Task GetByName_ReturnProduct()
        {

            var product = TestProductFactory.CreateProduct(5);

            product.Name = "test";

            _mock.Setup(repo=>repo.GetByNameAsync("test")).ReturnsAsync(product);

            var result=await _service.GetByName("test");

            Assert.NotNull(result);
            Assert.Equal(product, result);

        }

        [Fact]
        public async Task GetIdOrderedByPriceDesc_ItemsDoesNotExist()
        {

            _mock.Setup(repo => repo.GetIdOrderedByPriceDescAsync()).ReturnsAsync(new List<int>());

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetIdOrderedByPriceDesc());

            Assert.Equal(Constants.NO_PRODUCTS_EXIST,exception.Message);

        }

        [Fact]
        public async Task GetIdOrderedByPriceDesc_ReutrnPrices()
        {
            var prices=TestProductFactory.CreatePrices(5);

            _mock.Setup(repo => repo.GetIdOrderedByPriceDescAsync()).ReturnsAsync(prices);

            var result = await _service.GetIdOrderedByPriceDesc();

            Assert.NotNull(result);
            Assert.Equal(prices, result);

        }




    }
}
