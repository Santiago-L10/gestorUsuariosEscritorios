using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorUsuarios.models
{
    public enum EstadoUsuario { Activo, Inactivo, Bloqueado }
    public enum EstadoTarea { Pendiente, EnCurso, Completada, Vencida }
    class User
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string PasswordHash { get; set; }
        public bool Estado { get; set; }
        public DateTime? FechaUltimoLogin { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int RolId { get; set; }
        public int PersonId { get; set; }
        public ICollection<TaskItem>? Tareas { get; set; }
    
    public User(int id, string nombre, string passwordHash, int tiempoActividad, DateTime fechaCreacion, int rolId) {
            Id = id;
            Nombre = nombre;
            PasswordHash = passwordHash;
            GestorDeEstado.TemporizadorUsuario(this, tiempoActividad);
            FechaCreacion = fechaCreacion;
            RolId = rolId;
        }

        public void CambioDeEstado(bool estado) {
            if (estado) {
                Estado = true;
                Console.WriteLine("Usuario activo.");
            }
            else {
                Estado = false;
                Console.WriteLine("Usuario inactivo.");
            }
        }
    } 
}
