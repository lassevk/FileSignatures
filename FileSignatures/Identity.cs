using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace FileSignatures
{
    /// <summary>
    /// This struct holds information about identified contents, such as
    /// the content format category, the name, version number if applicable, etc.
    /// </summary>
    public struct Identity : IEquatable<Identity>
    {
        private readonly string _Category;
        private readonly string _Name;
        private readonly string _Version;

        /// <summary>
        /// Initializes a new instance of the <see cref="Identity"/> struct.
        /// </summary>
        /// <param name="category">
        /// The category of the identified file format.
        /// </param>
        /// <param name="name">
        /// The name of the identified file format.
        /// </param>
        /// <param name="version">
        /// The version of the identified file format, if applicable;
        /// otherwise, <see cref="String.Empty"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="category"/> is <c>null</c> or empty.</para>
        /// <para>- or -</para>
        /// <para><paramref name="name"/> is <c>null</c> or empty.</para>
        /// <para>- or -</para>
        /// <para><paramref name="version"/> is <c>null</c>.</para>
        /// </exception>
        public Identity(string category, string name, string version)
        {
            if (StringEx.IsNullOrWhiteSpace(category))
                throw new ArgumentNullException("category");
            if (StringEx.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");
            if (version == null)
                throw new ArgumentNullException("version");

            _Category = category.Trim();
            _Name = name.Trim();
            _Version = version.Trim();
        }

        /// <summary>
        /// Gets the category of the identified file format.
        /// </summary>
        public string Category
        {
            get
            {
                return _Category;
            }
        }

        /// <summary>
        /// Gets the name of the identified file format.
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
        }

        /// <summary>
        /// Gets the version of the identified file format, if applicable;
        /// otherwise, <see cref="String.Empty"/>.
        /// </summary>
        public string Version
        {
            get
            {
                return _Version;
            }
        }

        #region IEquatable<Identity> Members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">
        /// An object to compare with this object.
        /// </param>
        public bool Equals(Identity other)
        {
            return Equals(other._Category, _Category) && Equals(other._Name, _Name) && Equals(other._Version, _Version);
        }

        #endregion

        /// <summary>
        /// Implements the operator ==, equality comparison.
        /// </summary>
        /// <param name="left">
        /// The left <see cref="Identity"/> operand.
        /// </param>
        /// <param name="right">
        /// The right <see cref="Identity"/> operand.
        /// </param>
        /// <returns>
        /// <c>true</c> if the two <see cref="Identity"/> operands contains the same values;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(Identity left, Identity right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=, inequality comparison.
        /// </summary>
        /// <param name="left">
        /// The left <see cref="Identity"/> operand.
        /// </param>
        /// <param name="right">
        /// The right <see cref="Identity"/> operand.
        /// </param>
        /// <returns>
        /// <c>true</c> if the two <see cref="Identity"/> operands contains different values;
        /// otherwise, <c>false</c> if they contain the same values.
        /// </returns>
        public static bool operator !=(Identity left, Identity right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Object"/> is equal to the current <see cref="Identity"/>.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Object"/> is equal to the current <see cref="Identity"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <param name="obj">
        /// The <see cref="Object"/> to compare with the current <see cref="Identity"/>. 
        /// </param>
        /// <exception cref="NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != typeof(Identity)) return false;
            return Equals((Identity)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="Identity"/>.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = _Category != null ? _Category.GetHashCode() : 0;
                result = (result * 397) ^ (_Name != null ? _Name.GetHashCode() : 0);
                result = (result * 397) ^ (_Version != null ? _Version.GetHashCode() : 0);
                return result;
            }
        }

        /// <summary>
        /// Returns a <see cref="String"/> that represents the current <see cref="Identity"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="String"/> that represents the current <see cref="Identity"/>.
        /// </returns>
        /// <remarks>
        /// The <see cref="String"/> will be on the format "<see cref="Category"/>/<see cref="Name"/>/<see cref="Version"/>"
        /// if <see cref="Version"/> was specified; otherwise just "<see cref="Category"/>/<see cref="Name"/>".
        /// </remarks>
        public override string ToString()
        {
            if (Version.Length > 0)
                return string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", Category, Name, Version);

            return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", Category, Name);
        }
    }
}