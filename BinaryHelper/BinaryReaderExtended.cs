using System;
using System.IO;

namespace BinaryHelper
{
    /// <summary>
    /// BinaryReader with new methods.
    /// </summary>
    public class BinaryReaderExtended : BinaryReader
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        public BinaryReaderExtended(string filePath) : base(File.OpenRead(filePath))
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">Data.</param>
        public BinaryReaderExtended(byte[] data) : base(new MemoryStream(data))
        {
        }

        /// <summary>
        /// Find position in byte array.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="pattern">Pattern.</param>
        /// <returns></returns>
        public static BinaryPosition FindPosition(byte[] data, byte[] pattern)
        {
            using (BinaryReaderExtended br = new BinaryReaderExtended(data))
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
        public BinaryPosition FindPosition(byte[] pattern, SeekOrigin seekOrigin = SeekOrigin.Current, bool resetPosition = true)
        {
            if (seekOrigin == SeekOrigin.End)
                throw new ArgumentException("'SeekOrigin.End' not supported");

            long currentPos = BaseStream.Position;

            if (seekOrigin == SeekOrigin.Begin)
                BaseStream.Position = 0;

            try
            {
                byte e = 0;
                for (int i = 0; i < BaseStream.Length; i++)
                {
                    for (int p = 0; p < pattern.Length; p++)
                    {
                        if (ReadByte() == pattern[p])
                        {
                            if (++e == pattern.Length)
                            {
                                return new BinaryPosition(BaseStream.Length == BaseStream.Position, BaseStream.Position);
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
                    BaseStream.Position = currentPos;
            }
        }
    }
}
