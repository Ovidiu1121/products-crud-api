using Moq;
using ProductsCrudApi.Dto;
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
    public class TestCommandService
    {
        Mock<IProductRepository> _mock;
        IProductCommandService _service;

        public TestCommandService()
        {
            _mock = new Mock<IProductRepository>();
            _service = new ProductCommandService(_mock.Object);
        }

        [Fact]
        public async Task Create_InvalidData()
        {
            var create = new CreateProductRequest
            {
                Name="test",
                Price=0,
                Stock=0,
                Producer="Test"
            };

            var negativePrice = new CreateProductRequest
            {
                Name="test",
                Price=-1,
                Stock=0,
                Producer="Test"
            };

            var negativeStock = new CreateProductRequest
            {
                Name="test",
                Price=0,
                Stock=-1,
                Producer="Test"
            };

            var product = TestProductFactory.CreateProduct(5);

            _mock.Setup(repo => repo.GetByPriceAsync(-1)).ReturnsAsync(product);
            _mock.Setup(repo => repo.GetByStokAsync(-1)).ReturnsAsync(product);
            _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync(product);

            var priceException = await Assert.ThrowsAsync<InvalidPrice>(() => _service.CreateProduct(negativePrice));
            var stockException = await Assert.ThrowsAsync<InvalidStock>(() => _service.CreateProduct(negativeStock));
            var nameException = await Assert.ThrowsAsync<ItemAlreadyExists>(() => _service.CreateProduct(create));


            Assert.Equal(Constants.INVALID_PRICE, priceException.Message);
            Assert.Equal(Constants.INVALID_STOCK,stockException.Message);
            Assert.Equal(Constants.PRODUCT_ALREADY_EXIST,nameException.Message);
        }

        [Fact]
        public async Task Create_ReturnProduct()
        {
            var create = new CreateProductRequest
            {
                Name="test",
                Price=0,
                Stock=0,
                Producer="Test"
            };

            var product = TestProductFactory.CreateProduct(5);

            _mock.Setup(repo=>repo.CreateProduct(It.IsAny<CreateProductRequest>())).ReturnsAsync(product);

            var result= await _service.CreateProduct(create);

            Assert.NotNull(result);
            Assert.Equal(result, product);

        }
        
        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.DeleteProductById(It.IsAny<int>())).ReturnsAsync((Product)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeleteProduct(5));

            Assert.Equal(Constants.PRODUCT_DOES_NOT_EXIST,exception.Message);

        }

        [Fact]
        public async Task Delete_ValidData()
        {

            var product=TestProductFactory.CreateProduct(1);

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

            var result=await _service.DeleteProduct(1);

            Assert.NotNull(result);
            Assert.Equal(product, result);

        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var update = new UpdateProductRequest
            {
                Price=0,
                Stock=0,
                Producer="Test"
            };

            var product = TestProductFactory.CreateProduct(5);

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Product)null);

            var idException = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateProduct(1, update));

            Assert.Equal(Constants.PRODUCT_DOES_NOT_EXIST, idException.Message);

        }

        [Fact]
        public async Task Update_InvalidData()
        {
            var negativePrice = new UpdateProductRequest
            {
                Price=-1,
                Stock=0,
                Producer="Test"
            };

            var negativeStock = new UpdateProductRequest
            {
                Price=0,
                Stock=-1,
                Producer="Test"
            };

            var product = TestProductFactory.CreateProduct(5);

            _mock.Setup(repo => repo.GetByPriceAsync(-1)).ReturnsAsync(product);
            _mock.Setup(repo => repo.GetByStokAsync(-1)).ReturnsAsync(product);

            var priceException = await Assert.ThrowsAsync<InvalidPrice>(() => _service.UpdateProduct(1, negativePrice));
            var stockException = await Assert.ThrowsAsync<InvalidStock>(() => _service.UpdateProduct(1, negativeStock));

            Assert.Equal(Constants.INVALID_PRICE, priceException.Message);
            Assert.Equal(Constants.INVALID_STOCK, stockException.Message);


        }

        [Fact]
        public async Task Update_ValidData()
        {
            var update = new UpdateProductRequest
            {
                Price=0,
                Stock=0,
                Producer="Test"
            };

            var product = TestProductFactory.CreateProduct(5);

            product.Price = update.Price.Value;
            product.Stock = update.Stock.Value;

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(product);
            _mock.Setup(repo => repo.UpdateProduct(It.IsAny<int>(), It.IsAny<UpdateProductRequest>())).ReturnsAsync(product);

            var result = await _service.UpdateProduct(5, update);

            Assert.NotNull(result);
            Assert.Equal(product, result);
        }


    }
}
