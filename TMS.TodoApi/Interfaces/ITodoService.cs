using System.Collections.Generic;
using System.Threading.Tasks;
using TMS.TodoApi.Enums;
using TMS.TodoApi.Models;
using TaskStatus = TMS.TodoApi.Enums.TaskStatus;

namespace TMS.TodoApi.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoTask>> GetTasksAsync();
        Task<IEnumerable<TodoTask>> GetTasksAsync(TaskStatus status);
        Task<Dictionary<TaskStatus, IEnumerable<TodoTask>>> GetTaskGroupByStatusAsync();

        Task<TodoTask> GetByIdAsync(int id);

        Task<TodoTask> CreateAsync(string title, string description, TaskPriority priority);
        Task UpdateAsync(int id, string title, string description);
        Task DeleteAsync(int id);

        Task SetStatusAsync(int id, TaskStatus status);
        Task SetPriorityAsync(int id, TaskPriority priority);
    }
}
