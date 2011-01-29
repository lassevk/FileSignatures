using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FileSignatures
{
    /// <summary>
    /// This class implements <see cref="IByteContainer"/> for <see cref="byte"/>[] array objects.
    /// </summary>
    public class ByteArrayContainer : IByteContainer
    {
        private readonly byte[] _Data;

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayContainer"/> class.
        /// </summary>
        /// <param name="data">
        /// The bytes to make available as a <see cref="IByteContainer"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="data"/> is <c>null</c>.</para>
        /// </exception>
        public ByteArrayContainer(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            _Data = data;
        }

        #region IByteContainer Members

        /// <summary>
        /// Gets the length of the contents in the container.
        /// </summary>
        public long Length
        {
            get
            {
                return _Data.Length;
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

            if (offset >= _Data.Length)
                return new byte[0];

            long bytesLeft = _Data.Length - offset;
            if (bytesLeft < length)
                length = (int)bytesLeft;

            if (length == 0)
                return new byte[0];

            var result = new byte[length];
            Array.Copy(_Data, offset, result, 0, length);
            return result;
        }

        #endregion
    }
}