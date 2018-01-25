// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DemoResponse.cs" company="zealous">
//    MIT License
// </copyright>
// <summary>
//   The ag live response.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Demo.Shared.Helpers
{
    /// <summary>
    /// The ag live response.
    /// </summary>
    /// <typeparam name="T"> The generic response parameter  </typeparam>
    public class DemoResponse<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DemoResponse{T}"/> class.
        /// </summary>
        public DemoResponse()
        {
            Exceptions = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        public T Response { get; set; }

        /// <summary>
        /// Gets or sets the exceptions.
        /// </summary>
        public Dictionary<string, string> Exceptions { get; set; }
    }
}