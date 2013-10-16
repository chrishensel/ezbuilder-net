using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Linq;
using ezBuilder.Shared.Core;

namespace ezBuilder.Shared.Project
{
    /// <summary>
    /// Represents the bdproj-file.
    /// </summary>
    [DebuggerDisplay("FileName = {FileName}, Definitions = {Definitions.Count}")]
    public sealed class ProjectNode : Node<object>
    {
        /// <summary>
        /// The file name of the bdproj-file.
        /// </summary>
        [ReadOnly(true)]
        public string FileName { get; set; }

        /// <summary>
        /// The type name (ignored here).
        /// </summary>
        [ReadOnly(true)]
        public string Type { get; set; }
        /// <summary>
        /// The definitions.
        /// </summary>
        [Browsable(false)]
        public List<BuildDefinitionNode> Definitions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectNode"/> class.
        /// </summary>
        public ProjectNode()
        {
            Definitions = new List<BuildDefinitionNode>();
        }

        /// <summary>
        /// Parses the bdproj from the given file.
        /// </summary>
        /// <param name="fileName"></param>
        public void Parse(string fileName)
        {
            this.FileName = fileName;

            XDocument doc = XDocument.Load(fileName);
            this.Type = doc.Root.TryGetAttributeValue("Type", null);
            this.Name = doc.Root.TryGetAttributeValue("Name", null);
            this.Data = GetData(doc.Root);

            // parse BuildDefinition elements
            foreach (XElement definitionE in doc.Root.Elements("BuildDefinition"))
            {
                BuildDefinitionNode definition = new BuildDefinitionNode();
                definition.Parent = this;
                definition.Name = definitionE.TryGetAttributeValue("Name", null);
                definition.Description = definitionE.TryGetAttributeValue("Description", null);
                definition.Data = GetData(definitionE);

                // parse BuildConfiguration elements
                foreach (XElement configurationE in definitionE.Elements("BuildConfiguration"))
                {
                    BuildConfigurationNode configuration = new BuildConfigurationNode();
                    configuration.Parent = definition;
                    configuration.Name = configurationE.TryGetAttributeValue("Name", null);
                    configuration.Configuration = configurationE.TryGetAttributeValue("Configuration", null);
                    configuration.Verbosity = configurationE.TryGetAttributeValue("Verbosity", "minimal");
                    configuration.Data = GetData(configurationE);

                    // parse BuildItem elements
                    foreach (XElement solutionItemE in configurationE.Elements("BuildItem"))
                    {
                        BuildItemNode solutionItem = new BuildItemNode();
                        solutionItem.Name = solutionItemE.TryGetAttributeValue("Name", null);
                        solutionItem.Path = solutionItemE.TryGetAttributeValue("Path", null);
                        solutionItem.Parent = configuration;
                        solutionItem.Data = GetData(solutionItemE);

                        // If the name does not exist --> use path
                        if (solutionItem.Name == null)
                        {
                            solutionItem.Name = solutionItem.Path;
                        }

                        configuration.BuildItems.Add(solutionItem);
                    }

                    definition.Configurations.Add(configuration);
                }

                this.Definitions.Add(definition);
            }
        }

        private StringDictionary GetData(XElement element)
        {
            StringDictionary data = new StringDictionary();
            foreach (XAttribute attribute in element.Attributes())
            {
                data[attribute.Name.LocalName] = attribute.Value;
            }

            return data;
        }
    }
}
