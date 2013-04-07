// <copyright file="Information.cs" company="Bitwise">
// Copyright (c) 2011 All Right Reserved
// </copyright>

namespace Bitwise.Common.Communication
{
    /// <summary>
    /// The purpose of this class is to version the service contract.
    /// </summary>
    public static class Information
    {
        /// <summary>
        /// The root namespace for sungard service.
        /// </summary>
        public const string NamespaceRoot = "http://www.bitwise.com/service/";

        /// <summary>
        /// The purpose of this class is to version service contract.
        /// </summary>
        public static class Namespace
        {
            /// <summary>
            /// The version constant for header.
            /// </summary>
            public const string Header = Information.NamespaceRoot + "Header/2010/11/";

            /// <summary>
            /// The version constant for exception.
            /// </summary>
            public const string Exception = Information.NamespaceRoot + "Exception/2010/11/";
        }
    }
}
