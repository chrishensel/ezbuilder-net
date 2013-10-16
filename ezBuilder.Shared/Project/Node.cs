using System.Collections.Specialized;
using System.ComponentModel;

namespace ezBuilder.Shared.Project
{
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
