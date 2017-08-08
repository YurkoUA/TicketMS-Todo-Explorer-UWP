using System.Collections.Generic;
using System.Threading.Tasks;
using TMS.TodoApi.Enums;
using TMS.TodoApi.Models;
using TaskStatus = TMS.TodoApi.Enums.TaskStatus;

namespace TMS.TodoApi.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoTask>> GetTasks();
        Task<IEnumerable<TodoTask>> GetTasks(TaskStatus status);
        Task<Dictionary<TaskStatus, IEnumerable<TodoTask>>> GetTaskGroupByStatus();

        Task<TodoTask> GetById(int id);

        Task<TodoTask> Create(string title, string description, TaskPriority priority);
        Task Update(string title, string description);
        Task Delete(int id);

        Task SetStatus(int id, TaskStatus status);
        Task SetPriority(int id, TaskPriority priority);
    }
}
