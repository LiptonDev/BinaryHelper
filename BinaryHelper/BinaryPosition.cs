namespace BinaryHelper
{
    /// <summary>
    /// Position in binary file.
    /// </summary>
    public class BinaryPosition
    {
        /// <summary>
        /// True - position is located at the end of the file.
        /// </summary>
        public bool EndOfFile { get; }

        /// <summary>
        /// Position in file.
        /// </summary>
        public long Position { get; set; }

        public BinaryPosition(bool endOfFile, long position)
        {
            EndOfFile = endOfFile;
            Position = position;
        }

        public static implicit operator bool(BinaryPosition binaryPosition) => binaryPosition.Position > -1 && !binaryPosition.EndOfFile;
    }
}
