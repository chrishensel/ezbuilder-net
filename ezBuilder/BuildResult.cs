using System.Collections.Generic;
using System.Globalization;

namespace ezBuilder
{
    /// <summary>
    /// Represents the result of a single build operation of a <see cref="BuildItem"/>.
    /// </summary>
    public class BuildResult
    {
        /// <summary>
        /// Gets/sets the info items that represent what occurred during build.
        /// </summary>
        public List<BuildInfoItem> Items { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildResult"/> class.
        /// </summary>
        public BuildResult()
        {
            Items = new List<BuildInfoItem>();
        }

        /// <summary>
        /// Convenience method to add a new <see cref="BuildInfoItem"/>.
        /// </summary>
        /// <param name="level">The level of the <see cref="BuildInfoItem"/>.</param>
        /// <param name="format">The format string of the <see cref="BuildInfoItem.Text"/> to use.</param>
        /// <param name="args">Optional arguments to mix into the <see cref="BuildInfoItem.Text"/> using the <paramref name="format"/> string.</param>
        public void AddItem(BuildInfoItemLevel level, string format, params object[] args)
        {
            Items.Add(new BuildInfoItem()
            {
                Level = level,
                Text = string.Format(CultureInfo.InvariantCulture, format, args),
            });
        }

        /// <summary>
        /// Returns a <see cref="BuildResult"/> instance representing success.
        /// </summary>
        public static BuildResult Success
        {
            get { return new BuildResult(); }
        }
    }

    /// <summary>
    /// Represents one piece of info that occurred during a build.
    /// </summary>
    public class BuildInfoItem
    {
        /// <summary>
        /// Gets/sets the level of this item.
        /// </summary>
        public BuildInfoItemLevel Level { get; set; }
        /// <summary>
        /// Gets/sets the text describing this item.
        /// </summary>
        public string Text { get; set; }
    }

    /// <summary>
    /// Specifies the level for <see cref="BuildInfoItem"/>.
    /// </summary>
    public enum BuildInfoItemLevel
    {
        /// <summary>
        /// A build event that usually occurs by the build system and can mostly be ignored.
        /// </summary>
        BuildEvent,
        /// <summary>
        /// A trace item. Usually this contains unimportant data.
        /// </summary>
        Trace,
        /// <summary>
        /// An info.
        /// </summary>
        Info,
        /// <summary>
        /// A warning.
        /// </summary>
        Warning,
        /// <summary>
        /// An error.
        /// </summary>
        Error,
    }

    /// <summary>
    /// Raised when a build has completed.
    /// </summary>
    /// <param name="result"></param>
    public delegate void BuildCompleted(BuildResult result);
    /// <summary>
    /// Raised when the build process has completed.
    /// </summary>
    /// <param name="completionType"></param>
    public delegate void BuildFinished(BuildCompletionType completionType);
}
