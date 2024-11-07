namespace Task_Tracker_CLI.Task
{
    public class Task
    {

        //Each task should have the following properties:
        //id: A unique identifier for the task
        //description: A short description of the task
        //status: The status of the task(todo, in-progress, done)
        //createdAt: The date and time when the task was created
        //updatedAt: The date and time when the task was last updated

        private string id;
        private string description;
        private string status;
        private DateTime createdAt;
        private DateTime updatedAt;

        // Setter and getter
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public DateTime CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; }
        }

        public DateTime UdatetedAt
        {
            get { return updatedAt; }
            set { updatedAt = value; }
        }

        // Constructor with args
        public Task(string id, string description, string status, DateTime createdAt, DateTime updatedAt)
        {
            this.id = id;
            this.description = description;
            this.status = status;
            this.createdAt = createdAt;
            this.updatedAt = updatedAt;
        }

        // Constructor withour args
        public Task()
        {
            this.id = "";
            this.description = "";
            this.status = "todo";
            this.createdAt = DateTime.Now;
        }

        public void clearTask()
        {
            this.id = "";
            this.description = "";
            this.status = "todo";
            this.createdAt = DateTime.Now;
        }
    }
}
