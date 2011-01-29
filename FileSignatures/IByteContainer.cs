using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FileSignatures
{
    /// <summary>
    /// This interface must be implemented by classes that can return bytes from a
    /// sequential random-access source of bytes, typically a file, stream or
    /// a byte array.
    /// </summary>
    public interface IByteContainer
    {
        /// <summary>
        /// Gets the length of the contents in the container.
        /// </summary>
        long Length
        {
            get;
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
        byte[] Read(long offset, int length);
    }
}