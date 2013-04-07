// <copyright file="TaskHelper.cs" company="Bitwise">
// Copyright (c) 2011 All Right Reserved
// </copyright>

namespace Bitwise.Common.Communication.Utilities
{
	using System;
	using System.Threading.Tasks;

    /// <summary>
    /// This class is a helper class for handling task.
    /// </summary>
	public static class TaskHelper
	{
        /// <summary>
        /// Creates the async result.
        /// </summary>
        /// <typeparam name="T">The template T.</typeparam>
        /// <param name="task">The task that needs to be executed.</param>
        /// <param name="callback">The callback when the task is finished.</param>
        /// <param name="state">The state of the object.</param>
        /// <returns>Asynchronous result.</returns>
		public static IAsyncResult CreateAsyncResult<T>(this Task<T> task, AsyncCallback callback, object state)
		{
			// create result object that can hold the asynchronously-computed value
			var result = new TaskCompletionSource<T>(state);
            if (callback != null)
                result.Task.ContinueWith(t => callback(t));

			// set the result (or failure) when the value is known
			task.ContinueWith(t =>
			{
				if (t.IsFaulted)
				{
				    if (t.Exception != null) result.SetException(t.Exception);
				}
				else if (t.IsCanceled)
					result.SetCanceled();
				else
					result.SetResult(t.Result);
			});

			// the result's task functions as the IAsyncResult APM return value
			return result.Task;
		}

        /// <summary>
        /// Interprets the result.
        /// </summary>
        /// <typeparam name="T">Expected type</typeparam>
        /// <param name="asyncResult">The async result.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The typeparam "T"</returns>
        public static T InterpretResult<T>(IAsyncResult asyncResult, Func<T> defaultValue = null)
        {
            if (defaultValue == null)
                defaultValue = () => default(T);
            var completedAsyncResult = asyncResult as Task<T>;
            if (completedAsyncResult != null)
            {
                return !completedAsyncResult.IsFaulted
                           ? completedAsyncResult.Result
                           : defaultValue.Invoke();
            }

            return defaultValue.Invoke();
        }
	}
}
