using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FileSignatures
{
    /// <summary>
    /// This interface must be implemented by classes that will act as content identification
    /// implementations for various file formats.
    /// </summary>
    public interface IContentIdentifier
    {
        /// <summary>
        /// When implemented in a class, attempt to identify the contents of the
        /// specified container, and return a collection of all matching file formats.
        /// </summary>
        /// <param name="container">
        /// The <see cref="IByteContainer"/> with the contents that needs to be identified.
        /// </param>
        /// <returns>
        /// A collection of <see cref="ContentFormat"/> objects that match the contents of the
        /// <paramref name="container"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="container"/> is <c>null</c>.</para>
        /// </exception>
        IEnumerable<ContentFormat> Identify(IByteContainer container);
    }
}