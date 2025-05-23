﻿using Microsoft.Build.Utilities;
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
        public int IdUser { get; set; }
        public string Nickname { get; set; }
        public string PasswordHash { get; set; }
        public bool Estado { get; set; }
        public DateTime? FechaUltimoLogin { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int RolId { get; set; }
        public int PersonId { get; set; }
        public ICollection<TaskItem>? Tareas { get; set; }

        public User() { }

    
    public User(int id, string nickname, string passwordHash, int tiempoActividad, DateTime fechaCreacion, int rolId) {
            IdUser = id;
            Nickname = nickname;
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
