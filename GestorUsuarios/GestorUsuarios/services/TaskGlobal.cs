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
            
            new TaskModel ("Actualizar software", "pipejfdv", false, "revisar versiones",DateTime.Now ),
            new TaskModel("software", "pipejfdv", false,"framework por incorporar", DateTime.Now),
            new TaskModel("AAAaActualizar software", "pipejfdv", false, "Prueba 1",DateTime.Now),
            new TaskModel("Revisión de seguridad", "Supervisor", false, "Prueba 2", DateTime.Now)
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
