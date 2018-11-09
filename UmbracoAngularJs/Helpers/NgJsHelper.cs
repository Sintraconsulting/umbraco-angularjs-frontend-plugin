// <copyright file="NgJsHelper.cs" company="Sintra">
// Copyright (c) Sintra. All rights reserved.
// </copyright>

namespace UmbracoAngularJs.Helpers
{
    using System.IO;
    using System.Web;
    using System.Web.UI;

    /// <summary>
    /// Helper to retrieve AngularJs file paths.
    /// </summary>
    public class NgJsHelper
    {
        /// <summary>
        /// Gets the file path of an AngularJS file, given its directoty and its filename.
        /// </summary>
        /// <param name="dirPath">The dir path.</param>
        /// <param name="fileName">Name of the file (without extension).</param>
        /// <param name="extension">The file extension, with or without leading dot. Default is <c>".js"</c>.</param>
        /// <returns>The path of the AngularJS related file.</returns>
        public static string GetNgJsFilePath(string dirPath, string fileName, string extension = ".js")
        {
            var actualExt = extension.StartsWith(".") ? extension : "." + extension;
            return Path.Combine(dirPath, fileName + actualExt);
        }

        /// <summary>
        /// Maps the provided virtual path in the actual path on server.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <param name="extIfNotPresent">The extension to append in the filename, if not already present.</param>
        /// <returns>The mapped path on server.</returns>
        public static string MapPath(string virtualPath, string extIfNotPresent = ".js")
        {
            var mappedPath = HttpContext.Current.Server.MapPath(virtualPath);
            return Path.HasExtension(mappedPath) ? mappedPath : mappedPath + ".js";
        }

        /// <summary>
        /// Gets the name of the currently requested page.
        /// </summary>
        /// <param name="page">The currently requested page.</param>
        /// <returns>The name of the currently requested page.</returns>
        public static string GetPageName(Page page)
        {
            return Path.GetFileNameWithoutExtension(page.Request.Path);
        }
    }
}