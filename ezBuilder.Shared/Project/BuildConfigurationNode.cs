using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace ezBuilder.Shared.Project
{
    /// <summary>
    /// Represents the "BuildConfiguration"-section of a bdproj-file.
    /// </summary>
    [DebuggerDisplay("Name = {Name}, Solution Items = {BuildItems.Count}")]
    public sealed class BuildConfigurationNode : Node<BuildDefinitionNode>
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        [ReadOnly(true)]
        public string Configuration { get; set; }
        /// <summary>
        /// The verbosity of the build output.
        /// </summary>
        [Description("The verbosity of the build output.")]
        public string Verbosity { get; set; }
        /// <summary>
        /// Whether or not to perform a rebuild before building.
        /// </summary>
        [Description("Whether or not to perform a rebuild before building")]
        public bool PerformReBuild { get; set; }

        /// <summary>
        /// The <see cref="BuildItemNode"/> list.
        /// </summary>
        [Browsable(false)]
        public List<BuildItemNode> BuildItems { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildConfigurationNode"/> class.
        /// </summary>
        public BuildConfigurationNode()
        {
            BuildItems = new List<BuildItemNode>();
            Verbosity = "minimal";
        }
    }
}
