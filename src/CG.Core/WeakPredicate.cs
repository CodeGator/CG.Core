using CG.Validations;
using System;
using System.Reflection;

namespace CG
{
    /// <summary>
    /// This class represents an <see cref="Delegate"/> that doesn't maintain a 
    /// strong reference back to the object's target.
    /// </summary>
    /// <typeparam name="T">The type argument.</typeparam>
    public sealed class WeakPredicate<T>
    {
        // *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields

        /// <summary>
        /// This field contains the raw predicate.
        /// </summary>
        private readonly Predicate<T> _predicate;

        /// <summary>
        /// This field contains reflection information about the predicate.
        /// </summary>
        private readonly MethodInfo _method;

        /// <summary>
        /// This field contains the type of the predicate.
        /// </summary>
        private readonly Type _predicateType;

        /// <summary>
        /// This field contains a weak reference to the target.
        /// </summary>
        private readonly WeakReference _weakReference;

        #endregion

        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains the target of the predicate.
        /// </summary>
        public Predicate<T> Target
        {
            get
            {
                // Do we have a predicate reference?
                if (null != _predicate)
                {
                    // Return the predicate.
                    return _predicate;
                }
                else
                {
                    // If we get here then we need to re-create a reference 
                    //   to the original delete target.

                    // Is the target a static method?
                    if (_method.IsStatic)
                    {
                        // Return the predicate.
                        return _method.CreateDelegate(
                            _predicateType,
                            null
                            ) as Predicate<T>;
                    }

                    // Is the target still alive?
                    if (null != _weakReference.Target)
                    {
                        // Return the predicate.
                        return _method.CreateDelegate(
                            _predicateType,
                            _weakReference.Target
                            ) as Predicate<T>;
                    }

                    // Can't make the predicate.
                    return null;
                }
            }
        }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// this cosntructor creates a new instance of the <see cref="WeakPredicate{T}"/>
        /// class.
        /// </summary>
        /// <param name="predicate">The predicate to use with the weak predicate.</param>
        /// <param name="keepAlive">True to keep the target alive; false otherwise.</param>
        public WeakPredicate(
            Predicate<T> @predicate,
            bool keepAlive
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(@predicate, nameof(@predicate));

            // What sort of reference should we create?
            if (keepAlive)
            {
                // Don't bother wrapping the predicate.
                _predicate = @predicate;
            }
            else
            {
                // Create a weak reference for the target.
                _weakReference = new WeakReference(
                    @predicate.Target
                    );

                // Get information about the predicate method.
                _method = @predicate.GetMethodInfo();

                //  Get the type of the predicate.
                _predicateType = @predicate.GetType();
            }
        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method determines whether the specified object is equal to the 
        /// current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the specified object is equal to the current object; 
        /// false otherwise.</returns>
        public override bool Equals(
            object obj
            )
        {
            // Is the parameter null?
            if (null == obj)
            {
                return false;
            }

            // Are the types mismatched?
            if (!(obj is WeakPredicate<T>))
            {
                return false;
            }

            // Cast the parameter to a predicate.
            var @predicate = obj as Predicate<T>;

            // Is the internal predicate missing?
            if (null != _predicate)
            {
                // Return the compare results.
                return _predicate == @predicate;
            }

            // Are we comparing against a null reference?
            if (null == obj)
            {
                // Return the compare results.
                return !_method.IsStatic && !_weakReference.IsAlive;
            }

            // Return the compare results.
            return _weakReference.Target.Equals(@predicate.Target) &&
                Equals(_method, @predicate.GetMethodInfo());
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a hashcode for the object.
        /// </summary>
        /// <returns>A hashcode for the object.</returns>
        public override int GetHashCode()
        {
            // Return the predicate's hashcode.
            return _predicate.GetHashCode();
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a string representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Return a string representation.
            return $"{base.ToString()} Predicate: {_predicate}";
        }

        #endregion
    }
}
