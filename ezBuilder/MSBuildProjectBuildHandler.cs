using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using ezBuilder.Shared.Build;
using ezBuilder.Shared.Extensibility;
using ezBuilder.Shared.Project;

namespace ezBuilder
{
    class MSBuildProjectBuildHandler : IProjectBuildHandler
    {
        #region Fields

        private bool _isBuildInProgress;
        private Process _currentBuildProcess;
        private string _msBuildPath;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MSBuildProjectBuildHandler"/> class.
        /// </summary>
        public MSBuildProjectBuildHandler()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.GetFolderPath(Environment.SpecialFolder.Windows)).Append(@"\");
            sb.Append(@"Microsoft.NET\Framework");
            if (Environment.Is64BitOperatingSystem)
            {
                // if this is a 64-bit process, use the dedicated build tool (otherwise the 32-bit one is used)
                sb.Append("64");
            }
            sb.Append(@"\v4.0.30319\");
            sb.Append("msbuild.exe");

            _msBuildPath = sb.ToString();

            // if the path couldn't be found, throw exception
            if (!File.Exists(_msBuildPath))
            {
                throw new InvalidOperationException(string.Format("The 'msbuild.exe' was not found at path '{0}'. Building with msbuild is not possible!", _msBuildPath));
            }
        }

        #endregion

        #region Methods

        private string GetConfiguration(string suggestedConfiguration, BuildItemNode buildItem)
        {
            if (buildItem.Data.ContainsKey("Configuration"))
            {
                return buildItem.Data["Configuration"];
            }
            return suggestedConfiguration;
        }

        #endregion

        #region IProjectBuildHandler Members

        bool IProjectBuildHandler.IsBuildInProgress
        {
            get { return _isBuildInProgress; }
        }

        BuildResult IProjectBuildHandler.Build(BuildItemNode buildItem)
        {
            _isBuildInProgress = true;

            ProjectNode project = buildItem.Parent.Parent.Parent;
            BuildConfigurationNode configuration = buildItem.Parent;

            string solutionFilePath = buildItem.GetFullPath();
            string workingDirectory = Path.GetDirectoryName(project.FileName);
            BuildResult result = new BuildResult();

            _currentBuildProcess = new Process();
            _currentBuildProcess.StartInfo.FileName = _msBuildPath;

            if (configuration.PerformReBuild)
            {
                PerformClean(buildItem);
            }
            _currentBuildProcess.StartInfo.Arguments = string.Format("\"{0}\" /p:Configuration={1} /verbosity:{2}", solutionFilePath, GetConfiguration(configuration.Configuration, buildItem), configuration.Verbosity);

            _currentBuildProcess.StartInfo.RedirectStandardOutput = true;
            _currentBuildProcess.StartInfo.UseShellExecute = false;
            _currentBuildProcess.EnableRaisingEvents = true;
            _currentBuildProcess.OutputDataReceived += (o, e) =>
            {
                string text = e.Data;
                if (string.IsNullOrWhiteSpace(e.Data))
                {
                    return;
                }

                text = text.Trim();

                BuildInfoItem item = new BuildInfoItem() { Text = text };
                item.Level = BuildInfoItemLevel.Trace;
                if (text.Contains("->")) { item.Level = BuildInfoItemLevel.Info; }
                if (text.Contains("error")) { item.Level = BuildInfoItemLevel.Error; }
                if (text.Contains("warning")) { item.Level = BuildInfoItemLevel.Warning; }
                result.Items.Add(item);
            };

            _currentBuildProcess.Start();
            _currentBuildProcess.BeginOutputReadLine();
            _currentBuildProcess.WaitForExit();
            _currentBuildProcess = null;

            _isBuildInProgress = false;
            return result;
        }

        private void PerformClean(BuildItemNode buildItem)
        {
            string solutionFilePath = buildItem.GetFullPath();
            Process cleanProcess = new Process();
            cleanProcess.StartInfo.FileName = _msBuildPath;
            cleanProcess.StartInfo.Arguments = string.Format("\"{0}\" /t:Clean", solutionFilePath);
            cleanProcess.StartInfo.RedirectStandardOutput = true;
            cleanProcess.StartInfo.UseShellExecute = false;
            cleanProcess.Start();
            cleanProcess.WaitForExit();
        }

        void IProjectBuildHandler.CancelCurrentBuild()
        {
            if (_currentBuildProcess != null)
            {
                _currentBuildProcess.Kill();
            }
        }

        #endregion
    }
}