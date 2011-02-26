﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FileSignatures
{
    /// <summary>
    /// This part of the SimpleSignatures class is generated by SimpleSignatures.tt
    /// </summary>
    [ContentIdentifier]
    internal partial class SimpleSignatures : IContentIdentifier
    {
        public IEnumerable<ContentFormat> Identify(IByteContainer container)
        {
            bool isMatch;

            // archive/zip (application/zip)
            isMatch = Match(container, 0x00000000, new short[] { 0x50, 0x4b, 0x03, 0x04 });
            if (isMatch)
                yield return new ContentFormat("archive", "zip", string.Empty, 4, "application/zip");

            // archive/7z (application/x-7z-compressed)
            isMatch = Match(container, 0x00000000, new short[] { 0x37, 0x7a, 0xbc, 0xaf, 0x27, 0x1c });
            if (isMatch)
                yield return new ContentFormat("archive", "7z", string.Empty, 6, "application/x-7z-compressed");

            // archive/cab (application/vnd.ms-cab-compressed)
            isMatch = Match(container, 0x00000000, new short[] { 0x4d, 0x53, 0x43, 0x46, 0x00, 0x00, 0x00, 0x00 });
            if (isMatch)
                yield return new ContentFormat("archive", "cab", string.Empty, 8, "application/vnd.ms-cab-compressed");

            // archive/bzip/2 (application/x-bzip)
            isMatch = Match(container, 0x00000000, new short[] { 0x42, 0x5a, 0x68, -1, 0x31, 0x41, 0x59, 0x26, 0x53, 0x59 });
            if (isMatch)
                yield return new ContentFormat("archive", "bzip", "2", 9, "application/x-bzip");

            // archive/bzip/1
            isMatch = Match(container, 0x00000000, new short[] { 0x42, 0x5a, 0x30, -1, 0x31, 0x41, 0x59, 0x26, 0x53, 0x59 });
            if (isMatch)
                yield return new ContentFormat("archive", "bzip", "1", 9, string.Empty);

            // archive/gzip (application/x-gzip)
            isMatch = Match(container, 0x00000000, new short[] { 0x1f, 0x8b });
            if (isMatch)
                yield return new ContentFormat("archive", "gzip", string.Empty, 2, "application/x-gzip");

            // archive/wim
            isMatch = Match(container, 0x00000000, new short[] { 0x4d, 0x53, 0x57, 0x49, 0x4d, 0x00 });
            if (isMatch)
                yield return new ContentFormat("archive", "wim", string.Empty, 6, string.Empty);

            // archive/xz (application/x-xz)
            isMatch = Match(container, 0x00000000, new short[] { 0xfd, 0x37, 0x7a, 0x58, 0x5a, 0x00 });
            if (isMatch)
                yield return new ContentFormat("archive", "xz", string.Empty, 6, "application/x-xz");

            // image/bmp (*)
            isMatch = Match(container, 0x00000000, new short[] { 0x42, 0x4d });
            if (isMatch)
                yield return new ContentFormat("image", "bmp", string.Empty, 2, "image/bmp");

            // image/jpeg (*)
            isMatch = Match(container, 0x00000000, new short[] { 0xff, 0xd8 });
            if (isMatch)
                yield return new ContentFormat("image", "jpeg", string.Empty, 2, "image/jpeg");

            // image/jpeg2000 (image/jp2)
            isMatch = Match(container, 0x00000000, new short[] { 0x00, 0x00, 0x00, 0x0C, 0x6A, 0x50, 0x20, 0x20, 0x0D, 0x0A, 0x87, 0x0A, 0x00, 0x00, 0x00, 0x14, 0x66, 0x74, 0x79, 0x70, 0x6A, 0x70, 0x32 });
            if (isMatch)
                yield return new ContentFormat("image", "jpeg2000", string.Empty, 23, "image/jp2");

            // image/pcx/2.5 (image/x-pcx)
            isMatch = Match(container, 0x00000000, new short[] { 0x0a, 0x00 });
            if (isMatch)
                yield return new ContentFormat("image", "pcx", "2.5", 2, "image/x-pcx");

            // image/pcx/2.8 (image/x-pcx)
            isMatch = Match(container, 0x00000000, new short[] { 0x0a, 0x02 });
            if (isMatch)
                yield return new ContentFormat("image", "pcx", "2.8", 2, "image/x-pcx");

            // image/pcx/2.8 (image/x-pcx)
            isMatch = Match(container, 0x00000000, new short[] { 0x0a, 0x03 });
            if (isMatch)
                yield return new ContentFormat("image", "pcx", "2.8", 2, "image/x-pcx");

            // image/pcx/4 (image/x-pcx)
            isMatch = Match(container, 0x00000000, new short[] { 0x0a, 0x04 });
            if (isMatch)
                yield return new ContentFormat("image", "pcx", "4", 2, "image/x-pcx");

            // image/pcx/5 (image/x-pcx)
            isMatch = Match(container, 0x00000000, new short[] { 0x0a, 0x05, 0x01 });
            if (isMatch)
                yield return new ContentFormat("image", "pcx", "5", 3, "image/x-pcx");

            // image/png (*)
            isMatch = Match(container, 0x00000000, new short[] { 0x2e, 0x50, 0x4e, 0x47, 0x0d, 0x0a, 0x1a, 0x0a });
            if (isMatch)
                yield return new ContentFormat("image", "png", string.Empty, 8, "image/png");

            // image/gif (*)
            isMatch = Match(container, 0x00000000, new short[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 });
            if (isMatch)
                yield return new ContentFormat("image", "gif", string.Empty, 6, "image/gif");

            // image/portable-bitmap/ascii (image/x-portable-bitmap)
            isMatch = Match(container, 0x00000000, new short[] { 0x50, 0x31 });
            if (isMatch)
                yield return new ContentFormat("image", "portable-bitmap", "ascii", 2, "image/x-portable-bitmap");

            // image/portable-graymap/ascii (image/x-portable-graymap)
            isMatch = Match(container, 0x00000000, new short[] { 0x50, 0x32 });
            if (isMatch)
                yield return new ContentFormat("image", "portable-graymap", "ascii", 2, "image/x-portable-graymap");

            // image/portable-pixmap/ascii (image/x-portable-pixmap)
            isMatch = Match(container, 0x00000000, new short[] { 0x50, 0x33 });
            if (isMatch)
                yield return new ContentFormat("image", "portable-pixmap", "ascii", 2, "image/x-portable-pixmap");

            // image/portable-bitmap/binary (image/x-portable-bitmap)
            isMatch = Match(container, 0x00000000, new short[] { 0x50, 0x34 });
            if (isMatch)
                yield return new ContentFormat("image", "portable-bitmap", "binary", 2, "image/x-portable-bitmap");

            // image/portable-graymap/binary (image/x-portable-graymap)
            isMatch = Match(container, 0x00000000, new short[] { 0x50, 0x35 });
            if (isMatch)
                yield return new ContentFormat("image", "portable-graymap", "binary", 2, "image/x-portable-graymap");

            // image/portable-pixmap/binary (image/x-portable-pixmap)
            isMatch = Match(container, 0x00000000, new short[] { 0x50, 0x36 });
            if (isMatch)
                yield return new ContentFormat("image", "portable-pixmap", "binary", 2, "image/x-portable-pixmap");

            // image/tiff/motorola (image/tiff)
            isMatch = Match(container, 0x00000000, new short[] { 0x4d, 0x4d, 0x00, 0x2a });
            if (isMatch)
                yield return new ContentFormat("image", "tiff", "motorola", 4, "image/tiff");

            // image/tiff/intel (image/tiff)
            isMatch = Match(container, 0x00000000, new short[] { 0x49, 0x49, 0x2a, 0x00 });
            if (isMatch)
                yield return new ContentFormat("image", "tiff", "intel", 4, "image/tiff");

            // executable/portable
            isMatch = Match(container, 0x00000000, new short[] { 0x4d, 0x5a });
            if (isMatch)
                yield return new ContentFormat("executable", "portable", string.Empty, 2, string.Empty);

            // audio/wave (audio/x-wav)
            isMatch = Match(container, 0x00000000, new short[] { 0x52, 0x49, 0x46, 0x46, -1, -1, -1, -1, 0x57, 0x41, 0x56, 0x45 });
            if (isMatch)
                yield return new ContentFormat("audio", "wave", string.Empty, 8, "audio/x-wav");

            // audio/ogg (application/ogg)
            isMatch = Match(container, 0x00000000, new short[] { 0x4f, 0x67, 0x67, 0x53 });
            if (isMatch)
                yield return new ContentFormat("audio", "ogg", string.Empty, 4, "application/ogg");

            // audio/midi (audio/mid)
            isMatch = Match(container, 0x00000000, new short[] { 0x4d, 0x54, 0x68, 0x64 });
            if (isMatch)
                yield return new ContentFormat("audio", "midi", string.Empty, 4, "audio/mid");

            // document/pdf (application/pdf)
            isMatch = Match(container, 0x00000000, new short[] { 0x25, 0x50, 0x44, 0x46, 0x2d });
            if (isMatch)
                yield return new ContentFormat("document", "pdf", string.Empty, 5, "application/pdf");

            // text/linqpad (text/plain)
            isMatch = Match(container, 0x00000000, new short[] { 0x3c, 0x51, 0x75, 0x65, 0x72, 0x79, 0x20, 0x4b, 0x69, 0x6e, 0x64, 0x3d, 0x22 });
            if (isMatch)
                yield return new ContentFormat("text", "linqpad", string.Empty, 13, "text/plain");

            yield break;
        }
    }
}