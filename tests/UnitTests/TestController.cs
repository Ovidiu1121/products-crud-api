using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductsCrudApi.Dto;
using ProductsCrudApi.Products.Controllers;
using ProductsCrudApi.Products.Controllers.Interfaces;
using ProductsCrudApi.Products.Model;
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
    public class TestController
    {
        Mock<IProductCommandService> _command;
        Mock<IProductQueryService> _query;
        ProductApiController _controller;

        public TestController()
        {
            _command = new Mock<IProductCommandService>();
            _query = new Mock<IProductQueryService>();
            _controller = new ProductController(_command.Object, _query.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {

            _query.Setup(repo => repo.GetAllProducts()).ThrowsAsync(new ItemDoesNotExist(Constants.PRODUCT_DOES_NOT_EXIST));

            var result = await _controller.GetAll();

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(404, notFound.StatusCode);
            Assert.Equal(Constants.PRODUCT_DOES_NOT_EXIST, notFound.Value);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {

            var products = TestProductFactory.CreateProducts(5);

            _query.Setup(repo => repo.GetAllProducts()).ReturnsAsync(products);

            var result=await _controller.GetAll();

            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            var productsAll = Assert.IsType<List<Product>>(okresult.Value);

            Assert.Equal(5, productsAll.Count);
            Assert.Equal(200, okresult.StatusCode);

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

            _command.Setup(repo => repo.CreateProduct(It.IsAny<CreateProductRequest>())).ThrowsAsync(new ItemAlreadyExists(Constants.PRODUCT_ALREADY_EXIST));

            var result = await _controller.CreateProduct(create);

            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(400, bad.StatusCode);
            Assert.Equal(Constants.PRODUCT_ALREADY_EXIST, bad.Value);


        }

        [Fact]
        public async Task Create_ValidData()
        {

            var create = new CreateProductRequest
            {
                Name="test",
                Price=0,
                Stock=0,
                Producer="Test"
            };

            var product = TestProductFactory.CreateProduct(5);

            product.Name=create.Name;
            product.Price=create.Price;
            product.Stock=create.Stock;
            product.Producer=create.Producer;   

            _command.Setup(repo => repo.CreateProduct(create)).ReturnsAsync(product);

            var result = await _controller.CreateProduct(create);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(product, okResult.Value);

        }

        [Fact]
        public async Task Update_InvalidDate()
        {

            var update = new UpdateProductRequest   
            {
                Price=0,
                Stock=0,
                Producer="Test"
            };

            _command.Setup(repo => repo.UpdateProduct(1, update)).ThrowsAsync(new ItemDoesNotExist(Constants.PRODUCT_DOES_NOT_EXIST));

            var result = await _controller.UpdateProduct(1, update);

            var bad = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(bad.StatusCode, 404);
            Assert.Equal(bad.Value, Constants.PRODUCT_DOES_NOT_EXIST);

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
            product.Price=update.Price.Value;
            product.Stock=update.Stock.Value;
            product.Producer=update.Producer;

            _command.Setup(repo => repo.UpdateProduct(5, update)).ReturnsAsync(product);

            var result = await _controller.UpdateProduct(5, update);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, product);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {

            _command.Setup(repo => repo.DeleteProduct(1)).ThrowsAsync(new ItemDoesNotExist(Constants.PRODUCT_DOES_NOT_EXIST));

            var result = await _controller.DeleteProduct(1);

            var notfound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notfound.StatusCode, 404);
            Assert.Equal(notfound.Value, Constants.PRODUCT_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var product = TestProductFactory.CreateProduct(1);

            _command.Setup(repo => repo.DeleteProduct(1)).ReturnsAsync(product);

            var result = await _controller.DeleteProduct(1);

            var okResult = Assert.IsType<AcceptedResult>(result.Result);

            Assert.Equal(202, okResult.StatusCode);
            Assert.Equal(product, okResult.Value);
        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {
            _query.Setup(repo => repo.GetByName("")).ThrowsAsync(new ItemDoesNotExist(Constants.PRODUCT_DOES_NOT_EXIST));

            var result = await _controller.GetByNameRoute("");

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(404, notFound.StatusCode);
            Assert.Equal(Constants.PRODUCT_DOES_NOT_EXIST, notFound.Value);


        }

        [Fact]
        public async Task GetByName_ReturnProduct()
        {

            var product=TestProductFactory.CreateProduct(1);

            product.Name="test";

            _query.Setup(repo => repo.GetByName("test")).ReturnsAsync(product);

            var result = await _controller.GetByNameRoute("test");

            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okresult.StatusCode);


        }




    }
}
