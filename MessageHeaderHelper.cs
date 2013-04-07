// <copyright file="MessageHeaderHelper.cs" company="Bitwise">
// Copyright (c) 2011 All Right Reserved
// </copyright>

namespace Bitwise.Common.Communication
{
	using System.ServiceModel;

    /// <summary>
    /// This is a helper class for handling the message header.
    /// </summary>
    /// <typeparam name="T">Template T</typeparam>
    public static class MessageHeaderHelper<T> where T : class
    {
        /// <summary>
        /// Adds the outgoing message header.
        /// </summary>
        /// <param name="messageHeader">The message header.</param>
        /// <param name="name">The name of the header.</param>
        /// <param name="ns">The namespace ns.</param>
        public static void AddOutgoingMessageHeader(T messageHeader, string name, string ns)
        {
            var header = new MessageHeader<T>(messageHeader);
            OperationContext.Current.OutgoingMessageHeaders.Add(header.GetUntypedHeader(name, ns));
        }

        /// <summary>
        /// Gets the add outgoing message header.
        /// </summary>
        /// <param name="name">The name of the header.</param>
        /// <param name="ns">The namespace ns.</param>
        /// <returns>Template T.</returns>
        public static T GetAddOutgoingMessageHeader(string name, string ns)
        {
            OperationContext context = OperationContext.Current;
            return context == null ? null : context.IncomingMessageHeaders.GetHeader<T>(name, ns);
        }
    }
}
