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
            byte[] fromContainer = container.Read(offset, values.Length);
            if (values.Length != fromContainer.Length)
                return false;

            for (int index = 0; index < values.Length; index++)
                if (values[index] != -1 && values[index] != fromContainer[index])
                    return false;

            return true;
        }
    }
}