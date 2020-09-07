using System;
using System.Diagnostics;

namespace CG
{
    /// <summary>
    /// This class is a base implementation of the singleton pattern.
    /// </summary>
    /// <typeparam name="T">The type associated with the derived class.</typeparam>
    public abstract class SingletonBase<T>
        where T : SingletonBase<T>
    {
        /// <summary>
        /// This field contains the singleton instance.
        /// </summary>
        private static T _instance;

        /// <summary>
        /// This method creates a new singleton instance of the 
        /// <typeparamref name="T"/> type.
        /// </summary>
        /// <returns>The singleton instance of <typeparamref name="T"/></returns>
        /// <exception cref="MissingMethodException">This exception is thrown
        /// if the type <typeparamref name="T"/> is missing it's private ctor.</exception>
        /// <example>
        /// The following example illustrates how to create a singleton instance of 
        /// the MyClass type:
        /// <code>
        /// Given the following concrete type:
        /// class MyClass : SingletonBase&lt;MyClass&gt;
        /// {
        ///     private MyClass() {} // This private ctor is important!
        /// }
        /// </code>
        /// A singleton instance can be created using the <see cref="SingletonBase{T}.Instance"/>
        /// method:
        /// <code>
        /// void MyTest()
        /// {
        ///     // This creates a singleton instance of MyClass.
        ///     MyClass instance = MyClass.Instance();
        /// }
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static T Instance()
        {
            // Should we create the singleton?
            if (_instance == null)
            {
                // Create the instance of T
                _instance = Activator.CreateInstance(typeof(T), true) as T;
            }

            // Return the instance.
            return _instance;
        }
    }
}
