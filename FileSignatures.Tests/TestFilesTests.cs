using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace FileSignatures.Tests
{
    [TestFixture]
    public class TestFilesTests
    {
        public IEnumerable<string[]> TestFiles
        {
            get
            {
                string path = Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, @"..\..\..\..\TestFiles"));

                yield return new[]
                {
                    Path.Combine(path, "archive.7z"), "archive/7z"
                };
                yield return new[]
                {
                    Path.Combine(path, "archive.zip"), "archive/zip"
                };

                // yield return new[] { Path.Combine(path, "image.png"), "image/png" };
                // yield return new[] { Path.Combine(path, "image.jpg"), "image/jpeg" };
                // yield return new[] { Path.Combine(path, "image.gif"), "image/gif" };
                // yield return new[] { Path.Combine(path, "image.bmp"), "image/bmp" };
                // yield return new[] { Path.Combine(path, "textfile.txt"), "text/plain" };
            }
        }

        [TestCaseSource("TestFiles")]
        public void Test(string filename, string expectedIdentification)
        {
            string identification = Identifier.Default.Identify(filename).Select(id => id.ToString()).FirstOrDefault() ?? string.Empty;

            Assert.That(identification, Is.EqualTo(expectedIdentification));
        }
    }
}