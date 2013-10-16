using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Linq;

namespace ezBuilder.Shared.Project
{
    /// <summary>
    /// Represents the "BuildDefinition"-section of a bdproj-file.
    /// </summary>
    [DebuggerDisplay("Name = {Name}, Configurations = {Configurations.Count}")]
    public sealed class BuildDefinitionNode : Node<ProjectNode>
    {
        /// <summary>
        /// The description of the build definition.
        /// </summary>
        [ReadOnly(true)]
        public string Description { get; set; }

        /// <summary>
        /// The available configurations.
        /// </summary>
        [Browsable(false)]
        public List<BuildConfigurationNode> Configurations { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildDefinitionNode"/> class.
        /// </summary>
        public BuildDefinitionNode()
        {
            Configurations = new List<BuildConfigurationNode>();
        }
    }
}
