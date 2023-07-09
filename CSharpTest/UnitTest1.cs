using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Interfaces.Streaming;
using Microsoft.Analytics.Types.Sql;
using Microsoft.Analytics.UnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    [TestClass]
    public class MercaderiaControllerTests
    {
        [TestMethod]
        public async Task GetSerach_Returns_NotFound_If_Result_Is_Null()
        {
            // Arrange
            var serviceMock = new Mock<IMercaderiaService>();
            serviceMock.Setup(s => s.GetBuscar(null, null, null)).ReturnsAsync(null);

            var controller = new MercaderiaController(serviceMock.Object);

            // Act
            var result = await controller.GetSerach(null, null, null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task GetSerach_Returns_JsonResult_If_Result_Is_Not_Null()
        {
            // Arrange
            var expected = new List<Something>() { new Something() { Id = 1, Name = "Test" } };
            var serviceMock = new Mock<IMercaderiaService>();
            serviceMock.Setup(s => s.GetBuscar(null, null, null)).ReturnsAsync(expected);

            var controller = new MercaderiaController(serviceMock.Object);

            // Act
            var result = await controller.GetSerach(null, null, null) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.Value);
            Assert.AreEqual(201, result.StatusCode);
        }

        [TestMethod]
        public async Task GetSerach_Returns_StatusCode_500_If_Exception_Is_Thrown()
        {
            // Arrange
            var serviceMock = new Mock<IMercaderiaService>();
            serviceMock.Setup(s => s.GetBuscar(null, null, null)).ThrowsAsync(new Exception("Something went wrong"));

            var controller = new MercaderiaController(serviceMock.Object);

            // Act
            var result = await controller.GetSerach(null, null, null) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
        }
    }

}
