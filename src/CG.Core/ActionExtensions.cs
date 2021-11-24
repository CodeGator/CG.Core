using CG.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CG
{
    /// <summary>
    /// This class containts extension methods related to the <see cref="Action"/>
    /// type.
    /// </summary>
    public static partial class ActionExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method runs the collection of actions while limiting the number
        /// that run concurrently to, at most, <paramref name="maxConcurrency"/>.
        /// </summary>
        /// <param name="actions">The collection of actions to run.</param>
        /// <param name="maxConcurrency">The maximum number of actions to run 
        /// concurrently. A positive value limits the number of concurrent 
        /// operations to the set value. If it is -1, there is no limit on the 
        /// number of concurrently running operations.</param>
        /// <param name="token">An optional cancellation token.</param>
        public static void WaitAll(
            this IEnumerable<Action> actions,
            int maxConcurrency,
            CancellationToken token = default
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(actions, nameof(actions))
                .ThrowIfLessThan(maxConcurrency, -1, nameof(maxConcurrency))
                .ThrowIfNull(token, nameof(token));

            // Create the parallel options.
            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = maxConcurrency,
                CancellationToken = token
            };

            // Run the tasks in parallel.
            Parallel.Invoke(
                options,
                actions.ToArray()
                );
        }

        // *******************************************************************

        /// <summary>
        /// This method runs the collection of actions while limiting the number
        /// that run concurrently to, at most, <paramref name="maxConcurrency"/>.
        /// </summary>
        /// <param name="actions">The collection of actions to run.</param>
        /// <param name="maxConcurrency">The maximum number of actions to run 
        /// concurrently. A positive value limits the number of concurrent 
        /// operations to the set value. If it is -1, there is no limit on the 
        /// number of concurrently running operations.</param>
        /// <param name="token">An optional cancellation token.</param>
        /// <remarks>A task to perform the oepration.</remarks>
        public static async Task WhenAll(
            this IEnumerable<Action> actions,
            int maxConcurrency,
            CancellationToken token = default
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(actions, nameof(actions))
                .ThrowIfLessThan(maxConcurrency, -1, nameof(maxConcurrency));

            // Run the actions in a task. 
            await Task.Run(() =>
            {
                // Create the parallel options.
                var options = new ParallelOptions
                {
                    MaxDegreeOfParallelism = maxConcurrency,
                    CancellationToken = token
                };

                // Run the tasks in parallel.
                Parallel.Invoke(
                    options,
                    actions.ToArray()
                    );
            }, token
            ).ConfigureAwait(false);
        }

        #endregion
    }
}
