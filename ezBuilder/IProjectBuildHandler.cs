using System;

namespace ezBuilder
{
    /// <summary>
    /// Defines a means for a type that controls a project build.
    /// </summary>
    public interface IProjectBuildHandler
    {
        /// <summary>
        /// Returns whether or not there is currently a build in process.
        /// </summary>
        bool IsBuildInProgress { get; }

        /// <summary>
        /// Begins building the given solution item.
        /// See documentation for further information.
        /// </summary>
        /// <param name="buildItem">The <see cref="BuildItem"/> to begin building.</param>
        /// <remarks>This method is executed from within an own thread, and thus this method must not spawn another thread itself,
        /// but instead block until the <see cref="BuildItem"/> has fully built!</remarks>
        /// <returns>A <see cref="BuildResult"/> instance representing the result of the operation.</returns>
        BuildResult Build(BuildItem buildItem);
        /// <summary>
        /// Cancels the current build, if any is active.
        /// </summary>
        void CancelCurrentBuild();
    }
}
