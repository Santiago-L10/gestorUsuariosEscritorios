using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorUsuarios.models
{
    public class TaskModel
    {
        public string Name { get; set; }
        public bool Completed { get; set; }
        public string AssignedTo { get; set; }
        public DateTime? CompletedTime { get; set; }

        public TaskModel() { }
        public TaskModel (string name, string assignedTo, bool stated)
        {
            Name = name;
            AssignedTo = assignedTo;
            Completed = stated;
        }
    }
}
