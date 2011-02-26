using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FileSignatures
{
    [ContentIdentifier]
    internal class TextFileIdentifier : IContentIdentifier
    {
        public IEnumerable<ContentFormat> Identify(IByteContainer container)
        {
            var toRead = (int)Math.Min(container.Length, 1024);
            byte[] bytes = container.Read(0, toRead);
            if (bytes.Length >= 4 && bytes[0] == 0 && bytes[1] == 0 && bytes[2] == 0xfe && bytes[3] == 0xff)
            {
                // UTF-32, big-endian
                var decoder = new UTF32Encoding(true, false, false);
                var chars = new char[2048];
                int decoded = decoder.GetChars(bytes, 4, bytes.Length - 4, chars, 0);
                if (decoded > (int)(((toRead - 4) / 4) * 0.9))
                {
                    yield return new ContentFormat("text", TextCategory(bytes, decoder), "utf-32/big-endian", (decoded * 4) + 4, "text/plain", ".txt");
                    yield break;
                }
            }
            else if (bytes.Length >= 4 && bytes[0] == 0 && bytes[1] == 0 && bytes[2] == 0xfe && bytes[3] == 0xff)
            {
                // UTF-32, little-endian
                var decoder = new UTF32Encoding(false, false, false);
                var chars = new char[2048];
                int decoded = decoder.GetChars(bytes, 4, bytes.Length - 4, chars, 0);
                if (decoded > (int)(((toRead - 4) / 4) * 0.9))
                {
                    yield return new ContentFormat("text", TextCategory(bytes, decoder), "utf-32/little-endian", (decoded * 4) + 4, "text/plain", ".txt");
                    yield break;
                }
            }
            else if (bytes.Length >= 2 && bytes[0] == 0xfe && bytes[1] == 0xff)
            {
                // UTF-16, big-endian
                var decoder = new UnicodeEncoding(true, false, false);
                var chars = new char[2048];
                int decoded = decoder.GetChars(bytes, 2, bytes.Length - 2, chars, 0);
                if (decoded > (int)(((toRead - 2) / 2) * 0.9))
                {
                    yield return new ContentFormat("text", TextCategory(bytes, decoder), "utf-16/big-endian", (decoded * 2) + 2, "text/plain", ".txt");
                    yield break;
                }
            }
            else if (bytes.Length >= 2 && bytes[0] == 0xff && bytes[1] == 0xfe)
            {
                // UTF-16, little-endian
                var decoder = new UnicodeEncoding(false, false, false);
                var chars = new char[2048];
                int decoded = decoder.GetChars(bytes, 2, bytes.Length - 2, chars, 0);
                if (decoded > (int)(((toRead - 2) / 2) * 0.9))
                {
                    yield return new ContentFormat("text", TextCategory(bytes, decoder), "utf-16/little-endian", (decoded * 2) + 2, "text/plain", ".txt");
                    yield break;
                }
            }
            else if (bytes.Length >= 3 && bytes[0] == 0xef && bytes[1] == 0xbb && bytes[2] == 0xbf)
            {
                // UTF-8
                var decoder = new UTF8Encoding(false, false);
                var chars = new char[2048];
                int decoded = decoder.GetChars(bytes, 3, bytes.Length - 3, chars, 0);
                if (decoded > (int)(((toRead - 3) / 2) * 0.8))
                {
                    yield return new ContentFormat("text", TextCategory(bytes, decoder), "utf-8", (decoded * 2) + 2, "text/plain", ".txt");
                    yield break;
                }
            }

            var encoders = new Dictionary<string, Encoding>
            {
                { "utf-8", new UTF8Encoding(true, true) },
                { "cp1252", Encoding.GetEncoding("Windows-1252") },
                { "iso-8859-1", Encoding.GetEncoding("iso-8859-1") },
                { "utf-16/big-endian", new UnicodeEncoding(true, false, true) },
                { "utf-16/little-endian", new UnicodeEncoding(false, false, true) },
                { "utf-32/big-endian", new UTF32Encoding(true, false, true) },
                { "utf-32/little-endian", new UTF32Encoding(false, false, true) },
            };
            foreach (var encoder in encoders)
            {
                byte[] bytes2;
                try
                {
                    string s = encoder.Value.GetString(bytes, 0, bytes.Length);
                    bytes2 = encoder.Value.GetBytes(s.Substring(0, (int)(s.Length * 0.9)));
                }
                catch
                {
                    continue;
                }
                if (bytes2.Length == 0)
                    continue;
                if (bytes.Any(b => b == 0))
                    continue;
                bool isMatch = true;
                for (int index = 0; index < bytes2.Length; index++)
                {
                    if (bytes[index] != bytes2[index])
                    {
                        isMatch = false;
                        break;
                    }
                }
                if (isMatch)
                {
                    yield return new ContentFormat("text", TextCategory(bytes, encoder.Value), encoder.Key, bytes2.Length, "text/plain", ".txt");
                    yield break;
                }
            }
        }

        private static string TextCategory(byte[] bytes, Encoding encoding)
        {
            string s = encoding.GetString(bytes);
            if (s.Contains("<?xml"))
                return "xml";
            if (s.Contains("<Query Kind="))
                return "linqpad";

            return "plain";
        }
    }
}