using TaskManagerApp;

List<TaskItem> tasks = new List<TaskItem>();

bool exit = false;


while (!exit)
{
    Console.WriteLine("\n=== Task Manager ===");
    Console.WriteLine("1. Add task");
    Console.WriteLine("2. List tasks");
    Console.WriteLine("3. Exit");
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
            exit = true;
            Console.WriteLine("Bye Bye");
            break;
        default:
            Console.WriteLine("Invalid choice.Try again from 1 to 3");
            break;
    }
    void AddTask()
    {
        Console.Write("Enter task title: ");
        string title = Console.ReadLine();

        Console.Write("End due date (yyyy-mm-dd): ");
        DateTime dueDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Enter Priority(Low,Medium,High): ");
        Priority priority = Enum.Parse<Priority>(Console.ReadLine(),true);

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
}