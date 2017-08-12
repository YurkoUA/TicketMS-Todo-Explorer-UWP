using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMS.TodoApi.Enums;
using TMS.TodoApi.Extensions;
using TMS.TodoApi.Interfaces;
using TMS.TodoApi.Models;
using TaskStatus = TMS.TodoApi.Enums.TaskStatus;

namespace TMS.TodoApi.Services
{
    public class TodoService : ITodoService
    {
        private IHttpService _httpService;

        public TodoService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<TodoTask>> GetTasksAsync()
        {
            return await _httpService.Client.GetJsonAsync<IEnumerable<TodoTask>>("Todo/GetAllTasks");
        }

        public async Task<IEnumerable<TodoTask>> GetTasksAsync(TaskStatus status)
        {
            return await _httpService.Client.GetJsonAsync<IEnumerable<TodoTask>>($"Todo/GetTasks/{(int)status}");
        }

        public async Task<Dictionary<TaskStatus, IEnumerable<TodoTask>>> GetTaskGroupByStatusAsync()
        {
            return await _httpService.Client.GetJsonAsync<Dictionary<TaskStatus, IEnumerable<TodoTask>>>("Todo/GetGroupedTasks");
        }

        public async Task<TodoTask> GetByIdAsync(int id)
        {
            return await _httpService.Client.GetJsonAsync<TodoTask>($"Todo/Get/{id}");
        }

        public async Task<TodoTask> CreateAsync(string title, string description, TaskPriority priority)
        {
            var obj = new
            {
                Title = title,
                Description = description,
                Priority = priority
            };

            return await _httpService.Client.PostAndReturnJsonAsync<object, TodoTask>("Todo/Create", obj);
        }

        public async Task UpdateAsync(int id, string title, string description)
        {
            await _httpService.Client.PutJsonAsync($"Todo/Edit/{id}", new
            {
                Title = title,
                Description = description
            });
        }

        public async Task DeleteAsync(int id)
        {
            await _httpService.Client.DeleteAsync($"Todo/Delete/{id}");
        }

        public async Task SetStatusAsync(int id, TaskStatus status)
        {
            await _httpService.Client.PutAsync($"Todo/SetStatus/{id}", new StringContent($"status={(int)status}"));
        }

        public async Task SetPriorityAsync(int id, TaskPriority priority)
        {
            await _httpService.Client.PutAsync($"Todo/SetPriority/{id}", new StringContent($"priority={(int)priority}"));
        }
    }
}
