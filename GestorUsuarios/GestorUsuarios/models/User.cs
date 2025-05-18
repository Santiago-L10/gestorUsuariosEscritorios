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
    public class User
    {
        public int IdUser { get; set; }
        public string Nickname { get; set; }
        public string PasswordHash { get; set; }
        public bool Estado { get; set; }
        public DateTime? FechaUltimoLogin { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int RolId { get; set; }
        public int PersonId { get; set; }
        public ICollection<TaskModel>? Tareas { get; set; }

        public User() { }

        
        public User(string nickname, string passwordHash, int tiempoActividad, int idRol, int idPerson, bool state, DateTime login, DateTime fechaCreacion) {
            Nickname = nickname;
            PasswordHash = passwordHash;
            RolId = idRol;
            PersonId = idPerson;
            Estado = state;
            GestorDeEstado.TemporizadorUsuario(this, tiempoActividad);
            FechaUltimoLogin = login;
            FechaCreacion = fechaCreacion;
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
