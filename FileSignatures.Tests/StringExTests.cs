using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace FileSignatures.Tests
{
    [TestFixture]
    public class StringExTests
    {
        [TestCase((string)null, true)]
        [TestCase("", true)]
        [TestCase(" \t\n\r ", true)]
        [TestCase(" \t\n\r x \t\r\n ", false)]
        public void IsNullOrWhiteSpace_WithVariousPatterns_ReturnsCorrectResult(string input, bool expected)
        {
            bool result = StringEx.IsNullOrWhiteSpace(input);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}