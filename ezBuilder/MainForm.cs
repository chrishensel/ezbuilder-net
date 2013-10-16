using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ezBuilder.Shared.Build;
using ezBuilder.Shared.Core;
using ezBuilder.Shared.Project;

namespace ezBuilder
{
    /// <summary>
    /// Represents the main form.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Fields

        private ProjectNode _project;
        private ProjectBuildProcess _buildProcess;

        private bool _outputShowErrorsOnly = true;
        private bool _outputShowVerbose;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            _buildProcess = new ProjectBuildProcess();
            _buildProcess.BuildCompleted += BuildProcess_BuildCompleted;
            _buildProcess.BuildFinished += BuildProcess_BuildFinished;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.FormClosing"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> that contains the event data.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (_buildProcess.IsRunning)
            {
                e.Cancel = true;
                MessageBox.Show(Properties.Resources.FormCloseWarning_BuildIsRunning, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CanBuild()
        {
            if (_project == null
                || _buildProcess.IsRunning)
            {
                return false;
            }

            if (trvList.SelectedNode == null
                || trvList.SelectedNode.Name == "ROOT")
            {
                return false;
            }

            return trvList.SelectedNode.Tag is BuildConfigurationNode;
        }

        #endregion

        #region Event handlers

        private void tsmExit_Click(object sender, EventArgs e)
        {
            _buildProcess.Stop();
            Application.Exit();
        }

        private void tsmProjectLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Project files (*.bdproj)|*.bdproj";
                dialog.InitialDirectory = Assembly.GetExecutingAssembly().GetWorkingDirectory();

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    _project = new ProjectNode();
                    _project.Parse(dialog.FileName);

                    // build treeview
                    trvList.Nodes["ROOT"].Nodes.Clear();

                    TreeNode tnProject = new TreeNode("Project [ '" + _project.Type + "' ]");
                    tnProject.Tag = _project;
                    tnProject.ForeColor = Color.Gray;
                    foreach (BuildDefinitionNode def in _project.Definitions)
                    {
                        TreeNode tnDefinition = new TreeNode(def.Name);
                        tnDefinition.ToolTipText = def.Description;
                        tnDefinition.Tag = def;
                        tnDefinition.ForeColor = Color.Gray;

                        foreach (BuildConfigurationNode conf in def.Configurations)
                        {
                            TreeNode tnConfiguration = new TreeNode(conf.Name + " [ '" + conf.Configuration + "' ]");
                            tnConfiguration.Tag = conf;

                            tnDefinition.Nodes.Add(tnConfiguration);
                        }

                        tnProject.Nodes.Add(tnDefinition);
                    }

                    trvList.Nodes["ROOT"].Nodes.Add(tnProject);

                    trvList.ExpandAll();
                }
            }
        }

        private void trvList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.clbBuildItems.Items.Clear();

            if (trvList.SelectedNode == null
                || trvList.SelectedNode.Name == "ROOT")
            {
                this.propertyGrid1.SelectedObject = null;
                return;
            }

            BuildConfigurationNode configuration = trvList.SelectedNode.Tag as BuildConfigurationNode;
            if (configuration == null)
            {
                this.propertyGrid1.SelectedObject = null;
                return;
            }
            this.propertyGrid1.SelectedObject = configuration;

            foreach (BuildItemNode item in configuration.BuildItems)
            {
                clbBuildItems.Items.Add(new BuildItemEntry() { Item = item }, true);
            }

            tsbBuildProject.Enabled = CanBuild();
        }

        private void tsmBuildProjects_Click(object sender, EventArgs e)
        {
            if (!CanBuild())
            {
                return;
            }

            lsvOutput.Items.Clear();

            BuildStartArgs args = new BuildStartArgs();
            args.ItemsToBuild = new List<string>();
            // TODO
            foreach (BuildItemEntry item in clbBuildItems.CheckedItems)
            {
                args.ItemsToBuild.Add(item.Item.Path);
            }

            _buildProcess.Start((BuildConfigurationNode)trvList.SelectedNode.Tag, args);

            tmrBuildTimer.Start();
        }

        private void tsmBuildStop_Click(object sender, EventArgs e)
        {
            if (!_buildProcess.IsRunning)
            {
                return;
            }

            _buildProcess.Stop();
            tmrBuildTimer.Stop();
        }

        private void BuildProcess_BuildCompleted(BuildResult result)
        {
            this.lsvOutput.Invoke((Action)(() =>
            {
                lsvOutput.BeginUpdate();
                lsvOutput.Items.AddRange(CreateListViewItems(result));

                if (lsvOutput.Items.Count > 0)
                {
                    lsvOutput.EnsureVisible(lsvOutput.Items.Count - 1);
                }

                lsvOutput.EndUpdate();
            }));
        }

        private void BuildProcess_BuildFinished(BuildCompletionType completionType)
        {
            _buildProcess.SaveBuildLog();
            tmrBuildTimer.Stop();
        }

        private ListViewItem[] CreateListViewItems(object o)
        {
            List<ListViewItem> items = new List<ListViewItem>();

            BuildResult r = (BuildResult)o;
            foreach (BuildInfoItem item in r.Items)
            {
                // If show only errors
                if (_outputShowErrorsOnly && (item.Level != BuildInfoItemLevel.Warning
                    && item.Level != BuildInfoItemLevel.Error))
                {
                    continue;
                }

                ListViewItem lvi = new ListViewItem("");
                lvi.SubItems.Add(item.Text);
                lvi.Tag = item;

                switch (item.Level)
                {
                    case BuildInfoItemLevel.Trace:
                        if (!_outputShowVerbose)
                        {
                            continue;
                        }
                        lvi.ForeColor = Color.Gray;
                        break;
                    case BuildInfoItemLevel.Info:
                        break;
                    case BuildInfoItemLevel.Warning:
                        lvi.BackColor = Color.Yellow;
                        break;
                    case BuildInfoItemLevel.Error:
                        lvi.BackColor = Color.Red;
                        break;
                }

                items.Add(lvi);
            }


            return items.ToArray();
        }

        private void tsmOutputShowVerbose_CheckedChanged(object sender, EventArgs e)
        {
            _outputShowVerbose = tsmOutputShowVerbose.Checked;
        }

        private void tsmOutputShowErrorsOnly_CheckedChanged(object sender, EventArgs e)
        {
            _outputShowErrorsOnly = tsmOutputShowErrorsOnly.Checked;
        }

        private void tsmBuildItems_SelectOnly_Click(object sender, EventArgs e)
        {
            int selection = clbBuildItems.SelectedIndex;
            if (selection == -1)
            {
                return;
            }

            for (int i = 0; i < clbBuildItems.Items.Count; i++)
            {
                bool value = false;
                if (i == selection)
                {
                    value = true;
                }

                clbBuildItems.SetItemChecked(i, value);
            }
        }

        private void tsmBuildItems_SelectDeselectAll_Click(object sender, EventArgs e)
        {
            tsmBuildItems_SelectDeselectAll.Checked = !tsmBuildItems_SelectDeselectAll.Checked;

            bool value = tsmBuildItems_SelectDeselectAll.Checked;
            for (int i = 0; i < clbBuildItems.Items.Count; i++)
            {
                clbBuildItems.SetItemChecked(i, value);
            }
        }

        private void tmrBuildTimer_Tick(object sender, EventArgs e)
        {
            tsslBuildStatus.Text = string.Format("{0}", DateTime.UtcNow - _buildProcess.BuildStarted);
        }

        #endregion

        #region Nested types

        class BuildItemEntry
        {
            public BuildItemNode Item { get; set; }
            public override string ToString()
            {
                return string.IsNullOrWhiteSpace(Item.Name) ? Item.Path : Item.Name;
            }
        }

        #endregion

    }
}
