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

            // archive/zip (application/zip) .zip
            isMatch = Match(container, 0x00000000, new short[] { 0x50, 0x4b, 0x03, 0x04 });
            if (isMatch)
                yield return new ContentFormat("archive", "zip", string.Empty, 4, "application/zip", ".zip");

            // archive/7z (application/x-7z-compressed) .7z
            isMatch = Match(container, 0x00000000, new short[] { 0x37, 0x7a, 0xbc, 0xaf, 0x27, 0x1c });
            if (isMatch)
                yield return new ContentFormat("archive", "7z", string.Empty, 6, "application/x-7z-compressed", ".7z");

            // archive/cab (application/vnd.ms-cab-compressed) .cab
            isMatch = Match(container, 0x00000000, new short[] { 0x4d, 0x53, 0x43, 0x46, 0x00, 0x00, 0x00, 0x00 });
            if (isMatch)
                yield return new ContentFormat("archive", "cab", string.Empty, 8, "application/vnd.ms-cab-compressed", ".cab");

            // archive/bzip/2 (application/x-bzip) .bz2
            isMatch = Match(container, 0x00000000, new short[] { 0x42, 0x5a, 0x68, -1, 0x31, 0x41, 0x59, 0x26, 0x53, 0x59 });
            if (isMatch)
                yield return new ContentFormat("archive", "bzip", "2", 9, "application/x-bzip", ".bz2");

            // archive/bzip/1 .bz
            isMatch = Match(container, 0x00000000, new short[] { 0x42, 0x5a, 0x30, -1, 0x31, 0x41, 0x59, 0x26, 0x53, 0x59 });
            if (isMatch)
                yield return new ContentFormat("archive", "bzip", "1", 9, string.Empty, ".bz");

            // archive/gzip (application/x-gzip) .gz
            isMatch = Match(container, 0x00000000, new short[] { 0x1f, 0x8b });
            if (isMatch)
                yield return new ContentFormat("archive", "gzip", string.Empty, 2, "application/x-gzip", ".gz");

            // archive/wim .wim
            isMatch = Match(container, 0x00000000, new short[] { 0x4d, 0x53, 0x57, 0x49, 0x4d, 0x00 });
            if (isMatch)
                yield return new ContentFormat("archive", "wim", string.Empty, 6, string.Empty, ".wim");

            // archive/xz (application/x-xz) .xm
            isMatch = Match(container, 0x00000000, new short[] { 0xfd, 0x37, 0x7a, 0x58, 0x5a, 0x00 });
            if (isMatch)
                yield return new ContentFormat("archive", "xz", string.Empty, 6, "application/x-xz", ".xm");

            // image/bmp (*) .bmp
            isMatch = Match(container, 0x00000000, new short[] { 0x42, 0x4d });
            if (isMatch)
                yield return new ContentFormat("image", "bmp", string.Empty, 2, "image/bmp", ".bmp");

            // image/jpeg (*) .jpg
            isMatch = Match(container, 0x00000000, new short[] { 0xff, 0xd8 });
            if (isMatch)
                yield return new ContentFormat("image", "jpeg", string.Empty, 2, "image/jpeg", ".jpg");

            // image/jpeg2000 (image/jp2) .jp2
            isMatch = Match(container, 0x00000000, new short[] { 0x00, 0x00, 0x00, 0x0C, 0x6A, 0x50, 0x20, 0x20, 0x0D, 0x0A, 0x87, 0x0A, 0x00, 0x00, 0x00, 0x14, 0x66, 0x74, 0x79, 0x70, 0x6A, 0x70, 0x32 });
            if (isMatch)
                yield return new ContentFormat("image", "jpeg2000", string.Empty, 23, "image/jp2", ".jp2");

            // image/pcx/2.5 (image/x-pcx) .pcx
            isMatch = Match(container, 0x00000000, new short[] { 0x0a, 0x00 });
            if (isMatch)
                yield return new ContentFormat("image", "pcx", "2.5", 2, "image/x-pcx", ".pcx");

            // image/pcx/2.8 (image/x-pcx) .pcx
            isMatch = Match(container, 0x00000000, new short[] { 0x0a, 0x02 });
            if (isMatch)
                yield return new ContentFormat("image", "pcx", "2.8", 2, "image/x-pcx", ".pcx");

            // image/pcx/2.8 (image/x-pcx) .pcx
            isMatch = Match(container, 0x00000000, new short[] { 0x0a, 0x03 });
            if (isMatch)
                yield return new ContentFormat("image", "pcx", "2.8", 2, "image/x-pcx", ".pcx");

            // image/pcx/4 (image/x-pcx) .pcx
            isMatch = Match(container, 0x00000000, new short[] { 0x0a, 0x04 });
            if (isMatch)
                yield return new ContentFormat("image", "pcx", "4", 2, "image/x-pcx", ".pcx");

            // image/pcx/5 (image/x-pcx) .pcx
            isMatch = Match(container, 0x00000000, new short[] { 0x0a, 0x05, 0x01 });
            if (isMatch)
                yield return new ContentFormat("image", "pcx", "5", 3, "image/x-pcx", ".pcx");

            // image/png (*) .png
            isMatch = Match(container, 0x00000000, new short[] { 0x89, 0x50, 0x4e, 0x47, 0x0d, 0x0a, 0x1a, 0x0a });
            if (isMatch)
                yield return new ContentFormat("image", "png", string.Empty, 8, "image/png", ".png");

            // image/gif (*) .gif
            isMatch = Match(container, 0x00000000, new short[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 });
            if (isMatch)
                yield return new ContentFormat("image", "gif", string.Empty, 6, "image/gif", ".gif");

            // image/pbm/ascii (image/x-portable-bitmap) .pbm
            isMatch = Match(container, 0x00000000, new short[] { 0x50, 0x31 });
            if (isMatch)
                yield return new ContentFormat("image", "pbm", "ascii", 2, "image/x-portable-bitmap", ".pbm");

            // image/pgm/ascii (image/x-portable-graymap) .pgm
            isMatch = Match(container, 0x00000000, new short[] { 0x50, 0x32 });
            if (isMatch)
                yield return new ContentFormat("image", "pgm", "ascii", 2, "image/x-portable-graymap", ".pgm");

            // image/ppm/ascii (image/x-portable-pixmap) .ppm
            isMatch = Match(container, 0x00000000, new short[] { 0x50, 0x33 });
            if (isMatch)
                yield return new ContentFormat("image", "ppm", "ascii", 2, "image/x-portable-pixmap", ".ppm");

            // image/pbm/binary (image/x-portable-bitmap) .pbm
            isMatch = Match(container, 0x00000000, new short[] { 0x50, 0x34 });
            if (isMatch)
                yield return new ContentFormat("image", "pbm", "binary", 2, "image/x-portable-bitmap", ".pbm");

            // image/pgm/binary (image/x-portable-graymap) .pgm
            isMatch = Match(container, 0x00000000, new short[] { 0x50, 0x35 });
            if (isMatch)
                yield return new ContentFormat("image", "pgm", "binary", 2, "image/x-portable-graymap", ".pgm");

            // image/ppm/binary (image/x-portable-pixmap) .ppm
            isMatch = Match(container, 0x00000000, new short[] { 0x50, 0x36 });
            if (isMatch)
                yield return new ContentFormat("image", "ppm", "binary", 2, "image/x-portable-pixmap", ".ppm");

            // image/tiff/motorola (image/tiff) .tif
            isMatch = Match(container, 0x00000000, new short[] { 0x4d, 0x4d, 0x00, 0x2a });
            if (isMatch)
                yield return new ContentFormat("image", "tiff", "motorola", 4, "image/tiff", ".tif");

            // image/tiff/intel (image/tiff) .tif
            isMatch = Match(container, 0x00000000, new short[] { 0x49, 0x49, 0x2a, 0x00 });
            if (isMatch)
                yield return new ContentFormat("image", "tiff", "intel", 4, "image/tiff", ".tif");

            // image/tga (image/x-tga) .tga
            isMatch = Match(container, -0x0000012, new short[] { 0x54, 0x52, 0x55, 0x45, 0x56, 0x49, 0x53, 0x49, 0x4f, 0x4e, 0x2d, 0x58, 0x46, 0x49, 0x4c, 0x45, 0x2e, 0x00 });
            if (isMatch)
                yield return new ContentFormat("image", "tga", string.Empty, 18, "image/x-tga", ".tga");

            // video/wmv (video/x-ms-wmv) .wmv
            isMatch = Match(container, 0x00000000, new short[] { 0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF, 0x11, 0xA6, 0xD9, 0x00, 0xAA, 0x00, 0x62, 0xCE, 0x6C });
            if (isMatch)
                yield return new ContentFormat("video", "wmv", string.Empty, 16, "video/x-ms-wmv", ".wmv");

            // video/mkv (video/x-matroska) .mkv
            isMatch = Match(container, 0x00000000, new short[] { 0x1A, 0x45, 0xDF, 0xA3, 0x93, 0x42, 0x82, 0x88, 0x6D, 0x61, 0x74, 0x72, 0x6F, 0x73, 0x6B, 0x61 });
            if (isMatch)
                yield return new ContentFormat("video", "mkv", string.Empty, 16, "video/x-matroska", ".mkv");

            // executable/portable .exe
            isMatch = Match(container, 0x00000000, new short[] { 0x4d, 0x5a });
            if (isMatch)
                yield return new ContentFormat("executable", "portable", string.Empty, 2, string.Empty, ".exe");

            // audio/wave (audio/x-wav) .wav
            isMatch = Match(container, 0x00000000, new short[] { 0x52, 0x49, 0x46, 0x46, -1, -1, -1, -1, 0x57, 0x41, 0x56, 0x45 });
            if (isMatch)
                yield return new ContentFormat("audio", "wave", string.Empty, 8, "audio/x-wav", ".wav");

            // audio/ogg (application/ogg) .ogg
            isMatch = Match(container, 0x00000000, new short[] { 0x4f, 0x67, 0x67, 0x53 });
            if (isMatch)
                yield return new ContentFormat("audio", "ogg", string.Empty, 4, "application/ogg", ".ogg");

            // audio/midi (audio/mid) .mid
            isMatch = Match(container, 0x00000000, new short[] { 0x4d, 0x54, 0x68, 0x64 });
            if (isMatch)
                yield return new ContentFormat("audio", "midi", string.Empty, 4, "audio/mid", ".mid");

            // document/pdf (application/pdf) .pdf
            isMatch = Match(container, 0x00000000, new short[] { 0x25, 0x50, 0x44, 0x46 });
            isMatch = isMatch && Match(container, -0x0000006, new short[] { 0x25, 0x25, 0x45, 0x4f, 0x46, 0x0a });
            if (isMatch)
                yield return new ContentFormat("document", "pdf", string.Empty, 10, "application/pdf", ".pdf");

            // document/pdf (application/pdf) .pdf
            isMatch = Match(container, 0x00000000, new short[] { 0x25, 0x50, 0x44, 0x46 });
            isMatch = isMatch && Match(container, -0x0000006, new short[] { 0x25, 0x25, 0x45, 0x4f, 0x46, 0x0d });
            if (isMatch)
                yield return new ContentFormat("document", "pdf", string.Empty, 10, "application/pdf", ".pdf");

            // document/pdf (application/pdf) .pdf
            isMatch = Match(container, 0x00000000, new short[] { 0x25, 0x50, 0x44, 0x46 });
            isMatch = isMatch && Match(container, -0x0000007, new short[] { 0x25, 0x25, 0x45, 0x4f, 0x46, 0x0d, 0x0a });
            if (isMatch)
                yield return new ContentFormat("document", "pdf", string.Empty, 11, "application/pdf", ".pdf");

            // text/linqpad (text/plain) .linq
            isMatch = Match(container, 0x00000000, new short[] { 0x3c, 0x51, 0x75, 0x65, 0x72, 0x79, 0x20, 0x4b, 0x69, 0x6e, 0x64, 0x3d, 0x22 });
            if (isMatch)
                yield return new ContentFormat("text", "linqpad", string.Empty, 13, "text/plain", ".linq");

            // text/xml (text/xml) .xml
            isMatch = Match(container, 0x00000000, new short[] { 0x3c, 0x3f, 0x78, 0x6d, 0x6c });
            if (isMatch)
                yield return new ContentFormat("text", "xml", string.Empty, 5, "text/xml", ".xml");

            // error-correction/par2 .par2
            isMatch = Match(container, 0x00000000, new short[] { 0x50, 0x41, 0x52, 0x32, 0x00, 0x50, 0x4b, 0x54 });
            if (isMatch)
                yield return new ContentFormat("error-correction", "par2", string.Empty, 8, string.Empty, ".par2");

            yield break;
        }
    }
}