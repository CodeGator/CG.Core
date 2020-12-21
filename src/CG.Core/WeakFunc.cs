using CG.Validations;
using System;
using System.Reflection;

namespace CG
{
    /// <summary>
    /// This class represents an <see cref="Func{T}"/> that doesn't maintain a 
    /// strong reference back to the object's target.
    /// </summary>
    /// <typeparam name="T">The type parameter.</typeparam>
    public sealed class WeakFunc<T> 
    {
        // *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields

        /// <summary>
        /// This field contains the raw func.
        /// </summary>
        private readonly Func<T> _func;

        /// <summary>
        /// This field contains reflection information about the func.
        /// </summary>
        private readonly MethodInfo _method;

        /// <summary>
        /// This field contains the type of the func.
        /// </summary>
        private readonly Type _funcType;

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
        /// This property contains the target of the func.
        /// </summary>
        public Func<T> Target
        {
            get
            {
                // Do we have a func reference?
                if (null != _func)
                {
                    // Return the func.
                    return _func;
                }
                else
                {
                    // If we get here then we need to re-create a reference 
                    //   to the original delete target.

                    // Is the target a static method?
                    if (_method.IsStatic)
                    {
                        // Return the func.
                        return _method.CreateDelegate(
                            _funcType,
                            null
                            ) as Func<T>;
                    }

                    // Is the target still alive?
                    if (null != _weakReference.Target)
                    {
                        // Return the func.
                        return _method.CreateDelegate(
                            _funcType,
                            _weakReference.Target
                            ) as Func<T>;
                    }

                    // Can't make the func.
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
        /// this cosntructor creates a new instance of the <see cref="WeakFunc{T}"/>
        /// class.
        /// </summary>
        /// <param name="func">The func to use with the weak func.</param>
        /// <param name="keepAlive">True to keep the target alive; false otherwise.</param>
        public WeakFunc(
            Func<T> @func,
            bool keepAlive
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(@func, nameof(@func));

            // What sort of reference should we create?
            if (keepAlive)
            {
                // Don't bother wrapping the func.
                _func = @func;
            }
            else
            {
                // Create a weak reference for the target.
                _weakReference = new WeakReference(
                    @func.Target
                    );

                // Get information about the func method.
                _method = @func.GetMethodInfo();

                //  Get the type of the func.
                _funcType = @func.GetType();
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
            if (!(obj is WeakFunc<T>))
            {
                return false;
            }

            // Cast the parameter to a func.
            var @func = obj as Func<T>;

            // Is the internal func missing?
            if (null != _func)
            {
                // Return the compare results.
                return _func == @func;
            }

            // Are we comparing against a null reference?
            if (null == obj)
            {
                // Return the compare results.
                return !_method.IsStatic && !_weakReference.IsAlive;
            }

            // Return the compare results.
            return _weakReference.Target.Equals(@func.Target) &&
                Equals(_method, @func.GetMethodInfo());
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a hashcode for the object.
        /// </summary>
        /// <returns>A hashcode for the object.</returns>
        public override int GetHashCode()
        {
            // Return the func's hashcode.
            return _func.GetHashCode();
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a string representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Return a string representation.
            return $"{base.ToString()} Func: {_func}";
        }

        #endregion
    }


    /// <summary>
    /// This class represents an <see cref="Func{T1, T2}"/> that doesn't maintain a 
    /// strong reference back to the object's target.
    /// </summary>
    public sealed class WeakFunc<T1, T2>
    {
        // *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields.

        /// <summary>
        /// This field contains the raw func.
        /// </summary>
        private readonly Func<T1, T2> _func;

        /// <summary>
        /// This field contains reflection informaiton about the func.
        /// </summary>
        private readonly MethodInfo _method;

        /// <summary>
        /// This field contains the type of the func.
        /// </summary>
        private readonly Type _funcType;

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
        /// This property contains the target of the func.
        /// </summary>
        public Func<T1, T2> Target
        {
            get
            {
                // Do we have a func reference?
                if (null != _func)
                {
                    // Return the func.
                    return _func;
                }
                else
                {
                    // If we get here then we need to re-create a reference 
                    //   to the original delete target.

                    // Is the target a static method?
                    if (_method.IsStatic)
                    {
                        // Return the func.
                        return _method.CreateDelegate(
                            _funcType,
                            null
                            ) as Func<T1, T2>;
                    }

                    // Is the target still alive?
                    if (null != _weakReference.Target)
                    {
                        // Return the func.
                        return _method.CreateDelegate(
                            _funcType,
                            _weakReference.Target
                            ) as Func<T1, T2>;
                    }

                    // Can't make the func.
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
        /// this cosntructor creates a new instance of the <see cref="WeakFunc{T}"/>
        /// class.
        /// </summary>
        /// <param name="func">The func to use with the weak func.</param>
        /// <param name="keepAlive">True to keep the target alive; false otherwise.</param>
        public WeakFunc(
            Func<T1, T2> @func,
            bool keepAlive
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(@func, nameof(@func));

            // What sort of reference should we create?
            if (keepAlive)
            {
                // Don't bother wrapping the func.
                _func = @func;
            }
            else
            {
                // Create a weak reference for the target.
                _weakReference = new WeakReference(
                    @func.Target
                    );

                // Get information about the func method.
                _method = @func.GetMethodInfo();

                //  Get the type of the func.
                _funcType = @func.GetType();
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
            if (!(obj is WeakFunc<T1, T2>))
            {
                return false;
            }

            // Cast the parameter to a func.
            var @func = obj as Func<T1, T2>;

            // Is the internal func missing?
            if (null != _func)
            {
                // Return the compare results.
                return _func == @func;
            }

            // Are we comparing against a null reference?
            if (null == obj)
            {
                // Return the compare results.
                return !_method.IsStatic && !_weakReference.IsAlive;
            }

            // Return the compare results.
            return _weakReference.Target.Equals(@func.Target) &&
                Equals(_method, @func.GetMethodInfo());
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a hashcode for the object.
        /// </summary>
        /// <returns>A hashcode for the object.</returns>
        public override int GetHashCode()
        {
            // Return the func's hashcode.
            return _func.GetHashCode();
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a string representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Return a string representation.
            return $"{base.ToString()} Func: {_func}";
        }

        #endregion
    }
}
