using System.Diagnostics;

namespace ezBuilder.Shared.Project
{
    /// <summary>
    /// Represents the "BuildItem"-section of a bdproj-file.
    /// </summary>
    [DebuggerDisplay("Path = {Path}")]
    public sealed class BuildItemNode : Node<BuildConfigurationNode>
    {
        /// <summary>
        /// The path relative to the bdproj-file.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Returns the full path of this solution item.
        /// </summary>
        /// <returns></returns>
        public string GetFullPath()
        {
            string directory = System.IO.Path.GetDirectoryName(this.Parent.Parent.Parent.FileName);
            return System.IO.Path.Combine(directory, Path);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return Path;
            }
            return Name;
        }
    }
}
