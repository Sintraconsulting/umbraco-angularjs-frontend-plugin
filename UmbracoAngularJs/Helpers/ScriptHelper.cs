// <copyright file="ScriptHelper.cs" company="Sintra">
// Copyright (c) Sintra. All rights reserved.
// </copyright>

namespace UmbracoAngularJs.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Helper to handle writing of JS scripts in page.
    /// </summary>
    public static class ScriptHelper
    {
        /// <summary>
        /// Converts the given string for JS use.
        /// </summary>
        /// <param name="text">The string to convert.</param>
        /// <param name="nullAsEmptyString">
        /// If set to <c>true</c> and <paramref name="text"/> is <c>null</c>, it will be converted in an empty string
        /// for JS, i.e. <c>''</c>. If <c>false</c> and <paramref name="text"/> is <c>null</c>, the resulting JS string
        /// will be <c>null</c>.
        /// </param>
        /// <returns>The string converted for JS use.</returns>
        public static string ConvertStringForJs(string text, bool nullAsEmptyString = true)
        {
            if (text == null)
            {
                return nullAsEmptyString ? "''" : "null";
            }

            return "'" + text.Replace("'", "\\'").Replace("\"", "\\\"") + "'";
        }

        /// <summary>
        /// Gets the a string array compatible with JS language.
        /// </summary>
        /// <param name="entries">The entries to insert in the array.</param>
        /// <returns>the JS array.</returns>
        public static string GetJsStringArray(List<string> entries)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            if (entries != null && entries.Count > 0)
            {
                sb.Append(string.Join(",", entries.Select(s => ConvertStringForJs(s))));
            }

            sb.Append("]");

            return sb.ToString();
        }

        /// <summary>
        /// Wraps the given JS code in an IIFE.
        /// </summary>
        /// <param name="jsCode">The JS code.</param>
        /// <returns>The resulting IIFE.</returns>
        public static string WrapJsCodeInIIFE(string jsCode)
        {
            return string.Format("(function(){{\n{0}\n}})();", jsCode);
        }
    }
}