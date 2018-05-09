using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enigma;
using System.Collections.Generic;
using System.Linq;
 

namespace UnitTests
{
    [TestClass]
    public class ReflectorTests
    {
        [TestMethod]
        public void SetWiring_ShouldNotThrow()
        {
            // Arrange 

            // Act (calls SetWiring)
            Reflector reflector = new Reflector();

            // Assert
        }

        [TestMethod]
        public void GetWiring_ShouldReturn_Dictionary()
        {
            // Arrange
            Reflector reflector = new Reflector();

            // Act
            Dictionary<char, char> wiring = reflector.GetWiring();
            //Type t = "System.Collections.Generic.Dictionary";

            // Assert
            Assert.AreEqual(26, wiring.Count);
            //Assert.IsInstanceOfType(wiring, Type.GetType("Dictionary<char, char>"));
        }

        [TestMethod]
        public void GetWiring_ShouldDecrypt_TheLetterItEncrypts()
        {
            // Arrange
            Reflector reflector = new Reflector();

            // Act
            Dictionary<char, char> wiring = reflector.GetWiring();
            char a = wiring['a'];
            char b = wiring[a];

            // Assert
            Assert.AreEqual(b, 'a');
        }

        [TestMethod]
        public void SetWiring_ShouldEncrypt_ALetter()
        {
            // Arrange
            Reflector reflector = new Reflector();

            // Act
            char reflectedLetter = reflector.ReflectLetter('a');
            var wiring = reflector.GetWiring();

            // Assert
            Assert.AreEqual(wiring['a'], reflectedLetter);
        }

        [TestMethod]
        public void SetWiring_ShouldEncryptAndDecrypt_ALetter()
        {
            // Arrange
            Reflector reflector = new Reflector();
            char initialLetter = 'b';

            // Act
            
            char reflectedLetter = reflector.ReflectLetter(initialLetter);
            char finalLetter = reflector.ReflectLetter(reflectedLetter);

            // Assert
            Assert.AreEqual(initialLetter, finalLetter);
        }
    }






    public static class Extensions
    {
        public static void DoesNotThrow<T>(this Assert a, Action expressionUnderTest) where T : System.Exception
        {
            try
            {
                expressionUnderTest();
            }
            catch (T)
            {
                Assert.Fail("It Through the exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
            Assert.IsTrue(true);
        }

    }
}
