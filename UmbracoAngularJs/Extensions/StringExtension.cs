// <copyright file="StringExtensions.cs" company="Sintra">
// Copyright (c) Sintra. All rights reserved.
// </copyright>

namespace UmbracoAngularJs.Extensions
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using UmbracoUtils.Extensions;

    /// <summary>
    /// Extension methods for <see cref="string"/>.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Convert a string represeting an AngularJS filename to the corresponding JS variable name.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The corresponding JS variable name.</returns>
        public static string ToNgJsVarName(this string input)
        {
            if (input == null)
            {
                return null;
            }

            if (input == string.Empty)
            {
                return string.Empty;
            }

            return string.Join(
                string.Empty,
                input.Split(new char[] { '.', '-' }).Select(s => s.FirstCharToUpper()).ToList());
        }

        /// <summary>
        /// Convert a string represeting an AngularJS variable name to the corresponding filename.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The corresponding filename.</returns>
        public static string ToNgJsFileName(this string input)
        {
            if (input == null)
            {
                return null;
            }

            if (input == string.Empty)
            {
                return string.Empty;
            }

            return string.Join("-", Regex.Split(input, @"(?<!^)(?=[A-Z])").Select(s => s.ToLower()));
        }
    }
}