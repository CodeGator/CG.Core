using CG.Validations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Utilities
{
    /// <summary>
    /// This class contains extensiom methods related to the <see cref="IRetry"/>
    /// type.
    /// </summary>
    public static partial class RetryExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method calls the <paramref name="action"/> func and automatically
        /// retries as long as the maximum number of retries hasn't yet been reached.
        /// </summary>
        /// <typeparam name="T">The type to return from the action.</typeparam>
        /// <param name="retry">The retry object to use for the operation.</param>
        /// <param name="action">The action to be performed.</param>
        /// <param name="maxRetries">The maximum number of times to retry the
        /// operation.</param>
        /// <param name="delayMS">The number of milliseconds to wait between
        /// retry attempts.</param>
        /// <returns>The return from the action.</returns>
        /// <exception cref="ArgumentNullException">This exception is thrown 
        /// if the <paramref name="action"/> parameter is NULL.</exception>
        public static T Execute<T>(
            this IRetry retry,
            Func<T> action,
            int maxRetries = 3,
            int delayMS = 0
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(retry, nameof(retry))
                .ThrowIfNull(action, nameof(action))
                .ThrowIfLessThanZero(maxRetries, nameof(maxRetries))
                .ThrowIfLessThanZero(delayMS, nameof(delayMS));

            // If no retries just run the action and leave.
            if (maxRetries == 0)
            {
                // Perform the operation.
                return action.Invoke();
            }

            // Loop and keep retrying.
            int retries = 0;
            while (true)
            {
                try
                {
                    // Perform the operation.
                    return action.Invoke();
                }
                catch (Exception)
                {
                    // If we retried enough then rethrow the exception.
                    if (retries >= maxRetries)
                    {
                        throw;
                    }

                    // Wait between retry attempts.
                    Thread.Sleep(delayMS);

                    // Keep track of the # of retries.
                    retries++;
                }
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method calls the <paramref name="action"/> func and automatically
        /// retries as long as the <paramref name="shouldRetry"/> returns true and
        /// the maximum number of retries hasn't yet been reached.
        /// </summary>
        /// <typeparam name="T">The type to return from the action.</typeparam>
        /// <param name="retry">The retry object to use for the operation.</param>
        /// <param name="action">The action to be performed.</param>
        /// <param name="shouldRetry">A func to determine if another retry to be 
        /// performed. Should return True to retry; False otherwise.</param>
        /// <returns>The return from the action.</returns>
        /// <param name="maxRetries">The maximum number of times to retry the
        /// operation.</param>
        /// <param name="delayMS">The number of milliseconds to wait between
        /// retry attempts.</param>
        /// <exception cref="ArgumentNullException">This exception is thrown if the 
        /// <paramref name="action"/> or <paramref name="shouldRetry"/> parameters 
        /// are NULL.</exception>
        public static T Execute<T>(
            this IRetry retry,
            Func<T> action,
            Func<Exception, bool> shouldRetry,
            int maxRetries = 3,
            int delayMS = 0
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(retry, nameof(retry))
                .ThrowIfNull(action, nameof(action))
                .ThrowIfNull(shouldRetry, nameof(shouldRetry))
                .ThrowIfLessThanZero(maxRetries, nameof(maxRetries))
                .ThrowIfLessThanZero(delayMS, nameof(delayMS));

            // If no retries just run the action and leave.
            if (maxRetries == 0)
            {
                // Perform the operation.
                return action.Invoke();
            }

            // Loop and keep retrying.
            int retries = 0;
            while (true)
            {
                try
                {
                    // Perform the operation.
                    return action.Invoke();
                }
                catch (Exception ex)
                {
                    // If we shouldn't retry then rethrow the exception.
                    if (!shouldRetry(ex))
                    {
                        throw;
                    }

                    // If we retried enough then rethrow the exception.
                    if (retries >= maxRetries)
                    {
                        throw;
                    }

                    // Wait between retry attempts.
                    Thread.Sleep(delayMS);

                    // Keep track of the # of retries.
                    retries++;
                }
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method calls the <paramref name="action"/> action and automatically
        /// retries as long as the the maximum number of retries hasn't yet been
        /// reached.
        /// </summary>
        /// <param name="retry">The retry object to use for the operation.</param>
        /// <param name="action">The action to be performed.</param>
        /// <param name="maxRetries">The maximum number of times to retry the
        /// operation.</param>
        /// <param name="delayMS">The number of milliseconds to wait between
        /// retry attempts.</param>
        /// <exception cref="ArgumentNullException">This exception is thrown if 
        /// the <paramref name="action"/> parameter is NULL.</exception>
        public static void Execute(
            this IRetry retry,
            Action action,
            int maxRetries = 3,
            int delayMS = 0
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(retry, nameof(retry))
                .ThrowIfNull(action, nameof(action))
                .ThrowIfLessThanZero(maxRetries, nameof(maxRetries))
                .ThrowIfLessThanZero(delayMS, nameof(delayMS));

            // If no retries just run the action and leave.
            if (maxRetries == 0)
            {
                // Perform the operation.
                action.Invoke();
                return;
            }

            // Loop and keep retrying.
            int retries = 0;
            while (true)
            {
                try
                {
                    // Perform the operation.
                    action.Invoke();
                    break;
                }
                catch (Exception)
                {
                    // If we retried enough then rethrow the exception.
                    if (retries >= maxRetries)
                    {
                        throw;
                    }

                    // Wait between retry attempts.
                    Thread.Sleep(delayMS);

                    // Keep track of the # of retries.
                    retries++;
                }
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method calls the <paramref name="action"/> action and automatically
        /// retries as long as the <paramref name="shouldRetry"/> returns true and
        /// the maximum number of retries hasn't yet been reached.
        /// </summary>
        /// <param name="retry">The retry object to use for the operation.</param>
        /// <param name="action">The action to be performed.</param>
        /// <param name="shouldRetry">A func to determine if another retry to be 
        /// performed. Should return True to retry; False otherwise.</param>
        /// <param name="maxRetries">The maximum number of times to retry the
        /// operation.</param>
        /// <param name="delayMS">The number of milliseconds to wait between
        /// retry attempts.</param>
        /// <exception cref="ArgumentNullException">This exception is thrown if the <paramref name="action"/> 
        /// or <paramref name="shouldRetry"/> parameters are NULL.</exception>
        public static void Execute(
            this IRetry retry,
            Action action,
            Func<Exception, bool> shouldRetry,
            int maxRetries = 3,
            int delayMS = 0
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(retry, nameof(retry))
                .ThrowIfNull(action, nameof(action))
                .ThrowIfLessThanZero(maxRetries, nameof(maxRetries))
                .ThrowIfLessThanZero(delayMS, nameof(delayMS));

            // If no retries just run the action and leave.
            if (maxRetries == 0)
            {
                // Perform the operation.
                action.Invoke();
                return;
            }

            // Loop and keep retrying.
            int retries = 0;
            while (true)
            {
                try
                {
                    // Perform the operation.
                    action.Invoke();
                    break;
                }
                catch (Exception ex)
                {
                    // If we shouldn't retry then rethrow the exception.
                    if (!shouldRetry(ex))
                    {
                        throw;
                    }

                    // If we retried enough then rethrow the exception.
                    if (retries >= maxRetries)
                    {
                        throw;
                    }

                    // Wait between retry attempts.
                    Thread.Sleep(delayMS);

                    // Keep track of the # of retries.
                    retries++;
                }
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method calls the <paramref name="action"/> action and automatically
        /// retries as long as the the maximum number of retries hasn't yet been
        /// reached.
        /// </summary>
        /// <param name="retry">The retry object to use for the operation.</param>
        /// <param name="action">The action to be performed.</param>
        /// <param name="maxRetries">The maximum number of times to retry the
        /// operation.</param>
        /// <param name="delayMS">The number of milliseconds to wait between
        /// retry attempts.</param>
        /// <returns>A task to perform the operation.</returns>
        /// <exception cref="ArgumentNullException">This exception is thrown if 
        /// the <paramref name="action"/> parameter is NULL.</exception>
        public static async Task ExecuteAsync(
            this IRetry retry,
            Action action,
            int maxRetries = 3,
            int delayMS = 0
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(retry, nameof(retry))
                .ThrowIfNull(action, nameof(action))
                .ThrowIfLessThanZero(maxRetries, nameof(maxRetries))
                .ThrowIfLessThanZero(delayMS, nameof(delayMS));

            // If no retries just run the action and leave.
            if (maxRetries == 0)
            {
                // Perform the operation.
                await Task.Run(
                    action
                    ).ConfigureAwait(false);
                return;
            }

            // Loop and keep retrying.
            int retries = 0;
            while (true)
            {
                try
                {
                    // Perform the operation.
                    await Task.Run(
                        action
                        ).ConfigureAwait(false);
                    break;
                }
                catch (Exception)
                {
                    // If we retried enough then rethrow the exception.
                    if (retries >= maxRetries)
                    {
                        throw;
                    }

                    // Wait between retry attempts.
                    await Task.Delay(delayMS).ConfigureAwait(false);

                    // Keep track of the # of retries.
                    retries++;
                }
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method calls the <paramref name="action"/> action and automatically
        /// retries as long as the <paramref name="shouldRetry"/> returns true and
        /// the maximum number of retries hasn't yet been reached.
        /// </summary>
        /// <param name="retry">The retry object to use for the operation.</param>
        /// <param name="action">The action to be performed.</param>
        /// <param name="shouldRetry">A func to determine if another retry to be 
        /// performed. Should return True to retry; False otherwise.</param>
        /// <param name="maxRetries">The maximum number of times to retry the
        /// operation.</param>
        /// <param name="delayMS">The number of milliseconds to wait between
        /// retry attempts.</param>
        /// <returns>A task to perform the operation.</returns>
        /// <exception cref="ArgumentNullException">This exception is thrown if the 
        /// <paramref name="action"/> or <paramref name="shouldRetry"/> parameters 
        /// are NULL.</exception>
        public static async Task ExecuteAsync(
            this IRetry retry,
            Action action,
            Func<Exception, bool> shouldRetry,
            int maxRetries = 3,
            int delayMS = 0
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(retry, nameof(retry))
                .ThrowIfNull(action, nameof(action))
                .ThrowIfLessThanZero(maxRetries, nameof(maxRetries))
                .ThrowIfLessThanZero(delayMS, nameof(delayMS));

            // If no retries just run the action and leave.
            if (maxRetries == 0)
            {
                // Perform the operation.
                await Task.Run(
                    action
                    ).ConfigureAwait(false);
                return;
            }

            // Loop and keep retrying.
            int retries = 0;
            while (true)
            {
                try
                {
                    // Perform the operation.
                    await Task.Run(
                        action
                        ).ConfigureAwait(false);

                    break;
                }
                catch (Exception ex)
                {
                    // If we shouldn't retry then rethrow the exception.
                    if (!shouldRetry(ex))
                    {
                        throw;
                    }

                    // If we retried enough then rethrow the exception.
                    if (retries >= maxRetries)
                    {
                        throw;
                    }

                    // Wait between retry attempts.
                    await Task.Delay(delayMS).ConfigureAwait(false);

                    // Keep track of the # of retries.
                    retries++;
                }
            }
        }
        
        // *******************************************************************

        /// <summary>
        /// This method calls the <paramref name="action"/> func and automatically
        /// retries as long as the the maximum number of retries hasn't yet been reached.
        /// </summary>
        /// <typeparam name="T">The type to return from the action.</typeparam>
        /// <param name="retry">The retry object to use for the operation.</param>
        /// <param name="action">The action to be performed.</param>
        /// <param name="maxRetries">The maximum number of times to retry the
        /// operation.</param>
        /// <param name="delayMS">The number of milliseconds to wait between
        /// retry attempts.</param>
        /// <returns>A task to perform the operation.</returns>
        /// <exception cref="ArgumentNullException">This exception is thrown if the 
        /// <paramref name="action"/> parameter is NULL.</exception>
        public static async Task<T> ExecuteAsync<T>(
            this IRetry retry,
            Func<Task<T>> action,
            int maxRetries = 3,
            int delayMS = 0
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(retry, nameof(retry))
                .ThrowIfNull(action, nameof(action))
                .ThrowIfLessThanZero(maxRetries, nameof(maxRetries))
                .ThrowIfLessThanZero(delayMS, nameof(delayMS));

            // If no retries just run the action and leave.
            if (maxRetries == 0)
            {
                // Perform the operation.
                return await Task.Run(
                    action
                    ).ConfigureAwait(false);
            }

            // Loop and keep retrying.
            int retries = 0;
            while (true)
            {
                try
                {
                    // Perform the operation.
                    return await Task.Run(
                        action
                        ).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    // If we retried enough then rethrow the exception.
                    if (retries >= maxRetries)
                    {
                        throw;
                    }

                    // Wait between retry attempts.
                    await Task.Delay(delayMS).ConfigureAwait(false);

                    // Keep track of the # of retries.
                    retries++;
                }
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method calls the <paramref name="action"/> func and automatically
        /// retries as long as the <paramref name="shouldRetry"/> returns true and
        /// the maximum number of retries hasn't yet been reached.
        /// </summary>
        /// <typeparam name="T">The type to return from the action.</typeparam>
        /// <param name="retry">The retry object to use for the operation.</param>
        /// <param name="action">The action to be performed.</param>
        /// <param name="shouldRetry">A func to determine if another retry to be 
        /// performed. Should return True to retry; False otherwise.</param>
        /// <param name="maxRetries">The maximum number of times to retry the
        /// operation.</param>
        /// <param name="delayMS">The number of milliseconds to wait between
        /// retry attempts.</param>
        /// <returns>A task to perform the operation.</returns>
        /// <exception cref="ArgumentNullException">This exception is thrown if the
        /// <paramref name="action"/> or <paramref name="shouldRetry"/> parameters 
        /// are NULL.</exception>
        public static async Task<T> ExecuteAsync<T>(
            this IRetry retry,
            Func<Task<T>> action,
            Func<Exception, bool> shouldRetry,
            int maxRetries = 3,
            int delayMS = 0
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(retry, nameof(retry))
                .ThrowIfNull(action, nameof(action))
                .ThrowIfLessThanZero(maxRetries, nameof(maxRetries))
                .ThrowIfLessThanZero(delayMS, nameof(delayMS));

            // If no retries just run the action and leave.
            if (maxRetries == 0)
            {
                // Perform the operation.
                return await Task.Run(
                    action
                    ).ConfigureAwait(false);
            }

            // Loop and keep retrying.
            int retries = 0;
            while (true)
            {
                try
                {
                    // Perform the operation.
                    return await Task.Run(
                        action
                        ).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    // If we shouldn't retry then rethrow the exception.
                    if (!shouldRetry(ex))
                    {
                        throw;
                    }

                    // If we retried enough then rethrow the exception.
                    if (retries >= maxRetries)
                    {
                        throw;
                    }

                    // Wait between retry attempts.
                    await Task.Delay(delayMS).ConfigureAwait(false);

                    // Keep track of the # of retries.
                    retries++;
                }
            }
        }

        #endregion
    }
}
