using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enigma;

namespace UnitTests
{
    /// <summary>
    /// Summary description for RotorTests
    /// </summary>
    [TestClass]
    public class RotorTests
    {
        public RotorTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        [TestMethod]
        public void Ctor_ShouldNotThrow_When()
        {
            // Arrange
            Rotor rotor = new Rotor();

            // Act

            // Assert
        }

        [TestMethod]
        public void Offset_ShouldGetSet_WhenSettingIt()
        {
            // Arrange
            Rotor rotor = new Rotor();

            // Act
            rotor.Offset = 5;

            // Assert
            Assert.AreEqual(5, rotor.Offset);
        }

        [TestMethod]
        public void Offset_ShouldMod26_WhenSettingIt()
        {
            // Arrange
            Rotor rotor = new Rotor();

            // Act
            rotor.Offset = 26;
            int offset1 = rotor.Offset;
            rotor.Offset = 27;
            int offset2 = rotor.Offset;

            // Assert
            Assert.AreEqual(0, offset1);
            Assert.AreEqual(1, offset2);
        }


        [TestMethod]
        public void SetDial_ShouldSetOffset_WhenSettingIt()
        {
            // Arrange
            Rotor rotor = new Rotor();

            // Act
            rotor.SetDial(5);

            // Assert
            Assert.AreEqual(5, rotor.GetDialOffset());
        }


        [TestMethod]
        public void GetCurrentRotor_ShouldReturnRotor()
        {
            // Arrange
            Rotor rotor = new Rotor();

            // Act
            char[] curRotor = rotor.GetCurrentRotor();

            // Assert
            Assert.AreEqual(26, curRotor.Length);
        }


        [TestMethod]
        public void Rotate_ShouldChangeOffset()
        {
            // Arrange
            Rotor rotor = new Rotor();

            // Act
            rotor.SetDial(0);
            rotor.Rotate();
            Assert.AreEqual(1, rotor.GetDialOffset());

            rotor.SetDial(18);
            rotor.Rotate();
            Assert.AreEqual(19, rotor.GetDialOffset());

            rotor.SetDial(25);
            rotor.Rotate();
            Assert.AreEqual(0, rotor.GetDialOffset());
        }


        [TestMethod]
        public void ConvertLetter_ShouldGetLetterAndRotate_WhenConvertingByIndex()
        {
            // Arrange
            Rotor rotor = new Rotor("BDFHJLCPRTXVZNYEIWGAKMUSQO".ToLower(), true, 21);

            // Act
            rotor.SetDial('z');
            char letter = rotor.ConvertLetter(1);

            // Assert
            Assert.AreEqual('d', letter);
            Assert.AreEqual('f', rotor.ConvertLetter(1));
            Assert.AreEqual('h', rotor.ConvertLetter(1));
            Assert.AreEqual('j', rotor.ConvertLetter(1));
            Assert.AreEqual('l', rotor.ConvertLetter(1));
        }


        [TestMethod]
        public void ConvertLetter_ShouldConvert_WhenConvertingByLetterA()
        {
            // Arrange
            Rotor rotor = new Rotor("BDFHJLCPRTXVZNYEIWGAKMUSQO".ToLower(), true, 21); // rotor 3

            // Act
            rotor.SetDial('z');
            char letter = rotor.ConvertLetter('a');

            // Assert
            Assert.AreEqual('b', letter);
            Assert.AreEqual('d', rotor.ConvertLetter('a'));
            Assert.AreEqual('f', rotor.ConvertLetter('a'));
            Assert.AreEqual('h', rotor.ConvertLetter('a'));
            Assert.AreEqual('j', rotor.ConvertLetter('a'));
            Assert.AreEqual('l', rotor.ConvertLetter('a'));
            Assert.AreEqual('c', rotor.ConvertLetter('a'));
            Assert.AreEqual('p', rotor.ConvertLetter('a'));
            Assert.AreEqual('r', rotor.ConvertLetter('a'));
            Assert.AreEqual('t', rotor.ConvertLetter('a'));
            Assert.AreEqual('x', rotor.ConvertLetter('a'));
            Assert.AreEqual('v', rotor.ConvertLetter('a'));
            Assert.AreEqual('z', rotor.ConvertLetter('a'));
            Assert.AreEqual('n', rotor.ConvertLetter('a'));
            Assert.AreEqual('y', rotor.ConvertLetter('a'));
            Assert.AreEqual('e', rotor.ConvertLetter('a'));
            Assert.AreEqual('i', rotor.ConvertLetter('a'));
            Assert.AreEqual('w', rotor.ConvertLetter('a'));
            Assert.AreEqual('g', rotor.ConvertLetter('a'));
            Assert.AreEqual('a', rotor.ConvertLetter('a'));
            Assert.AreEqual('k', rotor.ConvertLetter('a'));
            Assert.AreEqual('m', rotor.ConvertLetter('a'));
            Assert.AreEqual('u', rotor.ConvertLetter('a'));
            Assert.AreEqual('s', rotor.ConvertLetter('a'));
            Assert.AreEqual('q', rotor.ConvertLetter('a'));
            Assert.AreEqual('o', rotor.ConvertLetter('a'));
            Assert.AreEqual('b', rotor.ConvertLetter('a'));
        }



        [TestMethod]
        public void ReverseConvertLetter_ShouldConvertOnReverse_ForOneChar()
        {
            // Arrange
            Rotor rotor = new Rotor("BDFHJLCPRTXVZNYEIWGAKMUSQO".ToLower(), true, 21); // TODO: move to Rotor3 constant

            // Act
            rotor.SetDial('z');
            char letter = rotor.ReverseConvertLetter('j');

            // Assert
            Assert.AreEqual('b', letter);
        }


    }
}
