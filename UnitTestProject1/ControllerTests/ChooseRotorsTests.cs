using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnigmaMachineRestApi.Controllers;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using EnigmaMachineRestApi.Models;

namespace UnitTestProject1.ControllerTests
{
    [TestClass]
    public class ChooseRotorsTests
    {
        [TestMethod]
        public void ChooseRotors_ShouldReturn200Ok_WhenPassedRotors123()
        {
            // Arrange
            var controller = new EnigmaController();
            controller.Request = new HttpRequestMessage();

            // Act
            HttpResponseMessage message = controller.ChooseRotors("123");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, message.StatusCode);
        }

        [TestMethod]
        public void ChooseRotors_ShouldReturn200Ok_WhenPassedRotors321()
        {
            // Arrange
            var controller = new EnigmaController();
            controller.Request = new HttpRequestMessage();

            // Act
            HttpResponseMessage message = controller.ChooseRotors("321");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, message.StatusCode);
        }

        [TestMethod]
        public void ChooseRotors_ShouldReturn200Ok_WhenPassedRotors254()
        {
            // Arrange
            var controller = new EnigmaController();
            controller.Request = new HttpRequestMessage();

            // Act
            HttpResponseMessage message = controller.ChooseRotors("254");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, message.StatusCode);
        }

        [TestMethod]
        public void ChooseRotors_ShouldReturn400_WhenPassedRotors1234()
        {
            // Arrange
            var controller = new EnigmaController();
            controller.Request = new HttpRequestMessage();

            // Act
            HttpResponseMessage message = controller.ChooseRotors("1234");

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, message.StatusCode);
        }

        [TestMethod]
        public void ChooseRotors_ShouldReturn400_WhenPassedRotors12()
        {
            // Arrange
            var controller = new EnigmaController();
            controller.Request = new HttpRequestMessage();

            // Act
            HttpResponseMessage message = controller.ChooseRotors("12");

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, message.StatusCode);
        }

        [TestMethod]
        public void ChooseRotors_ShouldReturn400_WhenPassedRotorsABC()
        {
            // Arrange
            var controller = new EnigmaController();
            controller.Request = new HttpRequestMessage();

            // Act
            HttpResponseMessage message = controller.ChooseRotors("ABC");

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, message.StatusCode);
        }

    }
}
