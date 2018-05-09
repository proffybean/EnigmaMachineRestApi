using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnigmaMachineRestApi;
using EnigmaMachineRestApi.Models;
using EnigmaMachineRestApi.Controllers;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SetRotor_Should_When()
        {
            // Arrange
            var enigmaController = new EnigmaController();
            RotorDto rotorDto = new RotorDto
            {
                RotorNum = 1,
                InitialDialSetting = 'z'
            };

            // Act
            HttpResponseMessage response = enigmaController.SetRotor(rotorDto, 3);

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void SetRotor_Should_WhenInputIsJSON()
        {
            // Arrange
            var enigmaController = new EnigmaController();
            string json = @"{
	                        'RotorNum':1,
	                        'Setting':'a'
                        }";

            RotorDto rotorDto = JsonConvert.DeserializeObject(json) as RotorDto;

            // Act
            HttpResponseMessage response = enigmaController.SetRotor(rotorDto, 3);

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
    }
}
