using Microsoft.Win32.TaskScheduler;
using System;
using System.Windows.Forms;
using Task = Microsoft.Win32.TaskScheduler.Task;

namespace GPDWin2XTUManager
{
    public static class StartupTaskManager
    {
        public static bool TaskExists()
        {
            bool exists = false;

            using (TaskService ts = new TaskService())
            {
                Task t = ts.GetTask("GPDWin2XTUManager");
                exists = (t != null);
            }

            return exists;
        }

        public static string GetTaskParameter()
        {
            string profile = "";

            using (TaskService ts = new TaskService())
            {
                Task t = ts.GetTask(Shared.APP_NAME_VALUE);
                profile = ((ExecAction)t.Definition.Actions[0]).Arguments;
            }

            return profile;
        }

        public static void CreateTask(string profileName)
        {
            using (TaskService ts = new TaskService())
            {
                // Create a new task definition and assign properties
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = Shared.APP_NAME_VALUE;
                td.Principal.LogonType = TaskLogonType.InteractiveToken;
                td.Principal.RunLevel = TaskRunLevel.Highest;
                td.Principal.UserId = "SYSTEM";

                // Add a trigger that will fire the task at logon
                td.Triggers.Add(new LogonTrigger() { Delay = new TimeSpan(0, 0, 30) });

                // Add an action that will launch the app whenever the trigger fires
                td.Actions.Add(new ExecAction(Application.ExecutablePath, profileName, null));

                td.Settings.DisallowStartIfOnBatteries = false;

                // Register the task in the root folder
                string taskName = Shared.APP_NAME_VALUE;
                ts.RootFolder.RegisterTaskDefinition(taskName, td);
            }
        }

        public static void DeleteTask()
        {
            using (TaskService ts = new TaskService())
            {
                ts.RootFolder.DeleteTask(Shared.APP_NAME_VALUE);
            }
        }
    }
}