using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FileSignatures
{
    /// <summary>
    /// This class implements a system for identifying file, stream, byte array, or similar contents
    /// in terms of file formats.
    /// </summary>
    public sealed class Identifier
    {
        private static readonly Identifier _Default = GetDefaultIdentifier();
        private readonly HashSet<Type> _Identifiers = new HashSet<Type>();

        /// <summary>
        /// Gets the default <see cref="Identifier"/> instance.
        /// </summary>
        /// <value>
        /// The default <see cref="Identifier"/> instance.
        /// </value>
        public static Identifier Default
        {
            get
            {
                return _Default;
            }
        }

        /// <summary>
        /// Creates and initializes the default <see cref="Identifier"/> instance.
        /// </summary>
        /// <returns>
        /// The default <see cref="Identifier"/> instance.
        /// </returns>
        private static Identifier GetDefaultIdentifier()
        {
            var result = new Identifier();
            result.ScanAssembly(typeof(Identifier).Assembly);
            return result;
        }

        /// <summary>
        /// Identifies the contents of the file with the specified filename.
        /// </summary>
        /// <param name="filename">
        /// The full path to and name of the file to identify the contents of.
        /// </param>
        /// <returns>
        /// An collection of <see cref="ContentFormat"/> objects that identify the file contents.
        /// </returns>
        /// <remarks>
        /// Note that the returned collection contains the <see cref="ContentFormat"/> objects
        /// ordered in such a way that the first one is deemed the one most likely to be
        /// correct, the next one less likely, the next one after that even less likely,
        /// and so on.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="filename"/> is <c>null</c> or empty.</para>
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// <para><paramref name="filename"/> specifies a file that does not exist.</para>
        /// </exception>
        public IEnumerable<ContentFormat> Identify(string filename)
        {
            if (StringEx.IsNullOrWhiteSpace(filename))
                throw new ArgumentNullException("filename");

            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Identify(stream);
            }
        }

        /// <summary>
        /// Identifies the contents of the specified file.
        /// </summary>
        /// <param name="file">
        /// The <see cref="FileInfo"/> object that refers to the file.
        /// </param>
        /// <returns>
        /// An collection of <see cref="ContentFormat"/> objects that identify the file contents.
        /// </returns>
        /// <remarks>
        /// Note that the returned collection contains the <see cref="ContentFormat"/> objects
        /// ordered in such a way that the first one is deemed the one most likely to be
        /// correct, the next one less likely, the next one after that even less likely,
        /// and so on.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="file"/> is <c>null</c>.</para>
        /// </exception>
        public IEnumerable<ContentFormat> Identify(FileInfo file)
        {
            if (file == null)
                throw new ArgumentNullException("file");

            using (FileStream stream = file.OpenRead())
            {
                return Identify(stream);
            }
        }

        /// <summary>
        /// Identifies the contents of the specified stream.
        /// </summary>
        /// <param name="stream">
        /// The <see cref="Identify(System.IO.Stream)"/> object with the contents to identify.
        /// </param>
        /// <returns>
        /// An collection of <see cref="ContentFormat"/> objects that identify the stream contents.
        /// </returns>
        /// <remarks>
        /// Note that the returned collection contains the <see cref="ContentFormat"/> objects
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
        public IEnumerable<ContentFormat> Identify(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (!stream.CanRead)
                throw new ArgumentException("Cannot read from the specified stream, CanRead returns false", "stream");

            var container = new StreamContainer(stream);
            return Identify(container);
        }

        /// <summary>
        /// Identifies the contents of the specified byte array.
        /// </summary>
        /// <param name="data">
        /// The <see cref="Byte"/>[] array containing the contents to identify.
        /// </param>
        /// <returns>
        /// An collection of <see cref="ContentFormat"/> objects that identify the byte array contents.
        /// </returns>
        /// <remarks>
        /// Note that the returned collection contains the <see cref="ContentFormat"/> objects
        /// ordered in such a way that the first one is deemed the one most likely to be
        /// correct, the next one less likely, the next one after that even less likely,
        /// and so on.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="data"/> is <c>null</c>.</para>
        /// </exception>
        public IEnumerable<ContentFormat> Identify(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            var container = new ByteArrayContainer(data);
            return Identify(container);
        }

        /// <summary>
        /// Identifies the contents of the specified container.
        /// </summary>
        /// <param name="container">
        /// The <see cref="IByteContainer"/> with the contents to identify.
        /// </param>
        /// <returns>
        /// An collection of <see cref="ContentFormat"/> objects that identify the <paramref name="container"/> contents.
        /// </returns>
        /// <remarks>
        /// Note that the returned collection contains the <see cref="ContentFormat"/> objects
        /// ordered in such a way that the first one is deemed the one most likely to be
        /// correct, the next one less likely, the next one after that even less likely,
        /// and so on.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="container"/> is <c>null</c>.</para>
        /// </exception>
        public ContentFormat[] Identify(IByteContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            var formats = new List<ContentFormat>();
            foreach (Type type in _Identifiers)
            {
                var identifier = (IContentIdentifier)Activator.CreateInstance(type);
                using (identifier as IDisposable)
                {
                    formats.AddRange(identifier.Identify(container));
                }
            }
            return formats.OrderByDescending(f => f.Confidence).ToArray();
        }

        /// <summary>
        /// Registers the type of a class that implements the <see cref="IContentIdentifier"/>
        /// interface and can participate in content format identification.
        /// </summary>
        /// <param name="contentIdentifierType">
        /// The <see cref="Type"/> of a class that implements <see cref="IContentIdentifier"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="contentIdentifierType"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="contentIdentifierType"/> refers to an abstract type.</para>
        /// <para>- or -</para>
        /// <para><paramref name="contentIdentifierType"/> refers to a type that does not implement <see cref="IContentIdentifier"/>.</para>
        /// <para>- or -</para>
        /// <para><paramref name="contentIdentifierType"/> refers to a type that does not have a public parameterless constructor.</para>
        /// </exception>
        public void RegisterContentIdentifier(Type contentIdentifierType)
        {
            if (contentIdentifierType == null)
                throw new ArgumentNullException("contentIdentifierType");

            if (contentIdentifierType.IsAbstract)
                throw new ArgumentException("contentIdentifierType is an abstract type", "contentIdentifierType");
            if (!typeof(IContentIdentifier).IsAssignableFrom(contentIdentifierType))
                throw new ArgumentException("contentIdentifierType does not implement IContentIdentifier", "contentIdentifierType");
            if (contentIdentifierType.GetConstructor(new Type[0]) == null)
                throw new ArgumentException("contentIdentifierType does not have a public parameterless constructor", "contentIdentifierType");

            _Identifiers.Add(contentIdentifierType);
        }

        /// <summary>
        /// Scans the specified assembly for matching content identifiers, classes that
        /// implement <see cref="IContentIdentifier"/>, is tagged with the <see cref="ContentIdentifierAttribute"/>
        /// attribute, and has a public parameterless constructor.
        /// </summary>
        /// <param name="assembly">
        /// The <see cref="Assembly"/> to scan for matching content identifier types.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="assembly"/> is <c>null</c>.</para>
        /// </exception>
        public void ScanAssembly(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            Type requiredInterface = typeof(IContentIdentifier);
            var ctorParameterTypes = new Type[0];
            foreach (Type type in assembly.GetTypes())
            {
                if (!type.IsClass)
                    continue;
                if (type.IsAbstract)
                    continue;
                if (!requiredInterface.IsAssignableFrom(type))
                    continue;
                if (type.GetConstructor(ctorParameterTypes) == null)
                    continue;

                RegisterContentIdentifier(type);
            }
        }

        /// <summary>
        /// Scans all the assemblies in the current <see cref="AppDomain"/> for matching content identifiers, classes that
        /// implement <see cref="IContentIdentifier"/>, is tagged with the <see cref="ContentIdentifierAttribute"/>
        /// attribute, and has a public parameterless constructor.
        /// </summary>
        public void ScanAppDomain()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                ScanAssembly(assembly);
        }
    }
}