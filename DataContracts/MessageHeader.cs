// <copyright file="MessageHeader.cs" company="Bitwise">
// Copyright (c) 2011 All Right Reserved
// </copyright>

namespace Bitwise.Common.Communication
{
    using System.Runtime.Serialization;
    using System.ServiceModel;

    /// <summary>
    /// This class defines Message Header data contract
    /// </summary>
    [DataContract(Namespace = Information.Namespace.Header)]
    public class MessageHeader
    {
        /// <summary>
        /// Gets or sets the type of the header.
        /// </summary>
        /// <value>The type of the header.</value>
        [DataMember]
        public MessageCredentialType HeaderType { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [DataMember]
        public string Content { get; set; }
    }
}
