using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace ezBuilder
{

    /// <summary>
    /// Represents the build log for one build run.
    /// </summary>
    public class BuildLog
    {
        public string AffectedUser { get; set; }
        public DateTime BuildStarted { get; set; }
        public DateTime BuildCompleted { get; set; }
        public BuildCompletionType CompletionType { get; set; }
        public List<BuildLogItem> Items { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildLog"/> class.
        /// </summary>
        public BuildLog()
        {
            Items = new List<BuildLogItem>();
        }

        #region Methods

        /// <summary>
        /// Saves the current build log under the specified path.
        /// </summary>
        /// <param name="file">The file.</param>
        public void Save(string file)
        {
            string dir = Path.GetDirectoryName(file);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            XDocument doc = new XDocument();
            XElement rootE = new XElement("BuildLog");
            rootE.Add(new XAttribute("Started", BuildStarted));
            rootE.Add(new XAttribute("Completed", BuildCompleted));
            rootE.Add(new XAttribute("CompletionType", CompletionType));
            doc.Add(rootE);

            foreach (var item in Items)
            {
                XElement bliE = new XElement("Item");
                bliE.Add(new XAttribute("ItemName", item.ItemName));
                bliE.Add(new XAttribute("Started", item.BuildStarted));
                bliE.Add(new XAttribute("Completed", item.BuildCompleted));

                XElement infosE = new XElement("Infos");
                foreach (var info in item.Infos)
                {
                    infosE.Add(new XElement("Info", info));
                }
                foreach (var warning in item.Warnings)
                {
                    infosE.Add(new XElement("Warning", warning));
                }
                foreach (var error in item.Errors)
                {
                    infosE.Add(new XElement("Error", error));
                }
                bliE.Add(infosE);

                rootE.Add(bliE);
            }

            doc.Save(file);
        }

        #endregion
    }

    public class BuildLogItem
    {
        public DateTime BuildStarted { get; set; }
        public DateTime BuildCompleted { get; set; }
        /// <summary>
        /// Gets/sets the name of the item that has been built.
        /// </summary>
        public string ItemName { get; set; }
        public List<string> Infos { get; set; }
        /// <summary>
        /// Gets/sets the list of warnings.
        /// </summary>
        public List<string> Warnings { get; set; }
        /// <summary>
        /// Gets/sets the list of errors.
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildLogItem"/> class.
        /// </summary>
        public BuildLogItem()
        {
            Infos = new List<string>();
            Warnings = new List<string>();
            Errors = new List<string>();
        }
    }
}
