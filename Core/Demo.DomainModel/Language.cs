// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Language.cs" company="zealous">
//
// </copyright>
// <summary>
//   The language.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.DomainModel
{
    /// <summary>
    /// The language.
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Gets or sets the language id.
        /// </summary>
        [Key]
        public Guid LanguageId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get;
            set;
        }
    }
}