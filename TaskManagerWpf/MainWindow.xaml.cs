using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskManagerWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TaskItem> tasks = new();
        private string filePath = "tasks.json";
        public MainWindow()
        {
            InitializeComponent();
            LoadTasks();
            RefreshTaskList();
        }

        private void LoadTasks()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
            }
        }
        private void RefreshTaskList()
        {
            TaskList.Items.Clear();
            foreach (var task in tasks.OrderBy(t => t.DueDate))
            {
                TaskList.Items.Add(task);
            }
        }
        private void SaveTasks()
        {
            string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(TitleInput.Text) || DueDateInput.SelectedDate == null || PriorityInput.SelectedIndex < 0)
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            TaskItem task = new TaskItem
            {
                Title = TitleInput.Text,
                DueDate = DueDateInput.SelectedDate.Value,
                Priority = (Priority)PriorityInput.SelectedIndex,
                IsCompleted = false
            };
            tasks.Add(task);
            SaveTasks();
            RefreshTaskList();

            TitleInput.Text = "";
            DueDateInput.SelectedDate = null;
            PriorityInput.SelectedIndex = -1;
        }
        private void MarkDone_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedIndex >= 0)
            {
                tasks[TaskList.SelectedIndex].IsCompleted = true;
                SaveTasks();
                RefreshTaskList();
            }
        }
        private void DeleteTask_Click(object obj, RoutedEventArgs e)
        {
            if (TaskList.SelectedIndex >= 0) 
            {
                tasks.RemoveAt(TaskList.SelectedIndex);
                SaveTasks(); 
                RefreshTaskList();
            }

        }
    }
}