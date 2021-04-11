using System.IO;
using System.Text;

namespace CG.IO
{
    /// <summary>
    /// This class is a string based implementation of <see cref="MemoryStream"/>.
    /// </summary>
    public class StringStream : MemoryStream
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="StringStream"/>
        /// class.
        /// </summary>
        /// <param name="value">The value for the stream.</param>
        public StringStream(
            string value
            ) : base(Encoding.UTF8.GetBytes(value))
        {

        }

        #endregion
    }
}
