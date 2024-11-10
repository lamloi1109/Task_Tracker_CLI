using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task_Tracker_CLI
{
    public class TaskService
    {
        private List<Task> tasks;
        private JsonHelper jsonHelper;
        private List<string> commandList = new List<string>() {
            "add",
            "update",
            "delete",
            "mark-in-progress",
            "mark-done",
            "list",
            "list done",
            "list todo",
            "list in-progress"
        };
        public List<Task> Tasks
        {
            get
            {
                if (this.tasks == null) this.tasks = this.jsonHelper.GetData();
                return this.tasks;
            }
        }

        public TaskService(string path)
        {
            this.jsonHelper = new JsonHelper(path);
            this.tasks = jsonHelper.GetData();
        }

        public void ClearTasks()
        {
            this.Tasks.Clear();
            jsonHelper.SetData(this.tasks);
        }

        // Logic for console-app 
        public void AddTask(string Description)
        {
            // Kiểm tra xem đã có task nào với nội dung tương tự trong list hay chưa
            string errorMessage = "";
            var result = this.tasks.Find(task => task.Description == Description.Trim('"'));

            // Task đã tồn tại 
            if( result != null )
            {
                errorMessage = "This task already exists in the list.";
                ShowOutput(errorMessage);
                return;
            }

            int id = this.Tasks.Count + 1;
            Task currentTask = new Task();
            currentTask.Description = Description.Trim('"');
            currentTask.Id = id;
            currentTask.CreatedAt = DateTime.Now;
            currentTask.UdatetedAt = DateTime.Now;
            currentTask.Status = "todo";
            this.tasks.Add(currentTask);
            jsonHelper.SetData(this.tasks);
        }


        public void ShowOutput(string message)
        {
            string output = $"# Output: ${message}";
            Console.WriteLine(output);
        }

        public string input()
        {
            try
            {

                Console.Write("task-cli ");
                string input = Console.ReadLine() ?? "";
                string errorMessage = $"'{input}' is invalid command!";

                if (string.IsNullOrWhiteSpace(input))
                {
                    errorMessage = "Please enter your valid command!";
                    ShowOutput(errorMessage);
                    return "";
                };

                if (input.Equals("exit"))
                {
                    return input;
                }


                if (input.IndexOf(" ") == -1)
                {
                    ShowOutput(errorMessage);
                    return "";
                };

                int lastSpace = input.LastIndexOf(" ");
                if (lastSpace == -1) ShowOutput(errorMessage);
                string command = input.Substring(0, lastSpace);
                string content = input.Substring(lastSpace + 1);

                if (!commandList.Contains(command))
                {
                    ShowOutput(errorMessage);
                    return "";
                }

                if (string.IsNullOrEmpty(content))
                {
                    errorMessage = "Content is empty!";
                    ShowOutput(errorMessage);
                    return "";
                }

                if (command.Contains("add") || command.Contains("update"))
                {
                    string pattern = @"^\""[A-Za-z\d].*.\""$";

                    Regex regex = new Regex(pattern);
                    if (!regex.IsMatch(content))
                    {
                        errorMessage = "Content is invalid, please try again!";
                        ShowOutput(errorMessage);
                        return "";
                    }
                }

                return input;
            }
            catch (Exception ex)
            {
                ShowOutput(ex.ToString());
            }
            return "";
        }

        public void showMenu()
        {
            Console.Clear();
            Console.WriteLine("The list of commands:");
            Console.WriteLine("add \"Task\"");
            Console.WriteLine("update \"Task\"");
            Console.WriteLine("delete taskId");
            Console.WriteLine("mark-in-progress taskId");
            Console.WriteLine("mark-done taskId");
            Console.WriteLine("list");
            Console.WriteLine("list done");
            Console.WriteLine("list todo");
            Console.WriteLine("list in-progress");
            Console.WriteLine("exit");
        }

        public void Start()
        {
            showMenu();
            while (true)
            {
                string commands = input();

                if (string.IsNullOrEmpty(commands)) continue;

                if (commands.Equals("exit"))
                {
                    break;
                }

                string content = getContentFromCommand(commands);

                if (commands.Contains("add")) AddTask(content);
            }

            // Add task
            // Update task
            // Delete task
            // Mark a task as in progress or done
            // List all tasks
            // List all tasks that are done
            // List all tasks that are not done
            // List all tasks that are in progress
        }

        public string getContentFromCommand(string command)
        {
            if (string.IsNullOrEmpty(command) || string.IsNullOrWhiteSpace(command))
            {
                ShowOutput("Invalid Command!");
                return "";
            }

            int lastSpace = command.LastIndexOf(" ");
            if (lastSpace == -1)
            {
                ShowOutput("Invalid Command!");
                return "";
            }

            string content = command.Substring(lastSpace + 1);

            return content;
        }


    }
}
