using CG.Validations;
using System;
using System.Reflection;

namespace CG
{
    /// <summary>
    /// This class represents an <see cref="Delegate"/> that doesn't maintain a 
    /// strong reference back to the object's target.
    /// </summary>
    public sealed class WeakDelegate
    {
        // *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields

        /// <summary>
        /// This field contains the raw delegate.
        /// </summary>
        private readonly Delegate _delegate;

        /// <summary>
        /// This field contains reflection information about the delegate.
        /// </summary>
        private readonly MethodInfo _method;

        /// <summary>
        /// This field contains the type of the delegate.
        /// </summary>
        private readonly Type _delegateType;

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
        /// This property contains the target of the delegate.
        /// </summary>
        public Delegate Target
        {
            get
            {
                // Do we have a delegate reference?
                if (null != _delegate)
                {
                    // Return the delegate.
                    return _delegate;
                }
                else
                {
                    // If we get here then we need to re-create a reference 
                    //   to the original delete target.

                    // Is the target a static method?
                    if (_method.IsStatic)
                    {
                        // Return the delegate.
                        return _method.CreateDelegate(
                            _delegateType,
                            null
                            );
                    }

                    // Is the target still alive?
                    if (null != _weakReference.Target)
                    {
                        // Return the delegate.
                        return _method.CreateDelegate(
                            _delegateType,
                            _weakReference.Target
                            );
                    }

                    // Can't make the delegate.
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
        /// this cosntructor creates a new instance of the <see cref="WeakDelegate"/>
        /// class.
        /// </summary>
        /// <param name="delegate">The delegate to use with the weak delegate.</param>
        /// <param name="keepAlive">True to keep the target alive; false otherwise.</param>
        public WeakDelegate(
            Delegate @delegate,
            bool keepAlive
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(@delegate, nameof(@delegate));

            // What sort of reference should we create?
            if (keepAlive)
            {
                // Don't bother wrapping the delegate.
                _delegate = @delegate;
            }
            else
            {
                // Create a weak reference for the target.
                _weakReference = new WeakReference(
                    @delegate.Target
                    );

                // Get information about the delegate method.
                _method = @delegate.GetMethodInfo();

                //  Get the type of the delegate.
                _delegateType = @delegate.GetType();
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
            if (!(obj is WeakDelegate))
            {
                return false;
            }

            // Cast the parameter to a delegate.
            var @delegate = obj as Delegate;

            // Is the internal delegate missing?
            if (null != _delegate)
            {
                // Return the compare results.
                return _delegate == @delegate;
            }

            // Are we comparing against a null reference?
            if (null == obj)
            {
                // Return the compare results.
                return !_method.IsStatic && !_weakReference.IsAlive;
            }

            // Return the compare results.
            return _weakReference.Target.Equals(@delegate.Target) &&
                Equals(_method, @delegate.GetMethodInfo());
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a hashcode for the object.
        /// </summary>
        /// <returns>A hashcode for the object.</returns>
        public override int GetHashCode()
        {
            // Return the delegate's hashcode.
            return _delegate.GetHashCode();
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a string representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Return a string representation.
            return $"{base.ToString()} Delegate: {_delegate}";
        }

        #endregion
    }
}
