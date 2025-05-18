using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskModel = GestorUsuarios.models.TaskModel;

namespace GestorUsuarios.services
{
    public static class TaskGlobal
    {
        public static List<TaskModel> Tasks { get; set; } = new List<TaskModel>
        {
            
            new TaskModel { Name = "Actualizar software", AssignedTo = "pipejfdv", Completed = false },
            new TaskModel { Name = "software", AssignedTo = "pipejfdv", Completed = false },
            new TaskModel { Name = "AAAaActualizar software", AssignedTo = "pipejfdv", Completed = false },
            new TaskModel { Name = "Revisión de seguridad", AssignedTo = "Supervisor", Completed = false }
        };

        public static void MarkTaskCompleted(string nameTask, string user)
        {
            var task = Tasks.FirstOrDefault(t => t.Name == nameTask && t.AssignedTo == user);
            if(task != null)
            {
                task.Completed = true;
                task.CompletedTime = DateTime.Now;
            }
        }

        public static List<TaskModel> GetTasksForUser(string user) {
            return user == "Supervisor" ? Tasks : Tasks.Where(t => !t.Completed && t.AssignedTo == user).ToList();
        }
    }
}
