using Task_Tracker_CLI;

class Program
{
    static string path = @"./Tasks.json";
    static void Main(string[] args)
    {
        TaskService TaskTracker = new TaskService(path);
        TaskTracker.Start();
    }
}