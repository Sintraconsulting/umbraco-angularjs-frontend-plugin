// <copyright file="StringExtensions.cs" company="Sintra">
// Copyright (c) Sintra. All rights reserved.
// </copyright>

namespace UmbracoUtils.Extensions
{
    using System.Linq;

    /// <summary>
    /// Extension methods for <see cref="string"/>.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Make the first char in a string uppercase.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The string with the first char in uppercase.</returns>
        /// <exception cref="System.ArgumentNullException">input</exception>
        /// <exception cref="System.ArgumentException">input - input</exception>
        public static string FirstCharToUpper(this string input)
        {
            if (input == null)
            {
                return null;
            }

            if (input == string.Empty)
            {
                return string.Empty;
            }

            return input.First().ToString().ToUpperInvariant() + input.Substring(1);
        }

        /// <summary>
        /// Make the first char in a string lowercase.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The string with the first char in lowercase.</returns>
        /// <exception cref="System.ArgumentNullException">input</exception>
        /// <exception cref="System.ArgumentException">input - input</exception>
        public static string FirstCharToLower(this string input)
        {
            if (input == null)
            {
                return null;
            }

            if (input == string.Empty)
            {
                return string.Empty;
            }

            return input.First().ToString().ToLowerInvariant() + input.Substring(1);
        }
    }
}