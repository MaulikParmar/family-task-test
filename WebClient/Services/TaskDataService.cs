using System;
using System.Collections.Generic;
using System.Linq;
using WebClient.Abstractions;
using System.Net.Http;
using Domain.Commands;
using Domain.Queries;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Domain.ViewModel;

namespace WebClient.Services
{
    public class TaskDataService: ITaskDataService
    {
        private readonly HttpClient httpClient;

        public TaskDataService(IHttpClientFactory clientFactory)
        {
            Tasks = new List<TaskVm>();
            httpClient = clientFactory.CreateClient("FamilyTaskAPI");
            LoadTasks();
        }

        public IEnumerable<TaskVm> Tasks { get; private set; }
        public TaskVm SelectedTask { get; private set; }


        public event EventHandler TasksUpdated;
        public event EventHandler TaskSelected;
        public event EventHandler TaskChanged;

        private async void LoadTasks()
        {
            var tasks = (await GetAllTasks()).Payload;

            Tasks = tasks?.Where(x => x != null).ToList();
            TaskChanged?.Invoke(this, null);
        }

        public void SelectTask(Guid id)
        {
            SelectedTask = Tasks.SingleOrDefault(t => t.Id == id);            
            TasksUpdated?.Invoke(this, null);
        }

        public async void ToggleTask(Guid id)
        {
            foreach (TaskVm taskModel in Tasks)
            {
                if (taskModel.Id == id)
                {
                    taskModel.IsComplete = !taskModel.IsComplete;
                    await CompleteTask(taskModel);
                    LoadTasks();
                    break;
                }
            }

            TasksUpdated?.Invoke(this, null);
        }

        public async Task AddTask(CreateTaskCommand model)
        {
            var response = await Create(model);
            LoadTasks();
            TasksUpdated?.Invoke(this, null);
        }

        private async Task<CreateTaskCommandResult> Create(CreateTaskCommand command)
        {
            return await httpClient.PostJsonAsync<CreateTaskCommandResult>("tasks", command);
        }

        private async Task<GetAllTasksQueryResult> GetAllTasks()
        {
            return await httpClient.GetJsonAsync<GetAllTasksQueryResult>("tasks");
        }

        private async Task<UpdateTaskCommandResult> Update(UpdateMemberCommand command)
        {
            return await httpClient.PutJsonAsync<UpdateTaskCommandResult>($"tasks/{command.Id}", command);
        }

        private async Task<HttpResponseMessage> DeleteTask(Guid taskId)
        {
            return await httpClient.DeleteAsync($"tasks/{taskId}");
        }

        private async Task<CompleteTaskCommandResult> CompleteTask(TaskVm task)
        {
            return await httpClient.PostJsonAsync<CompleteTaskCommandResult>($"tasks/complete-task/{task.Id}", null);
        }

        private async Task<CompleteTaskCommandResult> AssignTask(AssignTaskCommand command)
        {
            return await httpClient.PostJsonAsync<CompleteTaskCommandResult>($"tasks/assign-task/", command);
        }

        public async Task OnDeleteTask(Guid taskId)
        {
            await DeleteTask(taskId);
            LoadTasks();
        }

        public async Task AssignTask(Guid taskId, Guid memberId)
        {
            var reponse = await AssignTask(new AssignTaskCommand()
            {
                TaskId = taskId,
                MemberId = memberId
            });

            LoadTasks();
        }
    }
}