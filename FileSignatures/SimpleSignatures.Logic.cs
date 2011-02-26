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
        internal static bool Match(IByteContainer container, int offset, object[] values)
        {
            long realOffset = offset;
            if (realOffset < 0)
                realOffset += container.Length;
            if (realOffset < 0)
                realOffset = 0;
            byte[] fromContainer = container.Read(realOffset, values.Length);
            if (values.Length != fromContainer.Length)
                return false;

            for (int index = 0; index < values.Length; index++)
            {
                object value = values[index];
                byte[] bytes = value as byte[];
                if (bytes != null)
                {
                    if (!bytes.Any(b => fromContainer[index] == b))
                        return false;
                }
                else if (value is int)
                {
                    var i = (int)value;
                    if (i != -1 && fromContainer[index] != i)
                        return false;
                }
            }

            return true;
        }
    }
}