using System;

namespace ezBuilder.Shared.Build
{
    /// <summary>
    /// Specifies how a build process has completed.
    /// </summary>
    public enum BuildCompletionType
    {
        /// <summary>
        /// Enumeration default value.
        /// </summary>
        None = 0,
        /// <summary>
        /// The build completed successfully.
        /// </summary>
        RanToCompletion,
        /// <summary>
        /// The build was canceled due to errors.
        /// </summary>
        CanceledDueToErrors,
        /// <summary>
        /// The user has manually aborted the build process.
        /// </summary>
        UserAborted,
    }
}
