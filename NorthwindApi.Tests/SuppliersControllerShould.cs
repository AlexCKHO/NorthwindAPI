using Moq;
using NorthwindApi.Controllers;
using NorthwindApi.Models.DTO;
using NorthwindApi.Models;
using NorthwindAPI.Services;
using Microsoft.AspNetCore.Mvc;


namespace NorthwindApi.Tests
{
    internal class SuppliersControllerShould
    {

        [Test]
        public async Task GetSuppliers_WhenThereAreSuppliers_ReturnsListOfSupplierDTOs()
        {
            var mockService = new Mock<INorthwindService<Supplier>>();
            List<Supplier> suppliers = new List<Supplier> { Mock.Of<Supplier>(s => s.Products == Mock.Of<List<Product>>()) };

            mockService
            .Setup(sc => sc.GetAllAsync().Result)
            .Returns(suppliers);

            var sut = new SuppliersController(mockService.Object);
            var result = await sut.GetSuppliers();
            Assert.That(result.Value, Is.InstanceOf<IEnumerable<SupplierDTO>>());
        }

        [Test]
        public async Task GetSupplier_GiveVaildID_ReturnsASupplier()
        {
            var mockService = new Mock<INorthwindService<Supplier>>();
            var supplier = new Supplier { };
            int id = 1;

            mockService
            .Setup(sc => sc.GetAsync(id).Result)
            .Returns(supplier);


            var sut = new SuppliersController(mockService.Object);
            var result = await sut.GetSupplier(id);
            Assert.That(result.Value, Is.InstanceOf<SupplierDTO>());
        }



        [Test]
        public async Task GetProductsBySupplier_GiveVaildID_ReturnsListOfProduct()
        {
            var mockService = new Mock<INorthwindService<Supplier>>();
            List<Product> products = new List<Product> { Mock.Of<Product>() };
            var supplier = new Mock<Supplier> { };
            int id = 1;

            supplier
                .Setup(sc => sc.Products)
                .Returns(products);


            mockService
            .Setup(sc => sc.GetAsync(id).Result)
            .Returns(supplier.Object);

            var sut = new SuppliersController(mockService.Object);
            var result = await sut.GetProductsBySupplier(id);
            Assert.That(result.Value, Is.InstanceOf<List<ProductDTO>>());

        }

        [Test]
        public async Task PutSupplier_GiveVaildIDAndSupplier_ReturnsListOfProduct()
        {
            var mockService = new Mock<INorthwindService<Supplier>>();
            int id = 1;
            var supplier = new Supplier
            {
                SupplierId = 1,
                CompanyName = "Sparta Global",
                City = "Birmingham",
                Country = "UK",
                ContactName = "Nish Mandal",
                ContactTitle = "Manager"
            };

            mockService
                .Setup(cs => cs.UpdateAsync(1, supplier))
                .ReturnsAsync(true);

            var sut = new SuppliersController(mockService.Object);
            var result = await sut.PutSupplier(id, supplier);

            mockService.Verify(cs => cs.UpdateAsync(id, supplier), Times.Once);
            Assert.IsInstanceOf<NoContentResult>(result);
        }



        [Test]
        public async Task PostSupplier_GivenSupplier_ReturnsSupplier()
        {
            var mockService = new Mock<INorthwindService<Supplier>>();
            int id = 1;
            var supplier = new Supplier
            {
                CompanyName = "Sparta Global",
                City = "Birmingham",
                Country = "UK",
                ContactName = "Nish Mandal",
                ContactTitle = "Manager"
            };

            mockService
                .Setup(cs => cs.CreateAsync(supplier))
                .ReturnsAsync(true);

            var sut = new SuppliersController(mockService.Object);
            var result = await sut.PostSupplier(supplier);

            mockService.Verify(cs => cs.CreateAsync(supplier), Times.Once);
            Assert.That(result, Is.TypeOf<ActionResult<SupplierDTO>>());

        }

        [Test]
        public async Task DeleteSupplier_GivenSupplierID_ReturnsSupplier()
        {
            var mockService = new Mock<INorthwindService<Supplier>>(behavior: MockBehavior.Strict);
            Supplier supplier = new Supplier { };
            int id = 1;
            mockService
                .Setup(cs => cs.GetAsync(id).Result)
                .Returns(supplier);

            mockService
                .Setup(cs => cs.DeleteAsync(id))
                .ReturnsAsync(true);

            var sut = new SuppliersController(mockService.Object);
            var result = await sut.DeleteSupplier(id);

            mockService.Verify(cs => cs.DeleteAsync(id), Times.Once);
            Assert.IsInstanceOf<NoContentResult>(result);

        }
    }

}
