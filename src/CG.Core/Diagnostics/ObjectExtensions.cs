using CG.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CG.Diagnostics
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="object"/>
    /// type.
    /// </summary>
    public static partial class ObjectExtensions
    {
        // *******************************************************************
        // Types.
        // *******************************************************************

        #region Types

        /// <summary>
        /// This class is a custom expression visitor, for deconstructing LINQ
        /// expressions at runtime.
        /// </summary>
        private class _ExpressionVisitor : ExpressionVisitor
        {
            /// <summary>
            /// This property contains a stack of expression parts.
            /// </summary>
            public IList<Expression> Expressions { get; } = new List<Expression>();

            /// <summary>
            /// This method is called for each method call in the expression.
            /// </summary>
            /// <param name="node">The node to use for the operation.</param>
            /// <returns>The LINQ expression.</returns>
            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                Expressions.Insert(0, node);
                return base.VisitMethodCall(node);
            }

            /// <summary>
            /// This method is called for each member access in the expression.
            /// </summary>
            /// <param name="node">The node to use for the operation.</param>
            /// <returns>The LINQ expression.</returns>
            protected override Expression VisitMember(MemberExpression node)
            {
                Expressions.Insert(0, node);
                return base.VisitMember(node);
            }
        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method reads a field specified by a LINQ expression and returns 
        /// the value. The linq expression can have nested elements and limited
        /// inline method calls, such as: x => x.A.B.C[2]._myField;
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TFld">The type of field.</typeparam>
        /// <param name="source">The sorce object to use for the operation.</param>
        /// <param name="selector">The LINQ expression to use for the operation.</param>
        /// <param name="value">The value from the specified field.</param>
        /// <returns>True if the field was read; false otherwise.</returns>
        /// <remarks>
        /// <para>The idea, with this method, is to use reflection to go find
        /// and return a field value from an object at runtime. The intent is 
        /// to use this sparingly because the performance isn't great. I see
        /// this approach as something useful for things like unit testing.</para>
        /// </remarks>
        public static bool TryGetFieldValue<TSource, TFld>(
            this TSource source,
            Expression<Func<TSource, TFld>> selector,
            out TFld value
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(source, nameof(source))
                .ThrowIfNull(selector, nameof(selector));

            // Create a visitor to dissect the LINQ expression.
            var visitor = new _ExpressionVisitor();

            // Visit all the expression nodes.
            var e = visitor.Visit(selector.Body);

            // Loop through the expression nodes.
            object obj = source;
            foreach (var exp in visitor.Expressions)
            {
                switch (exp.NodeType)
                {
                    // This is a field access expression.
                    case ExpressionType.MemberAccess:

                        // Look for the reflection info.
                        var fi = (exp as MemberExpression).Member as FieldInfo;
                        if (fi != null)
                        {
                            // Get the value of the field.
                            obj = fi.GetValue(obj);
                        }
                        else
                        {
                            // We failed to get the value of the field.
                            value = default(TFld);
                            return false;
                        }
                        break;

                    // This is a method call expression.
                    case ExpressionType.Call:

                        // Look for the reflection info.
                        var mi = (exp as MethodCallExpression).Method as MethodInfo;
                        if (mi != null)
                        {
                            // Assume the arguments are constants, for now.
                            var args = (exp as MethodCallExpression).Arguments.OfType<ConstantExpression>()
                                .Select(x => x.Value).ToArray();

                            // Call the method.
                            obj = mi.Invoke(obj, args);
                        }
                        break;
                }
            }

            // We got the field value.
            value = (TFld)obj;
            return true;
        }

        // *******************************************************************

        /// <summary>
        /// This method reads a property specified by a LINQ expression and returns 
        /// the value. The linq expression can have nested elements and limited
        /// inline method calls, such as: x => x.A.B.C[2].MyProperty;
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TProp">The type of property.</typeparam>
        /// <param name="source">The sorce object to use for the operation.</param>
        /// <param name="selector">The LINQ expression to use for the operation.</param>
        /// <param name="value">The value from the specified property.</param>
        /// <returns>True if the property was read; false otherwise.</returns>
        /// <remarks>
        /// <para>The idea, with this method, is to use reflection to go find
        /// and return a property value from an object at runtime. The intent is 
        /// to use this sparingly because the performance isn't great. I see
        /// this approach as something useful for things like unit testing.</para>
        /// </remarks>
        public static bool TryGetPropertyValue<TSource, TProp>(
            this TSource source,
            Expression<Func<TSource, TProp>> selector,
            out TProp value
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(source, nameof(source))
                .ThrowIfNull(selector, nameof(selector));

            // Create a visitor to dissect the LINQ expression.
            var visitor = new _ExpressionVisitor();

            // Visit all the expression nodes.
            var e = visitor.Visit(selector.Body);

            // Loop through the expression nodes.
            object obj = source;
            foreach (var exp in visitor.Expressions)
            {
                switch (exp.NodeType)
                {
                    // This is a property access expression.
                    case ExpressionType.MemberAccess:

                        // Look for the reflection info.
                        var pi = (exp as MemberExpression).Member as PropertyInfo;
                        if (pi != null)
                        {
                            // Get the value of the property.
                            obj = pi.GetValue(obj, null);
                        }
                        else
                        {
                            // We failed to get the value of the property.
                            value = default(TProp);
                            return false;
                        }
                        break;

                    // This is a method call expression.
                    case ExpressionType.Call:

                        // Look for the reflection info.
                        var mi = (exp as MethodCallExpression).Method as MethodInfo;
                        if (mi != null)
                        {
                            // Assume the arguments are constants, for now.
                            var args = (exp as MethodCallExpression).Arguments.OfType<ConstantExpression>()
                                .Select(x => x.Value).ToArray();

                            // Call the method.
                            obj = mi.Invoke(obj, args);
                        }
                        break;
                }
            }

            // We got the property value.
            value = (TProp)obj;
            return true;
        }

        // *******************************************************************

        /// <summary>
        /// This method reads a field value from the specified object.
        /// </summary>
        /// <param name="obj">The object to use for the operation.</param>
        /// <param name="fieldName">The field to use for the operation.</param>
        /// <param name="includeProtected">Determines if protected fields are included 
        /// in the search.</param>
        /// <returns>The value of the field.</returns>
        /// <remarks>
        /// <para>The idea, with this method, is to use reflection to go find
        /// and return a field value from an object at runtime. The intent is 
        /// to use this sparingly because the performance isn't great. I see
        /// this approach as something useful for things like unit testing.</para>
        /// </remarks>
        public static object GetFieldValue(
            this object obj,
            string fieldName,
            bool includeProtected = false
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(obj, nameof(obj))
                .ThrowIfNullOrEmpty(fieldName, nameof(fieldName));

            // Get the object type.
            var type = obj.GetType();

            // Find the reflection info for the field.
            var fi = type.GetField(
                fieldName,
                BindingFlags.Static |
                BindingFlags.Instance |
                BindingFlags.Public |
                (includeProtected ? BindingFlags.NonPublic : BindingFlags.Public)
                );

            // Did we fail?
            if (fi == null)
            {
                return null;
            }

            // Get the field value.
            var value = fi.GetValue(obj);

            // Get the actual return type.
            var valType = value.GetType();

            // Deal with weak reference types.
            if (valType == typeof(WeakReference))
            {
                return (value as WeakReference).Target;
            }

            // Return the results.
            return value;
        }

        // *******************************************************************

        /// <summary>
        /// This method reads a field value from the specified object.
        /// </summary>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <param name="obj">The object to use for the operation.</param>
        /// <param name="fieldName">The field to use for the operation.</param>
        /// <param name="includeProtected">Determines if protected fields are included 
        /// in the search.</param>
        /// <returns>The value of the field.</returns>
        /// <remarks>
        /// <para>The idea, with this method, is to use reflection to go find
        /// and return a field value from an object at runtime. The intent is 
        /// to use this sparingly because the performance isn't great. I see
        /// this approach as something useful for things like unit testing.</para>
        /// </remarks>
        public static T GetFieldValue<T>(
            this object obj,
            string fieldName,
            bool includeProtected = false
            ) where T : class
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(obj, nameof(obj))
                .ThrowIfNullOrEmpty(fieldName, nameof(fieldName));

            // Get the object type.
            var type = obj.GetType();

            // Find the reflection info for the field.
            var fi = type.GetField(
                fieldName,
                BindingFlags.Static |
                BindingFlags.Instance |
                BindingFlags.Public |
                (includeProtected ? BindingFlags.NonPublic : BindingFlags.Public)
                );

            // Did we fail?
            if (fi == null)
            {
                return default(T);
            }

            // Get the field value.
            var value = fi.GetValue(obj);

            // Get the actual return type.
            var valType = value.GetType();

            // Deal with weak reference types.
            if (valType == typeof(WeakReference))
            {
                return (T)(value as WeakReference).Target;
            }

            // Deal with strongly typed weak references.
            if (valType == typeof(WeakReference<T>))
            {
                if ((value as WeakReference<T>).TryGetTarget(out T target))
                {
                    return target;
                }
            }

            // Return the results.
            return (T)value;
        }

        // *******************************************************************

        /// <summary>
        /// This method writes a field value for the specified object.
        /// </summary>
        /// <param name="obj">The object to use for the operation.</param>
        /// <param name="fieldName">The field to use for the operation.</param>
        /// <param name="value">The value to use for the operation.</param>
        /// <param name="includeProtected">Determines if protected fields are 
        /// included in the search.</param>
        /// <remarks>
        /// <para>The idea, with this method, is to use reflection to go find
        /// a field on an object and set the value at runtime. The intent is 
        /// to use this sparingly because the performance isn't great. I see
        /// this approach as something useful for things like unit testing.</para>
        /// </remarks>
        public static void SetFieldValue(
            this object obj,
            string fieldName,
            object value,
            bool includeProtected = false
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(obj, nameof(obj))
                .ThrowIfNullOrEmpty(fieldName, nameof(fieldName));

            // Get the object type.
            var type = obj.GetType();

            // Find the reflection info for the field.
            var fi = type.GetField(
                fieldName,
                BindingFlags.Static |
                BindingFlags.Instance |
                BindingFlags.Public |
                (includeProtected ? BindingFlags.NonPublic : BindingFlags.Public)
                );

            // Did we fail?
            if (fi == null)
            {
                return;
            }

            // Set the property value.
            fi.SetValue(obj, value);
        }

        // *******************************************************************

        /// <summary>
        /// This method reads a property value from the specified object.
        /// </summary>
        /// <param name="obj">The object to use for the operation.</param>
        /// <param name="propertyName">The property to use for the operation.</param>
        /// <param name="includeProtected">Determines if protected properties are 
        /// included in the search.</param>
        /// <returns>The value of the property.</returns>
        /// <remarks>
        /// <para>The idea, with this method, is to use reflection to go find
        /// and return a property value from an object at runtime. The intent is 
        /// to use this sparingly because the performance isn't great. I see
        /// this approach as something useful for things like unit testing.</para>
        /// </remarks>
        public static object GetPropertyValue(
            this object obj,
            string propertyName,
            bool includeProtected = false
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(obj, nameof(obj))
                .ThrowIfNullOrEmpty(propertyName, nameof(propertyName));

            // Get the object type.
            var type = obj.GetType();

            // Find the reflection info for the property.
            var pi = type.GetProperty(
                propertyName,
                BindingFlags.Static |
                BindingFlags.Instance |
                BindingFlags.Public |
                (includeProtected ? BindingFlags.NonPublic : BindingFlags.Public) 
                );

            // Did we fail?
            if (pi == null)
            {
                return null;
            }

            // Is the property write-only?
            if (!pi.CanRead)
            {
                return null;
            }

            // Get the property value.
            var value = pi.GetValue(obj, new object[0]);

            // Check for a null return.
            if (value == null)
            {
                return null;
            }

            // Get the actual return type.
            var valType = value.GetType();

            // Deal with weak reference types.
            if (valType == typeof(WeakReference))
            {
                return (value as WeakReference).Target;
            }

            // Return the results.
            return value;
        }

        // *******************************************************************

        /// <summary>
        /// This method reads a property value from the specified object.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="obj">The object to use for the operation.</param>
        /// <param name="propertyName">The property to use for the operation.</param>
        /// <param name="includeProtected">Determines if protected properties are 
        /// included in the search.</param>
        /// <returns>The value of the property.</returns>
        /// <remarks>
        /// <para>The idea, with this method, is to use reflection to go find
        /// and return a property value from an object at runtime. The intent is 
        /// to use this sparingly because the performance isn't great. I see
        /// this approach as something useful for things like unit testing.</para>
        /// </remarks>
        public static T GetPropertyValue<T>(
            this object obj,
            string propertyName,
            bool includeProtected = false
            ) where T : class
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(obj, nameof(obj))
                .ThrowIfNullOrEmpty(propertyName, nameof(propertyName));

            // Get the object type.
            var type = obj.GetType();

            // Find the reflection info for the property.
            var pi = type.GetProperty(
                propertyName,
                BindingFlags.Static |
                BindingFlags.Instance |
                BindingFlags.Public |
                (includeProtected ? BindingFlags.NonPublic : BindingFlags.Public)
                );

            // Did we fail?
            if (pi == null)
            {
                return default(T);
            }

            // Is the property write-only?
            if (!pi.CanRead)
            {
                return default(T);
            }

            // Get the property value.
            var value = pi.GetValue(obj, new object[0]);

            // Get the actual return type.
            var valType = value.GetType();

            // Deal with weak reference types.
            if (valType == typeof(WeakReference))
            {
                return (T)(value as WeakReference).Target;
            }

            // Deal with strongly typed weak references.
            if (valType == typeof(WeakReference<T>))
            {
                if ((value as WeakReference<T>).TryGetTarget(out T target))
                {
                    return target;
                }
            }

            // Return the results.
            return (T)value;
        }

        // *******************************************************************

        /// <summary>
        /// This method writes a property value on the specified object.
        /// </summary>
        /// <param name="obj">The object to use for the operation.</param>
        /// <param name="propertyName">The property to use for the operation.</param>
        /// <param name="value">The value to use for the operation.</param>
        /// <param name="includeProtected">Determines if protected properties are 
        /// included in the search.</param>
        /// <remarks>
        /// <para>The idea, with this method, is to use reflection to go find
        /// a property on an object and set the value at runtime. The intent is 
        /// to use this sparingly because the performance isn't great. I see
        /// this approach as something useful for things like unit testing.</para>
        /// </remarks>
        /// <remarks>
        /// <para>The idea, with this method, is to use reflection to go find
        /// a property on an object and set the value at runtime. The intent is 
        /// to use this sparingly because the performance isn't great. I see
        /// this approach as something useful for things like unit testing.</para>
        /// </remarks>
        public static void SetPropertyValue(
            this object obj,
            string propertyName,
            object value,
            bool includeProtected = false
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(obj, nameof(obj))
                .ThrowIfNullOrEmpty(propertyName, nameof(propertyName));

            // Get the object type.
            var type = obj.GetType();

            // Find the reflection info for the property.
            var pi = type.GetProperty(
                propertyName,
                BindingFlags.Static |
                BindingFlags.Instance |
                BindingFlags.Public |
                (includeProtected ? BindingFlags.NonPublic : BindingFlags.Public)
                );

            // Did we fail?
            if (pi == null)
            {
                return;
            }

            // Is the property read-only?
            if (!pi.CanWrite)
            {
                return;
            }

            // Set the property value.
            pi.SetValue(obj, (object)value);
        }

        #endregion
    }
}
