// <copyright file="OperationException.cs" company="Bitwise">
// Copyright (c) 2011 All Right Reserved
// </copyright>

namespace Bitwise.Common.Communication
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// This class defines the operation exception.
    /// </summary>
    [DataContract(Namespace = Information.Namespace.Exception)]
    public class OperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationException"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public OperationException(Exception exception)
        {
            this.Exception = exception;
        }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception { get; private set; }
    }
}
