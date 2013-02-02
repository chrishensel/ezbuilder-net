namespace ezBuilder
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("(Project)");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.trvList = new System.Windows.Forms.TreeView();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbLoadProject = new System.Windows.Forms.ToolStripButton();
            this.tsbBuildProject = new System.Windows.Forms.ToolStripButton();
            this.tsbStopBuild = new System.Windows.Forms.ToolStripButton();
            this.clbBuildItems = new System.Windows.Forms.CheckedListBox();
            this.cmsBuildItems = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmBuildItems_SelectDeselectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBuildItems_SelectOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lsvOutput = new System.Windows.Forms.ListView();
            this.hedIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hedText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsOutput = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmOutputShowVerbose = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOutputShowErrorsOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrBuildTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslBuildStatus = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.cmsBuildItems.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.cmsOutput.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // trvList
            // 
            this.trvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvList.Location = new System.Drawing.Point(4, 19);
            this.trvList.Margin = new System.Windows.Forms.Padding(4);
            this.trvList.Name = "trvList";
            treeNode2.Name = "ROOT";
            treeNode2.Text = "(Project)";
            this.trvList.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.trvList.Size = new System.Drawing.Size(294, 233);
            this.trvList.TabIndex = 1;
            this.trvList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvList_AfterSelect);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.HelpVisible = false;
            this.propertyGrid1.Location = new System.Drawing.Point(4, 19);
            this.propertyGrid1.Margin = new System.Windows.Forms.Padding(4);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGrid1.Size = new System.Drawing.Size(296, 233);
            this.propertyGrid1.TabIndex = 2;
            this.propertyGrid1.ToolbarVisible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLoadProject,
            toolStripSeparator3,
            this.tsbBuildProject,
            toolStripSeparator2,
            this.tsbStopBuild});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(932, 27);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbLoadProject
            // 
            this.tsbLoadProject.Image = global::ezBuilder.Properties.Resources.openfolderHS;
            this.tsbLoadProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadProject.Name = "tsbLoadProject";
            this.tsbLoadProject.Size = new System.Drawing.Size(113, 24);
            this.tsbLoadProject.Text = "&Load project";
            this.tsbLoadProject.Click += new System.EventHandler(this.tsmProjectLoad_Click);
            // 
            // tsbBuildProject
            // 
            this.tsbBuildProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBuildProject.Name = "tsbBuildProject";
            this.tsbBuildProject.Size = new System.Drawing.Size(47, 24);
            this.tsbBuildProject.Text = "&Build";
            this.tsbBuildProject.Click += new System.EventHandler(this.tsmBuildProjects_Click);
            // 
            // tsbStopBuild
            // 
            this.tsbStopBuild.Image = global::ezBuilder.Properties.Resources._109_AllAnnotations_Error_16x16_72;
            this.tsbStopBuild.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStopBuild.Name = "tsbStopBuild";
            this.tsbStopBuild.Size = new System.Drawing.Size(111, 24);
            this.tsbStopBuild.Text = "&Cancel build";
            this.tsbStopBuild.Click += new System.EventHandler(this.tsmBuildStop_Click);
            // 
            // clbBuildItems
            // 
            this.clbBuildItems.ContextMenuStrip = this.cmsBuildItems;
            this.clbBuildItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbBuildItems.FormattingEnabled = true;
            this.clbBuildItems.IntegralHeight = false;
            this.clbBuildItems.Location = new System.Drawing.Point(4, 19);
            this.clbBuildItems.Margin = new System.Windows.Forms.Padding(4);
            this.clbBuildItems.Name = "clbBuildItems";
            this.clbBuildItems.Size = new System.Drawing.Size(294, 233);
            this.clbBuildItems.TabIndex = 5;
            // 
            // cmsBuildItems
            // 
            this.cmsBuildItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmBuildItems_SelectDeselectAll,
            this.tsmBuildItems_SelectOnly});
            this.cmsBuildItems.Name = "cmsBuildItems";
            this.cmsBuildItems.Size = new System.Drawing.Size(204, 52);
            // 
            // tsmBuildItems_SelectDeselectAll
            // 
            this.tsmBuildItems_SelectDeselectAll.Name = "tsmBuildItems_SelectDeselectAll";
            this.tsmBuildItems_SelectDeselectAll.Size = new System.Drawing.Size(203, 24);
            this.tsmBuildItems_SelectDeselectAll.Text = "Select/Deselect &All";
            this.tsmBuildItems_SelectDeselectAll.Click += new System.EventHandler(this.tsmBuildItems_SelectDeselectAll_Click);
            // 
            // tsmBuildItems_SelectOnly
            // 
            this.tsmBuildItems_SelectOnly.Name = "tsmBuildItems_SelectOnly";
            this.tsmBuildItems_SelectOnly.Size = new System.Drawing.Size(203, 24);
            this.tsmBuildItems_SelectOnly.Text = "Select &Only";
            this.tsmBuildItems_SelectOnly.Click += new System.EventHandler(this.tsmBuildItems_SelectOnly_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(932, 529);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.propertyGrid1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(624, 4);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(304, 256);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Properties";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.clbBuildItems);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(314, 4);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(302, 256);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Build items";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.trvList);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(4, 4);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(302, 256);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Project structure";
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 3);
            this.groupBox1.Controls.Add(this.lsvOutput);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(4, 268);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(924, 257);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output";
            // 
            // lsvOutput
            // 
            this.lsvOutput.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hedIcon,
            this.hedText});
            this.lsvOutput.ContextMenuStrip = this.cmsOutput;
            this.lsvOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvOutput.FullRowSelect = true;
            this.lsvOutput.GridLines = true;
            this.lsvOutput.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsvOutput.Location = new System.Drawing.Point(4, 19);
            this.lsvOutput.Margin = new System.Windows.Forms.Padding(4);
            this.lsvOutput.Name = "lsvOutput";
            this.lsvOutput.Size = new System.Drawing.Size(916, 234);
            this.lsvOutput.TabIndex = 0;
            this.lsvOutput.UseCompatibleStateImageBehavior = false;
            this.lsvOutput.View = System.Windows.Forms.View.Details;
            // 
            // hedIcon
            // 
            this.hedIcon.Text = "";
            this.hedIcon.Width = 32;
            // 
            // hedText
            // 
            this.hedText.Text = "Text";
            this.hedText.Width = 627;
            // 
            // cmsOutput
            // 
            this.cmsOutput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmOutputShowVerbose,
            this.tsmOutputShowErrorsOnly});
            this.cmsOutput.Name = "cmsOutput";
            this.cmsOutput.Size = new System.Drawing.Size(189, 52);
            // 
            // tsmOutputShowVerbose
            // 
            this.tsmOutputShowVerbose.CheckOnClick = true;
            this.tsmOutputShowVerbose.Name = "tsmOutputShowVerbose";
            this.tsmOutputShowVerbose.Size = new System.Drawing.Size(188, 24);
            this.tsmOutputShowVerbose.Text = "Show &Verbose";
            this.tsmOutputShowVerbose.CheckedChanged += new System.EventHandler(this.tsmOutputShowVerbose_CheckedChanged);
            // 
            // tsmOutputShowErrorsOnly
            // 
            this.tsmOutputShowErrorsOnly.Checked = true;
            this.tsmOutputShowErrorsOnly.CheckOnClick = true;
            this.tsmOutputShowErrorsOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmOutputShowErrorsOnly.Name = "tsmOutputShowErrorsOnly";
            this.tsmOutputShowErrorsOnly.Size = new System.Drawing.Size(188, 24);
            this.tsmOutputShowErrorsOnly.Text = "Show &Errors only";
            this.tsmOutputShowErrorsOnly.CheckedChanged += new System.EventHandler(this.tsmOutputShowErrorsOnly_CheckedChanged);
            // 
            // tmrBuildTimer
            // 
            this.tmrBuildTimer.Interval = 1000;
            this.tmrBuildTimer.Tick += new System.EventHandler(this.tmrBuildTimer_Tick);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tableLayoutPanel1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(932, 529);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(932, 581);
            this.toolStripContainer1.TabIndex = 7;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslBuildStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(932, 25);
            this.statusStrip1.TabIndex = 0;
            // 
            // tsslBuildStatus
            // 
            this.tsslBuildStatus.Name = "tsslBuildStatus";
            this.tsslBuildStatus.Size = new System.Drawing.Size(127, 20);
            this.tsslBuildStatus.Text = "(No build started)";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 581);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "ezBuilder";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.cmsBuildItems.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.cmsOutput.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView trvList;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbBuildProject;
        private System.Windows.Forms.ToolStripButton tsbStopBuild;
        private System.Windows.Forms.ToolStripButton tsbLoadProject;
        private System.Windows.Forms.CheckedListBox clbBuildItems;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lsvOutput;
        private System.Windows.Forms.ColumnHeader hedIcon;
        private System.Windows.Forms.ColumnHeader hedText;
        private System.Windows.Forms.ContextMenuStrip cmsOutput;
        private System.Windows.Forms.ToolStripMenuItem tsmOutputShowVerbose;
        private System.Windows.Forms.ContextMenuStrip cmsBuildItems;
        private System.Windows.Forms.ToolStripMenuItem tsmBuildItems_SelectDeselectAll;
        private System.Windows.Forms.ToolStripMenuItem tsmBuildItems_SelectOnly;
        private System.Windows.Forms.ToolStripMenuItem tsmOutputShowErrorsOnly;
        private System.Windows.Forms.Timer tmrBuildTimer;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslBuildStatus;
    }
}