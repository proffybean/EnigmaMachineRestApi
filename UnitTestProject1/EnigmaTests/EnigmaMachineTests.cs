using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enigma;
using System.Collections.Generic;
using static Enigma.Constants;

namespace UnitTests
{
    [TestClass]
    public class EnigmaMachineTests
    {
        public static EnigmaMachine enigmaMachine;

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            
        }

        [TestInitialize]
        public void TestInitialize()
        {
            enigmaMachine = new EnigmaMachine();
            enigmaMachine.ChooseRotors(1,2,3);

            enigmaMachine.SetPlugboardPair('h', 's');
            enigmaMachine.SetPlugboardPair('a', 'e');
            enigmaMachine.SetPlugboardPair('d', 'j');
            enigmaMachine.SetPlugboardPair('p', 'q');
            enigmaMachine.SetPlugboardPair('g', 'm');
            enigmaMachine.SetRotorDials('a', 'a', 'z');
        }

        [TestMethod]
        public void EnimagmaMachine_ShouldEncodeWord_When()
        {
            // Arrange

            // Act
            string encodedMessage = enigmaMachine.Encode("hello");

            // Assert
            Assert.AreEqual("lbabg", encodedMessage);
        }

        [TestMethod]
        public void EnimagmaMachine_ShouldDecodeWord_AfterEncoding()
        {
            // Arrange
            string initialMessage = "hello";

            // Act
            string encodedMessage = enigmaMachine.Encode(initialMessage);
            enigmaMachine.SetRotorDials('a', 'a', 'z');
            string decodedMessage = enigmaMachine.Encode(encodedMessage);

            // Assert
            Assert.AreEqual(initialMessage, decodedMessage);
        }

        [TestMethod]
        public void EnimagmaMachine_ShouldDecodeWordWithSpaces_AfterEncoding()
        {
            // Arrange
            string initialMessage = "hello there";

            // Act
            string encodedMessage = enigmaMachine.Encode(initialMessage);
            enigmaMachine.SetRotorDials('a', 'a', 'z');
            string decodedMessage = enigmaMachine.Encode(encodedMessage);

            // Assert
            Assert.AreEqual(initialMessage.Replace(" ", ""), decodedMessage);
        }

        [TestMethod]
        public void EnimagmaMachine_ShouldDecodeWordWithCapitals_AfterEncoding()
        {
            // Arrange
            string initialMessage = "HelloThere";

            // Act
            string encodedMessage = enigmaMachine.Encode(initialMessage);
            enigmaMachine.SetRotorDials('a', 'a', 'z');
            string decodedMessage = enigmaMachine.Encode(encodedMessage);

            // Assert
            Assert.AreEqual(initialMessage.ToLower(), decodedMessage);
        }

        [TestMethod]
        public void EnimagmaMachine_ShouldDecodeWordWithSpaces_AfterEncoding2()
        {
            // Arrange
            enigmaMachine.LeaveWhiteSpace(true);
            string initialMessage = "Hello There Bob";

            // Act
            string encodedMessage = enigmaMachine.Encode(initialMessage);
            enigmaMachine.SetRotorDials('a', 'a', 'z');
            string decodedMessage = enigmaMachine.Encode(encodedMessage);

            // Assert
            Assert.AreEqual(initialMessage.ToLower(), decodedMessage);
        }

        [TestMethod]
        public void EnigmaMachine_2ndRotorShouldTick_WhenFirstRotorPassTickPoint()
        {
            // Arrange
            string initialMessage = "epiphany";
            enigmaMachine.SetRotorDials('a', 'a', 't');

            // Act
            string encodedMessage = enigmaMachine.Encode(initialMessage);
            enigmaMachine.SetRotorDials('a', 'a', 't');
            string decodedMessage = enigmaMachine.Encode(encodedMessage);

            // Assert 
            Assert.AreEqual("bxheqcme", encodedMessage);
            Assert.AreEqual(initialMessage, decodedMessage);
        }

        [TestMethod]
        public void EnigmaMachine_2ndAnd3rdRotorShouldTick_WhenFirstRotorPassesTickPoint()
        {
            // Arrange
            string initialMessage = "nemesis";
            enigmaMachine.SetRotorDials('a', 'e', 't');

            // Act
            string encodedMessage = enigmaMachine.Encode(initialMessage);
            enigmaMachine.SetRotorDials('a', 'e', 't');
            string decodedMessage = enigmaMachine.Encode(encodedMessage);

            // Assert 
            Assert.AreEqual("zpeizdr", encodedMessage);
            Assert.AreEqual(initialMessage, decodedMessage);
        }

        [TestMethod]
        public void EnigmaMachine_2ndAnd3rdRotorShouldTick_WhenThirdRotorPassesTickPoint()
        {
            // Arrange
            string initialMessage = "nemesis";
            enigmaMachine.SetRotorDials('a', 'e', 't');

            // Act
            //string encodedMessage = enigmaMachine.Encode(initialMessage);
            string encodedMessage = "";
            foreach (char c in initialMessage)
            {
                encodedMessage = encodedMessage + enigmaMachine.ConvertCharacter(c);
            }
            
            enigmaMachine.SetRotorDials('a', 'e', 't');
            string decodedMessage = enigmaMachine.Encode(encodedMessage);

            // Assert 
            Assert.AreEqual("zpeizdr", encodedMessage);
            Assert.AreEqual(initialMessage, decodedMessage);
        }

        [TestMethod]
        public void EnigmaMachine_AllRotorsShouldTick_WhenAllRotorsPassesTickPoint()
        {
            // Arrange
            string initialMessage = "nemesis is cool";
            enigmaMachine.SetRotorDials('q', 'e', 't');

            // Act
            string encodedMessage = enigmaMachine.Encode(initialMessage);
            //string encodedMessage = "";
            //foreach (char c in initialMessage)
            //{
            //    encodedMessage = encodedMessage + enigmaMachine.ConvertCharacter(c);
            //}

            enigmaMachine.SetRotorDials('q', 'e', 't');
            string decodedMessage = enigmaMachine.Encode(encodedMessage);

            // Assert 
            Assert.AreEqual("wrcngvxxtqyxc", encodedMessage);
            Assert.AreEqual(initialMessage.Replace(" ", ""), decodedMessage);
        }

        [TestMethod]
        public void EnigmaMachine_AllRotorsShouldTick_WhenAllRotorsPassesTickPoint_AndLeaveSpacesIsTrue()
        {
            // Arrange
            string initialMessage = "nemesis is cool";
            enigmaMachine.SetRotorDials('q', 'e', 't');
            enigmaMachine.LeaveWhiteSpace(true);

            // Act
            string encodedMessage = enigmaMachine.Encode(initialMessage);
            enigmaMachine.SetRotorDials('q', 'e', 't');
            string decodedMessage = enigmaMachine.Encode(encodedMessage);

            // Assert 
            Assert.AreEqual("wrcngvx xt qyxc", encodedMessage);
            Assert.AreEqual(initialMessage, decodedMessage);
        }

        [TestMethod]
        public void SetPlugboardPair_ShouldAddWiring_WithOnePair()
        {
            // Arrange
            char char1 = 'z';
            char char2 = 'x';

            // Act
            enigmaMachine.SetPlugboardPair(char1, char2);
        }

        [TestMethod]
        public void SetPlugboard_ShouldAddWiring_WithCollection()
        {
            // Arrange
            var col = new Dictionary<char, char>();
            col.Add('z', 'x');
            col.Add('b', 'c');

            // Act
            enigmaMachine.SetPlugboard(col);
        }
    }

    [TestClass]
    public class EnigmaMachineDifferentRotosTests
    {
        Dictionary<char, char> plugboardWiring = new Dictionary<char, char>
        {
            {'h', 's'},
            {'a', 'e'},
            {'d', 'j'},
            {'p', 'q'},
            {'g', 'm'},
            {'b', 'c'},
            {'f', 'i'},
            {'k', 'n'},
            {'l', 'o'},
            {'r', 't'}
        };

        [TestMethod]
        public void EnigmaMachine_ShouldDecodeWord_WithRotors123()
        {
            // Arrange
            var enigmaMachine = new EnigmaMachine();
            enigmaMachine.ChooseRotors(1, 2, 3);
            enigmaMachine.SetRotorDials('a', 'a', 'z');

            enigmaMachine.SetPlugboardPair('h', 's');
            enigmaMachine.SetPlugboardPair('a', 'e');
            enigmaMachine.SetPlugboardPair('d', 'j');
            enigmaMachine.SetPlugboardPair('p', 'q');
            enigmaMachine.SetPlugboardPair('g', 'm');
            enigmaMachine.SetPlugboardPair('b', 'c');
            enigmaMachine.SetPlugboardPair('f', 'i');
            enigmaMachine.SetPlugboardPair('k', 'n');
            enigmaMachine.SetPlugboardPair('l', 'o');
            enigmaMachine.SetPlugboardPair('r', 't');

            // Act
            string encodedMessage = enigmaMachine.Encode("hello");

            // Assert
            Assert.AreEqual("ocfkj", encodedMessage);
        }

        [TestMethod]
        public void EnigmaMachine_ShouldDecodeWordWithCapsAndSpaces_WithRotors123()
        {
            // Arrange
            var enigmaMachine = new EnigmaMachine();
            enigmaMachine.ChooseRotors(1, 2, 3);
            enigmaMachine.SetRotorDials('a', 'a', 'z');
            enigmaMachine.SetPlugboard(plugboardWiring);
            string plainText = "They call Me Mr Tibbs";

            // Act
            string encodedMessage = enigmaMachine.Encode(plainText);

            // Assert
            enigmaMachine.SetRotorDials('a', 'a', 'z');
            Assert.AreEqual("gdjwbtngooygonlym", encodedMessage);
            Assert.AreEqual(plainText.ToLower().Replace(" ", ""), enigmaMachine.Encode(encodedMessage));
        }

        [TestMethod]
        public void EnigmaMachine_ShouldDecodeWord_WithRotors234()
        {
            // Arrange
            var enigmaMachine = new EnigmaMachine();
            enigmaMachine.ChooseRotors(2, 3, 4);
            enigmaMachine.SetRotorDials('a', 'b', 'c');
            enigmaMachine.SetPlugboard(plugboardWiring);
            string plainText = "bungalow";

            // Act
            string encodedMessage = enigmaMachine.Encode(plainText);

            // Assert
            enigmaMachine.SetRotorDials('a', 'b', 'c');
            Assert.AreEqual("noavbjhg", encodedMessage);
            Assert.AreEqual(plainText, enigmaMachine.Encode(encodedMessage));
        }

        [TestMethod]
        public void EnigmaMachine_ShouldDecodeWord_WithRotors345()
        {
            // Arrange
            var enigmaMachine = new EnigmaMachine();
            enigmaMachine.ChooseRotors(3, 4, 5);
            enigmaMachine.SetRotorDials('b', 'c', 'd');
            enigmaMachine.SetPlugboard(plugboardWiring);
            string plainText = "cynosure";

            // Act
            string encodedMessage = enigmaMachine.Encode(plainText);

            // Assert
            enigmaMachine.SetRotorDials('b', 'c', 'd');
            Assert.AreEqual("oswemtyt", encodedMessage);
            Assert.AreEqual(plainText, enigmaMachine.Encode(encodedMessage));
        }

        [TestMethod]
        public void EnigmaMachine_ShouldDecodeWord_WithRotors531()
        {
            // Arrange
            var enigmaMachine = new EnigmaMachine();
            enigmaMachine.ChooseRotors(5, 3, 1);
            enigmaMachine.SetRotorDials('b', 'j', 'n');
            enigmaMachine.SetPlugboard(plugboardWiring);
            string plainText = "dalliance";

            // Act
            string encodedMessage = enigmaMachine.Encode(plainText);

            // Assert
            enigmaMachine.SetRotorDials('b', 'j', 'n');
            Assert.AreEqual("iymhgqkat", encodedMessage);
            Assert.AreEqual(plainText, enigmaMachine.Encode(encodedMessage));
        }

        [TestMethod]
        public void EnigmaMachine_ShouldDecodeWordWithSpaces_WithRotors531()
        {
            // Arrange
            var enigmaMachine = new EnigmaMachine();
            enigmaMachine.ChooseRotors(5, 3, 1);
            enigmaMachine.SetRotorDials('g', 'j', 'n');
            enigmaMachine.SetPlugboard(plugboardWiring);
            string plainText = "dalliance chatoyant";

            // Act
            string encodedMessage = enigmaMachine.Encode(plainText);

            // Assert
            enigmaMachine.SetRotorDials('g', 'j', 'n');
            Assert.AreEqual("jyssqzldhgqubgkoji", encodedMessage);
            Assert.AreEqual(plainText.Replace(" ", ""), enigmaMachine.Encode(encodedMessage));
        }

        [TestMethod]
        public void EnigmaMachine_ShouldDecodeWordWithSpaces_WithRotors312()
        {
            // Arrange
            var enigmaMachine = new EnigmaMachine();
            enigmaMachine.ChooseRotors(3, 1, 2);
            enigmaMachine.SetRotorDials('g', 'j', 'n');
            enigmaMachine.SetPlugboard(plugboardWiring);
            string plainText = "forbearance";

            // Act
            string encodedMessage = enigmaMachine.Encode(plainText);

            // Assert
            enigmaMachine.SetRotorDials('g', 'j', 'n');
            Assert.AreEqual("jiekfqfgkxz", encodedMessage);
            Assert.AreEqual(plainText, enigmaMachine.Encode(encodedMessage));
        }
    }
}
