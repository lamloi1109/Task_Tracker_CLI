using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace Task_Tracker_CLI
{
    public class JsonHelper
    {
        private string path;

        public string Path
        {
            set { path = value; }
            get { return path; }
        }

        public JsonHelper(string path)
        {
            this.path = path;
        }

        // Read & Write Data from json file

        // Check exist
        public bool isExisted()
        {
            return File.Exists(this.Path);
        }

        // Read Json File 
        public List<Task>  GetData()
        {
            // Kiểm tra xem file đã tồn tại hay chưa
            if (!this.isExisted()) File.Create(this.Path);

            string json = File.ReadAllText(this.Path);

            if (string.IsNullOrEmpty(json)) json = $"[]";

            List<Task> tasks = JsonSerializer.Deserialize<List<Task>>(json) ?? new List<Task>();

            return tasks;
        }

        // Write Json File
        public void SetData(List<Task> tasks)
        {
            if (!this.isExisted()) File.Create(this.Path);
            string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true});
            File.WriteAllText(this.Path, json);
        }

    }
}
