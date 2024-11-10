using Task_Tracker_CLI;

namespace TestTaskTrackerCLI
{
    [TestClass]
    public class UnitTest1
    {
        
        private StringWriter consoleOutput;
        private StringReader consoleInput;
        static string path = @"./Task.json";
        TaskService TaskTracker = new TaskService(path);

        [TestInitialize]
        public void Setup()
        {
            // Redirect console output to capture it
            consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Reset console to default
            consoleOutput.Dispose();
            if (consoleInput != null)
            {
                consoleInput.Dispose();
            }
        }

        [TestMethod]
        public void Input_ReturnsError_WhenInputIsEmpty()
        {
            SetConsoleInput("");
            var result = TaskTracker.input();
            Assert.AreEqual("", result);
            StringAssert.Contains(consoleOutput.ToString(), "Please enter your valid command!");
        }

        [TestMethod]
        public void Input_ReturnsExit_WhenInputIsExit()
        {
            SetConsoleInput("exit");
            var result = TaskTracker.input();
            Assert.AreEqual("exit", result);
        }

        [TestMethod]
        public void Input_ReturnsError_WhenCommandIsInvalid()
        {
            SetConsoleInput("invalidCommand param");
            var result = TaskTracker.input();
            Assert.AreEqual("", result);
            StringAssert.Contains(consoleOutput.ToString(), "task-cli # Output: $'invalidCommand param' is invalid command!");
        }

        [TestMethod]
        public void Input_ReturnsError_WhenCommandHasNoContent()
        {
            SetConsoleInput("add ");
            var result = TaskTracker.input();
            Assert.AreEqual("", result);
            StringAssert.Contains(consoleOutput.ToString(), "Content is empty!");
        }

        [TestMethod]
        public void Input_ReturnsError_WhenAddOrUpdateContentIsInvalid()
        {
            SetConsoleInput("add invalidContent");
            var result = TaskTracker.input();
            Assert.AreEqual("", result);
            StringAssert.Contains(consoleOutput.ToString(), "Content is invalid, please try again!");
        }

        [TestMethod]
        public void Input_ReturnsInput_WhenAddOrUpdateContentIsValid()
        {
            SetConsoleInput("add \"Valid Content\"");
            var result = TaskTracker.input();
            Assert.AreEqual("", result);
        }

        private void SetConsoleInput(string input)
        {
            consoleInput = new StringReader(input);
            Console.SetIn(consoleInput);
        }
    }
}