using GestorUsuarios.services;
using System;
using System.Collections.Generic;

namespace GestorUsuarios.models
{
    public enum EstadoUsuario { Activo, Inactivo, Bloqueado }
    public enum EstadoTarea { Pendiente, EnCurso, Completada, Vencida }

    public class User
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Email
        {
            get => Correo;
            set => Correo = value;
        }
        public string PasswordHash { get; set; }
        public bool Estado { get; set; }
        public int RolId { get; set; }
        public int PersonId { get; set; }
        public ICollection<TaskModel>? Tareas { get; set; }

        public User() { }

        public Rol Rol { get; set; }
        public string RolNombre
        {
            get => Rol?.Nombre;
            set
            {
                if (Rol == null)
                    Rol = new Rol(value);
                else
                    Rol.Nombre = value;
            }
        }

        public int IdPersona { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimoLogin { get; set; }

        
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

        public void CambioDeEstado(bool estado)
        {
            Estado = estado;
            Console.WriteLine(estado ? "Usuario activo." : "Usuario inactivo.");
        }
    }
}
