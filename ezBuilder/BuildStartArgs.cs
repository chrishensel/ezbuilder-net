using System.Collections.Generic;

namespace ezBuilder
{
    /// <summary>
    /// Represents the arguments that starts the build process.
    /// </summary>
    public class BuildStartArgs
    {
        /// <summary>
        /// Gets/sets a list with names that contain the items to be built.
        /// Set to <c>null</c> to build all <see cref="BuildItem"/>s.
        /// </summary>
        public List<string> ItemsToBuild { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildStartArgs"/> class.
        /// </summary>
        public BuildStartArgs()
        {
            ItemsToBuild = new List<string>();
        }
    }
}
