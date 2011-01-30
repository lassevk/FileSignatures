using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FileSignatures
{
    /// <summary>
    /// This <see cref="Attribute"/> can be applied to classes that implement <see cref="IContentIdentifier"/>
    /// so that <see cref="ContentIdentifier.ScanAssembly"/> can find the class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    [Serializable]
    public sealed class ContentIdentifierAttribute : Attribute
    {
        // Nothing here
    }
}