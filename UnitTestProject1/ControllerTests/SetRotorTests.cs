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
    public class SetRotorTests
    {
        [TestMethod]
        public void SetRotor_ShouldReturn200Ok_WhenSettingRotor1ToA()
        {
            // Arrange
            var controller = new EnigmaController();
            controller.Request = new HttpRequestMessage();

            string jsonRotor = @"
              {
                'RotorNum':1,
                'Setting':'a'
              }";

            HttpResponseMessage message = controller.ChooseRotors("123");

            // Act
            message = controller.SetRotor(JsonConvert.DeserializeObject<RotorDto>(jsonRotor), 1);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, message.StatusCode);
        }

        [TestMethod]
        public void SetRotor_ShouldReturn200Ok_WhenSettingRotor2ToZ()
        {
            // Arrange
            var controller = new EnigmaController();
            controller.Request = new HttpRequestMessage();

            string jsonRotor = @"
              {
                'RotorNum':2,
                'Setting':'z'
              }";

            HttpResponseMessage message = controller.ChooseRotors("123");

            // Act
            message = controller.SetRotor(JsonConvert.DeserializeObject<RotorDto>(jsonRotor), 2);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, message.StatusCode);
        }

        [TestMethod]
        public void SetRotor_ShouldReturn200Ok_WhenSettingRotor3ToK()
        {
            // Arrange
            var controller = new EnigmaController();
            controller.Request = new HttpRequestMessage();

            string jsonRotor = @"
              {
                'RotorNum':3,
                'Setting':'k'
              }";

            HttpResponseMessage message = controller.ChooseRotors("123");

            // Act
            message = controller.SetRotor(JsonConvert.DeserializeObject<RotorDto>(jsonRotor), 2);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, message.StatusCode);
        }

        /// <remark>
        ///  This could throw an error, but currently the EnigmaMAchine doesn't
        ///  throw when setting an unknown dial.
        /// </remark>
        [TestMethod]
        public void SetRotor_ShouldReturn200Ok_WhenSettingIncorrectRotor()
        {
            // Arrange
            var controller = new EnigmaController();
            controller.Request = new HttpRequestMessage();

            string jsonRotor = @"
              {
                'RotorNum':4,
                'Setting':'k'
              }";

            HttpResponseMessage message = controller.ChooseRotors("123");

            // Act
            message = controller.SetRotor(JsonConvert.DeserializeObject<RotorDto>(jsonRotor), 2);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, message.StatusCode);
        }
    }
}
