using TaskManagerApp;

List<TaskItem> tasks = new List<TaskItem>();

bool exit = false;


while (!exit)
{
    Console.WriteLine("\n=== Task Manager ===");
    Console.WriteLine("1. Add task");
    Console.WriteLine("2. List tasks");
    Console.WriteLine("3. Mark task as done");
    Console.WriteLine("4. Delete task");
    Console.WriteLine("5. Exit");
    Console.WriteLine("Choose an option: ");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            AddTask();
            break;
        case "2":
            ListTasks();
            break;
        case "3":
            MarkTaskAsDone();
            break;
        case "4":
            DeleteTask();
            break;
        case "5":
            exit = true;
            Console.WriteLine("Bye Bye");
            break;
        default:
            Console.WriteLine("Invalid choice.Try again from 1 to 3");
            break;
    }
}

void AddTask()
{
    Console.Write("Enter task title: ");
    string title = Console.ReadLine();

    Console.Write("End due date (yyyy-mm-dd): ");
    DateTime dueDate = DateTime.Parse(Console.ReadLine());

    Console.Write("Enter Priority(Low,Medium,High): ");
    Priority priority = Enum.Parse<Priority>(Console.ReadLine(), true);

    TaskItem task = new TaskItem()
    {
        Title = title,
        DueDate = dueDate,
        Priority = priority,
        IsCompleted = false
    };

    tasks.Add(task);
    Console.WriteLine("Task Added!");
}
void ListTasks()
{
    if (tasks.Count == 0)
    {
        Console.WriteLine("No tasks yet.");
        return;
    }
    Console.WriteLine("\n--- Your Tasks ---");
    for (int i = 0; i < tasks.Count; i++)
    {
        Console.WriteLine($"{i + 1}.{tasks[i]}");
    }
}
void MarkTaskAsDone()
{
    if (tasks.Count == 0) 
    {
        Console.WriteLine("No tasks to mark.");
    }
    ListTasks();
    Console.WriteLine("Enter the task number to mark as done: ");

    if(int.TryParse(Console.ReadLine(),out int index) && index > 0 && index <= tasks.Count){

        tasks[index - 1].IsCompleted = true;
        Console.WriteLine("Task marked as completed");
    }
    else
    {
        Console.WriteLine("Invalid task number");
    }
}

void DeleteTask()
{
    if (tasks.Count == 0)
    {
        Console.WriteLine("No tasks to delete.");
        return;
    }
    ListTasks();
    Console.WriteLine("Enter the task number to delete: ");

    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= tasks.Count)
    {
        tasks.RemoveAt(index - 1);
        Console.WriteLine("Task deleted!");
    }
    else
    {
        Console.WriteLine("Invalid task number");
    }
}