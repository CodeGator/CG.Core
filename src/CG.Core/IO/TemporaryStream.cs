using CG.Validations;
using System.IO;

namespace CG.IO
{
    /// <summary>
    /// This class is a temporary implementation of <see cref="Stream"/> backed
    /// by a temporary disk file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The idea, with this class, is to create a temporary stream mapped to an 
    /// equally temporary backing store. In this case, a temp file. So, this is
    /// good to use when you only need storage for a short period of time. It's
    /// not good if you want the information to stick around since temp files
    /// really aren't guaranteed to live for any set period of time. 
    /// </para>
    /// </remarks>
    public class TemporaryStream : Stream
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains the name of the underlying base stream.
        /// </summary>
        public string Name
        {
            get { return BaseStream.Name; }
        }

        // *******************************************************************

        /// <summary>
        /// This property contains a reference to a base stream.
        /// </summary>
        protected FileStream BaseStream { get; private set; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="TemporaryStream"/>
        /// class.
        /// </summary>
        /// <param name="ext">An optional extension for the underlying disk file.</param>
        public TemporaryStream(
            string ext = ".tmp"
            )
        {
            // Validate the parameter before attempting to use it.
            Guard.Instance().ThrowIfInvalidFileExtension(ext, nameof(ext));

            // Create a temporary base stream.
            BaseStream = File.Create(
                Path.ChangeExtension(
                    Path.GetTempFileName(),
                    ext
                    )
                );
        }

        #endregion

        // *******************************************************************
        // Stream overrides.
        // *******************************************************************

        #region Stream overrides

        /// <summary>
        /// This property gets a value indicating whether the current stream 
        /// supports reading.
        /// </summary>
        public override bool CanRead
        {
            get { return BaseStream.CanRead; }
        }

        // *******************************************************************

        /// <summary>
        /// This property gets a value indicating whether the current stream 
        /// supports seeking.
        /// </summary>
        public override bool CanSeek
        {
            get { return BaseStream.CanSeek; }
        }

        // *******************************************************************

        /// <summary>
        /// This property gets a value indicating whether the current stream 
        /// supports writing.
        /// </summary>
        public override bool CanWrite
        {
            get { return BaseStream.CanWrite; }
        }

        // *******************************************************************

        /// <summary>
        /// This property gets the length in bytes of the stream.
        /// </summary>
        public override long Length
        {
            get { return BaseStream.Length; }
        }

        // *******************************************************************

        /// <summary>
        /// This property gets or sets the current position of this stream.
        /// </summary>
        public override long Position
        {
            get { return BaseStream.Position; }
            set { BaseStream.Position = value; }
        }

        // *******************************************************************

        /// <summary>
        /// This method clears buffers for this stream and causes any buffered 
        /// data to be written to the underlying storage.
        /// </summary>
        public override void Flush()
        {
            BaseStream.Flush();
        }

        // *******************************************************************

        /// <summary>
        /// This method reads a block of bytes from the stream and writes the 
        /// data in a given buffer.
        /// </summary>
        /// <param name="buffer">When this method returns, contains the specified 
        /// byte array with the values between offset and (offset + count - 1) 
        /// replaced by the bytes read from the current source.</param>
        /// <param name="offset">The byte offset in array at which the read bytes 
        /// will be placed.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <returns>The total number of bytes read into the buffer. This might 
        /// be less than the number of bytes requested if that number of bytes 
        /// are not currently available, or zero if the end of the stream is 
        /// reached.</returns>
        public override int Read(
            byte[] buffer,
            int offset,
            int count
            )
        {
            return BaseStream.Read(
                buffer,
                offset,
                count
                );
        }

        // *******************************************************************

        /// <summary>
        /// This method sets the current position of this stream to the given 
        /// value.
        /// </summary>
        /// <param name="offset">The point relative to origin from which to 
        /// begin seeking.</param>
        /// <param name="origin">Specifies the beginning, the end, or the current 
        /// position as a reference point for offset, using a value of type 
        /// System.IO.SeekOrigin.</param>
        /// <returns>The new position in the stream.</returns>
        public override long Seek(
            long offset,
            SeekOrigin origin
            )
        {
            return BaseStream.Seek(
                offset,
                origin
                );
        }

        // *******************************************************************

        /// <summary>
        /// This method sets the length of this stream to the given value.
        /// </summary>
        /// <param name="value">The new length of the stream.</param>
        public override void SetLength(
            long value
            )
        {
            BaseStream.SetLength(value);
        }

        // *******************************************************************

        /// <summary>
        /// This method writes a block of bytes to the file stream.
        /// </summary>
        /// <param name="buffer">The buffer containing data to write to the 
        /// stream.</param>
        /// <param name="offset">The zero-based byte offset in array from which 
        /// to begin copying bytes to the stream.</param>
        /// <param name="count">The maximum number of bytes to write.</param>
        public override void Write(
            byte[] buffer,
            int offset,
            int count
            )
        {
            BaseStream.Write(
                buffer,
                offset,
                count
                );
        }

        // *******************************************************************

        /// <summary>
        /// This method releases the unmanaged resources used by the <see cref="FileStream"/>
        /// and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged 
        /// resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var tempPath = BaseStream.Name;

                BaseStream.Dispose();

                if (File.Exists(tempPath))
                    File.Delete(tempPath);
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
