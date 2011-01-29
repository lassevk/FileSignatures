using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace FileSignatures.Tests
{
    [TestFixture]
    public class ByteArrayContainerTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(1024)]
        [TestCase(1025)]
        public void Length_ReturnsLengthOfGivenArray(int length)
        {
            var data = new byte[length];
            var container = new ByteArrayContainer(data);

            Assert.That(container.Length, Is.EqualTo(length));
        }

        [Test]
        public void Constructor_NullData_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ByteArrayContainer(null));
        }

        [Test]
        public void Read_BeyondEnd_ReturnsEmptyArray()
        {
            var data = new byte[10];
            var container = new ByteArrayContainer(data);

            CollectionAssert.AreEqual(container.Read(20, 10), new byte[0]);
        }

        [Test]
        public void Read_Contents_ReturnsCorrectContents()
        {
            var data = new byte[]
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10
            };
            var container = new ByteArrayContainer(data);

            CollectionAssert.AreEqual(
                container.Read(5, 10), new byte[]
                {
                    6, 7, 8, 9, 10
                });
        }

        [Test]
        public void Read_NegativeLength_ThrowsArgumentOutOfRangeException()
        {
            var data = new byte[0];
            var container = new ByteArrayContainer(data);

            Assert.Throws<ArgumentOutOfRangeException>(() => container.Read(0, -1));
        }

        [Test]
        public void Read_NegativeOffset_ThrowsArgumentOutOfRangeException()
        {
            var data = new byte[0];
            var container = new ByteArrayContainer(data);

            Assert.Throws<ArgumentOutOfRangeException>(() => container.Read(-1, 0));
        }

        [Test]
        public void Read_TooCloseToEnd_ReturnsTruncatedArray()
        {
            var data = new byte[10];
            var container = new ByteArrayContainer(data);

            CollectionAssert.AreEqual(container.Read(5, 10), new byte[5]);
        }

        [Test]
        public void Read_ZeroLength_ReturnsEmptyArray()
        {
            var data = new byte[10];
            var container = new ByteArrayContainer(data);

            CollectionAssert.AreEqual(container.Read(0, 0), new byte[0]);
        }
    }
}