using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace ezBuilder
{
    class Program
    {
        private static string _bdprojFile;
        private static string _configuration;

        [STAThread()]
        static void Main(string[] args)
        {
            ParseArguments(args);

            bool showUI = string.IsNullOrWhiteSpace(_bdprojFile) && string.IsNullOrWhiteSpace(_configuration);

            if (showUI)
            {
                Application.EnableVisualStyles();
                using (MainForm form = new MainForm())
                {
                    Console.WriteLine("No options were specified. Starting client window...");
                    form.ShowDialog();
                }
            }
            else
            {
                // Duplicate hacked code to build via console
                string bdprojFile = Path.Combine(Assembly.GetExecutingAssembly().GetWorkingDirectory(), _bdprojFile);
                if (!File.Exists(bdprojFile))
                {
                    return;
                }

                Console.WriteLine("Parsing project file from {0}", bdprojFile);

                Project project = new Project();
                project.Parse(bdprojFile);
                BuildConfiguration configuration = project.Definitions.SelectMany(def => def.Configurations).FirstOrDefault(c => c.Name == _configuration);
                if (configuration == null)
                {
                    Console.WriteLine("Configuration {0} was not found!", _configuration);
                }

                // Setup build process
                ProjectBuildProcess buildProcess = new ProjectBuildProcess();
                BuildStartArgs buildStartArgs = new BuildStartArgs();
                buildStartArgs.ItemsToBuild = null;

                ManualResetEvent wait = new ManualResetEvent(false);

                buildProcess.BuildCompleted += (o) =>
                {
                    foreach (var item in o.Items)
                    {
                        switch (item.Level)
                        {
                            case BuildInfoItemLevel.Trace:
                                continue;
                            case BuildInfoItemLevel.Info:
                                break;
                            case BuildInfoItemLevel.Warning:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;
                            case BuildInfoItemLevel.Error:
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;
                        }

                        Console.WriteLine(item.Text);
                        Console.ResetColor();
                    }
                };
                buildProcess.BuildFinished += (e) =>
                {
                    wait.Set();
                    buildProcess.SaveBuildLog();
                };

                buildProcess.Start(configuration, buildStartArgs);
                wait.WaitOne();
            }
        }

        private static void ParseArguments(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                return;
            }
            
            List<string> tmp = new List<string>(args);
            while (tmp.Count > 0)
            {
                string arg = tmp[0];
                if (arg == "-p")
                {
                    // We have a bdproj file path!
                    _bdprojFile = tmp[1];
                    tmp.RemoveAt(0);

                    Console.WriteLine("Commandline: Using project file {0}.", _bdprojFile);
                }
                else if (arg == "-c")
                {
                    _configuration = tmp[1];
                    tmp.RemoveAt(0);

                    Console.WriteLine("Commandline: Using configuration {0}.", _configuration);
                }
                else
                {
                    Console.WriteLine("Unrecognized argument: {0}", arg);
                }

                tmp.RemoveAt(0);
            }
        }
    }
}
