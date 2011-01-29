using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace FileSignatures
{
    /// <summary>
    /// This class implements <see cref="IByteContainer"/> for <see cref="Stream"/> objects.
    /// </summary>
    public class StreamContainer : IByteContainer
    {
        private readonly Stream _Stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamContainer"/> class.
        /// </summary>
        /// <param name="stream">
        /// The stream to make available as a <see cref="IByteContainer"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="stream"/> is <c>null</c>.</para>
        /// </exception>
        public StreamContainer(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            _Stream = stream;
        }

        #region IByteContainer Members

        /// <summary>
        /// Gets the length of the contents in the container.
        /// </summary>
        public long Length
        {
            get
            {
                return _Stream.Length;
            }
        }

        /// <summary>
        /// Reads a series of bytes from the container.
        /// </summary>
        /// <param name="offset">
        /// The offset from the start of the container to start reading from.
        /// </param>
        /// <param name="length">
        /// The number of bytes to read.
        /// </param>
        /// <returns>
        /// A byte array containing the bytes read.
        /// </returns>
        /// <remarks>
        /// <para>The array can be fewer than <paramref name="length"/> bytes in size,
        /// down to an empty array, if there are fewer than <paramref name="length"/>
        /// bytes left in the container from the given <paramref name="offset"/>.</para>
        /// <para>In particular, if an attempt to read past the end of the container
        /// contents is made, an empty array should be returned instead of throwing an
        /// exception.</para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="offset"/> is less than 0.</para>
        /// <para>- or -</para>
        /// <para><paramref name="length"/> is less than 0.</para>
        /// </exception>
        public byte[] Read(long offset, int length)
        {
            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset", offset, "offset cannot be negative");
            if (length < 0)
                throw new ArgumentOutOfRangeException("length", length, "length cannot be negative");

            if (offset >= _Stream.Length)
                return new byte[0];

            long bytesLeft = _Stream.Length - offset;
            if (bytesLeft < length)
                length = (int)bytesLeft;

            if (length == 0)
                return new byte[0];

            var result = new byte[length];
            _Stream.Position = offset;
            int bytesRead = _Stream.Read(result, 0, length);
            if (bytesRead < length)
                Array.Resize(ref result, bytesRead);
            return result;
        }

        #endregion
    }
}