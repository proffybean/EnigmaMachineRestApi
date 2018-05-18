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
    public class EncryptTests
    {
        [TestMethod]
        public void Encrypt_ShouldReturn200Ok_WhenMessageIsProper()
        {
            // Arrange
            var controller = new EnigmaController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();

            string jsonEnigma = @"{
	                    'MachineName': 'Jeffs Enigma',
                        'Text':'hello enigma machine',
	                    'Rotor1':{
		                    'RotorNum':1,
		                    'Setting':'a'
                        },
	                    'Rotor2':{
		                    'RotorNum':2,
		                    'Setting':'a'
	                    },
	                    'Rotor3':{
		                    'RotorNum':3,
		                    'Setting':'z'
	                    },
	                    'Plugboard': {
	                        'Wiring': {
	    	                    'h':'s',
	    	                    'a':'e',
	    	                    'd':'j',
	    	                    'p':'q',
	    	                    'g':'m',
	    	                    'b':'c',
	    	                    'f':'i',
	    	                    'k':'n',
	    	                    'l':'o',
	    	                    'r':'t',
	                        }
	                    }
                    }";

            // Act
            var message = controller.Encrypt(JsonConvert.DeserializeObject<EnigmaMachineDto>(jsonEnigma), false);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, message.StatusCode);
            message.TryGetContentValue<string>(out string encryptedText);

            Assert.AreEqual("ocfkjlljkyburekwfj", encryptedText);
        }

        [TestMethod]
        public void Encrypt_ShouldReturn200Ok_WhenMessageIsProper_AndLeaveWhiteSpaceIsTrue()
        {
            // Arrange
            var controller = new EnigmaController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();

            string jsonEnigma = @"{
	                    'MachineName': 'Jeffs Enigma',
                        'Text':'hello enigma machine',
	                    'Rotor1':{
		                    'RotorNum':1,
		                    'Setting':'a'
                        },
	                    'Rotor2':{
		                    'RotorNum':2,
		                    'Setting':'a'
	                    },
	                    'Rotor3':{
		                    'RotorNum':3,
		                    'Setting':'z'
	                    },
	                    'Plugboard': {
	                        'Wiring': {
	    	                    'h':'s',
	    	                    'a':'e',
	    	                    'd':'j',
	    	                    'p':'q',
	    	                    'g':'m',
	    	                    'b':'c',
	    	                    'f':'i',
	    	                    'k':'n',
	    	                    'l':'o',
	    	                    'r':'t',
	                        }
	                    }
                    }";

            // Act
            var message = controller.Encrypt(JsonConvert.DeserializeObject<EnigmaMachineDto>(jsonEnigma), true);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, message.StatusCode);
            message.TryGetContentValue<string>(out string encryptedText);

            Assert.AreEqual("ocfkj lljkyb urekwfj", encryptedText);
        }

    }
}
