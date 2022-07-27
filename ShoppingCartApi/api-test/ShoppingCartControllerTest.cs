using Microsoft.AspNetCore.Mvc;
using ShoppingCartApi.Controllers;
using ShoppingCartApi.Models;
using ShoppingCartApi.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace api_test
{
    public class ShoppingCartControllerTest
    {
        private readonly ShoppingCartController _controller;
        private readonly ShoppingCartService _service;

        public ShoppingCartControllerTest()
        {
            _service = new ShoppingCartService();
            _controller = new ShoppingCartController(_service);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            var okResult = _controller.Get();
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            var okResult = _controller.Get() as OkObjectResult;
            var items = Assert.IsType<List<ShoppingItem>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            var notFoundResult = _controller.Get(Guid.NewGuid());
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            var testGuid = new Guid("87cc8bdb-d335-460d-95a2-cdca1d7c4ac8");
            var okResult = _controller.Get(testGuid);
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            var testGuid = new Guid("87cc8bdb-d335-460d-95a2-cdca1d7c4ac8");
            var okResult = _controller.Get(testGuid) as OkObjectResult;
            Assert.IsType<ShoppingItem>(okResult.Value);
            Assert.Equal(testGuid, (okResult.Value as ShoppingItem).Id);
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            var nameMissingItem = new ShoppingItem()
            {
                Manufacturer = "Godrej",
                Price = 12
            };

            _controller.ModelState.AddModelError("Name", "Required");
            var badResponse = _controller.Post(nameMissingItem);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            ShoppingItem testItem = new ShoppingItem()
            {
                Name = "Guinness original 6 pack",
                Manufacturer = "Guinness",
                Price = 12
            };

            var createdResponse = _controller.Post(testItem);
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            var notExistingGuid = Guid.NewGuid();
            var badResponse = _controller.Remove(notExistingGuid);
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Remove_ExistingGuidPassed_ReturnsNoContentResult()
        {
            var testGuid = new Guid("87cc8bdb-d335-460d-95a2-cdca1d7c4ac8");

            var noContentResult = _controller.Remove(testGuid);

            Assert.IsType<NoContentResult>(noContentResult);
        }


    }
}
