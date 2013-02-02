using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace ezBuilder
{
    /// <summary>
    /// Contains exports.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Returns the working directory of this assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>The working directory of this assembly.</returns>
        public static string GetWorkingDirectory(this Assembly assembly)
        {
            return Path.GetDirectoryName(assembly.Location);
        }
                
        /// <summary>
        /// Tries to get the attribute value of an attribute from the requested XElement.
        /// </summary>
        /// <param name="element">The XElement to get the attribute value from.</param>
        /// <param name="attributeName">The name of the attribute to get its value.</param>
        /// <param name="defaultValue">The default value to return, if the attribute did not exist.</param>
        /// <returns></returns>
        public static string TryGetAttributeValue(this XElement element, string attributeName, string defaultValue)
        {
            XAttribute attribute = element.Attribute(attributeName);
            if (attribute != null)
            {
                return attribute.Value;
            }
            return defaultValue;
        }

    }
}