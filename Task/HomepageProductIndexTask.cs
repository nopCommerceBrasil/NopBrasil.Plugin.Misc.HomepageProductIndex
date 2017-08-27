using Nop.Core.Domain.Tasks;
using Nop.Services.Tasks;
using NopBrasil.Plugin.Misc.HomepageProductIndex.Service;

namespace NopBrasil.Plugin.Misc.HomepageProductIndex.Task
{
    public class HomepageProductIndexTask : ITask
    {
        private readonly IScheduleTaskService _scheduleTaskService;
        private readonly HomepageProductIndexSettings _productIndexSettings;
        private readonly IHomepageProductIndexService _homepageProductIndexService;

        public HomepageProductIndexTask(IScheduleTaskService scheduleTaskService, HomepageProductIndexSettings productIndexSettings, IHomepageProductIndexService homepageProductIndexService)
        {
            this._scheduleTaskService = scheduleTaskService;
            this._productIndexSettings = productIndexSettings;
            this._homepageProductIndexService = homepageProductIndexService;
        }

        public void Execute() => _homepageProductIndexService.Index();

        private ScheduleTask GetScheduleTask()
        {
            ScheduleTask task = _scheduleTaskService.GetTaskByType(GetTaskType());
            if (task == null)
            {
                task = new ScheduleTask()
                {
                    Type = GetTaskType(),
                    Name = "Homepage Product Index",
                    Enabled = true,
                    StopOnError = false,
                    Seconds = 3600
                };
            }
            return task;
        }

        private string GetTaskType() => "NopBrasil.Plugin.Misc.HomepageProductIndex.Task.HomepageProductIndexTask, NopBrasil.Plugin.Misc.HomepageProductIndex";

        public void InstallTask()
        {
            ScheduleTask scheduleTask = GetScheduleTask();
            if (scheduleTask.Id > 0)
                _scheduleTaskService.UpdateTask(scheduleTask);
            else
                _scheduleTaskService.InsertTask(scheduleTask);

            RestartAllTasks();
        }

        public void UninstallTask()
        {
            ScheduleTask scheduleTask = GetScheduleTask();
            if (scheduleTask.Id > 0)
            {
                _scheduleTaskService.DeleteTask(scheduleTask);
                RestartAllTasks();
            }
        }

        private void RestartAllTasks()
        {
            TaskManager.Instance.Stop();
            TaskManager.Instance.Initialize();
            TaskManager.Instance.Start();
        }
    }
}
