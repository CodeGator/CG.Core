using CG.Validations;
using System;
using System.Reflection;

namespace CG
{
    /// <summary>
    /// This class represents an <see cref="Action"/> that doesn't maintain a 
    /// strong reference back to the object's target.
    /// </summary>
    public sealed class WeakAction
    {
        // *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields

        /// <summary>
        /// This field contains the raw action.
        /// </summary>
        private readonly Action _action;

        /// <summary>
        /// This field contains reflection information about the action.
        /// </summary>
        private readonly MethodInfo _method;

        /// <summary>
        /// This field contains the type of the action.
        /// </summary>
        private readonly Type _actionType;

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
        /// This property contains the target of the action.
        /// </summary>
        public Action Target
        {
            get
            {
                // Do we have a action reference?
                if (null != _action)
                {
                    // Return the action.
                    return _action;
                }
                else
                {
                    // If we get here then we need to re-create a reference 
                    //   to the original delete target.

                    // Is the target a static method?
                    if (_method.IsStatic)
                    {
                        // Return the action.
                        return _method.CreateDelegate(
                            _actionType,
                            null
                            ) as Action;
                    }

                    // Is the target still alive?
                    if (null != _weakReference.Target)
                    {
                        // Return the action.
                        return _method.CreateDelegate(
                            _actionType,
                            _weakReference.Target
                            ) as Action;
                    }

                    // Can't make the action.
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
        /// this cosntructor creates a new instance of the <see cref="WeakAction"/>
        /// class.
        /// </summary>
        /// <param name="action">The action to use with the weak action.</param>
        /// <param name="keepAlive">True to keep the target alive; false otherwise.</param>
        public WeakAction(
            Action @action,
            bool keepAlive
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(@action, nameof(@action));

            // What sort of reference should we create?
            if (keepAlive)
            {
                // Don't bother wrapping the action.
                _action = @action;
            }
            else
            {
                // Create a weak reference for the target.
                _weakReference = new WeakReference(
                    @action.Target
                    );

                // Get information about the action method.
                _method = @action.GetMethodInfo();

                //  Get the type of the action.
                _actionType = @action.GetType();
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
            if (!(obj is WeakAction))
            {
                return false;
            }

            // Cast the parameter to a action.
            var @action = obj as Action;

            // Is the internal action missing?
            if (null != _action)
            {
                // Return the compare results.
                return _action == @action;
            }

            // Are we comparing against a null reference?
            if (null == obj)
            {
                // Return the compare results.
                return !_method.IsStatic && !_weakReference.IsAlive;
            }

            // Return the compare results.
            return _weakReference.Target.Equals(@action.Target) &&
                Equals(_method, @action.GetMethodInfo());
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a hashcode for the object.
        /// </summary>
        /// <returns>A hashcode for the object.</returns>
        public override int GetHashCode()
        {
            // Return the action's hashcode.
            return _action.GetHashCode();
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a string representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Return a string representation.
            return $"{base.ToString()} Action: {_action}";
        }

        #endregion
    }



    /// <summary>
    /// This class represents an <see cref="Action{T}"/> that doesn't maintain a 
    /// strong reference back to the object's target.
    /// </summary>
    /// <typeparam name="T">The type parameter.</typeparam>
    public sealed class WeakAction<T> 
    {
        // *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields.

        /// <summary>
        /// This field contains the raw action.
        /// </summary>
        private readonly Action<T> _action;

        /// <summary>
        /// This field contains reflection informaiton about the action.
        /// </summary>
        private readonly MethodInfo _method;

        /// <summary>
        /// This field contains the type of the action.
        /// </summary>
        private readonly Type _actionType;

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
        /// This property contains the target of the action.
        /// </summary>
        public Action<T> Target
        {
            get
            {
                // Do we have a action reference?
                if (null != _action)
                {
                    // Return the action.
                    return _action;
                }
                else
                {
                    // If we get here then we need to re-create a reference 
                    //   to the original delete target.

                    // Is the target a static method?
                    if (_method.IsStatic)
                    {
                        // Return the action.
                        return _method.CreateDelegate(
                            _actionType,
                            null
                            ) as Action<T>;
                    }

                    // Is the target still alive?
                    if (null != _weakReference.Target)
                    {
                        // Return the action.
                        return _method.CreateDelegate(
                            _actionType,
                            _weakReference.Target
                            ) as Action<T>;
                    }

                    // Can't make the action.
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
        /// this cosntructor creates a new instance of the <see cref="WeakAction{T}"/>
        /// class.
        /// </summary>
        /// <param name="action">The action to use with the weak action.</param>
        /// <param name="keepAlive">True to keep the target alive; false otherwise.</param>
        public WeakAction(
            Action<T> @action,
            bool keepAlive
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(@action, nameof(@action));

            // What sort of reference should we create?
            if (keepAlive)
            {
                // Don't bother wrapping the action.
                _action = @action;
            }
            else
            {
                // Create a weak reference for the target.
                _weakReference = new WeakReference(
                    @action.Target
                    );

                // Get information about the action method.
                _method = @action.GetMethodInfo();

                //  Get the type of the action.
                _actionType = @action.GetType();
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
            if (!(obj is WeakAction<T>))
            {
                return false;
            }

            // Cast the parameter to a action.
            var @action = obj as Action<T>;

            // Is the internal action missing?
            if (null != _action)
            {
                // Return the compare results.
                return _action == @action;
            }

            // Are we comparing against a null reference?
            if (null == obj)
            {
                // Return the compare results.
                return !_method.IsStatic && !_weakReference.IsAlive;
            }

            // Return the compare results.
            return _weakReference.Target.Equals(@action.Target) &&
                Equals(_method, @action.GetMethodInfo());
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a hashcode for the object.
        /// </summary>
        /// <returns>A hashcode for the object.</returns>
        public override int GetHashCode()
        {
            // Return the action's hashcode.
            return _action.GetHashCode();
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a string representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Return a string representation.
            return $"{base.ToString()} Action: {_action}";
        }

        #endregion
    }



    /// <summary>
    /// This class represents an <see cref="Action{T1, T2}"/> that doesn't maintain a 
    /// strong reference back to the object's target.
    /// </summary>
    /// <typeparam name="T1">The first type parameter.</typeparam>
    /// <typeparam name="T2">The second type parameter.</typeparam>
    public sealed class WeakAction<T1, T2>
    {
        // *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields.

        /// <summary>
        /// This field contains the raw action.
        /// </summary>
        private readonly Action<T1, T2> _action;

        /// <summary>
        /// This field contains reflection informaiton about the action.
        /// </summary>
        private readonly MethodInfo _method;

        /// <summary>
        /// This field contains the type of the action.
        /// </summary>
        private readonly Type _actionType;

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
        /// This property contains the target of the action.
        /// </summary>
        public Action<T1, T2> Target
        {
            get
            {
                // Do we have a action reference?
                if (null != _action)
                {
                    // Return the action.
                    return _action;
                }
                else
                {
                    // If we get here then we need to re-create a reference 
                    //   to the original delete target.

                    // Is the target a static method?
                    if (_method.IsStatic)
                    {
                        // Return the action.
                        return _method.CreateDelegate(
                            _actionType,
                            null
                            ) as Action<T1, T2>;
                    }

                    // Is the target still alive?
                    if (null != _weakReference.Target)
                    {
                        // Return the action.
                        return _method.CreateDelegate(
                            _actionType,
                            _weakReference.Target
                            ) as Action<T1, T2>;
                    }

                    // Can't make the action.
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
        /// this cosntructor creates a new instance of the <see cref="WeakAction{T1, T2}"/>
        /// class.
        /// </summary>
        /// <param name="action">The action to use with the weak action.</param>
        /// <param name="keepAlive">True to keep the target alive; false otherwise.</param>
        public WeakAction(
            Action<T1, T2> @action,
            bool keepAlive
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(@action, nameof(@action));

            // What sort of reference should we create?
            if (keepAlive)
            {
                // Don't bother wrapping the action.
                _action = @action;
            }
            else
            {
                // Create a weak reference for the target.
                _weakReference = new WeakReference(
                    @action.Target
                    );

                // Get information about the action method.
                _method = @action.GetMethodInfo();

                //  Get the type of the action.
                _actionType = @action.GetType();
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
            if (!(obj is WeakAction<T1, T2>))
            {
                return false;
            }

            // Cast the parameter to a action.
            var @action = obj as Action<T1, T2>;

            // Is the internal action missing?
            if (null != _action)
            {
                // Return the compare results.
                return _action == @action;
            }

            // Are we comparing against a null reference?
            if (null == obj)
            {
                // Return the compare results.
                return !_method.IsStatic && !_weakReference.IsAlive;
            }

            // Return the compare results.
            return _weakReference.Target.Equals(@action.Target) &&
                Equals(_method, @action.GetMethodInfo());
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a hashcode for the object.
        /// </summary>
        /// <returns>A hashcode for the object.</returns>
        public override int GetHashCode()
        {
            // Return the action's hashcode.
            return _action.GetHashCode();
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a string representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Return a string representation.
            return $"{base.ToString()} Action: {_action}";
        }

        #endregion
    }
}
