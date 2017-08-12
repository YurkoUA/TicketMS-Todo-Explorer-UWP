using System;
using TMS.TodoApi.Enums;

namespace TMS.TodoApi.Models
{
    public class TodoTask
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }

        public string PriorityString { get; set; }
        public string StatusString { get; set; }

        public string LocalDateString => Date.ToLocalTime().ToString("d");

        #region System.Object methods

        public override string ToString()
        {
            return Title;
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is TodoTask task)
            {
                return task.Title.Equals(Title, StringComparison.CurrentCultureIgnoreCase);
            }
            return false;
        }

        #endregion
    }
}
