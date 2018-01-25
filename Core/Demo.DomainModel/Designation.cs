// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Designation.cs" company="zealous">
//
// </copyright>
// <summary>
//   The designation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Demo.DomainModel
{
    /// <summary>
    /// The designation.
    /// </summary>
    public class Designation
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }
    }
}