using CG.Core.Properties;
using CG.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CG.Collections.Generic
{
    /// <summary>
    /// This class contains extension methods realted to the <see cref="IEnumerable{T}"/>
    /// type.
    /// </summary>
    public static partial class EnumerableExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method returns elements from a sequence that are distinct, on 
        /// the specified property.
        /// </summary>
        /// <typeparam name="T">The type associated with the sequences.</typeparam>
        /// <typeparam name="K">The type associated with the property.</typeparam>
        /// <param name="lhs">The left-hand queryable sequence.</param>
        /// <param name="rhs">The right-hand LINQ expression to select a property.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> sequence.</returns>
        public static IEnumerable<T> DistinctOn<T, K>(
            this IEnumerable<T> lhs,
            Func<T, K> rhs
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(lhs, nameof(lhs))
                .ThrowIfNull(rhs, nameof(rhs));

            // Perform the operation.
            return lhs.GroupBy(rhs)
                .Select(x => x.First())
                .AsEnumerable();
        }

        // *******************************************************************

        /// <summary>
        /// This method filters out elements based on the contents of a comma
        /// separated black list. 
        /// </summary>
        /// <typeparam name="T">The type associated with the sequence.</typeparam>
        /// <param name="sequence">The sequence to use for the operation.</param>
        /// <param name="selector">The selector to apply for the operation.</param>
        /// <param name="blackList">The comma separated black list to use for 
        /// the operation.</param>
        /// <returns>An enumerable sequence of <typeparamref name="T"/> items.</returns>
        /// <remarks>
        /// <para>The intent, with this method, is to quickly filter an enumerable
        /// sequence by applying a black list to a specific element. So, anything 
        /// in the sequence that matches the corresponding black list is dropped
        /// from the collection.</para>
        /// </remarks>
        public static IEnumerable<T> ApplyBlackList<T>(
            this IEnumerable<T> sequence,
            Func<T, string> selector,
            string blackList
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(sequence, nameof(sequence))
                .ThrowIfNull(selector, nameof(selector));

            // Break up the black list into parts.
            var blackParts = blackList.Split(',');

            // An interesting phenomena that I've noticed is that, on it's own,
            //   the line below tends to produce duplicate results. I had to
            //   add the DistincOn call to stop that. The interesting part is,
            //   because, in this context, we have no idea what the elements 
            //   are in the sequence (type T, nothing else to go on), we can't
            //   pick a known unique value. In fact, even GetHashCode will often
            //   fail because the underlying objects may not be the same, even
            //   when they otherwise contain the same data. So, as a compromise,
            //   I'm calling distinct on a string representation of each object.
            // Yes, it's a hack. Yes, it will break if the string representations
            //   don't otherwise contain the property that makes an instance unique.
            // No, I haven't yet figured out any better approach.

            // Filter out anything on the black list.
            var result = sequence.Where(
                x => !blackParts.Any(y => selector(x).IsMatch(y.Trim()))
                ).DistinctOn(z => z.ToString())
                .ToList();

            // Return the results.
            return result;
        }

        // *******************************************************************

        /// <summary>
        /// This method filters out elements based on the contents of a comma
        /// separated white list. 
        /// </summary>
        /// <typeparam name="T">The type associated with the sequence.</typeparam>
        /// <param name="sequence">The sequence to use for the operation.</param>
        /// <param name="selector">The selector to apply for the operation.</param>
        /// <param name="whiteList">The comma separated white list to use for 
        /// the operation.</param>
        /// <returns>An enumerable sequence of <typeparamref name="T"/> items.</returns>
        /// <remarks>
        /// <para>The intent, with this method, is to quickly filter an enumerable
        /// sequence by applying a white list to a specific element. So, anything 
        /// in the sequence that doesn't match the white list is dropped from the 
        /// collection.</para>
        /// </remarks>
        public static IEnumerable<T> ApplyWhiteList<T>(
            this IEnumerable<T> sequence,
            Func<T, string> selector,
            string whiteList
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(sequence, nameof(sequence))
                .ThrowIfNull(selector, nameof(selector));

            // Break up the white list into parts.
            var whiteParts = whiteList.Split(',');

            // An interesting phenomena that I've noticed is that, on it's own,
            //   the line below tends to produce duplicate results. I had to
            //   add the DistincOn call to stop that. The interesting part is,
            //   because, in this context, we have no idea what the elements 
            //   are in the sequence (type T, nothing else to go on), we can't
            //   pick a known unique value. In fact, even GetHashCode will often
            //   fail because the underlying objects may not be the same, even
            //   when they otherwise contain the same data. So, as a compromise,
            //   I'm calling distinct on a string representation of each object.
            // Yes, it's a hack. Yes, it will break if the string representations
            //   don't otherwise contain the property that makes an instance unique.
            // No, I haven't yet figured out any better approach.

            // Filter out anything not on the white list.
            var results = sequence.Where(
                x => whiteParts.Any(y => selector(x).IsMatch(y.Trim()))
                ).DistinctOn(z => z.ToString())
                 .ToList();

            // Return the results.
            return results;
        }

        // *******************************************************************

        /// <summary>
        /// This method recursively iterates through an enumerable sequence
        /// applying the specified delegate action to each item.
        /// </summary>
        /// <typeparam name="T">The type of object in the sequence.</typeparam>
        /// <param name="sequence">The enumerable sequence to use for the operation.</param>
        /// <param name="selector">The selector for finding child sequences.</param>
        /// <param name="action">The delegate to apply to each item in the sequence.</param>
        /// <remarks>
        /// <para>The intent, with this method, is to quickly enumerate through
        /// the items in an enumerable sequence, without setting up the standard
        /// .NET foreach loop. Any errors are collected and thrown as a single
        /// <see cref="AggregateException"/> object.</para>
        /// <para>This approach is not good for all looping scenarios - such as 
        /// breaking from a loop early, or returning values from a loop, etc. So
        /// use your best judgment when deciding to loop this way.</para>
        /// </remarks>
        /// <example>
        /// This example shows how to call the <see cref="EnumerableExtensions.ForEach{T}(IEnumerable{T}, Func{T, IEnumerable{T}}, Action{T})"/>
        /// method.
        /// <code>
        /// class TestClass
        /// {
        ///     class TestType
        ///     {
        ///         public int Id { get; set; }
        ///         public TestType[] C {get; set; }
        ///     }
        ///
        ///     static void Main()
        ///     {
        ///         var list = new TestType[]
        ///         {
        ///             new TestType()
        ///             {
        ///                 Id = 0, 
        ///                 C = new TestType[]
        ///                 {
        ///                     new TestType
        ///                     {
        ///                         Id = 1,
        ///                         C = new TestType[0]
        ///                     }
        ///                 }
        ///            },
        ///            new TestType()
        ///            {
        ///                Id = 2,
        ///                C = new TestType[]
        ///                {
        ///                    new TestType
        ///                    {
        ///                        Id = 3,
        ///                        C = new TestType[0]
        ///                    }
        ///                }
        ///            }
        ///        };
        ///        
        ///        list.ForEach(x => x.C, y => 
        ///        { 
        ///            Console.Write($"{Id}");
        ///        });
        ///    }
        /// }
        /// </code>
        /// Prints: 0123
        /// </example>
        public static void ForEach<T>(
            this IEnumerable<T> sequence,
            Func<T, IEnumerable<T>> selector,
            Action<T> action
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(sequence, nameof(sequence))
                .ThrowIfNull(selector, nameof(selector))
                .ThrowIfNull(action, nameof(action));

            var errors = new List<Exception>();

            // Loop through each item in the sequence.
            foreach (var item in sequence)
            {
                try
                {
                    // Loop through each child item in the sequence.
                    selector(item).ForEach(
                        selector,
                        action
                        );

                    // Apply the action to the item.
                    action(item);
                }
                catch (Exception ex)
                {
                    // Remember any errors.
                    errors.Add(ex);
                }
            }

            // Were there any errors?
            if (errors.Any())
            {
                // Wrap any errors.
                throw new AggregateException(
                    Resources.IEnumerableExtensions_ForEach,
                    innerExceptions: errors
                    );
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method iterates through an enumerable sequence applying the 
        /// specified delegate action to each item.
        /// </summary>
        /// <typeparam name="T">The type of object in the sequence.</typeparam>
        /// <param name="sequence">The enumerable sequence to use for the operation.</param>
        /// <param name="action">The delegate to apply to each item in the sequence.</param>
        /// <remarks>
        /// <para>The intent, with this method, is to quickly enumerate through
        /// the items in an enumerable sequence, without setting up the standard
        /// .NET foreach loop. Any errors are collected and thrown as a single
        /// <see cref="AggregateException"/> object.</para>
        /// <para>This approach is not good for all looping scenarios - such as 
        /// breaking from a loop early, or returning values from a loop, etc. So
        /// use your best judgment when deciding to loop this way.</para>
        /// </remarks>
        /// <example>
        /// This example shows how to call the <see cref="EnumerableExtensions.ForEach{T}(IEnumerable{T}, Action{T})"/>
        /// method.
        /// <code>
        /// class TestClass 
        /// {
        ///     static void Main()
        ///     {
        ///         var list = new int[]
        ///         {
        ///             0, 1, 2, 3
        ///         };
        ///         
        ///         list.ForEach(x => 
        ///         {
        ///             Console.Write(x);
        ///         });
        ///     }
        /// }
        /// </code>
        /// Prints: 0123
        /// </example>
        public static void ForEach<T>(
            this IEnumerable<T> sequence,
            Action<T> action
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(sequence, nameof(sequence))
                .ThrowIfNull(action, nameof(action));

            var errors = new List<Exception>();

            // Loop through each item in the sequence.
            foreach (var item in sequence)
            {
                try
                {
                    // Apply the action to the item.
                    action(item);
                }
                catch (Exception ex)
                {
                    // Remember any errors.
                    errors.Add(ex);
                }
            }

            // Were there any errors?
            if (errors.Any())
            {
                // Wrap any errors.
                throw new AggregateException(
                    Resources.IEnumerableExtensions_ForEach,
                    innerExceptions: errors
                    );
            }
        }

        #endregion
    }
}
