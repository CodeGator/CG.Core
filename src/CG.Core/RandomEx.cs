using System;

namespace CG
{
    /// <summary>
    /// This class represents a random number generator with a (slightly) better
    /// default seed generator.
    /// </summary>
    public class RandomEx : Random
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="RandomEx"/>
        /// class.
        /// </summary>
        public RandomEx()
            : base(Guid.NewGuid().GetHashCode()+1)
        {

        }

        // *******************************************************************

        /// <summary>
        /// This constructor creates a new instance of the <see cref="RandomEx"/>
        /// class.
        /// </summary>
        /// <param name="seed">The seed value to use for the operation.</param>
        public RandomEx(
            int seed
            ) : base(seed)
        {

        }

        #endregion
    }
}
