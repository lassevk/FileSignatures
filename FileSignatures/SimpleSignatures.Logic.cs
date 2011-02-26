using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FileSignatures
{
    /// <summary>
    /// This class implements basic byte-based signatures.
    /// </summary>
    internal partial class SimpleSignatures
    {
        internal static bool Match(IByteContainer container, int offset, short[] values)
        {
            long realOffset = offset;
            if (realOffset < 0)
                realOffset += container.Length;
            if (realOffset < 0)
                realOffset = 0;
            byte[] fromContainer = container.Read(realOffset, values.Length);
            if (values.Length != fromContainer.Length)
                return false;

            return !values.Where((t, index) => t != -1 && t != fromContainer[index]).Any();
        }
    }
}