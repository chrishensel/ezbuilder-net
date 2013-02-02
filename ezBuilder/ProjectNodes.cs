using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Linq;

namespace ezBuilder
{
    /// <summary>
    /// Represents the bdproj-file.
    /// </summary>
    [DebuggerDisplay("FileName = {FileName}, Definitions = {Definitions.Count}")]
    public sealed class Project : Node<object>
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
        public List<BuildDefinition> Definitions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        public Project()
        {
            Definitions = new List<BuildDefinition>();
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
                BuildDefinition definition = new BuildDefinition();
                definition.Parent = this;
                definition.Name = definitionE.TryGetAttributeValue("Name", null);
                definition.Description = definitionE.TryGetAttributeValue("Description", null);
                definition.Data = GetData(definitionE);

                // parse BuildConfiguration elements
                foreach (XElement configurationE in definitionE.Elements("BuildConfiguration"))
                {
                    BuildConfiguration configuration = new BuildConfiguration();
                    configuration.Parent = definition;
                    configuration.Name = configurationE.TryGetAttributeValue("Name", null);
                    configuration.Configuration = configurationE.TryGetAttributeValue("Configuration", null);
                    configuration.Verbosity = configurationE.TryGetAttributeValue("Verbosity", "minimal");
                    configuration.Data = GetData(configurationE);

                    // parse BuildItem elements
                    foreach (XElement solutionItemE in configurationE.Elements("BuildItem"))
                    {
                        BuildItem solutionItem = new BuildItem();
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

    /// <summary>
    /// Represents the "BuildDefinition"-section of a bdproj-file.
    /// </summary>
    [DebuggerDisplay("Name = {Name}, Configurations = {Configurations.Count}")]
    public sealed class BuildDefinition : Node<Project>
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
        public List<BuildConfiguration> Configurations { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildDefinition"/> class.
        /// </summary>
        public BuildDefinition()
        {
            Configurations = new List<BuildConfiguration>();
        }
    }

    /// <summary>
    /// Represents the "BuildConfiguration"-section of a bdproj-file.
    /// </summary>
    [DebuggerDisplay("Name = {Name}, Solution Items = {BuildItems.Count}")]
    public sealed class BuildConfiguration : Node<BuildDefinition>
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
        /// The <see cref="BuildItem"/> list.
        /// </summary>
        [Browsable(false)]
        public List<BuildItem> BuildItems { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildConfiguration"/> class.
        /// </summary>
        public BuildConfiguration()
        {
            BuildItems = new List<BuildItem>();
            Verbosity = "minimal";
        }
    }

    /// <summary>
    /// Represents the "BuildItem"-section of a bdproj-file.
    /// </summary>
    [DebuggerDisplay("Path = {Path}")]
    public sealed class BuildItem : Node<BuildConfiguration>
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

    /// <summary>
    /// Represents the base class for any node of the bdproj file.
    /// </summary>
    /// <typeparam name="TParent">The type of the parent node.</typeparam>
    public abstract class Node<TParent> where TParent : class
    {
        /// <summary>
        /// Gets/sets the parent of this node, if any.
        /// </summary>
        [Browsable(false)]
        public TParent Parent { get; set; }

        /// <summary>
        /// The name of this node.
        /// </summary>
        [ReadOnly(true)]
        [Description("The name of this node.")]
        public string Name { get; set; }
        /// <summary>
        /// Any custom data that was present in the build configuration that may be used for building.
        /// </summary>
        [Browsable(false)]
        public StringDictionary Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Node&lt;TParent&gt;"/> class.
        /// </summary>
        public Node()
        {
            Data = new StringDictionary();
        }
    }
}
