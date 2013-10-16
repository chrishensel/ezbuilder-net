using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using ezBuilder.Shared.Build;
using ezBuilder.Shared.Core;
using ezBuilder.Shared.Extensibility;
using ezBuilder.Shared.Project;

namespace ezBuilder
{
    /// <summary>
    /// Wraps the build process.
    /// </summary>
    class ProjectBuildProcess
    {
        #region Events

        /// <summary>
        /// Raised when a build has completed.
        /// </summary>
        public event BuildCompleted BuildCompleted;
        /// <summary>
        /// Raised when the build is finished.
        /// </summary>
        public event BuildFinished BuildFinished;

        private void OnBuildCompleted(BuildResult result)
        {
            var copy = BuildCompleted;
            if (copy != null)
            {
                copy(result);
            }
        }

        private void OnBuildFinished(BuildCompletionType completionType)
        {
            var copy = BuildFinished;
            if (copy != null)
            {
                copy(completionType);
            }
        }

        #endregion

        #region Fields

        private object LOCK = new object();
        private Thread _buildThread;
        private IProjectBuildHandler _projectBuildHandler;

        private Stopwatch _stopwatch = new Stopwatch();
        private BuildLog _buildLog;

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public DateTime BuildStarted
        {
            get
            {
                if (_buildLog == null)
                {
                    return DateTime.MinValue;
                }
                return _buildLog.BuildStarted;
            }
        }
        /// <summary>
        /// Gets whether or not the process is running.
        /// </summary>
        public bool IsRunning
        {
            get { return _buildThread != null && (_projectBuildHandler != null && _projectBuildHandler.IsBuildInProgress); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectBuildProcess"/> class.
        /// </summary>
        public ProjectBuildProcess()
        {
            _projectBuildHandler = new MSBuildProjectBuildHandler();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Starts a new build. Does nothing if a build is already in process.
        /// </summary>
        /// <param name="configuration">The <see cref="BuildConfiguration"/> representing what configuration to build.</param>
        /// <param name="args">The arguments to use for building.</param>
        public void Start(BuildConfigurationNode configuration, BuildStartArgs args)
        {
            lock (LOCK)
            {
                if (configuration == null)
                {
                    throw new ArgumentNullException("configuration");
                }
                if (args == null)
                {
                    throw new ArgumentNullException("args");
                }

                if (IsRunning)
                {
                    return;
                }

                // Set up build log
                _buildLog = new BuildLog();
                _buildLog.AffectedUser = Environment.UserDomainName;
                _buildLog.BuildStarted = DateTime.UtcNow;

                _buildThread = new Thread(BuildThread);
                _buildThread.Start(new object[2] { configuration, args });
            }
        }

        /// <summary>
        /// Stops the build. Does nothing if the build is already stopped.
        /// </summary>
        public void Stop()
        {
            lock (LOCK)
            {
                if (!IsRunning)
                {
                    return;
                }

                _stopwatch.Reset();

                // kill process (ignore any possible exceptions here)
                try
                {
                    if (_projectBuildHandler != null && _projectBuildHandler.IsBuildInProgress)
                    {
                        _projectBuildHandler.CancelCurrentBuild();
                    }
                }
                catch (Exception) { }

                if (_buildThread != null)
                {
                    _buildThread.Abort();
                    _buildThread.Join();
                    _buildThread = null;
                }

                // Update build log
                _buildLog.BuildCompleted = DateTime.UtcNow;
                _buildLog.CompletionType = BuildCompletionType.UserAborted;

                SaveBuildLog();
            }
        }

        /// <summary>
        /// Saves the build log.
        /// </summary>
        public void SaveBuildLog()
        {
            if (_buildLog == null)
            {
                throw new InvalidOperationException();
            }

            // Store as Xml
            string blfile = Path.Combine(Assembly.GetExecutingAssembly().GetWorkingDirectory(), "BuildLogs", string.Format("BuildLog_{0}.xml", _buildLog.BuildCompleted.ToString("yyyyMMdd_HHmmss")));
            _buildLog.Save(blfile);
        }

        /// <summary>
        /// Represents the build thread, in which the build is running.
        /// </summary>
        /// <param name="parameter">An object-array containing the parameters for this method.</param>
        private void BuildThread(object parameter)
        {
            object[] parameters = (object[])parameter;

            BuildConfigurationNode configuration = (BuildConfigurationNode)parameters[0];
            BuildStartArgs args = (BuildStartArgs)parameters[1];

            // Process and build each item...
            foreach (BuildItemNode buildItem in configuration.BuildItems)
            {
                BuildResult result = new BuildResult();

                BuildLogItem bli = new BuildLogItem();
                bli.ItemName = buildItem.Name;
                bli.BuildStarted = DateTime.UtcNow;

                // shall we build this item?
                if (args.ItemsToBuild == null || args.ItemsToBuild.Contains(buildItem.Name))
                {
                    try
                    {
                        _stopwatch.Restart();

                        // Build the damn thing!
                        result = _projectBuildHandler.Build(buildItem);

                        bli.BuildCompleted = DateTime.UtcNow;
                        _stopwatch.Stop();

                        // add the result to the items
                        result.Items.Add(new BuildInfoItem()
                        {
                            Level = BuildInfoItemLevel.BuildEvent,
                            Text = string.Format("Build of '{0}' finished in '{1}' milliseconds.", buildItem.ToString(), _stopwatch.ElapsedMilliseconds),
                        });

                        OnBuildCompleted(result);
                    }
                    catch (Exception ex)
                    {
                        bli.BuildCompleted = DateTime.UtcNow;
                        string message = string.Format("An exception of type {0} occurred while attempting to build solution item '{1}' with project build handler {2}. The error message was: {3}", ex.GetType().Name, buildItem.Path, _projectBuildHandler.GetType().Name, ex.Message);

                        bli.Errors.Add(message);
                        Debug.WriteLine(message);
                    }
                }
                else
                {
                    bli.BuildCompleted = DateTime.UtcNow;

                    result.AddItem(BuildInfoItemLevel.BuildEvent, "Skipped building '{0}'.", buildItem.Name);
                    OnBuildCompleted(result);
                }

                // Before going on, fill the build log...
                if (result != null && result.Items.Count > 0)
                {
                    foreach (var item in result.Items)
                    {
                        switch (item.Level)
                        {
                            case BuildInfoItemLevel.BuildEvent:
                            case BuildInfoItemLevel.Trace:
                            case BuildInfoItemLevel.Info:
                            default:
                                bli.Infos.Add(item.Text);
                                break;
                            case BuildInfoItemLevel.Warning:
                                bli.Warnings.Add(item.Text);
                                break;
                            case BuildInfoItemLevel.Error:
                                bli.Errors.Add(item.Text);
                                break;
                        }
                    }
                }

                _buildLog.Items.Add(bli);
            }

            _buildLog.BuildCompleted = DateTime.UtcNow;
            _buildLog.CompletionType = BuildCompletionType.RanToCompletion;
            OnBuildFinished(BuildCompletionType.RanToCompletion);
        }

        #endregion
    }
}
