using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace FileSignatures.Tests
{
    [TestFixture]
    public class IdentityTests
    {
        [TestCase((string)null)]
        [TestCase("")]
        [TestCase(" \t\n\r ")]
        public void Constructor_NullOrEmptyCategory_ThrowsArgumentNullException(string category)
        {
            Assert.Throws<ArgumentNullException>(() => new Identity(category, "name", "version"));
        }

        [TestCase((string)null)]
        [TestCase("")]
        [TestCase(" \t\n\r ")]
        public void Constructor_NullOrEmptyName_ThrowsArgumentNullException(string name)
        {
            Assert.Throws<ArgumentNullException>(() => new Identity("category", name, "version"));
        }

        [TestCase("category", "category")]
        [TestCase(" \t\r\n category \t\r\n", "category")]
        [TestCase(" \t\r\n x \t\r\n", "x")]
        public void Constructor_AssignsTrimmedValueToCategoryProperty(string input, string expected)
        {
            var identity = new Identity(input, "name", "version");

            Assert.That(identity.Category, Is.EqualTo(expected));
        }

        [TestCase("name", "name")]
        [TestCase(" \t\r\n name \t\r\n", "name")]
        [TestCase(" \t\r\n x \t\r\n", "x")]
        public void Constructor_AssignsTrimmedValueToNameProperty(string input, string expected)
        {
            var identity = new Identity("category", input, "version");

            Assert.That(identity.Name, Is.EqualTo(expected));
        }

        [TestCase(" \t\n\r ", "")]
        [TestCase("version", "version")]
        [TestCase(" \t\r\n version \t\r\n", "version")]
        [TestCase(" \t\r\n x \t\r\n", "x")]
        public void Constructor_AssignsTrimmedValueToVersionProperty(string input, string expected)
        {
            var identity = new Identity("category", "name", input);

            Assert.That(identity.Version, Is.EqualTo(expected));
        }

        [TestCase("category", "name", "1.0", "category/name/1.0")]
        [TestCase("category", "name", "", "category/name")]
        [TestCase("x", "y", "z", "x/y/z")]
        public void ToString_VariousPatterns_ReturnsCorrectResult(string category, string name, string version, string expected)
        {
            var identity = new Identity(category, name, version);
            string output = identity.ToString();

            Assert.That(output, Is.EqualTo(expected));
        }

        [TestCase("category", "name", "version")]
        public void Equals_OnSameValues_ReturnsTrue(string category, string name, string version)
        {
            var identity1 = new Identity(category, name, version);
            var identity2 = new Identity(category, name, version);

            Assert.That(identity1.Equals(identity2), Is.True);
        }

        [TestCase("category", "name", "version")]
        public void EqualsOnObject_OnSameValues_ReturnsTrue(string category, string name, string version)
        {
            var identity1 = new Identity(category, name, version);
            var identity2 = new Identity(category, name, version);

            Assert.That(identity1.Equals((object)identity2), Is.True);
        }

        [Test]
        public void Constructor_NullVersion_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Identity("category", "name", null));
        }

        [Test]
        public void Equals_NotAnIdentityStruct_ReturnsFalse()
        {
            var identity = new Identity("category", "name", "version");

            Assert.That(identity.Equals("category/name/version"), Is.False);
        }

        [Test]
        public void Equals_WithNullObj_ReturnsFalse()
        {
            var identity = new Identity("category", "name", "version");

            Assert.That(identity.Equals(null), Is.False);
        }
    }
}