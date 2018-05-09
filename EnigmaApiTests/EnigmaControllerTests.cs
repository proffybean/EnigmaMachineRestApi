using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnigmaMachineRestApi;
using EnigmaMachineRestApi.Controllers;
using EnigmaMachineRestApi.Models;
using System.Net.Http;
using System.Web;
using System.Net;

namespace EnigmaApiTests
{
    [TestClass]
    public class EnigmaControllerTests
    {
        [TestMethod]
        public void SetRotor_Should_When()
        {
            // Arrange
            var enigmaController = new EnigmaController();
            RotorDto rotorDto = new RotorDto {
                Mapping = "bdfhjlcprtxvznyeiwgakmusqo",
                Rotate = true,
                AdjacentRotorAdvanceOffset = 21,
                InitialDialSetting = 'z'
            };

            // Act
            //HttpResponseMessage response = enigmaController.SetRotor(rotorDto, 3);
            enigmaController.SetPlugboard

            // Assert
        }
    }
}
