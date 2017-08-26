using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Trukman.Extensions
{
    #region TaskExtension
    public static class TaskExtension
    {
        public static void LogExceptions(this Task task, string taskName)
        {
            task.ContinueWith(t =>
            {
                var aggException = t.Exception.Flatten();
                foreach (var exception in aggException.InnerExceptions)
                    Console.WriteLine("{0}: {1}", taskName, exception);
            },
            TaskContinuationOptions.OnlyOnFaulted);
        }
    }
    #endregion
}
