using System;

namespace TaskManagerApp
{
    public enum Priority
    {
        Low,
        Medium,
        High
    }
    public class TaskItem
    {
        public Priority Priority {  get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }


        public override string ToString()
        {
            return $"{Title} | Due: {DueDate:yyyy-MM-dd} | Priority: {Priority} | Completed: {IsCompleted}";
        }

    }
}
