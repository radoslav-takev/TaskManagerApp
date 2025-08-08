using TaskManagerApp;
using System.Text.Json;
using System.Xml;

List<TaskItem> tasks = new List<TaskItem>();
string filePath = "tasks.json";

bool exit = false;


void LoadTasks()
{
    if (File.Exists(filePath))
    {
        string json = File.ReadAllText(filePath);
        tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
    }
}
void SaveTasks()
{
    string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true});
    File.WriteAllText(filePath, json);
}

LoadTasks();

while (!exit)
{
    Console.WriteLine("\n=== Task Manager ===");
    Console.WriteLine("1. Add task");
    Console.WriteLine("2. List tasks");
    Console.WriteLine("3. List incomplete tasks");
    Console.WriteLine("4. Mark task as done");
    Console.WriteLine("5. Delete task");
    Console.WriteLine("6. Exit");
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
            ListTasks(onlyIncomplete: true);
            break;
        case "4":
            MarkTaskAsDone();
            break;
        case "5":
            DeleteTask();
            break;
        case "6":
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
    SaveTasks();
    Console.WriteLine("Task Added!");
}
void ListTasks(bool onlyIncomplete = false)
{
    var tasksToShow = tasks
        .Where(t => !onlyIncomplete || !t.IsCompleted)
        .OrderBy(t => t.DueDate)
        .ToList();

    if (tasks.Count == 0)
    {
        Console.WriteLine("No tasks yet.");
        return;
    }
    Console.WriteLine("\n--- Your Tasks ---");
    for (int i = 0; i < tasksToShow.Count; i++)
    {
        var task = tasksToShow[i];

        Console.ForegroundColor = task.Priority switch
        {
            Priority.High => ConsoleColor.Red,
            Priority.Medium => ConsoleColor.Yellow,
            Priority.Low => ConsoleColor.Green,
            _ => ConsoleColor.White
        };

        Console.WriteLine($"{i + 1}.{tasks[i]}");
        Console.ResetColor();
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
        SaveTasks();
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
        SaveTasks();
        Console.WriteLine("Task deleted!");
    }
    else
    {
        Console.WriteLine("Invalid task number");
    }
}