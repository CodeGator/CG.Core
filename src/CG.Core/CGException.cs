using System;
using System.Runtime.Serialization;

namespace CG
{
    /// <summary>
    /// This class represents a codegator related exception.
    /// </summary>
    [Serializable]
    public class CGException : Exception
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="CGException"/>
        /// class.
        /// </summary>
        public CGException()
        {

        }

        // *******************************************************************

        /// <summary>
        /// This constructor creates a new instance of the <see cref="CGException"/>
        /// class.
        /// </summary>
        /// <param name="message">A message for the exception.</param>
        /// <param name="innerException">An inner exception for the exception.</param>
        public CGException(
            string message,
            Exception innerException
            ) : base(message, innerException)
        {

        }

        // *******************************************************************

        /// <summary>
        /// This constructor creates a new instance of the <see cref="CGException"/>
        /// class.
        /// </summary>
        /// <param name="message">A message for the exception.</param>
        public CGException(string message)
            : base(message)
        {

        }

        // *******************************************************************

        /// <summary>
        /// This constructor creates a new instance of the <see cref="CGException"/>
        /// class.
        /// </summary>
        /// <param name="info">The serialization info to use for the exception.</param>
        /// <param name="context">The streaming context to use for the exception.</param>
        public CGException(
            SerializationInfo info,
            StreamingContext context
            ) : base(info, context)
        {

        }

        #endregion
    }
}
