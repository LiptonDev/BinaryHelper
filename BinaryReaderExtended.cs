using System;
using System.IO;

namespace BinaryHelper
{
    /// <summary>
    /// Extend for BinaryReader.
    /// </summary>
    public static class BinaryReaderExtended
    {
        /// <summary>
        /// Find position in byte array.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="pattern">Pattern.</param>
        /// <returns></returns>
        public static BinaryPosition FindPosition(byte[] data, byte[] pattern)
        {
            using (MemoryStream ms = new MemoryStream(data))
            using (BinaryReader br = new BinaryReader(ms))
            {
                return br.FindPosition(pattern);
            }
        }

        /// <summary>
        /// Find position in file.
        /// </summary>
        /// <param name="pattern">Pattern.</param>
        /// <param name="seekOrigin">Seek origin.</param>
        /// <returns></returns>
        public static BinaryPosition FindPosition(this BinaryReader br, byte[] pattern, SeekOrigin seekOrigin = SeekOrigin.Current, bool resetPosition = true)
        {
            if (seekOrigin == SeekOrigin.End)
                throw new ArgumentException("'SeekOrigin.End' not supported");

            long currentPos = br.BaseStream.Position;

            if (seekOrigin == SeekOrigin.Begin)
                br.BaseStream.Position = 0;

            try
            {
                byte e = 0;
                for (int i = 0; i < br.BaseStream.Length; i++)
                {
                    for (int p = 0; p < pattern.Length; p++)
                    {
                        if (br.ReadByte() == pattern[p])
                        {
                            i++;
                            if (++e == pattern.Length)
                            {
                                return new BinaryPosition(br.BaseStream.Length == br.BaseStream.Position, br.BaseStream.Position);
                            }
                        }
                        else
                        {
                            e = 0;
                            break;
                        }
                    }
                }

                return new BinaryPosition(false, -1);
            }
            finally
            {
                if (resetPosition)
                    br.BaseStream.Position = currentPos;
            }
        }
    }
}
