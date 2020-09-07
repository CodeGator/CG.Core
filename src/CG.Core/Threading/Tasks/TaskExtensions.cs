using CG.Collections.Generic;
using CG.Validations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Threading.Tasks
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="Task"/>
    /// type.
    /// </summary>
    public static partial class TaskExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method runs the collection of tasks while limiting the number
        /// that run concurrently to, at most, <paramref name="maxConcurrency"/>.
        /// </summary>
        /// <param name="tasks">The collection of tasks to run.</param>
        /// <param name="maxConcurrency">The maximum number of tasks to run 
        /// concurrently. A positive value limits the number of concurrent 
        /// operations to the set value. If it is -1, there is no limit on the 
        /// number of concurrently running operations.</param>
        /// <param name="maxTimeout">The maximum number of milliseconds to wait
        /// for the operations to finish.</param>
        /// <param name="token">An optional cancellation token.</param>
        public static void WaitAll(
            this IEnumerable<Task> tasks,
            int maxConcurrency,
            int maxTimeout = -1,
            CancellationToken token = default(CancellationToken)
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(tasks, nameof(tasks))
                .ThrowIfLessThan(maxConcurrency, -1, nameof(maxConcurrency))
                .ThrowIfLessThan(maxTimeout, -1, nameof(maxTimeout))
                .ThrowIfNull(token, nameof(token));

            // Force a valid value for the concurrency.
            if (maxConcurrency <= 0)
            {
                maxConcurrency = int.MaxValue;
            }

            using (var sync = new SemaphoreSlim(maxConcurrency))
            {
                var postTaskTasks = new List<Task>();

                // Each task should release the sempahore.
                tasks.ForEach(t => postTaskTasks.Add(
                    t.ContinueWith(
                        tsk => sync.Release()
                        )
                    ));

                // Loop and start the tasks.
                foreach (var task in tasks)
                {
                    // Wait for the sempahore.
                    sync.Wait(maxTimeout, token);

                    // If the token cancelled, panic!
                    token.ThrowIfCancellationRequested();

                    // Should we start the task?
                    if (task.Status != TaskStatus.WaitingToRun)
                    {
                        task.Start();
                    }
                }

                // Wait for the tasks to finish.
                Task.WaitAll(
                    postTaskTasks.ToArray(),
                    maxTimeout,
                    token
                    );
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method runs the collection of tasks while limiting the number
        /// that run concurrently to, at most, <paramref name="maxConcurrency"/>.
        /// </summary>
        /// <param name="tasks">The collection of tasks to run.</param>
        /// <param name="maxConcurrency">The maximum number of tasks to run 
        /// concurrently. A positive value limits the number of concurrent 
        /// operations to the set value. If it is -1, there is no limit on the 
        /// number of concurrently running operations.</param>
        /// <param name="maxTimeout">The maximum number of milliseconds to wait
        /// for the operations to finish.</param>
        /// <param name="token">An optional cancellation token.</param>
        /// <remarks>A task to perform the operation.</remarks>
        public static async Task WhenAll(
            this IEnumerable<Task> tasks,
            int maxConcurrency,
            int maxTimeout = -1,
            CancellationToken token = default(CancellationToken)
            )
        {
            // Validate the parameter before attempting to use it.
            Guard.Instance().ThrowIfNull(tasks, nameof(tasks));

            // Do the operation in a task.
            await Task.Run(() =>
            {
                WaitAll(
                    tasks,
                    maxConcurrency,
                    maxTimeout,
                    token
                    );
            }).ConfigureAwait(false);
        }

        #endregion
    }
}
