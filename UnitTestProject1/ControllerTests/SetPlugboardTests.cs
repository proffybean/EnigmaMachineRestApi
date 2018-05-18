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
    public class SetPlugboardTests
    {
        [TestMethod]
        public void SetPlugboard_ShouldReturn200Ok_WhenSetting2Plugs()
        {
            // Arrange
            var controller = new EnigmaController();
            controller.Request = new HttpRequestMessage();

            string jsonPlugboard = @"
              {
                'a':'c',
                'k':'f'
              }";

            // Act
            var message = controller.SetPlugboard(JsonConvert.DeserializeObject<PlugboardDto>(jsonPlugboard));

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, message.StatusCode);
        }

        [TestMethod]
        public void SetPlugboard_ShouldReturn200Ok_WhenSetting0Plugs()
        {
            // Arrange
            var controller = new EnigmaController();
            controller.Request = new HttpRequestMessage();

            string jsonPlugboard = @"
              {
              }";

            // Act
            var message = controller.SetPlugboard(JsonConvert.DeserializeObject<PlugboardDto>(jsonPlugboard));

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, message.StatusCode);
        }

        [TestMethod]
        public void SetPlugboard_ShouldReturn200Ok_WhenSetting10Plugs()
        {
            // Arrange
            var controller = new EnigmaController();
            controller.Request = new HttpRequestMessage();

            string jsonPlugboard = @"
              {
                'a':'b',
                'c':'d',
                'e':'f',
                'g':'h',
                'i':'j',
                'k':'l',
                'm':'n',
                'o':'p',
                'q':'r',
                's':'t',
              }";

            // Act
            var message = controller.SetPlugboard(JsonConvert.DeserializeObject<PlugboardDto>(jsonPlugboard));

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, message.StatusCode);
        }

        /// <remarks>
        /// This should fail but I don't have an exception setup for 11 plugs
        /// </remarks>
        [TestMethod]
        public void SetPlugboard_ShouldReturn200Ok_WhenSetting11Plugs()
        {
            // Arrange
            var controller = new EnigmaController();
            controller.Request = new HttpRequestMessage();

            string jsonPlugboard = @"
              {
                'a':'b',
                'c':'d',
                'e':'f',
                'g':'h',
                'i':'j',
                'k':'l',
                'm':'n',
                'o':'p',
                'q':'r',
                's':'t',
                'z':'x'
              }";

            // Act
            var message = controller.SetPlugboard(JsonConvert.DeserializeObject<PlugboardDto>(jsonPlugboard));

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, message.StatusCode);
        }

    }
}
