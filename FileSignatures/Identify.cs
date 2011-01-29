using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace FileSignatures
{
    /// <summary>
    /// This class implements a system for identifying file, stream, byte array, or similar contents
    /// in terms of file formats.
    /// </summary>
    public static class Identify
    {
        /// <summary>
        /// Identifies the contents of the file with the specified filename.
        /// </summary>
        /// <param name="filename">
        /// The full path to and name of the file to identify the contents of.
        /// </param>
        /// <returns>
        /// An array of <see cref="Identity"/> objects that identify the file contents.
        /// </returns>
        /// <remarks>
        /// Note that the returned array contains the <see cref="Identity"/> objects
        /// ordered in such a way that the first one is deemed the one most likely to be
        /// correct, the next one less likely, the next one after that even less likely,
        /// and so on.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="filename"/> is <c>null</c> or empty.</para>
        /// </exception>
        public static Identity[] Contents(string filename)
        {
            if (StringEx.IsNullOrWhiteSpace(filename))
                throw new ArgumentNullException("filename");

            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Contents(stream);
            }
        }

        /// <summary>
        /// Identifies the contents of the specified file.
        /// </summary>
        /// <param name="file">
        /// The <see cref="FileInfo"/> object that refers to the file.
        /// </param>
        /// <returns>
        /// An array of <see cref="Identity"/> objects that identify the file contents.
        /// </returns>
        /// <remarks>
        /// Note that the returned array contains the <see cref="Identity"/> objects
        /// ordered in such a way that the first one is deemed the one most likely to be
        /// correct, the next one less likely, the next one after that even less likely,
        /// and so on.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="file"/> is <c>null</c>.</para>
        /// </exception>
        public static Identity[] Contents(FileInfo file)
        {
            if (file == null)
                throw new ArgumentNullException("file");

            using (FileStream stream = file.OpenRead())
            {
                return Contents(stream);
            }
        }

        /// <summary>
        /// Identifies the contents of the specified stream.
        /// </summary>
        /// <param name="stream">
        /// The <see cref="Contents(System.IO.Stream)"/> object with the contents to identify.
        /// </param>
        /// <returns>
        /// An array of <see cref="Identity"/> objects that identify the stream contents.
        /// </returns>
        /// <remarks>
        /// Note that the returned array contains the <see cref="Identity"/> objects
        /// ordered in such a way that the first one is deemed the one most likely to be
        /// correct, the next one less likely, the next one after that even less likely,
        /// and so on.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="stream"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para>Unable to read from <paramref name="stream"/>, the <see cref="System.IO.Stream.CanRead"/> property returns false.</para>
        /// </exception>
        public static Identity[] Contents(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (!stream.CanRead)
                throw new ArgumentException("Cannot read from the specified stream, CanRead returns false", "stream");

            var container = new StreamContainer(stream);
            return Contents(container);
        }

        /// <summary>
        /// Identifies the contents of the specified byte array.
        /// </summary>
        /// <param name="data">
        /// The <see cref="Byte"/>[] array containing the contents to identify.
        /// </param>
        /// <returns>
        /// An array of <see cref="Identity"/> objects that identify the byte array contents.
        /// </returns>
        /// <remarks>
        /// Note that the returned array contains the <see cref="Identity"/> objects
        /// ordered in such a way that the first one is deemed the one most likely to be
        /// correct, the next one less likely, the next one after that even less likely,
        /// and so on.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="data"/> is <c>null</c>.</para>
        /// </exception>
        public static Identity[] Contents(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            var container = new ByteArrayContainer(data);
            return Contents(container);
        }

        /// <summary>
        /// Identifies the contents of the specified container.
        /// </summary>
        /// <param name="container">
        /// The <see cref="IByteContainer"/> with the contents to identify.
        /// </param>
        /// <returns>
        /// An array of <see cref="Identity"/> objects that identify the <paramref name="container"/> contents.
        /// </returns>
        /// <remarks>
        /// Note that the returned array contains the <see cref="Identity"/> objects
        /// ordered in such a way that the first one is deemed the one most likely to be
        /// correct, the next one less likely, the next one after that even less likely,
        /// and so on.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="container"/> is <c>null</c>.</para>
        /// </exception>
        public static Identity[] Contents(IByteContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            throw new NotImplementedException();
        }
    }
}