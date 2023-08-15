using Microsoft.Extensions.Logging;
using Moq;
using NorthwindApi.Data.Repositories;
using NorthwindApi.Models;
using NorthwindApi.Services;
using NorthwindAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Tests
{
    internal class SupplierServiceShould
    {

        [Category("Happy Path")]
        [Category("GetSuppliers")]
        [Test]
        public async Task GetAllAsync_WhenThereAreSuppliers_ReturnsListOfSuppliers()
        {
            var mockRepository = GetRepository();
            var mockLogger = GetLogger();
            List<Supplier> suppliers = new List<Supplier> { It.IsAny<Supplier>() };
            Mock
                .Get(mockRepository)
                .Setup(sc => sc.GetAllAsync().Result)
                .Returns(suppliers);
            Mock
                .Get(mockRepository)
                .Setup(sc => sc.IsNull)
                .Returns(false);

            var _sut = new NorthwindService<Supplier>(mockLogger, mockRepository);
            var result = await _sut.GetAllAsync();
            Assert.That(result, Is.InstanceOf<IEnumerable<Supplier>>());
        }

        [Test]
        public async Task GetAsync_GivenVaildID_ReturnsSuppliers()
        {
            var mockRepository = GetRepository();
            var mockLogger = GetLogger();
            Supplier suppliers = new Supplier { };
            //It.IsAny<Supplier>() <----- is null

            Mock
                .Get(mockRepository)
                .Setup(sc => sc.FindAsync(It.IsAny<int>()).Result)
                .Returns(suppliers);
            Mock
                .Get(mockRepository)
                .Setup(sc => sc.IsNull)
                .Returns(false);

            var _sut = new NorthwindService<Supplier>(mockLogger, mockRepository);
            var result = await _sut.GetAsync(1);
            Assert.That(result, Is.InstanceOf<Supplier>());
        }


        [Test]
        public async Task CreateAsync_GivenVaildID_ReturnsSuppliers()
        {
            var mockRepository = new Mock<INorthwindRepository<Supplier>>();
            var mockLogger = GetLogger();
            Supplier suppliers = new Supplier { };

            var _sut = new NorthwindService<Supplier>(mockLogger, mockRepository.Object); //mockRepository.Object
            var status = await _sut.CreateAsync(suppliers);


            mockRepository.Verify(cs => cs.Add(suppliers), Times.Once);
            mockRepository.Verify(cs => cs.SaveAsync(), Times.Once);
            Assert.That(status, Is.EqualTo(true));
        }

        [Test]
        public async Task DeleteAsync_GivenVaildID_ReturnsTrue()
        {
            var mockRepository = new Mock<INorthwindRepository<Supplier>>();
            var mockLogger = GetLogger();
            int testingID = 1;
            Supplier suppliers = new Supplier { };
            mockRepository.Setup(cs => cs.FindAsync(testingID).Result)
                          .Returns(suppliers);

            var _sut = new NorthwindService<Supplier>(mockLogger, mockRepository.Object); //mockRepository.Object

            var status = await _sut.DeleteAsync(testingID);

            mockRepository.Verify(cs => cs.Remove(suppliers), Times.Once);
            mockRepository.Verify(cs => cs.FindAsync(1), Times.Once);
            mockRepository.Verify(cs => cs.SaveAsync(), Times.Once);
            Assert.That(status, Is.EqualTo(true));
        }

        [Test]
        public async Task UpdateAsync_GivenVaildIDAndSupplierObject_ReturnsTrue()
        {
            var mockRepository = new Mock<INorthwindRepository<Supplier>>();
            var mockLogger = GetLogger();
            int testingID = 1;
            Supplier suppliers = new Supplier { };

            var _sut = new NorthwindService<Supplier>(mockLogger, mockRepository.Object); //mockRepository.Object

            var status = await _sut.UpdateAsync(testingID, suppliers);

            mockRepository.Verify(cs => cs.Update(suppliers), Times.Once);
            mockRepository.Verify(cs => cs.SaveAsync(), Times.Once);
            Assert.That(status, Is.EqualTo(true));
        }





        private static ILogger<INorthwindService<Supplier>> GetLogger()
        {
            return Mock.Of<ILogger<INorthwindService<Supplier>>>(); //Mock of is an instance
        }



        private static INorthwindRepository<Supplier> GetRepository()
        {
            return Mock.Of<INorthwindRepository<Supplier>>();
        }
    }
}
