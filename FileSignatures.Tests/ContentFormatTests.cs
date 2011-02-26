using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace FileSignatures.Tests
{
    [TestFixture]
    public class ContentFormatTests
    {
        [TestCase((string)null)]
        [TestCase("")]
        [TestCase(" \t\n\r ")]
        public void Constructor_NullOrEmptyCategory_ThrowsArgumentNullException(string category)
        {
            Assert.Throws<ArgumentNullException>(() => new ContentFormat(category, "name", "version", 1, "mime", string.Empty));
        }

        [TestCase((string)null)]
        [TestCase("")]
        [TestCase(" \t\n\r ")]
        public void Constructor_NullOrEmptyName_ThrowsArgumentNullException(string name)
        {
            Assert.Throws<ArgumentNullException>(() => new ContentFormat("category", name, "version", 1, "mime", string.Empty));
        }

        [TestCase("category", "category")]
        [TestCase(" \t\r\n category \t\r\n", "category")]
        [TestCase(" \t\r\n x \t\r\n", "x")]
        public void Constructor_AssignsTrimmedValueToCategoryProperty(string input, string expected)
        {
            var format = new ContentFormat(input, "name", "version", 1, "mime", string.Empty);

            Assert.That(format.Category, Is.EqualTo(expected));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-10)]
        public void Constructor_NegativeOrZeroConfidence_ThrowsArgumentOutOfRangeException(int input)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ContentFormat("category", "name", "version", input, "mime", string.Empty));
        }

        [TestCase("name", "name")]
        [TestCase(" \t\r\n name \t\r\n", "name")]
        [TestCase(" \t\r\n x \t\r\n", "x")]
        public void Constructor_AssignsTrimmedValueToNameProperty(string input, string expected)
        {
            var format = new ContentFormat("category", input, "version", 1, "mime", string.Empty);

            Assert.That(format.Name, Is.EqualTo(expected));
        }

        [TestCase(" \t\n\r ", "")]
        [TestCase("version", "version")]
        [TestCase(" \t\r\n version \t\r\n", "version")]
        [TestCase(" \t\r\n x \t\r\n", "x")]
        public void Constructor_AssignsTrimmedValueToVersionProperty(string input, string expected)
        {
            var format = new ContentFormat("category", "name", input, 1, "mime", string.Empty);

            Assert.That(format.Version, Is.EqualTo(expected));
        }

        [TestCase("category", "name", "1.0", "category/name/1.0")]
        [TestCase("category", "name", "", "category/name")]
        [TestCase("x", "y", "z", "x/y/z")]
        public void ToString_VariousPatterns_ReturnsCorrectResult(string category, string name, string version, string expected)
        {
            var format = new ContentFormat(category, name, version, 1, "mime", string.Empty);
            string output = format.ToString();

            Assert.That(output, Is.EqualTo(expected));
        }

        [TestCase("category", "name", "version")]
        public void Equals_OnSameValues_ReturnsTrue(string category, string name, string version)
        {
            var identity1 = new ContentFormat(category, name, version, 1, "mime", string.Empty);
            var identity2 = new ContentFormat(category, name, version, 1, "mime", string.Empty);

            Assert.That(identity1.Equals(identity2), Is.True);
        }

        [TestCase("category", "name", "version")]
        public void EqualsOnObject_OnSameValues_ReturnsTrue(string category, string name, string version)
        {
            var identity1 = new ContentFormat(category, name, version, 1, "mime", string.Empty);
            var identity2 = new ContentFormat(category, name, version, 1, "mime", string.Empty);

            Assert.That(identity1.Equals((object)identity2), Is.True);
        }

        [Test]
        public void Constructor_NullVersion_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ContentFormat("category", "name", null, 1, "mime", string.Empty));
        }

        [Test]
        public void Equals_NotAnIdentityStruct_ReturnsFalse()
        {
            var format = new ContentFormat("category", "name", "version", 1, "mime", string.Empty);

            Assert.That(format.Equals("category/name/version"), Is.False);
        }

        [Test]
        public void Equals_WithNullObj_ReturnsFalse()
        {
            var format = new ContentFormat("category", "name", "version", 1, "mime", string.Empty);

            Assert.That(format.Equals(null), Is.False);
        }
    }
}